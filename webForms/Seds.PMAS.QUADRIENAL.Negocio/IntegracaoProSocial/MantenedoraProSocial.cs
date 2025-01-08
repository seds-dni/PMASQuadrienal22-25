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
    public class MantenedoraProSocial
    {
        private static IRepository<MantenedoraProSocialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MantenedoraProSocialInfo>>();
            }
        }

        public IQueryable<MantenedoraProSocialInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<MantenedoraProSocialInfo> GetByCNPJ(String cnpj)
        {
            return _repository.GetQuery().Where(m => m.CNPJ == cnpj);
        }
    }
}