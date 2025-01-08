using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Security.Permissions;
using System.ServiceModel;
using Seds.PMAS.QUADRIENAL.Negocio;
using Seds.PMAS.QUADRIENAL.UI.Processos;


namespace Seds.PMAS.QUADRIENAL.Pendencia
{
    public class VerificadorPendenciaPMAS
    {
        #region Propriedades
        private static List<int> Exercicios = new List<int> { 2022, 2023, 2024, 2025 };//Codigo de auxilio
        #endregion

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        [OperationBehavior(TransactionScopeRequired = true)]
        public ValidacaoPMASInfo ValidarPlanoMunicipalByPrefeitura(Int32 idPrefeitura, EPerfil perfil, Object dadosExtras = null)
        {
            ContextManager.OpenConnection();
            var pendencia = new VerificadorPendenciaPMAS().ValidarPlanoMunicipal(idPrefeitura, perfil, dadosExtras);
            ContextManager.CloseConnection();
            return pendencia;
        }

        public ValidacaoPMASInfo ValidarPlanoMunicipal(Int32 idPrefeitura, EPerfil perfil = 0, Object dadosExtras = null)
        {
            var pendencia = new ValidacaoPMASInfo();
            pendencia = ValidarBlocoI(idPrefeitura, pendencia, dadosExtras);

            pendencia = ValidarBlocoII(idPrefeitura, pendencia);

            pendencia = ValidarBlocoIII(idPrefeitura, pendencia, perfil);

            pendencia = ValidarBlocoIV(idPrefeitura, pendencia);

            pendencia = ValidarBlocoV(idPrefeitura, pendencia);

            pendencia = ValidarBlocoVI(idPrefeitura, pendencia);

            pendencia = ValidarBlocoVII(idPrefeitura, pendencia);

            if (perfil == EPerfil.CMAS)
            {
                pendencia = ValidarBlocoVIII(idPrefeitura, pendencia);
            }
            return pendencia;
        }

        #region pendencias
        public Boolean PlanoMunicipalPossuiPendencia(Int32 idPrefeitura, EPerfil perfil)
        {
            var pendencia = ValidarPlanoMunicipal(idPrefeitura, perfil);
            var ok = pendencia.InformacoesPrefeitura && pendencia.InformacoesPrefeito && pendencia.InformacoesOrgaoGestor && pendencia.InformacoesGestorMunicipal && pendencia.InformacoesFundoMunicipal && pendencia.InformacoesConselhosMunicipais
                && pendencia.RedeProtecaoSocialPublica && pendencia.RedeProtecaoSocialPrivada && pendencia.AnaliseInterpretacao
                && pendencia.ProgramasProjetos && pendencia.TransferenciaRenda && pendencia.BeneficiosContinuados && pendencia.BeneficiosEventuais
                && pendencia.AcoesPlanejadas && pendencia.FontesFinanciamento && pendencia.Educacao && pendencia.Saude && pendencia.SegurancaAlimentar && pendencia.Emprego && pendencia.OutrasPoliticas
                && pendencia.CronogramaDesembolsoProtecaoAlta && pendencia.CronogramaDesembolsoProtecaoBasica && pendencia.CronogramaDesembolsoProtecaoMedia && pendencia.CronogramaDesembolsoProgramaProjeto && pendencia.CronogramaDesembolsoBeneficioEventual
                && pendencia.Monitoramento && pendencia.VigilanciaSocioAssistencial && pendencia.Avaliacao && pendencia.AspectosGerais;
            return !ok;
        }

        public Boolean PlanoMunicipalPossuiPendenciaCMAS(Int32 idPrefeitura, EPerfil perfil)
        {
            var pendencia = ValidarPlanoMunicipal(idPrefeitura, perfil);
            var ok = pendencia.InformacoesPrefeitura && pendencia.InformacoesPrefeito && pendencia.InformacoesOrgaoGestor && pendencia.InformacoesGestorMunicipal && pendencia.InformacoesFundoMunicipal && pendencia.InformacoesConselhosMunicipais
                && pendencia.RedeProtecaoSocialPublica && pendencia.RedeProtecaoSocialPrivada && pendencia.AnaliseInterpretacao
                && pendencia.ProgramasProjetos && pendencia.TransferenciaRenda && pendencia.BeneficiosContinuados && pendencia.BeneficiosEventuais
                && pendencia.AcoesPlanejadas && pendencia.FontesFinanciamento && pendencia.Educacao && pendencia.Saude && pendencia.SegurancaAlimentar && pendencia.Emprego && pendencia.OutrasPoliticas
                && pendencia.CronogramaDesembolsoProtecaoAlta && pendencia.CronogramaDesembolsoProtecaoBasica && pendencia.CronogramaDesembolsoProtecaoMedia && pendencia.CronogramaDesembolsoProgramaProjeto && pendencia.CronogramaDesembolsoBeneficioEventual
                && pendencia.Monitoramento && pendencia.VigilanciaSocioAssistencial && pendencia.Avaliacao && pendencia.AspectosGerais
                && pendencia.ConselhoMunicipal && pendencia.SituacaoInscricaoCMAS && pendencia.SituacaoInscricaoAtualCMAS && pendencia.SituacaoInscricaoAtualCMAS && pendencia.DataInscricaoCMAS && pendencia.InscricaoCMAS;
            return !ok;

        }

        public Boolean PlanoMunicipalPossuiPendenciaOrgaoGestor(Int32 idPrefeitura, EPerfil perfil)
        {
            var pendencia = ValidarPlanoMunicipal(idPrefeitura, perfil);

            var ok = pendencia.InformacoesPrefeitura && pendencia.InformacoesPrefeito && pendencia.InformacoesOrgaoGestor && pendencia.InformacoesGestorMunicipal && pendencia.InformacoesFundoMunicipal && pendencia.InformacoesConselhosMunicipais
                && pendencia.RedeProtecaoSocialPublica && pendencia.RedeProtecaoSocialPrivada && pendencia.AnaliseInterpretacao
                && pendencia.ProgramasProjetos && pendencia.TransferenciaRenda && pendencia.BeneficiosContinuados && pendencia.BeneficiosEventuais
                && pendencia.AcoesPlanejadas && pendencia.Educacao && pendencia.Saude && pendencia.SegurancaAlimentar && pendencia.Emprego && pendencia.OutrasPoliticas
                && pendencia.CronogramaDesembolsoProtecaoAlta && pendencia.CronogramaDesembolsoProtecaoBasica && pendencia.CronogramaDesembolsoProtecaoMedia && pendencia.CronogramaDesembolsoProgramaProjeto && pendencia.CronogramaDesembolsoBeneficioEventual
                && pendencia.Monitoramento && pendencia.VigilanciaSocioAssistencial && pendencia.Avaliacao && pendencia.AspectosGerais;

            return !ok;
        }
        #endregion

        #region blocos

        public ValidacaoPMASInfo ValidarBlocoI(Int32 idPrefeitura, ValidacaoPMASInfo pendencia, Object dadosExtras = null)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();
            pendencia.Alertas = pendencia.Alertas ?? new List<string>();

            #region Prefeitura
            var prefeitura = new Prefeitura().GetById(idPrefeitura);
            pendencia.InformacoesPrefeitura = true;
            if (prefeitura == null)
            {
                pendencia.Pendencias.Add("I - Identificação da Prefeitura Municipal: Não existe Informações sobre a Prefeitura Municipal");
                pendencia.InformacoesPrefeitura = false;
            }
            else
            {
                try
                {
                    new ValidadorPrefeitura().Validar(prefeitura);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesPrefeitura = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Identificação da Prefeitura Municipal: " + s);
                }
            }
            #endregion

            #region Prefeito
            var prefeito = new Prefeito().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesPrefeito = true;
            if (prefeito == null)
            {
                pendencia.Pendencias.Add("I - Identificação do Prefeito: Não existe Cadastro na Identificação do Prefeito em Exercício");
                pendencia.InformacoesPrefeito = false;
            }
            else
            {
                try
                {
                    new ValidadorPrefeito().Validar(prefeito);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesPrefeito = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Identificação do Prefeito: " + s);
                }
            }
            #endregion

            #region Orgao Gestor
            var orgao = new OrgaoGestor().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesOrgaoGestor = true;

            if (orgao == null)
            {
                pendencia.Pendencias.Add("I - Identificação do Órgão Gestor: Não existe Cadastro nas Informações sobre o Órgão Gestor");
                pendencia.InformacoesOrgaoGestor = false;
            }
            else
            {
                try
                {
                    new ValidadorOrgaoGestor().ValidarOrgaoPendencias(orgao);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesOrgaoGestor = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Estrutura e Recursos Humanos do órgão gestor: " + s);
                }

            }
            #endregion

            #region Informações sobre o Gestor Municipal
            //new ValidadorGestorMunicipal().Validar(gestor);
            var gestormunicipal = new GestorMunicipal().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesGestorMunicipal = true;
            if (gestormunicipal == null)
            {
                pendencia.Pendencias.Add("I - Identificação do Gestor Municipal: Não existe Cadastro nas Informações sobre o Gestor Municipal");
                pendencia.InformacoesGestorMunicipal = false;
            }
            else
            {
                try
                {
                    new ValidadorGestorMunicipal().Validar(gestormunicipal);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesGestorMunicipal = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Identificação do Gestor Municipal: " + s);
                }
            }

            #endregion

            //#region Gestor Municipal Equipe
            //using (var proxy = new ProxyPrefeitura())
            //{
            //    var prefeituras = new Prefeituras(proxy);
            //    OrgaoGestorInfo orgaoGestorInfo = prefeituras.GetOrgaoGestor(idPrefeitura);
            //    new ValidadorOrgaoGestor().ValidarOrgaoIdentificacao(orgaoGestorInfo);

            //    new ValidadorOrgaoGestor().ValidarOrgaoPendencias(orgaoGestorInfo);

            //}
            //#endregion0,

            #region Informações sobre o Fundo Municipal

            var fmas = new FundoMunicipal().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesFundoMunicipal = true;
            if (fmas == null)
            {
                pendencia.Pendencias.Add("I - Identificação do Fundo Municipal: Não existe Cadastro nas Informações sobre o Fundo Municipal");
                pendencia.InformacoesFundoMunicipal = false;
            }
            else
            {
                try
                {
                    //PREENCHER

                }
                catch (Exception ex)
                {
                    pendencia.InformacoesFundoMunicipal = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Identificação do Fundo Municipal: " + s);
                }
            }



            #endregion


            #region Informações sobre o Gestor Municipal
            //new ValidadorGestorMunicipal().Validar(gestor);
            var gestorfundomunicipal = new GestorFundoMunicipal().GetByPrefeitura(idPrefeitura);
            pendencia.InformacoesGestorMunicipal = true;
            if (gestorfundomunicipal == null)
            {
                pendencia.Pendencias.Add("I - Identificação do Gestor do Fundo Municipal: Não existe Cadastro nas Informações sobre o Gestor do Fundo Municipal");
                pendencia.InformacoesGestorMunicipal = false;
            }
            else
            {
                try
                {
                    new ValidadorGestorFundoMunicipal().Validar(gestorfundomunicipal);
                }
                catch (Exception ex)
                {
                    pendencia.InformacoesGestorMunicipal = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("I - Identificação do Gestor do Fundo Municipal: " + s);
                }
            }

            #endregion


            #region Informações sobre os Conselhos Municipais

            var conselhos = new ConselhoExistente().GetByPrefeitura(idPrefeitura).ToList();
            pendencia.InformacoesConselhosMunicipais = true;
            if (conselhos.Count == 0)
            {
                pendencia.Alertas.Add("I - Informações sobre os Conselhos existentes: Não foi cadastrado nenhum outro Conselho referente à área de Assistência Social.");
            }
            else
            {
                foreach (var c in conselhos)
                {
                    try
                    {
                        new ValidadorConselhoExistente().Validar(c);
                    }
                    catch (Exception ex)
                    {
                        pendencia.InformacoesConselhosMunicipais = false;
                        foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                            pendencia.Pendencias.Add("I - Informações sobre os Conselhos existentes: " + s);
                    }
                }
            }
            #endregion

