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
    public class ConselhoMunicipalPresidenteAnterior
    {
        private static IRepository<ConselhoMunicipalPresidenteAnteriorInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConselhoMunicipalPresidenteAnteriorInfo>>();
            }
        }

        public IQueryable<ConselhoMunicipalPresidenteAnteriorInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<ConselhoMunicipalPresidenteAnteriorInfo> GetByConselhoMunicipal(int idConselhoMunicipal)
        {
            return _repository.GetQuery().Where(m => m.IdConselhoMunicipal == idConselhoMunicipal);
        }

        public void Add(ConselhoMunicipalPresidenteAnteriorInfo obj, Boolean commit)
        {
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ConselhoMunicipalPresidenteAnteriorInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
