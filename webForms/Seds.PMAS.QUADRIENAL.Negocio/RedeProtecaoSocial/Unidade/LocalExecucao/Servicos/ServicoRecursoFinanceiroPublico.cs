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
using Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial;
using System.Transactions;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ServicoRecursoFinanceiroPublico
    {
        #region repositories
        private static IRepository<ServicoRecursoFinanceiroPublicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPublicoInfo>>();
            }
        }

        private static IRepository<ConsultaServicosRecursosFinanceirosPublicoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaServicosRecursosFinanceirosPublicoInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroFundosPublicoInfo> _repositoryFundos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosPublicoInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> _repositoryFontes
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>>();
            }
        }

        #endregion

        public IQueryable<ServicoRecursoFinanceiroPublicoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroPublicoInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroPublicoCapacidade")
                .Include("ServicosRecursosFinanceiroPublicoCapacidadeLA")
                .Include("ServicosRecursosFinanceiroPublicoCapacidadePSC")
                .Include("ServicosRecursosFinanceiroPublicoMediaMensal")
                .Include("ServicosRecursosFinanceiroPublicoMediaMensalLA")
                .Include("ServicosRecursosFinanceiroPublicoMediaMensalPSC")
                //.Include("ServicosRecursosFinanceiroPublicoLA")
                //.Include("ServicosRecursosFinanceiroPublicoPSC")
                //.Include("ServicoRecursoFinanceiroPublicoFonteRecurso")
                .Include("ServicosRecursosFinanceirosFundosPublicoInfo")
                .Include("ServicosRecursosFinanceirosFundosPublicoInfo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo")

                .SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ConsultaServicosRecursosFinanceirosPublicoInfo> GetConsultaByLocalExecucao(Int32 idLocalExecucao)
        {
            return _repositoryConsulta.GetQuery().Where(c => c.IdLocalExecucao == idLocalExecucao);
        }

        public IQueryable<ServicoRecursoFinanceiroPublicoInfo> GetByLocalExecucao(Int32 idLocal)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroPublicoCapacidade")
                .Include("ServicosRecursosFinanceiroPublicoCapacidadeLA")
                .Include("ServicosRecursosFinanceiroPublicoCapacidadePSC")
                .Include("ServicosRecursosFinanceiroPublicoMediaMensal")
                .Include("ServicosRecursosFinanceiroPublicoMediaMensalLA")
                .Include("ServicosRecursosFinanceiroPublicoMediaMensalPSC")

                .Include("ServicosRecursosFinanceirosFundosPublicoInfo")
                .Include("ServicosRecursosFinanceirosFundosPublicoInfo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo")
                .Where(s => s.IdLocalExecucao == idLocal);
        }

        public void Update(ServicoRecursoFinanceiroPublicoInfo servico, Boolean commit)
        {
            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159, 160 };

            if (!servico.Desativado)
            {
                Validar(servico);

                if (!(servicosNaoTipificados.Contains(usuario.TipoServico.Id)))
                {
                    if (_repository.GetQuery().Any(s => s.Id != servico.Id && s.IdLocalExecucao == servico.IdLocalExecucao && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico && !s.Desativado))
                    {
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                    
                }
            }

            var lstDeleted = new List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>();
            var fonteRecurso = new ServicoRecursoFinanceiroPublicoFonteRecurso();
            var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
            servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();

            var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
            servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();

            servico.UsuarioTipoServico = usuario;
            _repository.Update(servico);




            var propriedadesEntity = _repository.GetModifiedProperties(servico);
            var propriedades = GetLabelForInfo(propriedadesEntity, servico);

            var servicoOriginal = GetById(servico.Id);

            #region fundos
            var fundoNovo = servico.ServicosRecursosFinanceirosFundosPublicoInfo.FirstOrDefault();
            var fundoExistente = servicoOriginal.ServicosRecursosFinanceirosFundosPublicoInfo
                                                 .Where(fundoOriginal => fundoOriginal.Exercicio == fundoNovo.Exercicio
                                                  && fundoOriginal.ServicoRecursoFinanceiroPublicoInfoId == fundoNovo.ServicoRecursoFinanceiroPublicoInfoId).FirstOrDefault();

            bool hasChangeFontesRecurso = false;

            if (fundoExistente == null)
            {
                servicoOriginal.ServicosRecursosFinanceirosFundosPublicoInfo.Add(fundoNovo);
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
                fundoExistente.ObjetoDemandaParlamentar = fundoNovo.ObjetoDemandaParlamentar;
                fundoExistente.CodigoDemandaParlamentar = fundoNovo.CodigoDemandaParlamentar;
                fundoExistente.ContrapartidaMunicipal = fundoNovo.ContrapartidaMunicipal;
                fundoExistente.ValorContrapartidaMunicipal = fundoNovo.ValorContrapartidaMunicipal;

                if (fundoNovo.ExisteOutraFonteFinanciamento.HasValue && fundoNovo.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var fundo in fundoNovo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo)
                    {
                        fundo.IdServicoRecursoFinanceiroFundosPublico = fundoExistente.Id;
                    }
                    hasChangeFontesRecurso = Merge(fundoExistente.ServicoRecursoFinanceiroPublicoFontesRecursosInfo
                                                    , fundoNovo.ServicoRecursoFinanceiroPublicoFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                     fundoExistente.ServicoRecursoFinanceiroPublicoFontesRecursosInfo != null
                     && fundoExistente.ServicoRecursoFinanceiroPublicoFontesRecursosInfo.Count > 0;
                }
                else
                {
                    hasChangeFontesRecurso = DeleteFontesRecurso(fundoExistente.ServicoRecursoFinanceiroPublicoFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                        fundoExistente.ServicoRecursoFinanceiroPublicoFontesRecursosInfo != null
                        && fundoExistente.ServicoRecursoFinanceiroPublicoFontesRecursosInfo.Count > 0;
                }
            }

            #endregion


            #region Funcionamento
            #region Capacidade
            if (servico.ServicosRecursosFinanceiroPublicoCapacidade != null)
            {
                foreach (var capacidadeComValorNovo in servico.ServicosRecursosFinanceiroPublicoCapacidade)
                {
                    var capacidadeExistente = servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == capacidadeComValorNovo.Exercicio).FirstOrDefault();
                    if (capacidadeExistente != null)
                    {
                        capacidadeExistente.Capacidade = capacidadeComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidade == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidade = new List<ServicoRecursoFinanceiroPublicoCapacidadeInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidade.Add(new ServicoRecursoFinanceiroPublicoCapacidadeInfo
                        {
                            IdServicoRecursoFinanceiroPublico = servicoOriginal.Id
                            ,
                            Exercicio = capacidadeComValorNovo.Exercicio
                            ,
                            Capacidade = capacidadeComValorNovo.Capacidade
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPublicoCapacidadeLA != null)
            {
                foreach (var CapacidadeLAComValorNovo in servico.ServicosRecursosFinanceiroPublicoCapacidadeLA)
                {
                    var CapacidadeLAExistente = servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == CapacidadeLAComValorNovo.Exercicio).FirstOrDefault();
                    if (CapacidadeLAExistente != null)
                    {
                        CapacidadeLAExistente.Capacidade = CapacidadeLAComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadeLA == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadeLA = new List<ServicoRecursoFinanceiroPublicoCapacidadeLAInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadeLA.Add(new ServicoRecursoFinanceiroPublicoCapacidadeLAInfo
                        {
                            IdServicoRecursoFinanceiroPublico = servicoOriginal.Id
                            ,
                            Exercicio = CapacidadeLAComValorNovo.Exercicio
                            ,
                            Capacidade = CapacidadeLAComValorNovo.Capacidade
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPublicoCapacidadePSC != null)
            {
                foreach (var CapacidadePSCComValorNovo in servico.ServicosRecursosFinanceiroPublicoCapacidadePSC)
                {
                    var CapacidadePSCExistente = servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == CapacidadePSCComValorNovo.Exercicio).FirstOrDefault();
                    if (CapacidadePSCExistente != null)
                    {
                        CapacidadePSCExistente.Capacidade = CapacidadePSCComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadePSC == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadePSC = new List<ServicoRecursoFinanceiroPublicoCapacidadePSCInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPublicoCapacidadePSC.Add(new ServicoRecursoFinanceiroPublicoCapacidadePSCInfo
                        {
                            IdServicoRecursoFinanceiroPublico = servicoOriginal.Id
                            ,
                            Exercicio = CapacidadePSCComValorNovo.Exercicio
                            ,
                            Capacidade = CapacidadePSCComValorNovo.Capacidade
                        });
                    }

                }
            }
            #endregion
            #region MediaMensal (Exercicio Atual é previsão e Media Mensal é dos Atendidos no ano)
            if (servico.ServicosRecursosFinanceiroPublicoMediaMensal != null)
            {
                foreach (var MediaMensalComValorNovo in servico.ServicosRecursosFinanceiroPublicoMediaMensal)
                {
                    var MediaMensalExistente = servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == MediaMensalComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalExistente != null)
                    {
                        MediaMensalExistente.MediaMensal = MediaMensalComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensal == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensal = new List<ServicoRecursoFinanceiroPublicoMediaMensalInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensal.Add(new ServicoRecursoFinanceiroPublicoMediaMensalInfo
                        {
                            IdServicoRecursoFinanceiroPublico = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalComValorNovo.MediaMensal
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPublicoMediaMensalLA != null)
            {
                foreach (var MediaMensalLAComValorNovo in servico.ServicosRecursosFinanceiroPublicoMediaMensalLA)
                {
                    var MediaMensalLAExistente = servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == MediaMensalLAComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalLAExistente != null)
                    {
                        MediaMensalLAExistente.MediaMensal = MediaMensalLAComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalLA == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalLA = new List<ServicoRecursoFinanceiroPublicoMediaMensalLAInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalLA.Add(new ServicoRecursoFinanceiroPublicoMediaMensalLAInfo
                        {
                            IdServicoRecursoFinanceiroPublico = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalLAComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalLAComValorNovo.MediaMensal
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC != null)
            {
                foreach (var MediaMensalPSCComValorNovo in servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC)
                {
                    var MediaMensalPSCExistente = servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == MediaMensalPSCComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalPSCExistente != null)
                    {
                        MediaMensalPSCExistente.MediaMensal = MediaMensalPSCComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalPSC == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalPSC = new List<ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Add(new ServicoRecursoFinanceiroPublicoMediaMensalPSCInfo
                        {
                            IdServicoRecursoFinanceiroPublico = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalPSCComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalPSCComValorNovo.MediaMensal
                        });
                    }

                }
            }
            #endregion
            #endregion


            _repository.Update(servicoOriginal);

            var objOriginal = GetById(servico.Id);
            var hasChangeSituacoes = _repository.UpdateNN<SituacaoEspecificaInfo>(objOriginal, servico.SituacoesEspecificas, (s, lst) => lst.Any(t => t.Id == s.Id), p => p.SituacoesEspecificas);
            var hasChangeAtividades = _repository.UpdateNN<AtividadeSocioAssistencialInfo>(objOriginal, servico.AtividadesSocioAssistenciais, (s, lst) => lst.Any(t => t.Id == s.Id), p => p.AtividadesSocioAssistenciais);

            if (hasChangeFontesRecurso)
                propriedades.Add("fontes de recursos atendidas pelo serviço");
            if (hasChangeSituacoes)
                propriedades.Add("situações específicas atendidas pelo serviço");
            if (hasChangeAtividades)
                propriedades.Add("trabalho social essencial do serviço");

            if (propriedades.Count > 0)
            {
                string descricao = string.Empty;
                var acao = EAcao.Update;
                if (servico.LocalExecucao == null)
                    servico.LocalExecucao = new LocalExecucaoPublico().GetById(servico.IdLocalExecucao);
                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade da Rede Direta " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";
                    acao = EAcao.Deactivate;
                }
                else
                {
                    descricao = "Serviço e Recurso Financeiro de Proteção Social "
                        + usuario.TipoServico.TipoProtecaoSocial.Nome
                        + " - " + usuario.TipoServico.Nome
                        + " - " + usuario.Nome + " do Local de Execução "
                        + servico.LocalExecucao.Nome + " (Unidade Pública da Rede Direta" + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";
                    descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                }

                //        var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, EAcao.Update, 21, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);
                var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, acao, 19, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroPublicoInfo servico, Boolean commit)
        {
            try
            {
                Validar(servico);
                var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);

                #region SRF [Servicos Sem tipo]
                var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };
                if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico))) //caso for serviço não tipificado, poderá inserir sem restrição
                {
                    //não poderá inserir mais de uma combinação de tipo de serviço + usuário para o mesmo local de execução
                    if (_repository.GetQuery()
                        .Any(s => s.IdLocalExecucao == servico.IdLocalExecucao
                               && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico
                               && !s.Desativado))
                    {
                        //var texto = string.Format("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.", "{0}{1}");
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                }
                #endregion

                #region SRF [Situacoes Especificas]
                var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
                servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
                #endregion

                #region SRF [Atividades Assistenciais]
                var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
                servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
                #endregion

                servico.UsuarioTipoServico = null;

                #region SRF [Local Execucao]
                if (servico.LocalExecucao == null)
                {
                    servico.LocalExecucao = new LocalExecucaoPublico().GetById(servico.IdLocalExecucao);
                }
                #endregion

                #region SRF [Log Inclusao]
                var descricao = "Incluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade Pública " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";
                var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, EAcao.Add, 19, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);
                servico.LocalExecucao = null;
                #endregion

                #region SRF [Fundos Publico]
                if (servico.ServicosRecursosFinanceirosFundosPublicoInfo != null)
                {
                    foreach (var fundo in servico.ServicosRecursosFinanceirosFundosPublicoInfo)
                    {
                        fundo.Desbloqueado = true;
                    }
                }
                #endregion

                _repository.Add(servico);

                #region SRF [Log Historico]
                if (log != null)
                {
                    new Log().Add(log, false);
                }
                #endregion

                if (commit)
                {
                    ContextManager.Commit();
                }

                ContextManager.CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// FAZER DELECAO EM CASCATA
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="commit"></param>
        public void Delete(ServicoRecursoFinanceiroPublicoInfo servico, Boolean commit)
        {
            if (new ProgramaProjetoCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosPublico == servico.Id))
                throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            if (new TransferenciaRendaCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosPublico == servico.Id))
                throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            if (new PrefeituraBeneficioEventualServico().GetAll().Any(t => t.IdServicosRecursosFinanceirosPublico == servico.Id))
                throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");

            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            if (servico.LocalExecucao == null)
                servico.LocalExecucao = new LocalExecucaoPublico().GetById(servico.IdLocalExecucao);

            var lstDeleted = new List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo>();
            var fontesRecursosBusiness = new ServicoRecursoFinanceiroPublicoFonteRecurso();

            var recursosFinanceirosExistente = fontesRecursosBusiness.GetByRecursoFinanceiroPublico(servico.Id);

            foreach (var recursoFinanceiroExistente in recursosFinanceirosExistente)
            {
                foreach (var fundoPublico in servico.ServicosRecursosFinanceirosFundosPublicoInfo)
                {
                    if (fundoPublico.ServicoRecursoFinanceiroPublicoFontesRecursosInfo.Any(t => t.Id == fundoPublico.Id))
                    {
                        lstDeleted.Add(recursoFinanceiroExistente);
                    }
                }
            }
            this.DeleteFontesRecurso(lstDeleted);

            var descricao = "Excluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade Pública " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";


            var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, EAcao.Remove, 19, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);
            servico.LocalExecucao = null;

            _repository.Delete(servico);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Validar(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            try
            {
                new ValidadorServicoRecursoFinanceiro().ValidarServicoPublico(servico);
            }
            catch
            {
                throw;
            }

            if (servico.TotalFuncionarios > new ServicoRecursoFinanceiroPublicoRecursosHumanos().GetTotalRHByIdServicoRecursoFinanceiro(servico.Id))
            {
                throw new Exception("O Total de profissionais que atuam neste serviço não pode ser maior que o RH deste serviço");
            }
        }

        public void ValidarProgramaServico(ServicoRecursoFinanceiroPublicoInfo servico)
        {
            if (servico.PossuiProgramaBeneficio == true)
            {
                var cofinanciamento = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(servico.Id, servico.IdLocalExecucao);
                if (cofinanciamento.Count() == 0)
                {
                    throw new Exception("Deve ser associado ao serviço pelo menos um programa ao serviço");
                }
            }
        }

        public List<String> GetLabelForInfo(List<String> propriedades, ServicoRecursoFinanceiroPublicoInfo servico)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "IdUsuarioTipoServico": labels.Add("usuário do tipo de serviço"); break;
                    case "PrevisaoMensalNumeroAtendidos": labels.Add("previsão mensal do número de pessoas atendidas"); break;
                    case "PrevisaoAnualNumeroAtendidos": labels.Add("previsão anual do número de pessoas atendidas"); break;
                    case "ValorMunicipalAssistencia": labels.Add("recursos financeiros do FMAS"); break;
                    case "ValorMunicipalFMDCA": labels.Add("recursos financeiros do FMDCA"); break;
                    case "ValorEstadualAssistencia": labels.Add("recursos financeiros do FEAS"); break;
                    case "ValorEstadualFEDCA": labels.Add("recursos financeiros do FEDCA"); break;
                    case "ValorFederalAssistencia": labels.Add("recursos financeiros do FNAS"); break;
                    case "ValorFederalFNDCA": labels.Add("recursos financeiros do FNDCA"); break;
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

        private bool Merge(List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> recursosExistentes, List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> recursosNovos)
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

        private bool DeleteFontesRecurso(List<ServicoRecursoFinanceiroPublicoFonteRecursoInfo> recursosExistentes)
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
    }
}
