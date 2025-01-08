<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RServicosEstadualizados.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RServicosEstadualizados" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #a6c9e2;">
                        <th align="center" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="160" colspan="2">Códigos
                        </th>
                        <th align="center"
                            width="180" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="180" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Tipo de rede
                        </th>
                        <th align="center"
                            width="250" rowspan="2">Nome da organização/unidade
                        </th>
                        <th align="center"
                            width="250" rowspan="2">Local de execução dos serviços
                        </th>

                        <th align="center"
                            width="150" rowspan="2">Endereço
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Bairro
                        </th>
                        <th align="center" width="180" rowspan="2">Proteção social
                        </th>
                        <th align="center" width="180" rowspan="2">Tipo de serviço
                        </th>
                        <th align="center" width="180" rowspan="2">Usuários
                        </th>
                        <th colspan="2">Funcionamento do serviço
                        </th>
                        <th align="center" width="150" rowspan="2">Capacidade mensal de atendimento
                        </th>
                        <th align="center" width="200" colspan="4">Média mensal de atendimento
                        </th>
                        <th width="100" rowspan="2">Total de trabalhadores do serviço</th>
                        <th width="150" rowspan="2">Este serviço atende a alguma comunidade tradicional ou grupo específico?</th>
                        <th align="center" colspan="9">Origem dos recursos de cofinanciamento via fundos (valores anuais)
                        </th>
                        <th align="center" width="100" rowspan="2">Recursos próprios da organização
                        </th>
                        <th align="center" width="80" rowspan="2">Valor do convênio<br />
                            estadualizado
                        </th>
                        <th align="center" width="100" rowspan="2">Recursos de outras fontes
                        </th>
                        <th align="center" width="100" rowspan="2">Total de recursos
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #a6c9e2;">
                        <th align="center"
                            width="90">Organização/<br />
                            Unidade
                        </th>
                        <th width="100">Local de execução<br />
                            ou ID-SUAS</th>
                        <th width="100">Início</th>
                        <th width="100">Término</th>
                        <th width="50">2017</th>
                        <th width="50">2018</th>
                        <th width="50">2019</th>
                        <th width="50">2020</th>
                        <th align="center" width="80">FMAS (R$)
                        </th>
                        <th align="center" width="80">FMDCA (R$)
                        </th>
                        <th align="center" width="80">FMI (R$)
                        </th>
                        <th align="center" width="80">FEAS (R$)
                        </th>
                        <th align="center" width="80">FEDCA (R$)
                        </th>
                        <th align="center" width="80">FEI (R$)
                        </th>
                        <th align="center" width="80">FNAS (R$)
                        </th>
                        <th align="center" width="80">FNDCA (R$)
                        </th>
                        <th align="center" width="80">FNI (R$)
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="15">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2020" runat="server" Font-Bold="true" />
                        </td>
                           <td align="right">
                            <asp:Label ID="lblTotalTrabalhadores" runat="server" Font-Bold="true" />
                        </td>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right">
                            <asp:Label ID="lblTotalOutrasFontes" runat="server" Font-Bold="true" />
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
                <td class="info" style="height: 22px; background-color: #7cc8ff;" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>

                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Endereco") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroMensalAtendidos")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2017")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2018")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2019")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2020")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Endereco") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroMensalAtendidos")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2017")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2018")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2019")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2020")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                 <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
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
