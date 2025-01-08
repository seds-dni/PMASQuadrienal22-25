<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FConselhoMunicipal.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVIII.FConselhoMunicipal" %>

<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc6" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc7" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmPrefeitura">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame active">
                        <div class="heading">
                            8.1 - Identificação do Conselho Municipal de Assistência Social
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="CMAS">
                                <div class="grid">
                                    <div class="row" id="alteracoesQuadro" runat="server" visible="false">
                                        <div class="cell" align="right">
                                            <a style="float: right; margin-right: 5%;" href="#" runat="server" id="linkAlteracoesQuadro72" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>&nbsp;
                                        </div>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Lei de criação do CMAS:</b></legend>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <b>Nº da Lei:</b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtNumeroLeiCriacao" runat="server" Width="60px" MaxLength="5"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLeiCriacao"
                                                            runat="server" TargetControlID="txtNumeroLeiCriacao" FilterType="Numbers" />
                                                        /
                                                    <asp:TextBox ID="txtAnoLeiCriacao" runat="server" Width="30px" MaxLength="2"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLeiCriacao"
                                                            runat="server" TargetControlID="txtAnoLeiCriacao" FilterType="Numbers" />
                                                        (Ex: 129/11)
                                                    </div>
                                                </div>
                                                <div class="cell" align="left">
                                                    <b>Data de publicação da Lei:</b><br />
                                                    <uc4:data ID="txtDtCriacao" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <b>
                                                        <asp:Label ID="lblHouveAlteracao" runat="server">Houve alteração na Lei de criação ?</asp:Label></b><br />
                                                    <asp:RadioButtonList ID="rblAlteracaoLei" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" Height="16px" OnSelectedIndexChanged="rblAlteracaoLei_SelectedIndexChanged">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="cell" id="tdLeiAlterada" runat="server" visible="false">
                                                    <b>
                                                        <asp:Label ID="lblLeiAlteracao" runat="server">&nbsp;&nbsp;Nº da Lei de Alteração:</asp:Label></b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtNumeroLei" runat="server" Width="113px" MaxLength="20"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="cell" id="tdDataLeiAlterada" runat="server" visible="false">
                                                    <b>
                                                        <asp:Label ID="lblDataAlteracao" runat="server">Data de publicação da Lei: </asp:Label></b><br />
                                                    <uc4:data ID="txtDataAlteracao" runat="server" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Composição do Conselho:</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Nº de conselheiros titulares que são representantes governamentais:</b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtNumeroRepresentateGovernamental" runat="server" MaxLength="5" style="width:80px"/>
                                                    </div>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroRepresentateGovernamental"
                                                        runat="server" TargetControlID="txtNumeroRepresentateGovernamental" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Nº de conselheiros titulares que são representantes da sociedade civil:</b><br />
                                                    <asp:TextBox ID="txtNumeroRepresentanteSociedadeCivil" runat="server" MaxLength="5" style="width:80px"/>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroRepresentanteSociedadeCivil"
                                                        runat="server" TargetControlID="txtNumeroRepresentanteSociedadeCivil" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Nº de Trabalhadores:</b><br />
                                                    <asp:TextBox ID="txtTrabalhadores" runat="server" MaxLength="5" style="width:80px"/>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                        runat="server" TargetControlID="txtTrabalhadores" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Nº de Usuários:</b><br />
                                                    <asp:TextBox ID="txtUsuarios" runat="server" MaxLength="5" style="width:80px"/>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                        runat="server" TargetControlID="txtUsuarios" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Nº de Entidades:</b><br />
                                                    <asp:TextBox ID="txtEntidades" runat="server" MaxLength="5" style="width:80px"/>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                                        runat="server" TargetControlID="txtEntidades" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>A Secretaria Executiva do CMAS já foi estruturada?</b><br />
                                                    <asp:RadioButtonList ID="rblSecretariaExecutiva" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" OnSelectedIndexChanged="rblSecretariaExecutiva_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                 <div class="row">
                                                <div class="cell" id="trNomeSecretario" runat="server">
                                                    <b>Nome do(a) Secretário(a)</b><br /> 
                                                    <asp:TextBox id="txtNomeSecretario" runat="server" Width="30%" />
                                                </div>
                                            </div>
                                                </div>
                                            <div class="row" id="trTecnicoSecretariaExecutiva" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Quantos trabalhadores fazem parte do corpo técnico da Secretaria Executiva do CMAS?</b>
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtTecnicoSecretariaExecutiva" runat="server" MaxLength="4" />
                                                    </div>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtTecnicoSecretariaExecutiva"
                                                        runat="server" TargetControlID="txtTecnicoSecretariaExecutiva" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row" id="trAdministrativoSecretariaExecutiva" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Quantos trabalhadores fazem parte do corpo administrativo da Secretaria Executiva
                                                        do CMAS?</b>
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtAdministrativoSecretariaExecutiva" runat="server" MaxLength="4" />
                                                    </div>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAdministrativoSecretariaExecutiva"
                                                        runat="server" TargetControlID="txtAdministrativoSecretariaExecutiva" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row" id="trConselheiros" runat="server" >
                                                <div class="cell">
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Indique a área de formação dos trabalhadores da secretaria executiva do conselho que possuem nível superior:</b>
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorServicoSocial" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Serviço Social
                                                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorServicoSocial"
                                                                                     runat="server" TargetControlID="txtSuperiorServicoSocial" FilterType="Numbers" />
                                                        </div>
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorAdministracao" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Administração
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorAdministracao"
                                                                                    runat="server" TargetControlID="txtSuperiorAdministracao" FilterType="Numbers" />
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorPsicologia" runat="server" CssClass="campoTexto" Width="60px"
                                                                MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Psicologia
                                                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorPsicologia"
                                                                                     runat="server" TargetControlID="txtSuperiorPsicologia" FilterType="Numbers" />
                                                        </div>
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorAntropologia" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Antropologia
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorAntropologia"
                                                                                    runat="server" TargetControlID="txtSuperiorAntropologia" FilterType="Numbers" />
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorPedagogia" runat="server" CssClass="campoTexto" Width="60px"
                                                                MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Pedagogia
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorPedagogia"
                                                                runat="server" TargetControlID="txtSuperiorPedagogia" FilterType="Numbers" />
                                                        </div>
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorContabilidade" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Contabilidade
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorContabilidade"
                                                                runat="server" TargetControlID="txtSuperiorContabilidade" FilterType="Numbers" />
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSociologia" runat="server" CssClass="campoTexto" Width="60px"
                                                                MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Sociologia
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSociologia" runat="server"
                                                                TargetControlID="txtSociologia" FilterType="Numbers" />
                                                        </div>
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorEconomia" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Economia
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorEconomia"
                                                                runat="server" TargetControlID="txtSuperiorEconomia" FilterType="Numbers" />
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtDireito" runat="server" CssClass="campoTexto" Width="60px" MaxLength="4"
                                                                AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Direito
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDireito" runat="server"
                                                                TargetControlID="txtDireito" FilterType="Numbers" />
                                                        </div>
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorTerapiaOcupacional" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Terapia
                                                            Ocupacional
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorTerapiaOcupacional"
                                                                runat="server" TargetControlID="txtSuperiorTerapiaOcupacional" FilterType="Numbers" />
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtSuperiorEconomiaDomestica" runat="server" CssClass="campoTexto"
                                                                Width="60px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Economia
                                                            Doméstica
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorEconomiaDomestica"
                                                                runat="server" TargetControlID="txtSuperiorEconomiaDomestica" FilterType="Numbers" />
                                                        </div>
                                                        <div class="cell">
                                                            <asp:TextBox ID="txtMusicoterapia" runat="server" Width="60px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Musicoterapia
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtMusicoterapia"
                                                                runat="server" TargetControlID="txtMusicoterapia" FilterType="Numbers" />
                                                        </div>
                                                    </div>
                                                </div>                                                
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Endereço do CMAS:</b></legend>
                                            <div class="row" width="100%">
                                                <uc2:cep ID="cep1" runat="server" />
                                            </div>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Telefone fixo:</b><br />
                                                    <uc3:telefone ID="txtTelefone" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Telefone celular:</b><br />
                                                    <uc5:celular ID="txtCelular" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>E-mail:</b><br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtEmail" runat="server" Width="291px" MaxLength="60"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" Width="89px" Text="Salvar" SkinID="button-save"
                                                OnClick="btnSalvar_Click" ValidationGroup="vgCampos"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="frame" id="frmPresidenteCMAS" runat="server">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro2" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                            </a>
                            8.2 - Identificação do Presidente do CMAS
                        <span class="mif-home icon"></span><a style="float: right; margin-right: 5%;" href="#" runat="server" id="A1" visible="false">
                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                        </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="presidente do CMAS">
                                <div class="grid">
                                    <div class="row cells3">
                                        <div class="cell colspan2">
                                            <b>Nome:</b><br />
                                            <div class="input-control select full-size">
                                                <asp:DropDownList ID="ddlUsuario" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>CPF:</b><br />
                                            <div class="input-control text">
                                                <uc7:cpf ID="txtCPF" runat="server" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells4">
                                        <div class="cell">
                                            <div class="input-control">
                                                <b>RG:</b><br />
                                                <uc6:rg ID="txtRG" runat="server" Enabled="false" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Data da emissão:</b><br />
                                            <uc4:data ID="txtDataEmissao" runat="server" Enabled="false" />
                                        </div>
                                        <div class="cell">
                                            <b>Sigla do órgão emissor:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtOrgEmissor" runat="server" Width="70px" MaxLength="6" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>U.F.:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlUFEmissor" Height="33px" runat="server" Enabled="false">
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
                                    <div class="row" id="trContatoPresidenteCMAS">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Contato</b></legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Telefone fixo:</b><br />
                                                    <uc3:telefone ID="txtTelefonePresidenteCMAS" runat="server" Enabled="false" />
                                                </div>
                                                <div class="cell">
                                                    <b>Telefone celular:</b><br />
                                                    <uc5:celular ID="txtCelularPresidenteCMAS" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>E-mail:</b><br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtEmailPresidenteCMAS" runat="server" Width="291px" MaxLength="60" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Período de mandato</b></legend>

                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Decreto / Portaria de nomeação:</b><br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtNumeroDecreto" runat="server" MaxLength="8" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <b>Data de publicação do Decreto/ Portaria:</b><br />
                                                    <div class="input-control text">
                                                        <uc4:data ID="txtDataDecreto" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Data de início do mandato:</b><br />
                                                    <uc4:data ID="txtDataInicio" runat="server" Enabled="false" />
                                                </div>
                                                <div class="cell">
                                                    <b>Data de término do mandato:</b><br />
                                                    <uc4:data ID="txtDataTermino" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="row cells3">
                                                    <div class="cell">
                                                        <asp:Button ID="btnSalvarPresidente" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvarPresidente_Click" Enabled="false"></asp:Button>
                                                    </div>
                                                    <div class="cell">
                                                        <asp:Button ID="btnEditar" runat="server" SkinID="button-save" Text="Atualizar dados do presidente" OnClick="btnEditar_Click"
                                                            CausesValidation="false" Enabled="false"></asp:Button>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button ID="btnSubstituir" runat="server" Width="250px" Text="Registrar dados do novo presidente" OnClick="btnSubstituir_Click" OnClientClick="openCustom(); return false;" CausesValidation="false" Enabled="false"></asp:Button>
                                                        <%--   <asp:Button ID="btnSubstituir" runat="server" Width="200px" Text="Substituir" OnClick="btnSubstituir_Click" OnClientClick="javascript: $('#registroCMAS').dialog('open'); return false;" CausesValidation="false"></asp:Button>--%>
                                                    </div>
                                                </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Presidentes anteriores:</b></legend>
                                            <asp:ListView ID="lstPresidentesAnteriores" runat="server" OnItemCommand="lstPresidentesAnteriores_ItemCommand"
                                                DataKeyNames="Id" OnItemDataBound="lstPresidentesAnteriores_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr class="ui-jqgrid-labels">
                                                                <th width="20" style="height: 22px;"></th>
                                                                <th width="300">Nome
                                                                </th>
                                                                <th width="180">Período de gestão
                                                                </th>
                                                                <th width="100">Excluir
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
                                                            <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataInicioMandato")).ToString("dd/MM/yyyy") %>
                                                        -
                                                        <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataTerminoMandato")).ToString("dd/MM/yyyy") %>
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                CommandName="Excluir_Presidente" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o presidente anterior?');" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro de outros presidentes neste período</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>

            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">&nbsp;
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FParecerConselhoMunicipal.aspx">Próximo
                            <span class="mif-arrow-right" /></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- <div id="registroCMAS" title="Confirmação de data">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>A data de término do mandato já foi atualizada?</p>
    </div>--%>
    <script type="text/javascript">
        function openCustom() {
            $.Dialog({
                title: "<span class='mif-warning mif-ani-flash mif-ani-slow'></span>  Confirmação de data",
                content: "A data de término do mandato já foi atualizada?",
                actions: [
                    {
                        title: "Sim",
                        cls: "fg-white bg-orange",
                        onclick: function (el) {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSubstituir))%>;
                            $(el).data('dialog').close();
                        }
                    },
                    {
                        title: "Não",
                        cls: "js-dialog-close fg-white bg-orange"
                    }
                ],
                options: {
                    modal: false,
                    overlay: true,
                    overlayColor: 'op-dark',
                    overlayClickClose: false,
                    type: 'warning', // success, alert, warning, info
                    place: 'center', // center, top-left, top-center, top-right, center-left, center-right, bottom-left, bottom-center, bottom-right
                    position: 'default',
                    content: false,
                    hide: false,
                    width: '500',
                    height: '158',
                    background: 'default',
                    color: 'default',
                    //closeButton: false,
                    //windowsStyle: false,
                    //show: false,
                    //href: false,
                    contentType: 'default', // video
                    closeAction: true,
                    closeElement: ".js-dialog-close",
                }
            });
            }

    </script>

</asp:Content>
