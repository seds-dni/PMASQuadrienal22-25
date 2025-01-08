using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CREAS
    {
        private static IRepository<CREASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CREASInfo>>();
            }
        }

        private static IRepository<CREASMunicipioInfo> _repositoryCREASMunicipio
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CREASMunicipioInfo>>();
            }
        }

        private static IRepository<ConsultaCREASInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaCREASInfo>>();
            }
        }

        public IQueryable<CREASInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CREASInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("AcoesSocioAssistenciais").Single(m => m.Id == id);
        }

        public IQueryable<CREASInfo> GetByUnidade(int idUnidade)
        {
            return _repository.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<ConsultaCREASInfo> GetConsultaByUnidade(int idUnidade)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public void Update(CREASInfo creas, Boolean commit)
        {
            var possuiPAEFI = _repository.GetQuery().Where(c => c.Id == creas.Id).Select(t => t.PossuiPAEFI).FirstOrDefault();
            var servico = new ServicoRecursoFinanceiroCREAS();

            if (creas.PossuiPAEFI && !possuiPAEFI)
            {
                var servicoPAIF = servico.CriarServicoPAEFI();
                servicoPAIF.IdCREAS = creas.Id;
                servicoPAIF.CREAS = creas;
                servico.Add(servicoPAIF, true, false);
            }
            else if (!creas.PossuiPAEFI && possuiPAEFI)
            {
                var servicoPAIF = servico.GetServicoPAEFIByCREAS(creas.Id);
                servicoPAIF.CREAS = creas;
                //servico.Delete(servicoPAIF, false, false);
            }

            new ValidadorCREAS().Validar(creas);

            var idsAcoes = creas.AcoesSocioAssistenciais.Select(s => s.Id).ToList();
            creas.AcoesSocioAssistenciais = new AcaoSocioAssistencial().GetCREAS().Where(s => idsAcoes.Contains(s.Id)).ToList();

            _repository.Update(creas);

            if (creas.UnidadePublica == null)
                creas.UnidadePublica = new UnidadePublica().GetById(creas.IdUnidade);

            //QUADRO 28
            var propriedadesEntity = _repository.GetModifiedProperties(creas);
            var propriedades = GetLabelForInfo(propriedadesEntity.Where(t => !t.Equals("AtendeOutrosMunicipios") && !t.Equals("NumeroAtendidosOutrosMunicipios")).ToList(), creas);
            var acao = EAcao.Update;
            if (propriedades.Count > 0)
            {
                String descricao = String.Empty;
                if (propriedades.Contains("Desativado"))
                {
                    acao = EAcao.Deactivate;
                    descricao = "Desativado o CREAS " + creas.IDCREAS + " - " + creas.Nome + ".";
                }
                else
                    descricao = "CREAS: " + creas.IDCREAS + " - " + creas.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);


                var log = Log.CreateLog(creas.UnidadePublica.IdPrefeitura, acao, 26, descricao, creas.Id);
                if (log != null)
                    new Log().Add(log, true);

                var logCreas = LogCreas.CreateLog(log.Id, creas.Id, creas.UnidadePublica.Id, log.DataHorario);
                if (logCreas != null)
                    new LogCreas().Add(logCreas, true);

            }

            ////QUADRO 30
            propriedades = GetLabelForInfo(propriedadesEntity.Where(t => t.Equals("AtendeOutrosMunicipios") || t.Equals("NumeroAtendidosOutrosMunicipios")).ToList(), creas);

            var objOriginal = GetById(creas.Id);

            //QUADRO 29
            var hasChangeAcoes = _repository.UpdateNN<AcaoSocioAssistencialInfo>(objOriginal, creas.AcoesSocioAssistenciais, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);
            if (hasChangeAcoes)
            {
                String descricao = "Modificado o trabalho social realizado pelo CREAS " + creas.IDCREAS + " - " + creas.Nome + ".";
                var log = Log.CreateLog(creas.UnidadePublica.IdPrefeitura, EAcao.Update, 27, descricao, creas.Id);
                if (log != null)
                    new Log().Add(log, true);

                var logCreas = LogCreas.CreateLog(log.Id, creas.Id, creas.UnidadePublica.Id, log.DataHorario);
                if (logCreas != null)
                    new LogCreas().Add(logCreas, true);
            }

            //QUADRO 28
            //  var hasChangeMunicipios = _repository.UpdateList<CREASMunicipioInfo>(objOriginal, creas.AbrangenciaMunicipios, (a, lst) => lst.Any(t => t.IdMunicipio == a.IdMunicipio && t.IdTipoAtendimento == a.IdTipoAtendimento), p => p.AbrangenciaMunicipios);

            if (!creas.Desativado)
            {

                var lstDeleted = new List<CREASMunicipioInfo>();
                var creasMunicipio = new CREASMunicipio();
                var listaAbrangencia = creasMunicipio.GetByCREAS(creas.Id).ToList();
                creas.AbrangenciaMunicipios = creas.AbrangenciaMunicipios ?? new List<CREASMunicipioInfo>();
                var hasChangeAbrangencia = false;

                foreach (var p in listaAbrangencia)
                    if (!creas.AbrangenciaMunicipios.Any(t => t.Id == p.Id))
                    {
                        hasChangeAbrangencia = true;
                        lstDeleted.Add(p);
                    }

                foreach (var p in lstDeleted)
                    creasMunicipio.Delete(p, false);

                foreach (var p in creas.AbrangenciaMunicipios)
                {
                    p.TipoAtendimento = null;
                    p.IdCREAS = creas.Id;
                    if (p.Id == 0)
                    {
                        creasMunicipio.Add(p, false);
                        hasChangeAbrangencia = true;
                    }
                    else
                        creasMunicipio.Update(p, false);
                }

                if (hasChangeAbrangencia && !propriedades.Any(t => t == "AbrangenciaMunicipios"))
                    propriedades.Add("AbrangenciaMunicipios");



                if (hasChangeAbrangencia || propriedades.Count > 0)
                {
                    String descricao = "CREAS " + creas.IDCREAS + " - " + creas.Nome;
                    if (propriedades.Count > 0)
                        descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                    if (hasChangeAbrangencia)
                        descricao += System.Environment.NewLine + "Modificado os municípios atendidos pelo CREAS.";

                    var log = Log.CreateLog(creas.UnidadePublica.IdPrefeitura, EAcao.Update, 28, descricao, creas.Id);
                    if (log != null)
                        new Log().Add(log, true);

                    var logCreas = LogCreas.CreateLog(log.Id, creas.Id, creas.UnidadePublica.Id, log.DataHorario);
                    if (logCreas != null)
                        new LogCreas().Add(logCreas, true);
                }
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(CREASInfo creas, Boolean commit)
        {
            new ValidadorCREAS().Validar(creas);
            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();
                var idsAcoes = creas.AcoesSocioAssistenciais.Select(s => s.Id).ToList();
                creas.AcoesSocioAssistenciais = new AcaoSocioAssistencial().GetCREAS().Where(s => idsAcoes.Contains(s.Id)).ToList();

                _repository.Add(creas);

                if (creas.UnidadePublica == null)
                    creas.UnidadePublica = new UnidadePublica().GetById(creas.IdUnidade);

                if (creas.AtendeOutrosMunicipios)
                {
                    var creasMunicipio = new CREASMunicipio();
                    creas.AbrangenciaMunicipios.ForEach(a =>
                    {
                        a.IdCREAS = creas.Id;
                        a.TipoAtendimento = null;
                        creasMunicipio.Add(a, true);
                    });
                }




                var log = Log.CreateLog(creas.UnidadePublica.IdPrefeitura, EAcao.Add, 26, "Incluído o CREAS " + creas.Nome + ".");
                if (log != null)
                    new Log().Add(log, false);

                ContextManager.Commit();


                if (creas.PossuiPAEFI)
                {
                    var servico = new ServicoRecursoFinanceiroCREAS();
                    var servicoPAEFI = servico.CriarServicoPAEFI();
                    servicoPAEFI.IdCREAS = creas.Id;
                    servicoPAEFI.CREAS = creas;
                    servicoPAEFI.IdHorasSemana = 1;
                    servico.Add(servicoPAEFI, true, false);
                }

                var logCreas = LogCreas.CreateLog(log.Id, creas.Id, creas.UnidadePublica.Id, log.DataHorario);
                if (logCreas != null)
                    new LogCreas().Add(logCreas, true);

                ContextManager.CloseConnection();
                ts.Complete();
            }
        }

        public void Delete(CREASInfo creas, Boolean commit)
        {
            var l = new ServicoRecursoFinanceiroCREAS();
            if (l.GetByCREAS(creas.Id).Count() > 0)
                throw new Exception("Esse CREAS possui serviços! Exclua primeiro os serviços para excluir o CREAS.");

            var hasChangeAcoes = _repository.UpdateList<AcaoSocioAssistencialInfo>(GetById(creas.Id), creas.AcoesSocioAssistenciais, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);
            String descricao = "Excluído o CREAS " + creas.IDCREAS + " - " + creas.Nome + ".";

            _repository.Delete(creas);
            if (hasChangeAcoes)
            {
                if (creas.UnidadePublica == null)
                    creas.UnidadePublica = new UnidadePublica().GetById(creas.IdUnidade);

                var log = Log.CreateLog(creas.UnidadePublica.IdPrefeitura, EAcao.Remove, 26, descricao);
                if (log != null)
                    new Log().Add(log, true);

                var logCreas = LogCreas.CreateLog(log.Id, creas.Id, creas.UnidadePublica.Id, log.DataHorario);
                if (logCreas != null)
                    new LogCreas().Add(logCreas, true);
            }
            if (commit)
                ContextManager.Commit();
        }

        //public Int32 GetTotalRHByCREAS(Int32 idCREAS)
        //{
        //    return _repository.GetQuery().Where(t => t.Id == idCREAS).Select(t => t.TotalFuncionariosNivelFundamental + t.TotalFuncionariosNivelMedio + t.TotalFuncionariosSemEscolaridade + t.TotalFuncionariosSuperior).First();
        //}

        public List<String> GetLabelForInfo(List<String> propriedades, CREASInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Nome": labels.Add("nome"); break;
                    case "IDCREAS": labels.Add("código Identificador do CREAS - IDCREAS"); break;
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
                    case "NumeroAtendidos": labels.Add("Previsão anual do número de pessoas atendidas"); break;
                    case "IdHorasSemana": labels.Add("quantidade de horas por semana o CREAS funciona"); break;
                    case "QuantidadeDiasSemana": labels.Add("quantidade de dias por semana o CREAS funciona"); break;
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
                    case "PossuiPAEFI": labels.Add("o CREAS oferta o serviço PAEFI"); break;
                    case "JustificativaPAEFI": if (!obj.PossuiPAEFI) labels.Add("justificativa por não ofertar o serviço PAEFI"); break;
                    case "AtendeOutrosMunicipios": labels.Add("o CREAS atende usuários de outros municípios"); break;
                    case "NumeroAtendidosOutrosMunicipios": if (obj.AtendeOutrosMunicipios) labels.Add("nº de atendidos de outros municípios"); break;
                    case "Desativado": labels.Add("Desativado"); break;
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
