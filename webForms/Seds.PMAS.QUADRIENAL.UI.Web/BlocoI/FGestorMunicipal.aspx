<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FGestorMunicipal.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoI.FGestorMunicipal" %>

<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc1" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <br />
            <form name="frmOrgaoGestor">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro7" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                             1.6 - Identificação do Gestor Municipal de Assistência Social
                             <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Gestor Municipal">
                                <div class="grid">
                                    <div class="row cell">
                                        <div class="cell">
                                            <b>Nome:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlUsuario" runat="server" Width="250px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Cargo:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlCargo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCargo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell" id="trOutros" runat="server" visible="false">
                                            <b>Especificar:</b>
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtCargoOutro" runat="server" MaxLength="60" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells4">
                                        <div class="cell">
                                            <b>Data de nomeação:</b><br />
                                            <uc4:data ID="txtdata" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Escolaridade:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlEscolaridade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEscolaridade_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell" id="tdFormacaoAcademica" runat="server" visible="false">
                                            <b>Área de formação acadêmica:</b><br />
                                            <div class="input-control select full-size">
                                                <asp:DropDownList ID="ddlFormacaoAcademica" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormacaoProfissional_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell" id="trOutraFormacao" runat="server" visible="false">
                                            <b>Especificar:</b>
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtOutraAreaFormacao" MaxLength="60" runat="server" CssClass="campoTexto"
                                                    Width="150px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <fieldset>
                                            <legend>Contato Institucional</legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Telefone:</b><br />
                                                    <uc1:telefone ID="txtTelefone" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Celular:</b><br />
                                                    <uc5:celular ID="txtCelular" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>E-mail:</b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" Width="232px" CausesValidation="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row cell" id="trDataTerminoGestao" runat="server" visible="false">
                                        <b>Data final da gestão:</b><br />
                                        <uc4:data ID="txtDataTerminoGestao" runat="server" />
                                        <asp:Button ID="btnSalvarTerminoGestao" runat="server" Text="Finalizar" SkinID="button-save" OnClick="btnSalvarTerminoGestao_Click" CausesValidation="false"></asp:Button>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvar_Click"
                                                ValidationGroup="vgCampos"></asp:Button>
                                        </div>
                                        <div class="cell">
                                            <asp:Button ID="btnEditar" runat="server" SkinID="button-save" Text="Atualizar dados do gestor atual"
                                                OnClick="btnEditar_Click" CausesValidation="false" CssClass="btn btn-primary button-edit"></asp:Button>
                                        </div>
                                        <div class="cell">
                                            <asp:Button ID="btnSubstituir" runat="server" SkinID="button-save" Width="230px" Text="Registrar dados do novo gestor"
                                                OnClick="btnSubstituir_Click" CausesValidation="false"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <fieldset>
                                    <div class="flex-grid frame active">
                                        <div class="subheader">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro8" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>&nbsp;
                                             Gestores municipais de Assistência Social anteriores
                                        </div>

                                        <div class="row cells2">
                                            <div class="cell auto-size">
                                                <asp:ListView ID="lstGestores" runat="server" OnItemCommand="lstGestores_ItemCommand"
                                                    DataKeyNames="Id" OnItemDataBound="lstGestores_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <table class="table striped border" cellspacing="0"
                                                            cellpadding="0" border="0" width="100%">
                                                            <thead class="info">
                                                                <tr>
                                                                    <th width="20" style="height: 22px;"></th>
                                                                    <th width="250">Nome
                                                                    </th>
                                                                    <th width="180">Período de gestão
                                                                    </th>
                                                                    <th width="100">Excluir
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--    <tr class="jqgfirstrow" style="height: auto;">
                                                                    <td style="height: 0px;"></td>
                                                                    <td style="height: 0px;"></td>
                                                                    <td style="height: 0px;"></td>
                                                                    <td style="height: 0px;"></td>
                                                                </tr>--%>
                                                                <tr id="itemPlaceholder" runat="server">
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="height: 22px;">
                                                                <asp:Label ID="lblSequencia" runat="server" />
                                                            </td>
                                                            <td align="left">
                                                                <%#DataBinder.Eval(Container.DataItem, "Nome") %></a>
                                                            </td>
                                                            <td align="center">
                                                                <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataNomeacao")).ToString("dd/MM/yyyy") %>
                                        -
                                        <%#DataBinder.Eval(Container.DataItem, "DataTerminoGestao") == null ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataTerminoGestao")).ToString("dd/MM/yyyy") %>
                                                            </td>
                                                            <td class="align-center">
                                                                <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                    CommandName="Excluir_Gestor" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o gestor municipal?');" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div align="center" style="width: 100%;">
                                                            <b class="titulo">Não existe registro de outros gestores neste período</b>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>
            </form>

            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="580" align="center" border="0">
                <tr>
                    <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique
                            as inconsistências:</b>
                        <br />
                        <br />
                        <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                    </td>

                </tr>
            </table>


            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FOrgaoGestor.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FFundoMunicipal.aspx">Próximo
                             <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfIdGestor" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
