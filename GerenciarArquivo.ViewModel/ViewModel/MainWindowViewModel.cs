using GerenciarArquivo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.ViewModel.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #region Arquivos
        private ObservableCollection<Arquivo> _arquivos = new ObservableCollection<Arquivo>();
        public ObservableCollection<Arquivo> Arquivos
        {
            get => _arquivos;
            set
            {
                _arquivos = value;
                OnPropertyChanged(nameof(Arquivos));
            }
        }
        public void RemoverTodosArquivos()
        {
            _arquivos.Clear();
            OnPropertyChanged(nameof(QuantidadeArquivos));
        }
        public void AdicionarArquivo(Arquivo arquivo)
        {
            if (!ValidarSeArquivoJaExiste(arquivo))
            {
                _arquivos.Add(arquivo);
                OnPropertyChanged(nameof(QuantidadeArquivos));
            }
        }
        public bool ValidarSeArquivoJaExiste(Arquivo arquivo)
        {
            return _arquivos.Any(a => a.Nome.Equals(arquivo.Nome, StringComparison.OrdinalIgnoreCase) && a.CaminhoCompleto.Equals(arquivo.CaminhoCompleto, StringComparison.OrdinalIgnoreCase));
        }
        public bool RemoverArquivoPorCaminho(string caminhoCompleto)
        {
            var arquivo = _arquivos.FirstOrDefault(i => i.CaminhoCompleto.Equals(caminhoCompleto));
            if (arquivo == null)
                return true;

            bool removido = _arquivos.Remove(arquivo);
            if (removido)
                OnPropertyChanged(nameof(QuantidadeArquivos));

            return removido;
        }
        public Arquivo ObterArquivo(string caminhoCompleto)
        {
            return _arquivos.FirstOrDefault(i => i.CaminhoCompleto.Equals(caminhoCompleto));
        }
        #endregion

        #region Destinos
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
        public string QuantidadeArquivos
        {
            get => "Total : " + _arquivos.Count();
        }
        #endregion
    }
}
