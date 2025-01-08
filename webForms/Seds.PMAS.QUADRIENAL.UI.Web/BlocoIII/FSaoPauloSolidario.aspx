<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FSaoPauloSolidario.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FSaoPauloSolidario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="700" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">49</b> <b>- III - Programas: Transferência de Renda - São
                                        Paulo Solidário</b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro49" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="bg-alternative">
                                <td colspan="2">
                                    <b>Nome do Programa:</b><br />
                                    <asp:TextBox ID="txtNome" runat="server" MaxLength="120" Width="490px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>Objetivo:</b><br />
                                    <asp:TextBox ID="txtObjetivo" runat="server" Width="668px" TextMode="MultiLine" Height="51px"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="bg-alternative">
                                <td colspan="2">
                                    <b>Beneficiários:</b><br />
                                    <asp:DropDownList ID="ddlBeneficiarios" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2">
                                    <b>Em que fase do Programa o município está?</b><br />
                                    <asp:RadioButtonList ID="rblFase" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblFase_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="Busca Ativa" Selected="True" />
                                        <asp:ListItem Value="2" Text="Agenda da Família" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend><b class="titulo">Informações sobre a Busca ativa</b></legend>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <fieldset>
                                                        <legend><b>Período de Realização:</b></legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <b>Data de Início:</b> (mês/ano)<br />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                Mês :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlMesInicio" runat="server">
                                                                                    <asp:ListItem Value="0" Text="[Selecione o mês]" Selected="True" />
                                                                                    <asp:ListItem Value="1" Text="Janeiro" />
                                                                                    <asp:ListItem Value="2" Text="Fevereiro" />
                                                                                    <asp:ListItem Value="3" Text="Março" />
                                                                                    <asp:ListItem Value="4" Text="Abril" />
                                                                                    <asp:ListItem Value="5" Text="Maio" />
                                                                                    <asp:ListItem Value="6" Text="Junho" />
                                                                                    <asp:ListItem Value="7" Text="Julho" />
                                                                                    <asp:ListItem Value="8" Text="Agosto" />
                                                                                    <asp:ListItem Value="9" Text="Setembro" />
                                                                                    <asp:ListItem Value="10" Text="Outubro" />
                                                                                    <asp:ListItem Value="11" Text="Novembro" />
                                                                                    <asp:ListItem Value="12" Text="Dezembro" />
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                Ano:
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAnoInicio" runat="server" MaxLength="4" Width="50px" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoInicio" runat="server"
                                                                                    TargetControlID="txtAnoInicio" FilterType="Numbers" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <b>Data de término:</b> (mês/ano)<br />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                Mês :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlMesTermino" runat="server">
                                                                                    <asp:ListItem Value="0" Text="[Selecione o mês]" Selected="True" />
                                                                                    <asp:ListItem Value="1" Text="Janeiro" />
                                                                                    <asp:ListItem Value="2" Text="Fevereiro" />
                                                                                    <asp:ListItem Value="3" Text="Março" />
                                                                                    <asp:ListItem Value="4" Text="Abril" />
                                                                                    <asp:ListItem Value="5" Text="Maio" />
                                                                                    <asp:ListItem Value="6" Text="Junho" />
                                                                                    <asp:ListItem Value="7" Text="Julho" />
                                                                                    <asp:ListItem Value="8" Text="Agosto" />
                                                                                    <asp:ListItem Value="9" Text="Setembro" />
                                                                                    <asp:ListItem Value="10" Text="Outubro" />
                                                                                    <asp:ListItem Value="11" Text="Novembro" />
                                                                                    <asp:ListItem Value="12" Text="Dezembro" />
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                Ano:
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAnoTermino" runat="server" MaxLength="4" Width="50px" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoTermino" runat="server"
                                                                                    TargetControlID="txtAnoTermino" FilterType="Numbers" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Quais os órgãos que
                                                        <asp:Label ID="lblOrgaoExecutam" runat="server" Text="executam" />
                                                        &nbsp;essa fase do programa?</b><br />
                                                    <asp:CheckBoxList ID="chkOrgaosExecutores" runat="server" RepeatDirection="Horizontal"
                                                        Width="583px">
                                                        <asp:ListItem Value="1" Text="Órgão Gestor da Assistência Social"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="CRAS"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Unidade Socioassistencial privada"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <fieldset>
                                                        <legend><b>Articulações promovidas para execução da Busca Ativa:</b></legend><b>
                                                            <asp:Label ID="lblParceriasExistem" runat="server" Text="Existem" />
                                                            &nbsp;parcerias estabelecidas, formal ou informalmente, para a execução desta fase?</b><br />
                                                        <asp:RadioButtonList ID="rblParcerias" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="true" OnSelectedIndexChanged="rblParcerias_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="Sim" />
                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                        </asp:RadioButtonList>
                                                        <table width="100%" id="tbParcerias" runat="server" visible="false">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <b>Informe com quem foram formalizadas as parcerias para a execução desta fase:</b><br />
                                                                    <asp:DropDownList ID="ddlParceria" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="350">
                                                                    <b>Nome do Órgão:</b><br />
                                                                    <asp:TextBox ID="txtNomeOrgao" runat="server" Width="340px" />
                                                                </td>
                                                                <td>
                                                                    <b>Tipo da Parceria:</b><br />
                                                                    <asp:DropDownList ID="ddlTipoParceria" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="btnAdicionarParceria" runat="server" SkinID="button-add" Text="Adicionar"
                                                                        Width="100px" OnClick="btnAdicionarParceria_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:ListView ID="lstParcerias" runat="server" OnItemDataBound="lstParcerias_ItemDataBound"
                                                                        OnItemCommand="lstParcerias_ItemCommand">
                                                                        <LayoutTemplate>
                                                                            <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                                                                cellpadding="0" border="0">
                                                                                <thead>
                                                                                    <tr class="ui-jqgrid-labels">
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;">
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="220">
                                                                                            Nome do Órgão
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="250">
                                                                                            Parcerias
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="125">
                                                                                            Tipo da Parceria
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="50">
                                                                                            Excluir
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr class="jqgfirstrow" style="height: auto;">
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="ui-widget-content row">
                                                                                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                                                                                    <asp:Label ID="lblSequencia" runat="server" />
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "NomeOrgao") %>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Parceria.Nome") %>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoParceria.Nome") %>
                                                                                </td>
                                                                                <td class="align-center">
                                                                                    <asp:ImageButton ID="btnExcluirParceria" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                        CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a parceria?');" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Informe outras fontes de recursos financeiros para execução dessa ação além do FEAS:</b>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <fieldset>
                                                                    <legend><b>Municipal</b></legend>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                FMAS:<br />
                                                                                <asp:TextBox ID="txtFMAS" runat="server" Text="0,00" />
                                                                            </td>
                                                                            <td>
                                                                                Orçamento Municipal:<br />
                                                                                <asp:TextBox ID="txtOrcamentoMunicipal" runat="server" Text="0,00" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                            <td>
                                                                <fieldset>
                                                                    <legend><b>Federal</b></legend>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                FNAS:<br />
                                                                                <asp:TextBox ID="txtFNAS" runat="server" Text="0,00" />
                                                                            </td>
                                                                            <td>
                                                                                IGD-PBF:<br />
                                                                                <asp:TextBox ID="txtIGDPBF" runat="server" Text="0,00" />
                                                                            </td>
                                                                            <td>
                                                                                IGD-SUAS:<br />
                                                                                <asp:TextBox ID="txtIGDSUAS" runat="server" Text="0,00" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr id="trAgendaFamilia" runat="server" visible="false">
                                <td colspan="2">
                                    <fieldset>
                                        <legend><b class="titulo">Agenda da Família</b></legend>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <b>Quantas famílias assinaram o termo da Agenda da Família em 2012?</b><br />
                                                    <asp:TextBox ID="txtNumeroFamilias" runat="server" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroFamilias"
                                                        runat="server" TargetControlID="txtNumeroFamilias" FilterType="Numbers" />
                                                </td>
                                                <td>
                                                    <b>Quantas famílias assinaram o termo da Agenda da Família em 2013?</b><br />
                                                    <asp:TextBox ID="txtNumeroFamilias2013" runat="server" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroFamilias2013"
                                                        runat="server" TargetControlID="txtNumeroFamilias2013" FilterType="Numbers" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Quais os órgãos que executam essa fase do programa?</b><br />
                                                    <asp:CheckBoxList ID="chkOrgaosAgendaFamilia" runat="server" RepeatDirection="Horizontal"
                                                        Width="317px">
                                                        <asp:ListItem Text="Órgão Gestor da Assistência Social" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="CRAS" Value="2"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset>
                                                        <legend><b>Articulações promovidas para execução da Agenda da Família:</b></legend>
                                                        <b>
                                                            <asp:Label ID="Label1" runat="server" Text="Existem" />
                                                            &nbsp;parcerias estabelecidas, formal ou informalmente, para a execução desta fase?</b><br />
                                                        <asp:RadioButtonList ID="rblParceriasAgendaFamilia" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="true" OnSelectedIndexChanged="rblParceriasAgendaFamilia_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="Sim" />
                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                        </asp:RadioButtonList>
                                                        <table width="100%" id="tbParceriasAgendaFamilia" runat="server" visible="false">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <b>Informe com quem foram formalizadas as parcerias para a execução desta fase:</b><br />
                                                                    <asp:DropDownList ID="ddlParceriaAgendaFamilia" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="350">
                                                                    <b>Nome do Órgão:</b><br />
                                                                    <asp:TextBox ID="txtNomeOrgaoAgendaFamilia" runat="server" Width="340px" />
                                                                </td>
                                                                <td>
                                                                    <b>Tipo da Parceria:</b><br />
                                                                    <asp:DropDownList ID="ddlTipoParceriaAgendaFamilia" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="btnAdicionarParceriaAgendaFamilia" runat="server" SkinID="button-add"
                                                                        Text="Adicionar" Width="100px" OnClick="btnAdicionarParceriaAgendaFamilia_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:ListView ID="lstParceriasAgendaFamilia" runat="server" OnItemDataBound="lstParcerias_ItemDataBound"
                                                                        OnItemCommand="lstParceriasAgendaFamilia_ItemCommand">
                                                                        <LayoutTemplate>
                                                                            <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                                                                cellpadding="0" border="0">
                                                                                <thead>
                                                                                    <tr class="ui-jqgrid-labels">
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;">
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="220">
                                                                                            Nome do Órgão
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="250">
                                                                                            Parcerias
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="125">
                                                                                            Tipo da Parceria
                                                                                        </th>
                                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="50">
                                                                                            Excluir
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr class="jqgfirstrow" style="height: auto;">
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                        <td style="height: 0px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="ui-widget-content row">
                                                                                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                                                                                    <asp:Label ID="lblSequencia" runat="server" />
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "NomeOrgao") %>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Parceria.Nome") %>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoParceria.Nome") %>
                                                                                </td>
                                                                                <td class="align-center">
                                                                                    <asp:ImageButton ID="btnExcluirParceria" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                        CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a parceria?');" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Informe outras fontes de recursos financeiros para execução dessa ação além do FEAS:</b>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <fieldset>
                                                                    <legend><b>Municipal</b></legend>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                FMAS:<br />
                                                                                <asp:TextBox ID="txtFMASAgendaFamilia" runat="server" Text="0,00" />
                                                                            </td>
                                                                            <td>
                                                                                Orçamento Municipal:<br />
                                                                                <asp:TextBox ID="txtOrcamentoMunicipalAgendaFamilia" runat="server" Text="0,00" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                            <td>
                                                                <fieldset>
                                                                    <legend><b>Federal</b></legend>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                FNAS:<br />
                                                                                <asp:TextBox ID="txtFNASAgendaFamilia" runat="server" Text="0,00" />
                                                                            </td>
                                                                            <td>
                                                                                IGD-PBF:<br />
                                                                                <asp:TextBox ID="txtIGDPBFAgendaFamilia" runat="server" Text="0,00" />
                                                                            </td>
                                                                            <td>
                                                                                IGD-SUAS:<br />
                                                                                <asp:TextBox ID="txtIGDSUASAgendaFamilia" runat="server" Text="0,00" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left">
                                                <b>De acordo com Protocolo de Gestão Integrada de Serviços, Benefícios e Transferência
                                                    de Renda no Âmbito do SUAS - Resolução CIT Nº 7 de 10/09/2009 - o beneficiário está
                                                    sendo atendido na rede de serviços socioassistenciais?</b><br />
                                                <asp:RadioButtonList ID="rblIntegracaoRede" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Sim" Value="1" />
                                                    <asp:ListItem Selected="True" Text="Não" Value="0" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trInformacoesTranferenciaRenda" runat="server" visible="false">
                                            <td>
                                                <fieldset>
                                                    <legend><b class="titulo">Informações sobre a Transferência de Renda</b></legend>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <b>Previsão anual de número de beneficiários e valor de repasse</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Meta pactuada:</b><br />
                                                                <asp:TextBox ID="txtMeta" runat="server" />
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtMeta" runat="server"
                                                                    TargetControlID="txtMeta" FilterType="Numbers" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Previsão anual do valor de repasse:</b><br />
                                                                <asp:TextBox ID="txtPrevisaoAnualRepasse" runat="server" Width="150px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" SkinID="button-save"
                                        Text="Salvar" Width="89px" />
                                    &nbsp;<asp:Button ID="btnVoltar" runat="server" PostBackUrl="~/BlocoIII/CTransferenciaRenda.aspx"
                                        Text="Voltar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="700" align="center" border="0">
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
            <asp:HiddenField ID="hdfTipoTransferenciaRenda" runat="server" Value="8" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
