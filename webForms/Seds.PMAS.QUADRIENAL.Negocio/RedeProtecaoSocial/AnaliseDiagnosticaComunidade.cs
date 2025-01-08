
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Linq;
namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AnaliseDiagnosticaComunidade
    {

        private static IRepository<AnaliseDiagnosticaComunidadeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AnaliseDiagnosticaComunidadeInfo>>();
            }
        }

        public IQueryable<AnaliseDiagnosticaComunidadeInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AnaliseDiagnosticaComunidadeInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public AnaliseDiagnosticaComunidadeInfo GetByPrefeitura(int idPrefeitura, int idExercicio)
        {
            return _repository.GetQuery().FirstOrDefault(m => m.IdPrefeitura == idPrefeitura && m.IdExercicio == idExercicio);
        }

        public AnaliseDiagnosticaComunidadeInfo GetByPrefeituraExercicio(int idPrefeitura, int idExercicio)
        {
            return _repository.GetQuery().FirstOrDefault(m => m.IdPrefeitura == idPrefeitura && m.IdExercicio == idExercicio);
        }


        public void Update(AnaliseDiagnosticaComunidadeInfo obj, Boolean commit)
        {
            new ValidadorAnaliseDiagnostica().ValidarComunidade(obj);
        
            _repository.Update(obj);

            //var propriedadesEntity = _repository.GetModifiedProperties(obj);
            //var propriedades = GetLabelForInfo(propriedadesEntity);
            //if (propriedades.Count > 0)
            //{
            //    var sv = new SituacaoVulnerabilidade().GetById(obj.IdSituacaoVulnerabilidade);
            //    String descricao = "Situação de Vulnerabilidade: " + sv.Nome + "." + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
            //    var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 15, descricao, obj.Id);
            //    if (log != null)
            //        new Log().Add(log, false);
            //}
            if (commit)
                ContextManager.Commit();
        }

        public void Add(AnaliseDiagnosticaComunidadeInfo obj, Boolean commit)
        {
            new ValidadorAnaliseDiagnostica().ValidarComunidade(obj);
           
            _repository.Add(obj);

            //var sv = new SituacaoVulnerabilidade().GetById(obj.IdSituacaoVulnerabilidade);
            //var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 15, "Incluída a Situação de Vulnerabilidade " + sv.Nome + ".");
            //if (log != null)
            //    new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }
    }
}
