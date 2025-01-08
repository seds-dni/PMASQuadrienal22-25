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
    public class ServicoRecursoFinanceiroCREASFonteRecurso
    {
        private static IRepository<ServicoRecursoFinanceiroCREASFonteRecursoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCREASFonteRecursoInfo>>();
            }
        }
        public IQueryable<ServicoRecursoFinanceiroCREASFonteRecursoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroCREASFonteRecursoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ServicoRecursoFinanceiroCREASFonteRecursoInfo> GetByRecursoFinanceiroCREAS(int idRecursoFinanceiroFundosCREAS)
        {
            return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroFundosCREAS == idRecursoFinanceiroFundosCREAS);
        }

        public void Update(ServicoRecursoFinanceiroCREASFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroCREASFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ServicoRecursoFinanceiroCREASFonteRecursoInfo servico, Boolean commit)
        {

            _repository.Delete(servico);

            if (commit)
                ContextManager.Commit();
        }
    }
}
