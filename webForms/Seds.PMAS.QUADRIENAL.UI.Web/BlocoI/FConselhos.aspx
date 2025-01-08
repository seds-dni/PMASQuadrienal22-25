<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FConselhos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoI.FConselhos" %>

<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc6" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmConselhos">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            1.8- Conselhos existentes no Município
                             <a href="#" runat="server" id="linkAlteracoesQuadro10" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                            <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="conselho municipal">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Não cadastre neste quadro o Conselho Municipal de Assistência Social. O registro do CMAS será feito exclusivamente pelo próprio Conselho, no último bloco de informações do sistema.</b>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Conselho:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlConselhos" runat="server" Width="380px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlConselhos_SelectedIndexChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="grid">
                                        <asp:Panel ID="pnlDados" runat="server" Visible="false">
                                            <div class="row cells2" id="trNome" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Nome do Conselho:</b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox runat="server" ID="txtNome" Width="316px" />
                                                    </div>
                                                </div>
                                            </div>
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Lei de criação:</b></legend>
                                                <div class="grid">
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <b>Nº da Lei:</b><br />
                                                            <div class="input-control text full-size">
                                                                <asp:TextBox ID="txtNumeroLeiCriacao" runat="server" Width="60px" MaxLength="5"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLeiCriacao"
                                                                    runat="server" TargetControlID="txtNumeroLeiCriacao" FilterType="Numbers" />
                                                                /
                                                                    <asp:TextBox ID="txtAnoLeiCriacao" runat="server" Width="40px" MaxLength="2"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLeiCriacao"
                                                                    runat="server" TargetControlID="txtAnoLeiCriacao" FilterType="Numbers" />
                                                                (Ex: 129/11)
                                                            </div>

                                                        </div>
                                                        <div class="cell">
                                                            <b>Data de publicação da Lei:</b><br />
                                                            <uc4:data ID="txtDataCriacao" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Endereço</b></legend>
                                                <uc2:cep ID="cep1" runat="server" />
                                            </fieldset>
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Contato Institucional</b></legend>

                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Telefone fixo:</b><br />
                                                        <uc3:telefone ID="txtTelefone" runat="server" />
                                                    </div>
                                                    <div class="cell">
                                                        <b>Telefone celular:</b><br />
                                                        <uc5:celular ID="txtcelular" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>E-mail:</b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtEmail" runat="server" Width="250px" MaxLength="60"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Presidente do conselho:</b></legend>
                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Nome do Presidente:</b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox runat="server" ID="txtPresidente" Width="265px" />
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>CPF:</b><br />
                                                        <div class="input-control text">
                                                            <uc6:cpf ID="txtCPF" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" id="trRG" visible="false">
                                                    <div class="cell">
                                                        <b>RG:</b><br />
                                                        <div class="input-control text">
                                                            <uc1:rg ID="txtRG" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row cells3" runat="server" id="trEmissor" visible="false">
                                                    <div class="cell">
                                                        <b>Data da emissão:</b><br />
                                                        <uc4:data ID="txtDataEmissao" runat="server" />
                                                    </div>
                                                    <div class="cell">
                                                        <b>Sigla do órgão emissor:</b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtOrgEmissor" runat="server" Width="60px" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>U.F.:</b><br />
                                                        <div class="input-control select">
                                                            <asp:DropDownList ID="ddlUF" Height="33px" runat="server">
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
                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Data de início do mandato:</b><br />
                                                        <uc4:data ID="txtDataInicio" runat="server" />
                                                    </div>
                                                    <div class="cell">
                                                        <b>Data de término do mandato:</b><br />
                                                        <uc4:data ID="txtDataTermino" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row cells3">
                                                    <div class="cell">
                                                        <asp:Button ID="btnSalvarPresidente" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvarPresidente_Click"></asp:Button>
                                                    </div>
                                                    <div class="cell">
                                                        <asp:Button ID="btnEditar" runat="server" SkinID="button-save" Text="Atualizar dados do presidente" OnClick="btnEditar_Click"
                                                            CausesValidation="false"></asp:Button>
                                                    </div>
                                                    <div class="cell">
                                                        <asp:Button ID="btnSubstituir" runat="server" SkinID="button-save" Width="250px" Text="Registrar dados do novo presidente"
                                                            OnClick="btnSubstituir_Click" OnClientClick="javascript: openCustom(); return false;" CausesValidation="false"></asp:Button>
                                                        <%--    OnClick="btnSubstituir_Click" OnClientClick="javascript: $('#registroPrefeito').dialog('open'); return false;" CausesValidation="false"></asp:Button>--%>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Presidentes anteriores do conselho</b></legend>
                                                            <div class="row">
                                                                <div class="cell auto-size">
                                                                    <asp:ListView ID="lstPresidentesAnteriores" runat="server" 
                                                                        DataKeyNames="Id" 
                                                                        OnItemDataBound="lstPresidentesAnteriores_ItemDataBound"
                                                                        OnItemCommand="lstPresidentesAnteriores_ItemCommand">
                                                                        <LayoutTemplate>
                                                                            <table class="table striped border" cellspacing="0"
                                                                                cellpadding="0" border="0" width="100%">
                                                                                <thead class="info">
                                                                                    <tr>
                                                                                        <th style="height: 22px; width: 22px;"></th>
                                                                                        <th width="200">Nome
                                                                                        </th>
                                                                                        <th width="100">CPF
                                                                                        </th>
                                                                                        <th width="150">Período de gestão
                                                                                        </th>
                                                                                        <th width="80">Excluir
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
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Nome") %></a>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "CPF") %></a>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "MandatoInicio") != null 
                                                                                        ?  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "MandatoInicio").ToString()).ToShortDateString() : String.Empty %>
                                                                             - 
                                                                            <%# DataBinder.Eval(Container.DataItem, "MandatoTermino") != null 
                                                                                        ?  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "MandatoTermino").ToString()).ToShortDateString() : String.Empty %>
                                                                                </td>
                                                                                <td class="align-center">
                                                                                    <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                        CommandName="Excluir_Presidente" CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir este registro?');"
                                                                                        ToolTip="Excluir presidente do conselho" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <EmptyDataTemplate>
                                                                            <div align="center" style="width: 100%;">
                                                                                <b class="titulo">Não existe registro de outros presidentes desse conselho no município.</b>
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <div class="row">
                                                <div class="cell">
                                                    <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                        width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                        <tr>
                                                            <td style="padding: 15px 10px 2px 15px">
                                                                <span class="mif-warning mif-2x"></span>
                                                                <b style='color: #000000 !important'>Verifique as inconsistências:</b>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 10px 10px 12px 45px;">
                                                                <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <asp:Button ID="btnSalvar" runat="server" Width="89px" Text="Salvar" SkinID="button-save"
                                                        ValidationGroup="vgCampos" OnClick="btnSalvar_Click"></asp:Button>
                                                    &nbsp;
                                                    <asp:Button ID="btnVoltar" runat="server" Width="89px" Text="Voltar" SkinID="button-save" OnClick="btnVoltar_Click" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="row cell">
                                        <div class="cell full-size">
                                            <asp:ListView ID="lstConselhos" runat="server" DataKeyNames="Id,IdConselho" OnItemDataBound="lstConselhos_ItemDataBound"
                                                OnItemCommand="lstConselhos_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table striped border" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="20" style="height: 22px;"></th>
                                                                <th width="460" align="left">Conselho
                                                                </th>
                                                                <th width="100">Ações
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
                                                            <%#DataBinder.Eval(Container.DataItem, "Nome") %></a>
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="btnVisualizar" runat="server" ImageUrl="~/Styles/Icones/find.png"
                                                                CommandName="Visualizar_Conselho" ToolTip="Visualizar" />
                                                            <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Styles/Icones/edit.png"
                                                                CommandName="Editar_Conselho" ToolTip="Editar" />&nbsp;
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir_Conselho" CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir este registro??');"
                                            ToolTip="Excluir" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro de outros conselhos no município</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>

            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FFundoMunicipal.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfIdConselho" runat="server" Value="0" />
            <asp:HiddenField ID="hdfIdPresidente" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
