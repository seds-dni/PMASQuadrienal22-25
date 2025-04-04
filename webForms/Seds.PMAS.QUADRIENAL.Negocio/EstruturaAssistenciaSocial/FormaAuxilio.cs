﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class FormaAuxilio
    {
        private static IRepository<FormaAuxilioInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<FormaAuxilioInfo>>();
            }
        }

        public IQueryable<FormaAuxilioInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public FormaAuxilioInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
