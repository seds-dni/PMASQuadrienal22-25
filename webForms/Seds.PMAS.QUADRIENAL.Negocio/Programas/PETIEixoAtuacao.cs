using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PETIEixoAtuacao
    {
        private static IRepository<PETIEixoAtuacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PETIEixoAtuacaoInfo>>();
            }
        }

        public IQueryable<PETIEixoAtuacaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }
    }
}