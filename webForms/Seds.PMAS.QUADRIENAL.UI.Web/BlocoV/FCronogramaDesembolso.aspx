<%@ Page Language="C#" MasterPageFile="~/Site.Master" CodeBehind="FCronogramaDesembolso.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FCronogramaDesembolso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .container {
            width: 100%;
        }

        .formInput {
            padding: .225rem 0.225rem .225rem 0.6rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    &nbsp;<script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
                            <div class="row cells2">

            <form name="frmCronograma">
                <div id="Quadrienal">
                    <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnLoadExercicio1_Click"></asp:Button>
                    <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnLoadExercicio2_Click"></asp:Button>
                    <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnLoadExercicio3_Click"></asp:Button>
                    <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnLoadExercicio4_Click"></asp:Button>
                </div>
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame" id="frmCronogramaDesembolso" runat="server">
                        <div class="heading">
                            <asp:Label ID="lblNumeracao" runat="server" />&nbsp;-&nbsp;
                                    <asp:Label ID="lblCabecalho" runat="server" />
                            <a href="#" runat="server" id="linkAlteracoesQuadro" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                           <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="cronograma de desembolso">
                                <div class="grid">
                                    <div class="row cells3" style="overflow-x: auto;">
                                        <div class="cell" style="width: 45%!important; padding: 0!important;">
                                            <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" style="font-size: 0.68rem !important;">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="7" style="height: 20px; font-size: 0.71rem;" runat="server" id="tdRedePublica">Rede Direta
                                                        </th>
                                                    </tr>
                                                    <tr id="trHeaderProgramas" runat="server" visible="false">
                                                        <td align="center" id="tdParcelasPrograma" visible="false" style="" rowspan="4">Parcelas
                                                        </td>
                                                        <td align="center" rowspan="3" id="tdReprogramacao" visible="false">Parcelas</td>
                                                        <td id="Td1" align="center" rowspan="3" runat="server">Recursos estaduais<br />
                                                            disponibilizados
                                                        </td>
                                                        <td align="center" id="td2" runat="server" colspan="5" style="height: 22px;">Previsão de Execução dos Recursos
                                                        </td>
                                                    </tr>

                                                    <tr id="trHeaderBenenifios" runat="server">
                                                        <td align="center" id="tdParcelas" runat="server" rowspan="2" style="height: 78px;">Parcelas
                                                        </td>
                                                        <td align="center" rowspan="2" id="tdRecursosEstaduais" runat="server">Recursos estaduais
                                                    <br />
                                                            disponibilizados
                                                        </td>
                                                        <td align="center" colspan="4" style="height: 22px;"
                                                            runat="server" id="tdItensDespesa">Previsão de Execução dos Recursos
                                                        </td>
                                                    </tr>

                                                    <tr id="trProgramas" visible="false" runat="server">
                                                        <td align="center" colspan="2">Custeio
                                                        </td>
                                                        <td align="center" id="td4" colspan="2" runat="server">Investimento
                                                        </td>
                                                        <td align="center" rowspan="2">Total
                                                        </td>
                                                    </tr>

                                                    <tr id="trRHDC" runat="server" visible="false">
                                                        <td align="center" style="height: 22px;">Recursos Humanos
                                                        </td>
                                                        <td align="center">Outras despesas de custeio
                                                        </td>
                                                        <td align="center" style="height: 22px;">Aquisição de Equipamentos
                                                        </td>
                                                        <td align="center">Obras
                                                        </td>

                                                    </tr>
                                                    <tr id="trProgramaProjeto" runat="server" visible="false">
                                                        <td id="Td5" align="center" style="height: 22px;" rowspan="2" runat="server">Custeio
                                                        </td>
                                                        <td id="Td6" align="center" colspan="2" runat="server">Investimento
                                                        </td>
                                                        <td id="Td7" align="center" runat="server" rowspan="2">Total
                                                        </td>
                                                    </tr>

                                                    <tr id="trBeneficiosEventuais" runat="server" visible="false">
                                                        <td id="Td15" align="center" style="height: 22px;" rowspan="2" runat="server">Custeio
                                                        </td>
                                                        <td id="Td16" align="center" colspan="2" runat="server">Investimento
                                                        </td>
                                                        <td id="Td17" align="center" runat="server" rowspan="2">Total
                                                        </td>
                                                    </tr>

                                                    <tr id="trBeneficio" runat="server">
                                                        <td align="center" style="height: 22px;" id="tdCusteio" runat="server">Custeio
                                                        </td>
                                                        <td align="center" runat="server"
                                                            id="tdInvestimento">Investimento
                                                        </td>
                                                        <td align="center" runat="server"
                                                            id="tdEquipamentos">Aquisição de Equipamentos
                                                        </td>

                                                        <td align="center" runat="server"
                                                            id="tdObras">Obras
                                                        </td>
                                                    </tr>
                                                    <tr id="trHeaderRecursosReprogramados" style="height: 25px;" runat="server" class="bg-lime fg-white padding10">
                                                        <td id="tdRecursosReprogramadosAnoAnterior"  colspan="6" style="padding-right: 10px; text-align: right; font-weight: normal;">Recursos reprogramados ano anterior
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblValorReprogramado" runat="server" Text="0,00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trRecursosReprogramados" runat="server" class="bg-lime fg-white padding10">
                                                        <td>Parcela única</td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramacaoRecursosDisponibilizados" runat="server" class="ui-state-reprogramacao-textbox" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td id="tdReprogramacaoRH" runat="server">
                                                            <asp:TextBox ID="txtReprogramacaoRH" runat="server"  class="ui-state-reprogramacao-textbox" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramacaoCusteio" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramacaoInvestimento" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td id="tdReprogramacaoObras" runat="server">
                                                            <asp:TextBox ID="txtReprogramacaoObras" runat="server"   MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <strong>
                                                                <asp:Label ID="lblTotalRecursosReprogramadoAnoAnterior" runat="server" Style="text-align: right; font-size: 0.7rem;">0,00</asp:Label></strong>
                                                        </td>
                                                    </tr>

                                                    <tr id="trHeaderDemandasParalamentares" style="height: 25px;" runat="server" class="bg-warning fg-white padding10">
                                                        <td id="tdDemandasParlamentares"  colspan="6" style="padding-right: 10px; text-align: right; font-weight: normal;">Demandas Parlamentares
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblValorDemandas" runat="server" Text="0,00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trDemandasParlamentaresParcela" runat="server" class="bg-warning fg-white padding10">
                                                        <td>Parcela única</td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemandasParlamentaresDisponibilizados" runat="server" class="ui-state-reprogramacao-textbox" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td id="td12" runat="server">
                                                            <asp:TextBox ID="txtDemandasRH" runat="server" class="ui-state-reprogramacao-textbox" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemandasCusteio" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemandasInvestimento" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemandasObras" runat="server"   MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <strong>
                                                                <asp:Label ID="lblTotalDemandasParlamentares" runat="server" Style="text-align: right; font-size: 0.7rem;">0,00</asp:Label></strong>
                                                        </td>
                                                    </tr>

                                                    <tr id="trHeaderReprogramadoDemandasParalamentares" style="height: 25px;" runat="server" class="bg-yellow fg-white padding10">
                                                        <td id="td13"  colspan="6" style="padding-right: 10px; text-align: right; font-weight: normal;">Reprogramação Demandas Parlamentares
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblValorReprogramadoDemandas" runat="server" Text="0,00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trReprogramadoDemandasParlamentaresParcela" runat="server" class="bg-yellow fg-white padding10">
                                                        <td>Parcela única</td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramadoDemandasParlamentaresDisponibilizados" runat="server" class="ui-state-reprogramacao-textbox" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td id="td14" runat="server">
                                                            <asp:TextBox ID="txtReprogramadoDemandasRH" runat="server" class="ui-state-reprogramacao-textbox" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramadoDemandasCusteio" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramadoDemandasInvestimento" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramadoDemandasObras" runat="server"   MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-weight: normal; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <strong>
                                                                <asp:Label ID="lblTotalReprogramadoDemandasParlamentares" runat="server" Style="text-align: right; font-size: 0.7rem;">0,00</asp:Label></strong>
                                                        </td>
                                                    </tr>

                                                    <tr id="trRecursosExercicioAtual" style="padding-right: 10px;" runat="server">
                                                        <td colspan="6" style="height: 25px; padding-right: 10px;" runat="server" id="tdRecursosExercicioAtual" >Recursos do Exercício Atual</td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblRecursosExercicioAtual" runat="server" Text="0,00"></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    </tr>
                                                </thead>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" style="width: 5%;" class="info"><b>1° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%-- <asp:Label ID="lblTot1Publica" runat="server" Width="100px" Visible="False" Height="16px"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot1Publica" MaxLength="14" runat="server" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio1" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC01">
                                                        <asp:TextBox ID="txtOutroCusteio1" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>

                                                    <td align="center" runat="server" id="tdInvestimento1">
                                                        <asp:TextBox ID="txtInv1Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras1">
                                                        <asp:TextBox ID="txtObras1" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica1">
                                                        <asp:Label ID="lblTotalExecPublica1" runat="server" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>2° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--            <asp:Label ID="lblTot2Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot2Publica" runat="server" MaxLength="14" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC02">
                                                        <asp:TextBox ID="txtOutroCusteio2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td id="Td8" align="center" runat="server">
                                                        <asp:TextBox ID="txtInv2Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras2" >
                                                        <asp:TextBox ID="txtObras2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica2">
                                                        <asp:Label ID="lblTotalExecPublica2" runat="server" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>3° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%-- <asp:Label ID="lblTot3Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot3Publica" MaxLength="14" runat="server" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC03">
                                                        <asp:TextBox ID="txtOutroCusteio3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td id="Td9" align="center" runat="server">
                                                        <asp:TextBox ID="txtInv3Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras3" >
                                                        <asp:TextBox ID="txtObras3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica3">
                                                        <asp:Label ID="lblTotalExecPublica3" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>4° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%-- <asp:Label ID="lblTot4Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot4Publica" runat="server" MaxLength="14" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC04">
                                                        <asp:TextBox ID="txtOutroCusteio4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv4Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras4">
                                                        <asp:TextBox ID="txtObras4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica4">
                                                        <asp:Label ID="lblTotalExecPublica4" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>5° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--        <asp:Label ID="lblTot5Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot5Publica" MaxLength="14" runat="server" Visible="false" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio5" runat="server" MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC05">
                                                        <asp:TextBox ID="txtOutroCusteio5" runat="server" MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv5Publica" runat="server" MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras5" >
                                                        <asp:TextBox ID="txtObras5" runat="server"  MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica5">
                                                        <asp:Label ID="lblTotalExecPublica5" runat="server" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>6° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--    <asp:Label ID="lblTot6Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot6Publica" MaxLength="14" runat="server" Visible="false" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio6" runat="server" MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC06">
                                                        <asp:TextBox ID="txtOutroCusteio6" runat="server" MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv6Publica" runat="server"  MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras6" >
                                                        <asp:TextBox ID="txtObras6" runat="server" MaxLength="14" Style="font-size: 0.7rem; width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica6">
                                                        <asp:Label ID="lblTotalExecPublica6" runat="server" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>7° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--            <asp:Label ID="lblTot7Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot7Publica" runat="server" MaxLength="14" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio7" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC07">
                                                        <asp:TextBox ID="txtOutroCusteio7" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv7Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras7" >
                                                        <asp:TextBox ID="txtObras7" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica7">
                                                        <asp:Label ID="lblTotalExecPublica7" runat="server" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>8° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--       <asp:Label ID="lblTot8Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot8Publica" MaxLength="14" runat="server" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio8" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC08">
                                                        <asp:TextBox ID="txtOutroCusteio8" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv8Publica"  runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras8" >
                                                        <asp:TextBox ID="txtObras8" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica8">
                                                        <asp:Label ID="lblTotalExecPublica8" runat="server" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>9° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--           <asp:Label ID="lblTot9Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot9Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio9" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC09">
                                                        <asp:TextBox ID="txtOutroCusteio9" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv9Publica"  runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras9" >
                                                        <asp:TextBox ID="txtObras9" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica9">
                                                        <asp:Label ID="lblTotalExecPublica9" runat="server" MaxLength="90px" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>10° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--         <asp:Label ID="lblTot10Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot10Publica" MaxLength="14" runat="server" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio10" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC10">
                                                        <asp:TextBox ID="txtOutroCusteio10" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv10Publica" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras10" >
                                                        <asp:TextBox ID="txtObras10" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica10">
                                                        <asp:Label ID="lblTotalExecPublica10" runat="server" MaxLength="90px" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>11° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--   <asp:Label ID="lblTot11Publica" runat="server" Width="100px" Visible="false" Height="16px"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot11Publica" MaxLength="14" runat="server" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC11">
                                                        <asp:TextBox ID="txtOutroCusteio11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv11Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras11" >
                                                        <asp:TextBox ID="txtObras11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica11">
                                                        <asp:Label ID="lblTotalExecPublica11" runat="server" MaxLength="90px" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>12° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <%--      <asp:Label ID="lblTot12Publica" runat="server" Width="100px" Visible="false"></asp:Label>--%>
                                                        <asp:TextBox ID="txtTot12Publica" MaxLength="14" runat="server" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtCusteio12" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" visible="false" id="tdOC12">
                                                        <asp:TextBox ID="txtOutroCusteio12" MaxLength="14" runat="server" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox></td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtInv12Publica" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" runat="server" id="tdObras12">
                                                        <asp:TextBox ID="txtObras12" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right" runat="server" id="tdTotalExecucaoPublica12">
                                                        <asp:Label ID="lblTotalExecPublica12" runat="server" MaxLength="90px" Text="0,00" Style="font-size: 0.7rem;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="info" style="height: 22px;">
                                                    <td align="center" class="info"><b>Total</b>
                                                    </td>
                                                    <td align="center">
                                                        <b>
                                                            <asp:Label ID="lblTotalPublica" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td align="center">
                                                        <b>
                                                            <asp:Label ID="lblTotalCusteio" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td align="center" id="tdTotalOC" runat="server" visible="false">
                                                        <b>
                                                            <asp:Label ID="lblTotalOC" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td id="Td10" align="center" runat="server">
                                                        <b>
                                                            <asp:Label ID="lblTotalInvestimentoPublica" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td align="center" runat="server" id="tdTotalObraPublica">
                                                        <b>
                                                            <asp:Label ID="lblTotalObraPublica" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td align="center">
                                                        <b>
                                                            <asp:Label ID="lblTotalExecPublica" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>

                                                </tr>
                                                <tr class="info" id="trTotalCofinanciamento" style="height: 22px;">
                                                    <td style="text-align: right !important;"
                                                        runat="server" id="tdTotalRedePublica">
                                                        <%--Valor Total do Cofinanciamento:--%><b><asp:Label ID="lblValorCofinanciamentoText" runat="server"></b></asp:Label>
                                                        <%--Bruno V.--%>
                                                    </td>
                                                    
                                                    <td>
                                                        <b>
                                                            <asp:Label ID="lblTotalCofinanciamentoPublica" runat="server" Width="100px"></asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="7" id="tdLimparRedePublica" runat="server">
                                                        <br />
                                                        <asp:Button ID="btnLimparRedePublica" runat="server" Text="Limpar" Width="89px" OnClick="btnLimparRedePublica_Click"
                                                            Height="25px"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        
                                        <div class="cell" style="width: 45%!important; padding: 0!important;" runat="server" id="trRedeIndireta">
                                            <table border="0" cellpadding="0" cellspacing="0" class="table border bordered tbCronograma" style="font-size: 0.70rem;">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="7" style="height: 20px; font-size: 0.71rem;" id="thRedePrivada" runat="server">
                                                            <span>Rede Indireta</span>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" rowspan="3" style="height: 78px;">Parcelas
                                                        </td>
                                                        <td align="center" rowspan="3">Recursos estaduais
                                                            disponibilizados
                                                        </td>
                                                        <td align="center" id="tdPrevisaoExecPrivada" runat="server" colspan="6">Previsão de Execução dos Recursos
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">Custeio
                                                        </td>
                                                        <td colspan="2" align="center" id="tdInvestimentoPrivado" runat="server">Investimento
                                                        </td>
                                                        <td align="center" rowspan="2">Total
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="height: 22px;">Recursos
                                                            Humanos
                                                        </td>

                                                        <td align="center">Outras despesas de  custeio
                                                        </td>

                                                        <td align="center" style="height: 22px;" runat="server" id="tdEquipamentosPrivado">Aquisição de Equipamentos
                                                        </td>

                                                        <td align="center" style="height: 22px;" runat="server" id="tdObrasPrivadas">Obras
                                                        </td>
                                                    </tr>

                                                    <tr id="trHeaderRecursosReprogramadosPrivado" style="height: 25px;" class="bg-lime fg-white padding10" runat="server">
                                                        <td colspan="6" style="padding-right: 10px; text-align: right; font-weight: normal;">Recursos reprogramados ano anterior
                                                        </td>
                                                        <td style="padding-right: 10px; text-align: right;">
                                                            <strong>
                                                                <asp:Label ID="lblValorReprogramadoPrivado" runat="server" Text="0,00" MaxLength="14" ></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trRecursosReprogramadosPrivado" runat="server" class="bg-lime fg-white padding10">
                                                        <td>Parcela única</td>
                                                        <td>
                                                            <asp:TextBox ID="txtRecursosReprogramadoPrivado" class="ui-state-reprogramacao-textbox"  MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRecursosHumanosReprogramadoPrivado" class="ui-state-reprogramacao-textbox" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutrasCusteioReprogramadoPrivado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEquipamentosPrivadoReprogramado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObrasReprogramadoPrivado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td class="ui-state-sub ui-th-column ui-th-rtl">
                                                            <asp:Label ID="lblTotalRecursosReprogramadoPrivada" runat="server" Style="text-align: right" MaxLength="14" >0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr id="trHeaderDemandasParlamentaresPrivado" style="height: 25px;" class="bg-warning fg-white padding10" runat="server">
                                                        <td colspan="6" style="padding-right: 10px; text-align: right; font-weight: normal;">Demandas Parlamentares
                                                        </td>
                                                        <td style="padding-right: 10px; text-align: right;">
                                                            <strong>
                                                                <asp:Label ID="lblValorDemandasPrivado" runat="server" Text="0,00" MaxLength="14" ></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trDemandasParlamentaresParcelaPrivado" runat="server" class="bg-warning fg-white padding10">
                                                        <td>Parcela única</td>
                                                        <td>
                                                            <asp:TextBox ID="txtDemandasParlamentaresPrivado" class="ui-state-reprogramacao-textbox" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRecursosHumanosDemandasPrivado" class="ui-state-reprogramacao-textbox" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutrasCusteioDemandasPrivado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEquipamentosPrivadoDemandas" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObrasDemandasPrivado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td class="ui-state-sub ui-th-column ui-th-rtl">
                                                            <asp:Label ID="lblTotalDemandasParlamentaresPrivada" runat="server" Style="text-align: right" MaxLength="14" >0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr id="trHeaderReprogramadoDemandasParlamentaresPrivado" style="height: 25px;" class="bg-yellow fg-white padding10" runat="server">
                                                        <td colspan="6" style="padding-right: 10px; text-align: right; font-weight: normal;">Reprogramação Demandas Parlamentares
                                                        </td>
                                                        <td style="padding-right: 10px; text-align: right;">
                                                            <strong>
                                                                <asp:Label ID="lblValorReprogramadoDemandasPrivado" runat="server" Text="0,00" MaxLength="14" ></asp:Label></strong>
                                                        </td>
                                                    </tr>
                                                    <tr id="trReprogramadoDemandasParlamentaresParcelaPrivado" runat="server" class="bg-yellow fg-white padding10">
                                                        <td>Parcela única</td>
                                                        <td>
                                                            <asp:TextBox ID="txtReprogramadoDemandasParlamentaresPrivado" class="ui-state-reprogramacao-textbox" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRecursosHumanosReprogramadoDemandasPrivado" class="ui-state-reprogramacao-textbox" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutrasCusteioReprogramadoDemandasPrivado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEquipamentosPrivadoReprogramadoDemandas" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtObrasReprogramadoDemandasPrivado" MaxLength="14" runat="server" Style="text-align: right; font-weight: normal; width: 80%; padding: 2px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                        </td>
                                                        <td class="ui-state-sub ui-th-column ui-th-rtl">
                                                            <asp:Label ID="lblTotalReprogramadoDemandasParlamentaresPrivada" runat="server" Style="text-align: right" MaxLength="14" >0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr id="trRecursosExercicioAtualPrivado" runat="server">
                                                        <td colspan="6" style="height: 25px; padding-right: 10px;">Recursos do Exercício Atual</td>
                                                        <td align="center">
                                                            <asp:Label ID="lblValorExercicioAtualPrivado" runat="server" Text="0,00" MaxLength="14"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </thead>

                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>1° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot1" runat="server" Width="100px" Visible="False" Height="16px"></asp:Label>
                                                        <asp:TextBox ID="txtTot1" runat="server" MaxLength="14" Visible="false" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH1" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC1" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST1" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada1" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada1" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada1" runat="server" Enabled="false" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>2° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot2" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST2" runat="server"  MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada2" runat="server" >
                                                        <asp:TextBox ID="txtObrasPrivada2" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada2" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>3° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot3" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot3" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada3" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada3" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada3" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>4° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot4" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada4" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada4" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada4" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>5° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot5" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot5" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH5" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC5" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST5" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada5" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada5" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada5" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>6° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot6" runat="server" Visible="false" Width="100px"></asp:Label>
                                                        <asp:TextBox ID="txtTot6" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH6" runat="server" MaxLength="14" Style="width: 80%; text-align: right; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC6" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST6" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada6" runat="server" >
                                                        <asp:TextBox ID="txtObrasPrivada6" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada6" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>7° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot7" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot7" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH7" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC7" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST7" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada7" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada7" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada7" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>8° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot8" runat="server" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot8" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH8" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC8" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST8" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada8" runat="server" >
                                                        <asp:TextBox ID="txtObrasPrivada8" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada8" runat="server" MaxLength="90px" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>9° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot9" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot9" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH9" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC9" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST9" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada9" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada9" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada9" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>10° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot10" runat="server" Visible="false" Width="100px"></asp:Label>
                                                        <asp:TextBox ID="txtTot10" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH10" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC10" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST10" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada10" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada10" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada10" runat="server" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>11° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot11" runat="server" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtTot11" runat="server" Visible="false" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada11" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada11" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada11" runat="server" Width="90" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="min-height: 22px;">
                                                    <td align="center" class="info">
                                                        <b>12° parcela</b>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTot12" Visible="false" runat="server" Width="100px"></asp:Label>
                                                        <asp:TextBox ID="txtTot12" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtRH12" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtMC12" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtST12" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="center" id="tdObrasPrivada12" runat="server">
                                                        <asp:TextBox ID="txtObrasPrivada12" runat="server" MaxLength="14" Style="width: 80%; text-align: right; padding: 1px; font-size: 0.7rem;">0,00</asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalExecPrivada12" runat="server" Width="90" Style="font-size: 0.7rem;" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="info" style="height: 22px;" class="info">
                                                    <td align="center" class="info"><b>Total</b>
                                                    </td>
                                                    <td align="center">
                                                        <b>
                                                            <asp:Label ID="lblTotal" runat="server" Text="0,00"></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td align="center">
                                                        <span id="lblTotRH">&nbsp;</span><asp:Label ID="lblTotalRecursosHumanos" runat="server"
                                                            Text="0,00"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <span id="lblTotMC">&nbsp;<b><asp:Label ID="lblTotalMateriaisConsumo" runat="server"
                                                            Text="0,00"></asp:Label>
                                                        </b></span>
                                                    </td>
                                                    <td align="center">
                                                        <span id="lblTotST">&nbsp;<b><asp:Label ID="lbltotalServicos" runat="server" Text="0,00"></asp:Label>
                                                        </b></span>
                                                    </td>
                                                    <td align="center" id="tdTotalObrasUnidadePrivada" runat="server">
                                                        <span id="lblTotObr">&nbsp;<b><asp:Label ID="lblTotalObrasPrivada" runat="server" Text="0,00"></asp:Label>
                                                        </b></span>
                                                    </td>
                                                    <td align="center">
                                                        <span id="lblTotEP">&nbsp;<b><asp:Label ID="lblTotalExecPrivada" runat="server" Text="0,00"></asp:Label>
                                                        </b></span>
                                                    </td>
                                                </tr>
                                                <tr class="info" style="height: 22px;">
                                                    <td colspan="6" id="tdValorCofinanciamentoPrivada" runat="server" style="text-align: right !important;">
                                                        <%--Valor Total do Cofinanciamento:--%><b><asp:Label ID="lblValorCofinanciamentoPrivadoText" runat="server"></b></asp:Label>
                                                        <%--Bruno V.--%>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:Label ID="lblTotalCofinanciamento" runat="server" Width="100px"></asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" id="tdLimparRedePrivada" runat="server" colspan="6">
                                                        <br />
                                                        <asp:Button ID="btnLimparRedePrivada" runat="server" Text="Limpar" Width="89px" OnClick="btnLimparRedePrivada_Click"
                                                            Height="25px"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                       
                                         <div class="cell" style="width: 5%!important;" runat="server" id="trTotalIndireta">
                                            <table border="0" cellpadding="0" cellspacing="0" class="table border bordered tbCronograma" style="font-size: 0.70rem;">
                                                <thead class="info">
                                                    <tr>
                                                        <th runat="server" id="Th1" runat="server">
                                                            <span class="ui-jqgrid-title">Totais de cada parcela<br />
                                                                (Rede Direta +<br />
                                                                Rede Indireta)</span>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tr style="height: 26px;" id="trTotalReprogramacaoGeral" runat="server">
                                                    <td align="center" class="ui-state-sub ui-th-column ui-th-rtl">
                                                        <asp:Label ID="lblTotalReprogramacao" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 55px;" runat="server" id="trExercicioAtualGeral">
                                                    <td align="center" width="30"></td>
                                                </tr>
                                                <tr style="height: 26px;" id="trTotalGeralDemandasParlamentares" runat="server">
                                                    <td align="center" class="ui-state-sub ui-th-column ui-th-rtl">
                                                        <asp:Label ID="lblTotalDemandas" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr style="height: 50px;" runat="server" id="tr2">
                                                    <td align="center" width="30"></td>
                                                </tr>

                                                <tr style="height: 26px;" id="trTotalGeralReprogramacaoDemandasParlamentares" runat="server">
                                                    <td align="center" class="ui-state-sub ui-th-column ui-th-rtl">
                                                        <asp:Label ID="lblTotalReprogramacaoDemandas" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>                                                
                                                <tr style="height: 80px;" runat="server" id="tr3">
                                                    <td align="center" width="30"></td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede1" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede2" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede3" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede4" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 55px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede5" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede6" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede7" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede8" runat="server" Width="100px" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 55px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede9" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede10" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede11" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 50px;">
                                                    <td align="right">
                                                        <asp:Label ID="lblTotalRede12" runat="server" Text="0,00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="ui-jqgrid-labels" style="height: 30px;">
                                                    <td align="center">

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 35px;"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" align="center">
                                    <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                        OnClick="btnSalvar_Click" Height="25px"></asp:Button>

                                    &nbsp;<asp:Button ID="btnCalcular" runat="server" SkinID="button-calc" Text="Calcular"
                                        Width="89px" OnClick="btnCalcular_Click" Height="25px" />
                                </div>
                                <div class="row">
                                    <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                        width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                        <tr>
                                            <td style="padding: 15px 10px 2px 15px">
                                                <span class="mif-warning mif-2x"></span>
                                                <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px 12px 45px;">
                                                <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>




                    <div class="frame" id="frmCofinanciamento">
                        <div class="heading">
                            <asp:Label ID="lblNumeracao2" runat="server" /></b> <b>-
                                <asp:Label ID="lblTitulo" runat="server" /></b>
                            <a href="#" runat="server" id="A1" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                           <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="cronograma de desembolso" style="min-height:205px;">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:HiddenField ID="hdnSomaPrevisaoOrcamentaria" runat="server" />
                                            <asp:HiddenField ID="hdnSomaRecursoReprogramadoAnoAnterior" runat="server" />
                                            <asp:HiddenField ID="hdnSomaDemandasParlamentares" runat="server" />
                                            <asp:HiddenField ID="hdnSomaReprogramadoDemandasParlamentares" runat="server" />
                                            <asp:HiddenField ID="hdnTotalCofinanciamentoEstadual" runat="server" />
                                            <asp:HiddenField ID="hdnSomaNumeroAtendidos" runat="server" />

                                            <asp:ListView ID="lstRecursos" runat="server" OnItemCreated="lstRecursos_ItemCreated"
                                                Visible="false" OnItemDataBound="lstRecursos_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="80">Tipo de<br />
                                                                    Rede
                                                                </th>
                                                                <th width="220">Unidade
                                                                </th>
                                                                <th width="280">Tipo de Serviço
                                                                </th>
                                                                <th width="180">Usuários
                                                                </th>
                                                                <th width="150">Capacidade mensal de<br />
                                                                    pessoas/famílias<br />
                                                                    atendidas
                                                                </th>
                                                                <th width="150">Cofinanciamento estadual<br />
                                                                    no exercício atual
                                                                </th>
                                                                <th width="180">Recursos reprogramados<br />
                                                                    ano anterior
                                                                </th>
                                                                <th width="80">
                                                                    Demandas <br />Parlamentares
                                                                </th>
                                                                <th width="80">
                                                                    Recursos reprogramados Demandas <br />Parlamentares
                                                                </th>
                                                                <th width="100">Total<br />
                                                                </th>
                                                                <th width="70">Status<br />
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%--<tr class="jqgfirstrow" style="height: auto;">
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                            </tr>--%>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate >
                                                    <tr id="Tr1" style="height: 22px;" runat="server" visible='<%# Eval("Id").ToString() != "0" ? true : false %>'>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                                                        </td>
                                                        <td>
                                                            <%#DataBinder.Eval(Container.DataItem, "Unidade") %>
                                                        </td>
                                                        <td>
                                                            <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Usuario") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidos")) %>
                                                        </td>
                                                        <td align="right">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                                        </td>
                                                        <td id="Td3" align="right" runat="server">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursoReprogramadoAnoAnterior")).ToString("N2") %>
                                                        </td>
                                                        <td align="right">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorEstadualDemandasParlamentares")).ToString("N2") %>
                                                        </td>
                                                        <td align="right">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorEstadualReprogramacaoDemandasParlamentares")).ToString("N2") %>
                                                        </td>
                                                        <td id="Td11" align="right" runat="server">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "TotalRecursos")).ToString("N2") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Boolean)DataBinder.Eval(Container.DataItem, "Desativado")) != true ? "Ativo" : "Inativo" %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">
                                                            <asp:Label runat="server" ID="lblEmpty" Text="Não existe registro de cofinanciamento estadual."></asp:Label></b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            <table>
                                                <tr>
                                                    <td colspan=""></td>
                                                </tr>
                                            </table>
                                            <asp:ListView ID="lstProgramasBeneficios" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"  cellpadding="0" border="0">
                                                        <thead class="info" runat="server">
                                                            <tr>
                                                                <th width="180">Tipo de Rede
                                                                </th>
                                                                <th width="900">Programa / Benefício
                                                                </th>
                                                                <th width="160">Valor do<br />
                                                                cofinanciamento estadual
                                                                <th width="160">
                                                                    Reprogramação do<br />
                                                                    exercício anterior
                                                                </th>
                                                                <th width="80" id="thDemandas" runat="server" >Demandas
                                                                    <br />
                                                                    Parlamentares
                                                                </th>
                                                                <th width="80">
                                                                    Recursos reprogramados Demandas <br />Parlamentares
                                                                </th>
                                                                </th>
                                                                <th >
                                                                    Total
                                                                </th>
                                                            </tr>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                                                        </td>
                                                        <td>
                                                            <%#DataBinder.Eval(Container.DataItem, "Unidade") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursoReprogramadoAnoAnterior")).ToString("N2") %>
                                                        </td>
                                                       <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorEstadualDemandasParlamentares")).ToString("N2") %>
                                                        </td>
                                                        <td align="right">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorEstadualReprogramacaoDemandasParlamentares")).ToString("N2") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "TotalRecursos")).ToString("N2") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">
                                                            <asp:Label runat="server" ID="lblEmpty" Text="Não existe registro de cofinanciamento estadual."></asp:Label></b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            
                                            <asp:ListView ID="lstProgramasProjetos" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"  cellpadding="0" border="0">
                                                        <thead id="Thead1" class="info" runat="server">
                                                            <tr>
                                                                <th width="180">Tipo de Rede
                                                                </th>
                                                                <th width="900">Programa / Benefício
                                                                </th>
                                                                <th width="160">Valor do<br />
                                                                cofinanciamento estadual
                                                                </th>
                                                                <th width="160">
                                                                    Reprogramação do<br />
                                                                    exercício anterior
                                                                </th>
                                                                <th >
                                                                    Total
                                                                </th>
                                                            </tr>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                                                        </td>
                                                        <td>
                                                            <%#DataBinder.Eval(Container.DataItem, "Unidade") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursoReprogramadoAnoAnterior")).ToString("N2") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#((Decimal)DataBinder.Eval(Container.DataItem, "TotalRecursos")).ToString("N2") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">
                                                            <asp:Label runat="server" ID="lblEmpty" Text="Não existe registro de cofinanciamento estadual."></asp:Label></b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>


                                            <table class="table border bordered" cellspacing="2" cellpadding="0"
                                                width="1200" align="center" border="0" runat="server" id="tblTotalCofinanciamentoEstadual">
                                                <tr class="ui-jqgrid-labels" style="height: 22px;">
                                                    <td  id="tdTotalGeralCofinanciamentos" runat="server" class="ui-state-default ui-th-column ui-th-ltr ui-jqgrid-labels" align="right" colspan="1" >Totais :&nbsp;
                                                    </td>
                                                    <td runat="server" align="right" id="tdCofinanciamentoEstadual">                                                       
                                                        <asp:Label ID="lblTotalCofinanciamentoEstadual" runat="server"></asp:Label>
                                                    </td>
                                                    <td runat="server" align="right" id="tdCofinanciamentoReprogramado">
                                                        <asp:Label ID="lblCofinanciamentoReprogramado" runat="server"></asp:Label>
                                                    </td>
                                                    <td  runat="server" align="right" id="tdCofinanciamentoDemandas">
                                                        <asp:Label runat="server" ID="lblCofinanciamentoDemandas"></asp:Label>
                                                    </td>
                                                    <td runat="server" align="right" id="tdCofinanciamentoReprogramadoDemandas">
                                                        <asp:Label runat="server" ID="lblCofinanciamentoReprogramadoDemandas"></asp:Label>
                                                    </td>
                                                    <td runat="server" align="right" id="tdTotalCofinanciamento"> 
                                                        <asp:Label ID="lblTotalCofinanciamentos" runat="server"></asp:Label>
                                                    </td>
                                                    <td runat="server" align="right" id="tdStatus"> 
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdfAno" runat="server" Value="" />
            </form>



            <table width="1280" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a id="aAnterior" runat="server" >
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a id="aProximo" runat="server">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
