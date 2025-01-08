<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RRHOrgaoGestor.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RRHOrgaoGestor" %>

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
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="120" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="250" rowspan="2">Denominação do órgão gestor
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Total de trabalhadores
                        </th>
                        <th align="center" colspan="4">Escolaridade
                        </th>
                        <th align="center" colspan="13">Área de formação
                        </th>
                        <th align="center" colspan="6">Tipo de vínculo
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px; font-size: 10px;">
                        <th align="center"
                            width="80">Sem escolarização
                        </th>
                        <th align="center"
                            width="80">Nível fundamental
                        </th>
                        <th align="center" width="80">Nível médio
                        </th>
                        <th align="center" width="80">Nível superior
                        </th>
                        <th align="center" width="80">Serviço Social
                        </th>
                        <th align="center" width="80">Psicologia
                        </th>
                        <th align="center" width="80">Pedagogia
                        </th>
                        <th align="center" width="80">Sociologia
                        </th>
                        <th align="center" width="80">Antropologia
                        </th>
                        <th align="center" width="80">Terapia Ocupacional
                        </th>
                        <th align="center" width="80">Direito
                        </th>
                        <th align="center" width="80">Administração
                        </th>
                        <th align="center" width="80">Contabilidade
                        </th>
                        <th align="center" width="80">Economia
                        </th>
                        <th align="center" width="80">Economia Doméstica
                        </th>
                        <th align="center" width="80">Outra
                        </th>
                    <%--    <th align="center" width="80">Pós-graduação
                        </th>--%>
                        <th align="center" width="80">Estatutários
                        </th>
                        <th align="center" width="120">Empregados públicos celetistas
                        </th>
                        <th align="center" width="80">Apenas comissionados
                        </th>
                        <th align="center" width="80">Outros vínculos trabalhistas
                        </th>
                        <th align="center" width="80">Estagiários
                        </th>
                        <th align="center" width="80">Voluntários
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="4">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosSemEscolaridade" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosNivelFundamental" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosNivelMedio" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosNivelSuperior" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosServicoSocial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosPsicologia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosPedagogia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosSociologia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosAntropologia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosTerapiaOcupacional" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosDireito" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosAdministracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosContabilidade" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosEconomia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionariosEconomiaDomestica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOutrasAreas" runat="server" Font-Bold="true" />
                        </td>
                 <%--       <td align="right">
                            <asp:Label ID="lblTotalFuncionariosPosGraduacao" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right">
                            <asp:Label ID="lblTotalEstatutarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCeletistas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalComissionados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblOutrosVinculos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstagiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalVoluntarios" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
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
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Denominacao") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionarios")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosSemEscolaridade")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosNivelFundamental")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosNivelMedio")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperior")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorServicoSocial")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorPsicologia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorPedagogia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorSociologia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorAntropologia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorTerapiaOcupacional"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorDireito"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorAdministracao"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorContabilidade"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorEconomia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorEconomiaDomestica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosOutrasAreas"))%>
                </td>
              <%--  <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosPosGraduacao"))%>
                </td>--%>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalEstatutarios"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCeletistas"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalComissionados"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalOutrosVinculos"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalEstagiarios"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalVoluntarios"))%>
                </td>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Denominacao") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionarios")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosSemEscolaridade")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosNivelFundamental")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosNivelMedio")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperior")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorServicoSocial")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorPsicologia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorPedagogia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorSociologia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorAntropologia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorTerapiaOcupacional"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorDireito"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorAdministracao"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorContabilidade"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorEconomia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorEconomiaDomestica"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosOutrasAreas"))%>
                </td>
              <%--  <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosPosGraduacao"))%>
                </td>--%>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalEstatutarios"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalCeletistas"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalComissionados"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalOutrosVinculos"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalEstagiarios"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalVoluntarios"))%>
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
