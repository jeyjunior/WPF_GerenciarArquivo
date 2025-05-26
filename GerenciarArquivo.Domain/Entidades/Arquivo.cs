using JJ.NET.Core.Extensoes;
using JJ.NET.Core.Validador;
using JJ.NET.CrossData.Atributo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Domain.Entidades
{
    [Entidade("Arquivo")]
    public class Arquivo
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_Arquivo { get; set; }
        [Obrigatorio]
        public string Nome { get; set; }
        [Obrigatorio]
        public string CaminhoCompleto { get; set; }

        [Editavel(false)]
        public ValidarResultado ValidarResultado { get; set; } = new ValidarResultado();

        public bool Validar()
        {
            ValidarResultado = new ValidarResultado();

            if (Nome.ObterValorOuPadrao("").Trim() == "")
            {
                ValidarResultado.Adicionar("Nome do arquivo é obrigatório.");
                return false;
            }
            else if (CaminhoCompleto.ObterValorOuPadrao("").Trim() == "")
            {
                ValidarResultado.Adicionar("Caminho completo do arquivo é obrigatório.");
                return false;
            }

            return true;
        }
    }
}
