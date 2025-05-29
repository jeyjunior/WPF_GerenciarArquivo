using GerenciarArquivo.Application;
using GerenciarArquivo.Application.Interface;
using GerenciarArquivo.Domain.Entidades;
using GerenciarArquivo.Domain.Enumerador;
using GerenciarArquivo.ViewModel.ViewModel;
using JJ.NET.Core.Extensoes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

namespace GerenciarArquivo
{
    public partial class MainWindow : Window
    {
        #region Interfaces
        private readonly IArquivoAppService arquivoAppService;
        private readonly IParametroAppService parametroAppService;
        private readonly IDestinoAppService destinoAppService;
        #endregion

        #region Propriedades
        public MainWindowViewModel ViewModel { get; set; }
        IEnumerable<Parametro> parametros;
        #endregion

        #region Construtor
        public MainWindow()
        {
            InitializeComponent();

            arquivoAppService = Bootstrap.ServiceProvider.GetService<IArquivoAppService>();
            destinoAppService = Bootstrap.ServiceProvider.GetService<IDestinoAppService>();
            parametroAppService = Bootstrap.ServiceProvider.GetService<IParametroAppService>();
        }
        #endregion

        #region Eventos
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel = new MainWindowViewModel();
                this.RootGrid.DataContext = ViewModel;

                CarregarArquivosDaBase();
                CarregarParametrosDaBase();
                CarregarDestinosDaBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                SalvarArquivosNaBase();
                SalvarParametrosNaBase();
                SalvarDestinosNaBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnSelecionarPastaOrigem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    Title = "Selecione a pasta de origem",
                    EnsurePathExists = true,
                    AllowNonFileSystemItems = false
                };

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string pastaSelecionada = dialog.FileName;

                    var arquivos = Directory.GetFiles(pastaSelecionada);

                    foreach (var item in arquivos)
                    {
                        var file = new FileInfo(item);

                        if (!file.Exists)
                            continue;

                        ViewModel.AdicionarArquivo(new Arquivo { CaminhoCompleto = file.FullName, Nome = file.Name });
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Acesso negado: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnSelecionarArquivos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = false, 
                    Title = "Selecione um ou mais arquivos",
                    EnsureFileExists = true,
                    Multiselect = true, 
                    AllowNonFileSystemItems = false
                };

                dialog.Filters.Add(new CommonFileDialogFilter("Todos os arquivos", "*.*"));

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    foreach (var caminhoArquivo in dialog.FileNames)
                    {
                        var file = new FileInfo(caminhoArquivo);

                        if (!file.Exists)
                            continue;

                        ViewModel.AdicionarArquivo(new Arquivo
                        {
                            CaminhoCompleto = file.FullName,
                            Nome = file.Name,
                        });
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Acesso negado: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAbrirPastaArquivo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn && btn.Tag is string caminhoCompleto)
                {
                    var file = new FileInfo(caminhoCompleto);

                    if (!file.Exists)
                    {
                        MessageBox.Show("O arquivo não existe mais.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        ViewModel.RemoverArquivoPorCaminho(caminhoCompleto);
                        return;
                    }

                    if (file.DirectoryName.ObterValorOuPadrao("").Trim() == "")
                    {
                        MessageBox.Show("O diretório do arquivo não existe mais.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        ViewModel.RemoverArquivoPorCaminho(caminhoCompleto);
                        return;
                    }

                    Process.Start("explorer.exe", file.DirectoryName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnDeletarTodosArquivos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.RemoverArquivosSelecionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCopiarTodosArquivos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeletarCaminhoDestino_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbrirPastaCaminhoDestino_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdicionarCaminhoDestino_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    Title = "Selecione a pasta de destino",
                    EnsurePathExists = true,
                    AllowNonFileSystemItems = false
                };

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string pastaSelecionada = dialog.FileName;

                    if (pastaSelecionada.ObterValorOuPadrao("").Trim() != "")
                    {
                        var dir = new DirectoryInfo(pastaSelecionada);

                        if (dir.Exists)
                            ViewModel.AdicionarDestino(new Destino { Nome = dir.Name, CaminhoCompleto = dir.FullName });
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Acesso negado: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Metodos
        private void AdicionarArquivoNaColecao(Arquivo arquivo)
        {
            if (arquivo == null)
                return;

            ViewModel.AdicionarArquivo(arquivo);
        }
        private void AdicionarDestinoNaColecao(Destino destino)
        {
            if (destino == null)
                return;

            ViewModel.AdicionarDestino(destino);
        }

        private void CarregarArquivosDaBase()
        {
            var arquivos = arquivoAppService.ObterListaArquivos();

            if (arquivos.Count() > 0)
            {
                foreach (var arquivo in arquivos)
                    AdicionarArquivoNaColecao(arquivo);
            }
        }
        private void CarregarParametrosDaBase()
        {
            parametros = parametroAppService.ObterParametros();

            if (parametros.Count() > 0)
            {
                foreach (var item in parametros)
                {
                    if (item.Nome.Equals(eParametros.HabilitarNomePadronizado.ToString()))
                    {
                        ViewModel.NomePadraoAtivo = item.Valor.ObterValorOuPadrao("0").ConverterParaInt32() == 1;
                    }
                    else if (item.Nome.Equals(eParametros.NomePadronizado.ToString()))
                    {
                        ViewModel.NomePadrao = item.Valor.ToString();
                    }
                }
            }
        }

        private void CarregarDestinosDaBase()
        {
            var destinos = destinoAppService.ObterListaDestinos();

            if (destinos.Count() > 0)
            {
                foreach (var destino in destinos)
                    AdicionarDestinoNaColecao(destino);
            }
        }
        private void SalvarArquivosNaBase()
        {
            var arquivos = ViewModel.ObterArquivos();
            arquivoAppService.RegistrarArquivos(arquivos);
        }
        private void SalvarParametrosNaBase()
        {
            if (parametros.Count() > 0)
            {
                foreach (var item in parametros)
                {
                    if (item.Nome.Equals(eParametros.HabilitarNomePadronizado.ToString()))
                    {
                        item.Valor = ViewModel.NomePadraoAtivo ? "1" : "0";
                    }
                    else if (item.Nome.Equals(eParametros.NomePadronizado.ToString()))
                    {
                        item.Valor = ViewModel.NomePadrao.ObterValorOuPadrao("").Trim();
                    }
                }

                parametroAppService.SalvarParametros(parametros);
            }
        }

        private void SalvarDestinosNaBase()
        {
            var destinos = ViewModel.ObterDestinos();
            destinoAppService.RegistrarDestinos(destinos);
        }
        #endregion
    }
}