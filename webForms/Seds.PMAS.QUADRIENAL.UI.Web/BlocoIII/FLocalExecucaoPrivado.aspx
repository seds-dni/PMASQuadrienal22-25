<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FLocalExecucaoPrivado.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FLocalExecucaoPrivado" %>

<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadePrivada">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            3.13 - Dados deste local de execução
                           <a href="#" runat="server" id="linkAlteracoesQuadro38" visible="false">
                               <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                           </a>&nbsp;
                             <span class="mif-organization icon"></span>
                        </div>
                    </div>
                    <div class="content">
                        <div class="formInput" data-text="Unidade Privada">
                            <div class="grid">
                                <div class="row cells2">
                                    <div class="cell">
                                        <b>CNPJ:</b><br />
                                        <uc1:cnpj ID="txtCNPJ" runat="server" />
                                    </div>
                                    <div class="cell">
                                        <b>Nome da Unidade:</b><br />
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtNome" runat="server" Width="319px" MaxLength="120"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Nome do local de execução:</b><br />
                                        <div class="input-control text mid-size">
                                            <asp:TextBox ID="txtNomeLocalExecucao" runat="server" MaxLength="120"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Nome da pessoa responsável por este equipamento:</b><br />
                                        <div class="input-control text mid-size">
                                            <asp:TextBox ID="txtTecnicoResponsavel" runat="server" MaxLength="120"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkNaoPossuiTecnico" runat="server" Text="Não há responsável pelo equipamento"
                                            AutoPostBack="True" OnCheckedChanged="chkNaoPossuiTecnico_CheckedChanged" />
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
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtEmailInstitucional" runat="server" Width="308px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="cell" align="left">
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
                                        <asp:DropDownList ID="ddlDistrito" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Capacidade mensal de pessoas atendidas neste local:</b><br />
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtNumeroAtendidos" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Segundo a avaliação do órgão gestor municipal, a organização do espaço físico e as instalações deste equipamento:</b><br />
                                        <asp:RadioButtonList ID="rblAvaliacaoLocalExecucao" runat="server"></asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="cell" align="center">
                                    <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                        OnClick="btnSalvar_Click"></asp:Button>
                                    &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                    width="100%" class="bg-yellow  fg-black" style="border: 1px dashed blue" align="center" border="0">
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
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
