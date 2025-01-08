<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FPrefeito.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoI.FPrefeito" %>

<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc2" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="~/Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastroPrefeito" runat="server">
        <ContentTemplate>
            <form name="frmPrefeito">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame" id="fraPrefeito" runat="server">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro2" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                            </a>
                            1.2 - Identificação do Prefeito
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="prefeito">
                                <div class="grid">
                                    <div class="row cells3">
                                        <div class="cell colspan2">
                                            <div class="input-control text full-size">
                                                <b>Nome:</b><br />
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="60"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>CPF:</b><br />
                                            <div class="input-control text">
                                                <uc2:cpf ID="txtCPF" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells4">
                                        <div class="cell">
                                            <div class="input-control">
                                                <b>RG:</b><br />
                                                <uc1:rg ID="txtRG" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Data da emissão:</b><br />
                                            <uc4:data ID="txtDataEmissao" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Sigla do órgão emissor:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtOrgEmissor" runat="server" Width="70px" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>U.F.:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlUFPrefeito" Height="33px" runat="server">
                                                    <asp:ListItem Value="0" Text="[Escolha uma Opção]" Selected="True" />
                                                    <asp:ListItem Value="1" Text="AC" />
                                                    <asp:ListItem Value="2" Text="AL" />
                                                    <asp:ListItem Value="3" Text="AM" />
                                                    <asp:ListItem Value="4" Text="AP" />
                                                    <asp:ListItem Value="5" Text="BA" />
                                                    <asp:ListItem Value="6" Text="CE" />
                                                    <asp:ListItem Value="7" Text="DF" />
                                                    <asp:ListItem Value="8" Text="ES" />
                                                    <asp:ListItem Value="9" Text="GO" />
                                                    <asp:ListItem Value="10" Text="MA" />
                                                    <asp:ListItem Value="11" Text="MG" />
                                                    <asp:ListItem Value="12" Text="MS" />
                                                    <asp:ListItem Value="13" Text="MT" />
                                                    <asp:ListItem Value="14" Text="PA" />
                                                    <asp:ListItem Value="15" Text="PB" />
                                                    <asp:ListItem Value="16" Text="PE" />
                                                    <asp:ListItem Value="17" Text="PI" />
                                                    <asp:ListItem Value="18" Text="PR" />
                                                    <asp:ListItem Value="19" Text="RJ" />
                                                    <asp:ListItem Value="20" Text="RN" />
                                                    <asp:ListItem Value="21" Text="RO" />
                                                    <asp:ListItem Value="22" Text="RR" />
                                                    <asp:ListItem Value="23" Text="RS" />
                                                    <asp:ListItem Value="24" Text="SC" />
                                                    <asp:ListItem Value="25" Text="SE" />
                                                    <asp:ListItem Value="26" Text="SP" />
                                                    <asp:ListItem Value="27" Text="TO" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>Contato Institucional</legend>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <b>Telefone fixo:</b><br />
                                                <uc3:telefone ID="telefonePrefeito" runat="server" />
                                            </div>
                                            <div class="cell">
                                                <b>Telefone Celular:</b><br />
                                                <uc5:celular ID="celularPrefeito" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <b>E-mail institucional:</b><br />
                                                <div class="input-control text full-size">
                                                    <asp:TextBox ID="txtEmailPrefeito" runat="server" Width="500px" MaxLength="60"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <fieldset>
                                        <legend>Período de mandato</legend>
                                        <div class="grid">
                                            <div class="row cells2">
                                                <div class="cell colspan2">
                                                    <b>Considerar período de mandato de quatro anos.<br />
                                                        Prefeito reeleito deve ser cadastrado novamente.</b>
                                                </div>
                                            </div>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Data de início do mandato:</b>
                                                    <uc4:data ID="txtDataInicio" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Data de término do mandato:</b><br />
                                                    <uc4:data ID="txtDataTermino" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                    <div class="row cells3">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvarPrefeito" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvarPrefeito_Click"></asp:Button>
                                        </div>
                                        <div class="cell">
                                            <asp:Button ID="btnEditar" runat="server" SkinID="button-save" Text="Atualizar dados do prefeito" OnClick="btnEditar_Click"
                                                CausesValidation="false"></asp:Button>
                                        </div>
                                        <div class="cell">
                                            <asp:Button ID="btnSubstituir" runat="server" SkinID="button-save" Width="250px" Text="Registrar dados do novo prefeito"
                                                OnClick="btnSubstituir_Click" OnClientClick="javascript: openCustom(); return false;" CausesValidation="false"></asp:Button>
                                            <%--    OnClick="btnSubstituir_Click" OnClientClick="javascript: $('#registroPrefeito').dialog('open'); return false;" CausesValidation="false"></asp:Button>--%>
                                        </div>
                                    </div>
                                </div>
                                <fieldset>
                                    <div class="flex-grid frame active">

                                        <div class="subheader">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro3" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                                            </a>
                                            Prefeitos anteriores
                                        </div>
                                        <div class="row">
                                            <div class="cell cell auto-size">
                                                <asp:ListView ID="lstPrefeitos" runat="server" OnItemDataBound="lstPrefeitos_ItemDataBound"
                                                    OnItemCommand="lstPrefeitos_ItemCommand" DataKeyNames="Id">
                                                    <LayoutTemplate>
                                                        <table class="table striped border" cellspacing="0"
                                                            cellpadding="0" border="0">
                                                            <thead class="info">
                                                                <tr>
                                                                    <th width="20" style="height: 22px;"></th>
                                                                    <th width="250" align="left">Nome
                                                                    </th>
                                                                      <th width="90">CPF
                                                                    </th>
                                                                    <th width="150">Período do mandato
                                                                    </th>
                                                                    <th width="90">Excluir
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--  <tr style="height: auto;">
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
                                                                <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                            </td>
                                                              <td align="center">
                                                                <%#DataBinder.Eval(Container.DataItem, "CPF") %>
                                                            </td>
                                                            <td align="center">
                                                                <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "MandatoInicio").ToString()).ToShortDateString()%>
                                        -
                                        <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "MandatoTerminio").ToString()).ToShortDateString()%>
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                    CommandName="Excluir_Prefeito" CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir este registro?');" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div align="center" style="width: 100%;">
                                                            <b class="titulo">Não existe registro de outros prefeitos neste período</b>
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
            <table id="tbInconsistenciasPrefeito" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="580" align="center" border="0">
                <tr>
                    <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique
                            as inconsistências:</b>
                        <br />
                        <br />
                        <asp:Label ID="lblInconsistenciasPrefeito" ForeColor="Red" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FPrefeitura.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FPrefeito.aspx">Próximo
                            <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfIdPrefeito" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- <div id="registroPrefeito" title="Confirmação de data">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>A data de término do mandato já foi atualizada?</p>
    </div>--%>
    <%--      $(function () {
            $("#registroPrefeito").dialog({
                autoOpen: false,
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Sim": function () {
                       
                        $(this).dialog("close");
                    },
                    "Não": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });--%>

    <script type="text/javascript">
        function openCustom() {
            metroDialog.createWarning({
                title: "<h5><span class=\"mif-notification mif-ani-flash\" style=\"float: left; margin: 0 7px 0 0;\"></span>Confirmação de data</h5>",
                content: "A data de término do mandato já foi atualizada?",
                actions: [
                    {
                        title: "Sim",
                        onclick: function (el) {
                            //console.log(el);
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSubstituir))%>;
                            $(el).data('dialog').close();
                        }
                    },
                    {
                        title: "Não",
                        cls: "js-dialog-close"
                    }
                ],
                options: {
                }
            });
            }

    </script>
</asp:Content>
