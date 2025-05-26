using GerenciarArquivo.Application.Interface;
using GerenciarArquivo.Domain.Entidades;
using GerenciarArquivo.Domain.Interfaces;
using GerenciarArquivo.InfraData.Repository;
using JJ.NET.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Application.Services
{
    public class DestinoAppService : IDestinoAppService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDestinoRepository _destinoRepository;

        public DestinoAppService(IUnitOfWork unitOfWork, IDestinoRepository destinoRepository)
        {
            _unitOfWork = unitOfWork;
            _destinoRepository = destinoRepository;
        }
        public bool RegistrarDestinos(IEnumerable<Destino> destinos)
        {
            if (destinos == null)
                return false;

            if (destinos.Count() <= 0)
                return false;

            try
            {
                _unitOfWork.Begin();

                _destinoRepository.ExecutarQuery("DELETE FROM Destino");

                foreach (var item in destinos)
                {
                    if (!item.Validar())
                        continue;

                    _destinoRepository.Adicionar(item);
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
        public IEnumerable<Destino> ObterListaDestinos()
        {
            return _destinoRepository.ObterLista();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
            _destinoRepository.Dispose();
        }
    }
}
