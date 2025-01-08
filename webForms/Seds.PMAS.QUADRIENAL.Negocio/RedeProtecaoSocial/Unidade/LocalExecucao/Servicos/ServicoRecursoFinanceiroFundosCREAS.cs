using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class ServicoRecursoFinanceiroFundosCREAS
    {

        #region construtor
        public ServicoRecursoFinanceiroFundosCREAS()
        {

        } 
        #endregion

        #region repositorios
        private static IRepository<ServicoRecursoFinanceiroFundosCREASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosCREASInfo>>();
            }
        } 
        #endregion

        #region crud
        #region consultas
        public IQueryable<ServicoRecursoFinanceiroFundosCREASInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroFundosCREASInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }
        #endregion



        public void Update(ServicoRecursoFinanceiroFundosCREASInfo entidade)
        {
            if (this.ValidarValoresFundosCREAS(entidade))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _repository.Update(entidade);
                    _repository.SaveChanges();
                    scope.Complete();
                }
            }
        }

        #endregion

        #region Validacao
        private bool ValidarValoresFundosCREAS(ServicoRecursoFinanceiroFundosCREASInfo entidade)
        {
            decimal total = 0;
            foreach (var property in entidade.GetType().GetProperties())
            {
                var valor = GetPropValue(entidade, property.Name);
                if (valor != null && valor is Decimal)
                {
                    total += (Decimal)valor;
                }
            }

            return (total > 0);
        }
        #endregion

        #region Helper
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        #endregion

      

    }
}
