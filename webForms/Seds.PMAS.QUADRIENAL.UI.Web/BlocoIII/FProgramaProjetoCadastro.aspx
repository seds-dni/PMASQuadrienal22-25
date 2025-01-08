<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true"
    CodeBehind="FProgramaProjetoCadastro.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FProgramaProjetoCadastro" %>

<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <form name="frmProgramaProjeto">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <asp:Label runat="server" ID="lblNumeracao" Text="39"></asp:Label>-
                                    <asp:Label runat="server" ID="lblTitulo" Text="Informações sobre o Programa/Projeto"></asp:Label>
                            <a href="#" runat="server" id="linkAlteracoesQuadroProgramaProjeto" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                        <span class="mif-organization icon"></span></a>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Programas e Projetos">
                                <div class="grid">
                                    <!--Comum a todos-->
                                    <div class="row" id="trNomePrograma" runat="server">
                                        <div class="cell">
                                            <b>Programa/Projeto:</b><br />
                                            <div class="input-control text mid-size">
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="120" Width="490px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblNome" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Descrição:</b><br />
                                            <div class="input-control textarea">
                                                <asp:TextBox ID="txtObjetivo" runat="server" Width="668px" TextMode="MultiLine" Height="51px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblObjetivo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="trBeneficiarios" runat="server" visible="false">
                                        <div class="cell">
                                            <b>Beneficiários:</b><br />
                                            <asp:Label ID="lblBeneficiarios" runat="server"></asp:Label>
                                            <div class="input-control select mid-size" runat="server">
                                                <asp:DropDownList ID="ddlBeneficiarios" runat="server" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <!--Fim Comum a todos-->

                                    <asp:UpdatePanel runat="server" ID="updatePanel10" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <!--Somente Novo Programa Municpal-->
                                            <div class="row" id="trTipoPrograma" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Este programa/projeto inclui transferência direta de renda ao beneficiário?</b>
                                                    <asp:RadioButtonList ID="rblTransferenciaRendaDireta" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblTransferenciaRendaDireta_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <!--Fim Somente Novo Programa Municpal-->



                                            <div class="row" id="trDataProgramaProjeto" runat="server" visible="false">
                                                <fieldset class="border-blue">
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <b>
                                                                <asp:Label ID="lblInicioProjeto" runat="server" Text="Início do Programa/Projeto:"></asp:Label>
                                                            </b>
                                                            <br />
                                                            <div class="row cells2">
                                                                <div class="cell">
                                                                    Mês:
                                                    <div class="input-control select">
                                                        <asp:DropDownList ID="ddlMesInicio" runat="server">
                                                            <asp:ListItem Selected="True" Text="[Selecione o mês]" Value="0" />
                                                            <asp:ListItem Text="Janeiro" Value="1" />
                                                            <asp:ListItem Text="Fevereiro" Value="2" />
                                                            <asp:ListItem Text="Março" Value="3" />
                                                            <asp:ListItem Text="Abril" Value="4" />
                                                            <asp:ListItem Text="Maio" Value="5" />
                                                            <asp:ListItem Text="Junho" Value="6" />
                                                            <asp:ListItem Text="Julho" Value="7" />
                                                            <asp:ListItem Text="Agosto" Value="8" />
                                                            <asp:ListItem Text="Setembro" Value="9" />
                                                            <asp:ListItem Text="Outubro" Value="10" />
                                                            <asp:ListItem Text="Novembro" Value="11" />
                                                            <asp:ListItem Text="Dezembro" Value="12" />
                                                        </asp:DropDownList>
                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    Ano:
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtAnoInicio" runat="server" MaxLength="4" Width="50px" />
                                                    </div>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoInicio" runat="server" FilterType="Numbers" TargetControlID="txtAnoInicio" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            <b>
                                                                <asp:Label ID="lblTerminoProjeto" runat="server" Text="Previsão de término do Programa/Projeto:"></asp:Label>
                                                            </b>
                                                            <br />
                                                            <div class="row cells2">
                                                                <div class="cell">
                                                                    Mês :
                                                    <div class="input-control select">
                                                        <asp:DropDownList ID="ddlMesTermino" runat="server">
                                                            <asp:ListItem Selected="True" Text="[Selecione o mês]" Value="0" />
                                                            <asp:ListItem Text="Janeiro" Value="1" />
                                                            <asp:ListItem Text="Fevereiro" Value="2" />
                                                            <asp:ListItem Text="Março" Value="3" />
                                                            <asp:ListItem Text="Abril" Value="4" />
                                                            <asp:ListItem Text="Maio" Value="5" />
                                                            <asp:ListItem Text="Junho" Value="6" />
                                                            <asp:ListItem Text="Julho" Value="7" />
                                                            <asp:ListItem Text="Agosto" Value="8" />
                                                            <asp:ListItem Text="Setembro" Value="9" />
                                                            <asp:ListItem Text="Outubro" Value="10" />
                                                            <asp:ListItem Text="Novembro" Value="11" />
                                                            <asp:ListItem Text="Dezembro" Value="12" />
                                                        </asp:DropDownList>
                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    Ano:
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtAnoTermino" runat="server" MaxLength="4" Width="50px" />
                                                    </div>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoTermino" runat="server" FilterType="Numbers" TargetControlID="txtAnoTermino" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>




                                            <div class="row" id="trMetaPactuada" runat="server" visible="false">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Meta Pactuada
                                                        <asp:Label ID="lblQuadro" runat="server" Visible="false" />
                                                    </b></legend>
                                                    <div class="row mid-size" id="trPrevisaoAnualPrimeiraInfancia" runat="server" visible="false">
                                                        <div class="cell">
                                                            <table class="table striped border bordered" cellspacing="0"
                                                                cellpadding="0" border="0" width="400">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th style="height: 22px;" colspan="4">Recursos financeiros repassados pelo FNAS em:</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label2" Text="2022" Width="50" runat="server"></asp:Label></td>
                                                                        <td style="width: 150px;">
                                                                            <div class="input-control text">
                                                                                <asp:TextBox ID="txtRecursosFNASExercicio1" Enabled="false" runat="server" Width="100" Text="0,00" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label6" Text="2023" Width="50" runat="server" /></td>
                                                                        <td style="width: 150px;">
                                                                            <div class="input-control text">
                                                                                <asp:TextBox ID="txtRecursosFNASExercicio2" runat="server" Width="100" Enabled="true" Text="0,00" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td style="width: 30px;">
                                                                            <asp:Label ID="Label7" Width="50" Text="2024" runat="server" /></td>
                                                                        <td>
                                                                            <div class="input-control text">
                                                                                <asp:TextBox ID="txtRecursosFNASExercicio3" runat="server" Text="0,00" Width="100" Enabled="false" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 30px;">
                                                                            <asp:Label ID="Label8" Text="2025" Width="50" runat="server" /></td>
                                                                        <td>
                                                                            <div class="input-control text">
                                                                                <asp:TextBox ID="txtRecursosFNASExercicio4" Width="100" Text="0,00" runat="server" Enabled="false" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="row mid-size" id="trPrevisaoAnual" runat="server">
                                                        <div class="cell">
                                                            <table class="table striped border bordered" cellspacing="0"
                                                                cellpadding="0" border="0" width="400">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th width="40" style="height: 22px;"
                                                                            rowspan="2">Exercício</th>
                                                                        <th width="80" rowspan="2">
                                                                            <asp:Label Text="Meta Pactuada" runat="server" ID="lblMetaPactuada"></asp:Label>
                                                                        </th>
                                                                        <th width="80" rowspan="2">
                                                                            <asp:Label ID="lblHeadtbPrevisaoAnual" runat="server"></asp:Label>

                                                                        </th>
                                                                        <th width="100" id="thPrevisaoAnualTotal" runat="server" visible="false">Previsão anual do valor do repasse
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr align="center">
                                                                        <td width="40">
                                                                            <asp:Label ID="lblExercicioExercicio1" Text="2022" runat="server"></asp:Label></td>
                                                                        <td width="150">
                                                                            <div class="input-control text mid-size">
                                                                                <asp:TextBox ID="txtMetaPactuadaExercicio1" MaxLength="6" Width="60" runat="server" Enabled="false" Style="text-align: right;" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMetaPactuadaExercicio1" />
                                                                            </div>
                                                                        </td>
                                                                        <td width="150">
                                                                            <div class="input-control text">
                                                                                <asp:Label ID="lblPrevisaoAnualExercicio1" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtPrevisaoAnualExercicio1" runat="server" Enabled="false" Width="100" Text="0,00" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                        <td width="150" id="tdPrevisaoAnualTotalExercicio1" visible="false" runat="server">
                                                                            <asp:Label ID="lblPrevisaoAnualTotalExercicio1" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblExercicioExercicio2" Width="150" Text="2023" runat="server" /></td>
                                                                        <td>
                                                                            <div class="input-control text mid-size">
                                                                                <asp:TextBox ID="txtMetaPactuadaExercicio2" MaxLength="6" runat="server" Width="60" Enabled="true" Style="text-align: right;" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtMetaPactuadaExercicio2" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-control text">
                                                                                <asp:Label ID="lblPrevisaoAnualExercicio2" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtPrevisaoAnualExercicio2" runat="server" Width="100" Enabled="true" Text="0,00" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                        <td width="150" id="tdPrevisaoAnualTotalExercicio2" visible="false" runat="server">
                                                                            <asp:Label ID="lblPrevisaoAnualTotalExercicio2" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblExercicioExercicio3" Width="150" Text="2024" runat="server" /></td>
                                                                        <td>
                                                                            <div class="input-control text mid-size">
                                                                                <asp:TextBox ID="txtMetaPactuadaExercicio3" MaxLength="6" runat="server" Width="60" Enabled="false" Style="text-align: right;" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtMetaPactuadaExercicio3" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-control text">
                                                                                <asp:Label ID="lblPrevisaoAnualExercicio3" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtPrevisaoAnualExercicio3" runat="server" Enabled="false" Width="100" Text="0,00" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                        <td width="150" id="tdPrevisaoAnualTotalExercicio3" visible="false" runat="server">
                                                                            <asp:Label ID="lblPrevisaoAnualTotalExercicio3" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblExercicioExercicio4" Width="150" Text="2025" runat="server" /></td>
                                                                        <td>
                                                                            <div class="input-control text mid-size">
                                                                                <asp:TextBox ID="txtMetaPactuadaExercicio4" Width="60" runat="server" Enabled="false" Style="text-align: right;" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="txtMetaPactuadaExercicio4" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-control text">
                                                                                <asp:Label ID="lblPrevisaoAnualExercicio4" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtPrevisaoAnualExercicio4" runat="server" Width="100" Enabled="false" Text="0,00" Style="text-align: right;" />
                                                                            </div>
                                                                        </td>
                                                                        <td width="150" id="tdPrevisaoAnualTotalExercicio4" visible="false" runat="server">
                                                                            <asp:Label ID="lblPrevisaoAnualTotalExercicio4" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                                </fieldset>
                                            </div>
                                            <div class="row" id="trCaracterizacaoUsuarios" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Usuários:</b>
                                                    <asp:CheckBoxList ID="chkCaracterizacaoUsuarios" runat="server" CellSpacing="5" RepeatColumns="2" RepeatDirection="Horizontal" />
                                                </div>
                                            </div>

                                            <%--<div class="row" id="trAcoesDesenvolvidas" runat="server" visible="false">
                                                <div class="cell" align="left" colspan="2">
                                                    <b>Ações desenvolvidas pelo programa:</b>
                                                    <asp:CheckBoxList ID="chkAcoesDesenvolvida" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkAcoesDesenvolvida_SelectedIndexChanged" />
                                                </div>
                                            </div>--%>
                                            <div class="row" id="trCursosOferecidos" runat="server" visible="false">
                                                <fieldset>
                                                    <legend><b class="titulo">
                                                        <asp:Label ID="Label3" runat="server" Text="Cursos Ofertados" />
                                                    </b></legend>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Unidade Ofertante:</b><br />
                                                            <asp:TextBox ID="txtUnidadeOfertante" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Eixo Tecnologico:</b><br />
                                                            <asp:DropDownList ID="ddlEixoTecnologico" runat="server" MaxLength="100" Width="300px">
                                                            </asp:DropDownList>
                                                            <asp:ImageButton ID="btnAjudaEixoTecnologico" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Styles/Icones/help.png" OnClientClick="return false;" />
                                                            <asp:Panel ID="pnlAjudaEixoTecnologico" runat="server" CssClass="ajuda" Height="50px" Width="300px">
                                                                <div style="float: right;">
                                                                    <asp:LinkButton ID="lnkCloseAjudaEixoTecnologico" runat="server" OnClientClick="return false;" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" Text="X" ToolTip="Fechar" />
                                                                </div>
                                                                <div>
                                                                    <p>
                                                                        Clique no link abaixo para consulta aos cursos
                                                    <br />
                                                                        já existentes distribuídos em 13 Eixos<br />
                                                                        tecnológicos <a href="http://pronatec.mec.gov.br/fic/" target="_blank">http://pronatec.mec.gov.br/fic</a>
                                                                    </p>
                                                                </div>
                                                            </asp:Panel>
                                                            <ajaxToolkit:AnimationExtender ID="OpenAnimationAjudaEixoTecnologico" runat="server" TargetControlID="btnAjudaEixoTecnologico">
                                                                <Animations>
                                                        <OnClick>
                                                            <Sequence AnimationTarget="pnlAjudaEixoTecnologico">
                                                            <EnableAction AnimationTarget="btnAjudaEixoTecnologico" Enabled="false" />
                                                            <StyleAction Attribute="display" Value="block" />                                    
                                                            <Parallel>
                                                                <FadeIn Duration="1" Fps="20" />                                    
                                                            </Parallel>
                                                            </Sequence>
                                                        </OnClick>
                                                                </Animations>
                                                            </ajaxToolkit:AnimationExtender>
                                                            <ajaxToolkit:AnimationExtender ID="CloseAnimationAjudaEixoTecnologico" runat="server" TargetControlID="lnkCloseAjudaEixoTecnologico">
                                                                <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="pnlAjudaEixoTecnologico">                                                    
                                                            <Parallel Duration=".3" Fps="15">                                                        
                                                                <FadeOut />
                                                            </Parallel>                        
                                                            <%--  Reset the sample so it can be played again --%>
                                                            <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                            <%--  Enable the button so it can be played again --%>
                                                            <EnableAction AnimationTarget="btnAjudaEixoTecnologico" Enabled="true" />
                                                        </Sequence>
                                                    </OnClick>
                                                    <OnMouseOver>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                    </OnMouseOver>
                                                    <OnMouseOut>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                    </OnMouseOut>
                                                                </Animations>
                                                            </ajaxToolkit:AnimationExtender>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell" width="350">
                                                            <b>Nome do Curso:</b><br />
                                                            <asp:TextBox ID="txtNomeCurso" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell" align="center">
                                                            <asp:Button ID="btnAdicionarUnidadeOfertante" runat="server" OnClick="btnAdicionarUnidadeOfertante_Click" SkinID="button-add" Text="Adicionar" Width="100px" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:ListView ID="lstUnidadeOfertante" runat="server" OnItemCommand="lstUnidadeOfertante_ItemCommand" OnItemDataBound="lstUnidadeOfertante_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <table border="0" cellpadding="0" cellspacing="0" class="table border bordered">
                                                                        <thead class="info">
                                                                            <tr>
                                                                                <th style="height: 22px;" width="20"></th>
                                                                                <th width="220">Unidade Ofertante </th>
                                                                                <th width="250">Eixo tecnológico </th>
                                                                                <th width="125">Nome do Curso </th>
                                                                                <th width="50">Excluir </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <%--<tr class="jqgfirstrow" style="height: auto;">
                                                                                <td style="height: 0px;"></td>
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
                                                                        <td align="left"><%#DataBinder.Eval(Container.DataItem, "UnidadeOfertante") %></td>
                                                                        <td align="left"><%#DataBinder.Eval(Container.DataItem, "EixoTecnologico.Descricao") %></td>
                                                                        <td align="left"><%#DataBinder.Eval(Container.DataItem, "NomeCurso") %></td>
                                                                        <td class="align-center">
                                                                            <asp:ImageButton ID="btnExcluirUnidadeOfertante" runat="server" CausesValidation="false" CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover a unidade ofertante?');" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <!--Fim Acessuas-->

                                            <div class="row" id="trAtividadesRealizadas" runat="server">
                                                <div class="cell">
                                                    <b>Principais ações e atividades realizadas por este programa/projeto:</b>
                                                    <asp:ImageButton ID="btnAjudaAcoesAtividades" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Styles/Icones/help.png" OnClientClick="return false;" />
                                                    <asp:Panel ID="pnlAjudaAcoesAtividades" runat="server" CssClass="ajuda" Height="200px" Width="400px">
                                                        <div style="float: right;">
                                                            <asp:LinkButton ID="lnkCloseAjudaAcoesAtividades" runat="server" OnClientClick="return false;" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" Text="X" ToolTip="Fechar" />
                                                        </div>
                                                        <div>
                                                            <p>
                                                                Ações são estratégias que visam à concretização de um processo ou de algum dos seus aspectos. Devem refletir esforço conjunto de várias pessoas, grupos ou organizações buscando alterar, de forma permanente ou não, uma determinada situação.
                                                            </p>
                                                            <p>
                                                                Atividades são os procedimentos que operacionalizam determinadas ações e qualificam a metodologia utilizada. São exemplos de atividades: palestras, oficinas, reuniões, visitas domiciliares, contatos institucionais, entre outras.
                                                            </p>
                                                        </div>
                                                    </asp:Panel>
                                                    <ajaxToolkit:AnimationExtender ID="OpenAnimationAcoesAtividades" runat="server" TargetControlID="btnAjudaAcoesAtividades">
                                                        <Animations>
                                                        <OnClick>
                                                            <Sequence AnimationTarget="pnlAjudaAcoesAtividades">
                                                            <EnableAction AnimationTarget="btnAjudaAcoesAtividades" Enabled="false" />
                                                            <StyleAction Attribute="display" Value="block" />                                    
                                                            <Parallel>
                                                                <FadeIn Duration="1" Fps="20" />                                    
                                                            </Parallel>
                                                            </Sequence>
                                                        </OnClick>
                                                        </Animations>
                                                    </ajaxToolkit:AnimationExtender>
                                                    <ajaxToolkit:AnimationExtender ID="CloseAnimationAcoesAtividades" runat="server" TargetControlID="lnkCloseAjudaAcoesAtividades">
                                                        <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="pnlAjudaAcoesAtividades">                                                    
                                                            <Parallel Duration=".3" Fps="15">                                                        
                                                                <FadeOut />
                                                            </Parallel>                        
                                                            <StyleAction Attribute="display" Value="none"/>                                                                                                       
                                                            <EnableAction AnimationTarget="btnAjudaAcoesAtividades" Enabled="true" />
                                                        </Sequence>
                                                    </OnClick>
                                                    <OnMouseOver>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                    </OnMouseOver>
                                                    <OnMouseOut>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                    </OnMouseOut>
                                                        </Animations>
                                                    </ajaxToolkit:AnimationExtender>
                                                    <br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtAcoes" runat="server" Height="51px" MaxLength="600" TextMode="MultiLine" Width="668px"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 600 caracteres." Font-Bold="True" MaxCharacterLength="600" TextBoxControlId="txtAcoes" />
                                                </div>
                                            </div>
                                            <div class="row" id="trAbrangencia" runat="server">
                                                <div class="cell">
                                                    <b>Abrangência Territorial:</b><br />
                                                    <asp:RadioButtonList ID="rblAbrangencia" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Urbana" Value="1" />
                                                        <asp:ListItem Text="Rural" Value="2" />
                                                        <asp:ListItem Selected="True" Text="Urbana e Rural" Value="3" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="trParcerias" runat="server">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">
                                                        <asp:Label ID="legendParcerias" runat="server" Text="Articulações promovidas neste programa"></asp:Label></b></legend>
                                                    <b>
                                                        <asp:Label Style="margin-left: 10px;" ID="lblExisteParceria" runat="server" Text="Existem parcerias estabelecidas, formal ou informalmente, para a execução deste programa/projeto?"></asp:Label></b><br />
                                                    <asp:RadioButtonList ID="rblParcerias" Style="margin-left: 5px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblParcerias_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Selected="True" Text="Não" Value="0" />
                                                    </asp:RadioButtonList>
                                                    <br />
                                                    <div class="row" id="tbParcerias" runat="server" visible="false" style="margin-left: 10px;">
                                                        <div style="width: 700px">
                                                            <div class="row" style="width: 450px; display: inline;">
                                                                <div class="cell">
                                                                    <b>
                                                                        <asp:Label ID="lblTituloParcerias" runat="server" Text="Informe com quem foram formalizadas as parcerias para a execução deste programa/projeto:"></asp:Label></b><br />
                                                                    <div class="input-control select mid-size">
                                                                        <asp:DropDownList ID="ddlParceria" runat="server" Width="410px" OnSelectedIndexChanged="ddlParceria_SelectedIndexChanged" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row cells3" style="width: 300px; display: inline;">
                                                                <div class="cell colspan2">
                                                                    <b>Nome do Órgão:</b><br />
                                                                    <div class="cell">
                                                                        <div class="input-control text mid-size">
                                                                            <asp:TextBox ID="txtNomeOrgao" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>
                                                                        <asp:Label ID="lblTipoParceria" runat="server" Text="Tipo da Parceria:"></asp:Label></b><br />
                                                                    <div class="input-control select mid-size">
                                                                        <asp:DropDownList ID="ddlTipoParceria" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:UpdatePanel ID="upParceria1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="cell">
                                                                        <asp:Button ID="btnAdicionarParceria" runat="server" OnClick="btnAdicionarParceria_Click" SkinID="button-save" Text="Adicionar Parceria" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <table id="tbInconsistenciasParceria" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                                        width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                                        <tr>
                                                                            <td style="padding: 15px 10px 2px 15px">
                                                                                <span class="mif-warning mif-2x"></span>
                                                                              <b style='color: #000000 !important'>Verifique as inconsistências:</b>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding: 10px 10px 12px 45px;">
                                                                                <asp:Label ID="lblInconsistenciasParceria" ForeColor="Red" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>


                                                                <div class="row">
                                                                    <div class="cell">
                                                                        <asp:ListView ID="lstParcerias" runat="server" OnItemCommand="lstParcerias_ItemCommand" OnItemDataBound="lstParcerias_ItemDataBound">
                                                                            <LayoutTemplate>
                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 93%" class="table border bordered">
                                                                                    <thead class="info">
                                                                                        <tr>
                                                                                            <th style="height: 22px;" width="20"></th>
                                                                                            <th style="max-width: 180px">Nome do Órgão </th>
                                                                                            <th style="max-width: 200px">Parcerias </th>
                                                                                            <th style="width: 70px">Tipo da Parceria </th>
                                                                                            <th style="width: 30px">Excluir </th>
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
                                                                                    <td style="max-width: 100px;" align="left"><%#DataBinder.Eval(Container.DataItem, "NomeOrgao") %></td>
                                                                                    <td style="max-width: 100px;" align="left"><%#DataBinder.Eval(Container.DataItem, "Parceria.Nome") %></td>
                                                                                    <td style="width: 100px;" align="left"><%#DataBinder.Eval(Container.DataItem, "TipoParceria.Nome") %></td>
                                                                                    <td style="width: 30px;">
                                                                                        <asp:ImageButton ID="btnExcluirParceria" runat="server" CausesValidation="false" CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover a parceria?');" />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarParceria" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rblTransferenciaRendaDireta" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row" id="trIdentificacaoTerritorio" runat="server" visible="false">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">
                                                    <asp:Label ID="legendIdentificacao" runat="server" Text="Identificação dos territórios"></asp:Label></b></legend>
                                                <br />
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Número de Identificação do Território:</b><br />
                                                        <asp:TextBox ID="txtNumeroIdentificacaoTerritorio" runat="server" Width="80px" MaxLength="4"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroIdentificacaoTerritorio" runat="server" FilterType="Numbers" TargetControlID="txtNumeroIdentificacaoTerritorio" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Nomes dos bairros que compõem o território:</b>
                                                        <br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtIdentificacaoTerritorio" runat="server" Width="340px"></asp:TextBox>
                                                        </div>
                                                        <asp:ImageButton ID="btnAjudaIdentificacaoTerritorio" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Styles/Icones/help.png" OnClientClick="return false;" />
                                                        <asp:Panel ID="pnlAjudaIdentificacaoTerritorio" runat="server" CssClass="ajuda" Height="70px" Width="400px">
                                                            <div style="float: right;">
                                                                <asp:LinkButton ID="lnkCloseAjudaIdentificacaoTerritorio" runat="server" OnClientClick="return false;" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" Text="X" ToolTip="Fechar" />
                                                            </div>
                                                            <div>
                                                                <p>
                                                                    Neste campo o município deverá informar um ou mais bairros que compõem o território, uma vez que o bairro é a referência de unidade territorial mínima do Programa Família Paulista.
                                                                </p>
                                                            </div>
                                                        </asp:Panel>
                                                        <ajaxToolkit:AnimationExtender ID="OpenAnimationIdentificacaoTerritorio" runat="server" TargetControlID="btnAjudaIdentificacaoTerritorio">
                                                            <Animations>
                                        <OnClick>
                                            <Sequence AnimationTarget="pnlAjudaIdentificacaoTerritorio">
                                            <EnableAction AnimationTarget="btnAjudaIdentificacaoTerritorio" Enabled="false" />
                                            <StyleAction Attribute="display" Value="block" />                                    
                                            <Parallel>
                                                <FadeIn Duration="1" Fps="20" />                                    
                                            </Parallel>
                                            </Sequence>
                                        </OnClick>
                                                            </Animations>
                                                        </ajaxToolkit:AnimationExtender>
                                                        <ajaxToolkit:AnimationExtender ID="CloseAnimationAjudaIdentificacaoTerritorio" runat="server" TargetControlID="lnkCloseAjudaIdentificacaoTerritorio">
                                                            <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="pnlAjudaIdentificacaoTerritorio">                                                    
                                                            <Parallel Duration=".3" Fps="15">                                                        
                                                                <FadeOut />
                                                            </Parallel>                        
                                                            <%--  Reset the sample so it can be played again --%>
                                                            <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                            <%--  Enable the button so it can be played again --%>
                                                            <EnableAction AnimationTarget="btnAjudaIdentificacaoTerritorio" Enabled="true" />
                                                        </Sequence>
                                                    </OnClick>
                                                    <OnMouseOver>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                    </OnMouseOver>
                                                    <OnMouseOut>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                    </OnMouseOut>
                                                            </Animations>
                                                        </ajaxToolkit:AnimationExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Nome do responsável pelo Programa neste território:</b><br />
                                                        <div class="input-control text mid-size">
                                                            <asp:TextBox ID="txtNomeResponsavelTerritorio" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Número de famílias beneficiárias neste território:</b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtNumeroBeneficiariosTerritorio" runat="server"></asp:TextBox>
                                                        </div>
                                                        <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroBeneficiariosTerritorio" runat="server" FilterType="Numbers" TargetControlID="txtNumeroBeneficiariosTerritorio" />--%>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell" align="center">
                                                        <asp:Button ID="btnAdicionarIdentificacao" runat="server" SkinID="button-add" Text="Adicionar" Width="100px" OnClick="btnAdicionarIdentificacao_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:ListView ID="lstIdentificacaoTerritorio" runat="server" OnItemCommand="lstIdentificacaoTerritorio_ItemCommand" OnItemDataBound="lstIdentificacaoTerritorio_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" class="table border bordered">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th colspan="6" style="height: 22px;">Territórios integrantes do Programa Família Paulista</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th colspan="2">Identificação
                                                                <br />
                                                                                do território</th>
                                                                            <th width="250">Bairros</th>
                                                                            <th width="275">Nome do responsável </th>
                                                                            <th width="80">N° de famílias
                                                                <br />
                                                                                beneficiárias</th>
                                                                            <th width="50">Excluir </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%--  <tr class="jqgfirstrow" style="height: auto;">
                                                                            <td style="height: 0px;"></td>
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
                                                                    <td style="height: 22px; width: 20px;">
                                                                        <asp:Label ID="lblSequencia" runat="server" />
                                                                    </td>
                                                                    <td align="left" style="width: 100px;"><%#DataBinder.Eval(Container.DataItem, "NumeroIdentificacao") %></td>
                                                                    <td align="left" style="width: 300px;"><%#DataBinder.Eval(Container.DataItem, "IdentificacaoTerritorio") %></td>
                                                                    <td align="left"><%#DataBinder.Eval(Container.DataItem, "NomeResponsavel") %></td>
                                                                    <td class="align-center"><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios")) %></td>
                                                                    <%-- <td align="right"><%#DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios") %></td>--%>
                                                                    <td class="align-center">
                                                                        <asp:ImageButton ID="btnExcluirIdentificacaoTerritorio" runat="server" CausesValidation="false" CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover o Território?');" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row" id="trRecursosFinanceiros" runat="server" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">
                                                <asp:Label ID="legendRecursosFinanceiros" runat="server" Text="Recursos financeiros para a execução deste programa:"></asp:Label></b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <asp:Label ID="lblDescricaoRecursosFinanceiros" runat="server" Text="Informe outra(s) fonte(s) e valores dos recursos financeiros para a execução deste programa, além dos valores já informados do FNAS." />
                                                </div>
                                            </div>
                                            <div class="row" id="trParcela1" runat="server" visible="false">
                                                <div class="cell">
                                                    <fieldset class="border-blue">
                                                        <legend class="lgnd"><b class="fg-blue">1° Parcela</b></legend>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>Valores dos recursos repassados Fundo a Fundo pelo FEAS: </b>
                                                                <div class="input-control text">
                                                                    <asp:TextBox ID="txtValorFEASPrimeiraParcela" runat="server" Text="0,00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>Data do repasse: </b>
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                Mês : 
                                                            <div class="input-control select">
                                                                <asp:DropDownList ID="ddlRepassePrimeiraParcela" runat="server">
                                                                    <asp:ListItem Selected="True" Text="[Selecione o mês]" Value="0" />
                                                                    <asp:ListItem Text="Janeiro" Value="1" />
                                                                    <asp:ListItem Text="Fevereiro" Value="2" />
                                                                    <asp:ListItem Text="Março" Value="3" />
                                                                    <asp:ListItem Text="Abril" Value="4" />
                                                                    <asp:ListItem Text="Maio" Value="5" />
                                                                    <asp:ListItem Text="Junho" Value="6" />
                                                                    <asp:ListItem Text="Julho" Value="7" />
                                                                    <asp:ListItem Text="Agosto" Value="8" />
                                                                    <asp:ListItem Text="Setembro" Value="9" />
                                                                    <asp:ListItem Text="Outubro" Value="10" />
                                                                    <asp:ListItem Text="Novembro" Value="11" />
                                                                    <asp:ListItem Text="Dezembro" Value="12" />
                                                                </asp:DropDownList>
                                                            </div>
                                                                Ano: 
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtAnoRepassePrimeiraParcela" runat="server" MaxLength="4" Width="50px" />
                                                            </div>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoRepassePrimeiraParcela" runat="server" FilterType="Numbers" TargetControlID="txtAnoRepassePrimeiraParcela" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                Valor programado para utilização em 2016:
                                                                <div class="input-control text">
                                                                    <asp:TextBox ID="txtValorProgramadoAnoAtual" runat="server" Text="0,00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                Valor programado para utilização em 2017:
                                                                <div class="input-control text">
                                                                    <asp:TextBox ID="txtValorProgramadoProximoAno" runat="server" Text="0,00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row" id="trRetratoFamilia" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>O município já concluiu as fases RETRATO DA FAMILIA, PLANO DE AÇÃO DO TERRITORIO e AGENDA DA FAMILIA?</b><br />
                                                    <asp:RadioButtonList ID="rblConcluiuRetratoFamilia" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Não" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="trInformativo" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>"O preenchimento das informações referentes à segunda parcela está condicionado à conclusão das etapas Retrato da Família, Plano de Ação do Território e Agenda da Família. Após concluí-la o
                                            município deve solicitar a reabertura do sistema PMASweb para complementação das informações correspondentes."
                                                    </b>
                                                </div>
                                            </div>
                                            
                                           <div class="row" id="trRecursosFinanceirosAbas" runat="server" visible="false">
                                               <div id="Quadrienal">
                                                   <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnLoadExercicio1_Click"></asp:Button>
                                                   <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnLoadExercicio2_Click"></asp:Button>
                                                   <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnLoadExercicio3_Click"></asp:Button>
                                                   <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnLoadExercicio4_Click"></asp:Button>
                                               </div>
                                           </div>

                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="hdfAno" runat="server" />


                                                    <div id="trRecursosFinanceirosExercicio1" runat="server">
                                                        <div class="row" id="trRecursosFinanceirosMunicipalExercicio1" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Municipal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFMASExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkFMASExercicio1_CheckedChanged" Text="FMAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFMASExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoMunicipalExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoMunicipalExercicio1_CheckedChanged" Text="Orçamento Municipal:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoMunicipalExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOutrosFundosMunicipaisExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosMunicipaisExercicio1_CheckedChanged" Text="Outros Fundos Municipais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosMunicipaisExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </td>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosEstadualExercicio1" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell" id="tdFEAS" runat="server">
                                                                        <asp:CheckBox ID="chkFEASExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkFEASExercicio1_CheckedChanged" Text="FEAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFEASExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoEstadualExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoEstadualExercicio1_CheckedChanged" Text="Orçamento Estadual:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoEstadualExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosEstaduaisExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosEstaduaisExercicio1_CheckedChanged" Text="Outros Fundos Estaduais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosEstaduaisExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosFederalExercicio1" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Federal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFNASExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkFNASExercicio1_CheckedChanged" Text="FNAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFNASExercicio1" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoFederalExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoFederalExercicio1_CheckedChanged" Text="Orçamento Federal:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoFederalExercicio1" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosFederaisExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosFederaisExercicio1_CheckedChanged" Text="Outros Fundos Nacionais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosFederaisExercicio1" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkIGDPBFExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDPAIFExercicio1_CheckedChanged" Text="IGD PBF:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDPBFExercicio1" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell colspan2">
                                                                        <asp:CheckBox ID="chkIGDSUASExercicio1" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDSUASExercicio1_CheckedChanged" Text="IGD SUAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDSUASExercicio1" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div id="trRecursosFinanceirosExercicio2" runat="server">
                                                        <div class="row" id="trRecursosFinanceirosMunicipalExercicio2" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Municipal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFMASExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkFMASExercicio2_CheckedChanged" Text="FMAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFMASExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoMunicipalExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoMunicipalExercicio2_CheckedChanged" Text="Orçamento Municipal:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoMunicipalExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOutrosFundosMunicipaisExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosMunicipaisExercicio2_CheckedChanged" Text="Outros Fundos Municipais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosMunicipaisExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </td>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosEstadualExercicio2" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell" id="Div1" runat="server">
                                                                        <asp:CheckBox ID="chkFEASExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkFEASExercicio2_CheckedChanged" Text="FEAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFEASExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoEstadualExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoEstadualExercicio2_CheckedChanged" Text="Orçamento Estadual:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoEstadualExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosEstaduaisExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosEstaduaisExercicio2_CheckedChanged" Text="Outros Fundos Estaduais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosEstaduaisExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosFederalExercicio2" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Federal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFNASExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkFNASExercicio2_CheckedChanged" Text="FNAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFNASExercicio2" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoFederalExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoFederalExercicio2_CheckedChanged" Text="Orçamento Federal:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoFederalExercicio2" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosFederaisExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosFederaisExercicio2_CheckedChanged" Text="Outros Fundos Nacionais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosFederaisExercicio2" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkIGDPBFExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDPAIFExercicio2_CheckedChanged" Text="IGD PBF:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDPBFExercicio2" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell colspan2">
                                                                        <asp:CheckBox ID="chkIGDSUASExercicio2" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDSUASExercicio2_CheckedChanged" Text="IGD SUAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDSUASExercicio2" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div id="trRecursosFinanceirosExercicio3" runat="server">
                                                        <div class="row" id="trRecursosFinanceirosMunicipalExercicio3" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Municipal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFMASExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkFMASExercicio3_CheckedChanged" Text="FMAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFMASExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoMunicipalExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoMunicipalExercicio3_CheckedChanged" Text="Orçamento Municipal:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoMunicipalExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOutrosFundosMunicipaisExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosMunicipaisExercicio3_CheckedChanged" Text="Outros Fundos Municipais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosMunicipaisExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </td>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosEstadualExercicio3" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell" id="Div2" runat="server">
                                                                        <asp:CheckBox ID="chkFEASExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkFEASExercicio3_CheckedChanged" Text="FEAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFEASExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoEstadualExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoEstadualExercicio3_CheckedChanged" Text="Orçamento Estadual:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoEstadualExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosEstaduaisExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosEstaduaisExercicio3_CheckedChanged" Text="Outros Fundos Estaduais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosEstaduaisExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosFederalExercicio3" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Federal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFNASExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkFNASExercicio3_CheckedChanged" Text="FNAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFNASExercicio3" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoFederalExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoFederalExercicio3_CheckedChanged" Text="Orçamento Federal:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoFederalExercicio3" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosFederaisExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosFederaisExercicio3_CheckedChanged" Text="Outros Fundos Nacionais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosFederaisExercicio3" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkIGDPBFExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDPAIFExercicio3_CheckedChanged" Text="IGD PBF:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDPBFExercicio3" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell colspan2">
                                                                        <asp:CheckBox ID="chkIGDSUASExercicio3" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDSUASExercicio3_CheckedChanged" Text="IGD SUAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDSUASExercicio3" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div id="trRecursosFinanceirosExercicio4" runat="server">
                                                        <div class="row" id="trRecursosFinanceirosMunicipalExercicio4" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Municipal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFMASExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkFMASExercicio4_CheckedChanged" Text="FMAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFMASExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoMunicipalExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoMunicipalExercicio4_CheckedChanged" Text="Orçamento Municipal:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoMunicipalExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOutrosFundosMunicipaisExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosMunicipaisExercicio4_CheckedChanged" Text="Outros Fundos Municipais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosMunicipaisExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </td>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosEstadualExercicio4" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell" id="Div3" runat="server">
                                                                        <asp:CheckBox ID="chkFEASExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkFEASExercicio4_CheckedChanged" Text="FEAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFEASExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoEstadualExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoEstadualExercicio4_CheckedChanged" Text="Orçamento Estadual:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoEstadualExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosEstaduaisExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosEstaduaisExercicio4_CheckedChanged" Text="Outros Fundos Estaduais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosEstaduaisExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>

                                                        <div class="row" id="trRecursosFinanceirosFederalExercicio4" runat="server">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Federal</b></legend>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkFNASExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkFNASExercicio4_CheckedChanged" Text="FNAS:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtFNASExercicio4" runat="server" Enabled="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOrcamentoFederalExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrcamentoFederalExercicio4_CheckedChanged" Text="Orçamento Federal:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOrcamentoFederalExercicio4" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkOutrosFundosFederaisExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutrosFundosFederaisExercicio4_CheckedChanged" Text="Outros Fundos Nacionais:" />
                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtOutrosFundosFederaisExercicio4" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row cells3">
                                                                    <div class="cell">
                                                                        <asp:CheckBox ID="chkIGDPBFExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDPAIFExercicio4_CheckedChanged" Text="IGD PBF:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDPBFExercicio4" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="cell colspan2">
                                                                        <asp:CheckBox ID="chkIGDSUASExercicio4" runat="server" AutoPostBack="true" OnCheckedChanged="chkIGDSUASExercicio4_CheckedChanged" Text="IGD SUAS:" />

                                                                        <br />
                                                                        <div class="input-control text">
                                                                            <asp:TextBox ID="txtIGDSUASExercicio4" runat="server" Enabled="false" />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnExercicio1" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnExercicio2" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnExercicio3" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnExercicio4" EventName="Click" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoMunicipalExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoMunicipalExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoMunicipalExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoMunicipalExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkFMASExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFMASExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFMASExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFMASExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosMunicipaisExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosMunicipaisExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosMunicipaisExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosMunicipaisExercicio4" EventName="CheckedChanged" />



                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoEstadualExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoEstadualExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoEstadualExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoEstadualExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkFEASExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFEASExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFEASExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFEASExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosEstaduaisExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosEstaduaisExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosEstaduaisExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosEstaduaisExercicio4" EventName="CheckedChanged" />



                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoFederalExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoFederalExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoFederalExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOrcamentoFederalExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkFNASExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFNASExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFNASExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkFNASExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosFederaisExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosFederaisExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosFederaisExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkOutrosFundosFederaisExercicio4" EventName="CheckedChanged" />


                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDPBFExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDPBFExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDPBFExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDPBFExercicio4" EventName="CheckedChanged" />

                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDSUASExercicio1" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDSUASExercicio2" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDSUASExercicio3" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="chkIGDSUASExercicio4" EventName="CheckedChanged" />




                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </fieldset>


                                    </div>

                                    </fieldset>
                                </div>
                                <div class="row">
                                    <div class="cell" align="center">
                                        <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" SkinID="button-save" Text="Salvar" Width="89px" />
                                        &nbsp;<asp:Button ID="btnVoltar" runat="server" PostBackUrl="~/BlocoIII/CProgramasProjetos.aspx" Text="Voltar" />
                                    </div>
                                </div>

                                <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                    width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                    <tr>
                                        <td style="padding: 15px 10px 2px 15px">
                                            <span class="mif-warning mif-2x"></span>
                                            <%-- <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />--%><b style='color: #000000 !important'>Verifique
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
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
