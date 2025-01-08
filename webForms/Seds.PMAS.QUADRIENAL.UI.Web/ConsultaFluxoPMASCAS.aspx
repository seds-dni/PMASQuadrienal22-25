<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ConsultaFluxoPMASCAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.ConsultaFluxoPMASCAS" %>

<%@ Import Namespace="Seds.PMAS.QUADRIENAL.Entidades.Estruturas" %>
<%@ Import Namespace="System.Linq" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form action="ConsultaFluxoPMASCAS.aspx">
        <div class="accordion">
            <div class="frame active">
                <div class="heading">
                    <b>Consultar/Fluxo PMAS</b>
                    <span class="mif-flow-cascade icon" style="margin-right: 5px;"></span>
                </div>
                <div class="content">

                    <div class="formInput" data-text="Consultar PMAS" style="min-height:150px;">

                        <div class="grid">

                            <div class="row" style="width: 850px; margin: 0px auto; margin-bottom: 10px;">
                                <div style="border-bottom: 1px solid #95bccf; padding: 6px; text-align: center; background-color: #fff; border-radius: 10px; width: 200px; height: 40px; float: left; margin-left: 2px;">
                                    <p>
                                        <asp:RadioButton ID="rdbPMAS" GroupName="g1" runat="server" onclick="seds.inicio.ConsultarPMAS.displayBuscador()" />
                                        <span style="color: rgb(27, 161, 226); margin-right: 5px;">PMAS</span>
                                    </p>
                                </div>

                                <div style="border-bottom: 1px solid #95bccf; padding: 6px; text-align: center; background-color: #fff; border-radius: 10px; width: 350px; height: 40px; float: left; margin-left: 10px;">
                                    <p>
                                        <asp:RadioButton ID="rdbEFLO" GroupName="g1" runat="server" onclick="seds.inicio.ConsultarPMAS.hideBuscador()" />
                                        <span style="color: rgb(27, 161, 226); margin-right: 5px;">Execucao Financeira / Lei Orçamentaria</span>
                                    </p>
                                </div>

                                <div style="border-bottom: 1px solid #95bccf; padding: 6px; text-align: center; background-color: #fff; border-radius: 10px; width: 200px; height: 40px; float: left;margin-left: 10px; ">
                                    <p>
                                        <asp:RadioButton ID="rdbPrestacaoDeContas" GroupName="g1" runat="server" onclick="seds.inicio.ConsultarPMAS.hideBuscador()" />
                                        <span style="color: rgb(27, 161, 226); margin-right: 5px;">Prestação de Contas </span>
                                    </p>
                                </div>
                            </div>

                            <div id="pnlBuscador" style="display: none;">
                                <asp:UpdatePanel ID="updatepnl" runat="server">
                                    <ContentTemplate>
                                        <div class="row cells3">
                                            <div class="cell">
                                                <div style="width: 300px; border-bottom: 1px solid #95bccf; background-color: #fff; border-radius: 10px; padding: 10px; text-align: center; margin-left: 10px">
                                                    <span style="color: rgb(27, 161, 226); margin-right: 5px;">DRADS:</span>
                                                    <div class="input-control select">
                                                        <asp:DropDownList runat="server" ID="ddlDrads" AutoPostBack="true" OnSelectedIndexChanged="ddlDrads_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="cell">
                                                <div style="width: 300px; border-bottom: 1px solid #95bccf; background-color: #fff; border-radius: 10px; padding: 10px; text-align: center; margin-left: 10px">
                                                    <span style="color: rgb(27, 161, 226); margin-right: 5px;">Município:</span>
                                                    <div class="input-control select">
                                                        <asp:DropDownList runat="server" ID="ddlMunicipio">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="cell" runat="server" id="tdSituacao">
                                                <div style="width: 350px; border-bottom: 1px solid #95bccf; background-color: #fff; border-radius: 10px; padding: 10px; text-align: center; margin-left: 10px">
                                                    <span style="color: rgb(27, 161, 226); margin-right: 5px;">Situação do Plano:</span>
                                                    <div class="input-control select" runat="server" id="tdLstSituacao">
                                                        <asp:DropDownList runat="server" ID="ddlSituacao" Width="180px">
                                                            <asp:ListItem Selected="True" Value="0">[Selecione uma Opção]</asp:ListItem>
                                                            <asp:ListItem Value="1">Desbloqueado</asp:ListItem>
                                                            <asp:ListItem Value="2">Em Análise Drads</asp:ListItem>
                                                            <asp:ListItem Value="3">Devolvido Drads</asp:ListItem>
                                                            <asp:ListItem Value="4">Para finalização</asp:ListItem>
                                                            <asp:ListItem Value="5">Em Análise do CMAS</asp:ListItem>
                                                            <asp:ListItem Value="6">Devolvido pelo CMAS</asp:ListItem>
                                                            <asp:ListItem Value="7">Rejeitado</asp:ListItem>
                                                            <asp:ListItem Value="8">Aprovado</asp:ListItem>
                                                            <asp:ListItem Value="9">Autoriza desbloqueio Gestor</asp:ListItem>
                                                            <asp:ListItem Value="10">Autoriza desbloqueio CMAS</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                        <div class="row align-center">
                                            <div class="cell">
                                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <asp:ListView ID="lstPMAS" runat="server" OnItemDataBound="lstPMAS_ItemDataBound"
                                                OnItemCommand="lstPMAS_ItemCommand" DataKeyNames="IdPrefeitura,IdMunicipio,Situacao, ReprogramarValores">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="12" style="height: 20px;">
                                                                    <span class="mif-file-text" style="margin-right: 5px;"></span>Planos Municipais
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="20" style="height: 22px;"></th>
                                                                <th width="100">Visualizar Plano
                                                                </th>
                                                                <th width="170">Drads
                                                                </th>
                                                                <th width="170">Município
                                                                </th>

                                                                <asp:Panel runat="server" ID="pnlPerfilADMHeader">
                                                                    <th width="100">Desbloqueio<br />
                                                                        Órgão Gestor
                                                                    </th>
                                                                    <th width="100">Desbloqueio<br />
                                                                        CMAS
                                                                    </th>
                                                                    <th width="100">Desbloquear para<br />
                                                                        reprogramação
                                                                    </th>
                                                                    <th width="100">Desbloquear para<br />
                                                                        Demandas
                                                                    </th>
                                                                </asp:Panel>

                                                                <th width="150">Situação PMAS
                                                                </th>
                                                                <th width="80">Histórico
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="info" style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisMunicipio" ToolTip="Visualizar Plano"
                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar_Municipio" />
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                                                        </td>
                                                        <asp:Panel runat="server" ID="pnlPerfilADMItemTemplate">
                                                            <td class="align-center">
                                                                <asp:ImageButton runat="server" ID="lkbDesbloqueioOrgaoGestor" ToolTip="Autorizar desbloqueio para Órgão Gestor"
                                                                    ImageUrl="~/Styles/Icones/desbloqueioOG.png" CommandName="Autorizar_Desbloqueio_OrgaoGestor" />
                                                            </td>
                                                            <td class="align-center">
                                                                <asp:ImageButton runat="server" ID="lkbDesbloqueioCMAS" ToolTip="Autorizar desbloqueio para CMAS"
                                                                    ImageUrl="~/Styles/Icones/desbloqueioCMAS.png" CommandName="Autorizar_Desbloqueio_CMAS" />
                                                            </td>
                                                            <td class="align-center">
                                                                <asp:ImageButton runat="server" ID="lkdDesbloqueioReprogramacao" ToolTip="Autorizar desbloqueio para reprogramação"
                                                                    ImageUrl="~/Styles/Icones/desbloqueioCMAS.png" CommandName="Autorizar_Desbloqueio_Reprogramacao" />
                                                            </td>
                                                            <td class="align-center">
                                                                <asp:ImageButton runat="server" ID="lkdDesbloqueioDemandas" ToolTip="Autorizar desbloqueio para demandas"
                                                                    ImageUrl="~/Styles/Icones/desbloqueioCMAS.png" CommandName="Autorizar_Desbloqueio_Demandas" />
                                                            </td>

                                                        </asp:Panel>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Situacao.Nome") %>
                                                        </td>

                                                        <td class="align-center">
                                                            <asp:ImageButton runat="server" ID="btnVisHistorico" ToolTip="Visualizar Histórico"
                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar_Historico" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                        <div class="row">
                                            <asp:ListView ID="lstPMAS2" runat="server" OnItemDataBound="lstExecucaoLeiOrcamentaria_ItemDataBound"
                                                OnItemCommand="lstPMAS_ItemCommand" DataKeyNames="IdPrefeitura,IdMunicipio,Situacao, ReprogramarValores">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="12" style="height: 20px;">
                                                                    <span class="mif-file-text" style="margin-right: 5px;"></span>Execução Financeira / Lei orçamentária
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="20" style="height: 22px;">
                                                                </th>
                                                                <th rowspan="2" width="140">Drads
                                                                </th>
                                                                <th rowspan="2" width="140">Município
                                                                </th>
                                                                <th width="150">
                                                                    Execução Financeira
                                                                </th>
                                                                <th width="150">Lei Orçamentária
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="20" style="height: 22px;">
                                                                </th>
                                                                <th width="150">
                                                                    <div style="width:25%; float:left; position:relative;">2021</div>
                                                                    <div style="width:25%; float:left; position:relative;">2022</div>
                                                                    <div style="width:25%; float:left; position:relative;">2023</div>
                                                                    <div style="width:25%; float:left; position:relative;">2024</div>
                                                                </th>
                                                                <th width="150">
                                                                    <div style="width:25%; float:left; position:relative;">2022</div>
                                                                    <div style="width:25%; float:left; position:relative;">2023</div>
                                                                    <div style="width:25%; float:left; position:relative;">2024</div>
                                                                    <div style="width:25%; float:left; position:relative;">2025</div>
                                                                </th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="info" style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                                                        </td>
                                                        <%-----------%>
                                                        <%--EF--%>
                                                        <%-----------%>

                                                        <td class="align-center">
                                                            <%--Linha 1 EF--%>
                                                            <asp:Repeater ID="rptExecucaoFinanceira" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "QuadrosExecucaoFinanceiraInner") %>'>
                                                                <HeaderTemplate>
                                                                    <table class="table striped border bordered" cellspacing="0"
                                                                        cellpadding="0" border="0">
                                                                        <thead class="info">
                                                                            <tr>
                                                                                <%--<td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                </td>
                                                                                 <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                </td>
                                                                                <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                </td>
                                                                                <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                </td>--%>
                                                                            </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                        </div>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:Repeater>

                                                            <%--Linha 2 EF--%>
                                                            <asp:Repeater ID="Repeater2" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "QuadrosExecucaoFinanceiraInner") %>'>
                                                                <HeaderTemplate>
                                                                    
                                                                        <div style="width: 100%;">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="width:25%; position:relative; float:left; text-align:center;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "SituacaoQuadroExecucaoFinanceira") %>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    
                                                                        </div>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>

                                                        <%-----------%>
                                                        <%--LO--%>
                                                        <%-----------%>

                                                        <td class="align-center">
                                                            <%--Linha 1 LO--%>
                                                            <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "QuadrosLeiOrcamentariaInner") %>'>
                                                                <HeaderTemplate>
                                                                    <table class="table striped border bordered" cellspacing="0"
                                                                        cellpadding="0" border="0">
                                                                        <thead class="info">
                                                                            <tr>
                                                                               <%-- <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2018
                                                                                </td>
                                                                                 <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2019
                                                                                </td>
                                                                                <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2020
                                                                                </td>
                                                                                <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2021
                                                                                </td>--%>
                                                                            </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                     
                                                                </FooterTemplate>
                                                            </asp:Repeater>

                                                            <%--Linha 2--%>
                                                            <asp:Repeater ID="Repeater3" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "QuadrosLeiOrcamentariaInner") %>'>
                                                                <HeaderTemplate>
                                                                        <div style="width: 100%;">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="width:25%; position:relative; float:left; text-align:center;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "SituacaoQuadroLeiOrcamentaria") %>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    
                                                                        </div>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                        <div class="row">
                                            <asp:ListView ID="lstPMAS3" runat="server" OnItemDataBound="lstExecucaoLeiOrcamentaria_ItemDataBound"
                                                OnItemCommand="lstPMAS_ItemCommand" DataKeyNames="IdPrefeitura,IdMunicipio,Situacao, ReprogramarValores">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="18" style="height: 20px;">
                                                                    <span class="mif-file-text" style="margin-right: 5px;"></span>Prestação de Contas
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="20" style="height: 22px;">
                                                                </th>
                                                                <th rowspan="2" width="140">Drads
                                                                </th>
                                                                <th rowspan="2" width="140">Município
                                                                </th>
                                                                <th width="150">
                                                                    Prestação de Contas
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="20" style="height: 22px;">
                                                                </th>
                                                                <th width="150">
                                                                    <div style="width:25%; float:left; position:relative;">2021</div>
                                                                    <div style="width:25%; float:left; position:relative;">2022</div>
                                                                    <div style="width:25%; float:left; position:relative;">2023</div>
                                                                    <div style="width:25%; float:left; position:relative;">2024</div>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="info" style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                                                        </td>
                                                        <%-----------%>
                                                        <%--PRC--%>
                                                        <%-----------%>


                                                        <td class="align-center">
                                                            <%--Linha 1 PRC--%>
                                                            <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "QuadrosPrestacaoDeContasInner") %>'>
                                                                <HeaderTemplate>
                                                                    <table class="table striped border bordered" cellspacing="0"
                                                                        cellpadding="0" border="0">
                                                                        <thead class="info">
                                                                            <tr>
                                                                               <%-- <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2018
                                                                                </td>
                                                                                 <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2019
                                                                                </td>
                                                                                <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2020
                                                                                </td>
                                                                                <td style="font-size:12px; width:25%; position:relative; float:left; text-align:center;">
                                                                                    2021
                                                                                </td>--%>
                                                                            </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                     
                                                                </FooterTemplate>
                                                            </asp:Repeater>

                                                            <%--Linha 2--%>
                                                            <asp:Repeater ID="Repeater3" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "QuadrosPrestacaoDeContasInner") %>'>
                                                                <HeaderTemplate>
                                                                        <div style="width: 100%;">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div style="width:25%; position:relative; float:left; text-align:center;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "SituacaoQuadroPrestacaoDeContas") %>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    
                                                                        </div>
                                                                    </div>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDrads" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</asp:Content>
