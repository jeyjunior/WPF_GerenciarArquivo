using GerenciarArquivo.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Application.Interface
{
    public interface IParametroAppService
    {
        bool SalvarParametros(IEnumerable<Parametro> parametros);
        IEnumerable<Parametro> ObterParametros();
    }
}
