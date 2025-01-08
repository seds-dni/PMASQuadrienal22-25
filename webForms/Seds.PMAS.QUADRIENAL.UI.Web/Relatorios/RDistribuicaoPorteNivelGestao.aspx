<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RDistribuicaoPorteNivelGestao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RDistribuicaoPorteNivelGestao" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info"  style="background-color: #7cc8ff;">
                        <th align="center"
                            rowspan="2" width="120">
                            Porte
                        </th>
                        <th align="center" colspan="4" style="height:22px;">
                            Nível de gestão
                        </th>
                        <th align="center" rowspan="2" width="80">
                            Totais
                        </th>
                        <th align="center" rowspan="2" width="100">
                            Porcentagens
                        </th>                        
                    </tr>
                    <tr class="info" style="background-color: #a6c9e2;height:22px;">
                        <th width="80" align="center">
                            Inicial
                        </th>
                        <th width="80" align="center">
                            Básica
                        </th>
                        <th width="80" align="center">
                            Plena
                        </th>
                        <th width="90" align="center">
                            Não habilitado
                        </th>                        
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info"  style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" >
                            <b>Totais:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalInicial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalPlena" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            <asp:Label ID="lblTotalNaoHabilitado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            100,00%
                        </td>                       
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" >
                            <b>Porcentagens:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblPorcentagemInicial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblPorcentagemBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblPorcentagemPlena" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            <asp:Label ID="lblPorcentagemNaoHabilitado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            100,00%
                        </td>                        
                        <td align="right" >
                            &nbsp;
                        </td>                                               
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info style="height: 22px;">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Inicial")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Basica")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Plena")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NaoHabilitado")) %>
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
