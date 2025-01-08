using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;


namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class SelecionarRelatorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
            }

        }

        #region sessao
        private void PreencherSessao()
        {
            try
            {
                Session["RELATORIO_ESTADO"] = null;
                Session["RELATORIO_MUN_ID"] = null;
                Session["RELATORIO_DRD_ID"] = null;
                Session["RELATORIO_REG_ID"] = null;
                Session["RELATORIO_MACRO_REGIAO_ID"] = null;
                Session["OPCAO_RELATORIO"] = null;
                Session["RELATORIO_PORTE_ID"] = null;
                Session["RELATORIO_NIVEL_GESTAO_ID"] = null;
                Session["RELATORIO_TIPO_EXECUTORA"] = null;
                Session["RELATORIO_TIPO_PROGRAMA"] = null;
                Session["RELATORIO_TIPO_PROTECAO_ID"] = null;
                Session["RELATORIO_TIPO_SERVICO_ID"] = null;
                Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] = null;
                Session["RELATORIO_PUBLICO_ALVO_ID"] = null;
                Session["SITUACAO_VULNERABILIDADE"] = null;
                Session["SITUACAO_ESPECIFICA"] = null;
                Session["ABRANGENCIA_SERVICO"] = null;

                Session["SEXO"] = null;
                Session["REGIAOMORADIA"] = null;
                Session["CARACTERISTICASTERRITORIO"] = null;

                Session["TIPO_BENEFICIO_EVENTUAL"] = null;

                Session["SITUACOES_VULNERABILIDADE"] = null;
                Session["SITUACAO_VULNERABILIDADE_CONDICAO"] = null;
                Session["SITUACOES_ESPECIFICAS"] = null;
                Session["TIPO_FINANCIAMENTO"] = null;

                Session["RELATORIO_TIPO_CRONOGRAMA"] = null;
                Session["RELATORIO_TOTAL_CRONOGRAMAS"] = null;

                Session["RELATORIO_TIPO_UNIDADE"] = null;
                Session["RELATORIO_FORMA_ATUACAO"] = null;
                Session["RELATORIO_ABRANGENCIA_PROGRAMAS"] = null;
                Session["RELATORIO_MUNICIPIO"] = null;
                Session["RELATORIO_DATA_IMPLEMENTACAO"] = null;
                Session["RELATORIO_EXERCICIO"] = null;
                Session["ATIVO"] = null;
                Session["DESATIVO"] = null;

                List<int> lstPrefeitura = new List<int>();
                List<int> lstDrads = new List<int>();
                List<int> lstRegiaoMetropolitana = new List<int>();
                List<int> lstMacroRegiao = new List<int>();
                List<int> lstPorte = new List<int>();
                List<int> lstNivelGestao = new List<int>();
                List<int> lstTipoPrograma = new List<int>();
                List<string> lstAbrangenciaProgramas = new List<string>();

                List<int> lstAbrangencias = new List<int>();
                List<ETipoUnidade> lstTipoExecutora = new List<ETipoUnidade>();
                Boolean estado;
                int? tipoFinanciamento = null;
                int? tipoProtecao = null;
                int? tipoServico = null;
                bool? ehativo = null;
                bool? ehDesativo = null;


                //welington pereira - 20/08/2014
                int? servicoSubtificado = null;
                int? publicoAlvo = null;
                int? situacaoVulnerabilidade = null;
                int? situacaoEspecifica = null;
                String situacaoVulnerabilidadeCondicao = String.Empty;
                int? totalCronogramas = null;

                int? tipoUnidade = null;
                List<int> formasAtuacoes = new List<int>();

                List<int> lstSituacoesVulnerabilidade = new List<int>();
                List<int> lstSituacoesEspecificas = new List<int>();
                List<int> lstCronogramas = new List<int>();

                int? municipio = null;
                int? tipoBeneficioEventual = null;

                int? sexo = null;
                int? regiaoMoradia = null;
                int? caracteristicasTerritorio = null;
                int? exercicio = null;
                int? exercicioAuxilioReclusaoPensaoMorte = null;

                foreach (ListItem item in lstMunicipiosSelecionados.Items)
                {
                    lstPrefeitura.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstDradsSelecionadas.Items)
                {
                    lstDrads.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstRegiaoMetropolitanaSelecionada.Items)
                {
                    lstRegiaoMetropolitana.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstmacroRegiaoSelecionada.Items)
                {
                    lstMacroRegiao.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstPorteSelecionados.Items)
                {
                    lstPorte.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstNivelGestaoSelecionados.Items)
                {
                    lstNivelGestao.Add(Convert.ToInt32(item.Value));
                }

                foreach (ListItem item in lstTipoExecutorasSelecionadas.Items)
                {
                    lstTipoExecutora.Add((ETipoUnidade)Convert.ToInt32(item.Value));
                }

                foreach (ListItem item in lstProgramasSelecionadas.Items)
                {
                    lstTipoPrograma.Add(Convert.ToInt32(item.Value));
                }

                foreach (ListItem item in lstCronogramasEscolhidos.Items)
                {
                    lstCronogramas.Add(Convert.ToInt32(item.Value));
                }
                if (Util.TryParseInt32(ddlTipoProtecao.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoProtecao.SelectedValue).Value != 0)
                        tipoProtecao = Util.TryParseInt32(ddlTipoProtecao.SelectedValue);
                }

                if (Util.TryParseInt32(ddlTipoServico.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoServico.SelectedValue).Value != 0)
                        tipoServico = Util.TryParseInt32(ddlTipoServico.SelectedValue);
                }

                if (Util.TryParseInt32(ddlServicoSubtipificado.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlServicoSubtipificado.SelectedValue).Value != 0)
                        servicoSubtificado = Util.TryParseInt32(ddlServicoSubtipificado.SelectedValue);
                }


                foreach (ListItem item in lstAbrangenciaProgramaProjetoSelecionados.Items)
                {
                    lstAbrangenciaProgramas.Add(item.Value);
                }

                if (chkConfinanciamento.Checked)
                {
                    tipoFinanciamento = 1;
                }

                if (chkFuncionamento.Checked)
                {
                    ehativo = true;
                }

                if (chkDesativado.Checked)
                {
                    ehDesativo = true;
                }

                if (chkTotalCronograma.Checked)
                {
                    totalCronogramas = 1;
                }
                if (Util.TryParseInt32(ddlPublicoAlvo.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlPublicoAlvo.SelectedValue).Value != 0)
                        publicoAlvo = Util.TryParseInt32(ddlPublicoAlvo.SelectedValue).Value;
                }

                if (Util.TryParseInt32(ddlMunicipio.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlMunicipio.SelectedValue).Value != 0)
                        municipio = Util.TryParseInt32(ddlMunicipio.SelectedValue).Value;
                }

                if (Util.TryParseInt32(ddlProblemaSocial.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlProblemaSocial.SelectedValue).Value != 0)
                    {
                        situacaoVulnerabilidade = Util.TryParseInt32(ddlProblemaSocial.SelectedValue).Value;
                        lstSituacoesVulnerabilidade.Add(situacaoVulnerabilidade.Value);
                    }

                }

                if (Util.TryParseInt32(ddlProblemaSocial2.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlProblemaSocial2.SelectedValue).Value != 0)
                        lstSituacoesVulnerabilidade.Add(Util.TryParseInt32(ddlProblemaSocial2.SelectedValue).Value);
                }

                if (Util.TryParseInt32(ddlSexo.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlSexo.SelectedValue).Value != 0)
                        sexo = Util.TryParseInt32(ddlSexo.SelectedValue).Value;
                }

                if (Util.TryParseInt32(ddlRegiaoMoradia.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlRegiaoMoradia.SelectedValue).Value != 0)
                        regiaoMoradia = Util.TryParseInt32(ddlRegiaoMoradia.SelectedValue).Value;
                }

                if (Util.TryParseInt32(ddlCaracteristicasTerritorio.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlCaracteristicasTerritorio.SelectedValue).Value != 0)
                        caracteristicasTerritorio = Util.TryParseInt32(ddlCaracteristicasTerritorio.SelectedValue).Value;
                }

                if (Util.TryParseInt32(ddlAbrangenciaServico.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlAbrangenciaServico.SelectedValue).Value != 0)
                    {
                        var idAbrangencia = Util.TryParseInt32(ddlAbrangenciaServico.SelectedValue).Value;
                        lstAbrangencias.Add(idAbrangencia);
                        if (idAbrangencia == 1)
                            lstAbrangencias.Add(2);
                    }
                }

                if (Util.TryParseInt32(ddlTipoBeneficioEventual.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoBeneficioEventual.SelectedValue).Value != 0)
                        tipoBeneficioEventual = Util.TryParseInt32(ddlTipoBeneficioEventual.SelectedValue).Value;
                }

                if (lstSituacoesVulnerabilidadeSelecionadas.Visible)
                {
                    foreach (ListItem item in lstSituacoesVulnerabilidadeSelecionadas.Items)
                    {
                        lstSituacoesVulnerabilidade.Add(Convert.ToInt32(item.Value));
                    }
                }
                if (lstSituacoesEspecificasSelecionadas.Visible)
                {
                    foreach (ListItem item in lstSituacoesEspecificasSelecionadas.Items)
                    {
                        lstSituacoesEspecificas.Add(Convert.ToInt32(item.Value));
                    }
                }

                if (lstCronogramasEscolhidos.Visible)
                {
                    foreach (ListItem item in lstCronogramasEscolhidos.Items)
                    {
                        lstCronogramas.Add(Convert.ToInt32(item.Value));
                    }
                }

                if (Util.TryParseInt32(ddlTipoUnidade.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoUnidade.SelectedValue).Value != 0)
                        tipoUnidade = Util.TryParseInt32(ddlTipoUnidade.SelectedValue).Value;
                }

                if (lstFormaAtuacaoSelecionados.Visible)
                {
                    foreach (ListItem item in lstFormaAtuacaoSelecionados.Items)
                    {
                        formasAtuacoes.Add(Convert.ToInt32(item.Value));
                    }
                }

                if (Util.TryParseInt32(ddlExercicio.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlExercicio.SelectedValue).Value != 0)
                        exercicio = Util.TryParseInt32(ddlExercicio.SelectedValue).Value;
                }

                if (Util.TryParseInt32(ddlExercicioAuxilioReclusaoPensaoMorte.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlExercicioAuxilioReclusaoPensaoMorte.SelectedValue).Value != 0 )
                    {
                        exercicioAuxilioReclusaoPensaoMorte = Util.TryParseInt32(ddlExercicioAuxilioReclusaoPensaoMorte.SelectedValue).Value;
                    }
                }


                //if (Util.TryParseInt32(ddlFormaAtuacao.SelectedValue).HasValue)
                //{
                //    if (Util.TryParseInt32(ddlFormaAtuacao.SelectedValue).Value != 0)
                //        formasAtuacoes.Add(Util.TryParseInt32(ddlFormaAtuacao.SelectedValue));
                //}

                estado = Convert.ToInt32(ddlRelatorio.SelectedValue) == 1;

                situacaoVulnerabilidadeCondicao = ddlProblemaSocialCondicao.SelectedValue;
                Session["RELATORIO_DATA_IMPLEMENTACAO"] = txtDataInicial.Text;

                Session["RELATORIO_ESTADO"] = estado;
                Session["RELATORIO_MUN_ID"] = lstPrefeitura;
                Session["RELATORIO_DRD_ID"] = lstDrads;
                Session["RELATORIO_REG_ID"] = lstRegiaoMetropolitana;
                Session["RELATORIO_MACRO_REGIAO_ID"] = lstMacroRegiao;
                Session["OPCAO_RELATORIO"] = (ERelatorio)Convert.ToInt32(ddlRelatorioDescritivo.SelectedItem.Value);
                Session["RELATORIO_PORTE_ID"] = lstPorte;
                Session["RELATORIO_NIVEL_GESTAO_ID"] = lstNivelGestao;
                Session["RELATORIO_TIPO_EXECUTORA"] = lstTipoExecutora;
                Session["RELATORIO_TIPO_PROGRAMA"] = lstTipoPrograma;
                Session["RELATORIO_TIPO_PROTECAO_ID"] = tipoProtecao;
                Session["RELATORIO_TIPO_SERVICO_ID"] = tipoServico;
                Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] = servicoSubtificado;
                Session["RELATORIO_PUBLICO_ALVO_ID"] = publicoAlvo;
                Session["SITUACAO_VULNERABILIDADE"] = situacaoVulnerabilidade;
                Session["SITUACAO_ESPECIFICA"] = situacaoEspecifica;
                Session["ABRANGENCIA_SERVICO"] = lstAbrangencias;

                Session["SEXO"] = sexo;
                Session["REGIAOMORADIA"] = regiaoMoradia;
                Session["CARACTERISTICASTERRITORIO"] = caracteristicasTerritorio;

                Session["TIPO_BENEFICIO_EVENTUAL"] = tipoBeneficioEventual;

                Session["SITUACOES_VULNERABILIDADE"] = lstSituacoesVulnerabilidade;
                Session["SITUACAO_VULNERABILIDADE_CONDICAO"] = situacaoVulnerabilidadeCondicao;
                Session["SITUACOES_ESPECIFICAS"] = lstSituacoesEspecificas;
                Session["TIPO_FINANCIAMENTO"] = tipoFinanciamento;

                Session["RELATORIO_TIPO_CRONOGRAMA"] = lstCronogramas;
                Session["RELATORIO_TOTAL_CRONOGRAMAS"] = totalCronogramas;

                Session["RELATORIO_TIPO_UNIDADE"] = tipoUnidade;
                Session["RELATORIO_FORMA_ATUACAO"] = formasAtuacoes;

                Session["RELATORIO_ABRANGENCIA_PROGRAMAS"] = lstAbrangenciaProgramas;

                Session["RELATORIO_MUNICIPIO"] = municipio;
                Session["RELATORIO_EXERCICIO"] = exercicio;
                Session["RELATORIO_EXERCICIO_ARPM"] = exercicioAuxilioReclusaoPensaoMorte;
                Session["ATIVO"] = ehativo;
                Session["DESATIVO"] = ehDesativo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PreencherSessaoCadastral()
        {
            try
            {
                int? tipoProtecao = null;
                int? tipoServico = null;
                int? publicoAlvo = null;
                int? tipoConselho = null;
                int? servicoSubtificado = null;

                List<int> lstPrefeitura = new List<int>();
                List<int> lstDrads = new List<int>();
                List<int> lstRegiaoMetropolitana = new List<int>();
                List<int> lstMacroRegiao = new List<int>();
                List<int> lstPorte = new List<int>();
                List<int> lstNivelGestao = new List<int>();
                Boolean? estado = false;

                List<ETipoUnidade> lstTipoExecutora = new List<ETipoUnidade>();

                foreach (ListItem item in lstMunicipiosSelecionadosCadastral.Items)
                {
                    lstPrefeitura.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstDradsSelecionadasCadastral.Items)
                {
                    lstDrads.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstRegiaoMetropolitanaSelecionadaCadastral.Items)
                {
                    lstRegiaoMetropolitana.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstmacroRegiaoSelecionadaCadastral.Items)
                {
                    lstMacroRegiao.Add(Convert.ToInt16(item.Value));
                }
                foreach (ListItem item in lstPorteSelecionadosCadastral.Items)
                {
                    lstPorte.Add(Convert.ToInt32(item.Value));
                }
                foreach (ListItem item in lstNivelGestaoSelecionadosCadastral.Items)
                {
                    lstNivelGestao.Add(Convert.ToInt16(item.Value));
                }

                foreach (ListItem item in lstTipoExecutorasSelecionadasCadastral.Items)
                {
                    lstTipoExecutora.Add((ETipoUnidade)Convert.ToInt32(item.Value));
                }

                if (Util.TryParseInt32(ddlTipoProtecaoCadastral.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoProtecaoCadastral.SelectedValue).Value != 0)
                        tipoProtecao = Util.TryParseInt32(ddlTipoProtecaoCadastral.SelectedValue);
                }
                if (Util.TryParseInt32(ddlTipoServicoCadastral.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoServicoCadastral.SelectedValue).Value != 0)
                        tipoServico = Util.TryParseInt32(ddlTipoServicoCadastral.SelectedValue);
                }

                if (Util.TryParseInt32(ddlServicoSubtipificadoCadastral.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlServicoSubtipificadoCadastral.SelectedValue).Value != 0)
                        servicoSubtificado = Util.TryParseInt32(ddlServicoSubtipificadoCadastral.SelectedValue);
                }

                if (Util.TryParseInt32(ddlPublicoAlvoCadastral.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlPublicoAlvoCadastral.SelectedValue).Value != 0)
                        publicoAlvo = Util.TryParseInt32(ddlPublicoAlvoCadastral.SelectedValue).Value;
                }
                if (ddlAbrangenciaRelatorioCadastral != null)
                {
                    if (Util.TryParseInt32(ddlAbrangenciaRelatorioCadastral.SelectedValue).HasValue)
                    {
                        estado = Util.TryParseInt32(ddlAbrangenciaRelatorioCadastral.SelectedValue).Value == 1;
                    }
                }

                if (Util.TryParseInt32(ddlTipoConselho.SelectedValue).HasValue)
                {
                    if (Util.TryParseInt32(ddlTipoConselho.SelectedValue).Value != 0)
                        tipoConselho = Util.TryParseInt32(ddlTipoConselho.SelectedValue).Value;
                }



                Session["RELATORIO_ESTADO"] = estado;
                Session["RELATORIO_MUN_ID"] = lstPrefeitura;
                Session["RELATORIO_DRD_ID"] = lstDrads;
                Session["RELATORIO_REG_ID"] = lstRegiaoMetropolitana;
                Session["RELATORIO_MACRO_REGIAO_ID"] = lstMacroRegiao;
                Session["RELATORIO_PORTE_ID"] = lstPorte;
                Session["RELATORIO_NIVEL_GESTAO_ID"] = lstNivelGestao;
                Session["RELATORIO_TIPO_EXECUTORA"] = lstTipoExecutora;
                Session["RELATORIO_PUBLICO_ALVO_ID"] = publicoAlvo;
                Session["RELATORIO_TIPO_SERVICO_ID"] = tipoServico;
                Session["RELATORIO_TIPO_PROTECAO_ID"] = tipoProtecao;
                Session["RELATORIO_TIPO_CONSELHO_ID"] = tipoConselho;
                Session["RELATORIO_SERVICO_SUBTIFICADO_ID"] = servicoSubtificado;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PreencherSessaoQuantitativo()
        {
            try
            {
                List<int> lstPrefeitura = new List<int>();
                List<int> lstDrads = new List<int>();
                List<int> lstRegiaoMetropolitana = new List<int>();
                List<int> lstMacroRegiao = new List<int>();
                Boolean? estado = false;

                int? situacao = null;
                int? situacaoEspecifica = null;

                foreach (ListItem item in lstMunicipiosSelecionadosQuantitativo.Items)
                {
                    lstPrefeitura.Add(Convert.ToInt16(item.Value));
                }
                foreach (ListItem item in lstDradsSelecionadasQuantitativo.Items)
                {
                    lstDrads.Add(Convert.ToInt16(item.Value));
                }
                foreach (ListItem item in lstRegiaoMetropolitanaSelecionadaQuantitativo.Items)
                {
                    lstRegiaoMetropolitana.Add(Convert.ToInt16(item.Value));
                }
                foreach (ListItem item in lstmacroRegiaoSelecionadaQuantitativo.Items)
                {
                    lstMacroRegiao.Add(Convert.ToInt16(item.Value));
                }

                if (ddlAbrangenciaRelatorioQuantitativo != null)
                {
                    if (Util.TryParseInt32(ddlAbrangenciaRelatorioQuantitativo.SelectedValue).HasValue)
                    {
                        estado = Util.TryParseInt32(ddlAbrangenciaRelatorioQuantitativo.SelectedValue).Value == 1;
                    }
                }

                Session["RELATORIO_ESTADO"] = estado;

                Session["RELATORIO_MUN_ID"] = lstPrefeitura;
                Session["RELATORIO_DRD_ID"] = lstDrads;
                Session["RELATORIO_REG_ID"] = lstRegiaoMetropolitana;
                Session["RELATORIO_MACRO_REGIAO_ID"] = lstMacroRegiao;

                Session["SITUACAO_VULNERABILIDADE"] = situacao;
                Session["SITUACAO_ESPECIFICA"] = situacaoEspecifica;
                Session["RELATORIO_DATA_IMPLEMENTACAO"] = DateTime.Now.ToShortDateString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region descritivo
        protected void btnIncluirItemDrads_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstDradsDisponiveis, lstDradsSelecionadas);
        }
        protected void btnIncluirListaDrads_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstDradsDisponiveis, lstDradsSelecionadas);
        }
        protected void btnExcluirItemDrads_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstDradsSelecionadas, lstDradsDisponiveis);
        }
        protected void btnExcluirListaDrads_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstDradsSelecionadas, lstDradsDisponiveis);
        }

        protected void btnIncluirItemSituacaoVulnerabilidade_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstSituacoesVulnerabilidadeDisponiveis, lstSituacoesVulnerabilidadeSelecionadas);
        }
        protected void btnIncluirListaSituacaoVulnerabilidade_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstSituacoesVulnerabilidadeDisponiveis, lstSituacoesVulnerabilidadeSelecionadas);
        }
        protected void btnExcluirItemSituacaoVulnerabilidade_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstSituacoesVulnerabilidadeSelecionadas, lstSituacoesVulnerabilidadeDisponiveis);
        }
        protected void btnExcluirListaSituacaoVulnerabilidade_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstSituacoesVulnerabilidadeSelecionadas, lstSituacoesVulnerabilidadeDisponiveis);
        }

        protected void btnIncluirItemSituacaoEspecifica_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstSituacoesEspecificasDisponiveis, lstSituacoesEspecificasSelecionadas);
        }
        protected void btnIncluirListaSituacaoEspecifica_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstSituacoesEspecificasDisponiveis, lstSituacoesEspecificasSelecionadas);
        }
        protected void btnExcluirItemSituacaoEspecifica_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstSituacoesEspecificasSelecionadas, lstSituacoesEspecificasDisponiveis);
        }
        protected void btnExcluirListaSituacaoEspecifica_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstSituacoesEspecificasSelecionadas, lstSituacoesEspecificasDisponiveis);
        }

        protected void btnIncluirMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMunicipiosDisponiveis, lstMunicipiosSelecionados);
        }
        protected void btnIncluirListaMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMunicipiosDisponiveis, lstMunicipiosSelecionados);
        }
        protected void btnExcluirMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMunicipiosSelecionados, lstMunicipiosDisponiveis);
        }
        protected void btnExcluirListaMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMunicipiosSelecionados, lstMunicipiosDisponiveis);
        }

        protected void ddlRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            EOpcaoFiltro enumRelatorio = (EOpcaoFiltro)Convert.ToInt32(ddlRelatorio.SelectedItem.Value);
            btnExcluirListaDrads_Click(sender, e);
            btnExcluirListaMunicipio_Click(sender, e);
            btnExcluirListaRegiaoMetropolitana_Click(sender, e);
            btnExcluirListaMacroRegiao_Click(sender, e);
            btnExcluirListaNivelGestao_Click(sender, e);
            btnExcluirListaPorteMunicipio_Click(sender, e);

            switch (enumRelatorio)
            {
                case EOpcaoFiltro.Estado:
                    trMunicipio.Visible = false;
                    trDrads.Visible = false;
                    trMacroRegiao.Visible = false;
                    trRegiaoMetropolitana.Visible = false;
                    trPorteMunicipio.Visible = false;
                    trNivelGestao.Visible = false;
                    break;
                case EOpcaoFiltro.Drads:
                    lstDradsDisponiveis.DataSource = ProxyDivisaoAdministrativa.Drads;
                    lstDradsDisponiveis.DataTextField = "Nome";
                    lstDradsDisponiveis.DataValueField = "Id";
                    lstDradsDisponiveis.DataBind();
                    trDrads.Visible = true;
                    trMunicipio.Visible = false;
                    trMacroRegiao.Visible = false;
                    trRegiaoMetropolitana.Visible = false;
                    trPorteMunicipio.Visible = false;
                    trNivelGestao.Visible = false;
                    break;
                case EOpcaoFiltro.Municipio:
                    lstMunicipiosDisponiveis.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
                    lstMunicipiosDisponiveis.DataTextField = "Nome";
                    lstMunicipiosDisponiveis.DataValueField = "Id";
                    lstMunicipiosDisponiveis.DataBind();
                    trMunicipio.Visible = true;
                    trDrads.Visible = false;
                    trMacroRegiao.Visible = false;
                    trRegiaoMetropolitana.Visible = false;
                    trPorteMunicipio.Visible = false;
                    trNivelGestao.Visible = false;
                    break;
                case EOpcaoFiltro.MacroRegiao:
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lstMacroRegiaoDisponivel.DataSource = proxy.Service.GetMacroRegioes();
                        lstMacroRegiaoDisponivel.DataTextField = "Nome";
                        lstMacroRegiaoDisponivel.DataValueField = "Id";
                        lstMacroRegiaoDisponivel.DataBind();
                        trMunicipio.Visible = false;
                        trDrads.Visible = false;
                        trMacroRegiao.Visible = true;
                        trRegiaoMetropolitana.Visible = false;
                        trPorteMunicipio.Visible = false;
                        trNivelGestao.Visible = false;
                    }
                    break;
                case EOpcaoFiltro.RegioesMetropolitanas:
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        // lstRegiaoMetropolitanaDisponivel.DataSource = proxy.Service.GetRegioesMetropolitanas();
                        var lst = proxy.Service.GetRegioesMetropolitanas();
                        foreach (var item in lst)
                        {
                            if (item.Nome.ToLower().Equals("grande são paulo"))
                                item.Nome = "São Paulo";
                        }
                        lstRegiaoMetropolitanaDisponivel.DataSource = lst;
                        lstRegiaoMetropolitanaDisponivel.DataTextField = "Nome";
                        lstRegiaoMetropolitanaDisponivel.DataValueField = "Id";
                        lstRegiaoMetropolitanaDisponivel.DataBind();
                        trMunicipio.Visible = false;
                        trDrads.Visible = false;
                        trMacroRegiao.Visible = false;
                        trRegiaoMetropolitana.Visible = true;
                        trPorteMunicipio.Visible = false;
                        trNivelGestao.Visible = false;
                    }
                    break;
                case EOpcaoFiltro.PorteMunicipios:
                    trMunicipio.Visible = false;
                    trDrads.Visible = false;
                    trMacroRegiao.Visible = false;
                    trRegiaoMetropolitana.Visible = false;
                    trPorteMunicipio.Visible = true;
                    trNivelGestao.Visible = false;
                    break;
                case EOpcaoFiltro.NivelGestao:
                    trMunicipio.Visible = false;
                    trDrads.Visible = false;
                    trMacroRegiao.Visible = false;
                    trRegiaoMetropolitana.Visible = false;
                    trPorteMunicipio.Visible = false;
                    trNivelGestao.Visible = true;
                    break;
                default:
                    trMunicipio.Visible = false;
                    trDrads.Visible = false;
                    trMacroRegiao.Visible = false;
                    trRegiaoMetropolitana.Visible = false;
                    trPorteMunicipio.Visible = false;
                    trNivelGestao.Visible = false;
                    break;
            }
        }

        protected void btnIncluirItemMacroRegiao_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMacroRegiaoDisponivel, lstmacroRegiaoSelecionada);
        }
        protected void btnIncluirListaMacroRegiao_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMacroRegiaoDisponivel, lstmacroRegiaoSelecionada);
        }
        protected void btnExcluirItemMacroRegiao_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstmacroRegiaoSelecionada, lstMacroRegiaoDisponivel);
        }
        protected void btnExcluirListaMacroRegiao_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstmacroRegiaoSelecionada, lstMacroRegiaoDisponivel);
        }
        protected void btnIncluirItemRegiaoMetropolitana_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstRegiaoMetropolitanaDisponivel, lstRegiaoMetropolitanaSelecionada);
        }
        protected void btnIncluirListaRegiaoMetropolitana_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstRegiaoMetropolitanaDisponivel, lstRegiaoMetropolitanaSelecionada);
        }
        protected void btnExcluirItemRegiaoMetropolitana_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstRegiaoMetropolitanaSelecionada, lstRegiaoMetropolitanaDisponivel);
        }
        protected void btnExcluirListaRegiaoMetropolitana_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstRegiaoMetropolitanaSelecionada, lstRegiaoMetropolitanaDisponivel);
        }
        protected void btnIncluirItemPorteMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstPorteDisponiveis, lstPorteSelecionados);
        }
        protected void btnIncluirListaPorteMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstPorteDisponiveis, lstPorteSelecionados);
        }
        protected void btnExcluirItemPorteMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstPorteSelecionados, lstPorteDisponiveis);
        }
        protected void btnExcluirListaPorteMunicipio_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstPorteSelecionados, lstPorteDisponiveis);
        }
        protected void btnIncluirItemNivelGestao_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstNivelGestaoDisponiveis, lstNivelGestaoSelecionados);
        }
        protected void btnIncluirListaNivelGestao_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstNivelGestaoDisponiveis, lstNivelGestaoSelecionados);
        }
        protected void btnExcluirItemNivelGestao_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstNivelGestaoSelecionados, lstNivelGestaoDisponiveis);
        }
        protected void btnExcluirListaNivelGestao_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstNivelGestaoSelecionados, lstNivelGestaoDisponiveis);
        }

        protected void btnRelatorioDescritivo_Click(object sender, EventArgs e)
        {
            try
            {
                this.PreencherSessao();
                String page = "";
                switch (ddlRelatorioDescritivo.SelectedValue)
                {
                    case "1": page = "RInformacoesMunicipais"; break;
                    case "2": page = "RInformacoesBasicasDrads"; break;
                    case "3": page = "ROrganizacaoOrgaoGestor"; break;
                    case "4": page = "RRHOrgaoGestor"; break;
                    case "5": page = "RRHLocalExecucao"; break;
                    case "6": page = "RInformacoesFMAS"; break;
                    case "8":
                        if (Convert.ToInt32(ddlMunicipio.SelectedValue) > 0)
                        {
                            page = "RDiagnosticoSocioterritorial"; break;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError("É necessário que seja selecionado um município!"), true);
                            return;
                        }
                    case "9": page = "RAnaliseDiagnostica"; break;
                    case "7": page = "RConselhosExistentes"; break;
                    case "10": page = "RComunidadesGrupos"; break;
                    case "11": page = "RUnidadesServicosSocioassistenciais"; break;
                    case "12": page = "RRedesSocioassistenciais"; break;
                    case "13": page = "RRedesSocioassistenciaisDetalhamento"; break;
                    case "14": page = "RFuncionamentoCRAS"; break;
                    case "15": page = "RFuncionamentoCREAS"; break;
                    case "16": page = "RFuncionamentoCentroPOP"; break;
                    case "17": page = "RProgramaProjeto"; break;
                    //case "15": page = "RSaoPauloSolidario"; break;
                    //case "16": page = "RSaoPauloAmigoIdoso"; break;
                    //case "17": page = "RProgramasTransferenciaRenda"; break;
                    //case "18": page = "RProgramasMunicipaisTransferenciaRenda"; break;
                    case "18": page = "RBeneficiosContinuados"; break;
                    case "19": page = "RInformacoesBeneficiosEventuais"; break;
                    case "20": page = "RIntegracaoServicos"; break;
                    case "21": page = "RAcoesPlanejadas"; break;
                    case "22": page = "RAcoesVigilancia"; break;
                    case "23": page = "RAcoesMonitoramento"; break;
                    case "24": page = "RAcoesAvaliacao"; break;
                    case "25": page = "RServicosEstadualizados"; break;
                    case "26": page = "RServicosIntermunicipais"; break;
                    case "27": page = "RDistribuicaoEstadualProtecaoSocial"; break;
                    case "28": page = "RDistribuicaoEstadualProgramaTrabalho"; break;
                    case "29": page = "RCronogramaDesembolso"; break;
                    //case "30": page = "RExecucaoFinanceira"; break;
                    //case "30": page = "RProgramaFamiliaPaulista"; break;
                    case "31": page = "RServicosRegionalizados"; break;
                    case "32": page = "Raepeti"; break;
                    case "33": page = "RPrestacaoDeContas"; break;
                    case "34": page = "RPrestacaoDeContasProtecaoMedia"; break;
                    case "35": page = "RPrestacaoDeContasProtecaoAlta"; break;
                    case "36": page = "RPrestacaoDeContasBE"; break;
                    case "37": page = "RPrestacaoDeContasPP"; break;
                    case "38": page = "RStatusPrestacaoDeContas"; break;
                    case "39": page = "RStatusLeiOrcamentaria"; break;
                    case "40": page = "RStatusExecucaoFinanceira"; break;
                    case "41": page = "RAuxilioReclusaoPensaoMorte"; break;
                    default: return;
                }

                somenteAtivos();

                bool exibirMensagemExercicioObrigatorio = VerificarRestricaoFiltroExercicio();
                if (exibirMensagemExercicioObrigatorio)
                {
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirecionamento", "window.open('Relatorios/" + page + ".aspx','_blank');", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void somenteAtivos() 
        {

            if (ddlRelatorioDescritivo.SelectedValue == "31")
            {
                Session["ATIVO"] = true;
                Session["DESATIVO"] = false;
            }

        }

        private bool VerificarRestricaoFiltroExercicio()
        {
            if (ddlRelatorioDescritivo.SelectedValue == "12"
                || ddlRelatorioDescritivo.SelectedValue == "6"
                || ddlRelatorioDescritivo.SelectedValue == "3"
                || ddlRelatorioDescritivo.SelectedValue == "4"
                || ddlRelatorioDescritivo.SelectedValue == "9"
                 || ddlRelatorioDescritivo.SelectedValue == "17"
                || ddlRelatorioDescritivo.SelectedValue == "22"
                || ddlRelatorioDescritivo.SelectedValue == "28"
                || ddlRelatorioDescritivo.SelectedValue == "29"
                || ddlRelatorioDescritivo.SelectedValue == "31"
                || ddlRelatorioDescritivo.SelectedValue == "19"
                || ddlRelatorioDescritivo.SelectedValue == "33"
                || ddlRelatorioDescritivo.SelectedValue == "34"
                || ddlRelatorioDescritivo.SelectedValue == "35"
                || ddlRelatorioDescritivo.SelectedValue == "36"
                || ddlRelatorioDescritivo.SelectedValue == "37")
            {
                if ((!Util.TryParseInt32(ddlExercicio.SelectedValue).HasValue) || (Util.TryParseInt32(ddlExercicio.SelectedValue).HasValue && ddlExercicio.SelectedValue == "0"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogWarning("Selecione o exercício."), true);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {

                if (ddlRelatorioDescritivo.SelectedValue == "41")
                {

                    if ((!Util.TryParseInt32(ddlExercicioAuxilioReclusaoPensaoMorte.SelectedValue).HasValue) || (Util.TryParseInt32(ddlExercicioAuxilioReclusaoPensaoMorte.SelectedValue).HasValue && ddlExercicioAuxilioReclusaoPensaoMorte.SelectedValue == "0"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogWarning("Selecione o exercício."), true);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                
            }




        }


        protected void btnIncluirAbrangenciaProgramaProjeto_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstAbrangenciaProgramaProjeto, lstAbrangenciaProgramaProjetoSelecionados);
        }

        protected void btnIncluirListaAbrangenciaProgramaProjeto_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstAbrangenciaProgramaProjeto, lstAbrangenciaProgramaProjetoSelecionados);
        }

        protected void btnExcluirItemAbrangenciaProgramaProjeto_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstAbrangenciaProgramaProjetoSelecionados, lstAbrangenciaProgramaProjeto);
        }

        protected void btnExcluirListaAbrangenciaProgramaProjeto_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstAbrangenciaProgramaProjetoSelecionados, lstAbrangenciaProgramaProjeto);
        }
        protected void ddlRelatorioDescritivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                trMunicipioEscolha.Visible = false;
                trTotalCronograma.Visible = false;
                trFinanciamento.Visible = false;
                trTipoCronograma.Visible = false;
                trFormaAtuacao.Visible = false;
                btnRelatorioDescritivo.Enabled = true;

                #region Selecao de Relatorios
                var relatorioSelecionado = ddlRelatorioDescritivo.SelectedItem.Value;
                switch (relatorioSelecionado)
                {

                    #region 1. Informações municipais básicas</option>
                    case "1":
                        {
                            txtDataInicial.Text = System.DateTime.Now.ToShortDateString();

                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";

                            trDataReferencia.Visible = true;

                            break;
                        }
                    #endregion

                    #region 2. Informações básicas por DRADS</option>
                    case "2":
                        {
                            trDataReferencia.Visible = true;
                            txtDataInicial.Text = System.DateTime.Now.ToShortDateString();
                            break;
                        }
                    #endregion

                    #region 5. Recursos humanos dos serviços</option>
                    case "5":
                        {
                            trTipoProtecao.Visible = true;
                            trTipoServico.Visible = true;
                            trPublicoAlvo.Visible = true;
                            trTipoExecutora.Visible = true;

                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {

                                ddlTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
                                ddlTipoProtecao.DataValueField = "Id";
                                ddlTipoProtecao.DataTextField = "Nome";
                                ddlTipoProtecao.DataBind();
                                ddlTipoProtecao.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Programas e Projetos"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Benefícios Eventuais"));
                            }

                            rblTipoProtecao_SelectedIndexChanged(sender, e);
                            ddlTipoServico_SelectedIndexChanged(sender, e);

                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trDataReferencia.Visible = false;

                            break;
                        }
                    #endregion

                    #region 8. Diagnóstico socioterritorial</option>
                    case "8":
                        {
                            trAbrangencia.Visible = false;
                            trMunicipioEscolha.Visible = true;
                            ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
                            ddlMunicipio.DataTextField = "Nome";
                            ddlMunicipio.DataValueField = "Id";
                            ddlMunicipio.DataBind();
                            ddlMunicipio.Items.Insert(0, new ListItem(" Selecione ", "0"));
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            //rbData.Visible = false;



                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 9. Situacões de vulnerabilidade e/ou risco social</option>
                    case "9":
                        {
                            trProblemaSocial.Visible = true;
                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {
                                ddlProblemaSocial.DataSource = ddlProblemaSocial2.DataSource = proxy.Service.GetSituacoesVulnerabilidade().OrderBy(t => t.Ordem);
                                ddlProblemaSocial.DataValueField = ddlProblemaSocial2.DataValueField = "Id";
                                ddlProblemaSocial.DataTextField = ddlProblemaSocial2.DataTextField = "Nome";
                                ddlProblemaSocial.DataBind();
                                ddlProblemaSocial2.DataBind();
                                ddlProblemaSocial.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlProblemaSocial2.Items.Insert(0, new ListItem(" Selecione ", "0"));
                            }
                            trAbrangencia.Visible = true;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAcoes.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            //rbData.Visible = false;
                            trDataReferencia.Visible = false;

                            break;
                        }
                    #endregion

                    #region 11. Organizações e unidades públicas</option>
                    case "11":
                        {
                            trProblemaSocial.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trProgramas.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trFormaAtuacao.Visible = true;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            trTipoUnidade.Visible = true;
                            trAbrangenciaServico.Visible = false;


                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {
                                lstFormaAtuacaoDisponiveis.DataSource = proxy.Service.GetFormaAtuacao();
                                lstFormaAtuacaoDisponiveis.DataValueField = "Id";
                                lstFormaAtuacaoDisponiveis.DataTextField = "Nome";
                                lstFormaAtuacaoDisponiveis.DataBind();
                            }

                            trDataReferencia.Visible = false;

                            break;
                        }
                    #endregion

                    #region 12 Rede de serviços socioassistenciais</option>
                    case "12":
                        {
                            trFinanciamento.Visible = ddlRelatorioDescritivo.SelectedItem.Value == "12";
                            trTipoCronograma.Visible = false;
                            trTipoProtecao.Visible =
                            trTipoServico.Visible =
                            trTipoExecutora.Visible =
                            trPublicoAlvo.Visible = true;
                            trAbrangenciaServico.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência do atendimento : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            ddlAbrangenciaServico.SelectedIndex = 0;

                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {
                                ddlTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
                                ddlTipoProtecao.DataValueField = "Id";
                                ddlTipoProtecao.DataTextField = "Nome";
                                ddlTipoProtecao.DataBind();
                                ddlTipoProtecao.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Programas e Projetos"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Benefícios Eventuais"));
                            }

                            rblTipoProtecao_SelectedIndexChanged(sender, e);
                            ddlTipoServico_SelectedIndexChanged(sender, e);

                            ddlSexo.SelectedIndex = 0;
                            ddlRegiaoMoradia.SelectedIndex = 0;
                            ddlCaracteristicasTerritorio.SelectedIndex = 0;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;

                            trDataReferencia.Visible = false;

                            break;
                        }
                    #endregion

                    #region 13. Atendimentos específicos realizados pelos serviços socioassistenciais</option>
                    case "13":
                        {
                            trTipoProtecao.Visible =
                            trTipoServico.Visible =
                            trTipoExecutora.Visible =
                            trPublicoAlvo.Visible =
                            trSituacoesEspecificas.Visible =
                            trSituacoesVulnerabilidade.Visible = true;
                            trAbrangenciaServico.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência do atendimento : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            ddlAbrangenciaServico.SelectedIndex = 0;

                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {

                                ddlTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
                                ddlTipoProtecao.DataValueField = "Id";
                                ddlTipoProtecao.DataTextField = "Nome";
                                ddlTipoProtecao.DataBind();
                                ddlTipoProtecao.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Programas e Projetos"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Benefícios Eventuais"));

                                lstSituacoesVulnerabilidadeDisponiveis.DataSource = proxy.Service.GetSituacoesVulnerabilidade().OrderBy(t => t.Ordem);
                                lstSituacoesVulnerabilidadeDisponiveis.DataValueField = "Id";
                                lstSituacoesVulnerabilidadeDisponiveis.DataTextField = "Nome";
                                lstSituacoesVulnerabilidadeDisponiveis.Width = 950;
                                lstSituacoesVulnerabilidadeDisponiveis.DataBind();
                                lstSituacoesVulnerabilidadeDisponiveis.Rows = lstSituacoesVulnerabilidadeDisponiveis.Items.Count;

                                lstSituacoesVulnerabilidadeSelecionadas.DataSource = null;
                                lstSituacoesVulnerabilidadeSelecionadas.DataBind();

                                lstSituacoesEspecificasDisponiveis.DataSource = proxy.Service.GetSituacoesEspecificas().OrderBy(t => t.Nome);
                                lstSituacoesEspecificasDisponiveis.DataValueField = "Id";
                                lstSituacoesEspecificasDisponiveis.DataTextField = "Nome";
                                lstSituacoesEspecificasDisponiveis.Width = 550;
                                lstSituacoesEspecificasDisponiveis.DataBind();
                                lstSituacoesEspecificasDisponiveis.Rows = lstSituacoesEspecificasDisponiveis.Items.Count;

                                lstSituacoesEspecificasSelecionadas.DataSource = null;
                                lstSituacoesEspecificasSelecionadas.DataBind();

                                ddlProblemaSocial.DataSource = ddlProblemaSocial2.DataSource = proxy.Service.GetSituacoesVulnerabilidade().OrderBy(t => t.Ordem);
                                ddlProblemaSocial.DataValueField = ddlProblemaSocial2.DataValueField = "Id";
                                ddlProblemaSocial.DataTextField = ddlProblemaSocial2.DataTextField = "Nome";
                                ddlProblemaSocial.DataBind();
                                ddlProblemaSocial2.DataBind();
                                ddlProblemaSocial.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlProblemaSocial2.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlProblemaSocial.SelectedValue = "0";
                                ddlProblemaSocial2.SelectedValue = "0";



                            }

                            rblTipoProtecao_SelectedIndexChanged(sender, e);
                            ddlTipoServico_SelectedIndexChanged(sender, e);

                            ddlSexo.SelectedIndex = 0;
                            ddlRegiaoMoradia.SelectedIndex = 0;
                            ddlCaracteristicasTerritorio.SelectedIndex = 0;

                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trProblemaSocial.Visible = false;
                            trTipoUnidade.Visible = false;

                            trDataReferencia.Visible = true;
                            txtDataInicial.Text = System.DateTime.Now.ToShortDateString();
                            break;
                        }
                    #endregion

                    #region CRIAR 14
                    case "14":
                        {
                            //********************************************************
                            //* INEXISTENTE - CRIAR COM BASE NO PMAS2017
                            //********************************************************
                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            //rbData.Visible = false;
                            trDataReferencia.Visible = true;
                            break;
                        }
                    #endregion

                    #region CRIAR 15
                    case "15":
                        {
                            //********************************************************
                            //* INEXISTENTE - CRIAR COM BASE NO PMAS2017
                            //********************************************************
                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            //rbData.Visible = false;
                            trDataReferencia.Visible = true;
                            break;
                        }
                    #endregion

                    #region CRIAR 16
                    case "16":
                        {
                            //********************************************************
                            //* INEXISTENTE - CRIAR COM BASE NO PMAS2017
                            //********************************************************
                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            trDataReferencia.Visible = true;
                            break;
                        }
                    #endregion

                    #region NAO NECESSARIO???? 17
                    case "17":
                        trExercicio.Visible = true;
                        trAbrangenciaProgramaProjeto.Visible = false;
                        lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência territorial";
                        trDataReferencia.Visible = false;
                        break;
                    #endregion

                    #region 19. Informações sobre os benefícios eventuais</option>
                    case "19":
                        trTipoBeneficioEventual.Visible = true;
                        ddlTipoBeneficioEventual.SelectedIndex = 0;

                        trTipoProtecao.Visible = false;
                        trTipoServico.Visible = false;
                        trPublicoAlvo.Visible = false;
                        trTipoExecutora.Visible = false;
                        trLabelCaracteristicasUsuarios.Visible = false;
                        trSexo.Visible = false;
                        trRegiaoMoradia.Visible = false;
                        trCaracteristicasTerritorio.Visible = false;
                        trProblemaSocial.Visible = false;
                        trAcoes.Visible = false;
                        trProgramas.Visible = false;
                        trAbrangenciaServico.Visible = false;
                        trSituacoesEspecificas.Visible = false;
                        trSituacoesVulnerabilidade.Visible = false;
                        trTipoUnidade.Visible = false;
                        trDataReferencia.Visible = false;
                        break;
                    #endregion

                    #region ???? 26 Nao selecionável!
                    case "26":
                        {
                            trTipoProtecao.Visible = true;
                            trTipoServico.Visible = true;
                            trTipoExecutora.Visible = true;
                            trPublicoAlvo.Visible = true;
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência do relatório : ";
                            ddlAbrangenciaServico.SelectedIndex = 0;

                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {
                                ddlTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
                                ddlTipoProtecao.DataValueField = "Id";
                                ddlTipoProtecao.DataTextField = "Nome";
                                ddlTipoProtecao.DataBind();
                                ddlTipoProtecao.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Programas e Projetos"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Benefícios Eventuais"));
                            }
                            rblTipoProtecao_SelectedIndexChanged(sender, e);
                            ddlTipoServico_SelectedIndexChanged(sender, e);

                            ddlSexo.SelectedIndex = 0;
                            ddlRegiaoMoradia.SelectedIndex = 0;
                            ddlCaracteristicasTerritorio.SelectedIndex = 0;

                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;

                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 28. Distribuição dos recursos do cofinanciamento estadual, segundo os programas de trabalho</option>
                    case "28":
                        {
                            //TODO:DBM:Novo
                            trExercicio.Visible = true;
                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            //rbData.Visible = false;
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 29. Cronogramas de Desembolso</option>
                    case "29":
                        {
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trTipoCronograma.Visible = true;
                            trTotalCronograma.Visible = true;
                            trExercicio.Visible = true;

                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {
                                lstCronogramas.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3 || s.Id == 4 || s.Id == 5);
                                lstCronogramas.DataValueField = "Id";
                                lstCronogramas.DataTextField = "Nome";
                                lstCronogramas.Width = 200;
                                lstCronogramas.DataBind();
                                lstCronogramas.Rows = lstCronogramas.Items.Count;

                            }
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 3. Estruturação do órgão gestor da Assistência Social | 4. Recursos humanos do órgão gestor | 6. Informações sobre o FMAS | 7. Conselhos existentes nos municípios | 10. Presença de povos tradicionais e/ou grupos específicos nos municípios | 18. Nº de beneficiários e recursos financeiros dos Benefícios Continuados (BPC) | 21. Ações planejadas no PMAS | 23. Ações de monitoramento | 24. Ações de avaliação | 25. Serviços estadualizados | 27. Distribuição dos recursos do cofinanciamento estadual, segundo as proteções sociais
                    default:
                        trAbrangencia.Visible = true;
                        trMunicipioEscolha.Visible = false;
                        trServicoSubtificado.Visible = false;
                        trTipoProtecao.Visible = false;
                        trTipoServico.Visible = false;
                        trPublicoAlvo.Visible = false;
                        trTipoExecutora.Visible = false;
                        trLabelCaracteristicasUsuarios.Visible = false;
                        trSexo.Visible = false;
                        trRegiaoMoradia.Visible = false;
                        trCaracteristicasTerritorio.Visible = false;
                        trProblemaSocial.Visible = false;
                        trAcoes.Visible = false;
                        trProgramas.Visible = false;
                        trAbrangenciaServico.Visible = false;
                        trTipoBeneficioEventual.Visible = false;
                        trSituacoesEspecificas.Visible = false;
                        trSituacoesVulnerabilidade.Visible = false;
                        trTipoUnidade.Visible = false;
                        trAbrangenciaProgramaProjeto.Visible = false;
                        lblAbrangenciaServico.Text = "Abrangência : ";
                        lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                        //rbData.Visible = false;
                        trDataReferencia.Visible = false;

                        break;
                    #endregion

                    #region 31 Rede de Serviços Regionalizados</option>
    
                    case "31":
                        {
                            trFinanciamento.Visible = ddlRelatorioDescritivo.SelectedItem.Value == "31";
                            trTipoCronograma.Visible = false;
                            trTipoProtecao.Visible =
                            trTipoServico.Visible =
                            trTipoExecutora.Visible =
                            trPublicoAlvo.Visible = true;
                            trAbrangenciaServico.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência do atendimento : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";
                            ddlAbrangenciaServico.SelectedIndex = 0;

                            using (var proxy = new ProxyEstruturaAssistenciaSocial())
                            {
                                ddlTipoProtecao.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
                                ddlTipoProtecao.DataValueField = "Id";
                                ddlTipoProtecao.DataTextField = "Nome";
                                ddlTipoProtecao.DataBind();
                                ddlTipoProtecao.Items.Insert(0, new ListItem(" Selecione ", "0"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Programas e Projetos"));
                                ddlTipoProtecao.Items.Remove(ddlTipoProtecao.Items.FindByText("Benefícios Eventuais"));
                            }

                            rblTipoProtecao_SelectedIndexChanged(sender, e);
                            ddlTipoServico_SelectedIndexChanged(sender, e);

                            ddlSexo.SelectedIndex = 0;
                            ddlRegiaoMoradia.SelectedIndex = 0;
                            ddlCaracteristicasTerritorio.SelectedIndex = 0;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;

                            trDataReferencia.Visible = false;

                            chkFuncionamento.Visible = false;
                            chkDesativado.Visible = false;
                            Session["ATIVO"] = true;
                            break;
                        }
                    #endregion

                    #region 33. PrestacaoDeContasBasica</option>
                    case "33":
                        {
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoCronograma.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trExercicio.Visible = true;
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 34. PrestacaoDeContasMedia</option>
                    case "34":
                        {
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoCronograma.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trExercicio.Visible = true;
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 35. PrestacaoDeContasAlta</option>
                    case "35":
                        {
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoCronograma.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trExercicio.Visible = true;
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 36. PrestacaoDeContasBE</option>
                    case "36":
                        {
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoCronograma.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trExercicio.Visible = true;
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 37. PrestacaoDeContasPP</option>
                    case "37":
                        {
                            trTipoProtecao.Visible = false;
                            trTipoExecutora.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoCronograma.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trExercicio.Visible = true;
                            trDataReferencia.Visible = false;
                            break;
                        }
                    #endregion

                    #region 38. Status prestação de contas</option>
                    case "38":
                        {
                            txtDataInicial.Text = System.DateTime.Now.ToShortDateString();

                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";

                            trDataReferencia.Visible = true;

                            break;
                        }
                    #endregion

                    #region 39. Status Lei Orçamentaria</option>
                    case "39":
                        {
                            txtDataInicial.Text = System.DateTime.Now.ToShortDateString();

                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";

                            trDataReferencia.Visible = true;

                            break;
                        }
                    #endregion

                    #region 40. Status Execução financeira</option>
                    case "40":
                        {
                            txtDataInicial.Text = System.DateTime.Now.ToShortDateString();

                            trAbrangencia.Visible = true;
                            trMunicipioEscolha.Visible = false;
                            trServicoSubtificado.Visible = false;
                            trTipoProtecao.Visible = false;
                            trTipoServico.Visible = false;
                            trPublicoAlvo.Visible = false;
                            trTipoExecutora.Visible = false;
                            trLabelCaracteristicasUsuarios.Visible = false;
                            trSexo.Visible = false;
                            trRegiaoMoradia.Visible = false;
                            trCaracteristicasTerritorio.Visible = false;
                            trProblemaSocial.Visible = false;
                            trAcoes.Visible = false;
                            trProgramas.Visible = false;
                            trAbrangenciaServico.Visible = false;
                            trTipoBeneficioEventual.Visible = false;
                            trSituacoesEspecificas.Visible = false;
                            trSituacoesVulnerabilidade.Visible = false;
                            trTipoUnidade.Visible = false;
                            trAbrangenciaProgramaProjeto.Visible = false;
                            lblAbrangenciaServico.Text = "Abrangência : ";
                            lblAbrangenciaRelatorioddlRelatorio.Text = "Abrangência : ";

                            trDataReferencia.Visible = true;

                            break;
                        }
                    #endregion

                } 
                #endregion

                ExibirFiltroExercicio();

                adicionarFiltroAbrangencia(ddlRelatorio, ddlRelatorioDescritivo.SelectedItem.Value);
                adicionarFiltroTipoExecutoras(lstTipoExecutorasDisponiveis, ddlRelatorioDescritivo.SelectedItem.Value);
                ddlRelatorio.SelectedIndex = 0;
                ddlRelatorio_SelectedIndexChanged(sender, e);
                btnExcluirItemTipoExecutora_Click(sender, e);
                btnExcluirListaProgramas_Click(sender, e);

                var drop = sender as DropDownList;
                if (drop != null && drop.ID != "ddlRelatoriosQuantitativos")
                {
                    ddlRelatoriosQuantitativos.SelectedIndex = 0;
                    ddlRelatoriosQuantitativos_SelectedIndexChanged(null, e);
                    btnRelatorioQuantitativo.Enabled = false;
                }

                if (drop != null && drop.ID != "ddlRelatorioCadastral")
                {
                    ddlRelatorioCadastral.SelectedIndex = 0;
                    ddlRelatorioCadastral_SelectedIndexChanged(null, e);
                    btnRelatorioCadastral.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExibirFiltroExercicio()
        {
            switch (ddlRelatorioDescritivo.SelectedItem.Value)
            {

                case "3":
                    trExercicio.Visible = true;
                    break;
                case "4":
                    trExercicio.Visible = true;
                    break;
                case "6":
                    trExercicio.Visible = true;
                    break;
                case "9":
                case "12":
                case "17":
                    trExercicio.Visible = true;
                    break;
                case "31":
                case "22":
                case "28":
                case "19":
                    trExercicio.Visible = true;
                    break;
                case "27":
                    trExercicio.Visible = true;
                    break;
                case "29":
                    trExercicio.Visible = true;
                    break;
                case "33":
                    trExercicio.Visible = true;
                    break;
                case "34":
                    trExercicio.Visible = true;
                    break;
                case "35":
                    trExercicio.Visible = true;
                    break;
                case "36":
                    trExercicio.Visible = true;
                    break;
                case "37":
                    trExercicio.Visible = true;
                    break;
                case "41":
                    trExercicioAuxilioReclusaoPensaoMorte.Visible = true;
                    break;
                default:
                    trExercicio.Visible = false;
                    break;
            }
        }

        void adicionarFiltroAbrangencia(DropDownList drop, String report)
        {
            drop.Items.Clear();
            drop.Items.Add(new ListItem() { Value = "0", Text = "[Selecione uma opção:]" });
            drop.Items.Add(new ListItem() { Value = "1", Text = "Estado" });
            drop.Items.Add(new ListItem() { Value = "2", Text = "DRADS" });
            if (report != "2")
                drop.Items.Add(new ListItem() { Value = "3", Text = "Município" });
            drop.Items.Add(new ListItem() { Value = "4", Text = "Macrorregião" });
            if (report != "2")
            {
                drop.Items.Add(new ListItem() { Value = "5", Text = "Região Metropolitana" });
                drop.Items.Add(new ListItem() { Value = "6", Text = "Porte do munícipio" });
                drop.Items.Add(new ListItem() { Value = "7", Text = "Nível de Gestão do munícípio" });
            }
        }

        void adicionarFiltroTipoExecutoras(ListControl lst, String report)
        {
            lst.Items.Clear();

            if (report != "28")
            {
                lst.Items.Add(new ListItem() { Value = "3", Text = "Apenas CRAS" });
                lst.Items.Add(new ListItem() { Value = "4", Text = "Apenas CREAS" });
                lst.Items.Add(new ListItem() { Value = "5", Text = "Apenas Centro Pop" });
                lst.Items.Add(new ListItem() { Value = "1", Text = "Outros locais da rede direta" });
                lst.Items.Add(new ListItem() { Value = "2", Text = "Locais de execução da rede indireta" });

            }
            else
            {
                lst.Items.Add(new ListItem() { Value = "1", Text = "Rede direta" });
                lst.Items.Add(new ListItem() { Value = "2", Text = "Rede indireta" });
            }
        }

        void adicionarFiltroFormaAtuacao(DropDownList drop, String report)
        {
            drop.Items.Clear();

        }

        protected void rblTipoProtecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    ddlTipoServico.DataSource = proxy.Service.GetTiposServicoByTipoProtecaoSocial(Convert.ToInt32(ddlTipoProtecao.SelectedItem.Value));
                    ddlTipoServico.DataValueField = "Id";
                    ddlTipoServico.DataTextField = "Nome";
                    ddlTipoServico.DataBind();
                    ListItem itemToRemove = ddlTipoServico.Items.FindByValue("142");
                    if (itemToRemove != null)
                    {
                        ddlTipoServico.Items.Remove(itemToRemove);
                    }
                    ddlTipoServico.Items.Insert(0, new ListItem(" Selecione ", "0"));
                }


                //if (ddlRelatorioDescritivo.SelectedItem.Value == "8" && ddlTipoProtecao.SelectedIndex != 0)
                //{
                //    lstTiposAcoesDisponiveis.DataSource = quadro.RetornaListaAcoes(Convert.ToInt32(ddlTipoProtecao.SelectedItem.Value));
                //    lstTiposAcoesDisponiveis.DataValueField = "ATI_ID";
                //    lstTiposAcoesDisponiveis.DataTextField = "ATI_DESC";
                //    lstTiposAcoesDisponiveis.DataBind();
                //    trAcoes.Visible = true;
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnIncluirItemTipoExecutora_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstTipoExecutorasDisponiveis, lstTipoExecutorasSelecionadas);
        }
        protected void btnIncluirListaTipoExecutora_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstTipoExecutorasDisponiveis, lstTipoExecutorasSelecionadas);
        }
        protected void btnExcluirItemTipoExecutora_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstTipoExecutorasSelecionadas, lstTipoExecutorasDisponiveis);
        }
        protected void btnExcluirListaTipoExecutora_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstTipoExecutorasSelecionadas, lstTipoExecutorasDisponiveis);
        }

        protected void btnIncluirProgramas_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstProgramasDisponiveis, lstProgramasSelecionadas);
        }
        protected void btnIncluirListaProgramas_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstProgramasDisponiveis, lstProgramasSelecionadas);
        }
        protected void btnExcluirProgramas_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstProgramasSelecionadas, lstProgramasDisponiveis);
        }
        protected void btnExcluirListaProgramas_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstProgramasSelecionadas, lstProgramasDisponiveis);
        }

        protected void btnIncluirFormaAtuacao_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstFormaAtuacaoDisponiveis, lstFormaAtuacaoSelecionados);
        }

        protected void btnIncluirListaFormaAtuacao_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstFormaAtuacaoDisponiveis, lstFormaAtuacaoSelecionados);
        }

        protected void btnExcluirItemFormaAtuacao_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstFormaAtuacaoSelecionados, lstFormaAtuacaoDisponiveis);
        }

        protected void btnExcluiristaFormaAtuacao_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstFormaAtuacaoSelecionados, lstFormaAtuacaoDisponiveis);
        }
        #endregion

        #region quantitativo
        protected void btnRelatorioQuantitativo_Click(object sender, EventArgs e)
        {
            try
            {
                this.PreencherSessaoQuantitativo();
                String page = "";
                switch (ddlRelatoriosQuantitativos.SelectedValue)
                {
                    case "1": page = "RDistribuicaoPorteNivelGestao"; break;
                    case "2": page = "RQuantidadeUnidadesLocaisServicos"; break;
                    case "3": page = "RDistribuicaoSituacaoVulnerabilidade"; break;
                    default: return;
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirecionamento", "window.open('Relatorios/" + page + ".aspx','_blank');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rblSituacaoQuantitativo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //QuadroBase quadro = new QuadroBase();

                //ddlFocoQuantitativo.DataSource = quadro.RetornaProblemaSocialCategoria(Convert.ToInt32(ddlSituacaoQuantitativo.SelectedItem.Value));
                //ddlFocoQuantitativo.DataValueField = "PROCAT_ID";
                //ddlFocoQuantitativo.DataTextField = "PROCAT_DESC";
                //ddlFocoQuantitativo.DataBind();
                //ddlFocoQuantitativo.Items.Insert(0, new ListItem(" Selecione ", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlRelatoriosQuantitativos_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlRelatoriosQuantitativos.SelectedItem.Value)
            {
                case "1":
                    trTipoExecutoraAbragenciaQuantitativo.Visible = true;
                    break;
                case "2":
                    trTipoExecutoraAbragenciaQuantitativo.Visible = true;
                    break;
                case "3":
                    trTipoExecutoraAbragenciaQuantitativo.Visible = true;
                    break;

                default:
                    trTipoExecutoraAbragenciaQuantitativo.Visible = false;
                    break;
            }

            ddlAbrangenciaRelatorioQuantitativo.SelectedIndex = 0;
            ddlAbrangenciaRelatorioQuantitativo_SelectedIndexChanged(sender, e);
            btnRelatorioQuantitativo.Enabled = true;
            var drop = sender as DropDownList;
            if (drop != null && drop.ID != "ddlRelatorioDescritivo")
            {
                ddlRelatorioDescritivo.SelectedIndex = 0;
                ddlRelatorioDescritivo_SelectedIndexChanged(null, e);
                btnRelatorioDescritivo.Enabled = false;
            }

            if (drop != null && drop.ID != "ddlRelatorioCadastral")
            {
                ddlRelatorioCadastral.SelectedIndex = 0;
                ddlRelatorioCadastral_SelectedIndexChanged(null, e);
                btnRelatorioCadastral.Enabled = false;
            }

        }
        protected void btnIncluirItemDradsQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstDradsDisponiveisQuantitativo, lstDradsSelecionadasQuantitativo);
        }
        protected void btnIncluirListaDradsQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstDradsDisponiveisQuantitativo, lstDradsSelecionadasQuantitativo);
        }
        protected void btnExcluirItemDradsQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstDradsSelecionadasQuantitativo, lstDradsDisponiveisQuantitativo);
        }
        protected void btnExcluirListaDradsQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstDradsSelecionadasQuantitativo, lstDradsDisponiveisQuantitativo);
        }
        protected void btnIncluirMunicipioQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMunicipiosDisponiveisQuantitativo, lstMunicipiosSelecionadosQuantitativo);
        }
        protected void btnIncluirListaMunicipioQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMunicipiosDisponiveisQuantitativo, lstMunicipiosSelecionadosQuantitativo);
        }
        protected void btnExcluirMunicipioQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMunicipiosSelecionadosQuantitativo, lstMunicipiosDisponiveisQuantitativo);
        }
        protected void btnExcluirListaMunicipioQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMunicipiosSelecionadosQuantitativo, lstMunicipiosDisponiveisQuantitativo);
        }
        protected void btnIncluirItemMacroRegiaoQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMacroRegiaoDisponivelQuantitativo, lstmacroRegiaoSelecionadaQuantitativo);
        }
        protected void btnIncluirListaMacroRegiaoQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMacroRegiaoDisponivelQuantitativo, lstmacroRegiaoSelecionadaQuantitativo);
        }
        protected void btnExcluirItemMacroRegiaoQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstmacroRegiaoSelecionadaQuantitativo, lstMacroRegiaoDisponivelQuantitativo);
        }
        protected void btnExcluirListaMacroRegiaoQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstmacroRegiaoSelecionadaQuantitativo, lstMacroRegiaoDisponivelQuantitativo);
        }
        protected void btnIncluirItemRegiaoMetropolitanaQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstRegiaoMetropolitanaDisponivelQuantitativo, lstRegiaoMetropolitanaSelecionadaQuantitativo);
        }
        protected void btnIncluirListaRegiaoMetropolitanaQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstRegiaoMetropolitanaDisponivelQuantitativo, lstRegiaoMetropolitanaSelecionadaQuantitativo);
        }
        protected void btnExcluirItemRegiaoMetropolitanaQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstRegiaoMetropolitanaSelecionadaQuantitativo, lstRegiaoMetropolitanaDisponivelQuantitativo);
        }
        protected void btnExcluirListaRegiaoMetropolitanaQuantitativo_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstRegiaoMetropolitanaSelecionadaQuantitativo, lstRegiaoMetropolitanaDisponivelQuantitativo);
        }
        protected void ddlAbrangenciaRelatorioQuantitativo_SelectedIndexChanged(object sender, EventArgs e)
        {
            EOpcaoFiltro opcao = (EOpcaoFiltro)Convert.ToInt32(ddlAbrangenciaRelatorioQuantitativo.SelectedItem.Value);
            btnExcluirListaDradsQuantitativo_Click(sender, e);
            btnExcluirListaMunicipioQuantitativo_Click(sender, e);
            btnExcluirListaRegiaoMetropolitanaQuantitativo_Click(sender, e);
            btnExcluirListaMacroRegiaoQuantitativo_Click(sender, e);
            switch (opcao)
            {
                case EOpcaoFiltro.Estado:
                    trMunicipiosQuantitativo.Visible = false;
                    trDradsQuantitativo.Visible = false;
                    trMacroRegiaoQuantitativo.Visible = false;
                    trRegiaoMetropolitanaQuantitativo.Visible = false;
                    break;
                case EOpcaoFiltro.Drads:
                    lstDradsDisponiveisQuantitativo.DataSource = ProxyDivisaoAdministrativa.Drads;
                    lstDradsDisponiveisQuantitativo.DataTextField = "Nome";
                    lstDradsDisponiveisQuantitativo.DataValueField = "Id";
                    lstDradsDisponiveisQuantitativo.DataBind();
                    trDradsQuantitativo.Visible = true;
                    trMunicipiosQuantitativo.Visible = false;
                    trMacroRegiaoQuantitativo.Visible = false;
                    trRegiaoMetropolitanaQuantitativo.Visible = false;
                    break;
                case EOpcaoFiltro.Municipio:
                    lstMunicipiosDisponiveisQuantitativo.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
                    lstMunicipiosDisponiveisQuantitativo.DataTextField = "Nome";
                    lstMunicipiosDisponiveisQuantitativo.DataValueField = "Id";
                    lstMunicipiosDisponiveisQuantitativo.DataBind();
                    trMunicipiosQuantitativo.Visible = true;
                    trDradsQuantitativo.Visible = false;
                    trMacroRegiaoQuantitativo.Visible = false;
                    trRegiaoMetropolitanaQuantitativo.Visible = false;
                    break;
                case EOpcaoFiltro.MacroRegiao:
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lstMacroRegiaoDisponivelQuantitativo.DataSource = proxy.Service.GetMacroRegioes();
                        lstMacroRegiaoDisponivelQuantitativo.DataTextField = "Nome";
                        lstMacroRegiaoDisponivelQuantitativo.DataValueField = "Id";
                        lstMacroRegiaoDisponivelQuantitativo.DataBind();
                        trMacroRegiaoQuantitativo.Visible = true;
                        trMunicipiosQuantitativo.Visible = false;
                        trDradsQuantitativo.Visible = false;
                        trRegiaoMetropolitanaQuantitativo.Visible = false;
                    }
                    break;
                case EOpcaoFiltro.RegioesMetropolitanas:
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lstRegiaoMetropolitanaDisponivelQuantitativo.DataSource = proxy.Service.GetRegioesMetropolitanas();
                        lstRegiaoMetropolitanaDisponivelQuantitativo.DataTextField = "Nome";
                        lstRegiaoMetropolitanaDisponivelQuantitativo.DataValueField = "Id";
                        lstRegiaoMetropolitanaDisponivelQuantitativo.DataBind();
                        trRegiaoMetropolitanaQuantitativo.Visible = true;
                        trMunicipiosQuantitativo.Visible = false;
                        trDradsQuantitativo.Visible = false;
                        trMacroRegiaoQuantitativo.Visible = false;
                    }
                    break;
                default:
                    trMunicipiosQuantitativo.Visible = false;
                    trDradsQuantitativo.Visible = false;
                    trMacroRegiaoQuantitativo.Visible = false;
                    trRegiaoMetropolitanaQuantitativo.Visible = false;
                    break;
            }

        }
        #endregion

        #region cadastral
        protected void btnRelatorioCadastral_Click(object sender, EventArgs e)
        {
            try
            {
                this.PreencherSessaoCadastral();
                String page = "";
                switch (ddlRelatorioCadastral.SelectedValue)
                {
                    case "1": page = "RInformacoesCadastraisPrefeituras"; break;
                    case "2": page = "RInformacoesCadastraisOrgaoGestor"; break;
                    case "3": page = "RInformacoesCadastraisConselhoMunicipal"; break;
                    case "4": page = "RInformacoesCadastraisLocalExecucao"; break;
                    default: return;
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirecionamento", "window.open('Relatorios/" + page + ".aspx','_blank');", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlRelatorioCadastral_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (ddlRelatorioCadastral.SelectedItem.Value)
                {
                    case "1":
                    case "2":
                        trTipoExecutoraAbragenciaCadastral.Visible = true;
                        trTipoProtecaoCadastral.Visible = false;
                        trTipoServicoCadastral.Visible = false;
                        trPublicoAlvoCadastral.Visible = false;
                        trTipoExecutoraCadastral.Visible = false;
                        trTipoConselho.Visible = false;
                        break;
                    case "3":

                        using (var proxy = new ProxyEstruturaAssistenciaSocial())
                        {
                            ddlTipoConselho.DataSource = proxy.Service.GetTiposConselhos();
                            ddlTipoConselho.DataValueField = "Id";
                            ddlTipoConselho.DataTextField = "Nome";
                            ddlTipoConselho.DataBind();
                            ddlTipoConselho.Items.Insert(0, new ListItem("CMAS - Conselho Municipal de Assistência Social", "10"));
                            ddlTipoConselho.Items.Insert(0, new ListItem(" Selecione ", "0"));
                        }

                        trTipoProtecaoCadastral.Visible = false;
                        trTipoServicoCadastral.Visible = false;
                        trPublicoAlvoCadastral.Visible = false;
                        trTipoExecutoraCadastral.Visible = false;
                        trTipoExecutoraAbragenciaCadastral.Visible = true;
                        trTipoConselho.Visible = true;
                        ddlAbrangenciaRelatorioCadastral.SelectedIndex = 0;
                        break;
                    case "4":

                        using (var proxy = new ProxyEstruturaAssistenciaSocial())
                        {
                            ddlTipoProtecaoCadastral.DataSource = proxy.Service.GetTiposProtecaoSocial().Where(s => s.Id == 1 || s.Id == 2 || s.Id == 3);
                            ddlTipoProtecaoCadastral.DataValueField = "Id";
                            ddlTipoProtecaoCadastral.DataTextField = "Nome";
                            ddlTipoProtecaoCadastral.DataBind();
                            ddlTipoProtecaoCadastral.Items.Remove(ddlTipoProtecaoCadastral.Items.FindByText("Programas e Projetos"));
                            ddlTipoProtecaoCadastral.Items.Remove(ddlTipoProtecaoCadastral.Items.FindByText("Benefícios Eventuais"));
                            ddlTipoProtecaoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));
                        }

                        ddlPublicoAlvoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));

                        ddlTipoServicoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));

                        trTipoProtecaoCadastral.Visible = true;
                        trTipoServicoCadastral.Visible = true;
                        trPublicoAlvoCadastral.Visible = true;
                        trTipoExecutoraCadastral.Visible = true;
                        trTipoExecutoraAbragenciaCadastral.Visible = true;
                        trTipoConselho.Visible = false;
                        ddlAbrangenciaRelatorioCadastral.SelectedIndex = 0;
                        break;
                    default:
                        trTipoProtecaoCadastral.Visible = false;
                        trTipoServicoCadastral.Visible = false;
                        trPublicoAlvoCadastral.Visible = false;
                        trServicoSubtificadoCadastral.Visible = false;
                        trTipoExecutoraCadastral.Visible = false;
                        trTipoExecutoraAbragenciaCadastral.Visible = false;
                        trTipoConselho.Visible = false;
                        break;
                }

                ddlAbrangenciaRelatorioCadastral.SelectedIndex = 0;
                ddlAbrangenciaRelatorioCadastral_SelectedIndexChanged(sender, e);
                btnExcluirItemTipoExecutoraCadastral_Click(sender, e);
                btnRelatorioCadastral.Enabled = true;
                var drop = sender as DropDownList;
                if (drop != null && drop.ID != "ddlRelatorioDescritivo")
                {
                    ddlRelatorioDescritivo.SelectedIndex = 0;
                    ddlRelatorioDescritivo_SelectedIndexChanged(null, e);
                    btnRelatorioDescritivo.Enabled = false;
                }

                if (drop != null && drop.ID != "ddlRelatoriosQuantitativos")
                {
                    ddlRelatoriosQuantitativos.SelectedIndex = 0;
                    ddlRelatoriosQuantitativos_SelectedIndexChanged(null, e);
                    btnRelatorioQuantitativo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rblTipoProtecaoCadastral_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    ddlTipoServicoCadastral.DataSource = proxy.Service.GetTiposServicoByTipoProtecaoSocial(Convert.ToInt32(ddlTipoProtecaoCadastral.SelectedItem.Value));
                    ddlTipoServicoCadastral.DataValueField = "Id";
                    ddlTipoServicoCadastral.DataTextField = "Nome";
                    ddlTipoServicoCadastral.DataBind();
                    ddlTipoServicoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnIncluirItemTipoExecutoraCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstTipoExecutorasDisponiveisCadastral, lstTipoExecutorasSelecionadasCadastral);
        }
        protected void btnIncluirListaTipoExecutoraCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstTipoExecutorasDisponiveisCadastral, lstTipoExecutorasSelecionadasCadastral);
        }
        protected void btnExcluirItemTipoExecutoraCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstTipoExecutorasSelecionadasCadastral, lstTipoExecutorasDisponiveisCadastral);
        }
        protected void btnExcluirListaTipoExecutoraCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstTipoExecutorasSelecionadasCadastral, lstTipoExecutorasDisponiveisCadastral);
        }
        protected void btnIncluirItemDradsCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstDradsDisponiveisCadastral, lstDradsSelecionadasCadastral);
        }
        protected void btnIncluirListaDradsCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstDradsDisponiveisCadastral, lstDradsSelecionadasCadastral);
        }
        protected void btnExcluirItemDradsCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstDradsSelecionadasCadastral, lstDradsDisponiveisCadastral);
        }
        protected void btnExcluirListaDradsCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstDradsSelecionadasCadastral, lstDradsDisponiveisCadastral);
        }

        protected void btnIncluirMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMunicipiosDisponiveisCadastral, lstMunicipiosSelecionadosCadastral);
        }
        protected void btnIncluirListaMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMunicipiosDisponiveisCadastral, lstMunicipiosSelecionadosCadastral);
        }
        protected void btnExcluirMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMunicipiosSelecionadosCadastral, lstMunicipiosDisponiveisCadastral);
        }
        protected void btnExcluirListaMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMunicipiosSelecionadosCadastral, lstMunicipiosDisponiveisCadastral);
        }
        protected void btnIncluirItemMacroRegiaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstMacroRegiaoDisponivelCadastral, lstmacroRegiaoSelecionadaCadastral);
        }
        protected void btnIncluirListaMacroRegiaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstMacroRegiaoDisponivelCadastral, lstmacroRegiaoSelecionadaCadastral);
        }
        protected void btnExcluirItemMacroRegiaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstmacroRegiaoSelecionadaCadastral, lstMacroRegiaoDisponivelCadastral);
        }
        protected void btnExcluirListaMacroRegiaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstmacroRegiaoSelecionadaCadastral, lstMacroRegiaoDisponivelCadastral);
        }
        protected void btnIncluirItemRegiaoMetropolitanaCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstRegiaoMetropolitanaDisponivelCadastral, lstRegiaoMetropolitanaSelecionadaCadastral);
        }
        protected void btnIncluirListaRegiaoMetropolitanaCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstRegiaoMetropolitanaDisponivelCadastral, lstRegiaoMetropolitanaSelecionadaCadastral);
        }
        protected void btnExcluirItemRegiaoMetropolitanaCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstRegiaoMetropolitanaSelecionadaCadastral, lstRegiaoMetropolitanaDisponivelCadastral);
        }
        protected void btnExcluirListaRegiaoMetropolitanaCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstRegiaoMetropolitanaSelecionadaCadastral, lstRegiaoMetropolitanaDisponivelCadastral);
        }
        protected void btnIncluirItemPorteMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstPorteDisponiveisCadastral, lstPorteSelecionadosCadastral);
        }
        protected void btnIncluirListaPorteMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstPorteDisponiveisCadastral, lstPorteSelecionadosCadastral);
        }
        protected void btnExcluirItemPorteMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstPorteSelecionadosCadastral, lstPorteDisponiveisCadastral);
        }
        protected void btnExcluirListaPorteMunicipioCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstPorteSelecionadosCadastral, lstPorteDisponiveisCadastral);
        }
        protected void btnIncluirItemNivelGestaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstNivelGestaoDisponiveisCadastral, lstNivelGestaoSelecionadosCadastral);
        }
        protected void btnIncluirListaNivelGestaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstNivelGestaoDisponiveisCadastral, lstNivelGestaoSelecionadosCadastral);
        }
        protected void btnExcluirItemNivelGestaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstNivelGestaoSelecionadosCadastral, lstNivelGestaoDisponiveisCadastral);
        }
        protected void btnExcluirListaNivelGestaoCadastral_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstNivelGestaoSelecionadosCadastral, lstNivelGestaoDisponiveisCadastral);
        }

        protected void ddlAbrangenciaRelatorioCadastral_SelectedIndexChanged(object sender, EventArgs e)
        {
            EOpcaoFiltro enumRelatorio = (EOpcaoFiltro)Convert.ToInt32(ddlAbrangenciaRelatorioCadastral.SelectedItem.Value);

            btnExcluirListaDradsCadastral_Click(sender, e);
            btnExcluirListaMunicipioCadastral_Click(sender, e);
            btnExcluirListaRegiaoMetropolitanaCadastral_Click(sender, e);
            btnExcluirListaMacroRegiaoCadastral_Click(sender, e);
            btnExcluirListaPorteMunicipioCadastral_Click(sender, e);
            btnExcluirListaNivelGestaoCadastral_Click(sender, e);

            switch (enumRelatorio)
            {
                case EOpcaoFiltro.Estado:
                    trMunicipiosCadastral.Visible = false;
                    trDradsCadastral.Visible = false;
                    trMacroRegiaoCadastral.Visible = false;
                    trRegiaoMetropolitanaCadastral.Visible = false;
                    trPorteMunicipioCadastral.Visible = false;
                    trNivelGestaoCadastral.Visible = false;
                    break;
                case EOpcaoFiltro.Drads:
                    lstDradsDisponiveisCadastral.DataSource = ProxyDivisaoAdministrativa.Drads;
                    lstDradsDisponiveisCadastral.DataTextField = "Nome";
                    lstDradsDisponiveisCadastral.DataValueField = "Id";
                    lstDradsDisponiveisCadastral.DataBind();
                    trDradsCadastral.Visible = true;
                    trMunicipiosCadastral.Visible = false;
                    trMacroRegiaoCadastral.Visible = false;
                    trRegiaoMetropolitanaCadastral.Visible = false;
                    trPorteMunicipioCadastral.Visible = false;
                    trNivelGestaoCadastral.Visible = false;
                    break;
                case EOpcaoFiltro.Municipio:
                    lstMunicipiosDisponiveisCadastral.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
                    lstMunicipiosDisponiveisCadastral.DataTextField = "Nome";
                    lstMunicipiosDisponiveisCadastral.DataValueField = "Id";
                    lstMunicipiosDisponiveisCadastral.DataBind();
                    trMunicipiosCadastral.Visible = true;
                    trDradsCadastral.Visible = false;
                    trMacroRegiaoCadastral.Visible = false;
                    trRegiaoMetropolitanaCadastral.Visible = false;
                    trPorteMunicipioCadastral.Visible = false;
                    trNivelGestaoCadastral.Visible = false;
                    break;
                case EOpcaoFiltro.MacroRegiao:
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lstMacroRegiaoDisponivelCadastral.DataSource = proxy.Service.GetMacroRegioes();
                        lstMacroRegiaoDisponivelCadastral.DataTextField = "Nome";
                        lstMacroRegiaoDisponivelCadastral.DataValueField = "Id";
                        lstMacroRegiaoDisponivelCadastral.DataBind();
                        trMunicipiosCadastral.Visible = false;
                        trDradsCadastral.Visible = false;
                        trMacroRegiaoCadastral.Visible = true;
                        trRegiaoMetropolitanaCadastral.Visible = false;
                        trPorteMunicipioCadastral.Visible = false;
                        trNivelGestaoCadastral.Visible = false;
                    }
                    break;
                case EOpcaoFiltro.RegioesMetropolitanas:
                    using (var proxy = new ProxyDivisaoAdministrativa())
                    {
                        lstRegiaoMetropolitanaDisponivelCadastral.DataSource = proxy.Service.GetRegioesMetropolitanas();
                        lstRegiaoMetropolitanaDisponivelCadastral.DataTextField = "Nome";
                        lstRegiaoMetropolitanaDisponivelCadastral.DataValueField = "Id";
                        lstRegiaoMetropolitanaDisponivelCadastral.DataBind();
                        trMunicipiosCadastral.Visible = false;
                        trDradsCadastral.Visible = false;
                        trMacroRegiaoCadastral.Visible = false;
                        trRegiaoMetropolitanaCadastral.Visible = true;
                        trPorteMunicipioCadastral.Visible = false;
                        trNivelGestaoCadastral.Visible = false;
                    }
                    break;
                case EOpcaoFiltro.PorteMunicipios:
                    trMunicipiosCadastral.Visible = false;
                    trDradsCadastral.Visible = false;
                    trMacroRegiaoCadastral.Visible = false;
                    trRegiaoMetropolitanaCadastral.Visible = false;
                    trPorteMunicipioCadastral.Visible = true;
                    trNivelGestaoCadastral.Visible = false;
                    break;
                case EOpcaoFiltro.NivelGestao:
                    trMunicipiosCadastral.Visible = false;
                    trDradsCadastral.Visible = false;
                    trMacroRegiaoCadastral.Visible = false;
                    trRegiaoMetropolitanaCadastral.Visible = false;
                    trPorteMunicipioCadastral.Visible = false;
                    trNivelGestaoCadastral.Visible = true;
                    break;
                default:
                    trMunicipiosCadastral.Visible = false;
                    trDradsCadastral.Visible = false;
                    trMacroRegiaoCadastral.Visible = false;
                    trRegiaoMetropolitanaCadastral.Visible = false;
                    trPorteMunicipioCadastral.Visible = false;
                    trNivelGestaoCadastral.Visible = false;
                    break;
            }

        }
        protected void ddlTipoServicoCadastral_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    ddlPublicoAlvoCadastral.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlTipoServicoCadastral.SelectedItem.Value));
                    ddlPublicoAlvoCadastral.DataValueField = "Id";
                    ddlPublicoAlvoCadastral.DataTextField = "Nome";
                    ddlPublicoAlvoCadastral.DataBind();
                    ddlPublicoAlvoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));

                    ddlServicoSubtipificadoCadastral.DataSource = proxy.Service.GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Convert.ToInt32(ddlTipoProtecaoCadastral.SelectedValue));
                    ddlServicoSubtipificadoCadastral.DataValueField = "Id";
                    ddlServicoSubtipificadoCadastral.DataTextField = "Nome";
                    ddlServicoSubtipificadoCadastral.DataBind();
                    ddlServicoSubtipificadoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));

                    if (ddlTipoServicoCadastral.SelectedItem.Value == "138" || ddlTipoServicoCadastral.SelectedItem.Value == "145")
                    {
                        if (ddlServicoSubtipificadoCadastral.Items.Count > 1)
                            trServicoSubtificadoCadastral.Visible = true;
                        if (ddlTipoServicoCadastral.SelectedValue == "138")
                            ddlServicoSubtipificadoCadastral.Items.Remove(ddlServicoSubtipificadoCadastral.Items.FindByValue("160"));
                    }
                    else
                    {
                        trServicoSubtificadoCadastral.Visible = false;
                        ddlServicoSubtipificadoCadastral.Items.Clear();
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }
        protected void ddlTipoServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var proxy = new ProxyEstruturaAssistenciaSocial())
                {
                    ddlPublicoAlvo.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlTipoServico.SelectedItem.Value));
                    ddlPublicoAlvo.DataValueField = "Id";
                    ddlPublicoAlvo.DataTextField = "Nome";
                    ddlPublicoAlvo.DataBind();

                    ddlPublicoAlvo.Items.Insert(0, new ListItem(" Selecione ", "0"));

                    //welingtonPereira 20/08/2014 
                    //Inclusão de Filtro relatórios 8 e 9 Serviços Subtipificados
                    ddlServicoSubtipificado.DataSource = proxy.Service.GetTiposServicoNaoTipificadoByTipoProtecaoSocial(Convert.ToInt32(ddlTipoProtecao.SelectedValue));
                    ddlServicoSubtipificado.DataValueField = "Id";
                    ddlServicoSubtipificado.DataTextField = "Nome";
                    ddlServicoSubtipificado.DataBind();
                    ddlServicoSubtipificado.Items.Insert(0, new ListItem(" Selecione ", "0"));
                }

                if (ddlTipoServico.SelectedIndex != 0 && ddlRelatorioDescritivo.SelectedItem.Value == "7")
                {
                    trLabelCaracteristicasUsuarios.Visible = true;
                    trSexo.Visible = true;
                    trRegiaoMoradia.Visible = true;
                    trCaracteristicasTerritorio.Visible = true;
                    trProblemaSocial.Visible = true;
                }

                else
                {
                    trLabelCaracteristicasUsuarios.Visible = false;
                    trSexo.Visible = false;
                    trRegiaoMoradia.Visible = false;
                    trCaracteristicasTerritorio.Visible = false;
                    trProblemaSocial.Visible = false;
                }


                if (ddlTipoServico.SelectedItem.Value == "138" || ddlTipoServico.SelectedItem.Value == "145")
                {
                    if (ddlServicoSubtipificado.Items.Count > 1)
                        trServicoSubtificado.Visible = true;
                    if (ddlTipoServico.SelectedValue == "138")
                        ddlServicoSubtipificado.Items.Remove(ddlServicoSubtipificado.Items.FindByValue("160"));
                }
                else
                {
                    trServicoSubtificado.Visible = false;
                    ddlServicoSubtipificado.Items.Clear();
                }

                //trSubcategoria.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btnIncluirAcoes_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstTiposAcoesDisponiveis, lstTiposAcoesSelecionadas);
        }
        protected void btnIncluirListaAcoes_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstTiposAcoesDisponiveis, lstTiposAcoesSelecionadas);
        }
        protected void btnExcluirAcoes_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstTiposAcoesSelecionadas, lstTiposAcoesDisponiveis);
        }
        protected void btnExcluirListaAcoes_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstTiposAcoesSelecionadas, lstTiposAcoesDisponiveis);
        }

        protected void ddlServicoSubtipificado_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlPublicoAlvo.Items.Clear();

                ddlPublicoAlvo.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlServicoSubtipificado.SelectedValue));
                ddlPublicoAlvo.DataValueField = "Id";
                ddlPublicoAlvo.DataTextField = "Nome";
                ddlPublicoAlvo.DataBind();

                ddlPublicoAlvo.Items.Insert(0, new ListItem(" Selecione ", "0"));
            }
        }

        protected void btnIncluirCronograma_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstCronogramas, lstCronogramasEscolhidos);
        }

        protected void btnIncluirListaCronograma_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstCronogramas, lstCronogramasEscolhidos);
        }

        protected void btnExcluirCronograma_Click(object sender, EventArgs e)
        {
            Util.MoveSelectedItems(lstCronogramasEscolhidos, lstCronogramas);
        }

        protected void btnExcluirListaCronogramas_Click(object sender, EventArgs e)
        {
            Util.MoveAllItems(lstCronogramasEscolhidos, lstCronogramas);
        }

        protected void chkTotalCronograma_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTotalCronograma.Checked)
                lstCronogramas.Enabled = btnExcluirCronograma.Enabled = btnIncluirCronograma.Enabled = btnIncluirListaCronograma.Enabled = btnExcluirListaCronogramas.Enabled = false;
            else
                lstCronogramas.Enabled = btnExcluirCronograma.Enabled = btnIncluirCronograma.Enabled = btnIncluirListaCronograma.Enabled = btnExcluirListaCronogramas.Enabled = true;
        }

        protected void ddlTipoUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlRelatorioDescritivo.SelectedValue == "11") //(ddlRelatorioDescritivo.SelectedValue == "32")
            {
                btnIncluirFormaAtuacao.Enabled = btnIncluirListaFormaAtuacao.Enabled =
                btnExcluirItemFormaAtuacao.Enabled = btnExcluiristaFormaAtuacao.Enabled =
                lstFormaAtuacaoDisponiveis.Enabled = lstFormaAtuacaoSelecionados.Enabled = true;
                if (ddlTipoUnidade.SelectedValue == "1")
                {
                    btnIncluirFormaAtuacao.Enabled = btnIncluirListaFormaAtuacao.Enabled =
                    btnExcluirItemFormaAtuacao.Enabled = btnExcluiristaFormaAtuacao.Enabled =
                 lstFormaAtuacaoDisponiveis.Enabled = lstFormaAtuacaoSelecionados.Enabled = false;
                    Util.MoveAllItems(lstFormaAtuacaoSelecionados, lstFormaAtuacaoDisponiveis);
                }
            }
        }

        protected void ddlServicoSubtipificadoCadastral_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlPublicoAlvoCadastral.Items.Clear();

                ddlPublicoAlvoCadastral.DataSource = proxy.Service.GetUsuariosByTipoServico(Convert.ToInt32(ddlServicoSubtipificadoCadastral.SelectedValue));
                ddlPublicoAlvoCadastral.DataValueField = "Id";
                ddlPublicoAlvoCadastral.DataTextField = "Nome";
                ddlPublicoAlvoCadastral.DataBind();

                ddlPublicoAlvoCadastral.Items.Insert(0, new ListItem(" Selecione ", "0"));
            }
        }


        protected void imgBntDataInicial_Click(object sender, ImageClickEventArgs e)
        {
            if (DataInicial.Visible)
            {
                DataInicial.Visible = false;
            }
            else
            {
                DataInicial.Visible = true;
            }
        }

        protected void DataInicial_SelectionChanged(object sender, EventArgs e)
        {
            txtDataInicial.Text = DataInicial.SelectedDate.ToShortDateString();
            DataInicial.Visible = false;

            
        }

        protected void DataInicial_DayRender(object sender, DayRenderEventArgs e)
        {

            if (e.Day.IsOtherMonth )
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
            }

            if (e.Day.Date.Year < 2018 )
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
            }

            if (e.Day.Date > DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
            }

        }

        protected void chkFuncionamento_CheckedChanged(object sender, EventArgs e)
        {
            chkDesativado.Checked = false;
        }

        protected void chkDesativado_CheckedChanged(object sender, EventArgs e)
        {
            chkFuncionamento.Checked = false;
        }



    }
}