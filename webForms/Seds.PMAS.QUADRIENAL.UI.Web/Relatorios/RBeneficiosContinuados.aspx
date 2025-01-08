<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RBeneficiosContinuados.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RBeneficiosContinuados" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            style="background-color: #7cc8ff;" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="200" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="200" rowspan="2">DRADS
                        </th>
                        <%-- <th align="center"  colspan="12" style="height:22px;">
                            Programas federais
                        </th>  --%>

                        <th align="center" colspan="6">BPC - Idosos
                        </th>
                        <th align="center" colspan="6">BPC - PCD
                        </th>
                        <th align="center" width="150" rowspan="2">Previsão total
                            <br />
                            de repasse anual
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" width="150">Estimativa mensal
                            <br />
                            de beneficiários
                        </th>
                        <th align="center" width="150">Previsão anual<br />
                            de repasse
                        </th>
                        <th align="center" width="150" colspan="2">Parcerias
                        </th>
                        <th align="center" width="150" colspan="2">Integração com<br />
                            serviços
                        </th>
                        <th align="center" width="150">Estimativa mensal<br />
                            de beneficiários
                        </th>
                        <th align="center" width="150">Previsão anual
                            <br />
                            de repasse
                        </th>
                        <th align="center" width="150" colspan="2">Parcerias
                        </th>
                        <th align="center" width="150" colspan="2">Integração com<br />
                            serviços
                        </th>

                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px; background-color: #7cc8ff;">
                        <td align="right" colspan="3">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCIdososBeneficiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCIdososRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCIdososParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCIdososParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCIdososIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCIdososServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCPCDBeneficiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCPCDRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCPCDParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCPCDParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCPCDIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalBPCPCDServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>

                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BPCIdososBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "BPCIdososRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaBPCIdosos") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCIdososTotalParcerias") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoBPCIdosos") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCIdososTotalServicosAssociados") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BPCPCDBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "BPCPCDRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaBPCPCD") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCPCDTotalParcerias") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoBPCPCD") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCPCDTotalServicosAssociados") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "TotalRepasseBPC"))%>
                </td>

            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BPCIdososBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "BPCIdososRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaBPCIdosos") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCIdososTotalParcerias") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoBPCIdosos") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCIdososTotalServicosAssociados") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BPCPCDBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "BPCPCDRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaBPCPCD") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCPCDTotalParcerias") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoBPCPCD") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BPCPCDTotalServicosAssociados") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "TotalRepasseBPC"))%>
                </td>


            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as características
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
