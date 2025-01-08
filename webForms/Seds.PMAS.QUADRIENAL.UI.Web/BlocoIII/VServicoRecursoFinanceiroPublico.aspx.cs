using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using System.Web.UI.HtmlControls;
using Seds.PMAS.QUADRIENAL.Resource;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class VServicoRecursoFinanceiroPublico : System.Web.UI.Page
    {

        private List<int> PublicoExercicios = new List<int> { 2022, 2023, 2024, 2025 };

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

                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/BlocoIII/CUnidadesPublicas.aspx");
                    return;
                }

                using (var proxyEstrutura = new ProxyEstruturaAssistenciaSocial())
                {
                    using (var proxy = new ProxyRedeProtecaoSocial())
                    {

                        using (var proxyProgramas = new ProxyProgramas())
                        {
                            load(proxy, proxyEstrutura, proxyProgramas);
                        }
                    }
                }
            }
        }

        void load(ProxyRedeProtecaoSocial proxy, ProxyEstruturaAssistenciaSocial proxyEstrutura, ProxyProgramas proxyProgramas)
        {
            int idServico = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            var servico = proxy.Service.GetServicoRecursoFinanceiroPublicoById(idServico);


            if (servico == null)
                return;

            if (servico.UsuarioTipoServico.IdTipoServico == 135)
                lblPAIF2.Text = "famílias";
            lblTipoProtecao.Text = servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 1
                ? "Básica" : servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2
                ? "Média Complexidade"
                : "Alta Complexidade";

            lblPublicoAlvo.Text = servico.UsuarioTipoServico.Nome;

            #region serviço não tipificado
            if (servico.IdTipoServicoNaoTipificado != null || servico.UsuarioTipoServico.IdTipoServico == 153)
            {


                if (servico.UsuarioTipoServico.IdTipoServico == 138 || servico.UsuarioTipoServico.IdTipoServico == 145 || servico.UsuarioTipoServico.IdTipoServico == 158 || servico.UsuarioTipoServico.IdTipoServico == 159)
                {

                    lblTipoServico.Text += "Serviço não tipificado pela Resolução n° 109 do CNAS, de 11/11/2009 - ";

                    lblTipoServico.Text += servico.UsuarioTipoServico.TipoServico.Nome;

                    if (!String.IsNullOrEmpty(servico.ObjetivoServicoNaoTipificado))
                    {
                        tbNaoTipificado.Visible = true;
                        lblNaoTipificado.Text = servico.DescricaoServicoNaoTipificado;
                        lblObjetivoNaoTipificado.Text = servico.ObjetivoServicoNaoTipificado;
                    }
                }
                else
                {
                    lblTipoServico.Text += servico.UsuarioTipoServico.TipoServico.Nome;
                    if (!String.IsNullOrEmpty(servico.ObjetivoServicoNaoTipificado))
                    {
                        tbNaoTipificado.Visible = true;
                        lblNaoTipificado.Text = servico.DescricaoServicoNaoTipificado;
                        lblObjetivoNaoTipificado.Text = servico.ObjetivoServicoNaoTipificado;
                    }

                }
            }
            else
            {
                lblTipoServico.Text = servico.UsuarioTipoServico.TipoServico.Nome;
            }
            #endregion

            #region situacoes especificas
            if (servico.SituacoesEspecificas != null && servico.SituacoesEspecificas.Count > 0)
            {
                lblSituacoesEspecificas.Text = Util.Concat(servico.SituacoesEspecificas.Select(s => s.Nome).ToList(), "<br/>");
            }

            #endregion

            #region Criança auxilio-reclusão

            if (servico.IdUsuarioTipoServico == 37 && servico.UsuarioTipoServico.IdTipoServico == 146)
            {
                trCriancasAuxilioReclusao2024.Visible = true;
                trCriancasPensaoMorte2024.Visible = true;
                trCriancasAuxilioReclusao2025.Visible = true;
                trCriancasPensaoMorte2025.Visible = true;
            }
            else
            {
                trCriancasAuxilioReclusao2024.Visible = false;
                trCriancasPensaoMorte2024.Visible = false;
                trCriancasAuxilioReclusao2025.Visible = false;
                trCriancasPensaoMorte2025.Visible = false;
            }



            if (servico.AtendeCriancasAuxilioReclusao != null)
            {
                if (servico.AtendeCriancasAuxilioReclusao.Value)
                {
                    trAuxilioReclusaoRequerimentosFeitos2024.Visible = true;
                    trAuxilioReclusaoRequerimentosAprovados2024.Visible = true;
                    trAuxilioReclusaoRequerimentosNegados2024.Visible = true;

                    lblCriancasAuxilioReclusao2024.Text = "Sim";

                    if (servico.CriancaAuxilioReclusaoFeitos != 0)
                    {
                        lblAuxilioReclusaoRequerimentosFeitos2024.Text = servico.CriancaAuxilioReclusaoFeitos.ToString();
                    }
                    else
                    {
                        lblAuxilioReclusaoRequerimentosFeitos2024.Text = "0";
                    }

                    if (servico.CriancaAuxilioReclusaoAprovados != 0)
                    {
                        lblAuxilioReclusaoRequerimentosAprovados2024.Text = servico.CriancaAuxilioReclusaoAprovados.ToString();
                    }
                    else
                    {
                        lblAuxilioReclusaoRequerimentosAprovados2024.Text = "0";
                    }

                    if (servico.CriancaAuxilioReclusaoNegado != 0)
                    {
                        lblAuxilioReclusaoRequerimentosNegados2024.Text = servico.CriancaAuxilioReclusaoNegado.ToString();
                    }
                    else
                    {
                        lblAuxilioReclusaoRequerimentosNegados2024.Text = "0";
                    }

                }
                else
                {
                    trAuxilioReclusaoRequerimentosFeitos2024.Visible = false;
                    trAuxilioReclusaoRequerimentosAprovados2024.Visible = false;
                    trAuxilioReclusaoRequerimentosNegados2024.Visible = false;
                    lblCriancasAuxilioReclusao2024.Text = "Não";
                }
            }
            else
            {
                trAuxilioReclusaoRequerimentosFeitos2024.Visible = false;
                trAuxilioReclusaoRequerimentosAprovados2024.Visible = false;
                trAuxilioReclusaoRequerimentosNegados2024.Visible = false;
                lblCriancasAuxilioReclusao2024.Text = "Não Informado";
            }


            if (servico.AtendeCriancasAuxilioReclusaoExercicio2025 != null)
            {
                if (servico.AtendeCriancasAuxilioReclusaoExercicio2025.Value)
                {
                    trAuxilioReclusaoRequerimentosFeitos2025.Visible = true;
                    trAuxilioReclusaoRequerimentosAprovados2025.Visible = true;
                    trAuxilioReclusaoRequerimentosNegados2025.Visible = true;

                    lblCriancasAuxilioReclusao2025.Text = "Sim";

                    if (servico.CriancaAuxilioReclusaoFeitos != 0)
                    {
                        lblAuxilioReclusaoRequerimentosFeitos2025.Text = servico.CriancaAuxilioReclusaoFeitos.ToString();
                    }
                    else
                    {
                        lblAuxilioReclusaoRequerimentosFeitos2025.Text = "0";
                    }

                    if (servico.CriancaAuxilioReclusaoAprovados != 0)
                    {
                        lblAuxilioReclusaoRequerimentosAprovados2025.Text = servico.CriancaAuxilioReclusaoAprovados.ToString();
                    }
                    else
                    {
                        lblAuxilioReclusaoRequerimentosAprovados2025.Text = "0";
                    }

                    if (servico.CriancaAuxilioReclusaoNegado != 0)
                    {
                        lblAuxilioReclusaoRequerimentosNegados2025.Text = servico.CriancaAuxilioReclusaoNegado.ToString();
                    }
                    else
                    {
                        lblAuxilioReclusaoRequerimentosNegados2025.Text = "0";
                    }

                }
                else
                {
                    trAuxilioReclusaoRequerimentosFeitos2025.Visible = false;
                    trAuxilioReclusaoRequerimentosAprovados2025.Visible = false;
                    trAuxilioReclusaoRequerimentosNegados2025.Visible = false;
                    lblCriancasAuxilioReclusao2025.Text = "Não";
                }
            }
            else
            {
                trAuxilioReclusaoRequerimentosFeitos2025.Visible = false;
                trAuxilioReclusaoRequerimentosAprovados2025.Visible = false;
                trAuxilioReclusaoRequerimentosNegados2025.Visible = false;
                lblCriancasAuxilioReclusao2025.Text = "Não Informado";
            }





            #endregion

            #region Criança auxilio-reclusão

            if (servico.AtendeCriancasPensaoMorte != null)
            {
                if (servico.AtendeCriancasPensaoMorte.Value)
                {
                    trPensaoMorteRequerimentosFeitos2024.Visible = true;
                    trPensaoMorteRequerimentosAprovados2024.Visible = true;
                    trPensaoMorteRequerimentosNegados2024.Visible = true;

                    lblCriancasPensaoMorte2024.Text = "Sim";

                    if (servico.CriancaPensaoMorteFeitos != 0)
                    {
                        lblPensaoMorteRequerimentosFeitos2024.Text = servico.CriancaPensaoMorteFeitos.ToString();
                    }
                    else
                    {
                        lblPensaoMorteRequerimentosFeitos2024.Text = "0";
                    }

                    if (servico.CriancaPensaoMorteAprovados != 0)
                    {
                        lblPensaoMorteRequerimentosAprovados2024.Text = servico.CriancaPensaoMorteAprovados.ToString();
                    }
                    else
                    {
                        lblPensaoMorteRequerimentosAprovados2024.Text = "0";
                    }

                    if (servico.CriancaPensaoMorteNegado != 0)
                    {
                        lblPensaoMorteRequerimentosNegados2024.Text = servico.CriancaPensaoMorteNegado.ToString();
                    }
                    else
                    {
                        lblPensaoMorteRequerimentosNegados2024.Text = "0";
                    }

                }
                else
                {
                    trPensaoMorteRequerimentosFeitos2024.Visible = false;
                    trPensaoMorteRequerimentosAprovados2024.Visible = false;
                    trPensaoMorteRequerimentosNegados2024.Visible = false;
                    lblCriancasPensaoMorte2024.Text = "Não";
                }
            }
            else
            {
                trPensaoMorteRequerimentosFeitos2024.Visible = false;
                trPensaoMorteRequerimentosAprovados2024.Visible = false;
                trPensaoMorteRequerimentosNegados2024.Visible = false;
                lblCriancasPensaoMorte2024.Text = "Não Informado";
            }

            if (servico.AtendeCriancasPensaoMorteExercicio2025 != null)
            {
                if (servico.AtendeCriancasPensaoMorteExercicio2025.Value)
                {
                    trPensaoMorteRequerimentosFeitos2025.Visible = true;
                    trPensaoMorteRequerimentosAprovados2025.Visible = true;
                    trPensaoMorteRequerimentosNegados2025.Visible = true;

                    lblCriancasPensaoMorte2025.Text = "Sim";

                    if (servico.CriancaPensaoMorteFeitos != 0)
                    {
                        lblPensaoMorteRequerimentosFeitos2025.Text = servico.CriancaPensaoMorteFeitos.ToString();
                    }
                    else
                    {
                        lblPensaoMorteRequerimentosFeitos2025.Text = "0";
                    }

                    if (servico.CriancaPensaoMorteAprovados != 0)
                    {
                        lblPensaoMorteRequerimentosAprovados2025.Text = servico.CriancaPensaoMorteAprovados.ToString();
                    }
                    else
                    {
                        lblPensaoMorteRequerimentosAprovados2025.Text = "0";
                    }

                    if (servico.CriancaPensaoMorteNegado != 0)
                    {
                        lblPensaoMorteRequerimentosNegados2025.Text = servico.CriancaPensaoMorteNegado.ToString();
                    }
                    else
                    {
                        lblPensaoMorteRequerimentosNegados2025.Text = "0";
                    }

                }
                else
                {
                    trPensaoMorteRequerimentosFeitos2025.Visible = false;
                    trPensaoMorteRequerimentosAprovados2025.Visible = false;
                    trPensaoMorteRequerimentosNegados2025.Visible = false;
                    lblCriancasPensaoMorte2025.Text = "Não";
                }
            }
            else
            {
                trPensaoMorteRequerimentosFeitos2025.Visible = false;
                trPensaoMorteRequerimentosAprovados2025.Visible = false;
                trPensaoMorteRequerimentosNegados2025.Visible = false;
                lblCriancasPensaoMorte2025.Text = "Não informado";
            }


            #endregion

            #region atividades socio assistenciais
            if (servico.AtividadesSocioAssistenciais != null && servico.AtividadesSocioAssistenciais.Count > 0)
            {
                lblAtividades.Text = Util.Concat(servico.AtividadesSocioAssistenciais.Select(s => s.Nome).ToList(), "<br/>");
            }
            #endregion

            var abrangencias = proxyEstrutura.Service.GetAbrangenciasServico();
            lblAbrangencia.Text = abrangencias.First(a => a.Id == servico.IdAbrangenciaServico).Abreviacao;
            var idAbrangencias = abrangencias.First(a => a.Id == servico.IdAbrangenciaServico).Id;

            if (idAbrangencias == 4)
            {
                trAbrangencia.Visible = true;
                trOfertaOuSede.Visible = true;

                if (servico.MunicipioSedeServico == true)
                {
                    lblSedeServico.Text = "Sim, municipio é sede do serviço.";
                    lblOfertaOuSede.Text = "Municípios que participam da oferta do serviço :";
                    txtOfertaOuSede.Text = servico.IndicaMunicipiosParticipamOfertaServico != String.Empty ? servico.IndicaMunicipiosParticipamOfertaServico : "Não há municípios cadastrados.";
                }
                else
                {
                    lblSedeServico.Text = "Municipio não é sede do serviço.";
                    lblOfertaOuSede.Text = "Municípios indicados como Sede do serviço :";
                    txtOfertaOuSede.Text = servico.IndicaMunicipiosSedeServico != String.Empty ? servico.IndicaMunicipiosSedeServico : "Não há municípios cadastrados.";
                }


                var juridica = proxyEstrutura.Service.GetFormaJuridica();

                if (servico.IdFormaJuridica == 1)
                {
                    var consorcio = proxyEstrutura.Service.GetConsorcioPublico(servico.Id);

                    lblJuridica.Text = juridica.First(a => a.Id == servico.IdFormaJuridica).NomeForma;

                    trTituloFormaJuridica.Visible = true;
                    trJuridica.Visible = true;

                    if (consorcio != null)
                    {
                        trJuridica.Visible = true;
                        lblNomeDoConsorcio.Text = "Nome Do Consórcio : " + consorcio.NomeConsorcio;
                        lblCNPJ.Text = "CNPJ : " + consorcio.CNPJ;
                        lblMunicipioSede.Text = "Município sede : " + consorcio.MunicipioSede;
                        txtMunicipiosEnvolvidos.Text = consorcio.MunicipioEnvolvido;
                    }
                }
                else if (servico.IdFormaJuridica != 0)
                {
                    lblJuridica.Text = juridica.First(a => a.Id == servico.IdFormaJuridica).NomeForma;
                    trTituloFormaJuridica.Visible = true;
                    trJuridica.Visible = false;
                }
            }
            else
            {
                trTituloFormaJuridica.Visible = false;
                trJuridica.Visible = false;
                trAbrangencia.Visible = false;
                trOfertaOuSede.Visible = false;
            }


            switch (servico.IdCaracteristicasTerritorio)
            {

                case 1: lblCaracteristicasTerritorio.Text = "Ciganos"; break;
                case 2: lblCaracteristicasTerritorio.Text = "Extrativistas"; break;
                case 3: lblCaracteristicasTerritorio.Text = "Pescadores artesanais"; break;
                case 4: lblCaracteristicasTerritorio.Text = "Comunidade tradicional de matriz africana"; break;
                case 5: lblCaracteristicasTerritorio.Text = "Comunidade ribeirinha"; break;
                case 6: lblCaracteristicasTerritorio.Text = "Indígenas"; break;
                case 7: lblCaracteristicasTerritorio.Text = "Quilombolas"; break;
                case 8: lblCaracteristicasTerritorio.Text = "Agricultores familiares"; break;
                case 9: lblCaracteristicasTerritorio.Text = "Acampamentos"; break;
                case 10: lblCaracteristicasTerritorio.Text = "População flutuante decorrente de instalação prisional"; break;
                case 11: lblCaracteristicasTerritorio.Text = "Trabalhadores sazonais"; break;
                case 12: lblCaracteristicasTerritorio.Text = "Aglomerados subnormais"; break;
                case 13: lblCaracteristicasTerritorio.Text = "Assentamentos"; break;
                case 14: lblCaracteristicasTerritorio.Text = "Nenhuma das condições socioterritoriais"; break;
                case 15: lblCaracteristicasTerritorio.Text = "População em situação de rua"; break;
                case 16: lblCaracteristicasTerritorio.Text = "Pessoas com deficiência"; break;
            }


            if (servico.UsuarioTipoServico.TipoServico.IdTipoProtecaoSocial == 2)
            {
                switch (servico.IdCaracteristicaOfertaServico)
                {

                    case 1: lblOfertaServico.Text = "Nenhuma das alternativas"; break;
                    case 2: lblOfertaServico.Text = "CDPCD – Centro Dia para Pessoas com Deficiência"; break;
                    case 3: lblOfertaServico.Text = "CDI – Centro Dia Idoso"; break;
                    case 4: lblOfertaServico.Text = "CDIPCD – Centro Dia para Idosos e Pessoas com Deficiência"; break;
                    case 5: lblOfertaServico.Text = "Domicílio"; break;
                }
            }
            else
            {
                tdServicoOfertado.Visible = false;
            }

            switch (servico.IdRegiaoMoradia)
            {
                case 1: lblMoradiaUsuarios.Text = "Zona Urbana"; break;
                case 2: lblMoradiaUsuarios.Text = "Zona Rural"; break;
                case 3: lblMoradiaUsuarios.Text = "Zona Urbana e Rural"; break;
            }
            switch (servico.IdSexo)
            {
                case 1: lblSexo.Text = "Feminino"; break;
                case 2: lblSexo.Text = "Masculino"; break;
                case 3: lblSexo.Text = "Ambos os sexos"; break;
            }

            #region Exibir: Funcionamento

            #region Capacidade
            var servicosRecursosFinanceiroPublicoCapacidadeExercicio1 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == this.PublicoExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadeLAExercicio1 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == this.PublicoExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadePSCExercicio1 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == this.PublicoExercicios[0]).FirstOrDefault();

            var servicosRecursosFinanceiroPublicoCapacidadeExercicio2 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == this.PublicoExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadeLAExercicio2 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == this.PublicoExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadePSCExercicio2 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == this.PublicoExercicios[1]).FirstOrDefault();

            var servicosRecursosFinanceiroPublicoCapacidadeExercicio3 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == this.PublicoExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadeLAExercicio3 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == this.PublicoExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadePSCExercicio3 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == this.PublicoExercicios[2]).FirstOrDefault();

            var servicosRecursosFinanceiroPublicoCapacidadeExercicio4 = servico.ServicosRecursosFinanceiroPublicoCapacidade.Where(x => x.Exercicio == this.PublicoExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadeLAExercicio4 = servico.ServicosRecursosFinanceiroPublicoCapacidadeLA.Where(x => x.Exercicio == this.PublicoExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoCapacidadePSCExercicio4 = servico.ServicosRecursosFinanceiroPublicoCapacidadePSC.Where(x => x.Exercicio == this.PublicoExercicios[3]).FirstOrDefault();
            #endregion

            #region Media Mensal
            var servicosRecursosFinanceiroPublicoMediaMensalExercicio1 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == this.PublicoExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalLAExercicio1 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == this.PublicoExercicios[0]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio1 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == this.PublicoExercicios[0]).FirstOrDefault();

            var servicosRecursosFinanceiroPublicoMediaMensalExercicio2 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == this.PublicoExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalLAExercicio2 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == this.PublicoExercicios[1]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio2 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == this.PublicoExercicios[1]).FirstOrDefault();

            var servicosRecursosFinanceiroPublicoMediaMensalExercicio3 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == this.PublicoExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalLAExercicio3 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == this.PublicoExercicios[2]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio3 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == this.PublicoExercicios[2]).FirstOrDefault();

            var servicosRecursosFinanceiroPublicoMediaMensalExercicio4 = servico.ServicosRecursosFinanceiroPublicoMediaMensal.Where(x => x.Exercicio == this.PublicoExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalLAExercicio4 = servico.ServicosRecursosFinanceiroPublicoMediaMensalLA.Where(x => x.Exercicio == this.PublicoExercicios[3]).FirstOrDefault();
            var servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio4 = servico.ServicosRecursosFinanceiroPublicoMediaMensalPSC.Where(x => x.Exercicio == this.PublicoExercicios[3]).FirstOrDefault();
            #endregion

            if (servico.UsuarioTipoServico.TipoServico.Id == R_TIPO_SERVICO.SERVICO_PROTECAO_SOCIAL_ADOLESC_CUMPR_MEDIDA_SOCIOEDUCATIVA_LA_PSC)
            {
                layout_capacidade_media_mensal_la_psc.Visible = true;
                layout_capacidade_media_mensal.Visible = false;

                #region Capacidade [LA]
                lblCapacidadeLAExercicio1.Text = servicosRecursosFinanceiroPublicoCapacidadeLAExercicio1 != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio1.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio1.Capacidade.Value.ToString() : "0": "0";
                lblCapacidadeLAExercicio2.Text = servicosRecursosFinanceiroPublicoCapacidadeLAExercicio2 != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio2.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio2.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeLAExercicio3.Text = servicosRecursosFinanceiroPublicoCapacidadeLAExercicio3 != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio3.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio3.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeLAExercicio4.Text = servicosRecursosFinanceiroPublicoCapacidadeLAExercicio4 != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio4.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeLAExercicio4.Capacidade.Value.ToString() : "0" : "0";

                lblCapacidadePSCExercicio1.Text = servicosRecursosFinanceiroPublicoCapacidadePSCExercicio1 != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio1.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio1.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadePSCExercicio2.Text = servicosRecursosFinanceiroPublicoCapacidadePSCExercicio2 != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio2.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio2.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadePSCExercicio3.Text = servicosRecursosFinanceiroPublicoCapacidadePSCExercicio3 != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio3.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio3.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadePSCExercicio4.Text = servicosRecursosFinanceiroPublicoCapacidadePSCExercicio4 != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio4.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadePSCExercicio4.Capacidade.Value.ToString() : "0" : "0";
                #endregion

                #region Media Mensal [LA]
                lblMediaMensalLAExercicio1.Text = servicosRecursosFinanceiroPublicoMediaMensalLAExercicio1 != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio1.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio1.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalLAExercicio2.Text = servicosRecursosFinanceiroPublicoMediaMensalLAExercicio2 != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio2.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio2.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalLAExercicio3.Text = servicosRecursosFinanceiroPublicoMediaMensalLAExercicio3 != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio3.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio3.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalLAExercicio4.Text = servicosRecursosFinanceiroPublicoMediaMensalLAExercicio4 != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio4.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalLAExercicio4.MediaMensal.Value.ToString() : "0" : "0";

                lblMediaMensalPSCExercicio1.Text = servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio1 != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio1.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio1.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalPSCExercicio2.Text = servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio2 != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio2.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio2.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalPSCExercicio3.Text = servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio3 != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio3.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio3.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalPSCExercicio4.Text = servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio4 != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio4.MediaMensal != null ? servicosRecursosFinanceiroPublicoMediaMensalPSCExercicio4.MediaMensal.Value.ToString() : "0" : "0";
                #endregion
            }
            else
            {
                layout_capacidade_media_mensal.Visible = true;
                layout_capacidade_media_mensal_la_psc.Visible = false;

                #region Capacidade [LA]
                lblCapacidadeExercicio1.Text = servicosRecursosFinanceiroPublicoCapacidadeExercicio1 != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio1.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio1.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeExercicio2.Text = servicosRecursosFinanceiroPublicoCapacidadeExercicio2 != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio2.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio2.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeExercicio3.Text = servicosRecursosFinanceiroPublicoCapacidadeExercicio3 != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio3.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio3.Capacidade.Value.ToString() : "0" : "0";
                lblCapacidadeExercicio4.Text = servicosRecursosFinanceiroPublicoCapacidadeExercicio4 != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio4.Capacidade != null ? servicosRecursosFinanceiroPublicoCapacidadeExercicio4.Capacidade.Value.ToString() : "0" : "0";
                #endregion

                #region Media Mensal [LA]
                lblMediaMensalExercicio1.Text = servicosRecursosFinanceiroPublicoMediaMensalExercicio1 != null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio1.MediaMensal !=null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio1.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalExercicio2.Text = servicosRecursosFinanceiroPublicoMediaMensalExercicio2 != null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio2.MediaMensal !=null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio2.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalExercicio3.Text = servicosRecursosFinanceiroPublicoMediaMensalExercicio3 != null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio3.MediaMensal !=null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio3.MediaMensal.Value.ToString() : "0" : "0";
                lblMediaMensalExercicio4.Text = servicosRecursosFinanceiroPublicoMediaMensalExercicio4 != null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio4.MediaMensal !=null ? servicosRecursosFinanceiroPublicoMediaMensalExercicio4.MediaMensal.Value.ToString() : "0" : "0";
                #endregion
            }

            #endregion


            lblDataInicio.Text = servico.DataFuncionamentoServico.HasValue ? servico.DataFuncionamentoServico.Value.ToShortDateString() : String.Empty;
            if (servico.IdHorasSemana.HasValue)
            {
                switch (servico.IdHorasSemana)
                {

                    case 1: lblHorasSemana.Text = "Até 20 horas"; break;
                    case 2: lblHorasSemana.Text = "de 21 a 39 horas"; break;
                    case 3: lblHorasSemana.Text = "40 horas"; break;
                    case 4: lblHorasSemana.Text = "mais de 40 horas"; break;
                    case 5: lblHorasSemana.Text = "ininterrupto (24 horas / 7 dias)"; break;
                }
            }

            lblDiasSemana.Text = servico.QuantidadeDiasSemana.HasValue ? servico.QuantidadeDiasSemana.Value.ToString() + " dia(s)" : String.Empty;

            if (servico.Desativado)
            {
                if (servico.IdMotivoDesativacao.HasValue)
                {
                    lblMotivoEncerramento.Text = proxy.Service.GetMotivoDesativacaoServicoById(servico.IdMotivoDesativacao.Value).Descricao;
                    
                    if (servico.IdMotivoDesativacao.HasValue)
                    {
                        tdEncerramento.Visible = true;
                        switch (servico.IdMotivoDesativacao)
                        {
                            case 1:
                                lblDataEncerramento.Text =  servico.DataRegistroLog.HasValue ? servico.DataRegistroLog.Value.ToShortDateString() : "";
                                trDetalhamentoDesativacao.Visible = trMotivoEncerramento.Visible = true;
                                break;
                            case 2:
                                lblDataEncerramento.Text = servico.DataDesativacao.HasValue ? servico.DataDesativacao.Value.ToShortDateString() : "";
                                lblDataEncerramentoServico.Text = "Data do encerramento das atividades deste serviço:";
                                lblDescricaoDetalhamento.Text = "Detalhamento sobre o motivo do encerramento das atividades deste serviço:";
                                lblDetalhamento.Text = servico.Detalhamento;
                                tdEncerramento.Visible = trMotivoEncerramento.Visible = trDetalhamentoDesativacao.Visible = true;
                                break;
                            case 3:
                                lblDataEncerramento.Text = servico.DataDesativacao.HasValue ? servico.DataDesativacao.Value.ToShortDateString() : "";
                                lblDataEncerramentoServico.Text = "Data de vigência das alterações na oferta deste serviço:";
                                lblDescricaoDetalhamento.Text = "Detalhamento sobre as alterações na oferta deste serviço:";
                                lblDetalhamento.Text = servico.Detalhamento;
                                tdEncerramento.Visible = trMotivoEncerramento.Visible = trDetalhamentoDesativacao.Visible = true;
                                break;
                        }
                    }
                }
            }

            if (servico.IdAvaliacaoServico.HasValue)
            {
                switch (servico.IdAvaliacaoServico)
                {
                    case 1: lblAvaliacaoGestor.Text = "Está completamente de acordo com as normativas existentes para seu funcionamento, em especial a Tipificação Nacional de Serviços Socioassistenciais."; break;
                    case 2: lblAvaliacaoGestor.Text = "Apesar de se organizar conforme as normativas existentes, este serviço ainda necessita de algumas adequações."; break;
                    case 3: lblAvaliacaoGestor.Text = "Este serviço ainda não está funcionando conforme as normativas existentes e necessita de um reordenamento"; break;
                }
            }

            if (servico.ServicosRecursosFinanceirosFundosPublicoInfo != null
                            && servico.ServicosRecursosFinanceirosFundosPublicoInfo.Count > 0)
            {

                rptFundosAbas.DataSource = servico.ServicosRecursosFinanceirosFundosPublicoInfo.Where(s => s.Exercicio >= 2022);
                rptFundosAbas.DataBind();


                var servicos = servico.ServicosRecursosFinanceirosFundosPublicoInfo;
       

                rptFundosConteudo.DataSource = servicos;
                rptFundosConteudo.DataBind();

                
             

                foreach (Control listItem in rptFundosAbas.Controls)
                {
                    if (listItem is HtmlGenericControl)
                    {
                        HtmlGenericControl li = listItem as HtmlGenericControl;
                        if (li.ID == "frame1_5_" + hdfAno)
                        {
                            li.Attributes.Add("class", "active");
                        }
                        else
                        {
                            li.Attributes.Remove("class");
                        }
                    }
                }
                trRecursosFinanceiros.Visible = true;
            }
            
            


            lblNomeTecnicoResponsavel.Text = servico.PossuiTecnicoResponsavel.HasValue && servico.PossuiTecnicoResponsavel.Value ? !String.IsNullOrWhiteSpace(servico.NomeTecnicoResponsavel) ?
                servico.NomeTecnicoResponsavel : "-" : "-";

            var lstProgramas = proxyProgramas.Service.GetProgramaProjetoCofinanciamentoByServicoRecursoFinanceiro(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])), 1);

            fldProgramas.Visible = lstProgramas.Count > 0;

            foreach (ProgramaProjetoCofinanciamentoInfo p in lstProgramas)
            {
                lblServicosProgramas.Text += p.ProgramaProjeto.Nome + "<br />";
            }

            var lstTransferencias = proxyProgramas.Service.GetTransferenciaRendaCofinanciamentoByServicoRecursoFinanceiro(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])), 1);
            fldTransferenciaRenda.Visible = lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda != 1 && l.TransferenciaRenda.IdTipoTransferenciaRenda != 2).ToList().Count > 0;
            foreach (ServicoRecursoFinanceiroTransferenciaRendaInfo t in lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda != 1 && l.TransferenciaRenda.IdTipoTransferenciaRenda != 2).ToList())
            {
                lblServicosTransferenciaRenda.Text += t.TransferenciaRenda.Nome + "<br />";
            }

            var lstBeneficios = proxyProgramas.Service.GetBeneficioEventualServicosByServicoRecursoFinanceiro(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])), 1);
            fldBeneficiosEventuais.Visible = lstBeneficios.Count > 0;
            foreach (PrefeituraBeneficioEventualServicoInfo b in lstBeneficios)
            {
                lblServicosBeneficiosEventuais.Text += b.PrefeituraBeneficioEventual.TipoBeneficioEventual.Nome + "<br />";
            }

            fldBeneficiosContinuados.Visible = lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda == 1 || l.TransferenciaRenda.IdTipoTransferenciaRenda == 2).ToList().Count > 0;
            foreach (ServicoRecursoFinanceiroTransferenciaRendaInfo t in lstTransferencias.Where(l => l.TransferenciaRenda.IdTipoTransferenciaRenda == 1 || l.TransferenciaRenda.IdTipoTransferenciaRenda == 2).ToList())
            {
                lblServicosBeneficiosContinuados.Text += t.TransferenciaRenda.Nome + "<br />";
            }

            var recursoshumanos = proxy.Service.GetRecursosHumanosPublicoByIdServicoRecursoFinanceiro(servico.Id);
            if (recursoshumanos != null)
            {

                lblSemEscolaridade.Text = recursoshumanos.SemEscolarizacao.ToString();
                lblFundamental.Text = recursoshumanos.NivelFundamental.ToString();
                lblMedio.Text = recursoshumanos.NivelMedio.ToString();
                lblSuperior.Text = recursoshumanos.NivelSuperior.ToString();
                int total = recursoshumanos.SemEscolarizacao + recursoshumanos.NivelFundamental + recursoshumanos.NivelMedio + recursoshumanos.NivelSuperior;

                lblSuperiorServicoSocial.Text = recursoshumanos.ServicoSocial.ToString();
                lblSuperiorPsicologia.Text = recursoshumanos.Psicologia.ToString();
                lblSuperiorPedagogia.Text = recursoshumanos.Pedagogia.ToString();
                lblSuperiorAntropologia.Text = recursoshumanos.Antropologia.ToString();
                lblSuperiorMusicoTerapia.Text = recursoshumanos.Musicoterapia.ToString();
                lblSuperiorTerapiaOcupacional.Text = recursoshumanos.TerapiaOcupacional.ToString();
                lblSuperiorEconomia.Text = recursoshumanos.Economia.ToString();
                lblSuperiorEconomiaDomestica.Text = recursoshumanos.EconomiaDomestica.ToString();
                lblSociologia.Text = recursoshumanos.Sociologia.ToString();
                lblDireito.Text = recursoshumanos.Direito.ToString();

                lblTotal.Text = total.ToString();

                //txtPosGraduacao.Text = recursoshumanos.PosGraduacao.ToString();
                lblEstagiarios.Text = recursoshumanos.Estagiarios.ToString();
                lblVoluntarios.Text = recursoshumanos.Voluntarios.ToString();

                lblExclusivoServico.Text = recursoshumanos.ExclusivoServico.ToString();
                lblOutroServicos.Text = recursoshumanos.OutrosServicosAssistenciais.ToString();
            }

            trServicosAssociados.Visible = fldProgramas.Visible || fldTransferenciaRenda.Visible || fldBeneficiosEventuais.Visible || fldBeneficiosContinuados.Visible;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["idUnidade"]))
            {
                var idUnidade = Genericos.clsCrypto.Decrypt(Request.QueryString["idUnidade"]);
                var idLocal = Genericos.clsCrypto.Decrypt(Request.QueryString["idLocal"]);
                Response.Redirect("~/BlocoIII/CServicosRecursosFinanceirosPublico.aspx?idLocal=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idLocal)) + "&idUnidade=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idUnidade)));
                return;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["idProjeto"]))
            {
                var idProjeto = Genericos.clsCrypto.Decrypt(Request.QueryString["idProjeto"]);
                Response.Redirect("~/BlocoIII/FProgramaProjetoCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idProjeto)));
                return;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["idTransferenciaRenda"]))
            {
                var idTransferenciaRenda = Genericos.clsCrypto.Decrypt(Request.QueryString["idTransferenciaRenda"]);
                Response.Redirect("~/BlocoIII/FTransferenciaRendaCofinanciamento.aspx?id=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idTransferenciaRenda)));
                return;
            }

        }


        protected void lstRecursosAdicionados_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
            }
        }

    }
}