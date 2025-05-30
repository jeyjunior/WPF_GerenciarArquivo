using GerenciarArquivo.Domain.Entidades;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace GerenciarArquivo.ViewModel.ViewModel
{
    public class DestinoViewModel : INotifyPropertyChanged
    {
        private readonly Destino _destino;
        public Destino Destino => _destino;

        public DestinoViewModel(Destino destino)
        {
            _destino = destino;
        }
        public string CaminhoCompleto => _destino.CaminhoCompleto;
        public string Nome => _destino.Nome;
        public string NomeFormatado
        {
            get
            {
                if (string.IsNullOrEmpty(_destino.CaminhoCompleto))
                    return string.Empty;

                if (IsRootDirectory(_destino.CaminhoCompleto))
                    return _destino.CaminhoCompleto;

                return FormatPath(_destino.CaminhoCompleto);
            }
        }

        private bool IsRootDirectory(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                return dirInfo.Parent == null;
            }
            catch
            {
                return false;
            }
        }

        private string FormatPath(string fullPath)
        {
            try
            {
                var parts = fullPath.Split(Path.DirectorySeparatorChar,
                                         StringSplitOptions.RemoveEmptyEntries);

                if (fullPath.Length <= 150 && parts.Length <= 3)
                    return fullPath;

                if (parts.Length > 3)
                {
                    var limitedParts = parts.Skip(parts.Length - 3).Take(3).ToArray();
                    return $"...\\{string.Join("\\", limitedParts)}";
                }

                if (fullPath.Length > 150)
                {
                    return $"...\\{fullPath.Substring(fullPath.Length - 147)}";
                }

                return fullPath;
            }
            catch
            {
                return _destino.Nome; 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}