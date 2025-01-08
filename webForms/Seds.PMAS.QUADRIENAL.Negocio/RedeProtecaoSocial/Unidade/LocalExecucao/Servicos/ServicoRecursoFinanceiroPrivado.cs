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
    public class ServicoRecursoFinanceiroPrivado
    {
        private ValidadorServicoRecursoFinanceiro ValidadorServicoRecursoFinanceiro { get; set; }
        public ServicoRecursoFinanceiroPrivado()
        {
            this.ValidadorServicoRecursoFinanceiro = new ValidadorServicoRecursoFinanceiro();
        }

        #region Repositorios
        private static IRepository<MotivoEstadualizadoInfo> _repositoryMotivo
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MotivoEstadualizadoInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroPrivadoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPrivadoInfo>>();
            }
        }

        private static IRepository<ConsultaServicosRecursosFinanceirosPrivadoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaServicosRecursosFinanceirosPrivadoInfo>>();
            }
        }
        private static IRepository<ServicoRecursoFinanceiroFundosPrivadoInfo> _repositoryFundos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosPrivadoInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> _repositoryFontes
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>>();
            }
        }

        #endregion

        #region Crud
        #region Consultas
        public IQueryable<ServicoRecursoFinanceiroPrivadoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroPrivadoInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                //.Include("ServicosRecursosFinanceiroPrivadoLA")
                //.Include("ServicosRecursosFinanceiroPrivadoPSC")
                .Include("ServicosRecursosFinanceiroPrivadoCapacidade")
                .Include("ServicosRecursosFinanceiroPrivadoCapacidadeLA")
                .Include("ServicosRecursosFinanceiroPrivadoCapacidadePSC")
                .Include("ServicosRecursosFinanceiroPrivadoMediaMensal")
                .Include("ServicosRecursosFinanceiroPrivadoMediaMensalLA")
                .Include("ServicosRecursosFinanceiroPrivadoMediaMensalPSC")
                .Include("ServicosRecursosFinanceirosFundosPrivadoInfo")
                .Include("ServicosRecursosFinanceirosFundosPrivadoInfo.MotivoEstadualizadoInfo")
                .Include("ServicosRecursosFinanceirosFundosPrivadoInfo.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo")
                .SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ConsultaServicosRecursosFinanceirosPrivadoInfo> GetConsultaByLocalExecucao(Int32 idLocalExecucao)
        {
            return _repositoryConsulta.GetQuery().Where(c => c.IdLocalExecucao == idLocalExecucao);
        }

        public IQueryable<ServicoRecursoFinanceiroPrivadoInfo> GetByLocalExecucao(Int32 idLocal)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                  .Include("ServicosRecursosFinanceiroPrivadoCapacidade")
                .Include("ServicosRecursosFinanceiroPrivadoCapacidadeLA")
                .Include("ServicosRecursosFinanceiroPrivadoCapacidadePSC")
                .Include("ServicosRecursosFinanceiroPrivadoMediaMensal")
                .Include("ServicosRecursosFinanceiroPrivadoMediaMensalLA")
                .Include("ServicosRecursosFinanceiroPrivadoMediaMensalPSC")
                //.Include("ServicoRecursoFinanceiroPrivadoFonteRecurso")
                .Include("ServicosRecursosFinanceirosFundosPrivadoInfo")
                .Include("ServicosRecursosFinanceirosFundosPrivadoInfo.MotivoEstadualizadoInfo")
                .Include("ServicosRecursosFinanceirosFundosPrivadoInfo.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo")
                .Where(s => s.IdLocalExecucao == idLocal);
        }

        public MotivoEstadualizadoInfo GetMotivoEstadualizadoByID(int idMotivo)
        {
            return _repositoryMotivo.GetQuery().Where(c => c.Id == idMotivo).SingleOrDefault();
        }
        #endregion

        public void Update(ServicoRecursoFinanceiroPrivadoInfo servico, Boolean commit)
        {
            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };


            if (!servico.Desativado)
            {
                ValidarServicoPrivado(servico);


                #region servico sem tipo
                bool ehServicoNaoTipificado = servicosNaoTipificados.Contains(usuario.IdTipoServico);
                if (!ehServicoNaoTipificado) //inserir sem restrição
                {
                    bool existeACombinacaoServicoUsuario = VerificarSeExisteACombinacaoServicoUsuario(servico);
                    if (existeACombinacaoServicoUsuario)
                    {
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                }
                #endregion
            }
        

            #region situacoes especificas
            var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
            servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
            #endregion

            #region atividades socio assistenciais
            var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
            servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
            #endregion

            _repository.Update(servico);

            var propriedadesEntity = _repository.GetModifiedProperties(servico);
            var propriedades = GetLabelForInfo(propriedadesEntity, servico);

            var servicoOriginal = GetById(servico.Id);


            #region fundos
            var fundoNovo = servico.ServicosRecursosFinanceirosFundosPrivadoInfo.First();
            var fundoExistente = servicoOriginal.ServicosRecursosFinanceirosFundosPrivadoInfo
                .Where(fundoOriginal => fundoOriginal.Exercicio == fundoNovo.Exercicio
                    && fundoOriginal.ServicoRecursoFinanceiroPrivadoInfoId == fundoNovo.ServicoRecursoFinanceiroPrivadoInfoId).FirstOrDefault();

            bool hasChangeFontesRecurso = false;

            if (fundoExistente == null)
            {
                servicoOriginal.ServicosRecursosFinanceirosFundosPrivadoInfo.Add(fundoNovo);
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
                fundoExistente.ValorRecursoExclusivoServico = fundoNovo.ValorRecursoExclusivoServico;
                fundoExistente.ValorEstadualDemandasParlamentares = fundoNovo.ValorEstadualDemandasParlamentares;
                fundoExistente.ValorEstadualDemandasParlamentaresReprogramacao = fundoNovo.ValorEstadualDemandasParlamentaresReprogramacao;
                fundoExistente.CodigoDemandaParlamentar = fundoNovo.CodigoDemandaParlamentar;
                fundoExistente.ObjetoDemandaParlamentar = fundoNovo.ObjetoDemandaParlamentar;
                fundoExistente.ContrapartidaMunicipal = fundoNovo.ContrapartidaMunicipal;
                fundoExistente.ValorContrapartidaMunicipal = fundoNovo.ValorContrapartidaMunicipal;

                if (fundoNovo.ExisteOutraFonteFinanciamento.HasValue && fundoNovo.ExisteOutraFonteFinanciamento.Value)
                {
                    foreach (var fundo in fundoNovo.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo)
                    {
                        fundo.IdServicoRecursoFinanceiroFundosPrivado = fundoExistente.Id;
                    }
                    hasChangeFontesRecurso = Merge(fundoExistente.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo
                                                    , fundoNovo.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                     fundoExistente.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo != null
                     && fundoExistente.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo.Count > 0;
                }
                else
                {
                    hasChangeFontesRecurso = DeleteFontesRecurso(fundoExistente.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                        fundoExistente.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo != null
                        && fundoExistente.ServicoRecursoFinanceiroPrivadoFontesRecursosInfo.Count > 0;
                }



                #region convenio
                if (fundoNovo.ConvenioEstadualizado.HasValue && fundoNovo.ConvenioEstadualizado.Value)
                {
                    fundoExistente.ConvenioEstadualizado = fundoNovo.ConvenioEstadualizado;
                    #region motivo
                    if (fundoExistente.MotivoEstadualizadoInfo != null)
                    {
                        fundoExistente.MotivoEstadualizadoInfoID = fundoNovo.MotivoEstadualizadoInfo.Id;
                    }
                    //else
                    //{
                    //    fundoExistente.MotivoEstadualizadoInfoID = fundoNovo.MotivoEstadualizadoInfo.Id;
                    //}

                    #endregion
                    fundoExistente.ValorEstadualizado = fundoNovo.ValorEstadualizado;
                }
                else
                {
                    fundoExistente.MotivoEstadualizadoInfo = null;
                    fundoExistente.ConvenioEstadualizado = fundoNovo.ConvenioEstadualizado;
                    fundoExistente.ValorEstadualizado = 0M;
                }
                #endregion
            }

            #endregion

            #region Funcionamento

            #region Capacidade
            if (servico.ServicosRecursosFinanceiroPrivadoCapacidade != null)
            {
                foreach (var capacidadeComValorNovo in servico.ServicosRecursosFinanceiroPrivadoCapacidade)
                {
                    var capacidadeExistente = servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidade.Where(x => x.Exercicio == capacidadeComValorNovo.Exercicio).FirstOrDefault();
                    if (capacidadeExistente != null)
                    {
                        capacidadeExistente.Capacidade = capacidadeComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidade == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidade = new List<ServicoRecursoFinanceiroPrivadoCapacidadeInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidade.Add(new ServicoRecursoFinanceiroPrivadoCapacidadeInfo
                        {
                            IdServicoRecursoFinanceiroPrivado = servicoOriginal.Id
                            ,
                            Exercicio = capacidadeComValorNovo.Exercicio
                            ,
                            Capacidade = capacidadeComValorNovo.Capacidade
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA != null)
            {
                foreach (var CapacidadeLAComValorNovo in servico.ServicosRecursosFinanceiroPrivadoCapacidadeLA)
                {
                    var CapacidadeLAExistente = servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Where(x => x.Exercicio == CapacidadeLAComValorNovo.Exercicio).FirstOrDefault();
                    if (CapacidadeLAExistente != null)
                    {
                        CapacidadeLAExistente.Capacidade = CapacidadeLAComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadeLA == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadeLA = new List<ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadeLA.Add(new ServicoRecursoFinanceiroPrivadoCapacidadeLAInfo
                        {
                            IdServicoRecursoFinanceiroPrivado = servicoOriginal.Id
                            ,
                            Exercicio = CapacidadeLAComValorNovo.Exercicio
                            ,
                            Capacidade = CapacidadeLAComValorNovo.Capacidade
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC != null)
            {
                foreach (var CapacidadePSCComValorNovo in servico.ServicosRecursosFinanceiroPrivadoCapacidadePSC)
                {
                    var CapacidadePSCExistente = servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Where(x => x.Exercicio == CapacidadePSCComValorNovo.Exercicio).FirstOrDefault();
                    if (CapacidadePSCExistente != null)
                    {
                        CapacidadePSCExistente.Capacidade = CapacidadePSCComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadePSC == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadePSC = new List<ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPrivadoCapacidadePSC.Add(new ServicoRecursoFinanceiroPrivadoCapacidadePSCInfo
                        {
                            IdServicoRecursoFinanceiroPrivado = servicoOriginal.Id
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
            if (servico.ServicosRecursosFinanceiroPrivadoMediaMensal != null)
            {
                foreach (var MediaMensalComValorNovo in servico.ServicosRecursosFinanceiroPrivadoMediaMensal)
                {
                    var MediaMensalExistente = servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensal.Where(x => x.Exercicio == MediaMensalComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalExistente != null)
                    {
                        MediaMensalExistente.MediaMensal = MediaMensalComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensal == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensal = new List<ServicoRecursoFinanceiroPrivadoMediaMensalInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensal.Add(new ServicoRecursoFinanceiroPrivadoMediaMensalInfo
                        {
                            IdServicoRecursoFinanceiroPrivado = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalComValorNovo.MediaMensal
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA != null)
            {
                foreach (var MediaMensalLAComValorNovo in servico.ServicosRecursosFinanceiroPrivadoMediaMensalLA)
                {
                    var MediaMensalLAExistente = servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Where(x => x.Exercicio == MediaMensalLAComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalLAExistente != null)
                    {
                        MediaMensalLAExistente.MediaMensal = MediaMensalLAComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalLA == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalLA = new List<ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalLA.Add(new ServicoRecursoFinanceiroPrivadoMediaMensalLAInfo
                        {
                            IdServicoRecursoFinanceiroPrivado = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalLAComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalLAComValorNovo.MediaMensal
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC != null)
            {
                foreach (var MediaMensalPSCComValorNovo in servico.ServicosRecursosFinanceiroPrivadoMediaMensalPSC)
                {
                    var MediaMensalPSCExistente = servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Where(x => x.Exercicio == MediaMensalPSCComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalPSCExistente != null)
                    {
                        MediaMensalPSCExistente.MediaMensal = MediaMensalPSCComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalPSC == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalPSC = new List<ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroPrivadoMediaMensalPSC.Add(new ServicoRecursoFinanceiroPrivadoMediaMensalPSCInfo
                        {
                            IdServicoRecursoFinanceiroPrivado = servicoOriginal.Id
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


            if (hasChangeSituacoes)
                propriedades.Add("situações específicas atendidas pelo serviço");
            if (hasChangeAtividades)
                propriedades.Add("trabalho social essencial do serviço");
            if (hasChangeFontesRecurso)
                propriedades.Add("Valor dos recursos da própria Organização utilizados exclusivamente para a execução deste serviço socioassistencial");


            if (propriedades.Count > 0)
            {
                if (servico.LocalExecucao == null)
                {
                    servico.LocalExecucao = new LocalExecucaoPrivado().GetById(servico.IdLocalExecucao);
                }

                string descricao = string.Empty;
                var acao = EAcao.Update;
                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade da Rede Indireta " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";
                    acao = EAcao.Deactivate;
                }
                else
                {
                    descricao = "Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade da Rede Indireta " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";
                    descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                }
                var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, acao, 39, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void Add(ServicoRecursoFinanceiroPrivadoInfo servico, Boolean commit)
        {
            try
            {
                this.ValidadorServicoRecursoFinanceiro.ValidarServicoPrivado(servico);
                var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);

                #region Inserir: [Serviço Não Tipificado] (pode inserir sem restrição)
                var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };
                if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico)))
                {

                    //if (_repository.GetQuery().Any(s => s.IdLocalExecucao == servico.IdLocalExecucao && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico))
                    //{
                    //    throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido a:<br/> 1 - Já existe cadastrado um serviço com esse tipo de usuário e tipo de serviço; <br/>ou<br/>2 - Já houve cadastro deste tipo de serviço e tipo de usuário neste local de execução e o referido serviço encontra-se desativado.<br/>Se for este o caso, será necessário desativar este local de execução e cadastrá-lo novamente para poder registrar o serviço.");
                    //}



                    //não poderá inserir mais de uma combinação de tipo de serviço + usuário para o mesmo local de execução
                    if (_repository.GetQuery()
                        .Any(s => s.IdLocalExecucao == servico.IdLocalExecucao
                               && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico
                               && !s.Desativado))
                    {
                        //var texto = string.Format("{0}", "Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas");
                    }



                } 
                #endregion

                #region Inserir: Situacoes Especificas
                var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
                servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
                #endregion

                #region Inserir: Atividades Socioassistenciais
                var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
                servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList(); 
                #endregion

                #region Inserir: Usuario Tipo Servico
                servico.UsuarioTipoServico = null;
                if (servico.LocalExecucao == null)
                {
                    servico.LocalExecucao = new LocalExecucaoPrivado().GetById(servico.IdLocalExecucao);
                } 
                #endregion

                var descricao = "Incluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade da Rede Indireta " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";
                var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, EAcao.Add, 39, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);

                servico.LocalExecucao = null;
                if (servico.ServicosRecursosFinanceirosFundosPrivadoInfo != null)
                {
                    foreach (var fundo in servico.ServicosRecursosFinanceirosFundosPrivadoInfo)
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

        public void Delete(ServicoRecursoFinanceiroPrivadoInfo servico, Boolean commit)
        {
            if (new ProgramaProjetoCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosPrivado == servico.Id))
            {
                throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            }
            if (new TransferenciaRendaCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosPrivado == servico.Id))
            {
                throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            }
            if (new PrefeituraBeneficioEventualServico().GetAll().Any(t => t.IdServicosRecursosFinanceirosPrivado == servico.Id))
            {
                throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            }


            var lstDeleted = new List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo>();
            var ppp = new ServicosRecursosFinanceirosPrivadoFonteRecursos();
            var lst = ppp.GetByRecursoFinanceiroPrivado(servico.Id);

            #region DBM: Fazer Deleção Lógica em cascata (Sem tempo habil - Não priorizado)
            //foreach (var p in lst)
            //    if (servico.ServicoRecursoFinanceiroPrivadoFonteRecurso.Any(t => t.Id == p.Id))
            //    {
            //        lstDeleted.Add(p);
            //    }

            //foreach (var p in lstDeleted)
            //    ppp.Delete(p, false);

            //var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            //if (servico.LocalExecucao == null)
            //    servico.LocalExecucao = new LocalExecucaoPrivado().GetById(servico.IdLocalExecucao);

            //var descricao = "Excluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.LocalExecucao.Nome + " (Unidade Privada " + servico.LocalExecucao.Unidade.Id + " - " + servico.LocalExecucao.Unidade.RazaoSocial + ").";

            //var log = Log.CreateLog(servico.LocalExecucao.Unidade.IdPrefeitura, EAcao.Remove, 39, descricao, servico.IdLocalExecucao, servico.LocalExecucao.IdUnidade);
            //servico.LocalExecucao = null; 
            #endregion

            _repository.Delete(servico);

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        #endregion

        #region Validacao
        public void ValidarServicoPrivado(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            var lstMsg = new List<string>();

            try
            {
                new ValidadorServicoRecursoFinanceiro().ValidarServicoPrivado(servico);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Helpers
        private static bool VerificarSeExisteACombinacaoServicoUsuario(ServicoRecursoFinanceiroPrivadoInfo servico)
        {
            if (_repository.GetQuery().Any(s => s.Id != servico.Id && s.IdLocalExecucao == servico.IdLocalExecucao && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico && !s.Desativado))
            {
                return true;
            }
            return false;
        }

        private List<String> GetLabelForInfo(List<String> propriedades, ServicoRecursoFinanceiroPrivadoInfo servico)
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
                    case "ValorPrivadoEmpresas": labels.Add("recursos financeiros de empresas"); break;
                    case "ValorPrivadoOrganizacoes": labels.Add("recursos financeiros de empresas"); break;
                    case "ValorPrivadoPessoasFisicas": labels.Add("recursos financeiros de pessoas físicas"); break;
                    case "ValorPrivadoProprios": labels.Add("recursos financeiros próprios"); break;
                    case "ServicoEstadualizado": labels.Add("serviço possui convênio firmado diretamente com Estado"); break;
                    case "ValorEstadualizado": labels.Add("valor anual do convênio"); break;
                    //case "ValorEstadualizado": if (servico.ServicosRecursosFinanceirosFundosPrivadoInfo[0].ServicoEstadualizado) labels.Add("valor anual do convênio 2018"); break;
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
        #endregion


        #region fontes de recurso

        private bool Merge(List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> recursosExistentes, List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> recursosNovos)
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

        private bool DeleteFontesRecurso(List<ServicoRecursoFinanceiroPrivadoFonteRecursoInfo> recursosExistentes)
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
