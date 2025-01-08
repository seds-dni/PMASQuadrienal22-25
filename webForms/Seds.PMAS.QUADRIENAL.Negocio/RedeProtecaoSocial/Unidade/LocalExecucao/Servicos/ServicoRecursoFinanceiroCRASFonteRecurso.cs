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
    public class ServicoRecursoFinanceiroCRASFonteRecurso
    {
        private static IRepository<ServicoRecursoFinanceiroCRASFonteRecursoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCRASFonteRecursoInfo>>();
            }
        }
        public IQueryable<ServicoRecursoFinanceiroCRASFonteRecursoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroCRASFonteRecursoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ServicoRecursoFinanceiroCRASFonteRecursoInfo> GetByRecursoFinanceiroCRAS(int idRecursoFinanceiroFundosCRAS)
        {
            //return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroCRAS == idRecursoFinanceiroCRAS);
            return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroFundosCRAS == idRecursoFinanceiroFundosCRAS);
        }

        public void Update(ServicoRecursoFinanceiroCRASFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroCRASFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ServicoRecursoFinanceiroCRASFonteRecursoInfo servico, Boolean commit)
        {

            _repository.Delete(servico);

            if (commit)
                ContextManager.Commit();
        }
    }
}
