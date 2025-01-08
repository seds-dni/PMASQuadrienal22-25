<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Rascunho.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Rascunho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function popUpRelatorio(strURL) {
            window.open(strURL, "Relatorio", "resizable=yes, toolbar=no, top=0, left=0, menubar=no, scrollbars=no, width=750, height=500");
        }
	</script>
    <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
        width="580" align="center" border="0">
        <tr>
            <td class="ui-state-default ui-widget-header ui-corner-top" style="height: 30px;
                padding-left: 10px;">
                <img src="Styles/Icones/knotes.png" align="absMiddle" />
                <b>Rascunho do Plano Municipal de Assistência Social</b>
            </td>
        </tr>
        <tr>
            <td>
                <ul>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoI.doc">Bloco I - Identificação</a> </li>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoII.doc">Bloco II - Diagnóstico socioterritorial</a> </li>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoIII.doc">Bloco III - Rede de Proteção Social</a> </li>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoIV.doc">Bloco IV - Financiamento</a> </li>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoV.doc">Bloco V - Planejamento</a> </li>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoVI.doc">Bloco VI - Vigilância, Monitoramento e Avaliação</a> </li>
                    <li style="padding:10px 5px 10px 10px;"><a href="Arquivos/formularioBlocoVII.doc">Bloco VII - CMAS</a> </li>
                </ul>
            </td>
        </tr>
    </table>
</asp:Content>
