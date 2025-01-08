<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RProgramaProjeto.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RProgramaProjeto" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound" >
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                     <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="150" rowspan="2">DRADS
                        </th>
                        <th align="center" rowspan="2" width="340">Nome do programa/projeto
                        </th>
                        <th align="center" rowspan="2" width="450">Beneficiários
                        </th>
                         <th align="center" rowspan="2" width="130">Nível de abrangência
                        </th>
                        <th align="center" rowspan="2" width="120">N&#186; de beneficiários
                            <br />
                            atendidos
                        </th>
                       
                        <%--   <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="130">
                            Data de início
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="200" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="200" colspan="2">
                            Parcerias
                        </th>--%>
                        <th align="center" colspan="12" width="880">Recursos investidos no programa/projeto
                        </th>
                    </tr>
                    <tr style="background-color:#7cc8ff;">
                        <th align="center" width="80">FMAS
                        </th>
                        <th align="center" width="80">Orçamento<br />
                            municipal
                        </th>
                        <th align="center" width="80">Outros fundos<br />
                            municipais
                        </th>
                        <th align="center" width="80">FEAS
                        </th>
                        <th align="center"  width="80">Orçamento<br />
                            estadual
                        </th>
                        <th align="center" width="80">Outros fundos<br />
                            estaduais
                        </th>
                        <th align="center" width="80">FNAS
                        </th>
                        <th align="center"  width="80">Orçamento<br />
                            federal
                        </th>
                        <th align="center"  width="80">Outros fundos<br />
                            nacionais
                        </th>
                        <th align="center" width="80">IGD-PBF
                        </th>
                        <th align="center" width="80">IGD-SUAS
                        </th>
                        <th align="center"  width="80">Total de<br />
                            recursos
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="6">
                            <b>Totais:</b>
                        </td>
                          <td align="right">
                            <asp:Label ID="lblTotalBeneficiarioAtendidos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOrcamentoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFundoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOrcamentoEstadual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFundoEstadual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOrcamentoFederal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFundoFederal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIGDPBF" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIGDSUAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRecursos" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Beneficiarios") %>
                </td>
                 <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "NivelAbrangencia") %>
                </td>
                <td class="align-right">
                     <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MetaPactuada"))%>
                </td>
                <%--   <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "DataInicio") %>
                </td>
                <td class="align-right">
                    <%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) == 0 ? "Não" : "Sim"%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaFormal")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "QuantidadeParcerias"))%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorOrcamentoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorOrcamentoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorIGDPBF"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorIGDSUAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorTotalRecursos"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
               <td class="info" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Beneficiarios") %>
                </td>
                  <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "NivelAbrangencia") %>
                </td>
                <td class="align-right">
                     <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MetaPactuada"))%>
                </td>
                <%-- <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "DataInicio") %>
                </td>
                <td class="align-right">
                    <%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) == 0 ? "Não" : "Sim"%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaFormal")%>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "QuantidadeParcerias")%>
                </td>--%>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorOrcamentoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorOrcamentoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorIGDPBF"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorIGDSUAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorTotalRecursos"))%>
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
