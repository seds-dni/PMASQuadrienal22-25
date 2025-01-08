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
    public class PETITipoAcao
    {
        private static IRepository<PETITipoAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PETITipoAcaoInfo>>();
            }
        }

        public IQueryable<PETITipoAcaoInfo> GetAll()
        {
            return _repository.GetObjectSet().Include("PETIEixoAtuacao");
        }

        public IQueryable<PETITipoAcaoInfo> GetByEixoAtuacao(Int32 idEixoAtuacao)
        {
            return _repository.GetObjectSet().Include("PETIEixoAtuacao").Where(a => a.IdPETIEixoAtuacao == idEixoAtuacao);
        }
    }
}