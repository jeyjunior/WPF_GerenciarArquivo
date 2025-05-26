using GerenciarArquivo.Application.Interface;
using GerenciarArquivo.Domain.Entidades;
using GerenciarArquivo.Domain.Interfaces;
using JJ.NET.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Application.Services
{
    public class ParametroAppService : IParametroAppService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IParametroRepository _parametroRepository;

        public ParametroAppService(IUnitOfWork unitOfWork, IParametroRepository parametroRepository)
        {
            _unitOfWork = unitOfWork;
            _parametroRepository = parametroRepository;
        }
        public IEnumerable<Parametro> ObterParametros()
        {
            return _parametroRepository.ObterLista();
        }
        public bool SalvarParametros(IEnumerable<Parametro> parametros)
        {
            if (parametros == null)
                return false;

            if (parametros.Count() <= 0)
                return false;

            try
            {
                _unitOfWork.Begin();

                foreach (var item in parametros)
                {
                    if (!item.Validar())
                        continue;

                    _parametroRepository.Atualizar(item);
                }

                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                _unitOfWork.Rollback();
            }

            return false;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
            _parametroRepository.Dispose();
        }
    }
}
