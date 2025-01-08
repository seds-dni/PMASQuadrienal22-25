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
using Seds.PMAS.QUADRIENAL.Negocio;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ServicoRecursoFinanceiroCREAS
    {
        #region repositories
        private static IRepository<ServicoRecursoFinanceiroCREASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCREASInfo>>();
            }
        }

        private static IRepository<ConsultaServicosRecursosFinanceirosCREASInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaServicosRecursosFinanceirosCREASInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroFundosCREASInfo> _repositoryFundos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosCREASInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroCREASFonteRecursoInfo> _repositoryFontes
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCREASFonteRecursoInfo>>();
            }
        }
        #endregion


        public IQueryable<ServicoRecursoFinanceiroCREASInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroCREASInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroCREASCapacidade")
                .Include("ServicosRecursosFinanceiroCREASCapacidadeLA")
                .Include("ServicosRecursosFinanceiroCREASCapacidadePSC")
                .Include("ServicosRecursosFinanceiroCREASMediaMensal")
                .Include("ServicosRecursosFinanceiroCREASMediaMensalLA")
                .Include("ServicosRecursosFinanceiroCREASMediaMensalPSC")
                //.Include("ServicosRecursosFinanceiroCREASLA")
                //.Include("ServicosRecursosFinanceiroCREASPSC")
                //.Include("ServicoRecursoFinanceiroCREASFonteRecurso")
                .Include("ServicosRecursosFinanceirosFundosCREASInfo")
                .Include("ServicosRecursosFinanceirosFundosCREASInfo.ServicoRecursoFinanceiroCREASFontesRecursosInfo")
                .SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ConsultaServicosRecursosFinanceirosCREASInfo> GetConsultaByCREAS(Int32 idCREAS)
        {
            return _repositoryConsulta.GetQuery().Where(c => c.IdCREAS == idCREAS);
        }

        public IQueryable<ServicoRecursoFinanceiroCREASInfo> GetByCREAS(Int32 idCREAS)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroCREASCapacidade")
                .Include("ServicosRecursosFinanceiroCREASCapacidadeLA")
                .Include("ServicosRecursosFinanceiroCREASCapacidadePSC")
                .Include("ServicosRecursosFinanceiroCREASMediaMensal")
                .Include("ServicosRecursosFinanceiroCREASMediaMensalLA")
                .Include("ServicosRecursosFinanceiroCREASMediaMensalPSC")
                .Include("ServicosRecursosFinanceirosFundosCREASInfo")
                .Include("ServicosRecursosFinanceirosFundosCREASInfo.ServicoRecursoFinanceiroCREASFontesRecursosInfo")
                .Where(s => s.IdCREAS == idCREAS);
        }
        public void Update(ServicoRecursoFinanceiroCREASInfo servico, Boolean commit)
        {
            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };

            if (!servico.Desativado)
            {
                Validar(servico);
                if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico)))
                {

                    if (_repository.GetQuery().Any(s => s.Id != servico.Id && s.IdCREAS == servico.IdCREAS && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico && !s.Desativado))
                    {
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                }
            }

            #region situacoes especificas
            var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
            servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
            #endregion

            #region atividades socio assistenciais
            var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
            servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
            #endregion

            servico.UsuarioTipoServico = usuario;

            _repository.Update(servico);

            var propriedadesEntity = _repository.GetModifiedProperties(servico);
            var propriedades = GetLabelForInfo(propriedadesEntity, servico);

            var servicoOriginal = GetById(servico.Id);

            #region fundos
            var fundoNovo = servico.ServicosRecursosFinanceirosFundosCREASInfo.FirstOrDefault();
            var fundoExistente = servicoOriginal.ServicosRecursosFinanceirosFundosCREASInfo
                .Where(fundoOriginal => fundoOriginal.Exercicio == fundoNovo.Exercicio
                    && fundoOriginal.ServicoRecursoFinanceiroCREASInfoId == fundoNovo.ServicoRecursoFinanceiroCREASInfoId).FirstOrDefault();

            bool hasChangeFontesRecurso = false;

            if (fundoExistente == null)
            {
                servicoOriginal.ServicosRecursosFinanceirosFundosCREASInfo.Add(fundoNovo);
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
                    foreach (var fundo in fundoNovo.ServicoRecursoFinanceiroCREASFontesRecursosInfo)
                    {
                        fundo.IdServicoRecursoFinanceiroFundosCREAS = fundoExistente.Id;
                    }
                    hasChangeFontesRecurso = Merge(fundoExistente.ServicoRecursoFinanceiroCREASFontesRecursosInfo
                                                    , fundoNovo.ServicoRecursoFinanceiroCREASFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                     fundoExistente.ServicoRecursoFinanceiroCREASFontesRecursosInfo != null
                     && fundoExistente.ServicoRecursoFinanceiroCREASFontesRecursosInfo.Count > 0;
                }
                else
                {
                    hasChangeFontesRecurso = DeleteFontesRecurso(fundoExistente.ServicoRecursoFinanceiroCREASFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                        fundoExistente.ServicoRecursoFinanceiroCREASFontesRecursosInfo != null
                        && fundoExistente.ServicoRecursoFinanceiroCREASFontesRecursosInfo.Count > 0;
                }
            }

            #endregion
            #region Funcionamento
            #region Capacidade
            if (servico.ServicosRecursosFinanceiroCREASCapacidade != null)
            {
                foreach (var capacidadeComValorNovo in servico.ServicosRecursosFinanceiroCREASCapacidade)
                {
                    var capacidadeExistente = servicoOriginal.ServicosRecursosFinanceiroCREASCapacidade.Where(x => x.Exercicio == capacidadeComValorNovo.Exercicio).FirstOrDefault();
                    if (capacidadeExistente != null)
                    {
                        capacidadeExistente.Capacidade = capacidadeComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCREASCapacidade == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCREASCapacidade = new List<ServicoRecursoFinanceiroCREASCapacidadeInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCREASCapacidade.Add(new ServicoRecursoFinanceiroCREASCapacidadeInfo
                        {
                            IdServicoRecursoFinanceiroCREAS = servicoOriginal.Id
                            ,
                            Exercicio = capacidadeComValorNovo.Exercicio
                            ,
                            Capacidade = capacidadeComValorNovo.Capacidade
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroCREASCapacidadeLA != null)
            {
                foreach (var CapacidadeLAComValorNovo in servico.ServicosRecursosFinanceiroCREASCapacidadeLA)
                {
                    var CapacidadeLAExistente = servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadeLA.Where(x => x.Exercicio == CapacidadeLAComValorNovo.Exercicio).FirstOrDefault();
                    if (CapacidadeLAExistente != null)
                    {
                        CapacidadeLAExistente.Capacidade = CapacidadeLAComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadeLA == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadeLA = new List<ServicoRecursoFinanceiroCREASCapacidadeLAInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadeLA.Add(new ServicoRecursoFinanceiroCREASCapacidadeLAInfo
                        {
                            IdServicoRecursoFinanceiroCREAS = servicoOriginal.Id
                            ,
                            Exercicio = CapacidadeLAComValorNovo.Exercicio
                            ,
                            Capacidade = CapacidadeLAComValorNovo.Capacidade
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroCREASCapacidadePSC != null)
            {
                foreach (var CapacidadePSCComValorNovo in servico.ServicosRecursosFinanceiroCREASCapacidadePSC)
                {
                    var CapacidadePSCExistente = servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadePSC.Where(x => x.Exercicio == CapacidadePSCComValorNovo.Exercicio).FirstOrDefault();
                    if (CapacidadePSCExistente != null)
                    {
                        CapacidadePSCExistente.Capacidade = CapacidadePSCComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadePSC == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadePSC = new List<ServicoRecursoFinanceiroCREASCapacidadePSCInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCREASCapacidadePSC.Add(new ServicoRecursoFinanceiroCREASCapacidadePSCInfo
                        {
                            IdServicoRecursoFinanceiroCREAS = servicoOriginal.Id
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
            if (servico.ServicosRecursosFinanceiroCREASMediaMensal != null)
            {
                foreach (var MediaMensalComValorNovo in servico.ServicosRecursosFinanceiroCREASMediaMensal)
                {
                    var MediaMensalExistente = servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensal.Where(x => x.Exercicio == MediaMensalComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalExistente != null)
                    {
                        MediaMensalExistente.MediaMensal = MediaMensalComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensal == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensal = new List<ServicoRecursoFinanceiroCREASMediaMensalInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensal.Add(new ServicoRecursoFinanceiroCREASMediaMensalInfo
                        {
                            IdServicoRecursoFinanceiroCREAS = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalComValorNovo.MediaMensal
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroCREASMediaMensalLA != null)
            {
                foreach (var MediaMensalLAComValorNovo in servico.ServicosRecursosFinanceiroCREASMediaMensalLA)
                {
                    var MediaMensalLAExistente = servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalLA.Where(x => x.Exercicio == MediaMensalLAComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalLAExistente != null)
                    {
                        MediaMensalLAExistente.MediaMensal = MediaMensalLAComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalLA == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalLA = new List<ServicoRecursoFinanceiroCREASMediaMensalLAInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalLA.Add(new ServicoRecursoFinanceiroCREASMediaMensalLAInfo
                        {
                            IdServicoRecursoFinanceiroCREAS = servicoOriginal.Id
                            ,
                            Exercicio = MediaMensalLAComValorNovo.Exercicio
                            ,
                            MediaMensal = MediaMensalLAComValorNovo.MediaMensal
                        });
                    }

                }
            }

            if (servico.ServicosRecursosFinanceiroCREASMediaMensalPSC != null)
            {
                foreach (var MediaMensalPSCComValorNovo in servico.ServicosRecursosFinanceiroCREASMediaMensalPSC)
                {
                    var MediaMensalPSCExistente = servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalPSC.Where(x => x.Exercicio == MediaMensalPSCComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalPSCExistente != null)
                    {
                        MediaMensalPSCExistente.MediaMensal = MediaMensalPSCComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalPSC == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalPSC = new List<ServicoRecursoFinanceiroCREASMediaMensalPSCInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCREASMediaMensalPSC.Add(new ServicoRecursoFinanceiroCREASMediaMensalPSCInfo
                        {
                            IdServicoRecursoFinanceiroCREAS = servicoOriginal.Id
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

            var hasChangeSituacoes = _repository.UpdateNN<SituacaoEspecificaInfo>(servicoOriginal, servico.SituacoesEspecificas, (s, lst) => lst.Any(t => t.Id == s.Id), p => p.SituacoesEspecificas);

            var hasChangeAtividades = _repository.UpdateNN<AtividadeSocioAssistencialInfo>(servicoOriginal, servico.AtividadesSocioAssistenciais, (s, lst) => lst.Any(t => t.Id == s.Id), p => p.AtividadesSocioAssistenciais);

            if (hasChangeFontesRecurso)
                propriedades.Add("fontes de recursos atendidas pelo serviço");
            if (hasChangeSituacoes)
                propriedades.Add("situações específicas atendidas pelo serviço");
            if (hasChangeAtividades)
                propriedades.Add("trabalho social essencial do serviço");
            var acao = EAcao.Update;

            if (propriedades.Count > 0)
            {
                string descricao = string.Empty;

                if (servico.CREAS == null)
                    servico.CREAS = new CREAS().GetById(servico.IdCREAS);

                if (servico.CREAS.UnidadePublica == null)
                    servico.CREAS.UnidadePublica = new UnidadePublica().GetById(servico.CREAS.IdUnidade);
                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CREAS.Id + " - " + servico.CREAS.Nome + ".";
                    acao = EAcao.Deactivate;
                }
                else
                {
                    descricao = "Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + " - " + servico.CREAS.Nome + ".";
                    descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                }

                var log = Log.CreateLog(servico.CREAS.UnidadePublica.IdPrefeitura, acao, 29, descricao, servico.IdCREAS);
                if (log != null)
                    new Log().Add(log, false);
            }


            if (commit)
                ContextManager.Commit();
        }

        public void Add(ServicoRecursoFinanceiroCREASInfo servico, Boolean commit)
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
        public void Add(ServicoRecursoFinanceiroCREASInfo servico, Boolean commit, Boolean validar)
        {
            try
            {
                if (validar)
                {
                    Validar(servico);
                }
                var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);

                #region servico nao tipificado
                var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };
                //caso for serviço não tipificado, poderá inserir sem restrição
                if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico)))
                {
                    if (_repository.GetQuery()
                        .Any(s => s.IdCREAS == servico.IdCREAS
                               && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico
                               && !s.Desativado))
                    {
                        var texto = string.Format("{0}{1}", "Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                        throw new Exception();
                    }

                    //if (_repository.GetQuery().Any(s => s.IdCREAS == servico.IdCREAS && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico))
                    //    throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido a:<br/> 1 - Já existe cadastrado um serviço com esse tipo de usuário e tipo de serviço; <br/>ou<br/>2 - Já houve cadastro deste tipo de serviço e tipo de usuário neste local de execução e o referido serviço encontra-se desativado.<br/>Se for este o caso, será necessário desativar este local de execução e cadastrá-lo novamente para poder registrar o serviço.");
                }
                #endregion

                #region situacoes especificas
                var idsSituacoes = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
                servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
                #endregion

                #region atividades socio assistenciais
                var idsAtividades = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
                servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
                servico.UsuarioTipoServico = null;
                #endregion

                if (servico.CREAS == null)
                {
                    servico.CREAS = new CREAS().GetById(servico.IdCREAS);
                }

                if (servico.CREAS.UnidadePublica == null)
                {
                    servico.CREAS.UnidadePublica = new UnidadePublica().GetById(servico.CREAS.IdUnidade);
                }

                var descricao = "Incluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CREAS.Id + " - " + servico.CREAS.Nome + ".";
                var log = Log.CreateLog(servico.CREAS.UnidadePublica.IdPrefeitura, EAcao.Add, 29, descricao, servico.IdCREAS);
                servico.CREAS = null;

                if (servico.ServicosRecursosFinanceirosFundosCREASInfo != null)
                {
                    foreach (var fundo in servico.ServicosRecursosFinanceirosFundosCREASInfo)
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

        public void Delete(ServicoRecursoFinanceiroCREASInfo servico, Boolean commit)
        {
            Delete(servico, commit, true);
        }
        public void Delete(ServicoRecursoFinanceiroCREASInfo servico, Boolean commit, Boolean validarVinculos)
        {
            if (validarVinculos)
            {
                if (new ProgramaProjetoCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosCREAS == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
                if (new TransferenciaRendaCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosCREAS == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
                if (new PrefeituraBeneficioEventualServico().GetAll().Any(t => t.IdServicosRecursosFinanceirosCREAS == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            }

            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            if (servico.CREAS == null)
                servico.CREAS = new CREAS().GetById(servico.IdCREAS);

            if (servico.CREAS.UnidadePublica == null)
                servico.CREAS.UnidadePublica = new UnidadePublica().GetById(servico.CREAS.IdUnidade);


            var lstDeleted = new List<ServicoRecursoFinanceiroCREASFonteRecursoInfo>();
            var ppp = new ServicoRecursoFinanceiroCREASFonteRecurso();
            var lst = ppp.GetByRecursoFinanceiroCREAS(servico.Id);

            //TODO: DBM: DELETA OS FUNDOS > RECURSOS
            //foreach (var p in lst)
            //    if (servico.ServicoRecursoFinanceiroCREASFonteRecurso.Any(t => t.Id == p.Id))
            //    {
            //        lstDeleted.Add(p);
            //    }

            //foreach (var p in lstDeleted)
            //    ppp.Delete(p, false);


            var descricao = "Excluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CREAS.Id + " - " + servico.CREAS.Nome + ".";
            var log = Log.CreateLog(servico.CREAS.UnidadePublica.IdPrefeitura, EAcao.Remove, 29, descricao, servico.IdCREAS);
            servico.CREAS = null;

            _repository.Delete(servico);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Validar(ServicoRecursoFinanceiroCREASInfo servico)
        {
            new ValidadorServicoRecursoFinanceiro().ValidarServicoCREAS(servico);
        }
        public void ValidarProgramaServico(ServicoRecursoFinanceiroCREASInfo servico)
        {
            if (servico.PossuiProgramaBeneficio == true)
            {
                var cofinanciamento = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(servico.Id, servico.IdCREAS);
                if (cofinanciamento.Count() == 0)
                {
                    throw new Exception("Deve ser associado ao serviço pelo menos um programa ao serviço");
                }
            }
        }
        public ServicoRecursoFinanceiroCREASInfo CriarServicoPAEFI()
        {
            var s = new ServicoRecursoFinanceiroCREASInfo();
            s.IdAbrangenciaServico = 3;//MUNICIPAL
            s.IdCaracteristicasTerritorio = 8;//NENHUMA
            s.IdUsuarioTipoServico = 18;//FAMÍLIAS E INDIVÍDUOS
            s.IdRegiaoMoradia = 3;//AMBOS
            s.IdSexo = 3;//AMBOS  
            s.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
            s.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
            return s;
        }

        public ServicoRecursoFinanceiroCREASInfo GetServicoPAEFIByCREAS(Int32 idCREAS)
        {
            return _repository.GetQuery().SingleOrDefault(t => t.IdCREAS == idCREAS && t.IdUsuarioTipoServico == 18);
        }

        #region Helper
        public List<String> GetLabelForInfo(List<String> propriedades, ServicoRecursoFinanceiroCREASInfo servico)
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


        private bool Merge(List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> recursosExistentes, List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> recursosNovos)
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

        private bool DeleteFontesRecurso(List<ServicoRecursoFinanceiroCREASFonteRecursoInfo> recursosExistentes)
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
