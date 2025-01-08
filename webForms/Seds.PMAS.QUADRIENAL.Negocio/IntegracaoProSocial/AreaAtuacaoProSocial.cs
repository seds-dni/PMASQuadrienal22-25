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
    public class AreaAtuacaoProSocial
    {
        private static IRepository<AreaAtuacaoProSocialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AreaAtuacaoProSocialInfo>>();
            }
        }

        public IQueryable<AreaAtuacaoProSocialInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<AreaAtuacaoProSocialInfo> GetById(Int32 id)
        {
            return _repository.GetQuery().Where(a => a.Id == id);
        }
    }
}