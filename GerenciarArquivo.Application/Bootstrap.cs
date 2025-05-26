using GerenciarArquivo.Application.Interface;
using GerenciarArquivo.Application.Services;
using GerenciarArquivo.Domain.Entidades;
using GerenciarArquivo.Domain.Interfaces;
using GerenciarArquivo.InfraData.Repository;
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
            await Configuracao.IniciarAsync();

            if (Configuracao.ConexaoBaseDados == null)
                return "";
            
            var services = new ServiceCollection();

            // Registro das dependências
            services.AddSingleton<IUnitOfWork>(_ => new UnitOfWork(Configuracao.ConexaoBaseDados));

            services.AddSingleton<IArquivoRepository, ArquivoRepository>();
            services.AddSingleton<IDestinoRepository, DestinoRepository>();
            services.AddSingleton<IParametroRepository, ParametroRepository>();

            services.AddSingleton<IArquivoAppService, ArquivoAppService>();
            services.AddSingleton<IDestinoAppService, DestinoAppService>();
            services.AddSingleton<IParametroAppService, ParametroAppService>();

            ServiceProvider = services.BuildServiceProvider();

            return Configuracao.CaminhoArquivoConfig;
        }
    }
}
