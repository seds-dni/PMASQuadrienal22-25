<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FFundoMunicipal.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoI.FFundoMunicipal" %>

<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc2" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <%-- <script type="text/javascript">
         
        function CalculateTotal() {
            var txtFMAS = document.getElementById('<%=txtFMAS.ClientID%>').value;
            var txtFNAS = document.getElementById('<%=txtFNAS.ClientID%>').value;
            var txtFEAS = document.getElementById('<%=txtFEAS.ClientID%>').value;
            var valores = [txtFMAS, txtFEAS, txtFNAS];
            PageMethods.CalcularValores(valores, function (val) {
                document.getElementById('<%=txtTotalFMAS.ClientID%>').value = val;
                document.getElementById('<%=hidTotalRecursos.ClientID%>').value = val;
            });
        }
    </script>--%>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <input type="hidden" runat="server" id="hidValorFMAS" value="0,00" />
            <input type="hidden" runat="server" id="hidValorFEAS" value="0,00" />
            <input type="hidden" runat="server" id="hidValorFNAS" value="0,00" />
            <input type="hidden" runat="server" id="hidValorCusteio" value="0,00" />
            <input type="hidden" runat="server" id="hidTotalRecursos" value="0,00" />
            <form name="frmOrgaoGestor">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame" id="fraFMAS" runat="server">
                        <div class="heading">
                            1.6 - Identificação do Fundo Municipal de Assistência Social
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Fundo Municipal">
                                <div class="grid">
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>CNPJ:</b><br />
                                            <uc1:cnpj ID="txtCNPJ" runat="server" />
                                        </div>
                                        <div class="cell" style="text-align:right;">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro9" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Nº da Lei de criação do FMAS</b><br />
                                            <div class="input-control text mid-size">
                                                <asp:TextBox ID="txtNumeroLeiCriacao" runat="server" MaxLength="5" Width="72px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLeiCriacao"
                                                    runat="server" TargetControlID="txtNumeroLeiCriacao" FilterType="Numbers" />
                                                /
                                    <asp:TextBox ID="txtAnoLeiCriacao" runat="server" MaxLength="2" Width="42px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLeiCriacao"
                                                    runat="server" TargetControlID="txtAnoLeiCriacao" FilterType="Numbers" />
                                                (Ex: 129/11)
                                            </div>

                                        </div>
                                        <div class="cell">
                                            <b>Data de publicação da Lei:</b><br />
                                            <uc4:data ID="txtDataPublicacaoLei" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>O FMAS já está legalmente regulamentado?</b><br />
                                            <asp:RadioButtonList ID="rblLeiRegulamenta" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                Width="125px" OnSelectedIndexChanged="rblLeiRegulamenta_SelectedIndexChanged">
                                                <asp:ListItem Text="Sim" Value="1" />
                                                <asp:ListItem Text="Não" Value="0" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="cell" id="trLeiRegulamenta1" runat="server" visible="false">
                                            <b>Decreto/Ano de regulamentação:</b><br />
                                            <asp:TextBox ID="txtNumeroDecreto" runat="server" MaxLength="5" Width="70px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroDecreto"
                                                runat="server" TargetControlID="txtNumeroDecreto" FilterType="Numbers" />
                                            /
                                                <asp:TextBox ID="txtAnoDecreto" runat="server" MaxLength="2" Width="42px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoDecreto" runat="server"
                                                TargetControlID="txtAnoDecreto" FilterType="Numbers" />
                                            (Ex: 129/11)
                                        </div>
                                        <div class="cell" id="trLeiRegulamenta2" runat="server" visible="false">
                                            <b>Data do decreto que regulamenta o FMAS:</b><br />
                                            <uc4:data ID="txtdatadecreto" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cell">
                                        <div class="cell">
                                            <b>O FMAS constitui-se como Unidade Orçamentária?</b><br />
                                            <asp:RadioButtonList ID="rblUnidade" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Sim" Value="1" />
                                                <asp:ListItem Text="Não" Value="0" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>
                                                <asp:Label ID="lblHouveAlteracao" runat="server">Houve alteração na Lei de criação ?</asp:Label></b><br />
                                            <asp:RadioButtonList ID="rblAlteracaoLei" runat="server" CssClass="RadioButton" RepeatDirection="Horizontal"
                                                AutoPostBack="True" Height="16px"
                                                OnSelectedIndexChanged="rblAlteracaoLei_SelectedIndexChanged">
                                                <asp:ListItem Text="Sim" Value="1" />
                                                <asp:ListItem Text="Não" Value="0" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="cell" id="tdLeiAlterada" runat="server" visible="false">
                                            <b>
                                                <asp:Label ID="lblLeiAlteracao" runat="server">&nbsp;&nbsp;Nº da Lei de Alteração:</asp:Label></b><br />
                                            <asp:TextBox ID="txtNumeroLei" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLei" runat="server"
                                                TargetControlID="txtNumeroLei" FilterType="Numbers" />
                                            /
                                                <asp:TextBox ID="txtAnoLei" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLei" runat="server"
                                                TargetControlID="txtAnoLei" FilterType="Numbers" />
                                            (Ex: 129/11)
                                        </div>
                                        <div class="cell" id="tdDataLeiAlterada" runat="server" visible="false">
                                            <b>
                                                <asp:Label ID="lblDataAlteracao" runat="server">Data de publicação da Lei: </asp:Label></b><br />
                                            <uc4:data ID="txtDataAlteracao" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cell" id="trAlerta" runat="server" visible="false">
                                        <div class="cell titulo">
                                            <b style="color: red;">Os fundos públicos que se encontram inscritos no CNPJ na condição de filial do órgão
                                        público a que estejam vinculados deverão providenciar nova inscrição nesse cadastro,
                                        na condição de matriz, com a natureza jurídica 120-1 (Fundo Público). Instrução
                                        Normativa nº 1143 de 01/04/2011 / RFB - Receita Federal do Brasil.</b>
                                        </div>
                                    </div>
                                    <div class="row cell">
                                        <asp:Button ID="btnSalvar" TabIndex="16" runat="server" Width="89px" Text="Salvar"
                                            SkinID="button-save" OnClick="btnSalvar_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="frame" id="fraGestor" runat="server">
                        <div class="heading">
                            <a href="#" runat="server" id="A1" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                            </a>
                            1.7 - Identificação do gestor do Fundo Municipal de Assistência Social
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Gestor do FMAS">
                                <div class="grid">
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Nome</b><br />
                                            <div class="input-control text mid-size">
                                                <asp:TextBox ID="txtnome" runat="server" MaxLength="60"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>CPF</b><br />
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
                                                <asp:DropDownList ID="ddlUFEmissor" Height="33px" runat="server">
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

                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Contato Institucional</b></legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Telefone Fixo:</b><br />
                                                    <uc3:telefone ID="txtTelefone" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Telefone Celular:</b><br />
                                                    <uc5:celular ID="txtCelular" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>E-mail:</b><br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtEmailGestor" runat="server" MaxLength="60" CausesValidation="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Período de gestão</b></legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Decreto / Portaria de nomeação:</b><br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtPortaria" runat="server" MaxLength="8" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <b>Data de publicação do Decreto/ Portaria:</b><br />
                                                    <div class="input-control text">
                                                        <uc4:data ID="txtDecretoGestor" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <b>Data de início:</b><br />
                                                    <uc4:data ID="txtdata" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Data de término:</b><br />
                                                    <uc4:data ID="txtDataTerminoGestao" runat="server" Enabled="false" />
                                                </div>
                                                <div class="cell">
                                                    <br />
                                                    <asp:Button ID="btnSalvarTerminoGestao" runat="server" Text="Finalizar" SkinID="button-save" Enabled="false" OnClick="btnSalvarTerminoGestao_Click" CausesValidation="false"></asp:Button>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Vínculo com a Assistência Social</b></legend>
                                        <div class="row">
                                            <div class="cell">
                                                <b>O gestor do Fundo Municipal de Assistência Social é:</b><br />
                                                <br />
                                                <asp:RadioButtonList ID="rblVinculo" runat="server" RepeatColumns="2" Width="60%" OnSelectedIndexChanged="rblVinculo_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row" id="tralertaGestor" runat="server" visible="false">
                                            <div class="cell titulo">
                                                <b style="color: red;">De acordo com a Resolução CNAS nº 33 de 12 de dezembro de 2012 (NOB-SUAS), em seu Art. 48: Cabe ao órgão da administração pública responsável pela coordenação da Política de Assistência Social na União, nos Estados, no Distrito Federal e nos Municípios gerir o Fundo de Assistência Social, sob orientação e controle dos respectivos Conselhos de Assistência Social.</b>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvarGestor" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvarGestor_Click"
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
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Gestores do fundo municipal de assistência Social anteriores</b></legend>
                                        <div class="row">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro8" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>&nbsp;
                                        </div>
                                        <div class="row">
                                            <div class="cell">
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
                                                                <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "InicioGestao")).ToString("dd/MM/yyyy") %>
                                        -
                                        <%#DataBinder.Eval(Container.DataItem, "TerminoGestao") == null ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TerminoGestao")).ToString("dd/MM/yyyy") %>
                                                            </td>
                                                            <td align="center">
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
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <br />
            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="100%" align="center" border="0">
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
                        <a href="FConselhos.aspx">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfIdGestor" runat="server" Value="0" />
            <asp:HiddenField ID="hdfIdFMAS" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
