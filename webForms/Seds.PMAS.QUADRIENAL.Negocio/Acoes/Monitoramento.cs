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
    public class Monitoramento
    {
        private static IRepository<MonitoramentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MonitoramentoInfo>>();
            }
        }

        public IQueryable<MonitoramentoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public MonitoramentoInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("Focos").Include("Procedimentos").Include("Instrumentos").Include("MeiosDivulgacao").SingleOrDefault(m => m.Id == id);
            return p;
        }

        public MonitoramentoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("Focos").Include("Procedimentos").Include("Instrumentos").Include("MeiosDivulgacao").Where(m => m.IdPrefeitura == idPrefeitura).FirstOrDefault();
        }

        public void Update(MonitoramentoInfo obj, Boolean commit)
        {
            new ValidadorMonitoramento().Validar(obj);

            var idsMeiosDivulgacao = obj.MeiosDivulgacao.Select(s => s.Id).ToList();
            obj.MeiosDivulgacao = new MeioDivulgacao().GetAll().Where(s => idsMeiosDivulgacao.Contains(s.Id)).ToList();

            var idsProcedimentos = obj.Procedimentos.Select(s => s.Id).ToList();
            obj.Procedimentos = new ProcedimentoMonitoramento().GetAll().Where(s => idsProcedimentos.Contains(s.Id)).ToList();

            var idsInstrumentos = obj.Instrumentos.Select(s => s.Id).ToList();
            obj.Instrumentos = new InstrumentoMonitoramento().GetAll().Where(s => idsInstrumentos.Contains(s.Id)).ToList();            

            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            var objOriginal = GetById(obj.Id);
            var hasChangeMeiosDivulgacao = _repository.UpdateNN<MeioDivulgacaoInfo>(objOriginal, obj.MeiosDivulgacao, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.MeiosDivulgacao);
            var hasChangeProcedimentos = _repository.UpdateNN<ProcedimentoMonitoramentoInfo>(objOriginal, obj.Procedimentos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Procedimentos);
            var hasChangeInstrumentos = _repository.UpdateNN<InstrumentoMonitoramentoInfo>(objOriginal, obj.Instrumentos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Instrumentos);
            var hasChangeFocos = _repository.UpdateList<PrefeituraMonitoramentoFocoInfo>(objOriginal, obj.Focos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Focos);

            if (obj.RealizaMonitoramento && hasChangeFocos)
                propriedades.Add("periodicidade e focos do monitoramento realizados na rede socioassistencial");

            if (obj.RealizaMonitoramento && (hasChangeInstrumentos || hasChangeProcedimentos))
                propriedades.Add("procedimentos e instrumentos utilizados no monitoramento das ações");

            if (obj.ResultadosDivulgados && hasChangeMeiosDivulgacao)
                propriedades.Add("meios de divulgação");

            if (propriedades.Count > 0)
            {
                String descricao = "Monitoramento: " + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 69, descricao);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(MonitoramentoInfo obj, Boolean commit)
        {
            new ValidadorMonitoramento().Validar(obj);

            var idsMeiosDivulgacao = obj.MeiosDivulgacao.Select(s => s.Id).ToList();
            obj.MeiosDivulgacao = new MeioDivulgacao().GetAll().Where(s => idsMeiosDivulgacao.Contains(s.Id)).ToList();

            var idsProcedimentos = obj.Procedimentos.Select(s => s.Id).ToList();
            obj.Procedimentos = new ProcedimentoMonitoramento().GetAll().Where(s => idsProcedimentos.Contains(s.Id)).ToList();

            var idsInstrumentos = obj.Instrumentos.Select(s => s.Id).ToList();
            obj.Instrumentos = new InstrumentoMonitoramento().GetAll().Where(s => idsInstrumentos.Contains(s.Id)).ToList();

            foreach (var f in obj.Focos)
                f.Monitoramento = obj;

            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }


        public List<String> GetLabelForInfo(List<String> propriedades, MonitoramentoInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "RealizaMonitoramento": labels.Add("realiza monitoramento das ações de assistência social"); break;
                    case "PretendeRealizarProximoAno": if(!obj.RealizaMonitoramento) labels.Add("pretende realizar monitoramento das ações no próximo ano"); break;
                    case "InformacoesSistematizadas": if (obj.RealizaMonitoramento) labels.Add("as informações de monitoramento são sistematizadas"); break;
                    case "ResultadosDivulgados": if (obj.RealizaMonitoramento) labels.Add("os resultados de monitoramento são divulgados"); break;
                    case "PMASObjetoMonitoramento": if (obj.RealizaMonitoramento) labels.Add("as informações do PMAS são objeto de monitoramento"); break;                    
                    case "OperacionalizadoOrgaoGestor":                         
                    case "OperacionalizadoTerceirizado":
                    case "OperacionalizadoOrgaoGestorEquipeEspecifica":
                    case "OperacionalizadoOrgaoGestorEquipeTecnicoProtecaoSocial":
                    case "OperacionalizadoOrgaoGestorTecnicosOutrasEquipes":             
                        if (obj.RealizaMonitoramento) labels.Add("como é operacionalizado"); break;
                }
            }
            return labels.Distinct().ToList();
        }

        public void Delete(MonitoramentoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
