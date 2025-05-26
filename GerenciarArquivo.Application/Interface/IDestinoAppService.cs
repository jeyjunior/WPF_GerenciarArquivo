using GerenciarArquivo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Application.Interface
{
    public interface IDestinoAppService
    {
        public bool RegistrarDestinos(IEnumerable<Destino> destinos);
        IEnumerable<Destino> ObterListaDestinos();
    }
}
