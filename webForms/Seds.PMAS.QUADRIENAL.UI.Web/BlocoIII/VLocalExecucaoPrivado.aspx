<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VLocalExecucaoPrivado.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VLocalExecucaoPrivado" %>

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
                                        <asp:Label ID="lblCNPJ" runat="server"></asp:Label>
                                    </div>
                                    <div class="cell">
                                        <b>Nome da Unidade:</b><br />
                                        <asp:Label ID="lblNome" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Nome do local de execução:</b><br />
                                        <asp:Label ID="lblNomeLocalExecucao" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Nome da pessoa responsável por este equipamento:</b><br />
                                        <asp:Label ID="lblTecnicoResponsavel" runat="server"></asp:Label>
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
                                        <asp:Label ID="lblEmailInstitucional" runat="server"></asp:Label>
                                    </div>
                                    <div class="cell" align="left">
                                        <b>Imóvel:</b><br />
                                        <asp:Label ID="lblImovel" runat="server" />
                                    </div>
                                </div>
                                <div class="row" runat="server" id="trDistritoSP" visible="false">
                                    <div class="cell">
                                        <b>Distrito:</b><br />
                                        <asp:Label ID="lblDistrito" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Segundo a avaliação do órgão gestor municipal, a organização do espaço físico e as instalações deste equipamento:</b><br />
                                        <asp:Label ID="lblAvaliacaoLocalExecucao" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Desativação deste local:</b></legend>
                                        <div class="row">
                                            <div class="cell">
                                                <b>Data da desativação do registro: </b>
                                                <asp:Label ID="lblDataExclusaoRegistro" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <b>A desativação deste local de execução é devida a:</b><br />
                                                <asp:Label ID="lblMotivoExclusao" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row" id="trDataEncerramento" runat="server" visible="false">
                                            <div class="cell">
                                                <b>
                                                    <asp:Label ID="lblDescEncerramentoServico" runat="server"></asp:Label>&nbsp;</b><asp:Label ID="lblDataEncerramentoServico" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row" id="trMotivoEncerramento" runat="server" visible="false">
                                            <div class="cell">
                                                <b>O encerramento das atividades deste Local de execução deve-se a:</b><br />
                                                <asp:Label ID="lblMotivoEncerramento" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row" id="trDetalhamento" runat="server" visible="false">
                                            <div class="cell">
                                                <b>
                                                    <asp:Label ID="lblDescDetalhamento" runat="server"></asp:Label></b><br />
                                                <asp:Label ID="lblDetalhamento" runat="server"></asp:Label>
                                                <br />
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell" align="center">
                                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
