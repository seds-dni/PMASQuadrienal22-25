<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VCRAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VCRAS" %>

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
                            Características deste CRAS
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="CRAS">
                                <div class="grid">
                                    <div class="row cells4">
                                        <div class="cell colspan2">
                                            <b>Nome da Unidade:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:Label ID="txtNome" runat="server" MaxLength="120" Width="400"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>IDCRAS:</b><br />
                                            <div class="input-control text">
                                                <asp:Label ID="txtIDCRAS" runat="server" MaxLength="11" />
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
                                                <asp:Label ID="txtCoordenador" runat="server" MaxLength="120"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="cell" id="tdEscolaridade" runat="server">
                                            <b>Escolaridade:</b><br />
                                            <div class="input-control select">
                                                <asp:Label ID="txtEscolaridade" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell" id="tdFormacaoAcademica" runat="server" visible="false">
                                            <b>Área de formação acadêmica:</b><br />
                                            <div class="input-control select">
                                                <asp:Label ID="txtFormacaoAcademica" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell" id="trOutraFormacao" runat="server" visible="false">
                                            <asp:Label ID="lblEspecifique" Text="Especificar:" runat="server"></asp:Label>
                                            <div class="input-control text">
                                                <asp:Label ID="txtOutraAreaFormacao" MaxLength="60" runat="server" CssClass="campoTexto" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>CEP:</b><br />
                                            <asp:Label ID="lblCep" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Endereço:</b><br />
                                            <asp:Label ID="lblLogradouro" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>Número:</b><br />
                                            <asp:Label ID="lblNumero" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Complemento:</b><br />
                                            <asp:Label ID="lblComplemento" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Bairro:</b><br />
                                            <asp:Label ID="lblBairro" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Cidade:</b><br />
                                            <asp:Label ID="lblCidade" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Telefone fixo:</b><br />
                                            <asp:Label ID="lblTelefone" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Telefone celular:</b><br />
                                            <asp:Label ID="lblCelular" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>E-mail institucional:</b><br />
                                            <asp:Label ID="txtEmailInstitucional" runat="server"></asp:Label>
                                        </div>
                                        <div class="cell">
                                            <b>Imóvel:</b><br />
                                            <asp:Label ID="lblImovel" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trDistritoSP" visible="false">
                                        <div class="cell">
                                            <b>Distrito:</b><br />
                                            <div class="input-control select">
                                                <asp:Label ID="lblDistrito" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Data da implantação:</b><br />
                                            <asp:Label ID="txtDataImplantacao" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Data do encerramento das atividades deste CRAS:</b><br />
                                            <asp:Label ID="lblDataExclusaoRegistro" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>A desativação deste CRAS é devida a:</b><br />
                                            <asp:Label ID="lblMotivoExclusao" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" id="trMotivoEncerramento" runat="server" visible="false">
                                        <div class="cell">
                                            <b>O encerramento das atividades deste CRAS deve-se a:</b><br />
                                            <asp:Label ID="lblMotivoEncerramento" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" id="trDetalhamento" runat="server" visible="false">
                                        <div class="cell">
                                            <b>Detalhamento sobre o motivo do encerramento das atividades deste CRAS:</b><br />
                                            <asp:Label ID="lblDetalhamentoEncerramento" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Nº de famílias referenciadas:</b><br />
                                            <asp:Label ID="txtCapacidadeAtendimento" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Previsão anual do número de famílias atendidas:</b><br />
                                            <asp:Label ID="txtNumeroAtendidos" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Número de trabalhadores deste CRAS</b>
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell">
                                            <b>Trabalhadores remunerados:</b>&nbsp;<asp:Label ID="txtTrabalhadoresRemunerados" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Voluntários</b>&nbsp;
                                            <asp:Label ID="txtVoluntarios" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Estagiários</b>&nbsp;<asp:Label ID="txtEstagiarios" runat="server" Width="48px" MaxLength="4" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Segundo a avaliação do órgão gestor municipal, a organização do espaço físico e as instalações deste equipamento:</b><br />
                                            <asp:Label ID="lblAvaliacaoLocalExecucao" runat="server" />
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
                                                        <asp:Label ID="lblServicoPAIF" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row" id="trJustificativaPAIF" runat="server" visible="true">
                                                    <div class="cell">
                                                        <b>Justifique:</b><br />
                                                        <asp:Label ID="txtJustificativaPAIF" runat="server" TextMode="MultiLine" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Este CRAS possui Equipe Volante?</b><br />
                                                        <asp:Label ID="rblEquipeVolante" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row" id="trEquipeVolante" runat="server" visible="false">
                                                    <div class="cell">
                                                        <fieldset>
                                                            <legend><b class="titulo">RH da Equipe Volante (estes trabalhadores já devem estar inseridos no RH do CRAS):</b></legend>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Indique o número de trabalhadores, segundo a escolaridade:</b>
                                                                </div>
                                                            </div>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteNivelMedio" runat="server" Width="48px" MaxLength="4"></asp:Label>&nbsp;Nível
                                                                Médio
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteNivelSuperior" runat="server" Width="48px" MaxLength="4"></asp:Label>&nbsp;Nível
                                                                Superior
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:Label ID="TextBox1" runat="server" Width="48px" MaxLength="4"></asp:Label>&nbsp;Total
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
                                                                        <asp:Label ID="txtVolanteServicoSocial" runat="server" Width="48px" MaxLength="4"></asp:Label>&nbsp;Serviço Social
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:Label ID="txtVolantePsicologia" runat="server" Width="48px" MaxLength="4"
                                                                            AutoCompleteType="Disabled"></asp:Label>&nbsp;Psicologia
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:Label ID="txtVolantePedagogia" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:Label>&nbsp;Pedagogia
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:Label ID="txtVolanteSociologia" runat="server" Width="48px" MaxLength="4"
                                                                            AutoCompleteType="Disabled"></asp:Label>&nbsp;Sociologia
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <div class="input-control text">
                                                                        <asp:Label ID="txtVolanteTerapiaOcupacional" runat="server" Width="48px" MaxLength="4"
                                                                            AutoCompleteType="Disabled"></asp:Label>&nbsp;Terapia Ocupacional
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row cells5">
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteAntropologia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:Label>&nbsp;Antropologia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteMusicoterapia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:Label>&nbsp;Musicoterapia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteEconomia" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:Label>&nbsp;Economia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteDireito" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:Label>&nbsp;Direito
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:Label ID="txtVolanteEconomiaDomestica" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:Label>&nbsp;Economia Doméstica
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Nome dos locais de abrangência territorial de atendimento da Equipe Volante:</b><br />
                                                                    <div class="input-control textarea">
                                                                        <asp:Label ID="txtNomeLocaisEquipeVolante" runat="server" TextMode="MultiLine"
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
                                                        <asp:ListView ID="lstAcoesSocioAssistenciais" runat="server">
                                                            <LayoutTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" class="table ">
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server">
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="no-border-top"><%#DataBinder.Eval(Container.DataItem, "Nome") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
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
