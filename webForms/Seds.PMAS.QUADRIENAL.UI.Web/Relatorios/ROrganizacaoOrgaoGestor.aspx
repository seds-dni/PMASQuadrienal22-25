<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="ROrganizacaoOrgaoGestor.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.ROrganizacaoOrgaoGestor" %>

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
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table striped border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center" width="180" rowspan="2">Município
                        </th>
                        <th align="center" width="180" rowspan="2">Drads
                        </th>
                        <th align="center" width="180" rowspan="2">Estrutura do órgão gestor
                        </th>
                        <th align="center" width="120" rowspan="2">Nível de escolaridade do gestor
                        </th>
                        <th align="center" width="170" rowspan="2">Área de formação do gestor
                        </th>
                        <th align="center" width="100" rowspan="2">Quantidade total de trabalhadores
                        </th>
                        <th align="center" colspan="10">Possui equipe específica de:
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center" width="100">Proteção social básica
                        </th>
                        <th align="center" width="100">Proteção social especial
                        </th>
                        <th align="center" width="100">Vigilância socioassistencial
                        </th>
                        <th align="center" width="100">Gestão de Benefícios/<br />
                            Transferência de Renda
                        </th>
                        <th align="center" width="100">Gestão do Cadastro Único
                        </th>
                        <th align="center" width="100">Gestão Financeira e Orçamentária
                        </th>
                        <th align="center" width="100">Gestão do Trabalho no SUAS
                        </th>
                        <th align="center" width="100">Regulação do Suas
                        </th>
<%--                        <th align="center" width="100">Execução dos serviços socioassistenciais da rede direta
                        </th>--%>
                        <th align="center" width="100">Não pertencem a nenhuma equipe ou trabalham em diversas equipes concomitantemente
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="6">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFuncionarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeProtecaoBasica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeProtecaoEspecial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeVigilancia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeTransferenciaRenda" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeCadUnico" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeGestaoFinanceira" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEquipeGSUAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRegulacaoSUAS" runat="server" Font-Bold="true" />
                        </td>
<%--                        <td align="right">
                            <asp:Label ID="lblTotalRedeDireta" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right">
                            <asp:Label ID="lblTotalOutrasEquipes" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "Estrutura") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "EscolaridadeGestor") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "FormacaoGestor") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TotalFuncionarios")) - (Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeRedeDireta")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeProtecaoBasica"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeProtecaoEspecial")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeVigilanciaSocioassistencial")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeTransferenciaRenda"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeCadUnico")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeGestaoFinanceira"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeGestaoSUAS"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeRegulacaoSUAS")) %>
                </td>
<%--                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeRedeDireta")) >= 1 ? "Sim" : "Não"%>
                </td>--%>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiOutrasEquipes")) %>
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
                    <%#DataBinder.Eval(Container.DataItem, "Estrutura") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "EscolaridadeGestor") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "FormacaoGestor") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TotalFuncionarios")) - (Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeRedeDireta")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeProtecaoBasica"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeProtecaoEspecial")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeVigilanciaSocioassistencial"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeTransferenciaRenda")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeCadUnico"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeGestaoFinanceira"))%>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeGestaoSUAS")) %>
                </td>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeRegulacaoSUAS")) %>
                </td>
<%--                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiEquipeRedeDireta")) >= 1 ? "Sim" : "Não"%>
                </td>--%>
                <td align="center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "PossuiOutrasEquipes"))%>
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
