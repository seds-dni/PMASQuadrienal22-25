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
    public class PETISituacaoAcao
    {
        private static IRepository<PETISituacaoAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PETISituacaoAcaoInfo>>();
            }
        }

        public IQueryable<PETISituacaoAcaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }
    }
}