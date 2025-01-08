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

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class CentroPOP
    {

        private const int USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_ADOLESCENTES = 27; //27	apenas crianças e adolescentes	144
        private const int USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_JOVENS_ADULTOS_IDOSOS_FAMILIAS = 28; //28 - apenas jovens, adultos, idosos e famílias	144
        private const int USUARIO_TIPO_SERVICO_SITUACAO_RUA_TODOS = 29; //29 - crianças, adolescentes, jovens, adultos, idosos e famílias	144

        private static IRepository<CentroPOPInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CentroPOPInfo>>();
            }
        }

        private static IRepository<CentroPOPMunicipioInfo> _repositoryCentroPOPMunicipio
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CentroPOPMunicipioInfo>>();
            }
        }

        private static IRepository<ConsultaCentroPOPInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaCentroPOPInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroFundosCentroPOPInfo> _repositoryServicosFundosCentroPopConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroFundosCentroPOPInfo>>();
            }
        }

        private static IRepository<ServicoRecursoFinanceiroCentroPOPInfo> _repositoryServicosCentroPopConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ServicoRecursoFinanceiroCentroPOPInfo>>();
            }
        }

        public IQueryable<CentroPOPInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public CentroPOPInfo GetById(int id)
        {
            var centro = _repository.GetObjectSet().Include("AcoesSocioAssistenciais").Single(m => m.Id == id);

            if (centro == null) return null;

            centro.Usuarios = new ServicoRecursoFinanceiroCentroPOP()
                .GetUsuariosServicoEspecializadoSituacaoRuaByCentroPOP(centro.Id).ToList();

            if (centro.AtendeOutrosMunicipios)
            {
                centro.AbrangenciaMunicipios = new CentroPOPMunicipio().GetByCentroPop(centro.Id).ToList();
            }

            return centro;
        }

        public IQueryable<CentroPOPInfo> GetByUnidade(int idUnidade)
        {
            return _repository.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<ConsultaCentroPOPInfo> GetConsultaByUnidade(int idUnidade)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public void Update(CentroPOPInfo centro, Boolean commit)
        {
            //var servicoRepositorio = new ServicoRecursoFinanceiroCentroPOP();
            //var servicoSituacaoRua = servicoRepositorio.GetUsuariosServicoEspecializadoSituacaoRuaByCentroPOP(centro.Id);

            var centroOriginal = GetById(centro.Id);
            #region Validacao
            new ValidadorCentroPOP().Validar(centro);
            #endregion

            ServicoRecursoFinanceiroCentroPOP servicoCentroPopBusiness = new ServicoRecursoFinanceiroCentroPOP();

            //DBM: Sistema só remove SITUACAO DE RUA via edição "centro pop" e não por desativação
            #region Situacao de Rua
            var optouPorServicoRua = centro.PossuiServicoEspecializadoSituacaoRua.Value;
            var servicos = servicoCentroPopBusiness.GetByCentroPOP(centro.Id).ToList();

            var existeServicosRecursosFinanceirosCentroPop = (servicos.Any() && servicos.Count > 0);

            if (optouPorServicoRua)
            {
                var servicosEmSituacaoRua = servicos.Where(s => (((s.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_ADOLESCENTES)
                                                                                         || (s.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_JOVENS_ADULTOS_IDOSOS_FAMILIAS)
                                                                                         || (s.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_TODOS)
                                                                                            ) && !s.Desativado)
                                                                                         ).ToList();
                if (!servicosEmSituacaoRua.Any())
                {
                    var servicosSituacaoRuaDefault = servicoCentroPopBusiness.CriarTodosOsServicosGenericosEspecializadoSituacaoRua(centro);

                    foreach (var servicoDefault in servicosSituacaoRuaDefault.Where(t => centro.Usuarios.Any(u => u.Id == t.IdUsuarioTipoServico)))
                    {
                        servicoDefault.IdCentroPOP = centro.Id;
                        servicoDefault.CentroPOP = centro;
                        servicoCentroPopBusiness.Add(servicoDefault, true, false);
                    }
                }
                else
                {
                    RegraDoCadastro27(centro, servicosEmSituacaoRua, servicoCentroPopBusiness);
                    RegraDoCadastro28(centro, servicosEmSituacaoRua, servicoCentroPopBusiness);
                    RegraDoCadastro29(centro, servicosEmSituacaoRua, servicoCentroPopBusiness);
                }

            }
            else
            {
                if (existeServicosRecursosFinanceirosCentroPop)
                {
                    var servicosEmSituacaoRua = servicos.Where(s => (s.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_ADOLESCENTES)
                                                                                             || (s.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_JOVENS_ADULTOS_IDOSOS_FAMILIAS)
                                                                                             || (s.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_TODOS)).ToList();
                    if (servicosEmSituacaoRua.Any())
                    {
                        //Situacao 2.1 - Setou Não e existe Servico cadastrado
                        foreach (var servico in servicosEmSituacaoRua)
                        {
                            servico.Desativado = true;
                            servico.IdMotivoDesativacao = 1;
                            servico.DataDesativacao = DateTime.Now;
                            servico.DataRegistroLog = DateTime.Now;
                            _repositoryServicosCentroPopConsulta.Update(servico);
                        }

                    }
                    centro.PossuiServicoEspecializadoSituacaoRua = false;
                    _repository.Update(centro);
                }
            }
            #endregion

            #region Acoes SocioAssistenciais
            var idsAcoes = centro.AcoesSocioAssistenciais.Select(s => s.Id).ToList();
            centro.AcoesSocioAssistenciais = new AcaoSocioAssistencial().GetCentroPOP().Where(s => idsAcoes.Contains(s.Id)).ToList();
            #endregion

            _repository.Update(centro);

            if (centro.UnidadePublica == null)
            {
                centro.UnidadePublica = new UnidadePublica().GetById(centro.IdUnidade);
            }

            #region Log [Quadro 32]
            var propriedadesEntity = _repository.GetModifiedProperties(centro);
            var propriedades = GetLabelForInfo(propriedadesEntity, centro);
            var acao = EAcao.Update;
            if (propriedades.Count > 0)
            {
                String descricao = String.Empty;
                if (propriedades.Contains("Desativado"))
                {
                    acao = EAcao.Deactivate;
                    descricao = "Desativado o CREAS " + centro.IDCREAS + " - " + centro.Nome + ".";
                }
                else
                    descricao = "CREAS: " + centro.IDCREAS + " - " + centro.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                //String descricao = "Centro POP: " + centro.IDCREAS + " - " + centro.Nome + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);

                var log = Log.CreateLog(centro.UnidadePublica.IdPrefeitura, acao, 96, descricao, centro.Id);
                if (log != null)
                    new Log().Add(log, true);

                var logCentroPop = LogCentroPop.CreateLog(log.Id, centro.Id, centro.UnidadePublica.Id, log.DataHorario);
                if (logCentroPop != null)
                    new LogCentroPop().Add(logCentroPop, true);
            }
            #endregion

            #region Log [Quadro 33]
            var hasChangeAcoes = _repository.UpdateNN<AcaoSocioAssistencialInfo>(centroOriginal, centro.AcoesSocioAssistenciais, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);
            if (hasChangeAcoes)
            {
                String descricao = "Modificado o trabalho social realizado pelo Centro POP " + centro.IDCREAS + " - " + centro.Nome + ".";
                var log = Log.CreateLog(centro.UnidadePublica.IdPrefeitura, EAcao.Update, 33, descricao, centro.Id);
                if (log != null)
                    new Log().Add(log, false);

                var logCentroPop = LogCentroPop.CreateLog(log.Id, centro.Id, centro.UnidadePublica.Id, log.DataHorario);
                if (logCentroPop != null)
                    new LogCentroPop().Add(logCentroPop, true);
            }
            #endregion

            if (!centro.Desativado)
            {
                var centroPopMunicipioNegocio = new CentroPOPMunicipio();
                centro.AbrangenciaMunicipios = centro.AbrangenciaMunicipios ?? new List<CentroPOPMunicipioInfo>();
                var hasChangeAbrangencia = false;

                #region Abrangências

                #region seleciona abrangências antigas
                var abrangencias = centroPopMunicipioNegocio.GetByCentroPop(centro.Id).ToList();
                var abrangenciasAntigas = new List<CentroPOPMunicipioInfo>();
                foreach (var abrangenciaAntiga in abrangencias)
                {
                    if (!centro.AbrangenciaMunicipios.Any(t => t.Id == abrangenciaAntiga.Id))
                    {
                        hasChangeAbrangencia = true;
                        abrangenciasAntigas.Add(abrangenciaAntiga);
                    }
                }
                #endregion

                #region deleta abrangências antigas
                foreach (var abrangenciaAntiga in abrangenciasAntigas)
                {
                    centroPopMunicipioNegocio.Delete(abrangenciaAntiga, false);
                }
                #endregion

                #region Adiciona abrangências novas

                foreach (var abrangenciaNova in centro.AbrangenciaMunicipios)
                {
                    abrangenciaNova.TipoAtendimento = null;
                    abrangenciaNova.IdCentroPop = centro.Id;
                    if (abrangenciaNova.Id == 0)
                    {
                        centroPopMunicipioNegocio.Add(abrangenciaNova, false);
                        hasChangeAbrangencia = true;
                    }
                    else
                    {
                        if (hasChangeAbrangencia)
                        {
                            centroPopMunicipioNegocio.Update(abrangenciaNova, false);
                        }
                    }
                }
                #endregion

                #region Log [Abrangências novas]
                if (hasChangeAbrangencia && !propriedades.Any(t => t == "AbrangenciaMunicipios"))
                    propriedades.Add("AbrangenciaMunicipios");

                if (hasChangeAbrangencia)
                {
                    String descricao = "Centro POP " + centro.IDCREAS + " - " + centro.Nome;
                    if (propriedades.Count > 0)
                        descricao += System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                    if (hasChangeAbrangencia)
                        descricao += System.Environment.NewLine + "Modificado os municípios atendidos pelo Centro POP.";

                    var log = Log.CreateLog(centro.UnidadePublica.IdPrefeitura, EAcao.Update, 96, descricao, centro.Id);
                    if (log != null)
                        new Log().Add(log, false);

                    var logCentroPop = LogCentroPop.CreateLog(log.Id, centro.Id, centro.UnidadePublica.Id, log.DataHorario);
                    if (logCentroPop != null)
                        new LogCentroPop().Add(logCentroPop, true);
                }
                #endregion
                #endregion

            }

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        private static void RegraDoCadastro27(CentroPOPInfo centro, List<ServicoRecursoFinanceiroCentroPOPInfo> servicosEmSituacaoRua, ServicoRecursoFinanceiroCentroPOP negocio)
        {
            var usuarioSelecionado = centro.Usuarios.Where(x => x.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_ADOLESCENTES);
            var selecionado27 = usuarioSelecionado != null ? usuarioSelecionado.FirstOrDefault() : null;
            var selecionado27Existe = (selecionado27 != null);

            var servicoExistenteCadastro27 = servicosEmSituacaoRua.Where(x => x.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_ADOLESCENTES).FirstOrDefault();
            var cadastro27Existe = (servicoExistenteCadastro27 != null);

            if (selecionado27Existe && cadastro27Existe)
            { }
            if (!selecionado27Existe && cadastro27Existe)
            {
                //suspender o cadastro
                servicoExistenteCadastro27.Desativado = true;
                servicoExistenteCadastro27.IdMotivoDesativacao = 1;
                servicoExistenteCadastro27.DataDesativacao = DateTime.Now;
                servicoExistenteCadastro27.DataRegistroLog = DateTime.Now;
                negocio.Update(servicoExistenteCadastro27, true);

            }
            if (selecionado27Existe && !cadastro27Existe)
            {
                //Cadastrar o servico
                var servico27Criado = negocio.CriarIndividualServicoGenericosEspecializadoSituacaoRua(centro, USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_ADOLESCENTES);
                negocio.Add(servico27Criado, true, false);
            }
        }

        private static void RegraDoCadastro28(CentroPOPInfo centro, List<ServicoRecursoFinanceiroCentroPOPInfo> servicosEmSituacaoRua, ServicoRecursoFinanceiroCentroPOP negocio)
        {
            var usuarioSelecionado = centro.Usuarios.Where(x => x.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_JOVENS_ADULTOS_IDOSOS_FAMILIAS);
            var selecionado28 = usuarioSelecionado != null ? usuarioSelecionado.FirstOrDefault() : null;

            var selecionado28Existe = (selecionado28 != null);
            var servicoExistenteCadastro28 = servicosEmSituacaoRua.Where(x => x.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_JOVENS_ADULTOS_IDOSOS_FAMILIAS).FirstOrDefault();
            var cadastro28Existe = (servicoExistenteCadastro28 != null);

            if (selecionado28Existe && cadastro28Existe)
            { }
            if (!selecionado28Existe && cadastro28Existe)
            {
                //suspender o cadasrtro
                servicoExistenteCadastro28.Desativado = true;
                servicoExistenteCadastro28.IdMotivoDesativacao = 1;
                servicoExistenteCadastro28.DataDesativacao = DateTime.Now;
                servicoExistenteCadastro28.DataRegistroLog = DateTime.Now;
                negocio.Update(servicoExistenteCadastro28, true);
            }
            if (selecionado28Existe && !cadastro28Existe)
            {
                //Cadastrar o servico
                var servico28Criado = negocio.CriarIndividualServicoGenericosEspecializadoSituacaoRua(centro, USUARIO_TIPO_SERVICO_SITUACAO_RUA_CRIANCAS_JOVENS_ADULTOS_IDOSOS_FAMILIAS);
                negocio.Add(servico28Criado, true, false);
            }
        }

        private static void RegraDoCadastro29(CentroPOPInfo centro, List<ServicoRecursoFinanceiroCentroPOPInfo> servicosEmSituacaoRua, ServicoRecursoFinanceiroCentroPOP negocio)
        {
            var usuarioSelecionado = centro.Usuarios.Where(x => x.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_TODOS);
            var selecionado29 = usuarioSelecionado != null ? usuarioSelecionado.FirstOrDefault() : null;
            var selecionado29Existe = (selecionado29 != null);
            var servicoExistenteCadastro29 = servicosEmSituacaoRua.Where(x => x.UsuarioTipoServico.Id == USUARIO_TIPO_SERVICO_SITUACAO_RUA_TODOS).FirstOrDefault();
            var cadastro29Existe = (servicoExistenteCadastro29 != null);

            if (selecionado29Existe && cadastro29Existe)
            { }
            if (!selecionado29Existe && cadastro29Existe)
            {
                //suspender o cadasrtro
                servicoExistenteCadastro29.Desativado = true;
                servicoExistenteCadastro29.IdMotivoDesativacao = 1;
                servicoExistenteCadastro29.DataDesativacao = DateTime.Now;
                servicoExistenteCadastro29.DataRegistroLog = DateTime.Now;
                negocio.Update(servicoExistenteCadastro29, true);
            }
            if (selecionado29Existe && !cadastro29Existe)
            {
                //Cadastrar o servico
                var servico29Criado = negocio.CriarIndividualServicoGenericosEspecializadoSituacaoRua(centro, USUARIO_TIPO_SERVICO_SITUACAO_RUA_TODOS);
                negocio.Add(servico29Criado, true, false);
            }
        }

        public void Add(CentroPOPInfo centro, Boolean commit)
        {
            #region Validação
            new ValidadorCentroPOP().Validar(centro);
            #endregion

            TransactionOptions tsOptions = new TransactionOptions();
            tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
            {
                ContextManager.OpenConnection();

                #region adicionar Centro Pop
                var idsAcoes = centro.AcoesSocioAssistenciais.Select(s => s.Id).ToList();
                centro.AcoesSocioAssistenciais = new AcaoSocioAssistencial().GetCentroPOP().Where(s => idsAcoes.Contains(s.Id)).ToList();
                _repository.Add(centro);
                #endregion

                #region adicionar Atendimento - Outros Municipios
                if (centro.AtendeOutrosMunicipios)
                {
                    var centroPOPMunicipio = new CentroPOPMunicipio();
                    foreach (var item in centro.AbrangenciaMunicipios)
                    {
                        var centroPop = new CentroPOPMunicipioInfo();
                        centroPop.IdCentroPop = centro.Id;
                        centroPop.IdMunicipio = item.IdMunicipio;
                        centroPop.IdTipoAtendimento = item.IdTipoAtendimento;
                        centroPop.Municipio = item.Municipio;
                        centroPop.NumeroAtendidos = item.NumeroAtendidos;
                        centroPOPMunicipio.Add(centroPop, true, false);
                    }
                }
                #endregion

                #region Obter Unidade Publica
                if (centro.UnidadePublica == null)
                {
                    centro.UnidadePublica = new UnidadePublica().GetById(centro.IdUnidade);
                }
                #endregion

                #region Adicionar - Log
                var log = Log.CreateLog(centro.UnidadePublica.IdPrefeitura, EAcao.Add, 32, "Incluído o Centro POP " + centro.Nome + ".");
                if (log != null)
                {
                    new Log().Add(log, true);
                }

                var logCentroPop = LogCentroPop.CreateLog(log.Id, centro.Id, centro.UnidadePublica.Id, log.DataHorario);
                if (logCentroPop != null)
                {
                    new LogCentroPop().Add(logCentroPop, true);
                }

                #endregion

                #region Adicionar Servico Especializado - Situação de Rua

                if (centro.PossuiServicoEspecializadoSituacaoRua.HasValue)
                {
                    if (centro.PossuiServicoEspecializadoSituacaoRua.Value)
                    {
                        var servico = new ServicoRecursoFinanceiroCentroPOP();
                        //27	apenas crianças e adolescentes	144
                        //28	apenas jovens, adultos, idosos e famílias	144
                        //29	crianças, adolescentes, jovens, adultos, idosos e famílias	144
                        var servicos = servico.CriarTodosOsServicosGenericosEspecializadoSituacaoRua(centro);
                        foreach (var s in servicos.Where(t => centro.Usuarios.Any(u => u.Id == t.IdUsuarioTipoServico)))
                        {
                            s.IdCentroPOP = centro.Id;
                            s.CentroPOP = centro;
                            servico.Add(s, true, false);
                        }
                    }
                }
                #endregion
                if (commit)
                {
                    ContextManager.Commit();
                }

                ContextManager.CloseConnection();
                ts.Complete();
            }
        }

        public void Delete(CentroPOPInfo centroPOP, Boolean commit)
        {
            var l = new ServicoRecursoFinanceiroCentroPOP();

            if (centroPOP.AtendeOutrosMunicipios)
            {
                var centroPopMunicpios = _repositoryCentroPOPMunicipio.GetAll().Where(s => s.IdCentroPop == centroPOP.Id);
                foreach (var item in centroPopMunicpios)
                {
                    _repositoryCentroPOPMunicipio.Delete(item);
                }
            }

            if (l.GetByCentroPOP(centroPOP.Id).Count() > 0)
                throw new Exception("Esse Centro POP possui serviços! Exclua primeiro os serviços para excluir o Centro POP.");

            var hasChangeAcoes = _repository.UpdateList<AcaoSocioAssistencialInfo>(GetById(centroPOP.Id), centroPOP.AcoesSocioAssistenciais, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.AcoesSocioAssistenciais);
            String descricao = "Excluído o Centro POP " + centroPOP.IDCREAS + " - " + centroPOP.Nome + ".";

            _repository.Delete(centroPOP);
            if (hasChangeAcoes)
            {
                if (centroPOP.UnidadePublica == null)
                    centroPOP.UnidadePublica = new UnidadePublica().GetById(centroPOP.IdUnidade);

                var log = Log.CreateLog(centroPOP.UnidadePublica.IdPrefeitura, EAcao.Remove, 32, descricao);
                if (log != null)
                    new Log().Add(log, true);

                var logCentroPop = LogCentroPop.CreateLog(log.Id, centroPOP.Id, centroPOP.UnidadePublica.Id, log.DataHorario);
                if (logCentroPop != null)
                    new LogCentroPop().Add(logCentroPop, true);
            }
            if (commit)
                ContextManager.Commit();
        }

        //public Int32 GetTotalRHByCentroPOP(Int32 idCentroPOP)
        //{
        //    return _repository.GetQuery().Where(t => t.Id == idCentroPOP).Select(t => t.TotalFuncionariosNivelFundamental + t.TotalFuncionariosNivelMedio + t.TotalFuncionariosSemEscolaridade + t.TotalFuncionariosSuperior).First();
        //}

        public List<String> GetLabelForInfo(List<String> propriedades, CentroPOPInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "Nome": labels.Add("nome"); break;
                    case "IDCREAS": labels.Add("código Identificador do Centro POP - IDCREAS"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "Email": labels.Add("e-mail institucional"); break;
                    case "Coordenador": labels.Add("coordenador"); break;
                    case "IdEscolaridadeCoordenador": if (obj.PossuiCoordenador) labels.Add("escolaridade do coordenador"); break;
                    case "IdFormacaoCoordenador":
                    case "OutraFormacaoCoordenador": if (obj.IdEscolaridadeCoordenador == 4 && obj.PossuiCoordenador) labels.Add("formação acadêmica do coordenador"); break;
                    case "NumeroAtendidos": labels.Add("previsão anual do número de pessoas atendidas"); break;
                    case "CapacidadeAtendimento": labels.Add("capacidade de atendimento anual"); break;
                    case "IdHorasSemana": labels.Add("quantidade de horas por semana o Centro POP funciona"); break;
                    case "QuantidadeDiasSemana": labels.Add("quantidade de dias por semana o Centro POP funciona"); break;
                    case "IdTipoImovel": labels.Add("tipo do imóvel"); break;
                    case "TotalFuncionarios":
                    case "TotalFuncionariosNivelFundamental":
                    case "TotalFuncionariosNivelMedio":
                    case "TotalFuncionariosSuperiorServicoSocial":
                    case "TotalFuncionariosSuperiorPsicologia":
                    case "TotalFuncionariosSuperiorPedagogia":
                    case "TotalFuncionariosSuperiorSociologia":
                    case "TotalFuncionariosSuperior":
                    case "TotalFuncionariosSuperiorPosGraduacao":
                    case "TotalFuncionariosSuperiorDireito":
                    case "TotalFuncionariosSemEscolaridade":
                    case "TotalEstagiarios":
                    case "TotalFuncionariosSuperiorAdministracao":
                    case "TotalFuncionariosSuperiorAntropologia":
                    case "TotalFuncionariosSuperiorContabilidade":
                    case "TotalFuncionariosSuperiorEconomia":
                    case "TotalFuncionariosSuperiorEconomiaDomestica":
                    case "TotalFuncionariosSuperiorTerapiaOcupacional":
                    case "TotalFuncionariosSuperiorMusicoterapia":
                        labels.Add("total de trabalhadores segundo a escolaridade"); break;
                    case "PossuiServicoEspecializadoSituacaoRua": labels.Add("o Centro POP oferta o Serviço Especializado para Pessoas em Situação de Rua"); break;
                    case "JustificativaServicoEspecializadoSituacaoRua": if (!obj.PossuiServicoEspecializadoSituacaoRua.Value) labels.Add("justificativa por não ofertar o Serviço Especializado para Pessoas em Situação de Rua"); break;
                    case "Desativado": labels.Add("Desativado"); break;
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
