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
    public class PrefeituraBeneficioEventual
    {
        private static IRepository<PrefeituraBeneficioEventualInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraBeneficioEventualInfo>>();
            }
        }

        private static IRepository<ConsultaPrefeituraBeneficioEventualInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaPrefeituraBeneficioEventualInfo>>();
            }
        }

        public IQueryable<PrefeituraBeneficioEventualInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public PrefeituraBeneficioEventualInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("PrefeituraBeneficiosEventuaisRecursosFinanceiros").Include("TipoBeneficioEventual").Include("Criterios").Include("BeneficiosOferecidos").Include("Necessidades").Include("OrgaosResponsaveis").Single(m => m.Id == id);
            return p;
        }


        public PrefeituraBeneficioEventualInfo GetByPrefeituraETipoBeneficioEventual(int idPrefeitura, int idTipoBeneficioEventual)
        {
            var p = _repository.GetObjectSet().Include("PrefeituraBeneficiosEventuaisRecursosFinanceiros").Include("Criterios").Include("BeneficiosOferecidos").Include("Necessidades").Include("OrgaosResponsaveis").Include("UnidadesExecutoras").SingleOrDefault(m => m.IdPrefeitura == idPrefeitura && m.IdTipoBeneficioEventual == idTipoBeneficioEventual);
            return p;
        }

        public IQueryable<PrefeituraBeneficioEventualInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("PrefeituraBeneficiosEventuaisRecursosFinanceiros").Include("TipoBeneficioEventual").Include("Criterios").Include("BeneficiosOferecidos").Include("Necessidades").Include("OrgaosResponsaveis").Include("UnidadesExecutoras").Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaPrefeituraBeneficioEventualInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Update(PrefeituraBeneficioEventualInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraBeneficioEventual().Validar(obj);
            obj.TipoBeneficioEventual = new TipoBeneficioEventual().GetById(obj.IdTipoBeneficioEventual);
            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            var original = GetById(obj.Id);
            var hasChangeNecessidades = _repository.UpdateNN<NecessidadeBeneficioEventualInfo>(original, obj.Necessidades, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Necessidades);
            var hasChangeBeneficios = _repository.UpdateNN<BeneficioEventualInfo>(original, obj.BeneficiosOferecidos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.BeneficiosOferecidos);
            var hasChangeCriterios = _repository.UpdateNN<CriterioConcessaoInfo>(original, obj.Criterios, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Criterios);
            var hasChangeResponsaveis = _repository.UpdateNN<OrgaoResponsavelInfo>(original, obj.OrgaosResponsaveis, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.OrgaosResponsaveis);  
            var hasChangeUnidadesExecutoras = _repository.UpdateNN<UnidadePrivadaInfo>(original, obj.UnidadesExecutoras, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.UnidadesExecutoras);


            #region recurso financeiro

            var recursoNovo = obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros.FirstOrDefault();
            if (original.PrefeituraBeneficiosEventuaisRecursosFinanceiros == null)
            {
                original.PrefeituraBeneficiosEventuaisRecursosFinanceiros = obj.PrefeituraBeneficiosEventuaisRecursosFinanceiros;
            }
            else
            {
                var recursoExistente = original.PrefeituraBeneficiosEventuaisRecursosFinanceiros.Where(x => x.Exercicio == recursoNovo.Exercicio).FirstOrDefault();
                if (recursoExistente == null)
                {
                    original.PrefeituraBeneficiosEventuaisRecursosFinanceiros.Add(recursoNovo);
                }
                else
                {
                    recursoExistente.ValorFMAS = recursoNovo.ValorFMAS;
                    recursoExistente.ValorFundoMunicipalSolidariedade = recursoNovo.ValorFundoMunicipalSolidariedade;
                    recursoExistente.ValorOrcamentoMunicipal = recursoNovo.ValorOrcamentoMunicipal;
                    recursoExistente.ValorFEAS = recursoNovo.ValorFEAS;
                    recursoExistente.ValorFundoEstadualSolidariedade = recursoNovo.ValorFundoEstadualSolidariedade;
                    recursoExistente.ValorFNAS = recursoNovo.ValorFNAS;
                    recursoExistente.ValorReprogramacaoAnoAnterior = recursoNovo.ValorReprogramacaoAnoAnterior;
                    recursoExistente.ValorDemandasParlamentares = recursoNovo.ValorDemandasParlamentares;
                    recursoExistente.ValorReprogramacaoDemandasParlamentares = recursoNovo.ValorReprogramacaoDemandasParlamentares;
                    recursoExistente.ObjetoDemandaParlamentar = recursoNovo.ObjetoDemandaParlamentar;
                    recursoExistente.ContrapartidaMunicipal = recursoNovo.ContrapartidaMunicipal;
                    recursoExistente.CodigoDemandaParlamentar = recursoNovo.CodigoDemandaParlamentar;
                    recursoExistente.ValorContrapartidaMunicipal = recursoNovo.ValorContrapartidaMunicipal;
                }

            }
            _repository.Update(obj);

            #endregion


            //if (!obj.BeneficiarioAtendidoRedeSocioAssistencial)
            //{
            //var l = new PrefeituraBeneficioEventualServico();
            //var servicos = l.GetByBeneficioEventual(obj.Id).ToList();
            //if (servicos.Count > 0)
            //    foreach (var s in servicos)
            //        l.Delete(s, false, false);
            //}

            if (hasChangeCriterios)
                propriedades.Add("critérios usados para concessão do benefício");
            if (hasChangeNecessidades)
                propriedades.Add("necessidades que o benefício atende");
            if (hasChangeBeneficios)
                propriedades.Add("benefícios eventuais que o município oferece");
            if (hasChangeResponsaveis)
                propriedades.Add("órgãos responsáveis pelo benefício eventual");

            if (propriedades.Count > 0)
            {
                String descricao = "Benefício Eventual: " + obj.TipoBeneficioEventual.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, GetQuadro((ETipoBeneficioEventual)obj.IdTipoBeneficioEventual), descricao, obj.Id);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(PrefeituraBeneficioEventualInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraBeneficioEventual().Validar(obj);
            var idsCriterios = obj.Criterios.Select(s => s.Id).ToList();
            obj.Criterios = new CriterioConcessao().GetAll().Where(s => idsCriterios.Contains(s.Id)).ToList();

            var idsResponsaveis = obj.OrgaosResponsaveis.Select(s => s.Id).ToList();
            obj.OrgaosResponsaveis = new OrgaoResponsavel().GetAll().Where(s => idsResponsaveis.Contains(s.Id)).ToList();  

            var idsUnidadesExecutoras = obj.UnidadesExecutoras.Select(s => s.Id).ToList();
            obj.UnidadesExecutoras = new UnidadePrivada().GetAll().Where(s => idsUnidadesExecutoras.Contains(s.Id)).ToList(); //Welington P.

            var idsNecessidades = obj.Necessidades.Select(s => s.Id).ToList();
            obj.Necessidades = new NecessidadeBeneficioEventual().GetAll().Where(s => idsNecessidades.Contains(s.Id)).ToList();

            var idsBeneficios = obj.BeneficiosOferecidos.Select(s => s.Id).ToList();
            obj.BeneficiosOferecidos = new BeneficioEventual().GetAll().Where(s => idsBeneficios.Contains(s.Id)).ToList();

            obj.TipoBeneficioEventual = new TipoBeneficioEventual().GetById(obj.IdTipoBeneficioEventual);

            _repository.Add(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 57, "Incluído o Benefício Eventual " + obj.TipoBeneficioEventual.Nome + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PrefeituraBeneficioEventualInfo obj, Boolean commit)
        {
            var l = new PrefeituraBeneficioEventualServico();
            var servicos = l.GetByBeneficioEventual(obj.Id).ToList();
            if (servicos.Count > 0)
                foreach (var s in servicos)
                    l.Delete(s, false, false);
            String descricao = "Excluído o Benefício Eventual " + obj.TipoBeneficioEventual.Nome + ".";

            _repository.Delete(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 57, descricao);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades, PrefeituraBeneficioEventualInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Regulamentacao": labels.Add("existe regulamentação municipal para concessão do benefício"); break;
                    case "IdTipoLegislacao": if (obj.Regulamentacao) labels.Add("tipo de legislação"); break;
                    case "Lei": if (obj.Regulamentacao) labels.Add("número da legislação"); break;
                    case "DataPublicacaoLei": if (obj.Regulamentacao) labels.Add("data de publicação da Lei"); break;
                    case "IdFormaAuxilio": labels.Add("forma que o auxílio é concedido"); break;
                    case "MediaSemestralBeneficiarios": labels.Add("média semestral de beneficiários"); break;
                    case "MediaSemestralBeneficiariosConcedidos": labels.Add("média semestral de benefícios concedidos"); break;
                    case "OrgaoGestorResponsavel":
                    case "CRASResponsavel":
                    case "UnidadePrivadaResponsavel":
                    case "CREASResponsavel":
                    case "CentroPOPResponsavel":
                    case "FundoSocialSolidariedadeResponsavel": labels.Add("órgão responsável pela execução do benefício"); break;
                    case "ValorFMAS": labels.Add("valor do FMAS"); break;
                    case "ValorOrcamentoMunicipal": labels.Add("valor do orçamento municipal"); break;
                    case "ValorFundoMunicipalSolidariedade": labels.Add("valor do fundo social de solidariedade municipal"); break;
                    case "ValorFEAS": labels.Add("valor do FEAS"); break;
                    case "ValorFundoEstadualSolidariedade": labels.Add("valor do fundo social de solidariedade estadual"); break;
                    case "ValorFNAS": labels.Add("valor do FNAS"); break;
                    case "BeneficiarioAtendidoRedeSocioAssistencial": labels.Add("o beneficiário está sendo atendido na rede de serviços socioassistenciais"); break;
                }
            }
            return labels.Distinct().ToList();
        }

        public IQueryable<PrefeituraBeneficioEventualInfo> GetByUnidadePrivada(UnidadePrivadaInfo unidadeprivada)
        {
            return _repository.GetQuery().Where(m => m.UnidadesExecutoras.Any(p => p.Id == unidadeprivada.Id));
            //return _repository.GetQuery().Where(m => m.UnidadesExecutoras.Contains(unidadeprivada));
        }



        private Int32 GetQuadro(ETipoBeneficioEventual beneficio)
        {
            switch (beneficio)
            {
                case ETipoBeneficioEventual.AuxilioFuneral: return 54;
                case ETipoBeneficioEventual.AuxilioNatalidade: return 53;
                case ETipoBeneficioEventual.CalamidadePublica: return 55;
                case ETipoBeneficioEventual.VulnerabilidadeTemporaria: return 56;

            }
            return 0;
        }
    }
}
