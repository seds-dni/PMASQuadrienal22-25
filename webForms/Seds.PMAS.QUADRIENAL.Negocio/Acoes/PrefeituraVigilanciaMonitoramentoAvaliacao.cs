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
    public class PrefeituraVigilanciaMonitoramentoAvaliacao
    {
        private static IRepository<PrefeituraVigilanciaMonitoramentoAvaliacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraVigilanciaMonitoramentoAvaliacaoInfo>>();
            }
        }

        public IQueryable<PrefeituraVigilanciaMonitoramentoAvaliacaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrefeituraVigilanciaMonitoramentoAvaliacaoInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("Pesquisas").Include("Aprimoramentos").SingleOrDefault(m => m.Id == id);
            return p;
        }

        public PrefeituraVigilanciaMonitoramentoAvaliacaoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("Pesquisas").Include("Aprimoramentos").Where(m => m.IdPrefeitura == idPrefeitura).FirstOrDefault();
        }

        public void Update(PrefeituraVigilanciaMonitoramentoAvaliacaoInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraVigilanciaMonitoramentoAvaliacao().Validar(obj);
            foreach (var f in obj.Pesquisas)            
                new ValidadorPrefeituraVigilanciaMonitoramentoAvaliacaoPesquisa().Validar(f);                          

            var idsAprimoramentos = obj.Aprimoramentos.Select(s => s.Id).ToList();
            obj.Aprimoramentos = new AprimoramentoAcao().GetAll().Where(s => idsAprimoramentos.Contains(s.Id)).ToList();      

            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity);

            var objOriginal = GetById(obj.Id);
            var hasChangeAprimoramento = _repository.UpdateNN<AprimoramentoAcaoInfo>(objOriginal, obj.Aprimoramentos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Aprimoramentos);            
            var hasChangePesquisas = _repository.UpdateList<PrefeituraVigilanciaMonitoramentoAvaliacaoPesquisaInfo>(objOriginal, obj.Pesquisas, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Pesquisas);

            if (hasChangeAprimoramento)
                propriedades.Add("de que forma os resultados obtidos com a vigilância sociassistencial, monitoramento e avaliação contribuem para o aprimoramento das ações");

            if (hasChangePesquisas)
                propriedades.Add("pesquisas, estudos ou levantamentos");

            if (propriedades.Count > 0)
            {
                String descricao = "Aspectos Gerais: " + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 71, descricao);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(PrefeituraVigilanciaMonitoramentoAvaliacaoInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraVigilanciaMonitoramentoAvaliacao().Validar(obj);

            var idsAprimoramentos = obj.Aprimoramentos.Select(s => s.Id).ToList();
            obj.Aprimoramentos = new AprimoramentoAcao().GetAll().Where(s => idsAprimoramentos.Contains(s.Id)).ToList();

            foreach (var f in obj.Pesquisas)
            {
                new ValidadorPrefeituraVigilanciaMonitoramentoAvaliacaoPesquisa().Validar(f);
                f.PrefeituraVigilanciaMonitoramentoAvaliacao = obj;
            }
        
            _repository.Add(obj);

            String descricao = "Informado aspectos gerais sobre vigilância, monitoramento e avaliação.";
            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 71, descricao);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PrefeituraVigilanciaMonitoramentoAvaliacaoInfo obj, Boolean commit)
        {
            String descricao = "Excluído aspectos gerais sobre vigilância, monitoramento e avaliação.";
            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 71, descricao);

            _repository.Delete(obj);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public Boolean VerificarExistenciaNoMunicipio(Int32 idPrefeitura)
        {
            var vigilancia = new VigilanciaSocioAssistencial().GetAll().Where(t => t.IdPrefeitura == idPrefeitura).FirstOrDefault();
            var monitoramento = new Monitoramento().GetAll().Where(t => t.IdPrefeitura == idPrefeitura).FirstOrDefault();
            var avaliacao = new Avaliacao().GetAll().Where(t => t.IdPrefeitura == idPrefeitura).FirstOrDefault();            
            return (vigilancia != null && vigilancia.OfereceVigilancia) || (monitoramento != null && monitoramento.RealizaMonitoramento) || (avaliacao != null && avaliacao.AvaliaAcoes);
        }

        public void ConsistirExistencia(Int32 idPrefeitura, Boolean commit)
        {
            var obj = GetByPrefeitura(idPrefeitura);
            if (obj == null)
                return;
            if (VerificarExistenciaNoMunicipio(idPrefeitura))
                return;
            Delete(obj,commit);            
        }

        public List<String> GetLabelForInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "PossuiSistemaInformatizadoProprio": labels.Add("possui sistema informatizado próprio utilizado para vigilância socioassistencial, monitoramento ou avaliação"); break;                    
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
