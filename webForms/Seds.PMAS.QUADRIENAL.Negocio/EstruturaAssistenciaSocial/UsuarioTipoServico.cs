using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class UsuarioTipoServico
    {
        private static IRepository<UsuarioTipoServicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UsuarioTipoServicoInfo>>();
            }
        }

        public IQueryable<UsuarioTipoServicoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public IQueryable<UsuarioTipoServicoInfo> GetByTipoServico(Int32 idTipoServico)
        {
            return _repository.GetQuery().Where(u => u.IdTipoServico == idTipoServico);

        }

        public IQueryable<UsuarioTipoServicoInfo> GetBySituacaoEspecifica(Int32 idSituacaoEspecifica)
        {
            return _repository.GetQuery().Where(t => t.SituacoesEspecificas.Any(s => s.Id == idSituacaoEspecifica));

        } 

        public UsuarioTipoServicoInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("TipoServico").Include("TipoServico.TipoProtecaoSocial").Single(m => m.Id == id);
        }        
    }
}
