using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PrefeituraBeneficioEventualServico
    {
        private static IRepository<PrefeituraBeneficioEventualServicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraBeneficioEventualServicoInfo>>();
            }
        }

        public IQueryable<PrefeituraBeneficioEventualServicoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrefeituraBeneficioEventualServicoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);            
        }

        public IQueryable<PrefeituraBeneficioEventualServicoInfo> GetByBeneficioEventual(int idPrefeituraBeneficioEventual)
        {
            return _repository.GetObjectSet().Where(m => m.IdPrefeituraBeneficioEventual == idPrefeituraBeneficioEventual);
        }

        public List<ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo> GetConsultaByBeneficioEventual(int idPrefeituraBeneficioEventual)
        {
            return (ContextManager.GetContext() as PMASContext).GetPrefeituraBeneficioEventualServicosByBeneficioEventual(idPrefeituraBeneficioEventual).Where(s => s.Exercicio >= 2022).ToList();
        }

        public void Update(PrefeituraBeneficioEventualServicoInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraBeneficioEventualServico().Validar(obj);
            obj = GetFull(obj);

            var beneficiariosServico = (GetByBeneficioEventual(obj.IdPrefeituraBeneficioEventual).Where(b => b.Id != obj.Id).Sum(b => (int?)b.NumeroBeneficiarios) ?? 0)
                + obj.NumeroBeneficiarios;
            var temp = new PrefeituraBeneficioEventual().GetById(obj.IdPrefeituraBeneficioEventual).MediaSemestralBeneficiarios;
            var mediaBeneficiarios = temp.HasValue ? temp.Value : 0;

            if (beneficiariosServico > mediaBeneficiarios)
            {
                throw new Exception("A soma dos beneficiários de todos os serviços não pode ser maior que o número de atendidos pelo benefício!");
            }

            _repository.Update(obj);

            var descricao = "Benefício Eventual " + obj.PrefeituraBeneficioEventual.TipoBeneficioEventual.Nome + ": atualizado o número de usuários que recebem o benefício eventual do " + GetFullDescription(obj) + ".";
            var log = Log.CreateLog(obj.PrefeituraBeneficioEventual.IdPrefeitura, EAcao.Update, 78, descricao, obj.IdPrefeituraBeneficioEventual);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(PrefeituraBeneficioEventualServicoInfo obj, Boolean commit)
        {
            new ValidadorPrefeituraBeneficioEventualServico().Validar(obj);
            try
            {
                obj = GetFull(obj);

                var beneficiariosServico = (GetByBeneficioEventual(obj.IdPrefeituraBeneficioEventual).Where(b => b.Id != obj.Id).Sum(b => (int?)b.NumeroBeneficiarios) ?? 0)
                    + obj.NumeroBeneficiarios;
                var temp = new PrefeituraBeneficioEventual().GetById(obj.IdPrefeituraBeneficioEventual).MediaSemestralBeneficiarios;
                var mediaBeneficiarios = temp.HasValue ? temp.Value : 0;

                /*if (beneficiariosServico > mediaBeneficiarios)
                {
                    throw new Exception("O numero de usuarios do serviço associados a esse beneficio não deve ser maior que o numero total de atendidos pelo beneficio!");
                }

                if (obj.Servico != null && obj.NumeroBeneficiarios > obj.Servico.PrevisaoAnualNumeroAtendidos)
                    throw new Exception("O numero de usuarios do serviço associado a esse beneficio não deve ser maior que o numero total de atendidos pelo serviço!");
                 * */
                _repository.Add(obj);

                var descricao = "Benefício Eventual " + obj.PrefeituraBeneficioEventual.TipoBeneficioEventual.Nome + ": vinculado o " + GetFullDescription(obj) + ".";
                var log = Log.CreateLog(obj.PrefeituraBeneficioEventual.IdPrefeitura, EAcao.Add, 78, descricao, obj.IdPrefeituraBeneficioEventual);
                if (log != null)
                    new Log().Add(log, false);

                if (commit)
                    ContextManager.Commit();
            }
            catch (Exception ex)
            {
                if(Extensions.GetExceptionMessage(ex).Contains("UNQ"))
                    throw new Exception("Este programa já foi associado a esse serviço..");
                throw ex;
            }
        }

        public void Delete(PrefeituraBeneficioEventualServicoInfo obj, Boolean commit,Boolean saveLog)
        {
            LogInfo log = null;
            if (saveLog)
            {
                var descricao = "Benefício Eventual " + obj.PrefeituraBeneficioEventual.TipoBeneficioEventual.Nome + ": desvinculado o " + GetFullDescription(obj) + ".";
                log = Log.CreateLog(obj.PrefeituraBeneficioEventual.IdPrefeitura, EAcao.Remove, 78, descricao, obj.IdPrefeituraBeneficioEventual);
            }
            obj.PrefeituraBeneficioEventual = null;

            _repository.Delete(obj);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        

        public PrefeituraBeneficioEventualServicoInfo GetFull(PrefeituraBeneficioEventualServicoInfo cofinanciamento)
        {
            cofinanciamento.PrefeituraBeneficioEventual = new PrefeituraBeneficioEventual().GetById(cofinanciamento.IdPrefeituraBeneficioEventual);
            if (cofinanciamento.IdServicosRecursosFinanceirosCentroPOP.HasValue)
            {
                cofinanciamento.ServicosRecursosFinanceirosCentroPOP = new ServicoRecursoFinanceiroCentroPOP().GetById(cofinanciamento.IdServicosRecursosFinanceirosCentroPOP.Value);
                cofinanciamento.ServicosRecursosFinanceirosCentroPOP.CentroPOP = new CentroPOP().GetById(cofinanciamento.ServicosRecursosFinanceirosCentroPOP.IdCentroPOP);
            }
            if (cofinanciamento.IdServicosRecursosFinanceirosCRAS.HasValue)
            {
                cofinanciamento.ServicosRecursosFinanceirosCRAS = new ServicoRecursoFinanceiroCRAS().GetById(cofinanciamento.IdServicosRecursosFinanceirosCRAS.Value);
                cofinanciamento.ServicosRecursosFinanceirosCRAS.CRAS = new CRAS().GetById(cofinanciamento.ServicosRecursosFinanceirosCRAS.IdCRAS);
            }
            if (cofinanciamento.IdServicosRecursosFinanceirosCREAS.HasValue)
            {
                cofinanciamento.ServicosRecursosFinanceirosCREAS = new ServicoRecursoFinanceiroCREAS().GetById(cofinanciamento.IdServicosRecursosFinanceirosCREAS.Value);
                cofinanciamento.ServicosRecursosFinanceirosCREAS.CREAS = new CREAS().GetById(cofinanciamento.ServicosRecursosFinanceirosCREAS.IdCREAS);
            }
            if (cofinanciamento.IdServicosRecursosFinanceirosPublico.HasValue)
            {
                cofinanciamento.ServicosRecursosFinanceirosPublico = new ServicoRecursoFinanceiroPublico().GetById(cofinanciamento.IdServicosRecursosFinanceirosPublico.Value);
                cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao = new LocalExecucaoPublico().GetById(cofinanciamento.ServicosRecursosFinanceirosPublico.IdLocalExecucao);
            }
            if (cofinanciamento.IdServicosRecursosFinanceirosPrivado.HasValue)
            {
                cofinanciamento.ServicosRecursosFinanceirosPrivado = new ServicoRecursoFinanceiroPrivado().GetById(cofinanciamento.IdServicosRecursosFinanceirosPrivado.Value);
                cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao = new LocalExecucaoPrivado().GetById(cofinanciamento.ServicosRecursosFinanceirosPrivado.IdLocalExecucao);
            }

            cofinanciamento.Servico.UsuarioTipoServico = new UsuarioTipoServico().GetById(cofinanciamento.Servico.IdUsuarioTipoServico);

            return cofinanciamento;

        }

        public PrefeituraBeneficioEventualServicoInfo GetFullById(int id)
        {
            var cofinanciamento = _repository.Single(m => m.Id == id);
            return GetFull(cofinanciamento);
        }

        public String GetFullDescription(PrefeituraBeneficioEventualServicoInfo cofinanciamento)
        {
            var sb = new StringBuilder();
            sb.Append("Servico de Proteção Social " + cofinanciamento.Servico.UsuarioTipoServico.TipoServico.TipoProtecaoSocial.Nome + " - " + cofinanciamento.Servico.UsuarioTipoServico.TipoServico.Nome + " - " + cofinanciamento.Servico.UsuarioTipoServico.Nome);
            if (cofinanciamento.IdServicosRecursosFinanceirosCentroPOP.HasValue)
                sb.Append(" do Local de Execução " + " - " + cofinanciamento.ServicosRecursosFinanceirosCentroPOP.CentroPOP.Nome);
            if (cofinanciamento.IdServicosRecursosFinanceirosCRAS.HasValue)
                sb.Append(" do Local de Execução " + " - " + cofinanciamento.ServicosRecursosFinanceirosCRAS.CRAS.Nome);
            if (cofinanciamento.IdServicosRecursosFinanceirosCREAS.HasValue)
                sb.Append(" do Local de Execução " + " - " + cofinanciamento.ServicosRecursosFinanceirosCREAS.CREAS.Nome);

            if (cofinanciamento.IdServicosRecursosFinanceirosPublico.HasValue)
                sb.Append(" do Local de Execução " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Nome + " (Unidade Pública " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.RazaoSocial + ")");
            if (cofinanciamento.IdServicosRecursosFinanceirosPrivado.HasValue)
                sb.Append(" do Local de Execução " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Nome + " (Unidade Privada " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.RazaoSocial + ")");

            return sb.ToString();
        }
    }
}
