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
    public class TransferenciaRendaCofinanciamento
    {
        private static IRepository<ServicoRecursoFinanceiroTransferenciaRendaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroTransferenciaRendaInfo>>();
            }
        }
        private static IRepository<ConsultaTransferenciaRendaInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaTransferenciaRendaInfo>>();
            }
        }

        public IQueryable<ServicoRecursoFinanceiroTransferenciaRendaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroTransferenciaRendaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);            
        }

        public IQueryable<ServicoRecursoFinanceiroTransferenciaRendaInfo> GetByTransferenciaRenda(int idTransferenciaRenda)
        {
            return _repository.GetObjectSet().Include("ServicosRecursosFinanceirosCRAS").Include("ServicosRecursosFinanceirosCREAS").Include("ServicosRecursosFinanceirosCentroPOP").Include("ServicosRecursosFinanceirosPublico").Include("ServicosRecursosFinanceirosPrivado").Where(m => m.IdTransferenciaRenda == idTransferenciaRenda);
        }

        public List<ConsultaTransferenciaRendaCofinanciamentoInfo> GetConsultaByTransferenciaRenda(int idTransferenciaRenda)
        {
            return (ContextManager.GetContext() as PMASContext).GetTransferenciaRendaCofinanciamentoByTransferenciaRenda(idTransferenciaRenda);
        }

        public void Update(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento, Boolean commit)
        {
            new ValidadorTransferenciaRendaCofinanciamento().Validar(cofinanciamento);
            cofinanciamento = CriarTransferenciaRendaCofinanciamentoCompleto(cofinanciamento);
            _repository.Update(cofinanciamento);

            var descricao = "Transferência de Renda " + cofinanciamento.TransferenciaRenda.Nome + ": atualizado o número de usuários vinculados ao programa do " + CriarTransferenciaDescricaoCompleta(cofinanciamento) + ".";
            var log = Log.CreateLog(cofinanciamento.TransferenciaRenda.IdPrefeitura, EAcao.Update, GetQuadro((ETipoTransferenciaRenda)cofinanciamento.TransferenciaRenda.IdTipoTransferenciaRenda), descricao, cofinanciamento.IdTransferenciaRenda);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento, Boolean commit)
        {
            new ValidadorTransferenciaRendaCofinanciamento().Validar(cofinanciamento);

            try
            {
                cofinanciamento = this.CriarTransferenciaRendaCofinanciamentoCompleto(cofinanciamento);                
                
                _repository.Add(cofinanciamento);
                
                var nomeTransferencia = (cofinanciamento.TransferenciaRenda != null ? cofinanciamento.TransferenciaRenda.Nome : string.Empty);
               
                var descTransferencia = CriarTransferenciaDescricaoCompleta(cofinanciamento);

                var descricao = string.Concat("Transferência de Renda ", nomeTransferencia, ": vinculado o ", descTransferencia , ".");
                
                var idTransferencia = cofinanciamento.TransferenciaRenda != null ? cofinanciamento.TransferenciaRenda.IdPrefeitura : 0;
                
                var idTipoTransferenciaRenda = cofinanciamento.TransferenciaRenda != null ? cofinanciamento.TransferenciaRenda.IdTipoTransferenciaRenda : 0;

                if (cofinanciamento.TransferenciaRenda != null)
                {
                    var log = Log.CreateLog(idTransferencia, EAcao.Add, GetQuadro((ETipoTransferenciaRenda)idTipoTransferenciaRenda), descricao, cofinanciamento.IdTransferenciaRenda);

                    if (log != null)
                        new Log().Add(log, false);
                }

                if (commit)
                    ContextManager.Commit();
            }
            catch (Exception ex)
            {
                if(Extensions.GetExceptionMessage(ex).Contains("UNQ"))
                    throw new Exception("Já existe vinculado esse tipo de serviço e tipo de usuário dessa unidade.");
                throw ex;
            }
        }

        public void Delete(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento, Boolean commit, Boolean saveLog)
        {
            LogInfo log = null;
            if (saveLog)
            {
                var descricao = "Transferência de Renda " + cofinanciamento.TransferenciaRenda.Nome + ": desvinculado o " + CriarTransferenciaDescricaoCompleta(cofinanciamento) + ".";
                log = Log.CreateLog(cofinanciamento.TransferenciaRenda.IdPrefeitura, EAcao.Remove, GetQuadro((ETipoTransferenciaRenda)cofinanciamento.TransferenciaRenda.IdTipoTransferenciaRenda), descricao, cofinanciamento.IdTransferenciaRenda);
            }
            cofinanciamento.TransferenciaRenda = null;

            _repository.Delete(cofinanciamento);

            if (log != null)
                new Log().Add(log, false);
            if (commit)
                ContextManager.Commit();
        }

        public Int32 GetQuadro(ETipoTransferenciaRenda transferenciaRenda)
        {
            switch (transferenciaRenda)
            {
                case ETipoTransferenciaRenda.BolsaFamilia:
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil: return 75;
                case ETipoTransferenciaRenda.AcaoJovem:
                case ETipoTransferenciaRenda.RendaCidada:
                case ETipoTransferenciaRenda.SaoPauloSolidario: return 76;
                case ETipoTransferenciaRenda.Outros: return 77;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso: return 79;
                case ETipoTransferenciaRenda.ProsperaFamilia: return 104;
                case ETipoTransferenciaRenda.FCadUnico: return 105;
                case ETipoTransferenciaRenda.AuxilioAluguel: return 107;
                case ETipoTransferenciaRenda.FVigilancia: return 106;

            }
            return 0;
        }

        public ServicoRecursoFinanceiroTransferenciaRendaInfo CriarTransferenciaRendaCofinanciamentoCompleto(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento)
        {
            cofinanciamento.TransferenciaRenda = new TransferenciaRenda().GetById(cofinanciamento.IdTransferenciaRenda);
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

            if (cofinanciamento.Servico != null)
            {
                cofinanciamento.Servico.UsuarioTipoServico = new UsuarioTipoServico().GetById(cofinanciamento.Servico.IdUsuarioTipoServico);    
            }
            

            return cofinanciamento;

        }

        public ServicoRecursoFinanceiroTransferenciaRendaInfo GetFullById(int id)
        {
            var cofinanciamento = _repository.Single(m => m.Id == id);
            return CriarTransferenciaRendaCofinanciamentoCompleto(cofinanciamento);
        }

        public String CriarTransferenciaDescricaoCompleta(ServicoRecursoFinanceiroTransferenciaRendaInfo cofinanciamento)
        {
            var sb = new StringBuilder();


            if (cofinanciamento.Servico != null)
            {
                sb.Append("Servico de Proteção Social " + cofinanciamento.Servico.UsuarioTipoServico.TipoServico.TipoProtecaoSocial.Nome + " - " + cofinanciamento.Servico.UsuarioTipoServico.TipoServico.Nome + " - " + cofinanciamento.Servico.UsuarioTipoServico.Nome);    
            }
            
            
            
            if (cofinanciamento.IdServicosRecursosFinanceirosCentroPOP.HasValue)
                sb.Append(" do Local de Execução " + " - " + cofinanciamento.ServicosRecursosFinanceirosCentroPOP.CentroPOP.Nome);
            if (cofinanciamento.IdServicosRecursosFinanceirosCRAS.HasValue)
                sb.Append(" do Local de Execução " + " - " + cofinanciamento.ServicosRecursosFinanceirosCRAS.CRAS.Nome);
            if (cofinanciamento.IdServicosRecursosFinanceirosCREAS.HasValue)
                sb.Append(" do Local de Execução " + " - " + cofinanciamento.ServicosRecursosFinanceirosCREAS.CREAS.Nome);

            if (cofinanciamento.IdServicosRecursosFinanceirosPublico.HasValue && cofinanciamento.IdServicosRecursosFinanceirosPublico.Value != 0)
                sb.Append(" do Local de Execução " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Nome + " (Unidade Pública " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.RazaoSocial + ")");
            if (cofinanciamento.IdServicosRecursosFinanceirosPrivado.HasValue)
                sb.Append(" do Local de Execução " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Nome + " (Unidade Privada " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.Id + " - " + cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.RazaoSocial + ")");

            return sb.ToString();
        }
    }
}
