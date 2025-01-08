<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultarUsuarioCadUnico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Usuario.ConsultarUsuarioCadUnico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form name="frmUsuarios">
        <div class="accordion">
            <div class="frame active">
                <div class="heading">
                    Cadastro Único de Usuários
                           <span class="mif-users icon"></span>
                </div>
                <div class="content">
                    <div class="formInput" data-text="Cadastro Único">
                        <div class="grid">
                            <div class="row cells2">
                                <div class="cell  text-right">
                                    Nome:	
                                    <div class="input-control text size-p100">

                                        <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="cell">
                                   
                                    <div class="input-control text full-size">
                                         RG:		
                                        <asp:TextBox ID="txtRg" runat="server" Width="100px"></asp:TextBox>
                                        <b>Sem o dígito </b>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="cell text-center">
                                    <asp:Button ID="btnPesquisar" runat="server" SkinID="button-find"
                                        Text="Pesquisar " OnClick="btnPesquisar_Click" />
                                    <asp:Button ID="btnIncluir" runat="server"
                                        Text="Incluir Usuário" Visible="False" PostBackUrl="~/Usuario/FUsuario.aspx" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <asp:ListView ID="lstUsuarios" runat="server" OnItemDataBound="lstUsuarios_ItemDataBound">
                                        <LayoutTemplate>
                                            <table class="table striped border bordered" cellspacing="0" cellpadding="0" border="0">
                                                <thead  class="info">
                                                    <tr>
                                                        <th colspan="5" style="height: 20px;">
                                                            <span class="mif-users"></span>Usuários
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th width="20" style="height: 22px;"></th>
                                                        <th  width="300">Nome                                            
                                                        </th>
                                                        <th width="120">Login                   
                                                        </th>
                                                        <th width="100">RG
                                                        </th>
                                                        <th width="100">Situação
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
                                                <td style="height: 22px;">
                                                    <asp:Label ID="lblSequencia" runat="server" /></td>
                                                <td align="left">
                                                    <a href="FUsuario.aspx?id=<%#Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem,"USU_ID").ToString()))%>"><%#DataBinder.Eval(Container.DataItem, "USU_NOME") %></a>
                                                </td>
                                                <td align="left"><%#DataBinder.Eval(Container.DataItem, "USU_LOGIN")%></td>
                                                <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "USU_RG") %>&nbsp</td>
                                                <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "USU_DES_SITUACAO")%></td>
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




    <%-- <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0" width="700" align="center" border="0">
        </td>
        </tr>
        <tr>
            <td>&nbsp</td>
        </tr>
    </table>--%>
</asp:Content>
