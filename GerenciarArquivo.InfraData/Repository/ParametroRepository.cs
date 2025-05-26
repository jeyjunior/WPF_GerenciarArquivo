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
    public class ParametroRepository : Repository<Parametro>, IParametroRepository
    {
        public ParametroRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
