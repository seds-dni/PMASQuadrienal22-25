<%@ Page Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RPrestacaoDeContas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RPrestacaoDeContas" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lstBasica" runat="server" OnItemDataBound="lstBasica_ItemDataBound">
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
                            width="180">Proteção Social Básica <br /> 
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
                            width="100">Reprogramação Proteção Social Básica <br /> 
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


                        <th align="center"
                            width="100">Demandas Proteção Social Básica<br /> 
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

                        <th align="center"
                            width="100">Reprogramação das Demandas Proteção Social Básica<br /> 
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
                            <asp:Label ID="lblTotalSomaProtecaoBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBasica" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoBasicaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBasicaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBasicaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBasicaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBasicaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        
                        
                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoBasicaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBasicaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBasicaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBasicaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBasicaDemandas" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoBasicaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBasicaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBasicaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBasicaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBasicaReprogramacaoDemandas" runat="server" Font-Bold="true" />
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
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasica"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasicaReprogramacao"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasicaDemandas"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasicaReprogramacaoDemandas"))%>
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
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasica"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasica"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasicaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasicaReprogramacao"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasicaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasicaDemandas"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBasicaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBasicaReprogramacaoDemandas"))%>
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