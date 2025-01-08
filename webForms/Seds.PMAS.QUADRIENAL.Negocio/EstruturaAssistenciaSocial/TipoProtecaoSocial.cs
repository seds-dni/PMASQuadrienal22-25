using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class TipoProtecaoSocial
    {
        private static IRepository<TipoProtecaoSocialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<TipoProtecaoSocialInfo>>();
            }
        }

        public IQueryable<TipoProtecaoSocialInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public TipoProtecaoSocialInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
