using GerenciarArquivo.Domain.Entidades;
using GerenciarArquivo.Domain.Interfaces;
using JJ.NET.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.InfraData.Repository
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
