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
    public class ServicosRecursosFinanceirosPrivadoFonteRecursos
    {

        private static IRepository<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>>();
            }
        }
        public IQueryable<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroPrivadoFonteRecursoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> GetByRecursoFinanceiroPrivado(int idRecursoFinanceiroFundosPrivado)
        {
            return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroFundosPrivado == idRecursoFinanceiroFundosPrivado);
        }


        public void Update(ServicoRecursoFinanceiroPrivadoFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroPrivadoFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ServicoRecursoFinanceiroPrivadoFonteRecursoInfo servico, Boolean commit)
        {

            _repository.Delete(servico);

            if (commit)
                ContextManager.Commit();
        }
    }
}
