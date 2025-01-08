using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIV
{
    public partial class FInterfacePolitica : System.Web.UI.Page
    {

        protected List<InterfacePublicaAlimentacaoRestauranteInfo> Restaurantes //Welington P.
        {
            get { return Session["RESTAURANTES"] as List<InterfacePublicaAlimentacaoRestauranteInfo>; }
            set { Session["RESTAURANTES"] = value; }
        }


        protected List<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo> OutrasFormas //Welington P.
        {
            get { return Session["OUTRAS_FORMAS"] as List<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo>; }
            set { Session["OUTRAS_FORMAS"] = value; }
        }

        protected List<InterfacePublicaAlimentacaoOutraAcaoInfo> OutrasAcoes //Welington P.
        {
            get { return Session["OUTRAS_ACOES"] as List<InterfacePublicaAlimentacaoOutraAcaoInfo>; }
            set { Session["OUTRAS_ACOES"] = value; }
        }


        protected List<InterfacePublicaOutroServicoInfo> OutrosServicos
        {
            get { return Session["OUTROS_SERVICOS"] as List<InterfacePublicaOutroServicoInfo>; }
            set { Session["OUTROS_SERVICOS"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                adicionarEventos();
                using (var proxy = new ProxyInterfacePolitica())
                {
                    load(proxy);
                }


            }
            Button btn = (Button)cep1.FindControl("cmdPesqCEP");
            btn.Click += new EventHandler(this.CEPBtn_Click);
            //}
        }

        private void CEPBtn_Click(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
        }


        void adicionarEventos()
        {
            txtValorRepasssado.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
        }


        void load(ProxyInterfacePolitica proxy)
        {
            carregarEducacao(proxy);
            carregarSaude(proxy);
            carregarEmprego(proxy);
            carregarAlimentacao(proxy);
            carregarOutraPolitica(proxy);
            frame1_1.Attributes.Add("class", "active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            BloquearControles();
        }
        private void BloquearControles()
        {
            WebControl[] controles = {
                                     rblProtocoloFormalEducacao,
                                     txtJustificativaProtocoloEducacao,
                                     rblIntervencaoConjuntaEducao,
                                     txtJustificativaIntervencao,
                                     rblIntervencaoConjuntaBPC,
                                     txtJustificativaConjuntaBPC,
                                     rblIntervencaoConjuntaAcaoJovem,
                                     txtJustificativaAcaoJovem,
                                     rblOutrasArticulacoesEducacao,
                                     txtJustificativaOutrasArticulacoesEducacao,
                                     btnSalvarEducacao,
                                     rblExecutaAcaoAlimentar,
                                     rblProtocoloFormalSaude,
                                     txtProtocoloFormalSaude,
                                     rblIntervencaoBolsaFamilia,
                                     txtJustificativaIntervencaoBolsaFamilia,
                                     rblIntervencaoSaudeBPC,
                                     txtJustificativaIntervencaoSaudeBPC,
                                     rblIntervencaoConjuntaVitimasExploracao,
                                     txtIntervencaoConjuntaVitimasExploracao,
                                     txtJustificativaAcaoJovem,
                                     rblIntervencaoConjuntaSaude,
                                     txtJustificativaIntervencaoConjuntaSaude,
                                     btnSalvarSaude,
                                     txtJustificativaIntervencaoPoliticaEmpregoPCD,
                                     rblExecutaAcaoAlimentar,
                                     chkRestaurantePopular,
                                     chkNaoPossuiInformacao,
                                     chkGestaoDiretaEstado,
                                     rbParceria,
                                     txtNomeRestaurante,
                                     txtUnidade,
                                     btnAdicionarBomPrato,
                                     rblIntervencaoPoliticaEmpregoPCD,
                                     rblIntervencaoSaudeBPC,
                                     rblOrgaoGestor,
                                     rblOutrasAcoesEmprego,
                                     rbConvenioBomPrato,
                                     rblExecutaAcaoAlimentar,
                                     chkDistribuicaoAlimentos,
                                     chkVivaleite,
                                     rblOrgaoGestor,
                                     rblDistribuidor,
                                     cblEntidades,
                                     chkOutraFormaDistribuicao,
                                     txtDescricao,
                                     txtResponsavel,
                                     btnAdicionarFormaDistribuicao,
                                     chkOutraAcao,
                                     txtAcaoDesenvolvida,
                                     txtOrgaoResponsavelAcao,
                                     btnSalvarAlimentacao,
                                     rblIntervencaoPoliticaEmprego,
                                     txtJustificativaIntervencaoPoliticaEmprego,
                                     rblIntervencaoPoliticaEmpregoPCD,
                                     rblOutrasAcoesEmprego,
                                     txtOutrasAcoesEmprego,
                                     btnSalvarEmprego,
                                     rblOutrasPoliticasPublicas,
                                     txtOutrasPoliticasPublicas,
                                     txtPrincipaisObstaculos,
                                     rblOutrosProgramasFinanciados,
                                     txtProgramaProjeto,
                                     txtPoliticaPublica,
                                     txtValorRepasssado,
                                     btnAdicionarOutrosServicos,
                                     btnSalvarInterface,};

            Permissao.VerificarPermissaoControles(controles, Session);
            Permissao.VerificarPermissaoControles(txtDataInicioRestaurante.Controles, Session);
        }
        void carregarOutraPolitica(ProxyInterfacePolitica proxy)
        {
            var obj = proxy.Service.GetInterfacePublicaOutraPoliticaByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
            {
                hdfIdInterfacePublica.Value = Genericos.clsCrypto.Encrypt("0");
                return;
            }
            hdfIdInterfacePublica.Value = Genericos.clsCrypto.Encrypt(obj.Id.ToString());
            rblOutrasPoliticasPublicas.SelectedValue = obj.ExisteOutraPoliticaPublica.HasValue ? Convert.ToInt32(obj.ExisteOutraPoliticaPublica.Value).ToString() : String.Empty;
            rblOutrasPoliticasPublicas_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.Descricao))
                txtOutrasPoliticasPublicas.Text = obj.Descricao;

            if (!String.IsNullOrEmpty(obj.DescricaoPrincipaisObstaculos))
                txtPrincipaisObstaculos.Text = obj.DescricaoPrincipaisObstaculos;


            rblOutrosProgramasFinanciados.SelectedValue = obj.ExistemServicosFinanciados.HasValue ? Convert.ToInt32(obj.ExistemServicosFinanciados.Value).ToString() : String.Empty;
            rblOutrosProgramasFinanciados_SelectedIndexChanged(null, null);
            if (obj.OutrosServicos.Count > 0 && rblOutrosProgramasFinanciados.SelectedValue == "1")
            {
                OutrosServicos = obj.OutrosServicos;
                carregarOutrosServicos();
            }
        }
        void carregarEmprego(ProxyInterfacePolitica proxy)
        {
            var obj = proxy.Service.GetInterfacePublicaEmpregoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
            {
                hdfIdEmprego.Value = Genericos.clsCrypto.Encrypt("0");
                return;
            }
            hdfIdEmprego.Value = Genericos.clsCrypto.Encrypt(obj.Id.ToString());

            rblIntervencaoPoliticaEmprego.SelectedValue = obj.ExisteIntervencaoJovens.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoJovens.Value).ToString() : String.Empty;
            rblIntervencaoPoliticaEmprego_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoIntervencaoJovens))
                txtJustificativaIntervencaoPoliticaEmprego.Text = obj.DescricaoIntervencaoJovens.ToString();

            rblIntervencaoPoliticaEmpregoPCD.SelectedValue = obj.ExisteIntervencaoPCD.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoPCD.Value).ToString() : String.Empty;
            rblIntervencaoPoliticaEmpregoPCD_SelectedIndexChanged(null, null);

            if (!String.IsNullOrEmpty(obj.DescricaoIntervencaoPCD))
                txtJustificativaIntervencaoPoliticaEmpregoPCD.Text = obj.DescricaoIntervencaoPCD.ToString();

            rblOutrasAcoesEmprego.SelectedValue = obj.ExisteOutrasAcoesArticuladas.HasValue ? Convert.ToInt32(obj.ExisteOutrasAcoesArticuladas.Value).ToString() : String.Empty;
            rblOutrasAcoesEmprego_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoOutrasAcoesArticuladas))
                txtOutrasAcoesEmprego.Text = obj.DescricaoOutrasAcoesArticuladas.ToString();
        }
        void carregarSaude(ProxyInterfacePolitica proxy)
        {
            var obj = proxy.Service.GetInterfacePublicaSaudeByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
            {
                hdfIdSaude.Value = Genericos.clsCrypto.Encrypt("0");
                return;
            }

            hdfIdSaude.Value = Genericos.clsCrypto.Encrypt(obj.Id.ToString());
            rblProtocoloFormalSaude.SelectedValue = obj.ExisteProtocoloAtendimentoSaude.HasValue ? Convert.ToInt32(obj.ExisteProtocoloAtendimentoSaude.Value).ToString() : String.Empty;
            rblProtocoloFormalSaude_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoProtocoloAtendimentoSaude))
                txtProtocoloFormalSaude.Text = obj.DescricaoProtocoloAtendimentoSaude.ToString();


            rblIntervencaoBolsaFamilia.SelectedValue = obj.ExisteIntervencaoBolsaFamilia.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoBolsaFamilia.Value).ToString() : String.Empty;
            rblIntervencaoBolsaFamilia_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoIntervencaoBolsaFamilia))
                txtJustificativaIntervencaoBolsaFamilia.Text = obj.DescricaoIntervencaoBolsaFamilia.ToString();

            rblIntervencaoSaudeBPC.SelectedValue = obj.ExisteIntervencaoBPC.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoBPC.Value).ToString() : String.Empty;
            rblIntervencaoSaudeBPC_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoIntervencaoBPC))
                txtJustificativaIntervencaoSaudeBPC.Text = obj.DescricaoIntervencaoBPC.ToString();

            rblIntervencaoConjuntaVitimasExploracao.SelectedValue = obj.ExisteIntervencaoVitimas.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoVitimas.Value).ToString() : String.Empty;
            rblIntervencaoConjuntaVitimasExploracao_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoIntervencaoVitimas))
                txtIntervencaoConjuntaVitimasExploracao.Text = obj.DescricaoIntervencaoVitimas.ToString();


            rblIntervencaoConjuntaSaude.SelectedValue = obj.ExisteIntervencaoIdosoPCD.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoIdosoPCD.Value).ToString() : String.Empty;
            rblIntervencaoConjuntaSaude_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoIntervencaoIdosoPCD))
                txtJustificativaIntervencaoConjuntaSaude.Text = obj.DescricaoIntervencaoIdosoPCD.ToString();

        }
        void carregarEducacao(ProxyInterfacePolitica proxy)
        {
            var obj = proxy.Service.GetInterfacePublicaEducacaoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
            {
                hidfIdEducacao.Value = Genericos.clsCrypto.Encrypt("0");
                return;
            }
            hidfIdEducacao.Value = Genericos.clsCrypto.Encrypt(obj.Id.ToString());
            rblProtocoloFormalEducacao.SelectedValue = obj.ExisteProtocoloFormal.HasValue ? Convert.ToInt32(obj.ExisteProtocoloFormal.Value).ToString() : String.Empty;
            rblProtocoloFormalEducacao_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DescricaoEncaminhamentoEducacao))
                txtJustificativaProtocoloEducacao.Text = obj.DescricaoEncaminhamentoEducacao.ToString();


            rblIntervencaoConjuntaEducao.SelectedValue = obj.ExisteIntervencaoBolsaFamilia.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoBolsaFamilia.Value).ToString() : String.Empty;
            rblIntervencaoConjuntaEducao_SelectedIndexChanged(null, null);
            txtJustificativaIntervencao.Text = !String.IsNullOrEmpty(obj.DescricaoIntervencaoBolsaFamilia) ? obj.DescricaoIntervencaoBolsaFamilia.ToString() : String.Empty;

            rblIntervencaoConjuntaBPC.SelectedValue = obj.ExisteIntervencaoBPC.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoBPC.Value).ToString() : String.Empty;
            rblIntervencaoConjuntaBPC_SelectedIndexChanged(null, null);
            txtJustificativaConjuntaBPC.Text = !String.IsNullOrEmpty(obj.DescricaoIntervencaoBPC) ? obj.DescricaoIntervencaoBPC.ToString() : String.Empty;

            rblIntervencaoConjuntaAcaoJovem.SelectedValue = obj.ExisteIntervencaoAcaoJovem.HasValue ? Convert.ToInt32(obj.ExisteIntervencaoAcaoJovem.Value).ToString() : String.Empty;
            rblIntervencaoConjuntaAcaoJovem_SelectedIndexChanged(null, null);
            txtJustificativaAcaoJovem.Text = !String.IsNullOrEmpty(obj.DescricaoIntervencaAcaoJovem) ? obj.DescricaoIntervencaAcaoJovem.ToString() : String.Empty;


            rblOutrasArticulacoesEducacao.SelectedValue = obj.ExisteOutrasArticulacoes.HasValue ? Convert.ToInt32(obj.ExisteOutrasArticulacoes.Value).ToString() : String.Empty;
            rblOutrasArticulacoesEducacao_SelectedIndexChanged(null, null);
            txtJustificativaOutrasArticulacoesEducacao.Text = !String.IsNullOrEmpty(obj.DescricaoOutrasArticulacoes) ? obj.DescricaoOutrasArticulacoes.ToString() : String.Empty;
        }
        void carregarAlimentacao(ProxyInterfacePolitica proxy)
        {
            var obj = proxy.Service.GetInterfacePublicaAlimentacaoByPrefeitura(SessaoPmas.UsuarioLogado.Prefeitura.Id);
            if (obj == null)
            {
                hdfIdAlimentacao.Value = Genericos.clsCrypto.Encrypt("0");
                return;
            }
            hdfIdAlimentacao.Value = Genericos.clsCrypto.Encrypt(obj.Id.ToString());
            rblExecutaAcaoAlimentar.SelectedValue = obj.ExecutaAcaoAlimentar.ToString();
            rblExecutaAcaoAlimentar_SelectedIndexChanged(null, null);
            chkRestaurantePopular.Checked = obj.RestaurantePopular;
            chkRestaurantePopular_CheckedChanged(null, null);
            chkNaoPossuiInformacao.Checked = obj.NaoPossuiInformacaoRestaurante;
            chkNaoPossuiInformacao_CheckedChanged(null, null);
            chkGestaoDiretaEstado.Checked = obj.GestaoDiretaEstado;
            chkGestaoDiretaEstado_CheckedChanged(null, null);
            rbConvenioBomPrato.SelectedIndex = -1;
            rbParceria.SelectedIndex = -1;

            if (obj.Restaurantes.Count > 0)
            {
                Restaurantes = obj.Restaurantes;
                carregarRestaurantes();
            }

            chkDistribuicaoAlimentos.Checked = obj.DistribuicaoAlimentos;
            chkDistribuicaoAlimentos_CheckedChanged(null, null);

            chkVivaleite.Checked = obj.ExecutaDistribuicaoVivaleite;
            chkVivaleite_CheckedChanged(null, null);

            rblOrgaoGestor.SelectedValue = obj.GestaoVivaleiteOrgaoGestor.HasValue ? Convert.ToInt32(obj.GestaoVivaleiteOrgaoGestor.Value).ToString() : String.Empty;
            rblOrgaoGestor_SelectedIndexChanged(null, null);
            rblDistribuidor.SelectedValue = obj.TipoDistribuidor.HasValue ? Convert.ToInt32(obj.TipoDistribuidor.Value).ToString() : String.Empty;
            rblDistribuidor_SelectedIndexChanged(null, null);
            if (!String.IsNullOrEmpty(obj.DecricaoOutraPoliticaDistribuicao))
                txtOutraPoliticaPublica.Text = obj.DecricaoOutraPoliticaDistribuicao;

            if (obj.DistribuicoesAlimentos.Count != null && obj.DistribuicoesAlimentos.Count > 0)
            {
                foreach (ListItem item in cblEntidades.Items)
                {
                    item.Selected = obj.DistribuicoesAlimentos.Any(s => s.IdUnidadePrivada == Convert.ToInt32(item.Value));
                }
            }

            chkOutraFormaDistribuicao.Checked = obj.OutraFormaDistribuicao.HasValue ? obj.OutraFormaDistribuicao.Value : false;
            chkOutraFormaDistribuicao_CheckedChanged(null, null);

            if (obj.FormasDistribuicoesAlimentos.Count != null && obj.FormasDistribuicoesAlimentos.Count > 0)
            {
                OutrasFormas = obj.FormasDistribuicoesAlimentos;
                carregarOutrasFormas();
            }
            chkOutraAcao.Checked = obj.OutraFormaAcao.HasValue ? obj.OutraFormaAcao.Value : false;
            chkOutraAcao_CheckedChanged(null, null);
            if (obj.OutrasAcoes.Count != null && obj.OutrasAcoes.Count > 0)
            {
                OutrasAcoes = obj.OutrasAcoes;
                carregarOutrasAcoes();
            }
        }
        void PreencherEducacao()
        {
            var objInterfaceEducacao = new InterfacePublicaEducacaoInfo();
            objInterfaceEducacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            objInterfaceEducacao.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hidfIdEducacao.Value));

            if (!String.IsNullOrEmpty(rblProtocoloFormalEducacao.SelectedValue))
            {
                objInterfaceEducacao.ExisteProtocoloFormal = rblProtocoloFormalEducacao.SelectedValue == "1" ? true : false;
                objInterfaceEducacao.DescricaoEncaminhamentoEducacao = txtJustificativaProtocoloEducacao.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoConjuntaEducao.SelectedValue))
            {
                objInterfaceEducacao.ExisteIntervencaoBolsaFamilia = rblIntervencaoConjuntaEducao.SelectedValue == "1" ? true : false;
                if (objInterfaceEducacao.ExisteIntervencaoBolsaFamilia.Value)
                    objInterfaceEducacao.DescricaoIntervencaoBolsaFamilia = txtJustificativaIntervencao.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoConjuntaBPC.SelectedValue))
            {
                objInterfaceEducacao.ExisteIntervencaoBPC = rblIntervencaoConjuntaBPC.SelectedValue == "1" ? true : false;
                if (objInterfaceEducacao.ExisteIntervencaoBPC.Value)
                    objInterfaceEducacao.DescricaoIntervencaoBPC = txtJustificativaConjuntaBPC.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoConjuntaAcaoJovem.SelectedValue))
            {
                objInterfaceEducacao.ExisteIntervencaoAcaoJovem = rblIntervencaoConjuntaAcaoJovem.SelectedValue == "1" ? true : false;
                if (objInterfaceEducacao.ExisteIntervencaoAcaoJovem.Value)
                    objInterfaceEducacao.DescricaoIntervencaAcaoJovem = txtJustificativaAcaoJovem.Text;
            }
            if (!String.IsNullOrEmpty(rblOutrasArticulacoesEducacao.SelectedValue))
            {
                objInterfaceEducacao.ExisteOutrasArticulacoes = rblOutrasArticulacoesEducacao.SelectedValue == "1" ? true : false;
                if (objInterfaceEducacao.ExisteOutrasArticulacoes.Value)
                    objInterfaceEducacao.DescricaoOutrasArticulacoes = txtJustificativaOutrasArticulacoesEducacao.Text;
            }
            using (var proxy = new ProxyInterfacePolitica())
            {
                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(hidfIdEducacao.Value)) == 0)
                {
                    var id = proxy.Service.AddInterfacePublicaEducacao(objInterfaceEducacao);
                    hidfIdEducacao.Value = Genericos.clsCrypto.Encrypt(id.ToString());
                }
                else
                {
                    proxy.Service.UpdateInterfacePublicaEducacao(objInterfaceEducacao);
                }
            }

        }
        void preencherEmprego()
        {
            var objInterfaceEmprego = new InterfacePublicaEmpregoInfo();
            objInterfaceEmprego.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            objInterfaceEmprego.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdEmprego.Value));
            if (!String.IsNullOrEmpty(rblIntervencaoPoliticaEmprego.SelectedValue) || rblIntervencaoPoliticaEmprego.SelectedValue == "1")
            {
                objInterfaceEmprego.ExisteIntervencaoJovens = rblIntervencaoPoliticaEmprego.SelectedValue == "1" ? true : false;
                objInterfaceEmprego.DescricaoIntervencaoJovens = txtJustificativaIntervencaoPoliticaEmprego.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoPoliticaEmpregoPCD.SelectedValue) || rblIntervencaoPoliticaEmpregoPCD.SelectedValue == "1")
            {
                objInterfaceEmprego.ExisteIntervencaoPCD = rblIntervencaoPoliticaEmpregoPCD.SelectedValue == "1" ? true : false;
                objInterfaceEmprego.DescricaoIntervencaoPCD = txtJustificativaIntervencaoPoliticaEmpregoPCD.Text;
            }

            if (!String.IsNullOrEmpty(rblOutrasAcoesEmprego.SelectedValue))
            {
                objInterfaceEmprego.ExisteOutrasAcoesArticuladas = rblOutrasAcoesEmprego.SelectedValue == "1" ? true : false;
                objInterfaceEmprego.DescricaoOutrasAcoesArticuladas = txtOutrasAcoesEmprego.Text;
            }
            using (var proxy = new ProxyInterfacePolitica())
            {

                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdEmprego.Value)) == 0)
                {
                    var id = proxy.Service.AddInterfacePublicaEmprego(objInterfaceEmprego);
                    hdfIdEmprego.Value = Genericos.clsCrypto.Encrypt(id.ToString());
                }
                else
                    proxy.Service.UpdateInterfacePublicaEmprego(objInterfaceEmprego);
            }
        }
        void preencherOutraPolitica()
        {
            var objInterfaceOutraPolitica = new InterfacePublicaOutraPoliticaInfo();
            objInterfaceOutraPolitica.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            objInterfaceOutraPolitica.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdInterfacePublica.Value));
            objInterfaceOutraPolitica.DescricaoPrincipaisObstaculos = txtPrincipaisObstaculos.Text;
            if (!String.IsNullOrEmpty(rblOutrasPoliticasPublicas.SelectedValue))
            {
                objInterfaceOutraPolitica.ExisteOutraPoliticaPublica = Convert.ToBoolean(rblOutrasPoliticasPublicas.SelectedValue == "1");
                if (objInterfaceOutraPolitica.ExisteOutraPoliticaPublica.Value)
                    objInterfaceOutraPolitica.Descricao = txtOutrasPoliticasPublicas.Text;
                //if(objInterfaceOutraPolitica.ExisteOutraPoliticaPublica.Value)
                //if (objInterfaceOutraPolitica.ExisteOutraPoliticaPublica.HasValue)
            }
            if (!String.IsNullOrEmpty(rblOutrosProgramasFinanciados.SelectedValue))
            {
                objInterfaceOutraPolitica.ExistemServicosFinanciados = rblOutrosProgramasFinanciados.SelectedValue == "1";
                if (objInterfaceOutraPolitica.ExistemServicosFinanciados.Value)
                    objInterfaceOutraPolitica.OutrosServicos = OutrosServicos;
                else
                    if (OutrosServicos != null)
                    {
                        OutrosServicos.Clear();
                        objInterfaceOutraPolitica.OutrosServicos = OutrosServicos;
                    }
            }
            using (var proxy = new ProxyInterfacePolitica())
            {
                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdInterfacePublica.Value)) == 0)
                {
                    var id = proxy.Service.AddInterfacePublicaOutraPolitica(objInterfaceOutraPolitica);
                    hdfIdInterfacePublica.Value = Genericos.clsCrypto.Encrypt(id.ToString());
                }
                else
                    proxy.Service.UpdateInterfacePublicaOutraPolitica(objInterfaceOutraPolitica);
            }
        }

        #region Educação
        protected void rblProtocoloFormalEducacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);
            frame1_1.Attributes.Add("class", "active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblProtocoloFormalEducacao.SelectedValue == "1")
            {
                trJustificativaProtocoloEducacao.Visible = true;
                lblJustificativaProtocoloEducacao.Text = "Descreva o protocolo estabelecido para atendimento dos usuários encaminhados pelos CRAS e CREAS";
            }
            else
            {
                trJustificativaProtocoloEducacao.Visible = false;
                lblJustificativaProtocoloEducacao.Text = "Descreva de que maneira o CRAS e o CREAS realizam os encaminhamentos de usuários para o serviço de educação";
            }
        }
        protected void rblIntervencaoConjuntaEducao_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoConjuntaEducao.SelectedValue == "1")
                trJustificativaIntervencao.Visible = true;
            else
            {
                trJustificativaIntervencao.Visible = false;
                txtJustificativaIntervencao.Text = String.Empty;
            }

        }
        protected void rblIntervencaoConjuntaBPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoConjuntaBPC.SelectedValue == "1")
                trJustificativaConjuntaBPC.Visible = true;
            else
            {
                trJustificativaConjuntaBPC.Visible = false;
                txtJustificativaConjuntaBPC.Text = String.Empty;
            }
        }
        protected void rblIntervencaoConjuntaAcaoJovem_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoConjuntaAcaoJovem.SelectedValue == "1")
                trJustificativaAcaoJovem.Visible = true;
            else
            {
                trJustificativaAcaoJovem.Visible = false;
                txtJustificativaAcaoJovem.Text = String.Empty;
            }
        }
        protected void rblOutrasArticulacoesEducacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_1.Attributes.Add("class", "active");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblOutrasArticulacoesEducacao.SelectedValue == "1")
                trJustificativaOutrasArticulacoesEducacao.Visible = true;
            else
            {
                trJustificativaOutrasArticulacoesEducacao.Visible = false;
                txtJustificativaOutrasArticulacoesEducacao.Text = String.Empty;
            }
        }

        protected void btnSalvarEducacao_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
            try
            {
                PreencherEducacao();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Interface Politica registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                return;
            }
        }
        #endregion


        #region Saúde
        protected void rblProtocoloFormalSaude_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_2.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (rblProtocoloFormalSaude.SelectedValue == "1")
            {
                trProtocoloFormalSaude.Visible = true;
                txtProtocoloFormalSaude.Text = String.Empty;
                lblProtocoloFormalSaude.Text = "Descreva o protocolo estabelecido para atendimento dos usuários encaminhados pelos CRAS, CREAS ou Centro Pop";
            }
            else
            {
                trProtocoloFormalSaude.Visible = false;
                txtProtocoloFormalSaude.Text = String.Empty;
                lblProtocoloFormalSaude.Text = "Descreva de que maneira o CRAS, CREAS e Centro Pop realizam os encaminhamentos de usuários para o serviço de saúde";
            }
        }
        protected void rblIntervencaoBolsaFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_2.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoBolsaFamilia.SelectedValue == "1")
                trJustificativaIntervencaoBolsaFamilia.Visible = true;
            else
                trJustificativaIntervencaoBolsaFamilia.Visible = false;
        }
        protected void rblIntervencaoSaudeBPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_2.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoSaudeBPC.SelectedValue == "1")
                trJustificativaIntervencaoSaudeBPC.Visible = true;
            else
                trJustificativaIntervencaoSaudeBPC.Visible = false;
        }
        protected void rblIntervencaoConjuntaVitimasExploracao_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_2.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoConjuntaVitimasExploracao.SelectedValue == "1")
                trIntervencaoConjuntaVitimasExploracao.Visible = true;
            else
                trIntervencaoConjuntaVitimasExploracao.Visible = false;
        }
        protected void rblIntervencaoConjuntaSaude_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_2.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoConjuntaSaude.SelectedValue == "1")
                trJustificativaIntervencaoConjuntaSaude.Visible = true;
            else
                trJustificativaIntervencaoConjuntaSaude.Visible = false;

        }
        protected void btnSalvarSaude_Click(object sender, EventArgs e)
        {
            String msg = String.Empty;
            try
            {
                PreencherSaude();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Interface Politica registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                return;
            }
        }
        void PreencherSaude()
        {
            var objInterfaceSaude = new InterfacePublicaSaudeInfo();
            objInterfaceSaude.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            objInterfaceSaude.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdSaude.Value));
            if (!String.IsNullOrEmpty(rblProtocoloFormalSaude.SelectedValue))
            {
                objInterfaceSaude.ExisteProtocoloAtendimentoSaude = rblProtocoloFormalSaude.SelectedValue == "1" ? true : false;
                objInterfaceSaude.DescricaoProtocoloAtendimentoSaude = txtProtocoloFormalSaude.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoBolsaFamilia.SelectedValue))
            {
                objInterfaceSaude.ExisteIntervencaoBolsaFamilia = rblIntervencaoBolsaFamilia.SelectedValue == "1" ? true : false;
                objInterfaceSaude.DescricaoIntervencaoBolsaFamilia = txtJustificativaIntervencaoBolsaFamilia.Text;
            }

            if (!String.IsNullOrEmpty(rblIntervencaoSaudeBPC.SelectedValue))
            {
                objInterfaceSaude.ExisteIntervencaoBPC = rblIntervencaoSaudeBPC.SelectedValue == "1" ? true : false;
                objInterfaceSaude.DescricaoIntervencaoBPC = txtJustificativaIntervencaoSaudeBPC.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoConjuntaVitimasExploracao.SelectedValue))
            {
                objInterfaceSaude.ExisteIntervencaoVitimas = rblIntervencaoConjuntaVitimasExploracao.SelectedValue == "1" ? true : false;
                objInterfaceSaude.DescricaoIntervencaoVitimas = txtIntervencaoConjuntaVitimasExploracao.Text;
            }
            if (!String.IsNullOrEmpty(rblIntervencaoConjuntaSaude.SelectedValue))
            {
                objInterfaceSaude.ExisteIntervencaoIdosoPCD = rblIntervencaoConjuntaSaude.SelectedValue == "1" ? true : false;
                objInterfaceSaude.DescricaoIntervencaoIdosoPCD = txtJustificativaIntervencaoConjuntaSaude.Text;
            }
            using (var proxy = new ProxyInterfacePolitica())
            {

                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdSaude.Value)) == 0)
                {
                    var id = proxy.Service.AddInterfacePublicaSaude(objInterfaceSaude);
                    hdfIdSaude.Value = Genericos.clsCrypto.Encrypt(id.ToString());
                }
                else
                {
                    proxy.Service.UpdateInterfacePublicaSaude(objInterfaceSaude);
                }
            }
        }
        #endregion
        #region Segurança Alimentar
        void carregarRestaurantes()
        {
            lstRestaurantes.DataSource = Restaurantes;
            lstRestaurantes.DataBind();

            if (lstRestaurantes.Items.Count > 0)
                trlstRestaurantes.Visible = true;
        }
        void carregarOutrasFormas()
        {
            lstOutrasFormasDistribuicao.DataSource = OutrasFormas;
            lstOutrasFormasDistribuicao.DataBind();

            if (lstOutrasFormasDistribuicao.Items.Count > 0)
                trlstOutrasFormasDistribuicao.Visible = true;
        }
        private void carregarOutrasAcoes()
        {
            lstOutrasAcoes.DataSource = OutrasAcoes;
            lstOutrasAcoes.DataBind();

            if (lstOutrasAcoes.Items.Count > 0)
                trlstOutrasAcoes.Visible = true;
        }
        protected void chkRestaurantePopular_CheckedChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            frmRestaurantePopular.Visible = chkRestaurantePopular.Checked;
            if (chkRestaurantePopular.Checked)
            {
                chkNaoPossuiInformacao.Checked = chkGestaoDiretaEstado.Checked = false;
                rbConvenioBomPrato.SelectedIndex = -1;
                rbParceria.SelectedIndex = -1;
                chkNaoPossuiInformacao_CheckedChanged(null, null);
                chkGestaoDiretaEstado_CheckedChanged(null, null);
            }
        }
        protected void chkGestaoDiretaEstado_CheckedChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (chkGestaoDiretaEstado.Checked)
            {
                chkNaoPossuiInformacao.Enabled = trlstRestaurantes.Visible = rbConvenioBomPrato.Enabled = rbParceria.Enabled =txtNomeRestaurante.Enabled = cep1.Enabled = telefone.Enabled = txtDataInicioRestaurante.Enabled = txtUnidade.Enabled = btnAdicionarBomPrato.Enabled = false;
                rbConvenioBomPrato.SelectedIndex = -1;
                if (Restaurantes != null)
                {
                    foreach (var item in Restaurantes)
                        Restaurantes.Remove(item);

                    lstRestaurantes.DataSource = Restaurantes;
                    lstRestaurantes.DataBind();
                    trlstRestaurantes.Visible = true;
                }
            }
            else
            {
                if (!chkNaoPossuiInformacao.Checked)
                {
                    chkNaoPossuiInformacao.Enabled = rbConvenioBomPrato.Enabled = rbParceria.Enabled = txtNomeRestaurante.Enabled = cep1.Enabled = telefone.Enabled = txtDataInicioRestaurante.Enabled = txtUnidade.Enabled = btnAdicionarBomPrato.Enabled = true;
                }
            }
        }
        protected void chkNaoPossuiInformacao_CheckedChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (chkNaoPossuiInformacao.Checked)
            {
                rbConvenioBomPrato.SelectedIndex = -1;
                chkGestaoDiretaEstado.Enabled = trlstRestaurantes.Visible = rbConvenioBomPrato.Enabled = rbParceria.Enabled = txtNomeRestaurante.Enabled = cep1.Enabled = telefone.Enabled = txtDataInicioRestaurante.Enabled = txtUnidade.Enabled = btnAdicionarBomPrato.Enabled = false;
            }
            else
            {
                if (!chkGestaoDiretaEstado.Checked)
                {
                    chkGestaoDiretaEstado.Enabled = rbConvenioBomPrato.Enabled = rbParceria.Enabled = txtNomeRestaurante.Enabled = cep1.Enabled = telefone.Enabled = txtDataInicioRestaurante.Enabled = txtUnidade.Enabled = btnAdicionarBomPrato.Enabled = true;
                }
                if (Restaurantes != null)
                {
                    trlstRestaurantes.Visible = true;
                }
            }
        }
        protected void btnAdicionarBomPrato_Click(object sender, EventArgs e)
        {
            var restaurante = new InterfacePublicaAlimentacaoRestauranteInfo();
            restaurante.Nome = txtNomeRestaurante.Text;
            restaurante.CEP = cep1.Txtcep;
            restaurante.Logradouro = cep1.Txtendereco;
            restaurante.Numero = cep1.Txtnumero;
            restaurante.Complemento = cep1.Txtcomplemento;
            restaurante.Bairro = cep1.Txtbairro;
            restaurante.Cidade = cep1.Txtcidade;
            restaurante.TelefoneFixo = telefone.Text;
            
            if (!String.IsNullOrEmpty(txtDataInicioRestaurante.Text))
                restaurante.DataInicioAtividade = Convert.ToDateTime(txtDataInicioRestaurante.Text);
            restaurante.UnidadeAtendimento = txtUnidade.Text;
            
            if (!String.IsNullOrEmpty(rbConvenioBomPrato.SelectedValue))
                restaurante.ConvenioBomPrato = rbConvenioBomPrato.SelectedValue == "1";
            
            if (!String.IsNullOrEmpty(rbParceria.SelectedValue))
                restaurante.ConvenioBomPrato = rbParceria.SelectedValue == "1";


            try
            {
                new ValidadorInterfacePublica().ValidarRestaurantePopular(restaurante);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Restaurantes = Restaurantes ?? new List<InterfacePublicaAlimentacaoRestauranteInfo>();
            Restaurantes.Add(restaurante);

            carregarRestaurantes();

            txtNomeRestaurante.Text =
            cep1.Txtbairro = cep1.Txtcep =
            cep1.Txtcidade = cep1.Txtcomplemento =
            cep1.Txtendereco = cep1.Txtnumero =
            telefone.Text = txtUnidade.Text = txtDataInicioRestaurante.Text = String.Empty;
            rbConvenioBomPrato.SelectedIndex = -1;
            rbParceria.SelectedIndex = -1;

            tbInconsistencias.Visible = false;
            trlstRestaurantes.Visible = true;
        }
        protected void chkDistribuicaoAlimentos_CheckedChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (chkDistribuicaoAlimentos.Checked)
                trProgramaVivaLeite.Visible = true;
            else
            {
                trProgramaVivaLeite.Visible = false;
                chkVivaleite.Checked = false;
                chkVivaleite_CheckedChanged(null, null);
                chkOutraFormaDistribuicao.Checked = false;
                chkOutraFormaDistribuicao_CheckedChanged(null, null);
            }
        }
        protected void chkOutraAcao_CheckedChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (chkOutraAcao.Checked)
                trOutroTipoAcao.Visible = true;
            else
            {
                trOutroTipoAcao.Visible = false;
                if (OutrasAcoes != null)
                {
                    var novaLista = (from outraAcao in OutrasAcoes select outraAcao).ToList();
                    foreach (var item in novaLista)
                    {
                        OutrasAcoes.Remove(item);
                    }
                }
                lstOutrasAcoes.DataSource = OutrasAcoes;
                lstOutrasAcoes.Items.Clear();
                lstOutrasAcoes.DataBind();

            }
        }
        protected void chkVivaleite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVivaleite.Checked)
                trProtocoloVivaleite.Visible = true;
            else
            {
                trProtocoloVivaleite.Visible = false;
                rblOrgaoGestor.SelectedValue = "0";
                rblOrgaoGestor_SelectedIndexChanged(null, null);
            }
        }
        protected void rblOrgaoGestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblOrgaoGestor.SelectedValue == "1")
            {
                trTipoDistribuicaoLeite.Visible = true;
            }
            else
            {
                trTipoDistribuicaoLeite.Visible = false;
                rblDistribuidor.SelectedIndex = -1;
                rblDistribuidor_SelectedIndexChanged(null, null);
                trOutraPolitica.Visible = false;
            }
        }

        protected void rblDistribuidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");

            if (rblDistribuidor.SelectedIndex == -1)
            {
                trOutrasPoliticasPublicas.Visible = trUnidadesRedeIndireta.Visible = false;
                cblEntidades.Items.Clear();
                txtOutraPoliticaPublica.Text = String.Empty;
            }

            if (rblDistribuidor.SelectedValue == "1")
            {
                trOutrasPoliticasPublicas.Visible = trUnidadesRedeIndireta.Visible = false;
                cblEntidades.Items.Clear();
                txtOutraPoliticaPublica.Text = String.Empty;
            }
            if (rblDistribuidor.SelectedValue == "2")
            {
                trOutraPolitica.Visible = false;
                txtOutraPoliticaPublica.Text = String.Empty;
                trUnidadesRedeIndireta.Visible = true;
                using (var proxy = new ProxyRedeProtecaoSocial())
                {
                    cblEntidades.DataSource = proxy.Service.GetUnidadesDistrituicaoVivaleite(SessaoPmas.UsuarioLogado.Prefeitura.Id);
                    cblEntidades.DataTextField = "RazaoSocial";
                    cblEntidades.DataValueField = "Id";
                    cblEntidades.DataBind();
                }
            }
            if (rblDistribuidor.SelectedValue == "3")
            {
                trOutraPolitica.Visible = true;
                trUnidadesRedeIndireta.Visible = false;
            }


        }

        protected void lstRestaurantes_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstRestaurantes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    Restaurantes.RemoveAt(e.Item.DataItemIndex);
                    carregarRestaurantes();
                    var script = Util.GetJavaScriptDialogOK("Restaurante removido com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void btnAdicionarFormaDistribuicao_Click(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            var outraForma = new InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo();
            outraForma.Responsavel = txtResponsavel.Text;
            outraForma.Descricao = txtDescricao.Text;
            try
            {
                new ValidadorInterfacePublica().ValidarOutraFormaDistribuicao(outraForma);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            OutrasFormas = OutrasFormas ?? new List<InterfacePublicaAlimentacaoFormaDistribuicaoAlimentosInfo>();
            OutrasFormas.Add(outraForma);
            carregarOutrasFormas();

            tbInconsistencias.Visible = false;
            trlstOutrasFormasDistribuicao.Visible = true;

        }
        protected void lstOutrasFormasDistribuicao_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void lstOutrasFormasDistribuicao_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    OutrasFormas.RemoveAt(e.Item.DataItemIndex);
                    carregarOutrasFormas();
                    var script = Util.GetJavaScriptDialogOK("Forma de distribuição removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void chkOutraFormaDistribuicao_CheckedChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (chkOutraFormaDistribuicao.Checked)
                trOutraFormaDistribuicao.Visible = true;
            else
            {
                trOutraFormaDistribuicao.Visible = false;

                trOutrosProgramasFinanciados.Visible = false;

                if (OutrasFormas != null)
                {
                    var novaLista = (from outraForma in OutrasFormas select outraForma).ToList();
                    foreach (var item in novaLista)
                    {
                        OutrasFormas.Remove(item);
                    }
                }

                lstOutrasFormasDistribuicao.DataSource = OutrasFormas;
                lstOutrasFormasDistribuicao.Items.Clear();
                lstOutrasFormasDistribuicao.DataBind();

            }
        }

        protected void rblExecutaAcaoAlimentar_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            trExecutaAcaoAlimentar.Visible = false;
            if (rblExecutaAcaoAlimentar.SelectedValue == "True")
                trExecutaAcaoAlimentar.Visible = true;
            else
            {
                trExecutaAcaoAlimentar.Visible = false;
                chkRestaurantePopular.Checked = chkDistribuicaoAlimentos.Checked = chkOutraAcao.Checked = false;
                chkRestaurantePopular_CheckedChanged(null, null);
                chkDistribuicaoAlimentos_CheckedChanged(null, null);
                chkOutraAcao_CheckedChanged(null, null);
            }
        }
        protected void btnAdicionarOutraFormaAcao_Click(object sender, EventArgs e)
        {
            frame1_3.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            var outraForma = new InterfacePublicaAlimentacaoOutraAcaoInfo();
            outraForma.AcaoDesenvolvida = txtAcaoDesenvolvida.Text;
            outraForma.OrgaoResponsavel = txtOrgaoResponsavelAcao.Text;
            try
            {
                new ValidadorInterfacePublica().ValidarOutraAcao(outraForma);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }
            txtAcaoDesenvolvida.Text = txtOrgaoResponsavelAcao.Text = String.Empty;

            OutrasAcoes = OutrasAcoes ?? new List<InterfacePublicaAlimentacaoOutraAcaoInfo>();
            OutrasAcoes.Add(outraForma);
            carregarOutrasAcoes();

            tbInconsistencias.Visible = false;
            trlstOutrasFormasDistribuicao.Visible = true;
        }

        protected void lstOutrasAcoes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    OutrasAcoes.RemoveAt(e.Item.DataItemIndex);
                    carregarOutrasAcoes();
                    var script = Util.GetJavaScriptDialogOK("Ação removida com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    frame1_3.Attributes.Add("class", "active");
                    frame1_1.Attributes.Remove("class");
                    frame1_2.Attributes.Remove("class");
                    frame1_4.Attributes.Remove("class");
                    frame1_5.Attributes.Remove("class");
                    break;

                default:
                    break;
            }
        }

        protected void lstOutrasAcoes_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }
        protected void btnSalvarAlimentacao_Click(object sender, EventArgs e)
        {
            String msg = String.Empty;
            try
            {
                preencherAlimentacao();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Interface Politica registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                frame1_3.Attributes.Add("class", "active");
                frame1_1.Attributes.Remove("class");
                frame1_2.Attributes.Remove("class");
                frame1_4.Attributes.Remove("class");
                frame1_5.Attributes.Remove("class");
                return;
            }
        }



        void preencherAlimentacao()
        {
            var objInterfaceAlimentacao = new InterfacePublicaAlimentacaoInfo();
            objInterfaceAlimentacao.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdAlimentacao.Value));

            objInterfaceAlimentacao.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            ///O município executa alguma ação relativa a segurança alimentar? 
            objInterfaceAlimentacao.ExecutaAcaoAlimentar = rblExecutaAcaoAlimentar.SelectedValue == "True";

            if (objInterfaceAlimentacao.ExecutaAcaoAlimentar)
            {

                //Restaurante Popular
                objInterfaceAlimentacao.RestaurantePopular = chkRestaurantePopular.Checked;

                if (objInterfaceAlimentacao.RestaurantePopular)
                {
                    objInterfaceAlimentacao.GestaoDiretaEstado = chkGestaoDiretaEstado.Checked;
                    objInterfaceAlimentacao.NaoPossuiInformacaoRestaurante = chkNaoPossuiInformacao.Checked;
                    objInterfaceAlimentacao.Restaurantes = Restaurantes;

                    if ((!objInterfaceAlimentacao.NaoPossuiInformacaoRestaurante) && (!objInterfaceAlimentacao.GestaoDiretaEstado) && (objInterfaceAlimentacao.Restaurantes != null))
                        objInterfaceAlimentacao.Restaurantes = Restaurantes;
                    else
                        if (Restaurantes != null)
                        {
                            Restaurantes.Clear();
                            objInterfaceAlimentacao.Restaurantes = Restaurantes;
                        }
                }
                else
                {
                    if (Restaurantes != null)
                    {
                        Restaurantes.Clear();
                        objInterfaceAlimentacao.Restaurantes = Restaurantes;
                    }
                }

                ///Distribuição de alimentos
                objInterfaceAlimentacao.DistribuicaoAlimentos = chkDistribuicaoAlimentos.Checked;
                if (objInterfaceAlimentacao.DistribuicaoAlimentos)
                {
                    //Programa estadual Vivaleite
                    objInterfaceAlimentacao.ExecutaDistribuicaoVivaleite = chkVivaleite.Checked;
                    objInterfaceAlimentacao.OutraFormaDistribuicao = chkOutraFormaDistribuicao.Checked;
                    if (objInterfaceAlimentacao.ExecutaDistribuicaoVivaleite)
                    {
                        //A gestão do programa Vivaleite é realizada pelo órgão gestor de assistência social
                        objInterfaceAlimentacao.GestaoVivaleiteOrgaoGestor = rblOrgaoGestor.SelectedValue == "1";
                        if (objInterfaceAlimentacao.GestaoVivaleiteOrgaoGestor.Value)
                        {
                            if (!String.IsNullOrEmpty(rblDistribuidor.SelectedValue))
                                objInterfaceAlimentacao.TipoDistribuidor = Convert.ToInt32(rblDistribuidor.SelectedValue);

                            if (objInterfaceAlimentacao.TipoDistribuidor.HasValue && objInterfaceAlimentacao.TipoDistribuidor.Value == 3)
                            {
                                objInterfaceAlimentacao.DecricaoOutraPoliticaDistribuicao = txtOutraPoliticaPublica.Text;

                                objInterfaceAlimentacao.DistribuicoesAlimentos = new List<InterfacePublicaDistribuicaoAlimentoInfo>();
                                foreach (ListItem item in cblEntidades.Items)
                                    if (item.Selected)
                                        objInterfaceAlimentacao.DistribuicoesAlimentos.Remove(new InterfacePublicaDistribuicaoAlimentoInfo() { IdUnidadePrivada = Convert.ToInt32(item.Value) });
                            }
                            if (objInterfaceAlimentacao.TipoDistribuidor.HasValue && objInterfaceAlimentacao.TipoDistribuidor.Value == 2)
                            {
                                objInterfaceAlimentacao.DecricaoOutraPoliticaDistribuicao = String.Empty;
                                objInterfaceAlimentacao.DistribuicoesAlimentos = new List<InterfacePublicaDistribuicaoAlimentoInfo>();
                                foreach (ListItem item in cblEntidades.Items)
                                    if (item.Selected)
                                        objInterfaceAlimentacao.DistribuicoesAlimentos.Add(new InterfacePublicaDistribuicaoAlimentoInfo() { IdUnidadePrivada = Convert.ToInt32(item.Value) });
                            }
                            else
                            {
                                if (objInterfaceAlimentacao.TipoDistribuidor.HasValue && objInterfaceAlimentacao.TipoDistribuidor.Value == 1)
                                {
                                    objInterfaceAlimentacao.DecricaoOutraPoliticaDistribuicao = String.Empty;
                                    objInterfaceAlimentacao.DistribuicoesAlimentos = new List<InterfacePublicaDistribuicaoAlimentoInfo>();
                                    foreach (ListItem item in cblEntidades.Items)
                                        if (item.Selected)
                                            objInterfaceAlimentacao.DistribuicoesAlimentos.Remove(new InterfacePublicaDistribuicaoAlimentoInfo() { IdUnidadePrivada = Convert.ToInt32(item.Value) });
                                }
                            }
                        }
                    }
                    //Outra forma de distribuição de alimentos (Não considerar cestas básicas fornecidas como benefícios eventuais)
                    if (objInterfaceAlimentacao.OutraFormaDistribuicao.Value || OutrasFormas != null)
                    {
                        if (objInterfaceAlimentacao.OutraFormaDistribuicao.Value)
                            objInterfaceAlimentacao.FormasDistribuicoesAlimentos = OutrasFormas;
                        else
                            if (OutrasFormas != null)
                                objInterfaceAlimentacao.FormasDistribuicoesAlimentos = OutrasFormas;
                    }
                }
                //Outro Tipo de Ação
                objInterfaceAlimentacao.OutraFormaAcao = chkOutraAcao.Checked;
                if (objInterfaceAlimentacao.OutraFormaAcao.Value)
                {
                    objInterfaceAlimentacao.OutrasAcoes = OutrasAcoes;

                    objInterfaceAlimentacao.DistribuicoesAlimentos = new List<InterfacePublicaDistribuicaoAlimentoInfo>();
                    foreach (ListItem item in cblEntidades.Items)
                        if (item.Selected)
                            objInterfaceAlimentacao.DistribuicoesAlimentos.Add(new InterfacePublicaDistribuicaoAlimentoInfo() { IdUnidadePrivada = Convert.ToInt32(item.Value) });

                    if (OutrasFormas != null)
                        objInterfaceAlimentacao.FormasDistribuicoesAlimentos = OutrasFormas;

                    if (OutrasAcoes != null)
                        objInterfaceAlimentacao.OutrasAcoes = OutrasAcoes;
                }
                else
                {
                    if (OutrasAcoes != null)
                    {
                        OutrasAcoes.Clear();
                        objInterfaceAlimentacao.OutrasAcoes = OutrasAcoes;
                    }
                }
            }

            using (var proxy = new ProxyInterfacePolitica())
            {
                if (Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdAlimentacao.Value)) == 0)
                {
                    var id = proxy.Service.AddInterfacePublicaAlimentacao(objInterfaceAlimentacao);
                    hdfIdAlimentacao.Value = Genericos.clsCrypto.Encrypt(id.ToString());
                }
                else
                    proxy.Service.UpdateInterfacePublicaAlimentacao(objInterfaceAlimentacao);
            }
        }
        #endregion
        #region Emprego, Trabalho
        protected void rblIntervencaoPoliticaEmprego_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_4.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoPoliticaEmprego.SelectedValue == "1")
                trJustificativaIntervencaoPoliticaEmprego.Visible = true;
            else
            {
                trJustificativaIntervencaoPoliticaEmprego.Visible = false;
                txtJustificativaIntervencaoPoliticaEmprego.Text = String.Empty;
            }
        }

        protected void rblIntervencaoPoliticaEmpregoPCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_4.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblIntervencaoPoliticaEmpregoPCD.SelectedValue == "1")
                trJustificativaIntervencaoPoliticaEmpregoPCD.Visible = true;
            else
            {
                trJustificativaIntervencaoPoliticaEmpregoPCD.Visible = false;
                txtJustificativaIntervencaoPoliticaEmpregoPCD.Text = String.Empty;
            }
        }

        protected void rblOutrasAcoesEmprego_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_4.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_5.Attributes.Remove("class");
            if (rblOutrasAcoesEmprego.SelectedValue == "1")
                trJustificativaOutrasAcoesEmprego.Visible = true;
            else
            {
                trJustificativaOutrasAcoesEmprego.Visible = false;
                txtOutrasAcoesEmprego.Text = String.Empty;
            }
        }
        protected void btnSalvarEmprego_Click(object sender, EventArgs e)
        {
            String msg = String.Empty;
            try
            {
                preencherEmprego();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Interface Politica registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                return;
            }
        }
        #endregion
        #region Outras políticas públicas
        void carregarOutrosServicos()
        {
            lstOutrosServicos.DataSource = OutrosServicos;
            lstOutrosServicos.DataBind();

            if (lstOutrosServicos.Items.Count > 0)
                trlstOutrosServicos.Visible = lstOutrosServicos.Visible = true;
        }
        protected void rblOutrasPoliticasPublicas_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_5.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            if (rblOutrasPoliticasPublicas.SelectedValue == "1")
                trOutrasPoliticasPublicas.Visible = true;
            else
            {
                trOutrasPoliticasPublicas.Visible = false;
                txtOutrasPoliticasPublicas.Text = String.Empty;
            }
        }

        protected void rblOutrosProgramasFinanciados_SelectedIndexChanged(object sender, EventArgs e)
        {
            frame1_5.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");
            if (rblOutrosProgramasFinanciados.SelectedValue == "1")
            {
                trOutrosProgramasFinanciados.Visible = true;
                carregarOutrosServicos();
            }
            else
            {
                trOutrosProgramasFinanciados.Visible = false;

                if (OutrosServicos != null)
                {
                    var novaLista = (from outroServico in OutrosServicos select outroServico).ToList();
                    foreach (var item in novaLista)
                    {
                        OutrosServicos.Remove(item);
                    }
                }

                lstOutrosServicos.DataSource = OutrosServicos;
                lstOutrosServicos.Items.Clear();
                lstOutrosServicos.DataBind();
            }
        }

        protected void btnAdicionarOutrosServicos_Click(object sender, EventArgs e)
        {
            frame1_5.Attributes.Add("class", "active");
            frame1_1.Attributes.Remove("class");
            frame1_2.Attributes.Remove("class");
            frame1_3.Attributes.Remove("class");
            frame1_4.Attributes.Remove("class");

            var outroServico = new InterfacePublicaOutroServicoInfo();
            outroServico.NomeProgramaProjeto = txtProgramaProjeto.Text;
            outroServico.PoliticaPublica = txtPoliticaPublica.Text;
            outroServico.ValorRepassePoliticaAssistencia = Convert.ToDecimal(txtValorRepasssado.Text);
            try
            {
                new ValidadorInterfacePublica().ValidarOutroServico(outroServico);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            OutrosServicos = OutrosServicos ?? new List<InterfacePublicaOutroServicoInfo>();
            OutrosServicos.Add(outroServico);
            carregarOutrosServicos();
            txtProgramaProjeto.Text = txtPoliticaPublica.Text = txtValorRepasssado.Text = String.Empty;
            tbInconsistencias.Visible = false;
            trlstOutrosServicos.Visible = lstOutrosServicos.Visible = true;


        }
        protected void lstOutrosServicos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Excluir":
                    OutrosServicos.RemoveAt(e.Item.DataItemIndex);
                    carregarOutrosServicos();
                    frame1_5.Attributes.Add("class", "active");
                    frame1_1.Attributes.Remove("class");
                    frame1_2.Attributes.Remove("class");
                    frame1_3.Attributes.Remove("class");
                    frame1_4.Attributes.Remove("class");
                    var script = Util.GetJavaScriptDialogOK("Recurso removido com sucesso");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                    break;

                default:
                    break;
            }
        }
        protected void lstOutrosServicos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

        protected void btnSalvarInterface_Click(object sender, EventArgs e)
        {
            String msg = String.Empty;
            try
            {
                preencherOutraPolitica();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty(msg))
            {
                msg = "Interface Politica registrada com sucesso!";
                lblInconsistencias.Text = "";
                tbInconsistencias.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavaScriptDialogOK(msg), true);
                return;
            }
        }

        #endregion







    }
}