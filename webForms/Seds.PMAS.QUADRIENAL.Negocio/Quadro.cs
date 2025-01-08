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
using Seds.Seguranca.Token;
using System.Threading;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Quadro
    {
        private static IRepository<QuadroInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<QuadroInfo>>();
            }
        }

        public IQueryable<QuadroInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public QuadroInfo GetById(int id)
        {
            var p = _repository.Single(m => m.Id == id);
            return p;
        }

        public IQueryable<QuadroInfo> GetByQuadroPai(int idQuadroPai)
        {
            return _repository.GetObjectSet().Where(m => m.IdPai == idQuadroPai);
        }        
    }
}
