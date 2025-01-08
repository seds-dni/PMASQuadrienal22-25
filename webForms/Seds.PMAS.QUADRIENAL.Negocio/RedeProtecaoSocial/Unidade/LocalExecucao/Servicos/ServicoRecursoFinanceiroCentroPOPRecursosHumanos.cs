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
    public class ServicoRecursoFinanceiroCentroPOPRecursosHumanos
    {
        private static IRepository<ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo>>();
            }
        }

        public ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo GetByServicoRecursoFinanceiro(Int32 IdRecursoFinanceiro)
        {
            return _repository.GetQuery().Where(r => r.IdServicosRecursosFinanceirosCentroPOP == IdRecursoFinanceiro).FirstOrDefault();
        }

        public Int32 GetTotalRHByIdServicoRecursoFinanceiro(Int32 IdRecursoFinanceiro)
        {
            return _repository.GetQuery().Where(t => t.IdServicosRecursosFinanceirosCentroPOP == IdRecursoFinanceiro).Select(t => t.NivelFundamental + t.NivelMedio + t.SemEscolarizacao + t.NivelSuperior).FirstOrDefault();
        }


        public void Add(ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo recursosHumanos, Boolean commit)
        {
            Validar(recursosHumanos);

            _repository.Add(recursosHumanos);

            if (commit)
                ContextManager.Commit();
        }

        public void Update(ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo recursosHumanos, Boolean commmit)
        {
            Validar(recursosHumanos);
            _repository.Update(recursosHumanos);

            if (commmit)
                ContextManager.Commit();
        }

        public void Validar(ServicoRecursoFinanceiroCentroPopRecursosHumanosInfo recursosHumanos) 
        {
            var lstMsg = new List<string>();

            new ValidadorRecursosHumanos().ValidarCREAS(recursosHumanos);

            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
        }
    }
}
