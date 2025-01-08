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
    public class ServicoRecursoFinanceiroPrivadoRecursosHumanos
    {
        private static IRepository<ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo>>();
            }
        }

        public ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo GetByServicoRecursoFinanceiro(Int32 IdRecursoFinanceiro)
        {
            return _repository.GetQuery().Where(r => r.IdServicosRecursosFinanceirosPrivado == IdRecursoFinanceiro).FirstOrDefault();
        }

        public Int32 GetTotalRHByIdServicoRecursoFinanceiro(Int32 IdRecursoFinanceiro)
        {
            return _repository.GetQuery().Where(t => t.IdServicosRecursosFinanceirosPrivado == IdRecursoFinanceiro).Select(t => t.NivelFundamental + t.NivelMedio + t.SemEscolarizacao + t.NivelSuperior).FirstOrDefault();
        }


        public void Add(ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursosHumanos, Boolean commit)
        {
            Validar(recursosHumanos);

            _repository.Add(recursosHumanos);

            if (commit)
                ContextManager.Commit();
        }

        public void Update(ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursosHumanos, Boolean commmit)
        {
            Validar(recursosHumanos);
            _repository.Update(recursosHumanos);

            if (commmit)
                ContextManager.Commit();
        }

        public void Validar(ServicoRecursoFinanceiroPrivadoRecursosHumanosInfo recursosHumanos)
        {
            var lstMsg = new List<string>();
            try
            {
                new ValidadorRecursosHumanos().ValidarRHPrivado(recursosHumanos);
            }
            catch (Exception ex)
            {
                lstMsg.Add(ex.Message);
            }


            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
        }
    }
}
