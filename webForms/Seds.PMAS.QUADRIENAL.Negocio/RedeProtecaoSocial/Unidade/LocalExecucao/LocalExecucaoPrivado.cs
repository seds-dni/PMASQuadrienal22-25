using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class LocalExecucaoPrivado
    {
        private static IRepository<LocalExecucaoPrivadoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LocalExecucaoPrivadoInfo>>();
            }
        }

        private static IRepository<ConsultaLocalExecucaoPrivadoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaLocalExecucaoPrivadoInfo>>();
            }
        }

        public IQueryable<LocalExecucaoPrivadoInfo> GetAll()
        {
            return _repository.GetQuery();

        }


        public LocalExecucaoPrivadoInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("Unidade").SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<LocalExecucaoPrivadoInfo> GetByUnidade(int idUnidade)
        {

            // return _repository.GetObjectSet().Include("Unidade").Where(m => m.IdUnidade == idUnidade);
            return _repository.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<ConsultaLocalExecucaoPrivadoInfo> GetConsultaByUnidade(int idUnidade)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }



        public void Update(LocalExecucaoPrivadoInfo local, Boolean commit)
        {
            new ValidadorLocalExecucaoPrivado().Validar(local);
            _repository.Update(local);
            if (local.Unidade == null)
                local.Unidade = new UnidadePrivada().GetById(local.IdUnidade);

            //QUADRO 40
            var propriedadesEntity = _repository.GetModifiedProperties(local);
            var propriedades = GetLabelForInfo(propriedadesEntity);
            if (propriedades.Count > 0)
            {
                String descricao = String.Empty;
                var acao = EAcao.Update;
                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Local de Execução " + local.Id + " - " + local.Nome + " (Unidade Pública " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ").";
                    acao = EAcao.Deactivate;
                }
                else
                 descricao = "Local de Execução: " + local.Id + " - " + local.Nome + " (Unidade da Rede Indireta " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ")" + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(local.Unidade.IdPrefeitura, acao, 38, descricao, local.Unidade.Id, local.IdUnidade);
                if (log != null)
                    new Log().Add(log, false);
            } 

            if (commit)
                ContextManager.Commit();
        }

        public void Add(LocalExecucaoPrivadoInfo local, Boolean commit)
        {
            new ValidadorLocalExecucaoPrivado().Validar(local);
            _repository.Add(local);

            if (local.Unidade == null)
                local.Unidade = new UnidadePrivada().GetById(local.IdUnidade);

            var log = Log.CreateLog(local.Unidade.IdPrefeitura, EAcao.Add, 38, "Incluído o Local de Execução " + local.Nome + " (Unidade Privada " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ")", local.IdUnidade);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(LocalExecucaoPrivadoInfo local, Boolean commit)
        {
            var s = new ServicoRecursoFinanceiroPrivado();
            if (s.GetByLocalExecucao(local.Id).Count() > 0)
                throw new Exception("Esse local de execução possui serviços! Exclua primeiro os serviços para excluir o local de execução.");

            if (local.Unidade == null)
                local.Unidade = new UnidadePrivada().GetById(local.IdUnidade);

            String descricao = "Excluído o Local de Execução " + local.Id + " - " + local.Nome + " (Unidade Privada " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ").";
            var log = Log.CreateLog(local.Unidade.IdPrefeitura, EAcao.Remove, 38, descricao, local.IdUnidade);

            _repository.Delete(local);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public Int32 GetTotalRHByLocalExecucao(Int32 idLocalExecucao)
        {
            return _repository.GetQuery().Where(t => t.Id == idLocalExecucao).Select(t => t.TotalFuncionariosNivelFundamental + t.TotalFuncionariosNivelMedio + t.TotalFuncionariosSemEscolaridade + t.TotalFuncionariosSuperior).First();
        }
        public List<String> GetLabelForInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Nome": labels.Add("nome"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "Email": labels.Add("e-mail institucional"); break;
                    case "TecnicoResponsavel": labels.Add("técnico responsável"); break;
                    case "NumeroAtendidos": labels.Add("previsão anual do número de pessoas atendidas"); break;
                    case "CapacidadeAtendimento": labels.Add("capacidade de atendimento anual"); break;
                    case "IdHorasSemana": labels.Add("quantidade de horas por semana o local de execução funciona"); break;
                    case "QuantidadeDiasSemana": labels.Add("quantidade de dias por semana o local de execução funciona"); break;
                    case "IdTipoImovel": labels.Add("tipo do imóvel"); break;
                    case "TotalFuncionarios":
                    case "TotalFuncionariosNivelFundamental":
                    case "TotalFuncionariosNivelMedio":
                    case "TotalFuncionariosSuperiorServicoSocial":
                    case "TotalFuncionariosSuperiorPsicologia":
                    case "TotalFuncionariosSuperiorPedagogia":
                    case "TotalFuncionariosSuperiorSociologia":
                    case "TotalFuncionariosSuperior":
                    case "TotalFuncionariosSuperiorPosGraduacao":
                    case "TotalFuncionariosSuperiorDireito":
                    case "TotalFuncionariosSemEscolaridade":
                    case "TotalEstagiarios":
                    case "TotalFuncionariosSuperiorAdministracao":
                    case "TotalFuncionariosSuperiorAntropologia":
                    case "TotalFuncionariosSuperiorContabilidade":
                    case "TotalFuncionariosSuperiorEconomia":
                    case "TotalFuncionariosSuperiorEconomiaDomestica":
                    case "TotalFuncionariosSuperiorTerapiaOcupacional":
                    case "TotalFuncionariosSuperiorMusicoterapia":
                        labels.Add("total de trabalhadores segundo a escolaridade"); break;
                    case "PossuiTaxasTributos":
                    case "PossuiCessaoImoveis":
                    case "PossuiTributoFederal":
                    case "PossuiTributoEstadual":
                    case "PossuiTributoMunicipal":
                        labels.Add("benefícios e isenções"); break;
                    case "Desativado":
                        labels.Add("Desativado"); break;
                }
            }
            return labels.Distinct().ToList();
        }


    }
}