            return pendencia;
        }

        public ValidacaoPMASInfo ValidarBlocoII(Int32 idPrefeitura, ValidacaoPMASInfo pendencia)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            #region Análise Diagnóstica

            var analise = new AnaliseDiagnostica().GetByPrefeitura(idPrefeitura).Where(s => s.Exercicio >= 2022).OrderBy(t => t.Classificacao).ToList();
                       
            var prefeitura = new Prefeitura().GetById(idPrefeitura);

            int exercicio = 0;

            try
            {
                exercicio = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 75 && x.Desbloqueado == true).FirstOrDefault().Exercicio;
            }
            catch
            {
                exercicio = 0;
            }


            int idExercicio = 0;

            if (exercicio == 2022)
            {
                idExercicio = 5;
            }
            else if (exercicio == 2023)
            {
                idExercicio = 6;
            }
            else if (exercicio == 2024)
            {
                idExercicio = 7;
            }
            else if (exercicio == 2025)
            {
                idExercicio = 8;
            }


            var situacao = new AnaliseDiagnosticaComunidade().GetByPrefeituraExercicio(idPrefeitura, idExercicio);

            var vunerabilidades = analise.Where(s => s.Exercicio == exercicio);


            if (situacao == null)
            {
                pendencia.Pendencias.Add("II – Situações de Vulnerabilidade – Devem ser indicadas a existência ou não de povos, comunidades tradicionais e/ou grupos específicos.");
            }

            if (vunerabilidades.Count() == 0)
            {
                pendencia.Pendencias.Add("II – Situações de Vulnerabilidade – Deve ser apresentada uma classificação para as situações de vulnerabilidade e/ou risco social.");
            }



            if (prefeitura != null)
            {
                try
                {
                    pendencia.TerritorioDemografia = true;
                    if (String.IsNullOrEmpty(prefeitura.Caracterizacao))
                    {
                        pendencia.Pendencias.Add("II - Território Demografia: O preenchimento do campo de caracterização do município em relação a território e demografia é obrigatório!");
                        pendencia.TerritorioDemografia = false;
                    }

                    pendencia.PopulacaoVulnerabilidade = true;
                    if (String.IsNullOrEmpty(prefeitura.CaracterizacaoPopulacao))
                    {
                        pendencia.Pendencias.Add("II - Território Demografia: O preenchimento do campo de caracterização do município em relação a população e vulnerabilidade social é obrigatório!");
                        pendencia.PopulacaoVulnerabilidade = false;
                    }

                    pendencia.EvolucaoRedeAtendimento = true;
                    if (String.IsNullOrEmpty(prefeitura.CaracterizacaoRedeSocioassistencial))
                    {
                        pendencia.Pendencias.Add("II - Território Demografia: O preenchimento do campo de caracterização do município em relação à evolução da rede de atendimento é obrigatório!");
                        pendencia.EvolucaoRedeAtendimento = false;
                    }

                    //new ValidadorPrefeitura().ValidarCaracterizacao(prefeitura);
                }
                catch (Exception ex)
                {
                    // pendencia.AnaliseDiagnostica = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("II - Território Demografia: " + s);
                }
            }

            pendencia.SituacoesVulnerabilidade = true;

            var comunidade = new AnaliseDiagnosticaComunidade().GetByPrefeitura(idPrefeitura, idExercicio);
            if (comunidade != null)
            {
                try
                {
                    new ValidadorAnaliseDiagnostica().ValidarComunidade(comunidade);
                }
                catch (Exception ex)
                {
                    pendencia.SituacoesVulnerabilidade = false;
                    pendencia.Pendencias.Add("II - Situações de Vulnerabilidade : " + ex.Message.ToString());
                }
            }
            else
            {
                pendencia.Pendencias.Add("II - Situações de Vulnerabilidade : Não há registro de povos, comunidades ou grupos específicos no município");
            }

            if (analise.Count == 0)
            {
                pendencia.Pendencias.Add("II - Situações de vulnerabilidade e/ou risco existentes no município : Não existe Cadastro da Análise Diagnóstica");
                pendencia.SituacoesVulnerabilidade = false;
            }
            else
            {

                try
                {
                    new ValidadorPrefeitura().ValidarCaracterizacao(prefeitura);
                }
                catch (Exception ex)
                {
                    pendencia.AnaliseInterpretacao = false;
                    foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        pendencia.Pendencias.Add("II - Análise e Interpretação : " + s);
                }

                /*for (int i = 0; i < analise.Count; i++)
                {
                    if (analise[i].Classificacao != (i + 1))
                    {
                        pendencia.SituacoesVulnerabilidade = false;
                        pendencia.Pendencias.Add("II - Situações de Vulnerabilidade : A sequência de classificação das vulnerabilidades estão incorretas");
                        break;
                    }
                }*/
            }


            pendencia.AnaliseInterpretacao = true;
            try
            {
                new ValidadorPrefeitura().ValidarCaracterizacaoAnaliseInterpretacao(prefeitura);
            }
            catch (Exception ex)
            {

                pendencia.Pendencias.Add("II - Análise e interpretação : Não existe Cadastro da Análise e interpretação");
                pendencia.AnaliseInterpretacao = false;
            }


            #region Atualizações Anuais

            try
            {

            }
            catch (Exception ex)
            {
                pendencia.Pendencias.Add("II - Atualizações Anuais : Não existe Cadastro da Atualização realizada no 2º semestre");
                pendencia.AnaliseInterpretacao = false;

            }

            #endregion
            #endregion


            return pendencia;
        }

        public ValidacaoPMASInfo ValidarBlocoIII(Int32 idPrefeitura, ValidacaoPMASInfo pendencia, EPerfil perfil)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            #region Rede Direta
            var unidadesPublicas = new UnidadePublica().GetByPrefeitura(idPrefeitura).Where(c => c.Desativado != true);
            pendencia.RedeProtecaoSocialPublica = true;
            pendencia.CRAS = true;
            pendencia.CREAS = true;
            pendencia.CentroPOP = true;
            int totalLocais = 0;

            var prefeitura = new Prefeitura().GetById(idPrefeitura);

            //int exercicio = (Int32)prefeitura

            foreach (var unidade in unidadesPublicas)
            {

                totalLocais = ValidarServicosCRAS(pendencia, totalLocais, unidade);
                totalLocais = ValidarServicosCREAS(pendencia, totalLocais, unidade);
                totalLocais = ValidarServicosCentroPOP(pendencia, totalLocais, unidade);
                totalLocais = ValidarServicoPublico(pendencia, totalLocais, unidade, prefeitura);
                
                if (pendencia.RedeProtecaoSocialPublica && totalLocais == 0)
                {
                    pendencia.Pendencias.Add("III - Rede Proteção Social Pública - Unidade Pública " + unidade.RazaoSocial + " não possui locais de execução");
                    pendencia.RedeProtecaoSocialPublica = false;
                }
            }




            
            /*var beneficiosEventuais = new PrefeituraBeneficioEventual().GetByPrefeitura(idPrefeitura);
            
            foreach (var item in beneficiosEventuais)
            {
                totalLocais = ValidarBeneficiosEventuais(pendencia, item);    
            }*/
            
            
            
            #endregion

            #region Rede Indireta

            var unidadesPrivadas = new UnidadePrivada().GetByPrefeitura(idPrefeitura).Where(c => c.Desativado != true);

            pendencia.RedeProtecaoSocialPrivada = true;
            pendencia.SituacaoInscricaoCMAS = true;
            pendencia.InscricaoCMAS = true;
            pendencia.SituacaoInscricaoAtualCMAS = true;
            pendencia.DataInscricaoCMAS = true;



            foreach (var unidade in unidadesPrivadas)
            {

                #region Validacao do Perfil CMAS
                if (perfil == EPerfil.CMAS)
                {
                    if (!unidade.IdSituacaoInscricao.HasValue)
                    {
                        pendencia.Pendencias.Add("III - Rede Indireta - Unidade Privada " + unidade.RazaoSocial + " deve ter sua situação de inscrição informada");
                        pendencia.SituacaoInscricaoCMAS = false;
                        continue;
                    }
                    if (unidade.IdSituacaoInscricao == 1)
                    {
                        if (string.IsNullOrEmpty(unidade.InscricaoCMAS) || !unidade.IdSituacaoAtualInscricao.HasValue || Convert.ToDateTime(unidade.DataPublicacao) <= DateTime.MinValue)
                        {
                            pendencia.Pendencias.Add("III - Rede Indireta - Unidade Privada " + unidade.RazaoSocial + " todos os campos sobre a inscrição da Unidade devem ser preenchidos!");
                            pendencia.InscricaoCMAS = false;
                            pendencia.SituacaoInscricaoAtualCMAS = false;
                            pendencia.DataInscricaoCMAS = false;
                            pendencia.SituacaoInscricaoCMAS = false;
                            continue;
                        }
                    }
                    if (unidade.IdSituacaoInscricao == 2)
                    {
                        if (!unidade.DataPublicacao.HasValue)
                        {
                            pendencia.Pendencias.Add("III - Rede Indireta - Unidade Privada " + unidade.RazaoSocial + " o campo data da inscrição deve ser preenchido!");
                            pendencia.DataInscricaoCMAS = false;
                            pendencia.SituacaoInscricaoCMAS = false;
                            continue;
                        }
                    }
                }
                #endregion

                #region Listar os locais
                var locais = new LocalExecucaoPrivado().GetByUnidade(unidade.Id).Where(c => c.Desativado != true);
                #endregion

                #region Valida os locais vazios

                if (locais.Count() == 0)
                {
                    bool atendimento = false;

                    bool ofertaServico = false;

                    if (unidade.FormasAtuacoes.Count > 0)
                    {
                        foreach (var a in unidade.FormasAtuacoes)
                        {
                            if (a.Id == 1)
                            {
                                atendimento = true;
                            }
                        }

                        if (atendimento)
                        {
                            foreach (var u in unidade.CaracterizacaoAtividades)
                            {
                                if (u.Id == 1)
                                {
                                    ofertaServico = true;
                                }
                            }

                            if (ofertaServico)
                            {
                                pendencia.Pendencias.Add("III - Rede Indireta - Unidade Privada " + unidade.RazaoSocial + ", quando o Tipo de Atendimento da unidade se referir a Oferta de serviços socioassistenciais deve ser informado ao menos um local de execução.");
                                pendencia.RedeProtecaoSocialPrivada = false;
                                continue;
                            }
                        }
                    }
                }
                #endregion

                #region Listar os servicos dos locais

                foreach (var local in locais)
                {
                    var servicos = new ServicoRecursoFinanceiroPrivado().GetByLocalExecucao(local.Id).Where(c => c.Desativado != true);

                    if (servicos.Count() == 0)
                    {
                        #region cria uma pendencia para formas especificas
                        bool seNaoExisteFormaDeAtuacao = !unidade.IdFormaAtuacao.HasValue;
                        bool seExistirFormaDeAtuacaoTipoAtendimento = (unidade.IdFormaAtuacao.HasValue && (unidade.IdFormaAtuacao.Value == 1) && unidade.Desativado != true);
                        if (seNaoExisteFormaDeAtuacao || seExistirFormaDeAtuacaoTipoAtendimento)
                        {
                            pendencia.Pendencias.Add(
                                String.Concat("III - Rede Indireta - Unidade Privada "
                                                    , unidade.RazaoSocial
                                                    , ": Local de Execução "
                                                    , local.Nome
                                                    , " não possui serviços e recursos financeiros cadastrados"));
                            pendencia.RedeProtecaoSocialPrivada = false;
                        }
                        #endregion
                    }
                    else
                    {
                        foreach (var servico in servicos)
                        {
                            if (servico.PossuiProgramaBeneficio == true)
                            {
                                try
                                {
                                    new ServicoRecursoFinanceiroPrivado().ValidarServicoPrivado(servico);
                                    List<ConsultaProgramaProjetoServicoCofinanciamentoFundosInfo> programaBeneficio = new ProgramaProjetoCofinanciamento().GetProgramaProjetoCofinanciamentoByIdServicosRecursosFinanceirosFundos(servico.Id, local.Id);

                                    if (programaBeneficio.Count <= 0)
                                    {
                                        pendencia.Pendencias.Add("III - Rede Indireta  " + unidade.RazaoSocial + " não possui programas associados.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    pendencia.RedeProtecaoSocialPrivada = false;

                                    
                                    
                                    if (servico.IdUsuarioTipoServico == 37 && servico.UsuarioTipoServico.IdTipoServico == 146
                                        && (servico.AtendeCriancasAuxilioReclusao == null || servico.AtendeCriancasPensaoMorte == null)
                                        && (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == null && servico.AtendeCriancasPensaoMorteExercicio2025 == null)
                                        )
                                    {
                                        pendencia.Pendencias.Add("III - Rede Indireta "
                                            + unidade.RazaoSocial + ": Local de Execução "
                                            + local.Nome + ": "
                                            + servico.UsuarioTipoServico.TipoServico.Nome
                                            + " - " + servico.UsuarioTipoServico.Nome + " - Caracterização do Usuário: " + ex.Message);
                                    }
                                    else
                                    {
                                        pendencia.Pendencias.Add("III - Rede Indireta "
                                            + unidade.RazaoSocial + ": Local de Execução "
                                            + local.Nome + ": "
                                            + servico.UsuarioTipoServico.TipoServico.Nome
                                            + " - " + servico.UsuarioTipoServico.Nome + " - FUNCIONAMENTO: " + ex.Message);
                                    }

                                    
                                }
                            }
                            else
                            {
                                try
                                {
                                    new ServicoRecursoFinanceiroPrivado().ValidarServicoPrivado(servico);
                                }
                                catch (Exception ex)
                                {
                                    pendencia.RedeProtecaoSocialPrivada = false;


                                    if (servico.IdUsuarioTipoServico == 37 && servico.UsuarioTipoServico.IdTipoServico == 146
                                        && (servico.AtendeCriancasAuxilioReclusao == null || servico.AtendeCriancasPensaoMorte == null)
                                        && (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == null && servico.AtendeCriancasPensaoMorteExercicio2025 == null)
                                        )
                                    {
                                        pendencia.Pendencias.Add("III - Rede Indireta "
                                         + unidade.RazaoSocial + ": Local de Execução "
                                         + local.Nome + ": "
                                         + servico.UsuarioTipoServico.TipoServico.Nome
                                         + " - " + servico.UsuarioTipoServico.Nome + " - Caracterização do Usuário: " + ex.Message);
                                    }
                                    else
                                    {
                                        pendencia.Pendencias.Add("III - Rede Indireta "
                                         + unidade.RazaoSocial + ": Local de Execução "
                                         + local.Nome + ": "
                                         + servico.UsuarioTipoServico.TipoServico.Nome
                                         + " - " + servico.UsuarioTipoServico.Nome + " - FUNCIONAMENTO: " + ex.Message);
                                    }	



                                }
                            }
                        }
                    }

                }
                #endregion
            }
            #endregion

            #region Programas / Projetos


            var programas = new ProgramaProjeto().GetByPrefeitura(idPrefeitura);


            pendencia.ProgramasProjetos = true;
            if (programas.Count() == 0)
            {
                pendencia.Alertas.Add("III : Não existe Cadastro de Programas / Projetos");
            }
            else
            {
                foreach (var programa in programas)
                {
                    var servicos = new ProgramaProjetoCofinanciamento().GetByProgramaProjeto(programa.Id);
                    if (programa.BeneficiarioAtendidoRedeSocioassistencial == true && servicos.Count() == 0)
                    {
                        pendencia.Pendencias.Add("III - Programas/Projetos: " + programa.Nome + " não possui serviços vinculados");
                        continue;
                    }
                    else
                    {
                        if (servicos.Count() == 0)
                        {
                            pendencia.Alertas.Add("III - Programas/Projetos: " + programa.Nome + " não possui serviços vinculados");
                            continue;
                        }
                        else if (programa.BeneficiarioAtendidoRedeSocioassistencial == false)
                        {
                            pendencia.Alertas.Add("III - Programas/Projetos: " + programa.Nome + " não possui serviços vinculados");
                            continue;
                        }
                    }

                    var Pactuada = 0;

                    foreach (var servico in servicos)
                    {
                        if (servico.NumeroUsuarios.HasValue)
                        {
                            var desativado = false;
                            if (servico.IdServicosRecursosFinanceirosCentroPOP.HasValue)
                            {
                                desativado = new ServicoRecursoFinanceiroCentroPOP().GetById(servico.IdServicosRecursosFinanceirosCentroPOP.Value).Desativado;
                            }
                            if (servico.IdServicosRecursosFinanceirosCRAS.HasValue)
                            {
                                desativado = new ServicoRecursoFinanceiroCRAS().GetById(servico.IdServicosRecursosFinanceirosCRAS.Value).Desativado;
                            }
                            if (servico.IdServicosRecursosFinanceirosCREAS.HasValue)
                            {
                                desativado = new ServicoRecursoFinanceiroCREAS().GetById(servico.IdServicosRecursosFinanceirosCREAS.Value).Desativado;
                            }
                            if (servico.IdServicosRecursosFinanceirosPublico.HasValue)
                            {
                                desativado = new ServicoRecursoFinanceiroPublico().GetById(servico.IdServicosRecursosFinanceirosPublico.Value).Desativado;
                            }
                            if (servico.IdServicosRecursosFinanceirosPrivado.HasValue)
                            {
                                desativado = new ServicoRecursoFinanceiroPrivado().GetById(servico.IdServicosRecursosFinanceirosPrivado.Value).Desativado;
                            }

                            var previsaoAnual = new ProgramaProjetoPrevisaoAnualBeneficiarios().GetByProgramaProjeto(programa.Id);

                            //DBM:: VALIDAR? ACERTAR LABEL??
                            var projetoProgramaExercicios = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 23);
                            var ppValidarExercicio1 = true;
                            var ppValidarExercicio2 = true;
                            var ppValidarExercicio3 = true;
                            var ppValidarExercicio4 = true;

                            foreach (var projetoProgramaExercicio in projetoProgramaExercicios)
                            {
                                if (projetoProgramaExercicio.Exercicio == 2022)
                                {
                                    ppValidarExercicio1 = projetoProgramaExercicio.Desbloqueado.Value;
                                }
                                if (projetoProgramaExercicio.Exercicio == 2023)
                                {
                                    ppValidarExercicio2 = projetoProgramaExercicio.Desbloqueado.Value;
                                }
                                if (projetoProgramaExercicio.Exercicio == 2024)
                                {
                                    ppValidarExercicio3 = projetoProgramaExercicio.Desbloqueado.Value;
                                }
                                if (projetoProgramaExercicio.Exercicio == 2025)
                                {
                                    ppValidarExercicio4 = projetoProgramaExercicio.Desbloqueado.Value;
                                }
                            }

                            //Validando meta compactuada para cada ano
                            if (ppValidarExercicio1)
                            {
                                if (previsaoAnual != null)
                                {
                                    Pactuada = previsaoAnual.MetaPactuadaExercicio1;
                                }
                            }

                            if (ppValidarExercicio2)
                            {
                                if (previsaoAnual != null)
                                {
                                    Pactuada = previsaoAnual.MetaPactuadaExercicio2;
                                }
                            }

                            if (ppValidarExercicio3)
                            {
                                if (previsaoAnual != null)
                                {
                                    Pactuada = previsaoAnual.MetaPactuadaExercicio3;
                                }
                            }

                            if (ppValidarExercicio4)
                            {
                                if (previsaoAnual != null)
                                {
                                    Pactuada = previsaoAnual.MetaPactuadaExercicio4;
                                }
                            }

                            var progMunicipio = false;

                            if (programa.ProgramaMunicipal.HasValue)
                            {
                                if (programa.ProgramaMunicipal.Value)
                                {
                                    progMunicipio = true;
                                }
                            }

                            if (programa.Nome.ToLower() != "são paulo amigo do idoso" && programa.Nome.ToUpper() != "PROGRAMA CRIANÇA FELIZ")
                            {
                                if (ppValidarExercicio1 || ppValidarExercicio2 || ppValidarExercicio3 || ppValidarExercicio4)
                                {
                                    if (servico.NumeroUsuarios.Value > Pactuada && !progMunicipio)
                                    {
                                        if (!desativado)
                                        {
                                            var usuario = new UsuarioTipoServico().GetById(servico.Servico.UsuarioTipoServico.Id);
                                            //O número de usuários do serviço atendidos nesse programa não pode ser maior que o número total de atendidos pelo serviço!
                                            var RazaoSocialUnidade = "Rede Direta ";
                                            if (servico.ServicosRecursosFinanceirosPublico != null)
                                            {
                                                RazaoSocialUnidade = servico.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.RazaoSocial;
                                            }
                                            if (servico.ServicosRecursosFinanceirosPrivado != null)
                                            {
                                                RazaoSocialUnidade = servico.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.RazaoSocial;
                                            }
                                            pendencia.Pendencias.Add("III - Programas e projetos - "
                                                + programa.Nome + " : "
                                                + usuario.TipoServico.Nome
                                                + " - " + usuario.Nome
                                                + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                                        }
                                    }
                                }
                            }
                        }
                    }



                }
            }
            #endregion

            #region Transferência de Renda
            var transferenciasFederais = new TransferenciaRenda().GetProgramasFederaisByPrefeitura(idPrefeitura);
            pendencia.TransferenciaRenda = true;

            PETIIndicadores peti = new PETIIndicadores();

            foreach (var t in transferenciasFederais)
            {
                if (!new TransferenciaRenda().ProgramaAderido(t))
                    continue;

                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                var totalServicos = servicos.Count();
                if (t.BeneficiarioAtendidoRedeSocioAssistencial && totalServicos == 0)
                {
                    pendencia.Pendencias.Add("III - Programas e projetos  - " + t.Nome + " não possui serviços vinculados");
                    pendencia.ProgramasProjetos = false;
                    continue;
                }
                else if (totalServicos == 0)
                {
                    pendencia.Alertas.Add("III - Programas e projetos  - " + t.Nome + " não possui serviços vinculados");
                    continue;
                }

                t.PetiIndicadores = peti.GetByMunicipio(t.Prefeitura.IdMunicipio);
                               
                var e = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 25 && x.Desbloqueado == true).FirstOrDefault();
                
                Int32 exercicio = 0;

                if (e != null)
                {
                    exercicio = (Int32)e.Exercicio;
                
                
                var numeroBeneficiarios = new TransferenciaRenda().GetNumeroBeneficiarios(t, exercicio);

                foreach (var s in servicos)
                {

                    var RazaoSocialUnidade = "Unidade Pública ";
                    if (s.ServicosRecursosFinanceirosPublico != null && s.ServicosRecursosFinanceirosPublico.Desativado != true)
                        RazaoSocialUnidade = s.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.RazaoSocial;
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.ServicosRecursosFinanceirosPrivado.Desativado != true)
                        RazaoSocialUnidade = new LocalExecucaoPrivado().GetById(s.ServicosRecursosFinanceirosPrivado.IdLocalExecucao).Nome;   // s.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.RazaoSocial;

                    //if (s.ServicosRecursosFinanceirosPrivado != null && s.ServicosRecursosFinanceirosPrivado.Desativado != true && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.PrevisaoMensalNumeroAtendidos)//  + s.ServicosRecursosFinanceirosPrivado.PrevisaoAnualNumeroAtendidos.ServicosRecursosFinanceirosPrivado.PrevisaoAnualNumeroAtendidos)
                    //{
                    //    var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);

                    //    pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                    //    // pendencia.TransferenciaRenda = false;
                    //    pendencia.ProgramasProjetos = false;
                    //    continue;
                    //}
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.ServicosRecursosFinanceirosPrivado.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }

                    //if (s.ServicosRecursosFinanceirosPublico != null && s.ServicosRecursosFinanceirosPublico.Desativado != true && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.NumeroAtendidosMensalServico + s.ServicosRecursosFinanceirosPublico.PrevisaoMensalNumeroAtendidos)
                    //{
                    //    var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                    //    pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                    //    // pendencia.TransferenciaRenda = false;
                    //    pendencia.ProgramasProjetos = false;
                    //    continue;
                    //}
                    if (s.ServicosRecursosFinanceirosPublico != null && s.ServicosRecursosFinanceirosPublico.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }

                    if (s.ServicosRecursosFinanceirosCRAS != null && s.ServicosRecursosFinanceirosCRAS.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0) //s.ServicosRecursosFinanceirosCRAS.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCRAS.PrevisaoMensalNumeroAtendidos)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.ServicosRecursosFinanceirosCRAS.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.ServicosRecursosFinanceirosCREAS.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0) // s.ServicosRecursosFinanceirosCREAS.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCREAS.PrevisaoMensalNumeroAtendidos
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.ServicosRecursosFinanceirosCREAS.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.ServicosRecursosFinanceirosCentroPOP.Desativado != true && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCentroPOP.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCentroPOP.PrevisaoMensalNumeroAtendidos && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.ServicosRecursosFinanceirosCentroPOP.Desativado != true && s.NumeroUsuarios > numeroBeneficiarios && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 1
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                }
                }

            }

            var transferenciasEstaduais = new TransferenciaRenda().GetProgramasEstaduaisByPrefeitura(idPrefeitura);
            foreach (var t in transferenciasEstaduais)
            {
                if (!new TransferenciaRenda().ProgramaAderido(t))
                    continue;
                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                var totalServicos = servicos.Count();
                if (t.BeneficiarioAtendidoRedeSocioAssistencial && totalServicos == 0)
                {
                    pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " não possui serviços vinculados");
                    // pendencia.TransferenciaRenda = false;
                    pendencia.ProgramasProjetos = false;
                    continue;
                }
                else if (totalServicos == 0)
                {
                    pendencia.Alertas.Add("III - Transferência de Renda - " + t.Nome + " não possui serviços vinculados");
                    continue;
                }

                var e = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 25 && x.Desbloqueado == true).FirstOrDefault();

                Int32 exercicio = 0;

                if (e != null)
                {
                    exercicio = (Int32)e.Exercicio;
                
                    //var numeroBeneficiarios = new TransferenciaRenda().GetNumeroBeneficiarios(t, exercicio);

                foreach (var s in servicos)
                {
                    var numeroBeneficiarios = new TransferenciaRenda().GetNumeroBeneficiarios(t, exercicio);

                    var RazaoSocialUnidade = "Unidade Pública ";
                    if (s.ServicosRecursosFinanceirosPublico != null)
                    {
                        if (s.ServicosRecursosFinanceirosPublico.LocalExecucao != null)
                        {
                            RazaoSocialUnidade = s.ServicosRecursosFinanceirosPublico.LocalExecucao.Unidade.RazaoSocial;
                        }
                        else
                        {
                            RazaoSocialUnidade = "";
                        }
                       
                    }

                    if (s.ServicosRecursosFinanceirosPrivado != null)
                        RazaoSocialUnidade = RazaoSocialUnidade = new LocalExecucaoPrivado().GetById(s.ServicosRecursosFinanceirosPrivado.IdLocalExecucao).Nome;
                    //s.ServicosRecursosFinanceirosPrivado.LocalExecucao.Unidade.RazaoSocial;

                    //if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.PrevisaoAnualNumeroAtendidos)
                    //{
                    //    var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                    //    pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                    //    // pendencia.TransferenciaRenda = false;
                    //    pendencia.ProgramasProjetos = false;
                    //    continue;
                    //}
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosPrivado.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    //if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.PrevisaoAnualNumeroAtendidos)
                    //{
                    //    var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                    //    pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                    //    //pendencia.TransferenciaRenda = false;
                    //    pendencia.ProgramasProjetos = false;
                    //    continue;
                    //}
                    if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosPublico.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }

                    //if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCRAS.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosCRAS.Desativado != true && exercicio != 0)
                    //{
                    //    var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                    //    pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                    //    //pendencia.TransferenciaRenda = false; 2
                    //    pendencia.ProgramasProjetos = false;
                    //    continue;
                    //}
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCRAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCREAS.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCREAS.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosCREAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCREAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        //pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCentroPOP.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCentroPOP.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + " (" + RazaoSocialUnidade + "): O registro do número de usuários deste serviço que são atendidos pelo programa citado ultrapassa o número total de beneficiários deste programa!");
                        // pendencia.TransferenciaRenda = false; 2
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                }
                }
            }

            var transferenciasMunicipais = new TransferenciaRenda().GetProgramasMunicipaisByPrefeitura(idPrefeitura);

            foreach (var t in transferenciasMunicipais)
            {
                if (!new TransferenciaRenda().ProgramaAderido(t))
                    continue;
                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                var totalServicos = servicos.Count();
                if (t.BeneficiarioAtendidoRedeSocioAssistencial && totalServicos == 0)
                {
                    pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " não possui serviços vinculados");
                    //pendencia.TransferenciaRenda = false;
                    pendencia.ProgramasProjetos = false;
                    continue;
                }
                else if (totalServicos == 0)
                {
                    pendencia.Alertas.Add("III - Transferência de Renda - " + t.Nome + " não possui serviços vinculados");
                    continue;
                }

                var e = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 25 && x.Desbloqueado == true).FirstOrDefault();

                Int32 exercicio = 0;

                if (e != null)
                {
                    exercicio = (Int32)e.Exercicio;
                

                var numeroBeneficiarios = new TransferenciaRenda().GetNumeroBeneficiarios(t, exercicio);

                foreach (var s in servicos)
                {

                    if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosPrivado.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        //pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosPrivado.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                        //pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosPublico.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        // pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosPublico.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                        //pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCRAS.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosCRAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        // pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCRAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                        // pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCREAS.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosCREAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        //pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCREAS.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                        //pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCentroPOP.PrevisaoAnualNumeroAtendidos && s.ServicosRecursosFinanceirosCentroPOP.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                        // pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                    if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > numeroBeneficiarios && s.ServicosRecursosFinanceirosCentroPOP.Desativado != true && exercicio != 0)
                    {
                        var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                        pendencia.Pendencias.Add("III - Transferência de Renda - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                        // pendencia.TransferenciaRenda = false;
                        pendencia.ProgramasProjetos = false;
                        continue;
                    }
                }
                }
            }


            #endregion

            #region Beneficios Eventuais

            var beneficios = new PrefeituraBeneficioEventual().GetByPrefeitura(idPrefeitura);
            pendencia.BeneficiosEventuais = true;

            if (beneficios.Count() == 0)
            {
                pendencia.Alertas.Add("III -  Benefícios Eventuais : Não existe Cadastro de Benefícios Eventuais!");
                //pendencia.BeneficiosEventuais = false;
            }
            else
            {
                foreach (var beneficio in beneficios)
                {
                    //var  = new PrefeituraBeneficioEventual().GetById(b.Id).MediaSemestralBeneficiarios;
                    var servicos = new PrefeituraBeneficioEventualServico().GetByBeneficioEventual(beneficio.Id);
                    var totalServicos = servicos.Count();

                    ValidarBeneficiosEventuais(pendencia, beneficio);
                    
                    if (beneficio.BeneficiarioAtendidoRedeSocioAssistencial && totalServicos == 0)
                    {
                        pendencia.Pendencias.Add("III -  Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + ": Não existe serviços vinculados");
                        pendencia.BeneficiosEventuais = false;
                        continue;
                    }
                    else if (totalServicos == 0)
                    {
                        pendencia.Alertas.Add("III -  Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + ": Não existe serviços vinculados");
                        continue;
                    }

                    foreach (var s in servicos)
                    {
                        if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroBeneficiarios > beneficio.MediaSemestralBeneficiarios) //s.ServicosRecursosFinanceirosPrivado.PrevisaoAnualNumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroBeneficiarios > beneficio.MediaSemestralBeneficiarios)// s.ServicosRecursosFinanceirosPublico.PrevisaoAnualNumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroBeneficiarios > beneficio.MediaSemestralBeneficiarios)//s.ServicosRecursosFinanceirosCRAS.PrevisaoAnualNumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroBeneficiarios > beneficio.MediaSemestralBeneficiarios) //s.ServicosRecursosFinanceirosCREAS.PrevisaoAnualNumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroBeneficiarios > beneficio.MediaSemestralBeneficiarios) //s.ServicosRecursosFinanceirosCentroPOP.PrevisaoAnualNumeroAtendidos)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Benefícios Eventuais - " + beneficio.TipoBeneficioEventual.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de beneficiários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosEventuais = false;
                            continue;
                        }
                    }
                }
            }

            #endregion

            #region Beneficios Continuados (Transferência Renda)
            var beneficiosContinuados = new TransferenciaRenda().GetBeneficiosContinuadosByPrefeitura(idPrefeitura);
            pendencia.BeneficiosContinuados = true;
            foreach (var t in beneficiosContinuados)
            {
                if (!new TransferenciaRenda().ProgramaAderido(t))
                    continue;
                var servicos = new TransferenciaRendaCofinanciamento().GetByTransferenciaRenda(t.Id);
                var totalServicos = servicos.Count();
                if (t.BeneficiarioAtendidoRedeSocioAssistencial && totalServicos == 0)
                {
                    pendencia.Pendencias.Add("III - Benefícios Continuados - " + t.Nome + " não possui serviços vinculados");
                    pendencia.BeneficiosContinuados = false;
                    continue;
                }
                else if (totalServicos == 0)
                {
                    pendencia.Alertas.Add("III - Benefícios Continuados - " + t.Nome + " não possui serviços vinculados");
                    continue;
                }

                var e = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 23 && x.Desbloqueado == true).FirstOrDefault();

                Int32 exercicio = 0;

                if (e != null)
                {
                    exercicio = (Int32)e.Exercicio;
               

                    var numeroBeneficiarios = new TransferenciaRenda().GetNumeroBeneficiarios(t, exercicio);

                    foreach (var s in servicos)
                    {

                        if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPrivado.NumeroAtendidosServico + s.ServicosRecursosFinanceirosPrivado.PrevisaoAnualNumeroAtendidos && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos  - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosPrivado != null && s.NumeroUsuarios > numeroBeneficiarios && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPrivado.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosPublico.NumeroAtendidosServico + s.ServicosRecursosFinanceirosPublico.PrevisaoAnualNumeroAtendidos && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosPublico != null && s.NumeroUsuarios > numeroBeneficiarios && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosPublico.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos  - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCRAS.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCRAS.PrevisaoAnualNumeroAtendidos && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCRAS != null && s.NumeroUsuarios > numeroBeneficiarios && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCRAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCREAS.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCREAS.PrevisaoAnualNumeroAtendidos && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCREAS != null && s.NumeroUsuarios > numeroBeneficiarios && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCREAS.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > s.ServicosRecursosFinanceirosCentroPOP.NumeroAtendidosServico + s.ServicosRecursosFinanceirosCentroPOP.PrevisaoAnualNumeroAtendidos && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo próprio serviço!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                        if (s.ServicosRecursosFinanceirosCentroPOP != null && s.NumeroUsuarios > numeroBeneficiarios && s.Servico.Desativado != true)
                        {
                            var usuario = new UsuarioTipoServico().GetById(s.ServicosRecursosFinanceirosCentroPOP.IdUsuarioTipoServico);
                            pendencia.Pendencias.Add("III - Programas e projetos - " + t.Nome + " : " + usuario.TipoServico.Nome + " - " + usuario.Nome + ": O número de usuários do serviço vinculado não pode ser maior que o número de atendidos pelo programa!");
                            pendencia.BeneficiosContinuados = false;
                            continue;
                        }
                    }
                }
            }
            #endregion

            return pendencia;
        }

        private static int ValidarServicosCentroPOP(ValidacaoPMASInfo pendencia, int totalLocais, UnidadePublicaInfo unidade)
        {
            #region Centro POP
            var centroPOP = new CentroPOP().GetByUnidade(unidade.Id).Where(c => c.Desativado != true);
            totalLocais += centroPOP.Count();
            foreach (var unidadePOP in centroPOP)
            {
                var servicos = new ServicoRecursoFinanceiroCentroPOP().GetByCentroPOP(unidadePOP.Id).Where(c => c.Desativado != true);
                if (servicos.Count() == 0)
                {
                    pendencia.Pendencias.Add("III - Rede Proteção Social Pública - Centro POP " + unidadePOP.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.CentroPOP = false;
                }
                else
                {
                    foreach (var servico in servicos)
                    {

                        if (!servico.CentroPOP.PossuiServicoEspecializadoSituacaoRua.Value)
                        {
                            if (servico.CentroPOP.JustificativaServicoEspecializadoSituacaoRua == String.Empty)
                            {
                                pendencia.Pendencias.Add("III - Rede Proteção Social Pública - Centro POP " + unidadePOP.Nome + " não justificou a não oferta o serviço especializado em situacao rua");
                                pendencia.CentroPOP = false;
                            }
                        }
                        else
                        {
                            try
                            {
                                new ServicoRecursoFinanceiroCentroPOP().Validar(servico);
                                //new ServicoRecursoFinanceiroCentroPOP().ValidarProgramaServico(servico);
                            }
                            catch (Exception ex)
                            {

                                pendencia.Pendencias.Add(
                                string.Concat("III - Rede Proteção Social Direta - Centro POP "
                                , unidadePOP.Nome.Contains("Centro POP") ? unidadePOP.Nome : ("- Centro POP " + unidadePOP.Nome)
                                , ": "
                                , servico.UsuarioTipoServico.TipoServico.Nome, " - "
                                , servico.UsuarioTipoServico.Nome, " - FUNCIONAMENTO: "
                                , ex.Message));

                                pendencia.CentroPOP = false;
                              /*if (servico.UsuarioTipoServico.IdTipoServico == 144)
                                {
                                    pendencia.Pendencias.Add("III - Rede Proteção Social Pública - Centro POP " + unidadePOP.Nome + ": " + servico.UsuarioTipoServico.TipoServico.Nome + " - " + servico.UsuarioTipoServico.Nome + ": " + "Inconsistências nos campos!");
                                }*/
                            }

                        }

                    }
                }
            }


            #endregion
            return totalLocais;
        }

        private static int ValidarServicosCREAS(ValidacaoPMASInfo pendencia, int totalLocais, UnidadePublicaInfo unidade)
        {
            #region CREAS
            var creas = new CREAS().GetByUnidade(unidade.Id).Where(c => c.Desativado != true);
            totalLocais += creas.Count();
            foreach (var unidadeCreas in creas)
            {
                var servicos = new ServicoRecursoFinanceiroCREAS().GetByCREAS(unidadeCreas.Id).Where(c => c.Desativado != true);
                if (servicos.Count() == 0)
                {
                    pendencia.Pendencias.Add("III - Rede Proteção Social Pública - CREAS " + unidadeCreas.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.CREAS = false;
                }
                else
                {
                    foreach (var servico in servicos)
                    {
                        if (!servico.CREAS.PossuiPAEFI)
                        {
                            if (servico.CREAS.JustificativaPAEFI == String.Empty)
                            {
                                pendencia.Pendencias.Add("III - Rede Proteção Social Pública - CREAS " + unidadeCreas.Nome + " não justificou a não oferta o serviço PAEFI ");
                                pendencia.CREAS = false;
                            }
                        }
                        else
                        {
                            try
                            {
                                new ServicoRecursoFinanceiroCREAS().Validar(servico);
                            }
                            catch (Exception ex)
                            {                                
                                pendencia.Pendencias.Add(
                                string.Concat("III - Rede Proteção Social Direta - CREAS "
                                , unidadeCreas.Nome.Contains("CREAS") ? unidadeCreas.Nome : ("- CREAS " + unidadeCreas.Nome)
                                , ": "
                                , servico.UsuarioTipoServico.TipoServico.Nome, " - "
                                , servico.UsuarioTipoServico.Nome, " - FUNCIONAMENTO: "
                                , ex.Message));
                                pendencia.CREAS = false;
                            }
                        }
                    }
                }
            }


            #endregion

            return totalLocais;
        }

        private static int ValidarServicosCRAS(ValidacaoPMASInfo pendencia, int totalLocaisCRAS, UnidadePublicaInfo unidade)
        {
            #region CRAS
            var cras = new CRAS().GetByUnidade(unidade.Id).Where(c => c.Desativado != true);
            totalLocaisCRAS += cras.Count();
            foreach (var unidadeCras in cras)
            {
                var servicos = new ServicoRecursoFinanceiroCRAS().GetByCRAS(unidadeCras.Id).Where(s => s.Desativado != true);
                if (servicos.Count() == 0)
                {
                    pendencia.Pendencias.Add("III - Rede Proteção Social Pública - CRAS " + unidadeCras.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.CRAS = false;
                }
                else
                {
                    foreach (var servico in servicos)
                    {
                        if (!servico.CRAS.PossuiPAIF)
                        {
                            if (servico.CRAS.JustificativaPAIF == String.Empty)
                            {
                                pendencia.Pendencias.Add("III - Rede Proteção Social Pública - CRAS " + unidadeCras.Nome + " não justificou a não oferta o serviço PAIF ");
                                pendencia.CRAS = false;
                            }
                        }
                        else
                        {
                            try
                            {
                                new ServicoRecursoFinanceiroCRAS().Validar(servico);
                                //new ServicoRecursoFinanceiroCRAS().ValidarProgramaServico(servico);
                            }
                            catch (Exception ex)
                            {
                                //if (servico.UsuarioTipoServico.IdTipoServico == 135)
                                //{
                                pendencia.Pendencias.Add(
                                    string.Concat("III - Rede Proteção Social Direta - CRAS "
                                    , unidadeCras.Nome.Contains("CRAS") ? unidadeCras.Nome : ("- CRAS " + unidadeCras.Nome)
                                    , ": "
                                    , servico.UsuarioTipoServico.TipoServico.Nome, " - "
                                    , servico.UsuarioTipoServico.Nome, " - FUNCIONAMENTO: "
                                    , ex.Message));
                                pendencia.CRAS = false;
                                //}
                            }

                        }

                    }
                }
            }
            #endregion
            return totalLocaisCRAS;

        }

        private static int ValidarServicoPublico(ValidacaoPMASInfo pendencia, int totalLocais, UnidadePublicaInfo unidade, PrefeituraInfo prefeitura)
        {
            var locaisPublicos = new LocalExecucaoPublico().GetByUnidade(unidade.Id).Where(c => c.Desativado != true);
            totalLocais += locaisPublicos.Count();
            foreach (var local in locaisPublicos)
            {

                var servicosPublico = new ServicoRecursoFinanceiroPublico().GetByLocalExecucao(local.Id).Where(c => c.Desativado != true);
                if (servicosPublico.Count() == 0)
                {
                    pendencia.Pendencias.Add("III - Rede Proteção Social Direta - Unidade Pública " + unidade.RazaoSocial + ": Local de Execução " + local.Nome + " não possui serviços e recursos financeiros cadastrados");
                    pendencia.RedeProtecaoSocialPublica = false;
                }
                else
                {
                    foreach (var servico in servicosPublico)
                    {
                        try
                        {
                            new ServicoRecursoFinanceiroPublico().Validar(servico);

                        }
                        catch (Exception ex)
                        {
                            pendencia.RedeProtecaoSocialPublica = false;


                            if (servico.IdUsuarioTipoServico == 37 && servico.UsuarioTipoServico.IdTipoServico == 146 
                                && (servico.AtendeCriancasAuxilioReclusao == null || servico.AtendeCriancasPensaoMorte == null)
                                && (servico.AtendeCriancasAuxilioReclusaoExercicio2025 == null && servico.AtendeCriancasPensaoMorteExercicio2025 == null)
                                )
                            {
                                pendencia.Pendencias.Add(String.Format("III - Rede Proteção Social Direta - Unidade Pública {0} : Local de Execução {1} : {2} - {3} - Caracterização do Usuário : {4}"
                                                     , unidade.RazaoSocial
                                                     , local.Nome
                                                     , servico.UsuarioTipoServico.TipoServico.Nome
                                                     , servico.UsuarioTipoServico.Nome
                                                     , ex.Message));
                            }
                            else
                            {
                                pendencia.Pendencias.Add(String.Format("III - Rede Proteção Social Direta - Unidade Pública {0} : Local de Execução {1} : {2} - {3} - Funcionamento : {4}."
                                                     , unidade.RazaoSocial
                                                     , local.Nome
                                                     , servico.UsuarioTipoServico.TipoServico.Nome
                                                     , servico.UsuarioTipoServico.Nome
                                                     , ex.Message));
                            }


                        }

                        if (servico.PossuiProgramaBeneficio.HasValue && servico.PossuiProgramaBeneficio.Value)
                        {
                            //VALIDAR PROGRAMA BENEFICIO???
                        }

                    }
                }
            }
            return totalLocais;
        }

        private static int ValidarBeneficiosEventuais(ValidacaoPMASInfo pendencia,PrefeituraBeneficioEventualInfo beneficios)
        {
            int retorno = 0;

            try
            {
                new ValidadorServicoRecursoFinanceiro().ValidarBeneficiosEventuais(beneficios);
            }
            catch (Exception ex)
            {

                pendencia.Pendencias.Add(string.Concat("III - Benefícios eventuais "
                                                        , beneficios.TipoBeneficioEventual.Nome
                                                        , " - Recursos: "
                                                        , ex.Message));

                retorno = 1;
            }

            return retorno;
        }

        public ValidacaoPMASInfo ValidarBlocoIV(Int32 idPrefeitura, ValidacaoPMASInfo pendencia, Object dadosExtras = null)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();
            pendencia.Educacao = true;
            #region Educacao
            var educacao = new InterfacePublicaEducacao().GetByPrefeitura(idPrefeitura);
            if (educacao != null)
            {
                try
                {
                    new ValidadorInterfacePublica().ValidarEducacao(educacao);
                }
                catch (Exception ex)
                {
                    pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Educação : Inconsistências nos campos");
                    pendencia.Educacao = false;

                }
            }
            else
            {
                pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Educação : Inconsistências nos campos");
                pendencia.Educacao = false;
            }
            #endregion

            #region Saude
            var saude = new InterfacePublicaSaude().GetByPrefeitura(idPrefeitura);
            pendencia.Saude = true;
            if (saude != null)
            {
                try
                {
                    new ValidadorInterfacePublica().ValidarSaude(saude);
                }
                catch (Exception ex)
                {
                    pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Saúde : Inconsistências nos campos");
                    pendencia.Saude = false;

                }
            }
            else
            {
                pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Saúde : Inconsistências nos campos");
                pendencia.Saude = false;
            }
            #endregion

            #region Segurança alimentar
            pendencia.SegurancaAlimentar = true;
            var alimentacao = new InterfacePublicaAlimentacao().GetByPrefeitura(idPrefeitura);
            if (alimentacao != null)
            {
                try
                {
                    new ValidadorInterfacePublica().ValidarAlimentacao(alimentacao);
                }
                catch (Exception ex)
                {
                    pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Segurança Alimentar : Inconsistências nos campos");
                    pendencia.SegurancaAlimentar = false;

                }
            }
            else
            {
                pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Segurança Alimentar : Inconsistências nos campos");
                pendencia.SegurancaAlimentar = false;
            }
            #endregion

            #region emprego trabalho e renda
            pendencia.Emprego = true;
            var emprego = new InterfacePublicaEmprego().GetByPrefeitura(idPrefeitura);
            if (emprego != null)
            {
                try
                {
                    new ValidadorInterfacePublica().ValidarEmprego(emprego);
                }
                catch (Exception ex)
                {
                    pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Emprego, Trabalho e Renda : Inconsistências nos campos");
                    pendencia.Emprego = false;
                }
            }
            else
            {
                pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Emprego, Trabalho e Renda : Inconsistências nos campos");
                pendencia.Emprego = false;
            }
            #endregion

            #region Outras politicas Publicas
            pendencia.OutrasPoliticas = true;
            var outrasPoliticas = new InterfacePublicaOutraPolitica().GetByPrefeitura(idPrefeitura);
            if (outrasPoliticas != null)
            {
                try
                {
                    new ValidadorInterfacePublica().ValidarOutraPolitica(outrasPoliticas);
                }
                catch (Exception ex)
                {
                    pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Outras políticas públicas : Inconsistências nos campos");
                    pendencia.OutrasPoliticas = false;
                }
            }
            else
            {
                pendencia.Pendencias.Add("IV - Interface com outras políticas públicas - Outras políticas públicas : Inconsistências nos campos");
                pendencia.OutrasPoliticas = false;
            }

            #endregion

            return pendencia;
        }

        public ValidacaoPMASInfo ValidarBlocoV(Int32 idPrefeitura, ValidacaoPMASInfo pendencia, Object dadosExtras = null)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            #region Flags
            pendencia.CronogramaDesembolsoProtecaoBasica = true;
            pendencia.CronogramaDesembolsoProtecaoMedia = true;
            pendencia.CronogramaDesembolsoProtecaoAlta = true;
            pendencia.CronogramaDesembolsoBeneficioEventual = true;
            pendencia.CronogramaDesembolsoProgramaProjeto = true;
            pendencia.FontesFinanciamento = true;

            #endregion


            var prefeitura = new Prefeitura().GetById(idPrefeitura);
            var servicosRedeDireta = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 30);

            var servicosReprogramacaoRedeDireta = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 1019);
            var servicosReprogramacaoRedeIndireta = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 1020);
            var servicosReprogramacaoBeneficios = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 1040);
            var servicosReprogramacaoProgramas = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 1040);


            #region [Desembolso Reprogramacao Rede direta - Indireta & Beneficios Eventuais]

            #region [Desembolso Reprogramação Direta]

            foreach (var prefeituraExercicio in servicosReprogramacaoRedeDireta)
            {
                if (prefeituraExercicio.Desbloqueado.Value)
                {

                    var exercicio = prefeituraExercicio.Exercicio;

                    #region basica

                    var valorReprogramacaoRecursosDisponibilizadosBasica = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 1, exercicio);
                    var valorRecursosHumanosReprogramadosBasica = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 1, exercicio);
                    var valorOutrasDespesasReprogramadosBasica = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 1, exercicio);
                    var valorAquisicaoEquipamentosBasica = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura,1,1,exercicio);
                    var valorObrasBasica = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 1, exercicio);

                    var valorDemandasParlamentaresBasicaReprogramados = new CronogramaDesembolso().GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 1, exercicio);


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 1, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoBasica = lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoBasica != 0 && valorReprogramadoBasica != null)
                        {
                            if (Decimal.Round(valorReprogramadoBasica, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosBasica, 2) + Decimal.Round(valorRecursosHumanosReprogramadosBasica, 2) + Decimal.Round(valorAquisicaoEquipamentosBasica, 2) + Decimal.Round(valorObrasBasica, 2)))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Básica, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoBasica = false;
                            }
                            else if (Decimal.Round(valorReprogramadoBasica, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosBasica, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Básica, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoBasica = false;
                            }

                        }
                    }


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 1, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoBasicaDemandasParlamentares = lst.Where(t => t.IdTipoUnidade != 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoBasicaDemandasParlamentares != 0 && valorReprogramadoBasicaDemandasParlamentares != null)
                        {
                            if (Decimal.Round(valorReprogramadoBasicaDemandasParlamentares, 2) != Decimal.Round(valorDemandasParlamentaresBasicaReprogramados, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parlamentares preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Básica, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoBasica = false;
                            }

                        }
                    }

                    #endregion

                    #region Media
                    var valorReprogramacaoRecursosDisponibilizadosMedia = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio);
                    var valorRecursosHumanosReprogramadosMedia = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio);
                    var valorOutrasDespesasReprogramadosMedia = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio);
                    var valorAquisicaoEquipamentosMedia = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio);
                    var valorObrasMedia = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio);

                    var valorDemandasParlamentaresReprogramados = new CronogramaDesembolso().GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio);


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstDiretaMedia = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 2, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoDiretaMedia = lstDiretaMedia.Where(t => t.IdTipoUnidade != 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoDiretaMedia != 0 && valorReprogramadoDiretaMedia != null)
                        {
                            if (Decimal.Round(valorReprogramadoDiretaMedia, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosMedia, 2) + Decimal.Round(valorRecursosHumanosReprogramadosMedia, 2) + Decimal.Round(valorAquisicaoEquipamentosMedia, 2) + Decimal.Round(valorObrasMedia, 2)))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Média, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoMedia = false;
                            }
                            else if (Decimal.Round(valorReprogramadoDiretaMedia, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosMedia, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Média, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoMedia = false;
                            }

                        }

                    }


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstDiretaMedia = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 2, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoDemandasDiretaMedia = lstDiretaMedia.Where(t => t.IdTipoUnidade != 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoDemandasDiretaMedia != 0 && valorReprogramadoDemandasDiretaMedia != null)
                        {
                            if (Decimal.Round(valorReprogramadoDemandasDiretaMedia, 2) != Decimal.Round(valorDemandasParlamentaresReprogramados, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parlamentares preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Média, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoMedia = false;
                            }

                        }

                    }


                    #endregion

                    #region Alta
                    var valorReprogramacaoRecursosDisponibilizadosAlta = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);
                    var valorRecursosHumanosReprogramadosAlta = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);
                    var valorOutrasDespesasReprogramadosAlta = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);
                    var valorAquisicaoEquipamentosAlta = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);
                    var valorObrasAlta = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);

                    var valorDemandasParlamentaresReprogramadosAlta = new CronogramaDesembolso().GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstDiretaAlta = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 3, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoDiretaAlta = lstDiretaAlta.Where(t => t.IdTipoUnidade != 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoDiretaAlta != 0 && valorReprogramadoDiretaAlta != null)
                        {
                            if (Decimal.Round(valorReprogramadoDiretaAlta, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosAlta, 2) + Decimal.Round(valorRecursosHumanosReprogramadosAlta, 2) + Decimal.Round(valorAquisicaoEquipamentosAlta, 2) + Decimal.Round(valorObrasAlta, 2)))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Alta, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoAlta = false;
                            }
                            else if (Decimal.Round(valorReprogramadoDiretaAlta, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosAlta, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Alta, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoAlta = false;
                            }
                        }

                    }

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstDiretaAlta = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 3, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoDiretaAltaDemandasParlamentares = lstDiretaAlta.Where(t => t.IdTipoUnidade != 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoDiretaAltaDemandasParlamentares != 0 && valorReprogramadoDiretaAltaDemandasParlamentares != null)
                        {
                            if (Decimal.Round(valorReprogramadoDiretaAltaDemandasParlamentares, 2) != Decimal.Round(valorDemandasParlamentaresReprogramadosAlta, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parlamentares preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Alta, da Rede Direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoAlta = false;
                            }
                        }

                    }

                    #endregion

                }




            }

            #endregion

            #region [Desembolso Reprogramação Indireta]

            foreach (var prefeituraExercicio in servicosReprogramacaoRedeIndireta)
            {
                if (prefeituraExercicio.Desbloqueado.Value)
                {
                    var exercicio = prefeituraExercicio.Exercicio;

                    #region basica

                    var valorReprogramacaoRecursosDisponibilizadosIndiretaBasica = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);
                    var valorRecursosHumanosReprogramadosIndiretaBasica = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);
                    var valorOutrasDespesasReprogramadosIndiretaBasica = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);
                    var valorAquisicaoReprogramadosIndiretaBasica = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);
                    var valorObrasReprogramadosIndiretaBasica = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);

                    var valorDemandasParlamentaresReprogramadosBasicaIndireta = new CronogramaDesembolso().GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 1, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);
                        var valorReprogramadoPrivadaBasica = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoPrivadaBasica != 0 && valorReprogramadoPrivadaBasica != null)
                        {
                            if (Decimal.Round(valorReprogramadoPrivadaBasica, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosIndiretaBasica, 2) + Decimal.Round(valorRecursosHumanosReprogramadosIndiretaBasica, 2) + (Decimal.Round(valorObrasReprogramadosIndiretaBasica, 2) + Decimal.Round(valorAquisicaoReprogramadosIndiretaBasica, 2) )))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Básica, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoBasica = false;
                            }
                            else if (Decimal.Round(valorReprogramadoPrivadaBasica, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosIndiretaBasica, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Básica, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoBasica = false;
                            }

                        }

                    }

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lst = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 1, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);
                        var valorReprogramadoPrivadaBasicaDemandasParlamentares = lst.Where(t => t.IdTipoUnidade == 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoPrivadaBasicaDemandasParlamentares != 0 && valorReprogramadoPrivadaBasicaDemandasParlamentares != null)
                        {
                            if (Decimal.Round(valorReprogramadoPrivadaBasicaDemandasParlamentares, 2) != Decimal.Round(valorDemandasParlamentaresReprogramadosBasicaIndireta, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parlamentares preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Básica, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoBasica = false;
                            }
                        }

                    }

                    #endregion

                    #region media

                    var valorReprogramacaoRecursosDisponibilizadosIndiretaMedia = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio);
                    var valorRecursosHumanosReprogramadosIndiretaMedia = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio);
                    var valorOutrasDespesasReprogramadosIndiretaMedia = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio);
                    var valorAquisicaoReprogramadosIndiretaMedia = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio);
                    var valorObrasReprogramadosIndiretaMedia = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio);

                    var valorDemandasParlamentaresReprogramadosIndiretas = new CronogramaDesembolso().GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstIndiretaMedia = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 2, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoIndiretaMedia = lstIndiretaMedia.Where(t => t.IdTipoUnidade == 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoIndiretaMedia != 0 && valorReprogramadoIndiretaMedia != null)
                        {
                            if (Decimal.Round(valorReprogramadoIndiretaMedia, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosIndiretaMedia, 2) + Decimal.Round(valorRecursosHumanosReprogramadosIndiretaMedia, 2) + (Decimal.Round(valorObrasReprogramadosIndiretaMedia, 2) + Decimal.Round(valorAquisicaoReprogramadosIndiretaMedia, 2))))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Média, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoMedia = false;
                            }
                            else if (Decimal.Round(valorReprogramadoIndiretaMedia, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosIndiretaMedia, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Média, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoMedia = false;
                            }
                        }
                    }


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstIndiretaMedia = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 2, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoDemandasIndiretaMedia = lstIndiretaMedia.Where(t => t.IdTipoUnidade == 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoDemandasIndiretaMedia != 0 && valorReprogramadoDemandasIndiretaMedia != null)
                        {
                            if (Decimal.Round(valorReprogramadoDemandasIndiretaMedia, 2) != Decimal.Round(valorDemandasParlamentaresReprogramadosIndiretas, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parlamentares preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Média, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoMedia = false;
                            }

                        }

                    }

                    #endregion

                    #region alta

                    var valorReprogramacaoRecursosDisponibilizadosIndiretaAlta = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);
                    var valorRecursosHumanosReprogramadosIndiretaAlta = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);
                    var valorOutrasDespesasReprogramadosIndiretaAlta = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);
                    var valorAquisicaoReprogramadosIndiretaAlta = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);
                    var valorObrasReprogramadosIndiretaAlta = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);

                    var valorDemandasParlamentaresReprogramadosAltaIndireta = new CronogramaDesembolso().GetValorDisponibilizadosReprogramadosDemandasParlamentaresByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstIndiretaAlta = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 3, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoIndiretaAlta = lstIndiretaAlta.Where(t => t.IdTipoUnidade == 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoIndiretaAlta != 0 && valorReprogramadoIndiretaAlta != null)
                        {
                            if (Decimal.Round(valorReprogramadoIndiretaAlta, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosIndiretaAlta, 2) + Decimal.Round(valorRecursosHumanosReprogramadosIndiretaAlta, 2) + (Decimal.Round(valorObrasReprogramadosIndiretaAlta, 2) + Decimal.Round(valorAquisicaoReprogramadosIndiretaAlta, 2))))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Alta, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoAlta = false;
                            }
                            else if (Decimal.Round(valorReprogramadoIndiretaAlta, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosIndiretaAlta, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Alta, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoAlta = false;
                            }
                        }
                    }


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstIndiretaAlta = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 3, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoIndiretaAltaDemandasParelamentares = lstIndiretaAlta.Where(t => t.IdTipoUnidade == 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoIndiretaAltaDemandasParelamentares != 0 && valorReprogramadoIndiretaAltaDemandasParelamentares != null)
                        {
                            if (Decimal.Round(valorReprogramadoIndiretaAltaDemandasParelamentares, 2) != Decimal.Round(valorDemandasParlamentaresReprogramadosAltaIndireta, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parelamentares preenchidos no Bloco 5 Cronograma de Desembolso da Proteção Alta, da Rede Indireta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoProtecaoAlta = false;
                            }
                        }
                    }

                    #endregion

                }
            }

            #endregion

            #region [Desenbolso Reprogramação Beneficios Eventuais]

            foreach (var prefeituraExercicio in servicosReprogramacaoBeneficios)
            {
                if (prefeituraExercicio.Desbloqueado.Value)
                {
                    var exercicio = prefeituraExercicio.Exercicio;

                    #region Direta
                    var valorReprogramacaoRecursosDisponibilizadosBeneficiosEventuais = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);
                    var valorRecursosHumanosReprogramadosBeneficiosEventuais = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);
                    var valorOutrasDespesasReprogramadosBeneficiosEventuais = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);
                    var valorAquisicaoReprogramadosBeneficiosEventuais = new CronogramaDesembolso().GetValorAquisicaoEquipamentosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);
                    var valorObrasReprogramadosBeneficiosEventuais = new CronogramaDesembolso().GetValorObrasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);
                    
                    var valorReprogramadoDemandasBeneficiosEventuais = new CronogramaDesembolso().GetValorDemandasDisponibilizadosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstBeneficiosEventuais = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoBeneficiosEventuais = lstBeneficiosEventuais.Where(t => t.IdTipoUnidade != 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoBeneficiosEventuais != 0 && valorReprogramadoBeneficiosEventuais != null)
                        {
                            if (Decimal.Round(valorReprogramadoBeneficiosEventuais, 2) != (Decimal.Round(valorOutrasDespesasReprogramadosBeneficiosEventuais, 2) + Decimal.Round(valorRecursosHumanosReprogramadosBeneficiosEventuais, 2) + Decimal.Round(valorAquisicaoReprogramadosBeneficiosEventuais, 2) + Decimal.Round(valorObrasReprogramadosBeneficiosEventuais, 2)))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso dos Benefícios Eventuais, da Rede direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoBeneficioEventual = false;
                            }
                            else if (Decimal.Round(valorReprogramadoBeneficiosEventuais, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosBeneficiosEventuais, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso dos Benefícios Eventuais, da Rede direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoBeneficioEventual = false;
                            }

                        }
                    }


                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstBeneficiosEventuais = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);
                        
                        var valorReprogramadoBeneficiosEventuaisDemandas = lstBeneficiosEventuais.Where(t => t.IdTipoUnidade != 2).Sum(t => t.ValorEstadualReprogramacaoDemandasParlamentares);

                        if (valorReprogramadoBeneficiosEventuaisDemandas != 0 && valorReprogramadoBeneficiosEventuaisDemandas != null)
                        {
                            if (Decimal.Round(valorReprogramadoBeneficiosEventuaisDemandas, 2) != Decimal.Round(valorReprogramadoDemandasBeneficiosEventuais, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados das demandas parlamentares preenchidos no Bloco 5 Cronograma de Desembolso dos Benefícios Eventuais, da Rede direta, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoBeneficioEventual = false;
                            }
   
                        }

                    }


                    #endregion

                    #region Indireta
                    //var valorReprogramacaoRecursosDisponibilizadosBeneficiosEventuaisIndireta = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 2, exercicio);
                    //var valorRecursosHumanosReprogramadosBeneficiosEventuaisIndireta = new CronogramaDesembolso().GetValorRecursosHumanosReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 2, exercicio);
                    //var valorOutrasDespesasReprogramadosBeneficiosEventuaisIndireta = new CronogramaDesembolso().GetValorOutrasDespesasReprogramadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 2, exercicio);

                    //using (var proxy = new ProxyPrefeitura())
                    //{
                    //var lstBeneficiosEventuaisIndireta = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 5, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                    //var valorReprogramadoBeneficiosEventuaisIndireta = lstBeneficiosEventuaisIndireta.Where(t => t.IdTipoUnidade == 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                    //if (valorReprogramadoBeneficiosEventuaisIndireta != 0 && valorReprogramadoBeneficiosEventuaisIndireta != null)
                    //{
                    //    if (valorReprogramadoBeneficiosEventuaisIndireta != (valorOutrasDespesasReprogramadosBeneficiosEventuaisIndireta + valorRecursosHumanosReprogramadosBeneficiosEventuaisIndireta))
                    //    {
                    //        pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso dos Benefícios Eventuais, da Rede Indireta, devem ser iguais aos informados.");
                    //        pendencia.CronogramaDesembolsoBeneficioEventual = false;
                    //    }
                    //    else if (valorReprogramadoBeneficiosEventuaisIndireta != valorReprogramacaoRecursosDisponibilizadosBeneficiosEventuaisIndireta)
                    //    {
                    //        pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso dos Benefícios Eventuais, da Rede Indireta, devem ser iguais aos informados.");
                    //        pendencia.CronogramaDesembolsoBeneficioEventual = false;
                    //    }
                    //}
                    //else
                    //{
                    //    pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados preenchidos no Bloco 5 Cronograma de Desembolso dos Benefícios Eventuais, da Rede Indireta, devem ser iguais aos informados.");
                    //    pendencia.CronogramaDesembolsoBeneficioEventual = false;
                    //}
                    //}
                    #endregion
                }
            }

            #endregion

            #region [Desenbolso Reprogramação Programas Projetos]

            foreach (var prefeituraExercicio in servicosReprogramacaoProgramas)
            {
                if (prefeituraExercicio.Desbloqueado.Value)
                {
                    var exercicio = prefeituraExercicio.Exercicio;

                    var valorReprogramacaoRecursosDisponibilizadosProgramasProjetos = new CronogramaDesembolso().GetValorReprogramacaoRecursosDisponibilizadosByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 4, 1, exercicio);

                    using (var proxy = new ProxyPrefeitura())
                    {
                        var lstProgramasProjetos = proxy.Service.GetCofinanciamentoEstadualPrefeituraByTipoProtecaoSocial(idPrefeitura, 4, exercicio).OrderBy(t => t.IdTipoUnidade).ThenBy(t => t.TipoUnidade).ThenBy(t => t.Unidade).ThenBy(t => t.TipoServico).ThenBy(t => t.Usuario);

                        var valorReprogramadoProgramasProjetos = lstProgramasProjetos.Where(t => t.IdTipoUnidade != 2).Sum(t => t.RecursoReprogramadoAnoAnterior);

                        if (valorReprogramadoProgramasProjetos != 0 && valorReprogramadoProgramasProjetos != null)
                        {
                            if (Decimal.Round(valorReprogramadoProgramasProjetos, 2) != Decimal.Round(valorReprogramacaoRecursosDisponibilizadosProgramasProjetos, 2))
                            {
                                pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor dos recursos Reprogramados dos programas e projetos preenchidos no Bloco 5 Cronograma de Desembolso, devem ser iguais aos informados.");
                                pendencia.CronogramaDesembolsoBeneficioEventual = false;
                            }

                        }

                    }

                }
            }
            #endregion

            #endregion


            foreach (var prefeituraExercicio in servicosRedeDireta)
            {
                if (prefeituraExercicio.Desbloqueado.Value)
                {
                    var exercicio = prefeituraExercicio.Exercicio;

                    #region V: [Bloco V - Cronograma Desembolso] - Básica

                    var valorPrevisaoCofinanciamentoRedePublicaBasica = new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura, 1, exercicio);
                    var valorCronogramaRedePublicaBasica = new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 1, exercicio);

                    #region V: rede direta
                    #region V: [cronograma da direta]

                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 1, 1, exercicio))
                    {
                        pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede direta para Proteção Social Básica deve ser igual ao valor total dos recursos estaduais disponibilizados");
                        pendencia.CronogramaDesembolsoProtecaoBasica = false;
                    }

                    if (Decimal.Round(valorPrevisaoCofinanciamentoRedePublicaBasica, 1) != Decimal.Round(valorCronogramaRedePublicaBasica, 1))
                    {
                        pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor total de desembolso da Proteção Social Básica para rede direta deve ser igual ao Total do Cofinanciamento");
                        pendencia.CronogramaDesembolsoProtecaoBasica = false;
                    }

                    #endregion
                    #endregion

                    #region V: rede indireta
                    #region V: [cronograma da indireta]
                    var cronogramaDesembolsoEhValido = new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 1, 2, exercicio);

                    if (!cronogramaDesembolsoEhValido)
                    {
                        pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede indireta para Proteção Social Básica deve ser igual ao valor total dos recursos estaduais disponibilizados");
                        pendencia.CronogramaDesembolsoProtecaoBasica = false;
                    }

                    var cofPorTipoProtecao = new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(idPrefeitura, 1, exercicio);
                    var cofValorCronogramaDesembolso = new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 1, 2, exercicio);

                    if (cofPorTipoProtecao != cofValorCronogramaDesembolso)
                    {
                        pendencia.Pendencias.Add("V Cronograma de Desembolso : O valor total de desembolso da Proteção Social Básica para rede indireta deve ser igual ao Total do Cofinanciamento");
                        pendencia.CronogramaDesembolsoProtecaoBasica = false;
                    }

                    #endregion
                    #endregion

                    #endregion

                    #region V: [Bloco V - Cronograma Desembolso] - Especial Média Complexidade


                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 2, 1, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede direta para Proteção Social Especial Média Complexidade de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }
                    if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura, 2, exercicio) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 1, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Média Complexidade para rede direta de {0} deve ser igual ao Total do Cofinanciamento", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }

                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 2, 3, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede indireta para Proteção Social Especial Média Complexidade de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }
                    if (new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(idPrefeitura, 2, exercicio) != new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 2, 2, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Média Complexidade para Rede indireta de {0} deve ser igual ao Total do Cofinanciamento", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }

                    #endregion

                    #region V: [Bloco V - Cronograma Desembolso] - Especial Alta Complexidade

                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 3, 1, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede direta para Proteção Social Especial Alta Complexidade de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }

                    decimal valorCofEstadual = new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura, 3, exercicio);
                    decimal valorTipoProtecaoSocial = new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 1, exercicio);
                    if (valorCofEstadual != valorTipoProtecaoSocial)
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Alta Complexidade para Rede Pública de {0} deve ser igual ao Total do Cofinanciamento", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoAlta = false;
                    }
                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 3, 2, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede indireta para Proteção Social Especial Alta Complexidade de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoAlta = false;
                    }

                    decimal valorCofEstadualPrivada = new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePrivada(idPrefeitura, 3, exercicio);
                    decimal valorTipoProtecaoSocialPrivada = new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 3, 2, exercicio);
                    
                    if (valorCofEstadualPrivada != valorTipoProtecaoSocialPrivada)
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total de desembolso da Proteção Social Especial de Alta Complexidade para rede indireta de {0} deve ser igual ao Total do Cofinanciamento", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoAlta = false;
                    }

                    #endregion

                    #region V: [Bloco V - Cronograma Desembolso] - Programas e Projetos
                    valorPrevisaoCofinanciamentoRedePublicaBasica = new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura, 4, exercicio);
                    valorCronogramaRedePublicaBasica = new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 4, 1, exercicio);
                    
                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 4, 1, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede direta para Programas de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }
                    
                    if (valorPrevisaoCofinanciamentoRedePublicaBasica != valorCronogramaRedePublicaBasica)
                    {

                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos para Programas de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados.", exercicio));
                        pendencia.CronogramaDesembolsoProgramaProjeto = false;
                    }

                    #endregion

                    #region V: [Bloco - V: Cronograma Desembolso] - Beneficios eventuais

                    valorPrevisaoCofinanciamentoRedePublicaBasica = new RecursosFinanceiros().GetValorCofinanciamentoEstadualPrefeituraByTipoProtecaoSocialRedePublica(idPrefeitura, 5, exercicio);
                    valorCronogramaRedePublicaBasica = new CronogramaDesembolso().GetValorByPrefeituraETipoProtecaoSocialETipoUnidade(idPrefeitura, 5, 1, exercicio);

                    if (!new CronogramaDesembolso().ValidarCronogramaDesembolso(idPrefeitura, 2, 1, exercicio))
                    {
                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos da rede direta para Benefícios de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados", exercicio));
                        pendencia.CronogramaDesembolsoProtecaoMedia = false;
                    }

                    if (valorPrevisaoCofinanciamentoRedePublicaBasica != valorCronogramaRedePublicaBasica)
                    {

                        pendencia.Pendencias.Add(string.Format("V Cronograma de Desembolso : O valor total da previsão de execução dos recursos para Benefícios de {0} deve ser igual ao valor total dos recursos estaduais disponibilizados.", exercicio));
                        pendencia.CronogramaDesembolsoBeneficioEventual = false;
                    }

                    #endregion

                }
            }

            #region Fonte de Recursos FMAS

            using (var proxyPrefeituras = new ProxyPrefeitura())
            {
                var fmas = new FundoMunicipal().GetByPrefeitura(idPrefeitura);

                var prefeituras = new Prefeituras(proxyPrefeituras);
                var exerciciosServicosFonteRecursosFMAS = prefeitura.PrefeiturasExerciciosBloqueio.Where(x => x.IdRefBloqueio == 70);

                #region Exercicio 1
                var exercicio1 = exerciciosServicosFonteRecursosFMAS.Where(x => x.Exercicio == Exercicios[0]).FirstOrDefault();
                if (exercicio1 != null && exercicio1.Desbloqueado.Value)
                {
                    try
                    {
                        List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio1 = prefeituras.GetPrevisaoOrcamentaria(idPrefeitura, Exercicios[0]).ToList();
                        new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fmas, previsaoOrcamentariaExercicio1, Exercicios[0], dadosExtras);
                    }
                    catch (Exception ex)
                    {
                        foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        {
                            pendencia.Pendencias.Add(string.Format("V Fontes de Recursos Financiamento - {0} - {1}", s, Exercicios[0]));
                        }
                        pendencia.FontesFinanciamento = false;
                    }

                    var igd = new IndiceGestaoDescentralizada().GetByPrefeitura(idPrefeitura, Exercicios[0]);
                    if (igd != null)
                    {

                        try
                        {
                            new ValidadorIndiceGestaoDescentralizada().Validar(igd);
                        }
                        catch (Exception ex)
                        {
                            //pendencia.AcoesPlanejadas = false;
                            foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                            {
                                pendencia.Pendencias.Add("V Fontes de Financiamento - Índice de Gestão Descentralizada: " + s);
                            }
                            pendencia.FontesFinanciamento = false;
                        }
                    }
                }
                #endregion

                #region Exercicio 2

                var exercicio2 = exerciciosServicosFonteRecursosFMAS.Where(x => x.Exercicio == Exercicios[1]).FirstOrDefault();
                if (exercicio2 != null && exercicio2.Desbloqueado.Value)
                {

                    try
                    {
                        List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio2 = prefeituras.GetPrevisaoOrcamentaria(idPrefeitura, Exercicios[1]).ToList();
                        new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fmas, previsaoOrcamentariaExercicio2, Exercicios[1], dadosExtras);
                    }
                    catch (Exception ex)
                    {
                        foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        {
                            pendencia.Pendencias.Add(string.Format("V Fontes de Recursos Financiamento - {0} - {1}", s, Exercicios[1]));
                        }
                        pendencia.FontesFinanciamento = false;

                    }


                    var igd = new IndiceGestaoDescentralizada().GetByPrefeitura(idPrefeitura, Exercicios[1]);
                    if (igd != null)
                    {

                        try
                        {
                            new ValidadorIndiceGestaoDescentralizada().Validar(igd);
                        }
                        catch (Exception ex)
                        {
                            //pendencia.AcoesPlanejadas = false;
                            foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                            {
                                pendencia.Pendencias.Add(string.Format("V Fontes de Financiamento - Índice de Gestão Descentralizada: {0} - {1}", s, Exercicios[1]));
                            }
                            pendencia.FontesFinanciamento = false;
                        }
                    }
                }
                #endregion

                #region Exercicio 3
                var exercicio3 = exerciciosServicosFonteRecursosFMAS.Where(x => x.Exercicio == Exercicios[2]).FirstOrDefault();
                if (exercicio3 != null && exercicio3.Desbloqueado.Value)
                {
                    try
                    {
                        List<PrevisaoOrcamentariaInfo> previsaoOrcamentariaExercicio3 = prefeituras.GetPrevisaoOrcamentaria(idPrefeitura, Exercicios[2]).ToList();
                        new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fmas, previsaoOrcamentariaExercicio3, Exercicios[2], dadosExtras);
                    }
                    catch (Exception ex)
                    {
                        foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                        {
                            pendencia.Pendencias.Add(string.Format("V Fontes de Recursos do FMAS - {0} {1}", s, Exercicios[2]));
                            /*pendencia.Pendencias.Add(string.Format("V Fontes de Recursos Financiamento - {0} - {1}", s, Exercicios[2]));*/
                        }
                        pendencia.FontesFinanciamento = false;
                    }

                    var igd = new IndiceGestaoDescentralizada().GetByPrefeitura(idPrefeitura, Exercicios[2]);
                    if (igd != null)
                    {

                        try
                        {
                            new ValidadorIndiceGestaoDescentralizada().Validar(igd);
                        }
                        catch (Exception ex)
                        {
                            //pendencia.AcoesPlanejadas = false;
                            foreach (var s in ex.Message.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None))
                            {
                                pendencia.Pendencias.Add("V Fontes de Recursos do FMAS: " + s);
                                /*pendencia.Pendencias.Add("V Fontes de Financiamento - Índice de Gestão Descentralizada: " + s);*/
                            }
                            pendencia.FontesFinanciamento = false;
                        }
                    }
                }
                #endregion

                #region Exercicio 4


                #endregion

            }

            #endregion

            return pendencia;
        }

        public ValidacaoPMASInfo ValidarBlocoVI(Int32 idPrefeitura, ValidacaoPMASInfo pendencia)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            var acoes = new PrefeituraAcaoPlanejamento().GetByPrefeitura(idPrefeitura);
            pendencia.AcoesPlanejadas = true;

            var prefeitura = new Prefeitura().GetById(idPrefeitura);
            if (acoes.Count() == 0 || String.IsNullOrEmpty(prefeitura.JustificativaAcaoPlanejamento))
            {

                if (acoes.Count() == 0 && String.IsNullOrEmpty(prefeitura.JustificativaAcaoPlanejamento))
                {
                    pendencia.Alertas.Add("VI - Planejamento de Ações: Não existe ações cadastradas");
                    pendencia.Pendencias.Add("VI - Planejamento de Ações: Não houve planejamento de ações.");
                    pendencia.AcoesPlanejadas = false;
                }
                else
                {
                    if (acoes.Count() == 0)
                    {
                        pendencia.Alertas.Add("VI - Planejamento de Ações: Não existe ações cadastradas");

                        if (String.IsNullOrEmpty(prefeitura.JustificativaAcaoPlanejamento))
                        {
                            pendencia.Pendencias.Add("VI - Planejamento de Ações: Não houve planejamento de ações.");
                            pendencia.AcoesPlanejadas = false;
                        }
                    }

                }
            }
            return pendencia;
        }

        public ValidacaoPMASInfo ValidarBlocoVII(Int32 idPrefeitura, ValidacaoPMASInfo pendencia)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            bool deveTerAspectoGeral = false;

            var vigilancia = new VigilanciaSocioAssistencial().GetByPrefeitura(idPrefeitura);
            pendencia.VigilanciaSocioAssistencial = true;

            if (vigilancia == null)
            {
                pendencia.Pendencias.Add("VII - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Vigilância Socioassistencial");
                pendencia.VigilanciaSocioAssistencial = false;
            }
            else if (vigilancia.OfereceVigilancia)
                deveTerAspectoGeral = true;

            var monitoramento = new Monitoramento().GetByPrefeitura(idPrefeitura);
            pendencia.Monitoramento = true;

            if (monitoramento == null)
            {
                pendencia.Pendencias.Add("VII - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Monitoramento");
                pendencia.Monitoramento = false;
            }
            else if (monitoramento.RealizaMonitoramento)
                deveTerAspectoGeral = true;

            var avaliacao = new Avaliacao().GetByPrefeitura(idPrefeitura);
            pendencia.Avaliacao = true;

            if (avaliacao == null)
            {
                pendencia.Pendencias.Add("VII - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Avaliação");
                pendencia.Avaliacao = false;
            }
            else if (avaliacao.AvaliaAcoes)
                deveTerAspectoGeral = true;

            pendencia.AspectosGerais = true;
            if (deveTerAspectoGeral)
            {
                var aspectoGeral = new PrefeituraVigilanciaMonitoramentoAvaliacao().GetByPrefeitura(idPrefeitura);
                if (aspectoGeral == null)
                {
                    pendencia.Pendencias.Add("VII - Vigilância, Monitoramento e Avaliação: Não existe Cadastro de Aspectos Gerais");
                    pendencia.AspectosGerais = false;
                }
            }
            return pendencia;
        }

        public ValidacaoPMASInfo ValidarBlocoVIII(Int32 idPrefeitura, ValidacaoPMASInfo pendencia)
        {
            pendencia = pendencia ?? new ValidacaoPMASInfo();
            pendencia.Pendencias = pendencia.Pendencias ?? new List<string>();

            var cmas = new ConselhoMunicipal().GetByPrefeitura(idPrefeitura);

            pendencia.ConselhoMunicipal = true;
            if (cmas == null)
            {
                pendencia.Pendencias.Add("VIII - CMAS: Não existe Cadastro do Conselho Municipal");
                pendencia.ConselhoMunicipal = false;
            }

            var parecer = new ConselhoMunicipalParecer().GetByPrefeitura(idPrefeitura);
            pendencia.ParecerConselhoMunicipal = true;
            if (parecer == null)
            {
                pendencia.Pendencias.Add("VIII - CMAS: Não existe parecer do Conselho Municipal");
                pendencia.ParecerConselhoMunicipal = false;
            }
            return pendencia;
        }

        #endregion





    }
}
