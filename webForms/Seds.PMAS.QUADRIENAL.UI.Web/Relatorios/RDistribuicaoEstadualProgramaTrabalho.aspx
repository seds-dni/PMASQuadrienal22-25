<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RDistribuicaoEstadualProgramaTrabalho.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RDistribuicaoEstadualProgramaTrabalho" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center" 
                            width="30" rowspan="3">Seq.
                        </th>
                        <th align="center" 
                            width="250" rowspan="3">Município
                        </th>
                        <th align="center" 
                            width="200" rowspan="3">DRADS
                        </th>
                        <th align="center"  colspan="3" width="340">&nbsp;08.244.3517.5530.0000
                        </th>
                        <th align="center"  colspan="3" width="340">&nbsp;08.244.3517.6197.0000
                        </th>
                        <th align="center"  colspan="3" width="340">&nbsp;08.244.3517.6035.0000
                        </th>
                        <%--<th align="center"  colspan="4" width="340"></th>--%>
                        <th align="center"  colspan="3" width="340">&nbsp;08.244.3517.6179.0000
                        </th>
                        <th align="center"  colspan="3" width="340">&nbsp;08.244.3517.6179.0000
                        </th>
                        <th align="center"  width="340" rowspan="2" colspan="3">Total
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"  colspan="3" width="340">Proteção Social Básica
                        </th>
                        <th align="center"  colspan="3" width="340">Proteção Social Especial
                            <br />
                            de Média Complexidade
                        </th>
                        <th align="center"  colspan="3" width="340">Proteção Social Especial
                            <br />
                            de Alta Complexidade
                        </th>
                        <th align="center"  colspan="3" width="340">Programas e Projetos
                        </th>
                        <th align="center"  colspan="3" width="340">Benefícios Eventuais
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"  width="120">Exercício atual
                        </th>
                        <th align="center"  width="120">Reprogramado
                        </th>
                        <th align="center"  width="120">Subtotal
                        </th>
                      <%--  <th align="center"  width="100">Previsão anual<br />
                            do n° atendidos
                        </th>--%>
                        <th align="center"  width="120">Exercício atual
                        </th>
                        <th align="center"  width="120">Reprogramado
                        </th>
                        <th align="center"  width="120">Subtotal
                        </th>
                       <%-- <th align="center"  width="100">Previsão anual<br />
                            do n° atendidos
                        </th>--%>
                        <th align="center"  width="120">Exercício atual
                        </th>
                        <th align="center"  width="120">Reprogramado
                        </th>
                        <th align="center"  width="120">Subtotal
                        </th>

                       <%-- <th align="center"  width="100">Número de<br />
                            atendidos anual
                        </th>--%>

                        <th align="center"  width="120">Exercício atual
                        </th>
                        <th align="center"  width="120">Reprogramado
                        </th>
                        <th align="center"  width="120">Subtotal
                        </th>


                        <th align="center"  width="120">Exercício atual
                        </th>
                        <th align="center"  width="120">Reprogramado
                        </th>
                        <th align="center"  width="120">Subtotal
                        </th>
                      <%--  <th align="center"  width="80">Previsão anual<br />
                            do n° atendidos
                        </th>--%>
                        <th align="center"  width="120">Valor
                        </th>
                        <th align="center"  width="120">Valor<br />
                            Reprogramado
                        </th>
                        <th align="center"  width="120">Subtotal
                        </th>
                     <%--   <th align="center"  width="80">Previsão anual<br />
                            do n° atendidos
                        </th>--%>
                     <%--   <th align="center"  width="120">Valor
                        </th>
                        <th align="center"  width="120">Reprogramado
                        </th>
                        <th align="center"  width="120">Total
                        </th>
                        <th align="center"  width="80">Previsão anual<br />
                            do n° atendidos
                        </th>--%>
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
                            <asp:Label ID="lblTotalProtecaoSocialBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalProtecaoSocialBasicaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblValorTotalProtecaoSocialBasica" runat="server" Font-Bold="true" />
                        </td>
                       <%-- <td align="right" >
                            <asp:Label ID="lblTotalNumeroAtendidosAnualBasica" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalProtecaoSocialMedia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalProtecaoSocialMediaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblValorTotalProtecaoSocialMedia" runat="server" Font-Bold="true" />
                        </td>
                     <%--   <td align="right" >
                            <asp:Label ID="lblTotalNumeroAtendidosAnualMedia" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalProtecaoSocialAlta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalProtecaoSocialAltaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblValorTotalProtecaoSocialAlta" runat="server" Font-Bold="true" />
                        </td>
                       <%-- <td align="right" >
                            <asp:Label ID="lblTotalNumeroAtendidosAnualAlta" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalSPSolidario" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalSPSolidarioReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblValorTotalSPSolidario" runat="server" Font-Bold="true" />
                        </td>
                        <%--<td align="right" >
                            <asp:Label ID="lblTotalNumeroAtendidos" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalBeneficios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalBeneficiosReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblValorTotalBeneficios" runat="server" Font-Bold="true" />
                        </td>
                      <%--  <td align="right" >
                            <asp:Label ID="lblTotalNumeroAtendidosAnualBeneficios" runat="server" Font-Bold="true" />
                        </td>--%>
                           <td align="right" >
                            <asp:Label ID="lblTotalAtual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>
                      <%--  <td align="right" >
                            <asp:Label ID="lblTotalPrevisaoAnualAtendidos" runat="server" Font-Bold="true" />
                        </td>--%>
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
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialBasica"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialBasicaReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorProtecaoSocialBasica"))%>
                </td>
              <%--  <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualBasica"))%>
                </td>--%>

                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialMedia"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialMediaReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorProtecaoSocialMedia"))%>
                </td>
           <%--     <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualMedia"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialAlta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialAltaReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorProtecaoSocialAlta"))%>
                </td>
             <%--   <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualAlta"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorSPSolidario"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorSPSolidarioReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorSPSolidario"))%>
                </td>
              <%--    <td class="align-right">
                             <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualLiberdadeAssistida"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorBeneficiosEventuais"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorBeneficiosEventuaisReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorBeneficiosEventuais"))%>
                </td>
            <%--    <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualBeneficios"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalExercicioAtual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
              <%--  <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalPrevisaoAnualAtendidos"))%>
                </td>--%>
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
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialBasica"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialBasicaReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorProtecaoSocialBasica"))%>
                </td>
                <%--<td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualBasica"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialMedia"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialMediaReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorProtecaoSocialMedia"))%>
                </td>
               <%-- <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualMedia"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialAlta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorProtecaoSocialAltaReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorProtecaoSocialAlta"))%>
                </td>
             <%--   <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualAlta"))%>
                </td>--%>
               <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorSPSolidario"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorSPSolidario"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorSPSolidarioReprogramado"))%>
                </td>
                <%--<td class="align-right">
                    <%--           <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualLiberdadeAssistida"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorBeneficiosEventuais"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorBeneficiosEventuaisReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SubTotalValorBeneficiosEventuais"))%>
                </td>
             <%--   <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnualBeneficios"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalExercicioAtual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TotalReprogramado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
               <%-- <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalPrevisaoAnualAtendidos"))%>
                </td>--%>
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
