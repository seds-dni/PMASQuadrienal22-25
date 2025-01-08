<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FUsuario.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Usuario.FUsuario" %>

<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc2" %>
<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc3" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <br />
            <form name="frmPrefeitura">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            Cadastro de Usuário
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Usuário do sistema">
                                <div class="grid">
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>Perfil:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:DropDownList ID="ddlPerfil" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlPerfil_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Drads:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:DropDownList ID="ddlDrads" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Município:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:DropDownList ID="ddlMunicipio" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Nome:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="100" Width="352px"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>E-mail:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="348px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells4">
                                        <div class="cell">
                                            <b>R.G.:</b>
                                            <br />
                                            <div class="input-control text">
                                                <uc3:rg ID="txtRG" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Orgão Emissor:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtOrgaoEmissor" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>UF:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtUF" runat="server" Width="40" MaxLength="2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>C.P.F.:</b>
                                            <br />
                                            <div class="input-control text">
                                                <uc2:cpf ID="txtCPF" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <uc1:cep ID="controleCep" runat="server" />
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Telefone:</b>
                                            <br />
                                            <uc4:telefone ID="txtTelefone" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Telefone:</b>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Instituição/Órgão:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtInstituicao" runat="server" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Cargo/Função:</b>
                                            <br />
                                            <asp:TextBox ID="txtCargo" runat="server" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Login:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Situação:</b>
                                            <br />
                                            <asp:RadioButtonList ID="rblSituacao" runat="server" AutoPostBack="True" Height="16px"
                                                RepeatDirection="Horizontal" Width="200px">
                                                <asp:ListItem Value="1">Ativo</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">Inativo</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" ValidationGroup="vgCampos" Text="Salvar" OnClick="btnSalvar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <br />
            <table cellspacing="2" cellpadding="0" width="580" align="center" border="0">
                <tr>
                    <td class="ui-state-highlight" style="padding: 2px 10px 2px 10px;">
                        <asp:RequiredFieldValidator ID="rfvddlPerfil" runat="server" Display="None" ControlToValidate="ddlPerfil" ErrorMessage="É obrigatório informar o perfil." InitialValue="0" ValidationGroup="vgCampos" />
                        <asp:RequiredFieldValidator ID="rfvNome" ControlToValidate="txtNome" Display="None" runat="server" ErrorMessage="É obrigatório informar o nome." ValidationGroup="vgCampos" />
                        <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" Display="None" runat="server" ErrorMessage="É obrigatório informar o email." ValidationGroup="vgCampos" />
                        <asp:RequiredFieldValidator ID="rfvOrgaoEmissor" ControlToValidate="txtOrgaoEmissor" Display="None" runat="server" ErrorMessage="É obrigatório informar o órgão emissor." ValidationGroup="vgCampos" />
                        <asp:RequiredFieldValidator ID="rfvUF" ControlToValidate="txtUF" Display="None" runat="server" ErrorMessage="É obrigatório informar a UF." ValidationGroup="vgCampos" />
                        <asp:RequiredFieldValidator ID="rfvLogin" ControlToValidate="txtLogin" Display="None" runat="server" ErrorMessage="É obrigatório informar o login." ValidationGroup="vgCampos" />
                        <asp:ValidationSummary ID="vsummary" runat="server" ValidationGroup="vgCampos" DisplayMode="List" HeaderText="<img src='../Styles/Icones/messagebox_warning.png' align='absMiddle' /><b style='color:#000000 !important'>Verifique as inconsistências:</b>" />
                    </td>
                </tr>
            </table>

            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0" width="580" align="center" border="0">
                <tr>
                    <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique as inconsistências:</b>
                        <br />
                        <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfAction" runat="server" Value="UPDATE" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
