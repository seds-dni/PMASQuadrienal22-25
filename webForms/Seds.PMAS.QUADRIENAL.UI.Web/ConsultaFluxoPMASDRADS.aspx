<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaFluxoPMASDRADS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.ConsultaFluxoPMASDRADS" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form name="frm">
        <div class="accordion">
            <div class="frame active">
                <div class="heading">
                    <b>Consultar/Fluxo PMAS</b>
                    <span class="mif-flow-cascade icon"></span>
                </div>
                <div class="content">
                    <div id="pnlBuscador">
                        <asp:UpdatePanel ID="updatepnl" runat="server">
                            <ContentTemplate>
                                <div class="formInput" data-text="Consultar PMAS" style="min-height:150px;">
                                    <div class="row" style="width: 850px; margin: 0px auto; margin-bottom: 20px;">
                                        
                                        <div style="border-bottom: 1px solid #95bccf; padding: 6px; text-align: center; background-color: #fff; border-radius: 10px; width: 200px; height: 40px; float: left; margin-left: 2px;">
                                            <p>
                                                <asp:RadioButton ID="rdbPMAS" GroupName="g1" AutoPostBack="true" runat="server" OnCheckedChanged="rdbPMAS_CheckedChanged" />
                                                <span style="color: rgb(27, 161, 226); margin-right: 5px;">PMAS</span>
                                            </p>
                                        </div>

                                        <div style="border-bottom: 1px solid #95bccf; padding: 6px; text-align: center; background-color: #fff; border-radius: 10px; width: 350px; height: 40px; float: left; margin-left: 10px;">
                                            <p>
                                                <asp:RadioButton ID="rdbEFLO" GroupName="g1" AutoPostBack="true" runat="server" OnCheckedChanged="rdbPMAS_CheckedChanged" />
                                                <span style="color: rgb(27, 161, 226); margin-right: 5px;">Execucao Financeira / Lei Orçamentaria</span>
                                            </p>
                                        </div>

                                        <div style="border-bottom: 1px solid #95bccf; padding: 6px; text-align: center; background-color: #fff; border-radius: 10px; width: 200px; height: 40px; float: left;margin-left: 10px; ">
                                            <p>
                                                <asp:RadioButton ID="rdbPrestacaoDeContas" GroupName="g1" AutoPostBack="true" runat="server" OnCheckedChanged="rdbPMAS_CheckedChanged" />
                                                <span style="color: rgb(27, 161, 226); margin-right: 5px;">Prestação de Contas </span>
                                            </p>
                                        </div>

                                    </div>
                                    <div class="grid">
                                        <div class="row cells3">
                                            <asp:ListView ID="lstPMAS" runat="server" OnItemDataBound="lstPMAS_ItemDataBound"
                                                OnItemCommand="lstPMAS_ItemCommand" DataKeyNames="IdPrefeitura,IdMunicipio,Situacao">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="12" style="height: 20px;">
                                                                    <span class="ui-jqgrid-title">Planos Municipais</span>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="20" style="height: 22px;"></th>
                                                                <th width="100">Visualizar Plano                                            
                                                                </th>
                                                                <th width="170">Município                   
                                                                </th>
                                                                <th width="160">Devolver/desbloquear para<br />
                                                                    Órgão Gestor
                                                                </th>
                                                                <th width="140">Desbloquear CMAS                   
                                                                </th>
                                                                <th width="120">Devolver para CAS               
                                                                </th>
                                                                <th width="150">Enviar para finalização                   
                                                                </th>
                                                                <%--       <th class="ui-state-default ui-th-column ui-th-ltr" width="150">
                                       Parecer da DRADS              
                                    </th>--%>
                                                                <th width="150">Situação PMAS                   
                                                                </th>
                                                                <th width="80">Histórico
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server"></tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="info" style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" /></td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisMunicipio" ToolTip="Visualizar Plano" ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar_Municipio" />
                                                        </td>
                                                        <td class="align-left"><%#DataBinder.Eval(Container.DataItem, "Municipio") %></td>
                                                        <td class="align-center">
                                                            <asp:LinkButton ID="lkbDevolverOrgaoGestor" runat="server" CausesValidation="false" CommandName="DevolverOrgaoGestor" Visible="false" />
                                                            <asp:LinkButton ID="lkbAutorizaDevolverOrgaoGestor" runat="server" CausesValidation="false" CommandName="AutorizarDevolucaoOrgaoGestor" Visible="false" />&nbsp                                                                
                                                        </td>
                                                        <td class="align-center">
                                                            <asp:LinkButton ID="lkbAutorizaDevolverCMAS" runat="server" CausesValidation="false" CommandName="AutorizarDevolucaoCMAS" Visible="false" />&nbsp                                
                                                        </td>
                                                        <td class="align-center">
                                                            <asp:LinkButton ID="lkbDevolverCAS" runat="server" CausesValidation="false" CommandName="DevolverCAS" Visible="false" />&nbsp                                
                                                        </td>
                                                        <td class="align-center">
                                                            <asp:LinkButton ID="lkbEnviarFinalizacao" runat="server" CausesValidation="false" CommandName="EnviarFinalizacao" Visible="false" />&nbsp                                                                
                                                        </td>
                                                        <%-- <td class="align-center">
                                                                <asp:LinkButton ID="lkbParecerDrads" runat="server" CausesValidation="false" CommandName="ParecerDrads" Visible="false" />&nbsp                                                                
                                                             </td>--%>
                                                        <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "Situacao.Nome") %></td>

                                                        <td class="align-center">
                                                            <asp:ImageButton runat="server" ID="btnVisHistorico" ToolTip="Visualizar Histórico" ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar_Historico" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
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
                                                                        </div>
                                                                    </div>
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
                                       
                                    <!--Prestação De Contas-->

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

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdbEFLO" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rdbPMAS" EventName="CheckedChanged" />
                                
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
