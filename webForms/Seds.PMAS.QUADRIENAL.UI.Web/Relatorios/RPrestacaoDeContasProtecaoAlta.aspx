<%@ Page Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RPrestacaoDeContasProtecaoAlta.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RPrestacaoDeContasProtecaoAlta" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lstAlta" runat="server" OnItemDataBound="lstAlta_ItemDataBound">
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
                            width="180">Proteção Social de Alta Complexidade<br /> 
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
                            width="100">Reprogramação Proteção Social de Alta Complexidade<br /> 
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
                            width="100">Demandas Proteção Social de Alta Complexidade<br /> 
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
                            width="100">Reprogramação das Demandas Proteção Social de Alta Complexidade<br /> 
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
                            <asp:Label ID="lblTotalSomaProtecaoAlta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosAlta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoAlta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosAlta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoAlta" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoAltaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosAltaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoAltaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosAltaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoAltaReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        
                        
                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoAltaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosAltaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoAltaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosAltaDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoAltaDemandas" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoAltaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosAltaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoAltaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosAltaReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoAltaReprogramacaoDemandas" runat="server" Font-Bold="true" />
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
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAlta"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAltaReprogramacao"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAltaDemandas"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAltaReprogramacaoDemandas"))%>
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
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAlta"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAlta"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAltaReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAltaReprogramacao"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAltaDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAltaDemandas"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosAltaReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoAltaReprogramacaoDemandas"))%>
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