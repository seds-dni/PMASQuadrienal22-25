using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class LocalExecucaoPublico
    {
        private static IRepository<LocalExecucaoPublicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LocalExecucaoPublicoInfo>>();
            }
        }

        private static IRepository<ConsultaLocalExecucaoPublicoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaLocalExecucaoPublicoInfo>>();
            }
        }

        private static IRepository<ConsultaLocalExecucaoPublicoInativoInfo> _repositoryConsultaInativo
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaLocalExecucaoPublicoInativoInfo>>();
            }
        }


        public IQueryable<LocalExecucaoPublicoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public LocalExecucaoPublicoInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("Unidade").SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<LocalExecucaoPublicoInfo> GetByUnidade(int idUnidade)
        {
            return _repository.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<ConsultaLocalExecucaoPublicoInfo> GetConsultaByUnidade(int idUnidade)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<ConsultaLocalExecucaoPublicoInativoInfo> GetConsultaByUnidadeInativa(int idUnidade)
        {
            return _repositoryConsultaInativo.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public void Update(LocalExecucaoPublicoInfo local, Boolean commit)
        {
            Validar(local);
            _repository.Update(local);

            if (local.Unidade == null)
                local.Unidade = new UnidadePublica().GetById(local.IdUnidade);

            //QUADRO 18
            var propriedadesEntity = _repository.GetModifiedProperties(local);
            var propriedades = GetLabelForInfo(propriedadesEntity);

            var acao = EAcao.Update;
            if (propriedades.Count > 0)
            {
                String descricao;
                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Local de Execução " + local.Id + " - " + local.Nome + " (Unidade Pública " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ").";
                    acao  = EAcao.Deactivate;    
                }
                else
                {
                    descricao = "Local de Execução: " + local.Id + " - " + local.Nome + " (Unidade Pública " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ")" + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                }
                var log = Log.CreateLog(local.Unidade.IdPrefeitura, acao, 18, descricao, local.Id, local.IdUnidade);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(LocalExecucaoPublicoInfo local, Boolean commit)
        {
            Validar(local);

            _repository.Add(local);

            if (local.Unidade == null)
                local.Unidade = new UnidadePublica().GetById(local.IdUnidade);

            var log = Log.CreateLog(local.Unidade.IdPrefeitura, EAcao.Add, 18, "Incluído o Local de Execução " + local.Nome + " (Unidade Pública " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ")", local.IdUnidade);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(LocalExecucaoPublicoInfo local, Boolean commit)
        {
            var s = new ServicoRecursoFinanceiroPublico();
            if (s.GetByLocalExecucao(local.Id).Count() > 0)
                throw new Exception("Esse local de execução possui serviços! Exclua primeiro os serviços para excluir o local de execução.");

            if (local.Unidade == null)
                local.Unidade = new UnidadePublica().GetById(local.IdUnidade);

            String descricao = "Desativado o Local de Execução " + local.Id + " - " + local.Nome + " (Unidade Pública " + local.Unidade.Id + " - " + local.Unidade.RazaoSocial + ").";
            var log = Log.CreateLog(local.Unidade.IdPrefeitura, EAcao.Remove, 18, descricao, local.IdUnidade);

            _repository.Delete(local);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Validar(LocalExecucaoPublicoInfo local)
        {
            var lstMsg = new List<string>();
            if (local.CEP.Length == 0)
            {
                lstMsg.Add("O preenchimendo do campo CEP do local de execução é obrigatório");
            }
            if (local.Logradouro.Length == 0)
            {
                lstMsg.Add("O preenchimendo do campo Endereço do local de execução é obrigatório");
            }
            if (local.Numero.Length == 0)
            {
                lstMsg.Add("É obrigatório informar o campo Número do local de execução!");
            }
            if (local.Bairro.Length == 0)
            {
                lstMsg.Add("É obrigatório informar o campo Bairro do local de execução");
            }
            //if (local.CapacidadeAtendimento == 0)
            //{
            //    lstMsg.Add("É obrigatório informar o campo capacidade de atendimento do local de execução!");                
            //}

            //if (local.NumeroAtendidos == 0)
            //{
            //    lstMsg.Add("É obrigatório informar o campo número de atendidos do local de execução!");                
            //}
            if (local.PossuiTecnicoResponsavel && String.IsNullOrEmpty(local.TecnicoResponsavel))
            {
                lstMsg.Add("O preenchimento do campo Técnico Responsável da Unidade é obrigatório");
            }

            if (!Util.ValidaString(local.Nome))
            {
                lstMsg.Add("O preenchimento do campo Nome do local de execução é obrigatório");
            }
            if (!local.IdTipoImovel.HasValue)
            {
                lstMsg.Add("O preenchimento do Imóvel é obrigatório");
            }

            if (local.Desativado)
            {
                if (local.IdMotivoDesativacao.HasValue)
                {
                    if (local.IdMotivoDesativacao.Value != 27)
                    {
                        if (local.IdMotivoDesativacao.Value == 28 && !local.IdMotivoEncerramento.HasValue)
                            lstMsg.Add("Deve ser informado uma das alternativas referente ao encerramento deste Local de execução");

                        if (!local.DataDesativacao.HasValue)
                            lstMsg.Add("O preenchimento do campo Data de encerramento deste local é obrigatório");
                        else if(local.DataDesativacao.HasValue && local.DataDesativacao.Value > DateTime.Now.Date)
                            lstMsg.Add("A data de encerramento não pode ser posterior à data atual");
                        if (String.IsNullOrEmpty(local.Detalhamento))
                            lstMsg.Add("O preenchimento do campo Detalhamento é obrigatório");

                    }
                }
            }

            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
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
                    case "Desativado":
                        labels.Add("Desativado"); break;

                }
            }
            return labels.Distinct().ToList();
        }
    }

}
