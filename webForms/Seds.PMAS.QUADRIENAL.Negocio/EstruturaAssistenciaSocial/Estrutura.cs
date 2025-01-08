using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Estrutura
    {
        private static IRepository<EstruturaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<EstruturaInfo>>();
            }
        }

        public IQueryable<EstruturaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public EstruturaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }     
   
        public void Add(EstruturaInfo obj){
            _repository.Add(obj);
            ContextManager.Commit();
        }
    }
}
