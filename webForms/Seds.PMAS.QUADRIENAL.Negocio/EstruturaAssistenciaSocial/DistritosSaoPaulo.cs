﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class DistritosSaoPaulo
    {
        private static IRepository<DistritosSaoPauloInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DistritosSaoPauloInfo>>();
            }
        }

        public IQueryable<DistritosSaoPauloInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public DistritosSaoPauloInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
