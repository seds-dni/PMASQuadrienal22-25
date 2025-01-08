using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class OrgaoResponsavel
    {
        private static IRepository<OrgaoResponsavelInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<OrgaoResponsavelInfo>>();
            }
        }

        public IQueryable<OrgaoResponsavelInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public OrgaoResponsavelInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}