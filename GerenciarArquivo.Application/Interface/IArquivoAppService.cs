using GerenciarArquivo.Application.Services;
using GerenciarArquivo.Domain.Entidades;
using GerenciarArquivo.Domain.Interfaces;
using JJ.NET.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Application.Interface
{
    public interface IArquivoAppService
    {
        public bool RegistrarArquivos(IEnumerable<Arquivo> arquivos);
        IEnumerable<Arquivo> ObterListaArquivos();
    }

}
