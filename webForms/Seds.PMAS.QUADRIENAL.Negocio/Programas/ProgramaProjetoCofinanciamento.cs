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
    public class ProgramaProjetoCofinanciamento
    {
        private static IRepository<ProgramaProjetoCofinanciamentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ProgramaProjetoCofinanciamentoInfo>>();
            }
        }
        private static IRepository<ConsultaProgramaProjetoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaProgramaProjetoInfo>>();
            }
        }

        public IQueryable<ProgramaProjetoCofinanciamentoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ProgramaProjetoCofinanciamentoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public IQueryable<ProgramaProjetoCofinanciamentoInfo> GetByProgramaProjeto(int idProgramaProjeto)
        {
            return _repository.GetQuery().Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public List<ConsultaProgramaProjetoServicoCofinanciamentoInfo> GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(int IdServicosRecursosFinanceiros, int IdLocal)
        {
            return (ContextManager.GetContext() as PMASContext).GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceiros(IdServicosRecursosFinanceiros, IdLocal);
        }

        public List<ConsultaProgramaProjetoServicoCofinanciamentoFundosInfo> GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(int IdServicosRecursosFinanceiros, int IdLocal)
        {
            return (ContextManager.GetContext() as PMASContext).GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(IdServicosRecursosFinanceiros, IdLocal);
        }

        public List<ConsultaProgramaProjetoCofinanciamentoInfo> GetConsultaByProgramaProjeto(int idProgramaProjeto)
        {
            return (ContextManager.GetContext() as PMASContext).GetProgramaProjetoCofinanciamentoByProgramaProjeto(idProgramaProjeto);
        }

        public List<ConsultarServicosDiretrizesInfo> GetConsultaByServicosDiretrizes(int idPrefeitura,int idAnalise,int exercicio)
        {
            return (ContextManager.GetContext() as PMASContext).GetServicosDiretrizesByIdPrefeitura(idPrefeitura,idAnalise,exercicio);
        }

        public void Update(ProgramaProjetoCofinanciamentoInfo cofinanciamento, Boolean commit)
        {
            new ValidadorProgramaProjetoCofinanciamento().Validar(cofinanciamento);
            cofinanciamento = GetFull(cofinanciamento);

            _repository.Update(cofinanciamento);

            var descricao = "Programa/Projeto " + cofinanciamento.ProgramaProjeto.Nome + ": atualizado o vínculo do " + GetFullDescription(cofinanciamento) + ".";
            var log = Log.CreateLog(cofinanciamento.ProgramaProjeto.IdPrefeitura, EAcao.Update, 42, descricao, cofinanciamento.IdProgramaProjeto);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ProgramaProjetoCofinanciamentoInfo cofinanciamento, Boolean commit)
        {
            // new ValidadorProgramaProjetoCofinanciamento().Validar(cofinanciamento);
            try
            {
                cofinanciamento = GetFull(cofinanciamento);
                //Gravar Programa projeto
                if (cofinanciamento.ProgramaProjeto != null)
                {
                    /*
                    if (!cofinanciamento.ProgramaProjeto.Nome.ToLower().Contains("são paulo solidário") && !cofinanciamento.ProgramaProjeto.Nome.ToLower().Contains("amigo do idoso")
                        && !cofinanciamento.ProgramaProjeto.Nome.ToLower().Contains("vivaleite") && !cofinanciamento.ProgramaProjeto.Nome.ToLower().Contains("bom prato")
                        && !cofinanciamento.ProgramaProjeto.ProgramaMunicipal.Value)
                    {
                        if (cofinanciamento.ServicosRecursosFinanceirosCRAS != null && cofinanciamento.NumeroUsuarios > cofinanciamento.ServicosRecursosFinanceirosCRAS.CRAS.NumeroAtendidos || cofinanciamento.NumeroUsuarios > cofinanciamento.ProgramaProjeto.MetaPactuada)
                        {
                            throw new Exception("O número de usuários do serviço associados a esse programa não deve ser maior que o número total de beneficiários desse programa!");
                        }
                        if (cofinanciamento.ServicosRecursosFinanceirosPublico != null && cofinanciamento.NumeroUsuarios > cofinanciamento.ServicosRecursosFinanceirosPublico.PrevisaoAnualNumeroAtendidos || cofinanciamento.NumeroUsuarios > cofinanciamento.ProgramaProjeto.MetaPactuada)
                        {
                            throw new Exception("O número de usuários do serviço atendidos nesse programa não pode ser maior que o número total de atendidos pelo serviço!");
                        }
                    }
                    else
                    {
                        if (cofinanciamento.Servico != null && cofinanciamento.NumeroUsuarios > cofinanciamento.Servico.PrevisaoMensalNumeroAtendidos)
                        {
                            throw new Exception("O número de usuários do serviço atendidos nesse programa não pode ser maior que o número total de atendidos pelo serviço!");
                        }
                    }
                    */
                    _repository.Add(cofinanciamento);

                    var descricao = "Programa/Projeto " + cofinanciamento.ProgramaProjeto.Nome + ": vinculado o " + GetFullDescription(cofinanciamento) + ".";
                    var log = Log.CreateLog(cofinanciamento.ProgramaProjeto.IdPrefeitura, EAcao.Add, 42, descricao, cofinanciamento.IdProgramaProjeto);
                    if (log != null)
                        new Log().Add(log, false);
                }

                //else
                //{

                //}
                if (commit)
                    ContextManager.Commit();
            }
            catch (Exception ex)
            {
                if (Extensions.GetExceptionMessage(ex).Contains("UNQ"))
                    throw new Exception("Este programa já foi associado a esse serviço");
                throw ex;
            }
        }

        public ProgramaProjetoCofinanciamentoInfo GetFull(ProgramaProjetoCofinanciamentoInfo cofinanciamento)
        {

            cofinanciamento.ProgramaProjeto = new ProgramaProjeto().GetById(cofinanciamento.IdProgramaProjeto);

            cofinanciamento.ProgramaProjeto.AcoesDesenvolvidasPrograma = null;

            if (cofinanciamento.ProgramaProjeto == null)
            {
                cofinanciamento.TransferenciaRenda = new TransferenciaRenda().GetById(cofinanciamento.IdProgramaProjeto);
            }

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
                
                if( cofinanciamento.ServicosRecursosFinanceirosPublico != null)
                cofinanciamento.ServicosRecursosFinanceirosPublico.LocalExecucao = new LocalExecucaoPublico().GetById(cofinanciamento.ServicosRecursosFinanceirosPublico.IdLocalExecucao);
            }
            if (cofinanciamento.IdServicosRecursosFinanceirosPrivado.HasValue)
            {
                cofinanciamento.ServicosRecursosFinanceirosPrivado = new ServicoRecursoFinanceiroPrivado().GetById(cofinanciamento.IdServicosRecursosFinanceirosPrivado.Value);
                cofinanciamento.ServicosRecursosFinanceirosPrivado.LocalExecucao = new LocalExecucaoPrivado().GetById(cofinanciamento.ServicosRecursosFinanceirosPrivado.IdLocalExecucao);
            }

            if (cofinanciamento.Servico != null)
                cofinanciamento.Servico.UsuarioTipoServico = new UsuarioTipoServico().GetById(cofinanciamento.Servico.IdUsuarioTipoServico);

            return cofinanciamento;

        }

        public ProgramaProjetoCofinanciamentoInfo GetFullById(int id)
        {
            var cofinanciamento = _repository.Single(m => m.Id == id);
            return GetFull(cofinanciamento);

        }
        //Welington P.
        public Int32 GetNumeroBeneficiario(ProgramaProjetoCofinanciamentoInfo confinaciamento)
        {
            var numeroBeneficiarios = 0;
            switch ((ETipoUnidade)confinaciamento.IdProgramaProjeto)
            {
                case ETipoUnidade.CRAS:
                    numeroBeneficiarios = confinaciamento.ServicosRecursosFinanceirosCRAS.CRAS.CapacidadeAtendimento.HasValue ? confinaciamento.ServicosRecursosFinanceirosCRAS.CRAS.CapacidadeAtendimento.Value : 0;
                    break;
                default:
                    break;
            }

            return numeroBeneficiarios;

        }

        public String GetFullDescription(ProgramaProjetoCofinanciamentoInfo cofinanciamento)
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

        public void Delete(ProgramaProjetoCofinanciamentoInfo cofinanciamento, Boolean commit, Boolean saveLog)
        {
            LogInfo log = null;
            if (saveLog)
            {
                var descricao = "Programa/Projeto " + cofinanciamento.ProgramaProjeto.Nome + ": desvinculado o " + GetFullDescription(cofinanciamento) + ".";
                log = Log.CreateLog(cofinanciamento.ProgramaProjeto.IdPrefeitura, EAcao.Remove, 42, descricao, cofinanciamento.IdProgramaProjeto);
            }
            cofinanciamento.ProgramaProjeto = null;

            _repository.Delete(cofinanciamento);

            if (log != null)
                new Log().Add(log, false);
            if (commit)
                ContextManager.Commit();
        }
    }
}
