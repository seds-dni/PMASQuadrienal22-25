using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class UnidadeOfertante
    {
        private static IRepository<UnidadeOfertanteInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UnidadeOfertanteInfo>>();
            }
        }
        private static IRepository<UnidadeOfertanteInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UnidadeOfertanteInfo>>();
            }
        }

        public IQueryable<UnidadeOfertanteInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<UnidadeOfertanteInfo> GetByIdProgramaProjeto(Int32 idProgramaProjeto)
        {
            return _repository.GetQuery().Where(t => t.IdProgramaProjeto == idProgramaProjeto);
        }

        public UnidadeOfertanteInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public void Add(UnidadeOfertanteInfo obj, Boolean commit)
        {
            _repository.Add(obj);

            //var log = Log.CreateLog(obj.I, EAcao.Add, 38, "Incluída a Unidade Privada " + obj.Nome + ".");
            //if (log != null)
            //    new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }


        public void Update(UnidadeOfertanteInfo obj, Boolean commit)
        {
            //new ValidadorUnidadePrivada().Validar(obj);
            //if (_repository.GetQuery().Any(t => t.CNPJ == obj.CNPJ && t.IdPrefeitura == obj.IdPrefeitura && t.Id != obj.Id))
            //    throw new Exception("Já existe uma Unidade Privada cadastrada com esse CNPJ.");

            _repository.Update(obj);

            ////QUADRO 39
            //var propriedadesEntity = _repository.GetModifiedProperties(obj);
            //var propriedades = GetLabelForInfo(propriedadesEntity);

            //var original = GetById(obj.Id);
            //var hasChangeCaracterizacaoAtividades = _repository.UpdateNN<CaracterizacaoAtividadesInfo>(original, obj.CaracterizacaoAtividades, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.CaracterizacaoAtividades);  
            //if (hasChangeCaracterizacaoAtividades)
            //    propriedades.Add("caracterização de atividades");

            //if (propriedades.Count > 0)
            //{
            //    String descricao = "Unidade Privada: " + obj.Id + " - " + obj.RazaoSocial + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
            //    var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 39, descricao, obj.Id);
            //    if (log != null)
            //        new Log().Add(log, false);

            //}

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(UnidadeOfertanteInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }  
    }
}
