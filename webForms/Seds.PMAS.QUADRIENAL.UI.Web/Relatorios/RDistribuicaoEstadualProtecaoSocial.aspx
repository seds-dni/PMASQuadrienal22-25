<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RDistribuicaoEstadualProtecaoSocial.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RDistribuicaoEstadualProtecaoSocial" %>

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
                            width="30" rowspan="2">
                            Seq.
                        </th>                                                
                        <th align="center" 
                            width="150" rowspan="2">
                            Município
                        </th>                        
                        <th align="center" 
                            width="150" rowspan="2">
                            DRADS
                        </th>                        
                        <th align="center" 
                            colspan="4" style="height:22px;">
                            Proteção Social Básica
                        </th>
                        <th align="center" 
                            colspan="3" style="height:22px;">
                            Proteção Social Especial de Média Complexidade
                        </th>
                        <th align="center" 
                            colspan="3" style="height:22px;">
                            Proteção Social Especial de Alta Complexidade
                        </th>
                        <th align="center" 
                            colspan="3" style="height:22px;">
                            Todas as proteções
                        </th>
                    </tr>    
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"  width="120">
                            Rede direta
                        </th>
                        <th align="center"  width="120">
                            Rede indireta
                        </th>
                   <%--     <th align="center"  width="120">
                            São Paulo Solidário
                        </th>--%>
                        <th align="center"  width="120">
                            Implantação de CRAS
                        </th>
                        <th align="center"  width="120">
                            Subtotal
                        </th>                        
                        <th align="center"  width="120">
                            Rede direta
                        </th>
                        <th align="center"  width="120">
                            Rede indireta
                        </th>
                        <th align="center"  width="120">
                            Subtotal
                        </th>                        
                        <th align="center"  width="120">
                            Rede direta
                        </th>
                        <th align="center"  width="120">
                            Rede indireta
                        </th>
                        <th align="center"  width="120">
                            Subtotal
                        </th>                        
                        <th align="center"  width="120">
                            Rede direta
                        </th>
                        <th align="center"  width="120">
                            Rede indireta
                        </th>
                        <th align="center"  width="120">
                            Total
                        </th>                        
                    </tr>                     
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="3" >
                            <b>Totais:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePublicaBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePrivadaBasica" runat="server" Font-Bold="true" />
                        </td>                        
                    <%--    <td align="right" >
                            <asp:Label ID="lblTotalSaoPauloSolidario" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalImplantacaoCRAS" runat="server" Font-Bold="true" />
                        </td>   
                        <td align="right" >
                            <asp:Label ID="lblTotalBasica" runat="server" Font-Bold="true" />
                        </td>  
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePublicaMedia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePrivadaMedia" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            <asp:Label ID="lblTotalMedia" runat="server" Font-Bold="true" />
                        </td> 
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePublicaAlta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePrivadaAlta" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            <asp:Label ID="lblTotalAlta" runat="server" Font-Bold="true" />
                        </td> 
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePublica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalRedePrivada" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>                       
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>                
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaBasica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaBasica"))%>
                </td>
          <%--      <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SaoPauloSolidario"))%>
                </td>--%>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ImplantacaoCRAS"))%>
                </td>
             <%--    <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "FamiliaPaulista"))%>
                </td>--%>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalBasica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEspecialMedia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEspecialMedia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalEspecialMedia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEspecialAlta"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEspecialAlta"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalEspecialAlta"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TotalRedePublica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TotalRedePrivada"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaBasica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaBasica"))%>
                </td>
             <%--   <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SaoPauloSolidario"))%>
                </td>--%>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ImplantacaoCRAS"))%>
                </td>
           <%--      <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "FamiliaPaulista"))%>
                </td>--%>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalBasica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEspecialMedia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEspecialMedia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalEspecialMedia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEspecialAlta"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEspecialAlta"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalEspecialAlta"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TotalRedePublica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TotalRedePrivada"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Total"))%>
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
