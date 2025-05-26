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
            }
            catch
            {
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                SalvarArquivosNaBase();
                SalvarParametrosNaBase();
            }
            catch
            {
            }
        }
        private void btnSelecionarPastaOrigem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                // await Mensagem.ErroAsync($"Erro ao selecionar pasta: {ex.Message}", this.Content.XamlRoot);
            }
        }
        private void btnSelecionarArquivos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbrirPastaArquivo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExcluirArquivo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCopiarArquivo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeletarTodosArquivos_Click(object sender, RoutedEventArgs e)
        {

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

        }
        #endregion

        #region Metodos
        private void AdicionarArquivoNaColecao(Arquivo arquivo)
        {
            if (arquivo == null)
                return;

            if (ViewModel.ValidarSeArquivoJaExiste(arquivo))
                return;

            ViewModel.AdicionarArquivo(arquivo);
        }
        private void AdicionarArquivoNaColecao(FileInfo fileInfo)
        {
            if (fileInfo == null)
                return;

            var arquivo = new Arquivo
            {
                Nome = fileInfo.Name,
                CaminhoCompleto = fileInfo.FullName,
                PK_Arquivo = 0,
            };

            if (ViewModel.ValidarSeArquivoJaExiste(arquivo))
                return;

            ViewModel.AdicionarArquivo(arquivo);
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
        private void SalvarArquivosNaBase()
        {
            var arquivos = ViewModel.Arquivos.ToList();

            if (arquivos.Count() <= 0)
                return;

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
        #endregion
    }
}