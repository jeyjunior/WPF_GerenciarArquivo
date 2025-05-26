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
    public class DestinoRepository : Repository<Destino>, IDestinoRepository
    {
        public DestinoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
