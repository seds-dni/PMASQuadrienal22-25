<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Impressao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Impressao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function popUpRelatorio(strURL) {
            window.open(strURL, "Relatorio", "resizable=yes, toolbar=no, top=0, left=0, menubar=no, scrollbars=no, width=750, height=500");
        }

        function showImpressao() {
            console.log($("#ddlExercicio").find(":selected").val())
            $("#ddlExercicio").find(":selected").val() != 0 ?
                    $("#trImpressaoRelatorios").show()
                    : $("#trImpressaoRelatorios").hide();
        }

        function ObterUrl(rel) {
            var exercicio = $("#ddlExercicio").find(":selected").val();
            switch (rel) {
                case 1:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=1');
                    break;
                case 22022:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=2-2022');
                    break;
                case 22023:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=2-2023');
                    break;
                case 22024:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=2-2024');
                    break;
                case 22025:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=2-2025');
                    break;
                case 301:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=301');
                    break;
                case 302:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=302');
                    break;
                case 303:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=303');
                    break;
                case 3031:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=304');
                    break;
                case 3032:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=305');
                    break;
                case 3033:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=306');
                    break;
                case 304:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Reprogramacao');
                    break;
                case 305:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Reprogramacao2');
                    break;
                case 306:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Reprogramacao3');
                    break;
                case 3061:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Reprogramacao4');
                    break;
                case 307:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=307');
                    break;
                case 308:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=308');
                    break;
                case 4:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=4');
                    break;
                case 501:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=501');
                    break;
                case 502:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=502');
                    break;
                case 503:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=503');
                    break;
                case 504:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=504');
                    break;
                case 62022:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=6-2022');
                    break;
                case 62023:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=6-2023');
                    break;
                case 62024:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=6-2024');
                    break;
                case 62025:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=6-2025');
                    break;
                case 7:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=7');
                    break;
                case 8:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Fluxo PMAS');
                    break;
                case 9:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=8');
                    break;
                case 10:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=9');
                    break;
                case 11:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=10');
                    break;
                case 12:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=11');
                    break;
                case 13:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=12');
                    break;
                case 14:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Rel-Gabinete-ex1');
                    break;
                case 15:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Rel-Gabinete-ex2');
                    break;
                case 16:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Rel-Gabinete-ex3');
                    break;
                case 17:
                    popUpRelatorio('popUpImpressao.aspx?Bloco=Rel-Gabinete-ex4');
                    break;
                default:
                    console.log('Nenhuma opção encontrada');
                    break;
            }
        }

    </script>
    <%--<div style="width: 580px; text-align: center; border: 0; margin: 0px auto;">
        <div class="input-control select full-size" style="float: left;">
            <div style="width: auto; margin-right:10px; float: left; position: relative;">Exercício:</div>
            <div style="width: 440px; float: left; position: relative;">
                <select id="ddlExercicio" onchange='javascript: showImpressao()' >
                    <option value="0">[Selecione uma opção:]</option>
                    <option value="2018">2018</option>
                    <option value="2019">2019</option>
                </select>
            </div>
        </div>
    </div>--%>
    <table id="trImpressaoRelatorios" class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
        width="580" align="center" border="0">
        <tr>
            <td class="ui-state-default ui-widget-header ui-corner-top" style="height: 30px; padding-left: 10px;">
                <img src="Styles/Icones/knotes.png" align="absMiddle" alt="Selecione um relatório" />
                <b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr id="trTextoExplicativo" runat="server" visible="false">
            <td style="padding: 5px 5px 5px 15px">
                <b>Este plano municipal ainda não recebeu aprovação do Conselho Municipal de Assistência Social,
             estando disponível apenas sua impressão provisória, que não é válida como documento oficial.
                <br />
                    <asp:Label Font-Bold="true" ID="lblMensagemOrgaoGestor" runat="server"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td>
                <ul>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:ObterUrl(1)" >Bloco 1 - Identificação</a> </li>
                    <li style="padding: 10px 5px 10px 10px;">Bloco 2 - Diagnóstico socioterritorial&nbsp;<a href="javascript:ObterUrl(22022)" >[2022]</a> <a href="javascript:ObterUrl(22023)" >[2023]</a><a href="javascript:ObterUrl(22024)" >[2024]</a><a href="javascript:ObterUrl(22025)" >[2025]</a></li>
                    <li style="padding: 10px 5px 10px 10px;">
                        <div style="width:50px; height:32px; padding-right:10px;  float:left;position:relative; padding-top:0px;">
                            Bloco 3 
                        </div>
                        <div style="float:left;position:relative; width:350px; height:32px; top:-8px; padding-left:10px; border-left:1px solid #d0cfcf; ">
                            <div style="clear:both;">Serviços socioassistenciais &nbsp; <a href="javascript:ObterUrl(301)" >[2022]</a> <a href="javascript:ObterUrl(302)" >[2023]</a><a href="javascript:ObterUrl(307)" >[2024]</a><a href="javascript:ObterUrl(308)" >[2025]</a></div>
                            <div style="clear:both;">Programas, projetos e benefícios &nbsp; <a href="javascript:ObterUrl(303)" >[2022]</a><a href="javascript:ObterUrl(3031)" >[2023]</a><a href="javascript:ObterUrl(3032)" >[2024]</a><a href="javascript:ObterUrl(3033)" >[2025]</a></div>
                        </div>
                    </li>
                    
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:ObterUrl(4)" >Bloco 4 - Interfaces com outras políticas públicas</a> </li>
                    <li style="padding: 10px 5px 10px 10px;">Bloco 5 - Financiamento&nbsp;<a href="javascript:ObterUrl(501)" >[2022]</a> <a href="javascript:ObterUrl(502)" >[2023]</a><a href="javascript:ObterUrl(503)" >[2024]</a><a href="javascript:ObterUrl(504)" >[2025]</a></li>
                    <li style="padding: 10px 5px 10px 10px;">Bloco 5 - Prestação De contas <a href="javascript:ObterUrl(10)" >[2021]</a>  <a href="javascript:ObterUrl(11)" >[2022]</a><a href="javascript:ObterUrl(12)" >[2023]</a><a href="javascript:ObterUrl(13)" >[2024]</a> </li>
                    <li style="padding: 10px 5px 10px 10px;">Bloco 6 - Planejamento&nbsp;<a href="javascript:ObterUrl(62022)" >[2022]</a> <a href="javascript:ObterUrl(62023)" >[2023]</a><a href="javascript:ObterUrl(62024)" >[2024]</a><a href="javascript:ObterUrl(62025)" >[2025]</a></li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:ObterUrl(7)" >Bloco 7 - Vigilância, monitoramento e avaliação</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:ObterUrl(8)" > Parecer da DRADS</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:ObterUrl(9)" >Bloco 8 - CMAS</a> </li>
                    <li style="padding: 10px 5px 10px 10px;">Reprogramação de recursos estaduais <a href="javascript:ObterUrl(304)" >[2022]</a>  <a href="javascript:ObterUrl(305)" >[2023]</a><a href="javascript:ObterUrl(306)" >[2024]</a><a href="javascript:ObterUrl(3061)" >[2025]</a></li>
                    <li style="padding: 10px 5px 10px 10px;">Relatório informativo <a href="javascript:ObterUrl(14)" >[2022]</a>  <a href="javascript:ObterUrl(15)" >[2023]</a><a href="javascript:ObterUrl(16)" >[2024]</a><a href="javascript:ObterUrl(17)" >[2025]</a></li>
                    
                    <%--<li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=1')">Bloco 1 - Identificação</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=2')">Bloco 2 - Diagnóstico socioterritorial</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=3')">Bloco 3 - Rede de Proteção Social</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=4')">Bloco 4 - Interfaces com outras políticas públicas</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=5')">Bloco 5 - Financiamento</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=6')">Bloco 6 - Planejamento</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=7')">Bloco 7 - Vigilância, monitoramento e avaliação</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=Fluxo PMAS')">Parecer da DRADS</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=8')">Bloco 8 - CMAS</a> </li>
                    <li style="padding: 10px 5px 10px 10px;"><a href="javascript:popUpRelatorio('popUpImpressao.aspx?Bloco=Reprogramacao')">Reprogramação de recursos estaduais</a> </li>--%>
                </ul>
            </td>
        </tr>
        
        <%-- <tr id="trNao" runat="server" visible="false">
            <td style="padding:20px 20px 20px 20px;" align="center">
                <b class="titulo"> Não é possível visualizar a impressão do Plano neste momento.<br />
                O plano deverá ser aprovado ou rejeitado.</b>
            </td>
        </tr>--%>
    </table>

</asp:Content>
