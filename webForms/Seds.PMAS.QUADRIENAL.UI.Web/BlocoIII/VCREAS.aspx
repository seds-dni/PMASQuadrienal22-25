<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VCREAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VCREAS" %>

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
                            Características deste CREAS
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="CREAS">
                                <div class="grid">
                                    <div class="row cells4">
                                        <div class="cell colspan2">
                                            <b>Nome da Unidade:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:Label ID="txtNome" runat="server" MaxLength="120" Width="400"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>IDCREAS:</b><br />
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
                                            <b><asp:Label ID="lblEspecifique" Text="Especificar:" runat="server"></asp:Label></b>
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
                                            <b>Data do encerramento das atividades deste CREAS:</b><br />
                                            <asp:Label ID="lblDataExclusaoRegistro" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>A desativação deste CREAS é devida a:</b><br />
                                            <asp:Label ID="lblMotivoExclusao" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" id="trMotivoEncerramento" runat="server" visible="false">
                                        <div class="cell">
                                            <b>O encerramento das atividades deste CREAS deve-se a:</b><br />
                                            <asp:Label ID="lblMotivoEncerramento" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" id="trDetalhamento" runat="server" visible="false">
                                        <div class="cell">
                                            <b>Detalhamento sobre o motivo do encerramento das atividades deste CREAS:</b><br />
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
                                            <b>Número de trabalhadores deste CREAS</b>
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
                                            <b>Este CREAS atende usuários de outro(s) município(s)?</b><br />
                                            <asp:Label ID="lblAtendeUsuarios" runat="server" />
                                        </div>
                                    </div>
                                    <div id="lstAtendidos" class="row" runat="server" visible="false">
                                        <div class="cell">
                                            <asp:ListView ID="lstMunicipiosAtendidos" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="30%">Município
                                                                </th>
                                                                <th width="15%">Número de Atendidos
                                                                </th>
                                                                <th width="40%">Tipo de Atedimento
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
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "NumeroAtendidos") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TipoAtendimento.TipoAtendimento") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existem registros de municípios atendidos por este CREAS</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Trabalho realizado por este CREAS</b></legend>
                                                <div class="row">
                                                    <div class="cell">
                                                        O órgão gestor deve informar somente as ações atualmente realizadas pelo CREAS. As que ainda estão em planejamento devem integrar as informações do bloco de Planejamento.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Este CREAS oferta o Serviço de Proteção e Atendimento Especializado a Famílias e Indivíduos - PAEFI?</b><br />
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
                                                        <b>Quais atividades são desenvolvidas por este CREAS?
                                                        </b>
                                                        <br />
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
