<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FDesbloqueioAnual.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.FDesbloqueioAnual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmOrgaoGestor">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>Atualização anual - Funcionalidade</b>
                        </div>
                        <div class="content">
                            <div class="formInput">
                                <div class="grid">
                                    <div class="row cell">
                                        <div class="row">
                                            <asp:HiddenField runat="server" ID="hdnExercicioParaBloquearDesbloquear" Value="" />
                                            <asp:HiddenField runat="server" ID="hdnEhdesbloqueio" Value="false" />

                                            <%--Recursos --%>
                                            <div class="row b-inicio-container default-bg-fff">
                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 200px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>Recursos
                                                        <asp:Label runat="server" ID="Label5" Value="" /></b>
                                                </div>

                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicio1" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton17" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicio1').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton18" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicio1').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicio2" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton19" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicio2').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton20" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicio2').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicio3" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton21" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicio3').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton22" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicio3').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>


                                                <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
                                                    <b>
                                                        <asp:Label runat="server" ID="lblExercicio4" Value="" /></b>
                                                    <asp:ImageButton ID="ImageButton23" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicio4').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                    <asp:ImageButton ID="ImageButton24" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicio4').text()); " OnClick="btnSalvar_BloquearDesbloquearClick" SkinID="button-save" runat="server" />
                                                </div>

                                            </div>

                                                <%--Reprogramados --%>
	                                            <div class="row b-inicio-container default-bg-fff">
		                                            <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 200px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>Reprogramados
				                                            <asp:Label runat="server" ID="Label2" Value="" /></b>
		                                            </div>

		                                            <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                        <asp:Label runat="server" ID="lblExercicioReprogramados1" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioReprogramados1').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioReprogramados1').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
		                                            </div>
		                                            <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                            <asp:Label runat="server" ID="lblExercicioReprogramados2" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton3" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioReprogramados2').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton4" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioReprogramados2').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
		                                            </div>
		                                            <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                            <asp:Label runat="server" ID="lblExercicioReprogramados3" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton5" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioReprogramados3').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton6" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioReprogramados3').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
		                                            </div>
		                                            <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                            <asp:Label runat="server" ID="lblExercicioReprogramados4" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton7" Width="30px" ImageUrl="~/Styles/Icones/unlock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioReprogramados4').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton8" Width="30px" ImageUrl="~/Styles/Icones/lock.png" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioReprogramados4').text()); " OnClick="btnSalvar_BloquearDesbloquearReprogramadosClick" SkinID="button-save" runat="server" />
		                                            </div>

	                                            </div>

                                                <%--DEMANDAS PARLAMENTARES--%>
	                                            <div class="row b-inicio-container default-bg-fff">
		                                            
                                                    <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 200px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>Demandas  Parlamentares
				                                            <asp:Label runat="server" ID="Label3" Value="" /></b>
		                                            </div>

                                                    <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                        <asp:Label runat="server" ID="lblExercicioDemandasParlamentares1" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton9" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioDemandasParlamentares1').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton10" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioDemandasParlamentares1').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
		                                            </div>
                                                    <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                        <asp:Label runat="server" ID="lblExercicioDemandasParlamentares2" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton11" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioDemandasParlamentares2').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton12" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioDemandasParlamentares2').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
		                                            </div>
                                                    <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                        <asp:Label runat="server" ID="lblExercicioDemandasParlamentares3" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton13" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioDemandasParlamentares3').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton14" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioDemandasParlamentares3').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
		                                            </div>                                                    
                                                    <div class="col-md-2 col-lg-2 col-xs-12" style="padding-top: 18px; width: 50px; margin-top: 0px; font-size: 12px; font-family: -apple-system, Segoe UI, Helvetica Neue,Arial,Sans-Serif;">
			                                            <b>
				                                        <asp:Label runat="server" ID="lblExercicioDemandasParlamentares4" Value="" /></b>
			                                            <asp:ImageButton ID="ImageButton15" ImageUrl="~/Styles/Icones/unlock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosDesbloquear($('#MainContent_lblExercicioDemandasParlamentares4').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
			                                            <asp:ImageButton ID="ImageButton16" ImageUrl="~/Styles/Icones/lock.png" Width="30px" OnClientClick="return seds.inicio.FBloqueioQuadroFinanceiro.RecursosBloquear($('#MainContent_lblExercicioDemandasParlamentares4').text()); " OnClick="btnSalvar_BloquearDesbloquearDemandasParlamentaresClick" SkinID="button-save" runat="server" />
		                                            </div>
                                                </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                    </div>
                                </div>
                                <asp:Label ID="Label1" runat="server" />
                            </div>
                        </div>
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
                                        <%--<div class="cell">
                                            DRADS:
                                    <div class="input-control select">
                                        <asp:DropDownList runat="server" ID="ddlDrads" AutoPostBack="true" OnSelectedIndexChanged="ddlDrads_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                        </div>--%>
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
                                                        <asp:ListView ID="lstPrefeiturasRecursos"
                                                            runat="server"
                                                            ItemPlaceholderID="itemPlaceholderRecursos"
                                                            DataSource='<%#DataBinder.Eval(Container.DataItem, "PrefeiturasExerciciosBloqueio") %>'>
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
                                                                        <%#MontarRecursosLabel(DataBinder.Eval(Container.DataItem, "Key").ToString()) %>
                                                                        <hr />
                                                                    </td>
                                                                    <td colspan="4" style="min-width: 230px; height: 40px; float: left; position: relative; font-weight: bold; padding: 10px 4px 4px 4px;">
                                                                        <asp:ListView ID="lstPrefeiturasRecursosExercicios"
                                                                            runat="server"
                                                                            OnItemCommand="lstPrefeiturasRecursos_ItemCommand"
                                                                            ItemPlaceholderID="itemPlaceholderRecursosExercicios"
                                                                            DataKeyNames="Exercicio, IdPrefeitura, IdRefBloqueio, Desbloqueado"
                                                                            DataSource='<%#DataBinder.Eval(Container.DataItem, "Valor.Exercicios") %>'>
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
                                                                                     
                                                                                        <%#(((Boolean)DataBinder.Eval(Container.DataItem, "Desbloqueado")) ? "Sim" : "Não") %>

                                                                                        <asp:ImageButton Style="height: 15px; padding: 0px; margin-left: 10px; margin-top: -5px" runat="server" ID="btnVisRecursos" ToolTip="Visualizar Recursos"
                                                                                            ImageUrl='<%#(((Boolean)DataBinder.Eval(Container.DataItem, "Desbloqueado")) ? "~/Styles/Icones/apply.png" : "~/Styles/Icones/cancel.png") %>'
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
                <asp:Label ID="lblErro" runat="server" />
            </form>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SecondContent" runat="server">
</asp:Content>
