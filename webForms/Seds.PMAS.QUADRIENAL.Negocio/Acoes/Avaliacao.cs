using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using StructureMap;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Avaliacao
    {
        private static IRepository<AvaliacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AvaliacaoInfo>>();
            }
        }

        public IQueryable<AvaliacaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AvaliacaoInfo GetById(int id)
        {
            var p = _repository.GetObjectSet().Include("Objetivos").Include("Procedimentos").Include("MotivosNaoAvaliacao").SingleOrDefault(m => m.Id == id);
            return p;
        }

        public AvaliacaoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("Objetivos").Include("Procedimentos").Include("MotivosNaoAvaliacao").Where(m => m.IdPrefeitura == idPrefeitura).FirstOrDefault();
        }

        public void Update(AvaliacaoInfo obj, Boolean commit)
        {
            new ValidadorAvaliacao().Validar(obj);           

            var idsObjetivos = obj.Objetivos.Select(s => s.Id).ToList();
            obj.Objetivos= new ObjetivoAvaliacao().GetAll().Where(s => idsObjetivos.Contains(s.Id)).ToList();

            var idsProcedimentos = obj.Procedimentos.Select(s => s.Id).ToList();
            obj.Procedimentos = new ProcedimentoAvaliacao().GetAll().Where(s => idsProcedimentos.Contains(s.Id)).ToList();

            var idsMotivosNaoAvaliacao = obj.MotivosNaoAvaliacao.Select(s => s.Id).ToList();
            obj.MotivosNaoAvaliacao = new MotivoNaoAvaliacao().GetAll().Where(s => idsMotivosNaoAvaliacao.Contains(s.Id)).ToList();

            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            var objOriginal = GetById(obj.Id);            
            var hasChangeObjetivos =  _repository.UpdateNN<ObjetivoAvaliacaoInfo>(objOriginal, obj.Objetivos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Objetivos);
            var hasChangeProcedimentos = _repository.UpdateNN<ProcedimentoAvaliacaoInfo>(objOriginal, obj.Procedimentos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.Procedimentos);
            var hasChangeMotivos = _repository.UpdateNN<MotivoNaoAvaliacaoInfo>(objOriginal, obj.MotivosNaoAvaliacao, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.MotivosNaoAvaliacao);

            if (obj.AvaliaAcoes && hasChangeObjetivos)
                propriedades.Add("objetivos");

            if (obj.AvaliaAcoes && hasChangeProcedimentos)
                propriedades.Add("procedimentos e métodos empregados");

            if (!obj.AvaliaAcoes && hasChangeMotivos)
                propriedades.Add("motivos de não ser realizada avaliação");

            if (propriedades.Count > 0)
            {
                String descricao = "Avaliação: " + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 70, descricao);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(AvaliacaoInfo obj, Boolean commit)
        {
            new ValidadorAvaliacao().Validar(obj);           

            var idsObjetivos = obj.Objetivos.Select(s => s.Id).ToList();
            obj.Objetivos = new ObjetivoAvaliacao().GetAll().Where(s => idsObjetivos.Contains(s.Id)).ToList();

            var idsProcedimentos = obj.Procedimentos.Select(s => s.Id).ToList();
            obj.Procedimentos = new ProcedimentoAvaliacao().GetAll().Where(s => idsProcedimentos.Contains(s.Id)).ToList();

            var idsMotivosNaoAvaliacao = obj.MotivosNaoAvaliacao.Select(s => s.Id).ToList();
            obj.MotivosNaoAvaliacao = new MotivoNaoAvaliacao().GetAll().Where(s => idsMotivosNaoAvaliacao.Contains(s.Id)).ToList();

            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(AvaliacaoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades, AvaliacaoInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "AvaliaAcoes": labels.Add("realiza monitoramento das ações de assistência social"); break;
                    case "UtilizaDadosMonitoramento": if (obj.AvaliaAcoes) labels.Add("utiliza os dados do monitoramento para a avaliação"); break;                    
                    case "AvaliadoOrgaoGestor":
                    case "AvaliadoTerceirizado":
                    case "AvaliadoOrgaoGestorEquipeEspecifica":
                    case "AvaliadoOrgaoGestorEquipeTecnicoProtecaoSocial":
                    case "AvaliadoOrgaoGestorTecnicosOutrasEquipes":
                        if (obj.AvaliaAcoes) labels.Add("quem realiza a avaliação da rede socioassistencial"); break;
                    case "AvaliacaoGovernoEstadual":
                    case "AvaliacaoGovernoFederal":
                    case "AvaliacaoConselhosMunicipais":
                    case "AvaliacaoCMAS":
                    case "AvaliacaoCMDCA":
                    case "AvaliacaoOutrosConselhosMunicipais":
                    case "AvaliacaoEmpresasPrivadas":
                    case "AvaliacaoONGs":
                        if (obj.AvaliaAcoes) labels.Add("utiliza-se de avaliações realizadas independentemente por outros órgãos"); break;                        
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
