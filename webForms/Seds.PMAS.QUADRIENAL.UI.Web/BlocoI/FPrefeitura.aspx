<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FPrefeitura.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoI.FPrefeitura"  %>

<%@ Register Src="~/Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Src="~/Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc6" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc7" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <script type="text/javascript">
        $('#MainContent_cep1_cmdPesqCEP').click(function () {
            //$("#fraPrefeitura").addClass("frame active");
            alert('1');
        })
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmPrefeitura">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame" id="fraPrefeitura" runat="server">
                        <div class="heading">
                            1.1 - Identificação da Prefeitura Municipal
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="prefeitura municipal">
                                <div class="grid">
                                    <div class="row" runat="server" visible="false" id="alteracoesQuadro">
                                        <div class="cell" align="right">
                                            <a href="#" runat="server" id="linkAlteracoes" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>CNPJ:</b><br />
                                            <div class="input-control text">
                                                <asp:Label ID="lblCNPJ" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Município:</b><br />
                                            <div class="input-control text">
                                                <asp:Label ID="txtMunicipio" runat="server" Width="150px" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>DRADS:</b><br />
                                            <div class="input-control text">
                                                <asp:Label ID="txtDrads" runat="server" Width="150px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>Nº de habitantes:</b><br />
                                            <div class="input-control text">
                                                <asp:Label ID="txtNHabitantes" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Porte:</b>
                                            <br />
                                            <div class="input-control text">
                                                <asp:Label ID="lblPorte" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Gestão:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:RadioButtonList ID="rblGestao" runat="server" Width="300px" RepeatDirection="Horizontal"
                                                    Height="24px">
                                                    <asp:ListItem Value="1" Selected="True">Inicial</asp:ListItem>
                                                    <asp:ListItem Value="0">Básica</asp:ListItem>
                                                    <asp:ListItem Value="2">Plena</asp:ListItem>
                                                    <asp:ListItem Value="3">Não habilitado</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Data da última publicação de nível de gestão no DOE:</b>
                                            <br />
                                            <uc4:data ID="txtDataPublicacao" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cell">
                                        <div class="cell">
                                            <uc2:cep ID="cep1" runat="server" />
                                        </div>
                                    </div>
                                    <fieldset class="border-blue">
                                        <legend class="lgnd fg-blue">Contato Institucional</legend>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <b>Telefone fixo:</b><br />
                                                <uc3:telefone ID="telefone" runat="server" />
                                            </div>
                                            <div class="cell">
                                                <b>Telefone celular:</b><br />
                                                <uc5:celular ID="celular" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <b>E-mail institucional:</b><br />
                                                <div class="input-control text mid-size">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="cell">
                                                <b>Site:</b><br />
                                                <div class="input-control text mid-size">
                                                    <asp:TextBox ID="txtSite" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkPossuiSite" runat="server" AutoPostBack="true" Text="Não possui site"
                                                OnCheckedChanged="chkPossuiSite_CheckedChanged" />
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="row">
                                    <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                        width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                        <tr>
                                            <td style="padding: 15px 10px 2px 15px">
                                                <span class="mif-warning mif-2x"></span>
                                                <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px 12px 45px;">
                                                <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:Button ID="btnSalvar" CssClass="btn btn-primary" runat="server" Width="89px" Text="Salvar" SkinID="button-save" OnClick="btnSalvar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="frame" id="frmPrefeito" runat="server">
                    <div class="heading">
                        1.2 - Identificação do Prefeito
                           <span class="mif-home icon"></span>
                    </div>
                    <div class="content">
                        <div class="formInput" data-text="prefeito">
                            <div class="grid">
                                <div class="row" runat="server" id="alteracoesQuadro2" visible="false">
                                    <div class="cell" align="right">
                                        <a href="#" runat="server" id="linkAlteracoesQuadro2" visible="false">
                                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                        </a>
                                    </div>
                                </div>
                                <div class="row cells3">
                                    <div class="cell colspan2">
                                        <div class="input-control text full-size">
                                            <b>Nome:</b><br />
                                            <asp:TextBox ID="txtNome" runat="server" MaxLength="60"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="cell" id="tdCPF" runat="server">
                                        <b>CPF:</b><br />
                                        <div class="input-control text">
                                            <%--<uc7:cpf ID="txtCPF" runat="server" />--%>
                                            <asp:TextBox ID="txtCPF" placeholder="000.000.000-00" oninput="javascript:aplicarFormatacao(this, '000.000.000-00')" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row cells4" id="trRG" runat="server">
                                    <div class="cell">
                                        <div class="input-control">
                                            <b>RG:</b><br />
                                            <uc6:rg ID="txtRG" runat="server" />
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
                                <fieldset class="border-blue">
                                    <legend class="lgnd"><b class="fg-blue">Contato Institucional</b></legend>
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
                                <fieldset class="border-blue">
                                    <legend class="lgnd"><b class="fg-blue">Período de mandato</b></legend>
                                    <div class="grid">
                                        <div class="row cells2">
                                            <div class="cell colspan2">
                                                <b>Prefeito reeleito deve ser cadastrado novamente.</b>
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
                                            OnClick="btnSubstituir_Click" OnClientClick="javascript: ExibirAlertaTrocaDePrefeito(); return false; " CausesValidation="false"></asp:Button>
                                        <asp:Button Style="display: none" ID="btnSubstituirConfirmacao" runat="server" SkinID="button-save" Width="250px" Text="Registrar dados do novo prefeito"
                                            OnClick="btnSubstituirConfirmacao_Click" CausesValidation="false"></asp:Button>
                                    </div>

                                </div>
                                <div class="row">
                                    <table id="tbInconsistenciasPrefeito" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                        width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                        <tr>
                                            <td style="padding: 15px 10px 2px 15px">
                                                <span class="mif-warning mif-2x"></span>
                                                <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px 12px 45px;">
                                                <asp:Label ID="lblInconsistenciasPrefeito" ForeColor="Red" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <fieldset>
                                    <div class="grid frame active">

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
            <asp:HiddenField ID="hdfIdPrefeito" runat="server" Value="0" />
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left">&nbsp;
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FOrgaoGestor.aspx">Próximo
                              <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubstituirConfirmacao" EventName="Click" />
            <%--<asp:AsyncPostBackTrigger ControlID="txtData" EventName="Click" />--%>
            
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function ExibirAlertaTrocaDePrefeito() {

            var resultado = metroDialog.createWarning({
                title: "<h5><span class=\"mif-notification mif-ani-flash\" style=\"float: left; margin: 0 7px 0 0;\"></span>Confirmação de data</h5>",
                content: "A data de término do mandato já foi atualizada?",
                actions: [
                    {
                        title: "Sim",
                        onclick: function (el) {
                            console.log("Enviar!")
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSubstituir))%>;


                            $(el).data('dialog').close();
                        }
                    },
                    {
                        title: "Não",
                        onclick: function (el) {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSubstituirConfirmacao))%>;
                            $(el).data('dialog').close();
                        },
                        cls: "js-dialog-close"
                    }
                ],
                options: {
                }
            });
            }

    </script>
</asp:Content>
