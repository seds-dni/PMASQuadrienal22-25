<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FCREAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FCREAS" %>

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
            <form name="frmCREAS">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro26" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp; 3.7 - Características deste CREAS
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="CREAS">
                                <div class="grid">
                                    <div class="row cells2">
                                        <div class="cell">
                                            <td>
                                                <b>Nome da Unidade:</b><br />
                                                <div class="input-control text full-size">
                                                    <asp:TextBox ID="txtNome" runat="server" MaxLength="120" Width="400px"></asp:TextBox>
                                                </div>
                                            </td>
                                        </div>
                                        <div class="cell">
                                            <b>Código Identificador do CREAS - IDCREAS:</b><br />
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
                                        <div class="cell" id="trOutraFormacao" runat="server" visible="false">
                                            <b>
                                                <asp:Label ID="lblEspecifique" Text="Especificar:" runat="server"></asp:Label></b><br />
                                            <div class="input-control text mid-size">
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
                                            <b>Capacidade de atendimento anual:</b><br />
                                            <asp:TextBox ID="txtCapacidadeAtendimento" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroAtendidos" runat="server" FilterType="Numbers" TargetControlID="txtNumeroAtendidos" />
                                        </div>
                                        <div class="cell">
                                            <b>Previsão anual do número de pessoas atendidas:</b><br />
                                            <asp:TextBox ID="txtNumeroAtendidos" runat="server" MaxLength="6" Width="62px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Número de trabalhadores deste CREAS:</b>
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
                                            <b>Este CREAS atende usuários de outro(s) município(s)?</b><br />
                                            <asp:RadioButtonList ID="rbAtendeUsuarios" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" OnSelectedIndexChanged="rbAtendeUsuarios_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row cells3" visible="false" runat="server" id="trMunicipios">
                                        <div class="cell">
                                            <b>Município de origem dos usuários atendidos:</b><br>
                                            <div class="input-control select mid-size">
                                                <asp:DropDownList ID="ddlMunicipioConveniado" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Nº de usuários atendidos:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtQuantosAtendidos" TabIndex="2" runat="server" Width="58px" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Tipo de Atendimento</b><br />
                                            <asp:RadioButtonList ID="rbTipoAtendimento" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </div>
                                        <%-- <div class="cell" align="center">
                                            <asp:Button runat="server" ID="incluium" Text=">" Width="50px" OnClick="incluium_Click" /><br />
                                            <asp:Button runat="server" ID="incluitodos" Text=">>" Width="50px" OnClick="incluitodos_Click" /><br />
                                            <asp:Button runat="server" ID="voltarum" Text="<" Width="50px" OnClick="voltarum_Click" /><br />
                                            <asp:Button runat="server" ID="voltartodos" Text="<<" Width="50px" OnClick="voltartodos_Click" /><br />
                                        </div>
                                        <div class="cell" align="left">
                                            Municípios selecionados<br>
                                            <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosSel" SelectionMode="Multiple"
                                                Height="120px" Width="150px"></asp:ListBox>
                                        </div>--%>
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
                                                <a href="#" runat="server" id="linkAlteracoesQuadro27" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>
                                                <div class="row">
                                                    <div class="cell">
                                                       O órgão gestor deve informar somente as ações atualmente realizadas pelo CREAS. As que ainda estão em planejamento devem integrar as informações do bloco de Planejamento.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Este CREAS oferta o Serviço de Proteção e Atendimento Especializado a Famílias e Indivíduos - PAEFI?</b><br />
                                                        <asp:RadioButtonList ID="rblServicoPAEFI" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblServicoPAEFI_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Sim" Value="1" />
                                                            <asp:ListItem Selected="True" Text="Não" Value="0" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="trJustificativaPAEFI" runat="server" visible="true">
                                                    <div class="cell">
                                                        <b>Justifique:</b><br />
                                                        <div class="input-control textarea">
                                                            <asp:TextBox ID="txtJustificativaPAEFI" runat="server" Height="38px" TextMode="MultiLine" Width="449px" MaxLegth="300" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <b>Quais atividades são desenvolvidas por este CREAS?</b><br />
                                                    <div class="cell">
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
          <%--      <div class="frame active">
                    <div class="heading">
                    &nbsp;3.7.2 - 
                    </div>
                    <div class="content">
                        <div class="formInput" data-text="Trabalho realizado">
                            <div class="grid">
                                
                              
                              
                            </div>
                        </div>
                    </div>
                </div>--%>
            </form>
            <%--                            <tr runat="server" id="trDistritoSP" visible="false">
                                <td colspan="2">
                                    <b>Distrito:</b><br />
                                    <asp:DropDownList ID="ddlDistrito" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="bg-alternative">
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <b>Data da implantação:</b><br />
                                                <uc4:data ID="txtDataImplantacao" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
            <%--    <tr class="bg-alternative">
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td valign="top"><b>Este local funciona quantas horas por semana?</b><br />
                                        <asp:RadioButtonList ID="rblHorasSemana" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="Até 20 horas" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="de 21 a 39 horas" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="40 horas" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="mais de 40 horas" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="ininterrupto (24 horas / 7 dias)" Value="5"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="top"><b>Este local funciona em quantos dias por semana?</b><br />
                                        <asp:RadioButtonList ID="rblDiasSemana" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                            <asp:ListItem Text="6" Value="6" />
                                            <asp:ListItem Text="7" Value="7" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <fieldset>
                                <legend><b class="titulo">RH deste local de serviços</b></legend>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2"><b>Indique o número de trabalhadores, segundo a escolaridade:</b> </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td width="200">
                                                        <asp:TextBox ID="txtSemEscolaridade" runat="server" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Sem Escolarização </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNivelFundamental" runat="server" CssClass="campoTexto" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Nível Fundamental </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNivelMedio" runat="server" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Nível Médio </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtSuperior" runat="server" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Superior </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="lblTotalRh" runat="server" CssClass="campoTexto" Enabled="false" Text="" Width="48px"></asp:TextBox>
                                                        &nbsp;Total </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td align="left" colspan="2"><b>Indique a área de formação dos trabalhadores que possuem nível superior:</b> </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtSuperiorServicoSocial" runat="server" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Serviço Social </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorTerapiaOcupacional" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Terapia Ocupacional </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtSuperiorPsicologia" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Psicologia </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorAntropologia" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Antropologia </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtSuperiorPedagogia" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Pedagogia </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorMusicoTerapia" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Musicoterapia </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtSociologia" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Sociologia </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorEconomia" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Economia </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtDireito" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Direito </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorEconomiaDomestica" runat="server" AutoCompleteType="Disabled" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Economia Doméstica </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td><b>Indique o número de trabalhadores que possuem pós-graduação:</b> </td>
                                                    <td style="padding-left: 5px;"><b>
                                                        <asp:Label ID="lblNroEstagiarios" runat="server" Text="Indique o número de:"></asp:Label>
                                                    </b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtPosGraduacao" runat="server" MaxLength="4" Width="48px"></asp:TextBox>
                                                        &nbsp;Pós-Graduação </td>
                                                    <td style="padding-left: 5px;">
                                                        <asp:TextBox ID="txtEstagiarios" runat="server" Text="" Width="48px"></asp:TextBox>
                                                        &nbsp;Estagiários
                                                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtEstagiarios" runat="server"
                                                                           TargetControlID="txtEstagiarios" FilterType="Numbers" />
                                                        <br />
                                                        <asp:TextBox ID="txtVoluntarios" runat="server" Text="" Width="48px"></asp:TextBox>&nbsp;Voluntários
                                                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtVoluntarios" runat="server"
                                                                           TargetControlID="txtVoluntarios" FilterType="Numbers" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>--%>

            <%--     <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="700" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">14</b> <b>- Abrangência</b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro28" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
