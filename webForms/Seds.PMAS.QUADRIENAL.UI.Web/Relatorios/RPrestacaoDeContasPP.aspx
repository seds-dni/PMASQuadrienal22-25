<%@ Page Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RPrestacaoDeContasPP.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RPrestacaoDeContasPP" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lstProgramasProjetos" runat="server" OnItemDataBound="lstProgramasProjetos_ItemDataBound">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30">Seq.
                        </th>
                        <th align="center"
                            width="150">Município
                        </th>
                        <th align="center"
                            width="140">DRADS
                        </th>
                        <th align="center"
                            width="180">Proteção Social Programas & Projetos <br /> 
                            (Disponibilizados + Aplicações Financeiras)
                        </th>
                        <th align="center"
                            width="120">Valores Executados
                        </th>
                        <th align="center"
                            width="120">Valores Passíveis de
                            <br/> Reprogramação
                        </th>
                        <th align="center"
                            width="120">Valores Devolvidos
                        </th>
                        <th align="center"
                            width="100">Porcentagem de Execução
                        </th>

                        <th align="center"
                            width="100">Reprogramação Proteção Programas & Projetos <br /> 
                            (Disponibilizados + Aplicações Financeiras)
                        </th>
                        <th align="center"
                            width="100">Valores Executados
                        </th>
                        <th align="center"
                            width="100">Valores Passíveis <br /> de Reprogramação
                        </th>
                        <th align="center"
                            width="100">Valores Devolvidos
                        </th>
                        <th align="center"
                            width="100">Porcentagem de <br />
                             execução
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
                            <asp:Label ID="lblTotalSomaProtecaoProgramasProjetos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosProgramasProjetos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoProgramasProjetos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosProgramasProjetos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoProgramasProjetos" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoProgramasProjetosReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosProgramasProjetosReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoProgramasProjetosReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosProgramasProjetosReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoProgramasProjetosReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        

                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:ListView ID="lstItems" runat="server">
                <LayoutTemplate>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="info" style="background-color: #7cc8ff; height: 22px;">
                            <asp:Label ID="lblSequencia" runat="server" />
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoProgramasProjetos"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoProgramasProjetosReprogramacao"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <tr>
                        <td class="info" style="height: 22px; background-color: #7cc8ff;">
                            <asp:Label ID="lblSequencia" runat="server" />
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosProgramasProjetos"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoProgramasProjetos"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosProgramasProjetosReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoProgramasProjetosReprogramacao"))%>
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
</asp:Content>