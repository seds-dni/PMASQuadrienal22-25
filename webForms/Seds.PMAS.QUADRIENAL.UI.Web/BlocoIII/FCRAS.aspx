<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FCRAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FCRAS" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmCRAS">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            3.5 - Características deste CRAS
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="CRAS">
                                <div class="grid">
                                    <div class="row cells4">
                                        <div class="cell colspan2">
                                            <b>Nome da Unidade:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="120" Width="400"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>IDCRAS:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtIDCRAS" runat="server" MaxLength="11" />
                                            </div>
                                        </div>
                                        <div class="cell" style="text-align: right;">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro21" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row cells5">
                                        <div class="cell colspan2">
                                            <b>Nome do Coordenador:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:TextBox ID="txtCoordenador" runat="server" MaxLength="120"></asp:TextBox>
                                            </div>
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
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlFormacaoAcademica" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormacaoAcademica_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell" id="trOutraFormacao" runat="server" visible="false">
                                            <asp:Label ID="lblEspecifique" Text="Especificar:" runat="server"></asp:Label>
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtOutraAreaFormacao" MaxLength="60" runat="server" CssClass="campoTexto"
                                                    Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:CheckBox ID="chkNaoPossuiCoordenador" runat="server" Text="Não há Coordenador"
                                                AutoPostBack="True" OnCheckedChanged="chkNaoPossuiCoordenador_CheckedChanged" />
                                        </div>
                                    </div>
                                    <uc2:cep ID="cep1" runat="server" />
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
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>E-mail institucional:</b><br />
                                            <div class="input-control email">
                                                <asp:TextBox ID="txtEmailInstitucional" runat="server" Width="308px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Imóvel:</b><br />
                                            <asp:RadioButtonList ID="rblImovel" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Próprio" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Cedido"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Alugado"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trDistritoSP" visible="false">
                                        <div class="cell">
                                            <b>Distrito:</b><br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlDistrito" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Data da implantação:</b><br />
                                            <uc4:data ID="txtDataImplantacao" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Nº de famílias referenciadas:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtCapacidadeAtendimento" runat="server" Width="62px" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Previsão anual do número de famílias atendidas:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtNumeroAtendidos" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Número de trabalhadores deste CRAS:</b>
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <asp:TextBox ID="txtTrabalhadoresRemunerados" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Trabalhadores remunerados
                                        </div>
                                        <div class="cell">
                                            <asp:TextBox ID="txtVoluntarios" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Voluntários
                                        </div>
                                        <div class="cell">
                                            <asp:TextBox ID="txtEstagiarios" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Estagiários
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Segundo a avaliação do órgão gestor municipal, a organização do espaço físico e as instalações deste equipamento:</b><br />
                                            <asp:RadioButtonList ID="rblAvaliacaoLocalExecucao" runat="server"></asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Trabalho realizado por este CRAS</b></legend>
                                                <div class="row">
                                                    <div class="cell">
                                                        O órgão gestor deve informar somente as ações atualmente realizadas pelo CRAS. As que ainda estão em planejamento devem integrar as informações do bloco de Planejamento.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Este CRAS oferta o Serviço de Proteção e Atendimento Integral à Família - PAIF?</b><br />
                                                        <asp:RadioButtonList ID="rblServicoPAIF" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="rblServicoPAIF_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="Sim" />
                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="trJustificativaPAIF" runat="server" visible="true">
                                                    <div class="cell">
                                                        <b>Justifique:</b><br />
                                                        <div class="input-control textarea">
                                                            <asp:TextBox ID="txtJustificativaPAIF" runat="server" TextMode="MultiLine" Height="38px"
                                                                Width="449px" MaxLength="300" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Este CRAS possui Equipe Volante?</b><br />
                                                        <asp:RadioButtonList ID="rblEquipeVolante" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="rblEquipeVolante_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="Sim" />
                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="trEquipeVolante" runat="server" visible="false">
                                                    <div class="cell">
                                                        <fieldset>
                                                            <legend><b class="titulo">RH da Equipe Volante (estes trabalhadores já devem estar inseridos
                                            no RH do CRAS):</b></legend>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Indique o número de trabalhadores, segundo a escolaridade:</b>
                                                                </div>
                                                            </div>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteNivelMedio" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Nível
                                                                Médio
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteNivelSuperior" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Nível
                                                                Superior
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="TextBox1" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Total
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Indique a área de formação dos trabalhadores que possuem nível superior:</b>
                                                                </div>
                                                            </div>
                                                            <div class="row cells5">
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtVolanteServicoSocial" runat="server" Width="48px" MaxLength="4"></asp:TextBox>
                                                                        &nbsp;Serviço Social
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtVolantePsicologia" runat="server" Width="48px" MaxLength="4"
                                                                            AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Psicologia
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtVolantePedagogia" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Pedagogia
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtVolanteSociologia" runat="server" Width="48px" MaxLength="4"
                                                                            AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Sociologia
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtVolanteTerapiaOcupacional" runat="server" Width="48px" MaxLength="4"
                                                                            AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Terapia Ocupacional
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row cells5">
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteAntropologia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Antropologia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteMusicoterapia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Musicoterapia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteEconomia" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    &nbsp;Economia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteDireito" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Direito
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtVolanteEconomiaDomestica" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Economia Doméstica
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Nome dos locais de abrangência territorial de atendimento da Equipe Volante:</b><br />
                                                                    <div class="input-control textarea">
                                                                        <asp:TextBox ID="txtNomeLocaisEquipeVolante" runat="server" TextMode="MultiLine"
                                                                            MaxLength="300" Width="440px" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Quais das seguintes ações são desenvolvidas por este CRAS?</b><br />
                                                        <asp:CheckBoxList ID="lstAcoesSocioAssistenciais" runat="server" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
                                            &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                        </div>
                                    </div>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
