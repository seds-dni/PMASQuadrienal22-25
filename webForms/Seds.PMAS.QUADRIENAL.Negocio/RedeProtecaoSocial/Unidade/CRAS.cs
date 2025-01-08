using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CRAS
    {
        private static IRepository<CRASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CRASInfo>>();
            }
        }
        private static IRepository<ConsultaCRASInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaCRASInfo>>();
            }
        }

        public IQueryable<CRASInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CRASInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("AcoesSocioAssistenciais").Single(m => m.Id == id);
        }

        public IQueryable<CRASInfo> GetByUnidade(int idUnidade)
        {
            return _repository.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<ConsultaCRASInfo> GetConsultaByUnidade(int idUnidade)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public void Update(CRASInfo cras, Boolean commit)
        {
            var possuiPAIF = _repository.GetQuery().Where(c => c.Id == cras.Id).Select(t => t.PossuiPAIF).FirstOrDefault();
            var servico = new ServicoRecursoFinanceiroCRAS();
            Int32 idLog = 0;
            DateTime dataCriacao = DateTime.Now;

            //cras.PAIFAtivo = !servico.GetServicoPAIFByCRAS(cras.Id).Desativado;

            if (cras.PossuiPAIF && !possuiPAIF)
            {
                var servicoPAIF = servico.CriarServicoPAIF();
                servicoPAIF.IdCRAS = cras.Id;
                servicoPAIF.CRAS = cras;
                servico.Add(servicoPAIF, true, false);
            }
            else if (!cras.PossuiPAIF && possuiPAIF)
            {
                var servicoPAIF = servico.GetServicoPAIFByCRAS(cras.Id);
                servicoPAIF.CRAS = cras;
                //  servico.Delete(servicoPAIF, false,false);
            }

            new ValidadorCRAS().Validar(cras);
            
            var idsAcoes = cras.AcoesSocioAssistenciais.Select(s => s.Id).ToList();
            cras.AcoesSocioAssistenciais = new AcaoSocioAssistencial().GetCRAS().Where(s => idsAcoes.Contains(s.Id)).ToList();
            _repository.Update(cras);

            if (cras.UnidadePublica == null)
                cras.UnidadePublica = new UnidadePublica().GetById(cras.IdUnidade);

            //QUADRO 23
            var propriedadesEntity = _repository.GetModifiedProperties(cras);
            var propriedades = GetLabelForInfo(propriedadesEntity, cras);
            var acao = EAcao.Update;
            if (propriedades.Count > 0)
            {
                String descricao = String.Empty;
                if (propriedades.Contains("Desativado"))
                {
                    acao = EAcao.Deactivate;
                    descricao = "Desativado o CRAS " + cras.IDCRAS + " - " + cras.Nome + ".";
                }
                else
                    descricao = "CRAS: " + cras.IDCRAS + " - " + cras.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);

                var log = Log.CreateLog(cras.UnidadePublica.IdPrefeitura, acao, 21, descricao, cras.Id);
                if (log != null)
                    new Log().Add(log, true);

                var logCras = LogCras.CreateLog(log.Id, cras.Id, cras.UnidadePublica.Id, log.DataHorario);
                if (logCras != null)
                    new LogCras().Add(logCras, true);

            }

            ////QUADRO 24
            var hasChangeAcoes = _repository.UpdateNN<AcaoSocioAssistencialInfo>(GetById(cras.Id), cras.AcoesSocioAssistenciais, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);
            if (hasChangeAcoes)
            {
                String descricao = "Modificado o trabalho social realizado pelo CRAS " + cras.IDCRAS + " - " + cras.Nome + ".";
                var log = Log.CreateLog(cras.UnidadePublica.IdPrefeitura, EAcao.Update, 22, descricao, cras.Id);
                if (log != null)
                    new Log().Add(log, true);

                var logCras = LogCras.CreateLog(log.Id, cras.Id, cras.UnidadePublica.Id, log.DataHorario);
                if (logCras != null)
                    new LogCras().Add(logCras, true);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(CRASInfo cras)
        {
            new ValidadorCRAS().Validar(cras);
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                var idsAcoes = cras.AcoesSocioAssistenciais.Select(s => s.Id).ToList();
                cras.AcoesSocioAssistenciais = new AcaoSocioAssistencial().GetCRAS().Where(s => idsAcoes.Contains(s.Id)).ToList();
                _repository.Add(cras);

                if (cras.UnidadePublica == null)
                    cras.UnidadePublica = new UnidadePublica().GetById(cras.IdUnidade);

                var log = Log.CreateLog(cras.UnidadePublica.IdPrefeitura, EAcao.Add, 21, "Incluído o CRAS " + cras.Nome + ".");
                if (log != null)
                    new Log().Add(log, false);

                ContextManager.Commit();
                if (cras.PossuiPAIF)
                {
                    var servico = new ServicoRecursoFinanceiroCRAS();
                    var servicoPAIF = servico.CriarServicoPAIF();
                    servicoPAIF.IdCRAS = cras.Id;
                    servicoPAIF.CRAS = cras;
                    servico.Add(servicoPAIF, true, false);
                }

                var logCras = LogCras.CreateLog(log.Id, cras.Id, cras.UnidadePublica.Id, log.DataHorario);
                if (logCras != null)
                    new LogCras().Add(logCras, true);

                ContextManager.CloseConnection();

                ts.Complete();
            }

        }

        public List<String> GetLabelForInfo(List<String> propriedades, CRASInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Nome": labels.Add("nome"); break;
                    case "IDCRAS": labels.Add("código Identificador do CRAS - IDCRAS"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "Email": labels.Add("e-mail institucional"); break;
                    case "Coordenador": labels.Add("coordenador"); break;
                    case "IdEscolaridadeCoordenador": if (obj.PossuiCoordenador) labels.Add("escolaridade do coordenador"); break;
                    case "IdFormacaoCoordenador":
                    case "OutraFormacaoCoordenador": if (obj.IdEscolaridadeCoordenador == 4 && obj.PossuiCoordenador) labels.Add("formação acadêmica do coordenador"); break;
                    case "CapacidadeAtendimento": labels.Add("nº de famílias referenciadas"); break;
                    case "NumeroAtendidos": labels.Add("previsão anual do número de famílias atendidas"); break;
                    case "IdHorasSemana": labels.Add("quantidade de horas por semana o CRAS funciona"); break;
                    case "QuantidadeDiasSemana": labels.Add("quantidade de dias por semana o CRAS funciona"); break;
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
                    case "PossuiEquipeVolante": labels.Add("o CRAS possui Equipe Volante"); break;
                    case "NomeLocaisAbrangenciaEquipeVolante": if (obj.PossuiEquipeVolante) labels.Add("nome dos locais de abrangência territorial de atendimento da Equipe Volante"); break;
                    case "TotalFuncionariosVolanteNivelMedio":
                    case "TotalFuncionariosVolanteNivelSuperior":
                    case "TotalFuncionariosVolanteSuperiorServicoSocial":
                    case "TotalFuncionariosVolanteSuperiorPsicologia":
                    case "TotalFuncionariosVolanteSuperiorPedagogia":
                    case "TotalFuncionariosVolanteSuperiorSociologia":
                    case "TotalFuncionariosVolanteSuperiorDireito":
                    case "TotalFuncionariosVolanteSuperiorTerapiaOcupacional":
                    case "TotalFuncionariosVolanteSuperiorMusicoterapia":
                    case "TotalFuncionariosVolanteSuperiorAntropologia":
                    case "TotalFuncionariosVolanteSuperiorEconomia":
                    case "TotalFuncionariosVolanteSuperiorEconomiaDomestica":
                        if (obj.PossuiEquipeVolante) labels.Add("total de trabalhadores da equipe volante segundo a escolaridade"); break;
                    case "PossuiPAIF": labels.Add("o CRAS oferta o serviço PAIF"); break;
                    case "JustificativaPAIF": if (!obj.PossuiPAIF) labels.Add("justificativa por não ofertar o serviço PAIF"); break;
                    case "Desativado": labels.Add("Desativado"); break;

                }
            }
            return labels.Distinct().ToList();
        }

        public void Delete(CRASInfo cras, Boolean commit)
        {
            var l = new ServicoRecursoFinanceiroCRAS();
            if (l.GetByCRAS(cras.Id).Count() > 0)
                throw new Exception("Esse CRAS possui serviços! Exclua primeiro os serviços para excluir o CRAS.");

            var hasChangeAcoes = _repository.UpdateList<AcaoSocioAssistencialInfo>(GetById(cras.Id), cras.AcoesSocioAssistenciais, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);
            String descricao = "Excluído o CRAS " + cras.IDCRAS + " - " + cras.Nome + ".";

            _repository.Delete(cras);

            if (hasChangeAcoes)
            {
                if (cras.UnidadePublica == null)
                    cras.UnidadePublica = new UnidadePublica().GetById(cras.IdUnidade);

                var log = Log.CreateLog(cras.UnidadePublica.IdPrefeitura, EAcao.Remove, 21, descricao);
                if (log != null)
                    new Log().Add(log, true);

                var logCras = LogCras.CreateLog(log.Id, cras.Id, cras.UnidadePublica.Id, log.DataHorario);
                if (logCras != null)
                    new LogCras().Add(logCras, true);
            }
            if (commit)
                ContextManager.Commit();
        }

        //public Int32 GetTotalRHByCRAS(Int32 idCRAS)
        //{
        //    return _repository.GetQuery().Where(t => t.Id == idCRAS).Select(t => t.TotalFuncionariosNivelFundamental + t.TotalFuncionariosNivelMedio + t.TotalFuncionariosSemEscolaridade + t.TotalFuncionariosSuperior).First();
        //}
    }
}
