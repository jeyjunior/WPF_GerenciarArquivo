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
    public class ArquivoAppService : IArquivoAppService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoAppService(IUnitOfWork unitOfWork, IArquivoRepository arquivoRepository)
        {
            _unitOfWork = unitOfWork;
            _arquivoRepository = arquivoRepository;
        }
        public bool RegistrarArquivos(IEnumerable<Arquivo> arquivos)
        {
            if (arquivos == null)
                return false;

            if (arquivos.Count() <= 0)
                return false;

            try
            {
                _unitOfWork.Begin();

                _arquivoRepository.ExecutarQuery("DELETE FROM Arquivo");

                foreach (var item in arquivos)
                {
                    if (!item.Validar())
                        continue;

                    _arquivoRepository.Adicionar(item);
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
        public IEnumerable<Arquivo> ObterListaArquivos()
        {
            return _arquivoRepository.ObterLista();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
            _arquivoRepository.Dispose();
        }
    }
}
