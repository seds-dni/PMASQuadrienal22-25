<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RRHLocalExecucao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RRHLocalExecucao" %>

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
                    <tr class="info" style="background-color: #7cc8ff;height: 22px;">
                        <th align="center"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="160" colspan="2">Códigos
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="150" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="70" rowspan="2">Tipo de rede
                        </th>
                        <th align="center"
                            width="270" rowspan="2">Nome da organização/unidade pública
                        </th>
                        <th align="center"
                            width="270" rowspan="2">Local de execução dos serviços
                        </th>
                         <th align="center"
                            width="170" rowspan="2">Tipo de Serviço
                        </th>
                         <th align="center"
                            width="100" rowspan="2">Usuários
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Total de trabalhadores do serviço
                        </th>
                        <th align="center" colspan="4">Escolaridade
                        </th>
                        <th align="center" colspan="11">Área de formação dos trabalhadores de nível superior
                        </th>
                        <th align="center" colspan="4">Outros indicadores
                        </th>
                    </tr>
                    <tr class="info" style="height: 22px; background-color: #a6c9e2;font-size: 10px;">
                        <th align="center"
                            width="60">Unidade
                        </th>
                        <th align="center"
                            width="100">Local de execução ou ID-SUAS
                        </th>
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
                        <th align="center" width="80">Musicoterapia
                        </th>
                        <th align="center" width="80">Direito
                        </th>
                        <th align="center" width="80">Economia
                        </th>
                        <th align="center" width="80">Economia Doméstica
                        </th>
                        <th align="center" width="80">Outra
                        </th>
                        <th align="center" width="90">Trabalhadores exclusivos deste serviço
                        </th>
                        <th align="center" width="90">Trabalhadores compartilhados
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
                    <tr class="info" style="background-color: #7cc8ff;height: 22px;">
                        <td align="right" colspan="10" >
                            <b>Totais:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosSemEscolaridade" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosNivelFundamental" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosNivelMedio" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosNivelSuperior" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosServicoSocial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosPsicologia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosPedagogia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosSociologia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosAntropologia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosTerapiaOcupacional" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosMusicoterapia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosDireito" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosEconomia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosEconomiaDomestica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFuncionariosOutrasAreas" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right" >
                            <asp:Label ID="lblTotalTrabalhadoresExclusivo" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalTrabalhadoresCompartilhados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalEstagiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalVoluntarios" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuario") %>
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
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorMusicoterapia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorDireito"))%>
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
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalExclusivoServico"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalOutrosServicosAssistenciais"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalEstagiarios"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalVoluntarios"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff;height: 22px;">
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
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuario") %>
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
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorMusicoterapia"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalFuncionariosSuperiorDireito"))%>
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
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalExclusivoServico"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalOutrosServicosAssistenciais"))%>
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
