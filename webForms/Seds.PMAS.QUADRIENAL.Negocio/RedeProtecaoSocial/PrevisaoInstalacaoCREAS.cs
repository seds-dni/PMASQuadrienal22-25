using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class PrevisaoInstalacaoCREAS
    {
        private static IRepository<PrevisaoInstalacaoCREASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrevisaoInstalacaoCREASInfo>>();
            }
        }
        public IQueryable<PrevisaoInstalacaoCREASInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrevisaoInstalacaoCREASInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<PrevisaoInstalacaoCREASInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(PrevisaoInstalacaoCREASInfo previsao, Boolean commit)
        {
            _repository.Add(previsao);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PrevisaoInstalacaoCREASInfo previsao, Boolean commit)
        {
            _repository.Delete(previsao);
            if (commit)
                ContextManager.Commit();
        }
    }
}
