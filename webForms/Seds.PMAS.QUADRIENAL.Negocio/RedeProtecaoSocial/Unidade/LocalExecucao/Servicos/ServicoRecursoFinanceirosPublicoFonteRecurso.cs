using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ServicoRecursoFinanceiroPublicoFonteRecurso
    {
        private static IRepository<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>>();
            }
        }
        public IQueryable<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroPublicoFonteRecursoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> GetByRecursoFinanceiroPublico(int idRecursoFinanceiroFundosPublico)
        {
            //TODO: DBM VALIDAR A MUDANCA DA BASE
            //return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroPublico == idRecursoFinanceiroPublico);
            return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroFundosPublico == idRecursoFinanceiroFundosPublico);
        }

        public void Update(ServicoRecursoFinanceiroPublicoFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroPublicoFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ServicoRecursoFinanceiroPublicoFonteRecursoInfo servico, Boolean commit)
        {

            _repository.Delete(servico);

            if (commit)
                ContextManager.Commit();
        }
    }
}
