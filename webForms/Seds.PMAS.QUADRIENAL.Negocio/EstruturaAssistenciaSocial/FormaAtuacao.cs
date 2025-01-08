using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class FormaAtuacao
    {
        private static IRepository<FormaAtuacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<FormaAtuacaoInfo>>();
            }
        }

        public IQueryable<FormaAtuacaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<FormaAtuacaoInfo> GetById(Int32 id)
        {
            return _repository.GetQuery().Where(f => f.Id == id);
        }

        public FormaAtuacaoInfo GetFormaAtuacaoById(Int32 id)
        {
            return _repository.Single(f => f.Id == id);
        }
    }
}