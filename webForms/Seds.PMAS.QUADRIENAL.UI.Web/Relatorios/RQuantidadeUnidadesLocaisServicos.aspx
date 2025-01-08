<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RQuantidadeUnidadesLocaisServicos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RQuantidadeUnidadesLocaisServicos" %>

<%@ Import Namespace="Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios" %>

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
                            rowspan="3" width="30">Seq.
                        </th>
                        <th align="center"
                            rowspan="3" width="150">Município
                        </th>
                        <th align="center"
                            rowspan="3" width="150">DRADS
                        </th>
                        <th align="center"
                            colspan="9" style="height: 22px;">Unidades p&#250;blicas                            
                        </th>
                        <th align="center"
                            colspan="3">Organiza&#231;&#245;es da Sociedade Civil
                        </th>
                        <th align="center"
                            colspan="3">Totais
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            style="height: 22px;" rowspan="2">N&#186; de unidades p&#250;blicas
                        </th>
                        <th align="center"
                            colspan="2">CRAS
                        </th>
                        <th align="center"
                            colspan="2">CREAS
                        </th>
                        <th align="center"
                            colspan="2">Centro POP
                        </th>
                        <th align="center"
                            colspan="2">Outros locais de execu&#231;&#227;o p&#250;blicos
                        </th>
                        <th align="center"
                            style="height: 22px;" rowspan="2">N&#186; de organiza&#231;&#245;es
                        </th>
                        <th align="center"
                            style="height: 22px;" rowspan="2">N&#186; de locais<br />
                            de execução
                        </th>
                        <th align="center"
                            style="height: 22px;" rowspan="2">N&#186; de servi&#231;os
                        </th>
                        <th align="center"
                            style="height: 22px;" rowspan="2"> N&#186; de Unidades/ organiza&#231;&#245;es
                        </th>
                        <th align="center"
                            style="height: 22px;" rowspan="2">N&#186; de locais<br />
                            de execução
                        </th>
                        <th align="center"
                            style="height: 22px;" rowspan="2">N&#186; de servi&#231;os
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            style="height: 22px;">N&#186; de CRAS
                        </th>
                        <th align="center"
                            style="height: 22px;">N&#186; de servi&#231;os
                        </th>
                        <th align="center"
                            style="height: 22px;">N&#186; de CREAS
                        </th>
                        <th align="center"
                            style="height: 22px;">N&#186; de servi&#231;os
                        </th>
                        <th align="center"
                            style="height: 22px;">N&#186; de Centros POP
                        </th>
                        <th width="70" align="center"
                            style="height: 22px;">N&#186; de servi&#231;os
                        </th>
                        <th align="center"
                            style="height: 22px;">N&#186; de locais<br />
                            de execução
                        </th>
                        <th align="center"
                            style="height: 22px;">N&#186; de serviços
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="3">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalUnidadesPublicas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCRAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicosCRAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCREAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicosCREAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCentroPOP" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicosCentroPOP" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalLocaisPublicos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicosPublicos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalUnidadesPrivadas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalLocaisPrivados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicosPrivados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalUnidades" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalLocais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicos" runat="server" Font-Bold="true" />
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
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalUnidadesPublicas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCRAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosCRAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCREAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosCREAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCentroPOP"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosCentroPOP"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalLocaisPublicos"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosPublicos"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalUnidadesPrivadas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalLocaisPrivados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosPrivados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalUnidadesPublicas + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalUnidadesPrivadas)%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalLocaisPublicos + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalLocaisPrivados  + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalCRAS  + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalCREAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalCentroPOP)%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosPublicos + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosPrivados + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosCRAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosCREAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosCentroPOP)%>
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
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalUnidadesPublicas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCRAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosCRAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCREAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosCREAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCentroPOP"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosCentroPOP"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalLocaisPublicos"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosPublicos"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalUnidadesPrivadas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalLocaisPrivados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosPrivados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalUnidadesPublicas + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalUnidadesPrivadas)%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalLocaisPublicos + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalLocaisPrivados + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalCRAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalCREAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalCentroPOP)%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosPublicos + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosPrivados + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosCRAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosCREAS + (Container.DataItem as QuantidadesServicosLocaisExecucaoInfo).TotalServicosCentroPOP)%>
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
