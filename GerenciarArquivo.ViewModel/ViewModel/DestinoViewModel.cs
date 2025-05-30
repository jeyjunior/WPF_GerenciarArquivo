using GerenciarArquivo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.ViewModel.ViewModel
{
    public class DestinoViewModel : INotifyPropertyChanged
    {
        private readonly Destino _destino;
        public Destino Destino { get => _destino; }

        public DestinoViewModel(Destino destino)
        {
            _destino = destino;
        }

        public string CaminhoCompleto => _destino.CaminhoCompleto;
        public string Nome => _destino.Nome;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
