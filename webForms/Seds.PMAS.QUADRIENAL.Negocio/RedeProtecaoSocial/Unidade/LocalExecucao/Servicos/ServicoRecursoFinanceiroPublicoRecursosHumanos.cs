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
    public class ServicoRecursoFinanceiroPublicoRecursosHumanos
    {
        private static IRepository<ServicoRecursoFinanceiroPublicoRecursosHumanosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPublicoRecursosHumanosInfo>>();
            }
        }

        public ServicoRecursoFinanceiroPublicoRecursosHumanosInfo GetByServicoRecursoFinanceiro(Int32 IdRecursoFinanceiro)
        {
            return _repository.GetQuery().Where(r => r.IdServicosRecursosFinanceirosPublico == IdRecursoFinanceiro).FirstOrDefault();
        }

        public Int32 GetTotalRHByIdServicoRecursoFinanceiro(Int32 IdRecursoFinanceiro)
        {
            return _repository.GetQuery().Where(t => t.IdServicosRecursosFinanceirosPublico == IdRecursoFinanceiro).Select(t => t.NivelFundamental + t.NivelMedio + t.SemEscolarizacao + t.NivelSuperior).FirstOrDefault();
        }


        public void Add(ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursosHumanos, Boolean commit)
        {
            Validar(recursosHumanos);

            _repository.Add(recursosHumanos);

            if (commit)
                ContextManager.Commit();
        }

        public void Update(ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursosHumanos, Boolean commmit)
        {
            Validar(recursosHumanos);
            _repository.Update(recursosHumanos);

            if (commmit)
                ContextManager.Commit();
        }

        public void Validar(ServicoRecursoFinanceiroPublicoRecursosHumanosInfo recursosHumanos) 
        {
            var lstMsg = new List<string>();

            new ValidadorRecursosHumanos().ValidarRecursosHumanosPublico(recursosHumanos);

            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
        }
    }
}
