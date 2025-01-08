<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RDistribuicaoSituacaoVulnerabilidade.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RDistribuicaoSituacaoVulnerabilidade" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color:  #7cc8ff;">
                        <th align="center" 
                            rowspan="2" width="300">
                            Situação de vulnerabilidade ou risco
                        </th>
                        <th align="center" colspan="10"  style="height:22px;">
                            Níveis de gravidade da situação
                        </th>
                        <th align="center"  rowspan="2" width="80">
                            Total de municípios que apontaram esta situação
                        </th>
                        <th align="center"  rowspan="2" width="100">
                            Porcentagem
                        </th>                        
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;height:22px;">
                        <th width="30" align="center" >
                            1&#186;
                        </th>                        
                        <th width="30" align="center" >
                            2&#186;
                        </th>
                        <th width="30" align="center" >
                            3&#186;
                        </th>
                        <th width="30" align="center" >
                            4&#186;
                        </th>
                        <th width="30" align="center" >
                            5&#186;
                        </th>
                        <th width="30" align="center" >
                            6&#186;
                        </th>
                        <th width="30" align="center" >
                            7&#186;
                        </th>
                        <th width="30" align="center" >
                            8&#186;
                        </th>
                        <th width="30" align="center" >
                            9&#186;
                        </th>
                        <th width="30" align="center" >
                            10&#186;
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px; background-color: #7cc8ff;">
                        <td align="right"  >
                            <b>Totais:</b>
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade1" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade2" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade3" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade4" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade5" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade6" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade7" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade8" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade9" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotalGravidade10" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right"  >
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right"  >
                            100,00%
                        </td>                       
                    </tr>                    
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 22px;">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoVulnerabilidade") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade1")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade2")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade3")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade4")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade5")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade6")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade7")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade8")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade9")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Gravidade10")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Total")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:P2}",DataBinder.Eval(Container.DataItem, "Porcentagem")) %>
                </td>                
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as características
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
    <br />
</asp:Content>
