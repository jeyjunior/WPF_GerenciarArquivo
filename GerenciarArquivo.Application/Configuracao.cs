using JJ.NET.CrossData.Atributo;
using JJ.NET.CrossData.DTO;
using JJ.NET.CrossData.Enumerador;
using JJ.NET.CrossData.Services;
using JJ.NET.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JJ.NET.CrossData;
using JJ.NET.CrossData.Extensao;
using GerenciarArquivo.Domain.Interfaces;
using GerenciarArquivo.Domain.Enumerador;
using JJ.NET.Data;
using GerenciarArquivo.InfraData.Repository;

namespace GerenciarArquivo.Application
{
    internal static class Configuracao
    {
        public static Conexao ConexaoAtiva { get; private set; } = Conexao.SQLite;
        public static IDbConnection ConexaoBaseDados = null;
        public static string CaminhoArquivoConfig = "";

        public static async Task IniciarAsync()
        {
            var ret = await new ConfiguracaoBancoDadosService().IniciarConfiguracaoAsync(Conexao.SQLite);

            if (ret == null)
                throw new Exception("Falha ao tentar criar/conectar na base de dados.");

            if (!ret.Sucesso)
                throw new Exception(ret.Erros.FirstOrDefault());

            ConexaoBaseDados = ret.DbConnection;
            CaminhoArquivoConfig = ret.CaminhoArquivoConfiguracao;

            RegistrarEntidades();
            RegistrarParametros();
        }
        public static void RegistrarEntidades()
        {
            using (var uow = new UnitOfWork(ConexaoBaseDados))
            {
                var entidades = ObterEntidadesMapeadas();
                var tabelasExistentes = uow.Connection.VerificarEntidadeExiste(entidades);

                if (tabelasExistentes.Any(i => !i.Existe))
                {
                    try
                    {
                        uow.Begin();

                        foreach (var entidade in tabelasExistentes.Where(e => !e.Existe))
                            uow.Connection.CriarTabela(entidade.TipoEntidade, uow.Transaction);

                        uow.Commit();
                    }
                    catch (SqlException ex)
                    {
                        uow.Rollback();
                        throw new Exception("Erro ao criar as entidades no banco de dados", ex);
                    }
                    catch (Exception ex)
                    {
                        uow.Rollback();
                        throw new Exception("Erro inesperado ao criar as entidades", ex);
                    }
                }
            }
        }
        public static void RegistrarParametros()
        {
            using (var uow = new UnitOfWork(ConexaoBaseDados))
            {
                var parametroRepository = new ParametroRepository(uow);

                try
                {
                    string parametro = eParametros.HabilitarNomePadronizado.ToString();

                    if (parametroRepository.ObterLista($" Parametro.Nome = '{parametro}' ").FirstOrDefault() == null)
                    {
                        uow.Begin();

                        parametroRepository.Adicionar(new Domain.Entidades.Parametro { Nome = eParametros.HabilitarNomePadronizado.ToString(), Valor = "0" });
                        parametroRepository.Adicionar(new Domain.Entidades.Parametro { Nome = eParametros.NomePadronizado.ToString(), Valor = "" });

                        uow.Commit();
                    }
                }
                catch (SqlException ex)
                {
                    uow.Rollback();
                    throw new Exception("Erro ao criar as entidades no banco de dados", ex);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new Exception("Erro inesperado ao criar as entidades", ex);
                }
            }
        }
        private static IEnumerable<Type> ObterEntidadesMapeadas()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            if (!assemblies.Any(a => a.FullName.Contains("GerenciarArquivo.Domain")))
            {
                var domainAssembly = Assembly.Load("GerenciarArquivo.Domain");
                assemblies.Add(domainAssembly);
            }

            return assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttribute<EntidadeAttribute>() != null && t.IsClass && !t.IsAbstract);
        }
    }
}
