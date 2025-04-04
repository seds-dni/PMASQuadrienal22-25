﻿using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AvaliacaoLocalExecucao
    {
        private static IRepository<AvaliacaoLocalExecucaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AvaliacaoLocalExecucaoInfo>>();
            }
        }

        public IQueryable<AvaliacaoLocalExecucaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AvaliacaoLocalExecucaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
