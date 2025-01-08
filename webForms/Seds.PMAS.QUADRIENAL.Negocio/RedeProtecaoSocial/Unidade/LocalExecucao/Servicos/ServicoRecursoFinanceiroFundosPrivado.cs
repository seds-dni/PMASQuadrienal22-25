using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class ServicoRecursoFinanceiroFundosPrivado
    {

        #region construtor
        public ServicoRecursoFinanceiroFundosPrivado()
        {

        } 
        #endregion

        #region repositorios
        private static IRepository<ServicoRecursoFinanceiroFundosPrivadoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosPrivadoInfo>>();
            }
        }
        
        #endregion

        #region crud

        #region consultas
        public IQueryable<ServicoRecursoFinanceiroFundosPrivadoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroFundosPrivadoInfo GetById(int id)
        {
            return _repository.GetObjectSet().SingleOrDefault(m => m.Id == id);
        }
        #endregion


        public void Update(ServicoRecursoFinanceiroFundosPrivadoInfo entidade)
        {
            if (this.ValidarValoresFundosPrivado(entidade))
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
        private bool ValidarValoresFundosPrivado(ServicoRecursoFinanceiroFundosPrivadoInfo entidade)
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
