using GerenciarArquivo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.ViewModel.ViewModel
{
    public class ArquivoViewModel : INotifyPropertyChanged
    {
        private readonly Arquivo _arquivo;
        public Arquivo Arquivo { get => _arquivo; }
        public ArquivoViewModel(Arquivo arquivo)
        {
            _arquivo = arquivo;
        }

        public bool Selecionado
        {
            get => _arquivo.Selecionado;
            set
            {
                _arquivo.Selecionado = value;
                OnPropertyChanged(nameof(Selecionado));
            }
        }
        public string Nome => _arquivo.Nome;
        public string CaminhoCompleto => _arquivo.CaminhoCompleto;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
