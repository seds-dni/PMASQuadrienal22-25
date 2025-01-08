using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class MotivoEstadualizado
    {
        private static IRepository<MotivoEstadualizadoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MotivoEstadualizadoInfo>>();
            }
        }

        public IQueryable<MotivoEstadualizadoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public MotivoEstadualizadoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}