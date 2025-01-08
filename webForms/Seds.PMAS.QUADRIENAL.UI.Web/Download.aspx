<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Download.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .toggle {
        }

        .displayObject {
            display: none;
        }
    </style>
    <script type="text/javascript">
         
        $(function () {
            $(".heading").click(function () {
                var opt = {};
                //$(this).parent().parent().next().find(".displayObject").first().toggle("blind", opt, 500);
                if ($(this).text().indexOf("expandir") >= 0) {
                    $(this).text($(this).text().replace("expandir", "ocultar"));
                }
                else {
                    $(this).text($(this).text().replace("ocultar", "expandir"));
                }
            });
        });
    </script>
    <form name="frmDownloads">
        <div class="head">
            <span class="mif-download2 mif-lg"></span>
            Downloads   <span class="mif-download icon"></span>
        </div>
        <div class="accordion" data-role="accordion" data-close-any="true">
            <div class="frame">
                <div class="heading">
                    Manuais e Orientações do Sistema (Expandir / Ocultar)
                </div>
                <div class="content">
                    <div class="formInput" data-text="Manuais e Orientações do Sistema">
                        <div class="grid">
                            <div class="row">
                                <ul>
                                    <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/ManuaisOrientacoesSistema/Orientacoessobreaatualizacaoanualpara2022-PMAS_2022-2025.pdf"
                                        target="_blank">Orientações para atualização anual de 2022 – PMAS 2022/2025</a>
                                    </li>
                                    <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/ManuaisOrientacoesSistema/Orientacoes_para_registro_da_Reprogramacao_no_PMASweb.pdf"
                                        target="_blank">Orientações para registro da Reprogramação no PMASweb</a>
                                    </li>
                                    <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/ManuaisOrientacoesSistema/Orientacoesparadesativacaodeserviços.pdf"
                                        target="_blank">Orientações para desativação de serviços</a>
                                    </li>
                                    <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/ManuaisOrientacoesSistema/Execução_Financeira_2022_e_Prestação_de_Contas_2022_PMASweb_2022_2025.pdf"
                                        target="_blank">Execução Financeira 2022 e Prestação de Contas 2022 PMASweb 2022/2025</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="frame">
                <div class="heading">
                    Formulários de Acesso (Expandir / Ocultar)
                </div>
                <div class="content">
                    <div class="formInput" data-text="Formulários de Acesso">
                        <div class="grid">
                            <ul>

                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_CONSULTA.pdf"
                                    target="_blank">Formulário de Acesso – Perfil Consulta</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_CONSULTA_REGIONAL.pdf"
                                    target="_blank">Formulário de Acesso – Perfil Consulta Regional</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_CMAS.pdf"
                                    target="_blank"> Formulário de Acesso – Perfil CMAS</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_DIRETOR_REGIONAL.pdf"
                                    target="_blank">Formulário de Acesso – Perfil Diretor Regional</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_DRADS.pdf"
                                    target="_blank"> Formulário de Acesso – Perfil DRADS</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_DRADS_ADM.pdf"
                                    target="_blank"> Formulário de Acesso – Perfil DRADS Administrador</a> 
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/FormulariosAcesso/PMAS_Formulário_perfil_ÓRGÃO_GESTOR.pdf"
                                    target="_blank"> Formulário de Acesso – Perfil Órgão Gestor</a> 
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="frame">
                <div class="heading">
                    Orientações Técnicas e Normativas SUAS (Expandir / Ocultar)
                </div>
                <div class="content">
                    <div class="formInput" data-text="Orientações Técnicas e Normativas SUAS">
                        <div class="grid">
                            <ul>
                                <li><a href="Arquivos/II Plano Decenal da Assistência Social 2016-2026.pdf">II PLANO DECENAL DA ASSISTÊNCIA SOCIAL (2016/2026)</a></li>
                                    <li><a href="Arquivos/Caderno de Orientações Técnicas - MSE em meio aberto.pdf">Caderno de Orientações Técnicas - MSE em meio aberto</a></li>
                                    <li><a href="Arquivos/Trabalho Social com Famílias Indígenas.pdf">Trabalho Social com Famílias Indígenas</a></li>
                                <li class="node" style="padding: 10px 5px 10px 10px;"><a href="Arquivos/1-NOB2012.pdf" target="_blank">1 - NOB 2012</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/2-TipificacaoNacionaldeServiçosSocioassistenciais-2009.pdf"
                                    target="_blank">2 - Tipificação Nacional de Serviços Socioassistenciais - 2009</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/4-Orientacoestecnicas-VigilanciaSocioassistencias.pdf"
                                    target="_blank">3 - Orientações técnicas - Vigilância Socioassistenciais</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/5-ConcepcaodeaConvivenciaeFortalecimentodeVinculos.pdf"
                                    target="_blank">4 - Concepção de Convivência e Fortalecimento de Vínculos</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/6-OrientacoesTecnicasCRAS-2009.pdf"
                                    target="_blank">5 - Orientações Técnicas CRAS - 2009</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/7-OCRASquetemosoCRASquequeremos-2011.pdf"
                                    target="_blank">6 - O CRAS que temos o CRAS que queremos - 2011</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/8-OrientacoesPAIFvolI-2012.pdf"
                                    target="_blank">7 - Orientações PAIF vol I - 2012</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/9-OrientacoesPAIFVolII-2012.pdf"
                                    target="_blank">8 - Orientações PAIF Vol II - 2012</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/10-Caderno_de_orientacoes_Paif_SCFV.pdf"
                                    target="_blank">9 - Caderno de Orientações : PAIF e SCFV - articulação necessária</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/11-OrientacoesTecnicasCREAS-2011.pdf"
                                    target="_blank">10 - Orientações Técnicas CREAS - 2011</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/12-PerguntaserespostasCREAS-2011.pdf"
                                    target="_blank">11 - Perguntas_e_respostas CREAS - 2011</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/13-OrientacoesTecnicasCentroPop-2011.pdf"
                                    target="_blank">12 - Orientações Técnicas Centro Pop - 2011</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/14-Perguntas_e_RespostasCentroPop-2011.pdf"
                                    target="_blank">13 - Perguntas_e_Respostas Centro Pop - 2011</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/15-OrientacoesTecnicasServicosdeAcolhimento-2009.pdf"
                                    target="_blank">14 - Orientações Técnicas Serviços de Acolhimento - 2009</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/16-PerguntaseRespostas-ServicoespecializadoemabordagemSocial.pdf"
                                    target="_blank">15 - Perguntas e Respostas - Serviço especializado em Abordagem
                                        Social</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/17-Orientacoestecnicas-ServicoparapessoacomdeficienciaemCentroDia.pdf"
                                    target="_blank">16 - Orientações técnicas - Serviço para pessoa com deficiência
                                        em Centro Dia</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/18-PerguntaseRespostas-ResidenciaInclusiva.pdf"
                                    target="_blank">17 -Perguntas e Respostas - Residência Inclusiva</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/19-CadernodeOrientacoesIGD-SUAS-2012.pdf"
                                    target="_blank">18 - Caderno de Orientações IGD-SUAS - 2012</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/20-Caderno_de_orientacao_pagamento_profissionais.pdf"
                                    target="_blank">19 - Caderno de Orientações técnicas sobre gastos de pagamento de profissionais</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/OrientacoesTecnicasNormativasSUAS/20-NotaTecnica25de2020.pdf"
                                    target="_blank">20 - Nota Técnica 25 de 2020</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="frame">
                <div class="heading">
                    Relatórios de Vulnerabilidade – DRADS (Expandir / Ocultar)
                </div>
                <div class="content">
                    <div class="formInput" data-text="Relatórios de Vulnerabilidade – DRADS">
                        <div class="grid">
                            <ul>
                                <li class="node" style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_ALTA_NOROESTE.pdf" target="_blank">DRADS_ALTA_NOROESTE</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_ALTA_PAULISTA.pdf" target="_blank">DRADS_ALTA_PAULISTA</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_ALTA_SOROCABANA.pdf" target="_blank">DRADS_ALTA_SOROCABANA</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_ARARAQUARA.pdf" target="_blank">DRADS_ARARAQUARA</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_AVARE.pdf" target="_blank">DRADS_AVARÉ</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_BAIXADA_SANTISTA.pdf" target="_blank">DRADS_BAIXADA_SANTISTA</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_BARRETOS.pdf" target="_blank">DRADS_BARRETOS</a>

                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_BAURU.pdf"
                                    target="_blank">DRADS_BAURU</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_BOTUCATU.pdf"
                                    target="_blank">DRADS_BOTUCATU</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_CAMPINAS.pdf"
                                    target="_blank">DRADS_CAMPINAS</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_CAPITAL.pdf"
                                    target="_blank">DRADS_CAPITAL</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_FERNANDOPOLIS.pdf"
                                    target="_blank">DRADS_FERNANDÓPOLIS</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_FRANCA.pdf"
                                    target="_blank">DRADS_FRANCA</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_GRANDE_SP_ABC.pdf"
                                    target="_blank">DRADS_GRANDE_SP_ABC</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_GRANDE_SP_LESTE.pdf"
                                    target="_blank">DRADS_GRANDE_SP_LESTE</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_GRANDE_SP_NORTE.pdf"
                                    target="_blank">DRADS_GRANDE_SP_NORTE</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_GRANDE_SP_OESTE.pdf"
                                    target="_blank">DRADS_GRANDE_SP_OESTE</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_ITAPEVA.pdf"
                                    target="_blank">DRADS_ITAPEVA</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_MARILIA.pdf"
                                    target="_blank">DRADS_MARÍLIA</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_MOGIANA.pdf"
                                    target="_blank">DRADS_MOGIANA</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_PIRACICABA.pdf"
                                    target="_blank">DRADS_PIRACICABA</a>
                                </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_RIBEIRAO_PRETO.pdf"
                                    target="_blank">DRADS_RIBEIRÃO_PRETO</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_SAO_JOSE_DO_RIO_PRETO.pdf"
                                    target="_blank">DRADS_SÃO_JOSÉ_DO_RIO_PRETO</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_SOROCABA.pdf"
                                    target="_blank">DRADS_SOROCABA</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_VALE_DO_PARAIBA.pdf"
                                    target="_blank">DRADS_VALE_DO_PARAÍBA</a> </li>
                                <li style="padding: 10px 5px 10px 10px;"><a href="Arquivos/RelatorioVulnerabilidade/DRADS_VALE_DO_RIBEIRA.pdf"
                                    target="_blank"> DRADS_VALE_DO_RIBEIRA</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

























    <%--<table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
        width="580" align="center" border="0">--%>
    <%--  <tr>
            <td class="ui-state-default ui-widget-header ui-corner-top" style="height: 30px; padding-left: 10px;">
                <img src="Styles/Icones/knotes.png" align="absMiddle" />
                <b>Downloads</b>
            </td>
        </tr>
        <tr>
            <td class="ui-state-default ui-th-column ui-th-ltr" align="center" style="height: 20px;">
                <a class="toggle" href="javascript:void(0);"></a>
            </td>
        </tr>
        <tr>
            <td>
                <div class="displayObject">
                    <table width="100%">
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>--%>
    <%--   <tr>
            <td class="ui-state-default ui-th-column ui-th-ltr" align="center" style="height: 20px;">
                <a class="toggle" href="javascript:void(0);">Artigos e publicações (expandir)</a>
            </td>
        </tr>
        <tr>
            <td>
                <div class="displayObject">
                    <table width="100%">
                        <tr>
                            <td>
                               
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>--%>
</asp:Content>
