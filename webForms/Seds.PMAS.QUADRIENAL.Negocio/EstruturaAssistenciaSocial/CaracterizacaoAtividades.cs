using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CaracterizacaoAtividades
    {
        private static IRepository<CaracterizacaoAtividadesInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CaracterizacaoAtividadesInfo>>();
            }
        }

        public IQueryable<CaracterizacaoAtividadesInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CaracterizacaoAtividadesInfo GetById(int id)
        {
            return _repository.GetQuery().SingleOrDefault(m => m.Id == id);
        }
    }
}
