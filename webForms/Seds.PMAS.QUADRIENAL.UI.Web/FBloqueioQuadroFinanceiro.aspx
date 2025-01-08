<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FBloqueioQuadroFinanceiro.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.FBloqueioQuadroFinanceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmOrgaoGestor">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <%--<b>Bloqueio dos Quadros de Execução financeira e Lei Orçamentária</b>--%>
                            <b>Atualização anual - Quadros de Execução financeira e Lei Orçamentária</b>
                        </div>
                        <div class="content">
                            <div class="formInput">
                                <div class="grid">
                                    <div class="row cell">
                                        <div class="row">

                                            <asp:HiddenField runat="server" ID="hdnPCExercicio" Value="" />
                                            <asp:HiddenField runat="server" ID="hdnPCDesbloqueado" Value=""/>


                                            <asp:HiddenField runat="server" ID="hdnEFExercicio" Value="" />
                                            <asp:HiddenField runat="server" ID="hdnEFDebloqueado" Value="false" />

                                            <asp:HiddenField runat="server" ID="hdnLOExercicio" Value="" />
                                            <asp:HiddenField runat="server" ID="hdnLODebloqueado" Value="false" />

                                            <!--Prestação de contas-->
                                                <div class="row b-inicio-container default-bg-fff">
                                                        <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 200px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                            <b>
                                                                Prestacao de contas
                                                            </b>
                                                        </div>

                                                        <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                            <b>
                                                               <asp:Label runat="server" ID="lblExercicioPC1" Value="" /></b>
                                                            <asp:HiddenField runat="server" ID="hdnExercicioPC1" Value="" />
                                                            <asp:ImageButton ID="ImageButton23" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCDesbloquear($('#MainContent_hdnExercicioPC1').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                            <asp:ImageButton ID="ImageButton24" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCBloquear($('#MainContent_hdnExercicioPC1').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                        </div>

                                                        <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                            <b>
                                                               <asp:Label runat="server" ID="lblExercicioPC2" Value="" /></b>
                                                            <asp:HiddenField runat="server" ID="hdnExercicioPC2" Value="" />
                                                            <asp:ImageButton ID="ImageButton21" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCDesbloquear($('#MainContent_hdnExercicioPC2').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                            <asp:ImageButton ID="ImageButton22" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCBloquear($('#MainContent_hdnExercicioPC2').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                        </div>

                                                        <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                            <b>
                                                               <asp:Label runat="server" ID="lblExercicioPC3" Value="" /></b>
                                                            <asp:HiddenField runat="server" ID="hdnExercicioPC3" Value="" />
                                                            <asp:ImageButton ID="ImageButton17" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCDesbloquear($('#MainContent_hdnExercicioPC3').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                            <asp:ImageButton ID="ImageButton18" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCBloquear($('#MainContent_hdnExercicioPC3').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                        </div>

                                                        <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                             <b>
                                                                <asp:Label runat="server" ID="lblExercicioPC4" Value="" /></b>
                                                             <asp:HiddenField runat="server" ID="hdnExercicioPC4" Value="" />
                                                             <asp:ImageButton ID="ImageButton19" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCDesbloquear($('#MainContent_hdnExercicioPC4').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                             <asp:ImageButton ID="ImageButton20" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.PCBloquear($('#MainContent_hdnExercicioPC4').val()); " OnClick="btnSalvar_PrestacaoDeContasBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                        </div>

                                              </div>
                                            <div class="row">
                                                <hr />
                                            </div>

                                            <%--Execução Financeira --%>
                                            <div class="row b-inicio-container default-bg-fff">
                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 200px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>Execução Financeira
                                                        <asp:Label runat="server" ID="Label3" Value="" /></b>
                                                </div>

                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                       <asp:Label runat="server" ID="lblExercicioEF1" Value="" /></b>
                                                    <asp:HiddenField runat="server" ID="hdnExercicioEF1" Value="" />
                                                    <asp:ImageButton ID="ImageButton7" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFDesbloquear($('#MainContent_hdnExercicioEF1').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton8" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFBloquear($('#MainContent_hdnExercicioEF1').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioEF2" Value="" /></b>
                                                    <asp:HiddenField runat="server" ID="hdnExercicioEF2" Value="" />
                                                    <asp:ImageButton ID="ImageButton1" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFDesbloquear($('#MainContent_hdnExercicioEF2').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton2" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFBloquear($('#MainContent_hdnExercicioEF2').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>

                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioEF3" Value="" /></b>
                                                    <asp:HiddenField runat="server" ID="hdnExercicioEF3" Value="" />
                                                    <asp:ImageButton ID="ImageButton3" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFDesbloquear($('#MainContent_hdnExercicioEF3').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton4" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFBloquear($('#MainContent_hdnExercicioEF3').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioEF4" Value="" /></b>
                                                    <asp:HiddenField runat="server" ID="hdnExercicioEF4" Value="" />
                                                    <asp:ImageButton ID="ImageButton5" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFDesbloquear($('#MainContent_hdnExercicioEF4').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton6" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.EFBloquear($('#MainContent_hdnExercicioEF4').val()); " OnClick="btnSalvar_ExecucaoFinanceiraBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>

                                            </div>

                                            <div class="row">
                                                <hr />
                                            </div>


                                            <%--Lei Orçamentária--%>
                                            <div class="row b-inicio-container dLOault-bg-fff">
                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 200px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>Lei Orçamentária
                                                        <asp:Label runat="server" ID="Label4" Value="" /></b>
                                                </div>

                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioLO1" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton9" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LODesbloquear($('#MainContent_lblExercicioLO1').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton10" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LOBloquear($('#MainContent_lblExercicioLO1').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioLO2" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton11" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LODesbloquear($('#MainContent_lblExercicioLO2').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton12" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LOBloquear($('#MainContent_lblExercicioLO2').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioLO3" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton13" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LODesbloquear($('#MainContent_lblExercicioLO3').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton14" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LOBloquear($('#MainContent_lblExercicioLO3').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicioLO4" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton15" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LODesbloquear($('#MainContent_lblExercicioLO4').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton16" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.LOBloquear($('#MainContent_lblExercicioLO4').text()); " OnClick="btnSalvar_LeiOrcamentariaBloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                    </div>
                                </div>
                                <asp:Label ID="Label2" runat="server" />
                            </div>
                        </div>

                        <asp:Label ID="lblErro" runat="server" />
                    </div>
                </div>

                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>Bloqueio / Desbloqueio</b>
                            <span class="mif-flow-cascade icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Consultar PMAS" style="min-height: 150px;">
                                <div class="grid">
                                    <div class="row cells3">
                                        <div class="cell">
                                            Município:
                                    <div class="input-control select">
                                        <asp:DropDownList runat="server" ID="ddlMunicipio">
                                        </asp:DropDownList>
                                    </div>
                                        </div>
                                        <div class="cell" runat="server" id="tdSituacao" visible="false">
                                            Situação do Plano:
                                    <div class="input-control select" runat="server" id="tdLstSituacao" visible="false">
                                        <asp:DropDownList runat="server" ID="ddlSituacao">
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
                                    <div class="row align-center">
                                        <div class="cell">
                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:ListView ID="lstPrefeituras" runat="server" DataKeyNames="Id,IdMunicipio,IdSituacao">
                                            <LayoutTemplate>
                                                <table class="table striped border bordered" cellspacing="0"
                                                    cellpadding="0" border="0">
                                                    <thead class="info">
                                                        <tr>
                                                            <th colspan="12" style="height: 20px;">
                                                                <span class="mif-file-text"></span>Planos Municipais
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <th style="min-width: 100px;">Municipio</th>
                                                            <th style="min-width: 100px;">Situação PMAS</th>
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
                                                    <td style="font-size: 12px; min-width: 100px; background-color: #bde0dd; text-align: center">
                                                        <%#DataBinder.Eval(Container.DataItem, "Cidade") %>
                                                    </td>
                                                    <td style="font-size: 12px; min-width: 100px; background-color: #bde0dd; text-align: center">
                                                        <%#DataBinder.Eval(Container.DataItem, "Situacao") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" class="align-center" style="padding: 0px;">
                                                        <asp:ListView runat="server"
                                                            ItemPlaceholderID="itemPlaceholderRecursos"
                                                            DataSource='<%#DataBinder.Eval(Container.DataItem, "PrefeiturasSituacoesQuadrosEF") %>'>
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0" cellpadding="0">
                                                                    <thead class="info">
                                                                        <th style="min-width: 320px; height: 30px; float: left; position: relative; font-weight: bold;">Funcionalidade
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                    </thead>
                                                                    <tbody id="itemPlaceholderRecursos" runat="server">
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="min-width: 320px; height: 50px; float: left; position: relative; font-weight: bold; padding: 10px 0px 0px 10px">
                                                                        <%#MontarRecursosLabel(DataBinder.Eval(Container.DataItem, "IdRecurso").ToString()) %>
                                                                        <hr />
                                                                    </td>
                                                                    <td colspan="4" style="min-width: 230px; height: 40px; float: left; position: relative; font-weight: bold; padding: 10px 4px 4px 4px;">
                                                                        <asp:ListView ID="lstPrefeiturasRecursosExercicios"
                                                                            runat="server"
                                                                            OnItemCommand="lstPrefeiturasRecursos_ItemCommand"
                                                                            ItemPlaceholderID="itemPlaceholderRecursosExercicios"
                                                                            DataKeyNames="Exercicio, IdPrefeitura, IdRecurso, IdSituacao"
                                                                            DataSource='<%#DataBinder.Eval(Container.DataItem, "Exercicios") %>'>
                                                                            <LayoutTemplate>
                                                                                <div id="itemPlaceholderRecursosExercicios" runat="server">
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <div class="<%#"top-container-exercicio-" + ((int)DataBinder.Eval(Container.DataItem, "Exercicio")) + "" %>">


                                                                                    <div style="text-align: center; width: 70px; float: left; position: relative; font-weight: bold;">
                                                                                        <div class="<%#"container-exercicio-" + ((int)DataBinder.Eval(Container.DataItem, "Exercicio")) + "" %>">
                                                                                            <%#((int)DataBinder.Eval(Container.DataItem, "Exercicio"))%>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div style="text-align: center; width: 100px; height: 30px; float: left; position: relative; font-weight: bold; display: table-cell"
                                                                                        class="<%#"container-exercicio-" + ((int)DataBinder.Eval(Container.DataItem, "Exercicio")) + "" %>">
                                                                                        <%#( (((Int32)DataBinder.Eval(Container.DataItem, "IdSituacao"))==1) ? "Sim" : "Não") %>

                                                                                        <asp:ImageButton Style="height: 15px; padding: 0px; margin-left: 10px; margin-top: -5px" runat="server" ID="btnVisRecursos" ToolTip="Visualizar Recursos"
                                                                                            ImageUrl='<%#( (((Int32)DataBinder.Eval(Container.DataItem, "IdSituacao")) == 1) ? "~/Styles/Icones/apply.png" : "~/Styles/Icones/cancel.png") %>'
                                                                                            CommandName="Bloqueio_Desbloqueio_Recursos" />
                                                                                    </div>


                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" class="align-center" style="padding: 0px;">
                                                        <asp:ListView ID="ListView1" runat="server"
                                                            ItemPlaceholderID="itemPlaceholderRecursos"
                                                            DataSource='<%#DataBinder.Eval(Container.DataItem, "PrefeiturasSituacoesQuadrosLO") %>'>
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0" cellpadding="0">
                                                                    <thead class="info">
                                                                        <th style="min-width: 320px; height: 30px; float: left; position: relative; font-weight: bold;">Funcionalidade
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                        <th style="width: 80px; height: 30px; float: left; position: relative; font-weight: bold;">Exercício 
                                                                        </th>
                                                                        <th style="width: 100px; height: 30px; float: left; position: relative; font-weight: bold;">Desbloqueado?
                                                                        </th>
                                                                    </thead>
                                                                    <tbody id="itemPlaceholderRecursos" runat="server">
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="min-width: 320px; height: 50px; float: left; position: relative; font-weight: bold; padding: 10px 0px 0px 10px">
                                                                        <%#MontarRecursosLabel(DataBinder.Eval(Container.DataItem, "IdRecurso").ToString()) %>
                                                                        <hr />
                                                                    </td>
                                                                    <td colspan="4" style="min-width: 230px; height: 40px; float: left; position: relative; font-weight: bold; padding: 10px 4px 4px 4px;">
                                                                        <asp:ListView ID="lstPrefeiturasRecursosExercicios"
                                                                            runat="server"
                                                                            OnItemCommand="lstPrefeiturasRecursos_ItemCommand"
                                                                            ItemPlaceholderID="itemPlaceholderRecursosExercicios"
                                                                            DataKeyNames="Exercicio, IdPrefeitura, IdRecurso, IdSituacao"
                                                                            DataSource='<%#DataBinder.Eval(Container.DataItem, "Exercicios") %>'>
                                                                            <LayoutTemplate>
                                                                                <div id="itemPlaceholderRecursosExercicios" runat="server">
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <div class="<%#"top-container-exercicio-" + ((int)DataBinder.Eval(Container.DataItem, "Exercicio")) + "" %>">

                                                                                    <div style="text-align: center; width: 70px; float: left; position: relative; font-weight: bold;">
                                                                                        <div class="<%#"container-exercicio-" + ((int)DataBinder.Eval(Container.DataItem, "Exercicio")) + "" %>">
                                                                                            <%#DataBinder.Eval(Container.DataItem, "Exercicio") %>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div style="text-align: center; width: 100px; height: 30px; float: left; position: relative; font-weight: bold; display: table-cell"
                                                                                        class="<%#"container-exercicio-" + ((int)DataBinder.Eval(Container.DataItem, "Exercicio")) + "" %>">
                                                                                        <%#( (((Int32)DataBinder.Eval(Container.DataItem, "IdSituacao"))==1) ? "Sim" : "Não") %>

                                                                                        <asp:ImageButton Style="height: 15px; padding: 0px; margin-left: 10px; margin-top: -5px" runat="server" ID="btnVisRecursos" ToolTip="Visualizar Recursos"
                                                                                            ImageUrl='<%#( (((Int32)DataBinder.Eval(Container.DataItem, "IdSituacao")) == 1) ? "~/Styles/Icones/apply.png" : "~/Styles/Icones/cancel.png") %>'
                                                                                            CommandName="Bloqueio_Desbloqueio_Recursos" />
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
