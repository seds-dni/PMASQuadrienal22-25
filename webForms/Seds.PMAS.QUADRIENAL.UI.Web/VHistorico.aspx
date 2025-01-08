<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VHistorico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.VHistorico" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="pnlConsulta">
        <ContentTemplate>
            <form>
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>Histórico do Plano Municipal</b>
                            <span class="mif-flow-cascade icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Histórico PMAS">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Data:</b><br />
                                            <asp:Label ID="lblData" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Situação:</b><br />
                                            <asp:Label ID="lblSituacao" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div align="left" class="cell auto-style1">
                                            <b>Responsável:</b><br />
                                            <asp:Label ID="lblResponsavel" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row" id="trParecerDrads" runat="server" visible="false">
                                        <div class="cell" align="left">
                                            <b>Parecer da Drads</b><br />
                                            <asp:Label ID="lblParecerDrads" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <b>
                                                <asp:Label ID="lblTituloTipoParecer" runat="server"></asp:Label></b><br />
                                            <asp:Label ID="lblDescricao" runat="server" />
                                        </div>
                                    </div>

                                    <%--exercicio 1--%>
                                    <tr runat="server" id="trValoresCofinanciamento" visible="false">
                                        <td>
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="90%" style="align: left;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="6" class="info"
                                                            style="height: 20px;">
                                                            <span>Distribuição dos recursos de cofinanciamento estadual repassados pelo sistema Fundo a Fundo em 2022, segundo os programas de trabalho da SEDS:
                                                            </span></th>
                                                    </tr>
                                                    <tr class="info" style="height: 22px;">
                                                        <th width="52%">Programa de Trabalho
                                                        </th>
                                                        <th width="12%">Valor Inicial
                                                        </th>
                                                        <th width="12%">Valor Reprogramado
                                                        </th>
                                                        <th width="12%">Valor Demandas
                                                        </th>
                                                        <th width="12%">Valor Reprogramado Demandas
                                                        </th>
                                                        <th width="12%">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Básica</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasica" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramado" runat="server">0,00</asp:Label>
                                                        </td>

                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaDemandas" runat="server">0,00</asp:Label>
                                                        </td>

                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoDemandas" runat="server">0,00</asp:Label>
                                                        </td>

                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialBasica" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Média Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecial" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramado" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialEspecial" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr tyle="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Alta Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistida" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramado" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalLiberdadeAssistida" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREAS" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramado" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCREAS" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Programas e Projetos</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidario" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramado" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoDemandas" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalSaoPauloSolidario" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Total</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCofinanciamento" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramacao" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalDemandas" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramadoDemandas" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalRecursos" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </td>
                                    </tr>

                                    <%--exercicio 2--%>
                                    <tr runat="server" id="trValoresCofinanciamentoExercicio2" visible="false">
                                        <td>
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="90%" style="align: left;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="6" class="info"
                                                            style="height: 20px;">
                                                            <span>Distribuição dos recursos de cofinanciamento estadual repassados pelo sistema Fundo a Fundo em 2023, segundo os programas de trabalho da SEDS:
                                                            </span></th>
                                                    </tr>
                                                    <tr class="info" style="height: 22px;">
                                                        <th width="52%">Programa de Trabalho
                                                        </th>
                                                        <th width="12%">Valor Inicial
                                                        </th>
                                                        <th width="12%">Valor Reprogramado
                                                        </th>
                                                        <th width="12%">Valor Demandas
                                                        </th>
                                                        <th width="12%">Valor Reprogramado Demandas
                                                        </th>
                                                        <th width="12%">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Básica</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialBasicaExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Média Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialEspecialExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr tyle="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Alta Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalLiberdadeAssistidaExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCREASExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Programas e Projetos</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoDemandasExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalSaoPauloSolidarioExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Total</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCofinanciamentoExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramacaoExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalDemandasExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramadoDemandasExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalRecursosExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr runat="server" id="trValoresCofinanciamentoExercicio3" visible="false">
                                        <td>
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="90%" style="align: left;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="6" class="info"
                                                            style="height: 20px;">
                                                            <span>Distribuição dos recursos de cofinanciamento estadual repassados pelo sistema Fundo a Fundo em 2024, segundo os programas de trabalho da SEDS:
                                                            </span></th>
                                                    </tr>
                                                    <tr class="info" style="height: 22px;">
                                                        <th width="52%">Programa de Trabalho
                                                        </th>
                                                        <th width="12%">Valor Inicial
                                                        </th>
                                                        <th width="12%">Valor Reprogramado
                                                        </th>
                                                        <th width="12%">Valor Demandas
                                                        </th>
                                                        <th width="12%">Valor Reprogramado Demandas
                                                        </th>
                                                        <th width="12%">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Básica</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialBasicaExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Média Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialEspecialExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr tyle="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Alta Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalLiberdadeAssistidaExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCREASExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Programas e Projetos</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoDemandasExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalSaoPauloSolidarioExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Total</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCofinanciamentoExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramacaoExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalDemandasExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramadoDemandasExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalRecursosExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trValoresCofinanciamentoExercicio4" visible="false">
                                        <td>
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="90%" style="align: left;">
                                                <thead>
                                                    <tr>
                                                        <th colspan="6" class="info"
                                                            style="height: 20px;">
                                                            <span>Distribuição dos recursos de cofinanciamento estadual repassados pelo sistema Fundo a Fundo em 2025, segundo os programas de trabalho da SEDS:
                                                            </span></th>
                                                    </tr>
                                                    <tr class="info" style="height: 22px;">
                                                        <th width="52%">Programa de Trabalho
                                                        </th>
                                                        <th width="12%">Valor Inicial
                                                        </th>
                                                        <th width="12%">Valor Reprogramado
                                                        </th>
                                                        <th width="12%">Valor Demandas
                                                        </th>
                                                        <th width="12%">Valor Reprogramado Demandas
                                                        </th>
                                                        <th width="12%">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Básica</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialBasicaReprogramadoDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialBasicaExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Média Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblProtecaoSocialEspecialReprogramadoDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalProtecaoSocialEspecialExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr tyle="height: 22px;">
                                                        <td align="Left">
                                                            <b>Proteção Social Especial de Alta Complexidade</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblLiberdadeAssistidaReprogramadoDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalLiberdadeAssistidaExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCREASReprogramadoDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCREASExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Programas e Projetos</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSaoPauloSolidarioReprogramadoDemandasExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalSaoPauloSolidarioExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="Left">
                                                            <b>Total</b>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalCofinanciamentoExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramacaoExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalDemandasExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalReprogramadoDemandasExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalRecursosExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </td>
                                    </tr>                    

                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClientClick="javascript:history.go(-1);return false;" />
                                        </td>
                                    </tr>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
