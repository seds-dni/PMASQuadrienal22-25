using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class SituacaoEspecifica
    {
        private static IRepository<SituacaoEspecificaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SituacaoEspecificaInfo>>();
            }
        }

        public IQueryable<SituacaoEspecificaInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<SituacaoEspecificaInfo> GetByUsuarios(Int32 idUsuarioTipoServico)
        {
            return _repository.GetQuery().Where(s=> s.Usuarios.Any(t=> t.Id == idUsuarioTipoServico));
        }

        public IQueryable<SituacaoEspecificaInfo> GetBySituacaoVulnerabilidade(Int32 idSituacaoVulnerabilidade)
        {
            return _repository.GetQuery().Where(s => s.IdSituacaoVulnerabilidade == idSituacaoVulnerabilidade);
        }

        public SituacaoEspecificaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
