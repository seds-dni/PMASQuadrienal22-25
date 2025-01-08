<%@ Page Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RPrestacaoDeContasBE.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RPrestacaoDeContasBE" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lstBeneficiosEventuais" runat="server" OnItemDataBound="lstBeneficiosEventuais_ItemDataBound">
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
                            width="180">Beneficios Eventuais <br /> 
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
                            width="100">Reprogramação Beneficios Eventuais <br /> 
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
                            width="100">Demandas Beneficios Eventuais<br /> 
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
                            width="100">Reprogramação das Demandas Beneficios Eventuais<br /> 
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
                            <asp:Label ID="lblTotalSomaProtecaoBeneficiosEventuais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBeneficiosEventuais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBeneficiosEventuais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBeneficiosEventuais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBeneficiosEventuais" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoBeneficiosEventuaisReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBeneficiosEventuaisReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBeneficiosEventuaisReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBeneficiosEventuaisReprogramacao" runat="server" Font-Bold="true" />
                        </td>
                        
                        
                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoBeneficiosEventuaisDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBeneficiosEventuaisDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBeneficiosEventuaisDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBeneficiosEventuaisDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBeneficiosEventuaisDemandas" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalSomaProtecaoBeneficiosEventuaisReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutadosBeneficiosEventuaisReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidosBeneficiosEventuaisReprogramacaoDemandas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagensExecucaoBeneficiosEventuaisReprogramacaoDemandas" runat="server" Font-Bold="true" />
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
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuais"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuaisReprogramacao"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuaisDemandas"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuaisReprogramacaoDemandas"))%>
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
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuais"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuais"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuaisReprogramacao"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuaisReprogramacao"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuaisDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuaisDemandas"))%>
                        </td>

                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SomaProtecaoBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresPassiveisReprogramacaoBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosBeneficiosEventuaisReprogramacaoDemandas"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagensExecucaoBeneficiosEventuaisReprogramacaoDemandas"))%>
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