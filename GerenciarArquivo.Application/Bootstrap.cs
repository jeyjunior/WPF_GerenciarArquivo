using JJ.NET.CrossData.Services;
using JJ.NET.Data;
using JJ.NET.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciarArquivo.Application
{
    public static class Bootstrap
    {
        public static System.IServiceProvider ServiceProvider { get; private set; }

        public static async Task<string> IniciarAsync()
        {
            var config = new ConfiguracaoBancoDadosService();
            var result = await config.IniciarConfiguracaoAsync(JJ.NET.CrossData.Enumerador.Conexao.SQLite);

            if (!result.Sucesso)
                return "";
            
            var services = new ServiceCollection();

            // Registro das dependências
            services.AddSingleton<IUnitOfWork>(_ => new UnitOfWork(result.DbConnection));


            ServiceProvider = services.BuildServiceProvider();

            return result.CaminhoArquivoConfiguracao;
        }
    }
}
