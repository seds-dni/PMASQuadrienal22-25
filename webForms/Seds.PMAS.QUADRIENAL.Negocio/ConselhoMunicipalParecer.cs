using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ConselhoMunicipalParecer
    {
        private static IRepository<ConselhoMunicipalParecerInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConselhoMunicipalParecerInfo>>();
            }
        }

        public IQueryable<ConselhoMunicipalParecerInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ConselhoMunicipalParecerInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public ConselhoMunicipalParecerInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(ConselhoMunicipalParecerInfo obj, Boolean commit)
        {
            new ValidadorConselhoMunicipalParecer().Validar(obj);
            if (GetByPrefeitura(obj.IdPrefeitura) != null)
                throw new Exception("Já existe cadastrado parecer do CMAS para a Prefeitura!");
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(ConselhoMunicipalParecerInfo obj, Boolean commit)
        {
            new ValidadorConselhoMunicipalParecer().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }       
    }
}
