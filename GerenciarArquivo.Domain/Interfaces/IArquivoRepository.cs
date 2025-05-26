using GerenciarArquivo.Domain.Entidades;
using JJ.NET.CrossData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Domain.Interfaces
{
    public interface IArquivoRepository : IRepository<Arquivo>
    {
    }
}
