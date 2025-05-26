using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Domain.Enumerador
{
    public enum eTipoDeAviso
    {
        Informacao,
        Sucesso,
        Erro,
        Alerta,
    }

    public enum eParametros
    {
        HabilitarNomePadronizado = 0,
        NomePadronizado = 1,
    }
}
