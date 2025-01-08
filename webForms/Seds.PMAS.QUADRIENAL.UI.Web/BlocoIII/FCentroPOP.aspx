<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FCentroPOP.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FCentroPOP" %>

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
                            <a href="#" runat="server" id="linkAlteracoesQuadro32" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;3.9 - Características deste Centro POP</b>
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Centro POP">
                                <div class="grid">
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Nome da Unidade:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="120" Width="400px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>IDCREAS:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtIDCREAS" runat="server" MaxLength="11" />
                                            </div>
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
                                        <div class="cell" id="tdOutraFormacao" runat="server" visible="false">
                                            <b>Especificar</b><br />
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
                                            <div class="input-control text">
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
                                            <b>Capacidade de atendimento anual:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtNumeroAtendidos" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroAtendidos"
                                                    runat="server" TargetControlID="txtNumeroAtendidos" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Previsão anual do número de pessoas atendidas:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtCapacidadeAtendimento" runat="server" Width="62px" MaxLength="6"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtCapacidadeAtendimento"
                                                    runat="server" TargetControlID="txtCapacidadeAtendimento" FilterType="Numbers" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Número de trabalhadores deste Centro POP:</b>
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
                                            <b>Este Centro POP atende usuários de outro(s) município(s)?</b><br />
                                            <asp:RadioButtonList ID="rbAtendeUsuarios" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" OnSelectedIndexChanged="rbAtendeUsuarios_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row cells3" visible="false" runat="server" id="trMunicipios">
                                        <div class="cell">
                                            <b>Municípios disponíveis:</b><br>
                                            <div class="input-control select mid-size">
                                                <asp:DropDownList ID="ddlMunicipioConveniado" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Nº de atendidos de outros Municípios:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtQuantosAtendidos" TabIndex="2" runat="server" Width="58px" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Tipo de Atendimento</b><br />
                                            <asp:RadioButtonList ID="rbTipoAtendimento" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" id="trMunicipiosBotao" runat="server" visible="false">
                                        <div class="cell">
                                            <asp:Button ID="btnAdicionarAtendimento" runat="server" SkinID="button-save" Text="Adicionar" OnClick="btnAdicionarAtendimento_Click" />
                                        </div>
                                    </div>
                                    <div id="lstAtendidos" class="row" runat="server" visible="false">
                                        <div class="cell">
                                            <asp:ListView ID="lstMunicipiosAtendidos" runat="server" OnItemDataBound="lstMunicipiosAtendidos_ItemDataBound"
                                                OnItemCommand="lstMunicipiosAtendidos_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th align="center" width="20" style="height: 22px;"></th>
                                                                <th width="30%">Município
                                                                </th>
                                                                <th width="15%">Número de Atendidos
                                                                </th>
                                                                <th width="40%">Tipo de Atedimento
                                                                </th>
                                                                <th width="15%">Excluir
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
                                                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "NumeroAtendidos") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TipoAtendimento.TipoAtendimento") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                CommandName="Excluir_Atendimento" CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir este registro?');" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existem registros de municípios atendidos por este Centro POP</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Trabalho realizado por este Centro POP</b></legend>
                                                <a href="#" runat="server" id="linkAlteracoesQuadro33" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>
                                                <div class="row">
                                                    <div class="cell">
                                                       O órgão gestor deve informar somente as ações atualmente realizadas pelo CREAS. As que ainda estão em planejamento devem integrar as informações do bloco de Planejamento.<br />
                                                        <b>Este Centro POP oferta o Serviço Especializado para Pessoas em Situação de Rua?</b><br />
                                                        <asp:RadioButtonList ID="rblServicoESR" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="rblServicoESR_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="Sim" />
                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="trUsuarios" runat="server" visible="false">
                                                    <div class="cell">
                                                        <b>Assinale qual o tipo de usuário do Serviço Especializado para Pessoas em Situação de Rua:</b><br />
                                                        
                                                        <asp:RadioButtonList runat="server" ID="lstUsuarios">
                                                            <asp:ListItem Value="28" Text="apenas jovens, adultos, idosos e famílias" />
                                                            <asp:ListItem Value="29" Text="crianças, adolescentes, jovens, adultos, idosos e famílias"/>
                                                        </asp:RadioButtonList>

                                                    </div>
                                                </div>
                                                <div class="row" id="trJustificativaESR" runat="server" visible="true">
                                                    <div class="cell" colspan="2">
                                                        <b>Justifique:</b><br />
                                                        <div class="input-control textarea">
                                                            <asp:TextBox ID="txtJustificativaESR" runat="server" TextMode="MultiLine" Height="38px"
                                                                Width="449px" MaxLength="300" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Quais atividades são desenvolvidas por este Centro POP?</b><br />
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
                </div>
 <%--               <div class="frame active">
                    <div class="heading">
                        &nbsp;3.8.2- 
                    </div>
                    <div class="content">
                        <div class="formInput" data-text="Trabalho realizado">
                            <div class="grid">
                               
                               
                               
                            </div>
                        </div>
                    </div>
                </div>--%>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
