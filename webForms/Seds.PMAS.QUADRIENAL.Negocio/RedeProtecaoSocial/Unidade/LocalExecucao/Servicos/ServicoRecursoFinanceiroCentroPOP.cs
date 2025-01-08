using System;
using System.Collections.Generic;
using System.Linq;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ServicoRecursoFinanceiroCentroPOP
    {
        private const int ID_SERVICO_RUA = 144;
        #region repositorios
        private static IRepository<ServicoRecursoFinanceiroCentroPOPInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCentroPOPInfo>>();
            }
        }

        private static IRepository<ConsultaServicosRecursosFinanceirosCentroPOPInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaServicosRecursosFinanceirosCentroPOPInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroFundosCentroPOPInfo> _repositoryFundos
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosCentroPOPInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> _repositoryFontes
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo>>();
            }
        }
        #endregion

        #region crud

        #region criar
        public List<ServicoRecursoFinanceiroCentroPOPInfo> CriarTodosOsServicosGenericosEspecializadoSituacaoRua(CentroPOPInfo centroPop)
        {
            var servicos = new List<ServicoRecursoFinanceiroCentroPOPInfo>();
            foreach (var tipoServico in new UsuarioTipoServico().GetByTipoServico(ID_SERVICO_RUA))
            {
                var servicoRua = new ServicoRecursoFinanceiroCentroPOPInfo();
                servicoRua.IdCentroPOP = centroPop.Id;
                servicoRua.IdAbrangenciaServico = 3;//MUNICIPAL
                servicoRua.IdCaracteristicasTerritorio = 14;//NENHUMA
                servicoRua.IdUsuarioTipoServico = tipoServico.Id;
                servicoRua.IdRegiaoMoradia = 3;//AMBOS
                servicoRua.IdSexo = 3;//AMBOS  
                servicoRua.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
                servicoRua.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
                servicos.Add(servicoRua);
            }
            return servicos;
        }
        public ServicoRecursoFinanceiroCentroPOPInfo CriarIndividualServicoGenericosEspecializadoSituacaoRua(CentroPOPInfo centroPop, int idTipoServico)
        {
                var servicoRua = new ServicoRecursoFinanceiroCentroPOPInfo();
                servicoRua.IdCentroPOP = centroPop.Id;
                servicoRua.IdAbrangenciaServico = 3;//MUNICIPAL
                servicoRua.IdCaracteristicasTerritorio = 14;//NENHUMA
                servicoRua.IdUsuarioTipoServico = idTipoServico;
                servicoRua.IdRegiaoMoradia = 3;//AMBOS
                servicoRua.IdSexo = 3;//AMBOS  
                servicoRua.SituacoesEspecificas = new List<SituacaoEspecificaInfo>();
                servicoRua.AtividadesSocioAssistenciais = new List<AtividadeSocioAssistencialInfo>();
                return servicoRua;
        }

        public void Add(ServicoRecursoFinanceiroCentroPOPInfo servico, Boolean commit)
        {
            Add(servico, commit, true);
        }

        public void Add(ServicoRecursoFinanceiroCentroPOPInfo centroPop, Boolean commit, Boolean validar)
        {
            var usuario = new UsuarioTipoServico().GetById(centroPop.IdUsuarioTipoServico);
            var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };

            if (!centroPop.Desativado)
            {
                if (validar)
                {
                    Validar(centroPop);
                }

                if (!(servicosNaoTipificados.Contains(usuario.IdTipoServico)))  
                {
                    if (_repository.GetQuery().Any(s => s.IdCentroPOP == centroPop.IdCentroPOP && s.IdUsuarioTipoServico == centroPop.IdUsuarioTipoServico && !s.Desativado))
                    {
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                }
            }


            var idsSituacoes = centroPop.SituacoesEspecificas.Select(s => s.Id).ToList();
            centroPop.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoes.Contains(s.Id)).ToList();
            var idsAtividades = centroPop.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
            centroPop.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();
            centroPop.UsuarioTipoServico = null;

            if (centroPop.CentroPOP == null)
            {
                centroPop.CentroPOP = new CentroPOP().GetById(centroPop.IdCentroPOP);
            }

            if (centroPop.CentroPOP.UnidadePublica == null)
                centroPop.CentroPOP.UnidadePublica = new UnidadePublica().GetById(centroPop.CentroPOP.IdUnidade);

            var descricao = "Incluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + centroPop.CentroPOP.Id + " - " + centroPop.CentroPOP.Nome + ".";
            var log = Log.CreateLog(centroPop.CentroPOP.UnidadePublica.IdPrefeitura, EAcao.Add, 34, descricao, centroPop.IdCentroPOP);
            centroPop.CentroPOP = null;

            if (centroPop.ServicosRecursosFinanceirosFundosCentroPOPInfo != null)
            {
                foreach (var fundo in centroPop.ServicosRecursosFinanceirosFundosCentroPOPInfo)
                {
                    fundo.Desbloqueado = true;
                }
            }
            _repository.Add(centroPop);


            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();

            var logCentroPop = LogCentroPop.CreateLog(log.Id, centroPop.CentroPOP.Id, centroPop.CentroPOP.UnidadePublica.Id, log.DataHorario);
            if (logCentroPop != null)
                new LogCentroPop().Add(logCentroPop, true);
        }
        #endregion

        #region consultas

        public IQueryable<ServicoRecursoFinanceiroCentroPOPInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ServicoRecursoFinanceiroCentroPOPInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroCentroPOPCapacidade")
                .Include("ServicosRecursosFinanceiroCentroPOPMediaMensal")
                //.Include("ServicosRecursosFinanceiroCentroPOPMediaMensal")
                //.Include("ServicosRecursosFinanceiroCentroPOPCapacidade")
                .Include("ServicosRecursosFinanceirosFundosCentroPOPInfo")
                .Include("ServicosRecursosFinanceirosFundosCentroPOPInfo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo")
                .SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<ConsultaServicosRecursosFinanceirosCentroPOPInfo> GetConsultaByCentroPOP(Int32 idCentroPOP)
        {
            return _repositoryConsulta.GetQuery().Where(c => c.IdCentroPOP == idCentroPOP);
        }

        public IQueryable<ServicoRecursoFinanceiroCentroPOPInfo> GetByCentroPOP(Int32 idCentroPOP)
        {
            return _repository.GetObjectSet()
                .Include("SituacoesEspecificas")
                .Include("AtividadesSocioAssistenciais")
                .Include("UsuarioTipoServico")
                .Include("UsuarioTipoServico.TipoServico")
                .Include("ServicosRecursosFinanceiroCentroPOPCapacidade")
                .Include("ServicosRecursosFinanceiroCentroPOPMediaMensal")
                //.Include("ServicoRecursoFinanceiroCentroPOPFonteRecurso")
                .Include("ServicosRecursosFinanceirosFundosCentroPOPInfo")
                .Include("ServicosRecursosFinanceirosFundosCentroPOPInfo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo")
                .Where(s => s.IdCentroPOP == idCentroPOP);
        }

        public IQueryable<UsuarioTipoServicoInfo> GetUsuariosServicoEspecializadoSituacaoRuaByCentroPOP(Int32 idCentroPOP)
        {
            return GetCriarServicoEspecializadoSituacaoRuaByCentroPOP(idCentroPOP)
                .Select(t => t.UsuarioTipoServico).Distinct();
        }

        #endregion

        #region atualizar
        public void Update(ServicoRecursoFinanceiroCentroPOPInfo servico, Boolean commit)
        {
            var servicosNaoTipificados = new List<Int32>() { 138, 145, 153, 154, 155, 156, 157, 158, 159 };
            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            bool ehServicoNaoTipificado = servicosNaoTipificados.Contains(usuario.IdTipoServico);
            if (!servico.Desativado)
            {
                Validar(servico);
                #region Servico sem tipo
                if (!ehServicoNaoTipificado) //poderá inserir sem restrição
                {
                    if (ExisteCombinacaoTipoDeServicoComUsuarioMesmaLocalizacao(servico))
                    {
                        throw new Exception("Impossível cadastrar este tipo de serviço e tipo de usuário neste local de execução devido já existir um serviço ativo com estas caracteristicas.");
                    }
                }
            }

            #endregion

            #region situacoes especificas
            var idsSituacoesEspecificas = servico.SituacoesEspecificas.Select(s => s.Id).ToList();
            servico.SituacoesEspecificas = new SituacaoEspecifica().GetAll().Where(s => idsSituacoesEspecificas.Contains(s.Id)).ToList();
            #endregion

            #region Atividades Socio Assistenciais
            var idsAtividadesSocioAssistenciais = servico.AtividadesSocioAssistenciais.Select(s => s.Id).ToList();
            servico.AtividadesSocioAssistenciais = new AtividadeSocioAssistencial().GetAll().Where(s => idsAtividadesSocioAssistenciais.Contains(s.Id)).ToList();
            servico.UsuarioTipoServico = usuario;
            #endregion



            #region Primeiro Update
            _repository.Update(servico);
            #endregion

            #region Obtem label para log
            var propriedadesEntity = _repository.GetModifiedProperties(servico);
            var propriedades = GetLabelForInfo(propriedadesEntity, servico);

            #endregion

            if (propriedades.Contains("Desativado"))
            {
                if (commit)
                {
                    ContextManager.Commit();
                }
                return;
            }

            #region Obtem Cras atualizado
            var servicoOriginal = GetById(servico.Id);
            #endregion

            #region fundos
            var fundoNovo = servico.ServicosRecursosFinanceirosFundosCentroPOPInfo.First();
            var fundoExistente = servicoOriginal.ServicosRecursosFinanceirosFundosCentroPOPInfo
                .Where(fundoOriginal => fundoOriginal.Exercicio == fundoNovo.Exercicio
                    && fundoOriginal.ServicoRecursoFinanceiroCentroPOPInfoId == fundoNovo.ServicoRecursoFinanceiroCentroPOPInfoId).FirstOrDefault();

            bool hasChangeFontesRecurso = false;

            if (fundoExistente == null)
            {
                servicoOriginal.ServicosRecursosFinanceirosFundosCentroPOPInfo.Add(fundoNovo);
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
                    foreach (var fundo in fundoNovo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo)
                    {
                        fundo.IdServicoRecursoFinanceiroFundosCentroPOP = fundoExistente.Id;
                    }
                    hasChangeFontesRecurso = Merge(fundoExistente.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo
                                                    , fundoNovo.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                     fundoExistente.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo != null
                     && fundoExistente.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo.Count > 0;
                }
                else
                {
                    hasChangeFontesRecurso = DeleteFontesRecurso(fundoExistente.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo);
                    fundoExistente.ExisteOutraFonteFinanciamento =
                        fundoExistente.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo != null
                        && fundoExistente.ServicoRecursoFinanceiroCentroPOPFontesRecursosInfo.Count > 0;
                }
            }

            #endregion


            #region Funcionamento
            #region Capacidade
            if (servico.ServicosRecursosFinanceiroCentroPOPCapacidade != null)
            {
                foreach (var capacidadeComValorNovo in servico.ServicosRecursosFinanceiroCentroPOPCapacidade)
                {
                    var capacidadeExistente = servicoOriginal.ServicosRecursosFinanceiroCentroPOPCapacidade.Where(x => x.Exercicio == capacidadeComValorNovo.Exercicio).FirstOrDefault();
                    if (capacidadeExistente != null)
                    {
                        capacidadeExistente.Capacidade = capacidadeComValorNovo.Capacidade;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCentroPOPCapacidade == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCentroPOPCapacidade = new List<ServicoRecursoFinanceiroCentroPOPCapacidadeInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCentroPOPCapacidade.Add(new ServicoRecursoFinanceiroCentroPOPCapacidadeInfo
                        {
                            IdServicoRecursoFinanceiroCentroPOP = servicoOriginal.Id
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
            if (servico.ServicosRecursosFinanceiroCentroPOPMediaMensal != null)
            {
                foreach (var MediaMensalComValorNovo in servico.ServicosRecursosFinanceiroCentroPOPMediaMensal)
                {
                    var MediaMensalExistente = servicoOriginal.ServicosRecursosFinanceiroCentroPOPMediaMensal.Where(x => x.Exercicio == MediaMensalComValorNovo.Exercicio).FirstOrDefault();
                    if (MediaMensalExistente != null)
                    {
                        MediaMensalExistente.MediaMensal = MediaMensalComValorNovo.MediaMensal;
                    }
                    else
                    {
                        if (servicoOriginal.ServicosRecursosFinanceiroCentroPOPMediaMensal == null)
                        {
                            servicoOriginal.ServicosRecursosFinanceiroCentroPOPMediaMensal = new List<ServicoRecursoFinanceiroCentroPOPMediaMensalInfo>();
                        }

                        servicoOriginal.ServicosRecursosFinanceiroCentroPOPMediaMensal.Add(new ServicoRecursoFinanceiroCentroPOPMediaMensalInfo
                        {
                            IdServicoRecursoFinanceiroCentroPOP = servicoOriginal.Id
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
                if (servico.CentroPOP == null)
                {
                    servico.CentroPOP = new CentroPOP().GetById(servico.IdCentroPOP);
                }

                if (servico.CentroPOP.UnidadePublica == null)
                {
                    servico.CentroPOP.UnidadePublica = new UnidadePublica().GetById(servico.CentroPOP.IdUnidade);
                }

                if (propriedades.Contains("Desativado"))
                {
                    descricao = "Desativado o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CentroPOP.Id + " - " + servico.CentroPOP.Nome + ".";
                    acao = EAcao.Deactivate;
                }
                else
                {
                    descricao = "Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + " - " + servico.CentroPOP.Nome + ".";
                    descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                }

                //var log = Log.CreateLog(servico.CentroPOP.UnidadePublica.IdPrefeitura, acao, 34, descricao, servico.IdCentroPOP);
                //if (log != null)
                //{
                //    new Log().Add(log, false);
                //}

                servico.CentroPOP = null;
            }
            #endregion log

            if (commit)
            {
                ContextManager.Commit();
            }

        }
        #endregion

        #region remover
        public void Delete(ServicoRecursoFinanceiroCentroPOPInfo servico, Boolean commit)
        {

            Delete(servico, commit, true);
        }

        public void Delete(ServicoRecursoFinanceiroCentroPOPInfo servico, Boolean commit, Boolean validarVinculos)
        {
            if (validarVinculos)
            {
                if (new ProgramaProjetoCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosCentroPOP == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
                if (new TransferenciaRendaCofinanciamento().GetAll().Any(t => t.IdServicosRecursosFinanceirosCentroPOP == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
                if (new PrefeituraBeneficioEventualServico().GetAll().Any(t => t.IdServicosRecursosFinanceirosCentroPOP == servico.Id))
                    throw new Exception("Esse serviço está vinculado à algum programa e/ou benefício!");
            }

            var usuario = new UsuarioTipoServico().GetById(servico.IdUsuarioTipoServico);
            if (servico.CentroPOP == null)
                servico.CentroPOP = new CentroPOP().GetById(servico.IdCentroPOP);

            if (servico.CentroPOP.UnidadePublica == null)
                servico.CentroPOP.UnidadePublica = new UnidadePublica().GetById(servico.CentroPOP.IdUnidade);

            var descricao = "Excluído o Serviço e Recurso Financeiro de Proteção Social " + usuario.TipoServico.TipoProtecaoSocial.Nome + " - " + usuario.TipoServico.Nome + " - " + usuario.Nome + " do Local de Execução " + servico.CentroPOP.Id + " - " + servico.CentroPOP.Nome + ".";

            var log = Log.CreateLog(servico.CentroPOP.UnidadePublica.IdPrefeitura, EAcao.Remove, 34, descricao, servico.IdCentroPOP);
            servico.CentroPOP = null;
            _repository.Delete(servico);

            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();

            var logCentroPop = LogCentroPop.CreateLog(log.Id, servico.CentroPOP.Id, servico.CentroPOP.UnidadePublica.Id, log.DataHorario);
            if (logCentroPop != null)
                new LogCentroPop().Add(logCentroPop, true);
        }

        #endregion

        #endregion

        #region validacao
        public void Validar(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            new ValidadorServicoRecursoFinanceiro().ValidarCentroPOP(servico);           
        }

        public void ValidarProgramaServico(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            if (servico.PossuiProgramaBeneficio == true)
            {
                var cofinanciamento = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(servico.Id, servico.IdCentroPOP);
                if (cofinanciamento.Count() == 0)
                {
                    throw new Exception("Deve ser associado ao serviço pelo menos um programa ao serviço");
                }
            }
        }
        #endregion

        #region helpers
        private IQueryable<ServicoRecursoFinanceiroCentroPOPInfo> GetCriarServicoEspecializadoSituacaoRuaByCentroPOP(Int32 idCentroPOP)
        {
            return _repository.GetObjectSet().Include("UsuarioTipoServico")
                .Where(t => t.IdCentroPOP == idCentroPOP && t.UsuarioTipoServico.IdTipoServico == 144 && !t.Desativado );
        }

        private static bool ExisteCombinacaoTipoDeServicoComUsuarioMesmaLocalizacao(ServicoRecursoFinanceiroCentroPOPInfo servico)
        {
            return _repository.GetQuery().Any(s => s.Id != servico.Id && s.IdCentroPOP == servico.IdCentroPOP && s.IdUsuarioTipoServico == servico.IdUsuarioTipoServico && !s.Desativado);
        }

        private List<String> GetLabelForInfo(List<String> propriedades, ServicoRecursoFinanceiroCentroPOPInfo servico)
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
                    case "IdMotivoDesativacao":
                    case "Detalhamento":
                    case "DataRegistroLog":
                        labels.Add("Exclusão do Serviço"); break;

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

        private bool Merge(List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> recursosExistentes, List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> recursosNovos)
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

        private bool DeleteFontesRecurso(List<ServicoRecursoFinanceiroCentroPOPFonteRecursoInfo> recursosExistentes)
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
