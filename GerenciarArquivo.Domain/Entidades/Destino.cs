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
    [Entidade("Destino")]
    public class Destino
    {
        [ChavePrimaria, Obrigatorio]
        public int PK_Destino { get; set; }
        [Obrigatorio]
        public string CaminhoCompleto { get; set; }

        [Editavel(false)]
        public ValidarResultado ValidarResultado { get; set; } = new ValidarResultado();

        public bool Validar()
        {
            ValidarResultado = new ValidarResultado();

            if (CaminhoCompleto.ObterValorOuPadrao("").Trim() == "")
            {
                ValidarResultado.Adicionar("Caminho da pasta é obrigatório.");
                return false;
            }

            return true;
        }
    }
}
