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
    public class VigilanciaSocioAssistencial
    {
        private static IRepository<VigilanciaSocioAssistencialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<VigilanciaSocioAssistencialInfo>>();
            }
        }

        public IQueryable<VigilanciaSocioAssistencialInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public VigilanciaSocioAssistencialInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("BasesDados").Include("Acoes").SingleOrDefault(m => m.Id == id);
            return p;
        }

        public VigilanciaSocioAssistencialInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("BasesDados").Include("Acoes").Where(m => m.IdPrefeitura == idPrefeitura).FirstOrDefault();
        }

        public void Update(VigilanciaSocioAssistencialInfo obj, Boolean commit)
        {
            new ValidadorVigilanciaSocioAssistencial().Validar(obj);            

            var idsAcoes = obj.Acoes.Select(s => s.Id).ToList();
            obj.Acoes = new AcaoVigilanciaSocioAssistencial().GetAll().Where(s => idsAcoes.Contains(s.Id)).ToList();

            var idsBasesDados = obj.BasesDados.Select(s => s.Id).ToList();
            obj.BasesDados = new BaseDados().GetAll().Where(s => idsBasesDados.Contains(s.Id)).ToList();

            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            var objOriginal = GetById(obj.Id);            
            var hasChangeBaseDados = _repository.UpdateNN<BaseDadosInfo>(objOriginal, obj.BasesDados, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.BasesDados);
            var hasChangeAcoes = _repository.UpdateNN<AcaoVigilanciaSocioAssistencialInfo>(objOriginal, obj.Acoes, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Acoes);

            if (obj.OfereceVigilancia && hasChangeAcoes)
                propriedades.Add("eixos e ações da vigilância socioassistencial");

            if (obj.OfereceVigilancia && hasChangeBaseDados && !propriedadesEntity.Any(t => t == "OutraBaseDados"))
                propriedades.Add("bases de dados utilizadas pela vigilância");

            if (propriedades.Count > 0)
            {
                String descricao = "Vigilância Socioassistencial: " + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 68, descricao);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(VigilanciaSocioAssistencialInfo obj, Boolean commit)
        {
            new ValidadorVigilanciaSocioAssistencial().Validar(obj);            

            var idsAcoes = obj.Acoes.Select(s => s.Id).ToList();
            obj.Acoes = new AcaoVigilanciaSocioAssistencial().GetAll().Where(s => idsAcoes.Contains(s.Id)).ToList();

            var idsBasesDados = obj.BasesDados.Select(s => s.Id).ToList();
            obj.BasesDados = new BaseDados().GetAll().Where(s => idsBasesDados.Contains(s.Id)).ToList();

            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(VigilanciaSocioAssistencialInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades, VigilanciaSocioAssistencialInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "OfereceVigilancia": labels.Add("realiza ações de vigilância socioassistencial"); break;
                    case "OutraBaseDados": if (!obj.OfereceVigilancia) labels.Add("bases de dados utilizadas pela vigilância"); break;                    
                }
            }
            return labels.Distinct().ToList();
        }

    }
}
