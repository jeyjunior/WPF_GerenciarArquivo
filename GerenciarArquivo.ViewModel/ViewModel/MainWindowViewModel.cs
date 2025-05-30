using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JJ.NET.Core.Extensoes;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GerenciarArquivo.Domain.Entidades;

namespace GerenciarArquivo.ViewModel.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #region Arquivos
        public ObservableCollection<ArquivoViewModel> Arquivos { get; } = new();

        private bool _selecionarTodos;
        public bool SelecionarTodos
        {
            get => _selecionarTodos;
            set
            {
                _selecionarTodos = value;
                foreach (var arquivoVM in Arquivos)
                {
                    arquivoVM.Selecionado = value;
                }
                OnPropertyChanged(nameof(SelecionarTodos));
            }
        }
        public void RemoverArquivosSelecionado()
        {
            var arquivosParaRemover = Arquivos.Where(a => a.Arquivo.Selecionado).ToList();
            foreach (var item in arquivosParaRemover)
                Arquivos.Remove(item);

            OnPropertyChanged(nameof(QuantidadeArquivos));
            OnPropertyChanged(nameof(HabilitarBotaoArquivo));
        }
        public void AdicionarArquivo(Arquivo arquivo)
        {
            if (!ValidarSeArquivoJaExiste(arquivo))
            {
                Arquivos.Add(new ArquivoViewModel(arquivo));
                OnPropertyChanged(nameof(QuantidadeArquivos));
                OnPropertyChanged(nameof(HabilitarBotaoArquivo));
            }
        }
        public bool ValidarSeArquivoJaExiste(Arquivo arquivo)
        {
            return Arquivos.Any(a => a.Nome.Equals(arquivo.Nome, StringComparison.OrdinalIgnoreCase) && a.CaminhoCompleto.Equals(arquivo.CaminhoCompleto, StringComparison.OrdinalIgnoreCase));
        }
        public bool RemoverArquivoPorCaminho(string caminhoCompleto)
        {
            var arquivo = Arquivos.FirstOrDefault(i => i.CaminhoCompleto.Equals(caminhoCompleto));
            if (arquivo == null)
                return true;

            bool removido = Arquivos.Remove(arquivo);

            OnPropertyChanged(nameof(QuantidadeArquivos));
            OnPropertyChanged(nameof(HabilitarBotaoArquivo));

            return removido;
        }
        #endregion

        #region Parametros
        private string _nomePadrao;
        public string NomePadrao
        {
            get => _nomePadrao;
            set
            {
                if (_nomePadrao != value)
                {
                    _nomePadrao = value;
                    OnPropertyChanged(nameof(NomePadrao));
                }
            }
        }
        private bool _nomePadraoAtivo;
        public bool NomePadraoAtivo
        {
            get => _nomePadraoAtivo;
            set
            {
                if (_nomePadraoAtivo != value)
                {
                    _nomePadraoAtivo = value;
                    OnPropertyChanged(nameof(NomePadraoAtivo));
                }
            }
        }
        #endregion

        #region Status
        public bool HabilitarBotaoArquivo
        {
            get => Arquivos.Count > 0;
        }
        public string QuantidadeArquivos
        {
            get => "Total : " + Arquivos.Count();
        }
        public bool HabilitarBotaoDestino
        {
            get => Destinos.Count > 0;
        }
        #endregion

        #region Destinos
        public ObservableCollection<DestinoViewModel> Destinos { get; } = new();
        public bool RemoverDestinoSelecionado()
        {
            var destino = Destinos.FirstOrDefault(i => i.CaminhoCompleto.Equals(DestinoSelecionado.CaminhoCompleto));
            if (destino == null)
                return true;

            bool removido = Destinos.Remove(destino);

            OnPropertyChanged(nameof(HabilitarBotaoDestino));
            DestinoSelecionado = (Destinos.Count > 0) ? Destinos.FirstOrDefault() : null;

            return removido;
        }
        public void AdicionarDestino(Destino destino)
        {
            if (!ValidarSeDestinoJaExiste(destino))
            {
                var destinoViewModel = new DestinoViewModel(destino);
                Destinos.Add(destinoViewModel);
                this.DestinoSelecionado = destinoViewModel;

                OnPropertyChanged(nameof(HabilitarBotaoDestino));
            }
        }
        public bool ValidarSeDestinoJaExiste(Destino destino)
        {
            return Destinos.Any(a => a.Nome.Equals(destino.Nome, StringComparison.OrdinalIgnoreCase) && a.CaminhoCompleto.Equals(destino.CaminhoCompleto, StringComparison.OrdinalIgnoreCase));
        }
        public List<Destino> ObterDestinos()
        {
            return Destinos.Select(i => i.Destino).ToList(); 
        }
        private DestinoViewModel _destinoSelecionado;
        public DestinoViewModel DestinoSelecionado
        {
            get => _destinoSelecionado;
            set
            {
                if (_destinoSelecionado != value)
                {
                    _destinoSelecionado = value;
                    OnPropertyChanged(nameof(DestinoSelecionado));
                }
            }
        }
        #endregion
    }
}
