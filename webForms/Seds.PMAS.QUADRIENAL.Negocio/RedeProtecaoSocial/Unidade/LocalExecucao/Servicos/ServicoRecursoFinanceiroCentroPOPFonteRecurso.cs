using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Linq;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ServicoRecursoFinanceiroCentroPOPFonteRecurso
    {
        private static IRepository<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>>();
            }
        }
        public IQueryable<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> GetByRecursoFinanceiroFonteRecursosCentroPOP(int idRecursoFinanceiroFundosCentroPOP)
        {
            return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroFundosCentroPOP == idRecursoFinanceiroFundosCentroPOP);
        }

        public void Update(ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Update(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo obj, Boolean commit)
        {
            new ValidadorServicosFinanceirosFonteRecursos().Validar(obj);

            _repository.Add(obj);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo servico, Boolean commit)
        {

            _repository.Delete(servico);

            if (commit)
                ContextManager.Commit();
        }
    }
}
