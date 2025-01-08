using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Persistencia;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ServicoRecursoFinanceiroCRAS
    {
        #region repositorios
        private static IRepository<ServicoRecursoFinanceiroCRASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCRASInfo>>();
            }
        }

        private static IRepository<ConsultaServicosRecursosFinanceirosCRASInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaServicosRecursosFinanceirosCRASInfo>>();
            }
        }


        private static IRepository<ServicoRecursoFinanceiroFundosCRASInfo> _repositoryFundos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosCRASInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroCRASFonteRecursoInfo> _repositoryFontes
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCRASFonteRecursoInfo>>();
            }
        }
        #endregion fundoNovo.ValorEstadualAssistenciaAnoAnterior


        public IQueryable<ServicoRecursoFinanceiroCRASInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroCRASInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroCRASMediaMensal")
                .Include("ServicosRecursosFinanceiroCRASCapacidade")
                //.Include("ServicoRecursoFinanceiroCRASFonteRecurso")
                .Include("ServicosRecursosFinanceirosFundosCRASInfo")
                .Include("ServicosRecursosFinanceirosFundosCRASInfo.ServicoRecursoFinanceiroCRASFontesRecursosInfo")
                .SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ConsultaServicosRecursosFinanceirosCRASInfo> GetConsultaByCRAS(Int32 idCRAS)
        {
            return _repositoryConsulta.GetQuery().Where(c => c.IdCRAS == idCRAS);
        }

        public IQueryable<ServicoRecursoFinanceiroCRASInfo> GetByCRAS(Int32 idCRAS)
        {
            return _repository.GetObjectSet().Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroCRASMediaMensal")
                .Include("ServicosRecursosFinanceiroCRASCapacidade")
                //.Include("ServicoRecursoFinanceiroCRASFonteRecurso")
                .Include("ServicosRecursosFinanceirosFundosCRASInfo")
                .Include("ServicosRecursosFinanceirosFundosCRASInfo.ServicoRecursoFinanceiroCRASFontesRecursosInfo")
                .Where(s => s.IdCRAS == idCRAS);
        }

        /// <summary>
        /// O parametro de validação é para a questão de "Desativação"
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="commit"></param>
        /// <param name="validar"></param>
        public void Update(ServicoRecursoFinanceiroCRASInfo servico, Boolean commit)
        {
            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };
            string campoAlterado = string.Empty;
            int exercicio = servico.ServicosRecursosFinanceirosFundosCRASInfo.First().Exercicio;

            if (!servico.Desativado)
            {
                this.Validar(servico);
                #region servico sem tipo

                if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico)))
                {
                    if (_repository.GetQuery().Any(s => s.Id != servico.Id && s.IdCRAS == servico.IdCRAS && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico && !s.Desativado))
                    {
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                }
                #endregion

            }


            #region Se equipe sem volante atribui false

            if (!new CRAS().GetAll().Where(t => t.Id == servico.IdCRAS).Select(t => t.PossuiEquipeVolante).First())
            {
                servico.OfertadoPelaEquipeVolante = false;
            }
            #endregion

            #region situacoes especificas
            var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
            servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
            #endregion

            #region atividades socio assistencias
            var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
            servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
            servico.UsuarioTipoServico = usuario;
            #endregion

            #region Primeiro Update
            _repository.Update(servico);
            #endregion

            #region Obtem label para log
            var propriedadesEntity = _repository.GetModifiedProperties(servico);
            var propriedades = GetLabelForInfo(propriedadesEntity, servico);
            #endregion

            #region Obtem Cras atualizado
            var servicoOriginal = GetById(servico.Id);
            #endregion

            #region fundos
            var fundoNovo = servico.ServicosRecursosFinanceirosFundosCRASInfo.FirstOrDefault();

            var fundoExistente = servicoOriginal.ServicosRecursosFinanceirosFundosCRASInfo.Where(fundoOriginal => fundoOriginal.Exercicio == fundoNovo.Exercicio
                    && fundoOriginal.ServicoRecursoFinanceiroCRASInfoId == fundoNovo.ServicoRecursoFinanceiroCRASInfoId).FirstOrDefault();

            bool hasChangeFontesRecurso = false;

            if (fundoExistente == null)
            {
                servicoOriginal.ServicosRecursosFinanceirosFundosCRASInfo.Add(fundoNovo);
            }
            else
            {
                fundoExistente.ValorEstadualAssistencia = fundoNovo.ValorEstadualAssistencia;
                fundoExistente.ValorEstadualAssistenciaAnoAnterior = fundoNovo.ValorEstadualAssistenciaAnoAnterior;                
                fundoExistente.ValorEstadualFEDCA = fundoNovo.ValorEstadualFEDCA;
                fundoExistente.ValorEstadualFEI = fundoNovo.ValorEstadualFEI;
                fundoExistente.ValorFederalAssistencia = fundoNovo.ValorFederalAssistencia;                
                fundoExistente.ValorFederalFNDCA = fundoNovo.ValorFederalFNDCA;
                fundoExistente.ValorFederalFNI = fundoNovo.ValorFederalFNI;
                fundoExistente.ValorMunicipalAssistencia = fundoNovo.ValorMunicipalAssistencia;
                fundoExistente.ValorMunicipalFMDCA = fundoNovo.ValorMunicipalFMDCA;
                fundoExistente.ValorMunicipalFMI = fundoNovo.ValorMunicipalFMI;
                fundoExistente.ValorEstadualDemandasParlamentares = fundoNovo.ValorEstadualDemandasParlamentares;
                fundoExistente.ValorEstadualDemandasParlamentaresReprogramacao = fundoNovo.ValorEstadualDemandasParlamentaresReprogramacao;
                fundoExistente.CodigoDemandaParlamentar = fundoNovo.CodigoDemandaParlamentar;
                fundoExistente.ObjetoDemandaParlamentar = fundoNovo.ObjetoDemandaParlamentar;
                fundoExistente.ContrapartidaMunicipal = fundoNovo.ContrapartidaMunicipal;
                fundoExistente.ValorContrapartidaMunicipal = fundoNovo.ValorContrapartidaMunicipal;


                if (fundoNovo.ExisteOutraFonteFinanciamento.HasValue && fundoNovo.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var fundo in fundoNovo.ServicoRecursoFinanceiroCRASFontesRecursosInfo)
                    {
                        fundo.IdServicoRecursoFinanceiroFundosCRAS = fundoExistente.Id;
                    }
                    hasChangeFontesRecurso = Merge(fundoExistente.ServicoRecursoFinanceiroCRASFontesRecursosInfo
                                                    , fundoNovo.ServicoRecursoFinanceiroCRASFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                     fundoExistente.ServicoRecursoFinanceiroCRASFontesRecursosInfo != null
                     && fundoExistente.ServicoRecursoFinanceiroCRASFontesRecursosInfo.Count > 0;
                }
                else
                {
                    hasChangeFontesRecurso = DeleteFontesRecurso(fundoExistente.ServicoRecursoFinanceiroCRASFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                        fundoExistente.ServicoRecursoFinanceiroCRASFontesRecursosInfo != null
                        && fundoExistente.ServicoRecursoFinanceiroCRASFontesRecursosInfo.Count > 0;
                }
            }

            #endregion

            #region Funcionamento
            #region Capacidade
            if (servico.ServicosRecursosFinanceiroCRASCapacidade != null)
            {
                foreach (var capacidadeComValorNovo in servico.ServicosRecursosFinanceiroCRASCapacidade)
                {
                    var capacidadeExistente = servicoOriginal.ServicosRecursosFinanceiroCRASCapacidade.Where(x => x.Exercicio == capacidadeComValorNovo.Exercicio).FirstOrDefault();
                    if (capacidadeExistente != null)
                    {
                        capacidadeExistente.Capacidade = capacidadeComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCRASCapacidade == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCRASCapacidade = new List<ServicoRecursoFinanceiroCRASCapacidadeInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCRASCapacidade.Add(new ServicoRecursoFinanceiroCRASCapacidadeInfo
                        {
                            IdServicoRecursoFinanceiroCRAS = servicoOriginal.Id
                            ,
                            Exercicio = capacidadeComValorNovo.Exercicio
                            ,
                            Capacidade = capacidadeComValorNovo.Capacidade
                        });
                    }

                }
            }

            #endregion
            #region MediaMensal (Exercicio Atual é previsão e Media Mensal é dos Atendidos no ano)
            if (servico.ServicosRecursosFinanceiroCRASMediaMensal != null)
            {
                foreach (var MediaMensalComValorNovo in servico.ServicosRecursosFinanceiroCRASMediaMensal)
                {
                    var MediaMensalExistente = servicoOriginal.ServicosRecursosFinanceiroCRASMediaMensal.Where(x => x.Exercicio == MediaMensalComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalExistente != null)
                    {
                        MediaMensalExistente.MediaMensal = MediaMensalComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCRASMediaMensal == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCRASMediaMensal = new List<ServicoRecursoFinanceiroCRASMediaMensalInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCRASMediaMensal.Add(new ServicoRecursoFinanceiroCRASMediaMensalInfo
                        {
                            IdServicoRecursoFinanceiroCRAS = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalComValorNovo.MediaMensal
                        });
                    }

                }
            }

            #endregion
            #endregion


            #region log
            var hasChangeSituacoes = _repository.UpdateNN<SituacaoEspecificaInfo>(servicoOriginal, servico.SituacoesEspecificas, (s, lst) => lst.Any(t => t.Id == s.Id), p => p.SituacoesEspecificas);
            var hasChangeAtividades = _repository.UpdateNN<AtividadeSocioAssistencialInfo>(servicoOriginal, servico.AtividadesSocioAssistenciais, (s, lst) => lst.Any(t => t.Id == s.Id), p => p.AtividadesSocioAssistenciais);

            if (hasChangeFontesRecurso)
            {
                propriedades.Add("fontes de recursos atendidas pelo serviço");
            }
            if (hasChangeSituacoes)
            {
                propriedades.Add("situações específicas atendidas pelo serviço");
            }
            if (hasChangeAtividades)
            {
                propriedades.Add("trabalho social essencial do serviço");
            }

            var descricao = String.Empty;
            var acao = EAcao.Update;
            if (propriedades.Count > 0)
            {
                if (servico.CRAS == null)
                    servico.CRAS = new CRAS().GetById(servico.IdCRAS);

                if (servico.CRAS.UnidadePublica == null)
                    servico.CRAS.UnidadePublica = new UnidadePublica().GetById(servico.CRAS.IdUnidade);

                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CRAS.Id + " - " + servico.CRAS.Nome  + ".";
                    acao = EAcao.Deactivate;
                }
                else
                {
                    descricao = "Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + " - " + servico.CRAS.Nome + ".";
                    descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                }



                var log = Log.CreateLog(servico.CRAS.UnidadePublica.IdPrefeitura, acao, 23, descricao, servico.IdCRAS);
                if (log != null)
                    new Log().Add(log, false);
            }
            else
            {
                if (servico.CRAS.UnidadePublica == null)
                    servico.CRAS.UnidadePublica = new UnidadePublica().GetById(servico.CRAS.IdUnidade);

                //descricao = "Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + " - " + servico.CRAS.Nome + "-" + "o(s) campo(s) alterado(s)" + campoAlterado + " exercicio: " + exercicio + ".";
                
                descricao = "Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + " - " + servico.CRAS.Nome + ".";

                var log = Log.CreateLog(servico.CRAS.UnidadePublica.IdPrefeitura, acao, 23, descricao, servico.IdCRAS);
                if (log != null)
                    new Log().Add(log, false);
            }
            #endregion

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void Add(ServicoRecursoFinanceiroCRASInfo servico, Boolean commit)
        {
            try
            {
                Add(servico, commit, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Add(ServicoRecursoFinanceiroCRASInfo servico, Boolean commit, Boolean validar)
        {
            try
            {
                var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
                var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };

                if (validar)
                {
                    Validar(servico);

                    if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico)))
                    {
                        if (_repository.GetQuery().Any(s => s.IdCRAS == servico.IdCRAS && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico && !s.Desativado))
                        {
                            throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                        }
                    }

                }

                if (!new CRAS().GetAll().Where(t => t.Id == servico.IdCRAS).Select(t => t.PossuiEquipeVolante).First())
                {
                    //caso o cras não possui equipe volante, o serviço não possui equipe volante
                    servico.OfertadoPelaEquipeVolante = false;
                }

                #region situacoes especificas
                var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
                servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
                #endregion

                #region atividades socio assistenciais
                var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
                servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
                #endregion

                servico.UsuarioTipoServico = null;
                if (servico.CRAS == null)
                {
                    servico.CRAS = new CRAS().GetById(servico.IdCRAS);
                }

                if (servico.CRAS.UnidadePublica == null)
                {
                    servico.CRAS.UnidadePublica = new UnidadePublica().GetById(servico.CRAS.IdUnidade);
                }

                var descricao = "Incluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CRAS.Id + " - " + servico.CRAS.Nome + ".";
                var log = Log.CreateLog(servico.CRAS.UnidadePublica.IdPrefeitura, EAcao.Add, 23, descricao, servico.IdCRAS);
                servico.CRAS = null;

                if (servico.ServicosRecursosFinanceirosFundosCRASInfo != null)
                {
                    foreach (var fundo in servico.ServicosRecursosFinanceirosFundosCRASInfo)
                    {
                        fundo.Desbloqueado = true;
                    }
                }

                _repository.Add(servico);


                if (log != null)
                {
                    new Log().Add(log, false);
                }

                if (commit)
                {
                    ContextManager.Commit();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(ServicoRecursoFinanceiroCRASInfo servico, Boolean commit)
        {
            Delete(servico, commit, true);
        }
        public void Delete(ServicoRecursoFinanceiroCRASInfo servico, Boolean commit, Boolean validarVinculos)
        {
            if (validarVinculos)
            {
                if (new ProgramaProjetoCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosCRAS == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
                if (new TransferenciaRendaCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosCRAS == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
                if (new PrefeituraBeneficioEventualServico().GetAll().Any(t => t.IdServicosRecursosFinanceirosCRAS == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            }

            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            if (servico.CRAS == null)
                servico.CRAS = new CRAS().GetById(servico.IdCRAS);

            if (servico.CRAS.UnidadePublica == null)
                servico.CRAS.UnidadePublica = new UnidadePublica().GetById(servico.CRAS.IdUnidade);



            var lstDeleted = new List<ServicoRecursoFinanceiroCRASFonteRecursoInfo>();
            var ppp = new ServicoRecursoFinanceiroCRASFonteRecurso();
            var lst = ppp.GetByRecursoFinanceiroCRAS(servico.Id);
            foreach (var p in lst)
                if (servico.ServicosRecursosFinanceirosFundosCRASInfo.Any(t => t.Id == p.Id))
                {
                    lstDeleted.Add(p);
                }

            foreach (var p in lstDeleted)
                ppp.Delete(p, false);

            var descricao = "Excluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CRAS.Id + " - " + servico.CRAS.Nome + ".";
            var log = Log.CreateLog(servico.CRAS.UnidadePublica.IdPrefeitura, EAcao.Remove, 23, descricao, servico.IdCRAS);
            servico.CRAS = null;

            _repository.Delete(servico);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Validar(ServicoRecursoFinanceiroCRASInfo servico)
        {
            var lstMsg = new List<string>();

            new ValidadorServicoRecursoFinanceiro().ValidarCRAS(servico);

            if (servico.TotalFuncionarios > new ServicoRecursoFinanceiroCRASRecursosHumanos().GetTotalRHByIdServicoRecursoFinanceiro(servico.Id))
            {
                throw new Exception("O Total de profissionais que atuam neste serviço não pode ser maior que o RH");
            }
            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
        }
        public void ValidarProgramaServico(ServicoRecursoFinanceiroCRASInfo servico)
        {
            if (servico.PossuiProgramaBeneficio != null && servico.PossuiProgramaBeneficio.Value)
            {
                var cofinanciamento = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(servico.Id, servico.IdCRAS);
                if (cofinanciamento.Count() == 0)
                {
                    throw new Exception("Deve ser associado ao serviço pelo menos um programa ao serviço");
                }
            }
        }
        public ServicoRecursoFinanceiroCRASInfo CriarServicoPAIF()
        {
            var s = new ServicoRecursoFinanceiroCRASInfo();
            s.IdAbrangenciaServico = 3;//MUNICIPAL
            s.IdCaracteristicasTerritorio = 8;//NENHUMA
            s.IdUsuarioTipoServico = 4;//FAMÍLIAS
            s.IdRegiaoMoradia = 3;//AMBOS
            s.IdSexo = 3;//AMBOS  
            s.IdHorasSemana = 1;
            s.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
            s.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
            return s;
        }

        public ServicoRecursoFinanceiroCRASInfo GetServicoPAIFByCRAS(Int32 idCRAS)
        {
            return _repository.GetQuery().SingleOrDefault(t => t.IdCRAS == idCRAS && t.IdUsuarioTipoServico == 4);
        }


        #region helper
        public List<String> GetLabelForInfo(List<String> propriedades, ServicoRecursoFinanceiroCRASInfo servico)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "IdUsuarioTipoServico": labels.Add("usuário do tipo de serviço"); break;
                    case "PrevisaoMensalNumeroAtendidos": labels.Add("previsão mensal do número de famílias atendidas"); break;
                    case "PrevisaoAnualNumeroAtendidos": labels.Add("previsão anual do número de famílias atendidas"); break;
                    case "ValorMunicipalAssistencia": labels.Add("recursos financeiros do FMAS"); break;
                    case "ValorMunicipalFMDCA": labels.Add("recursos financeiros do FMDCA"); break;
                    case "ValorEstadualAssistencia": labels.Add("recursos financeiros do FEAS"); break;
                    case "ValorEstadualFEDCA": labels.Add("recursos financeiros do FEDCA"); break;
                    case "ValorFederalAssistencia": labels.Add("recursos financeiros do FNAS"); break;
                    case "ValorFederalFNDCA": labels.Add("recursos financeiros do FNDCA"); break;
                    case "OfertadoPelaEquipeVolante": labels.Add("serviço é ofertado pela Equipe Volante"); break;
                    case "IdAbrangenciaServico": labels.Add("abrangência do serviço"); break;
                    case "DescricaoServicoNaoTipificado": if (servico.UsuarioTipoServico.IdTipoServico == 138 || servico.UsuarioTipoServico.IdTipoServico == 145 || servico.UsuarioTipoServico.IdTipoServico == 153) labels.Add("especificação do tipo de serviço"); break;
                    case "ObjetivoServicoNaoTipificado": if (servico.UsuarioTipoServico.IdTipoServico == 138 || servico.UsuarioTipoServico.IdTipoServico == 145 || servico.UsuarioTipoServico.IdTipoServico == 153) labels.Add("objetivo do tipo de serviço"); break;
                    case "TotalFuncionarios": labels.Add("total de profissionais que atuam no serviço"); break;
                    case "IdRegiaoMoradia":
                    case "IdCaracteristicasTerritorio":
                    case "IdSexo":
                        labels.Add("caracterização dos usuários"); break;

                    case "ValorMunicipalFMI": labels.Add("recursos financeiros do Fundo Municipal do Idoso"); break;
                    case "ValorEstadualFEI": labels.Add("recursos financeiros do Fundo Estadual do Idoso"); break;
                    case "ValorFederalFNI": labels.Add("recursos financeiros do Fundo Nacional do Idoso"); break;
                    case "Desativado": labels.Add("Desativado"); break;
                }
            }
            return labels.Distinct().ToList();
        }
        #region fontes de recurso

        private bool Merge(List<ServicoRecursoFinanceiroCRASFonteRecursoInfo> recursosExistentes, List<ServicoRecursoFinanceiroCRASFonteRecursoInfo> recursosNovos)
        {
            bool hasChanged = false;

            foreach (var re in recursosExistentes.ToList())
            {
                var recursoExistente = recursosNovos.SingleOrDefault(rn => rn.Id == re.Id);
                if (recursoExistente == null)
                {
                    _repositoryFontes.GetObjectSet().Context.DeleteObject(re);
                    hasChanged = true;
                }

            }

            //Adicionar os ids vazios 
            foreach (var rn in recursosNovos.Where(r => r.Id == 0).ToList())
            {
                recursosExistentes.Add(rn);
                hasChanged = true;
            }

            return hasChanged;
        }

        private bool DeleteFontesRecurso(List<ServicoRecursoFinanceiroCRASFonteRecursoInfo> recursosExistentes)
        {
            bool hasChanged = false;
            foreach (var recurso in recursosExistentes.ToList())
            {
                _repositoryFontes.GetObjectSet().Context.DeleteObject(recurso);
                hasChanged = true;
            }
            return hasChanged;
        }

        #endregion
        #endregion





    }
}
