<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FAvaliacao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVII.FAvaliacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form>
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>7.3 - Avaliação</b>
                            <a href="#" runat="server" id="linkAlteracoesQuadro70" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Avaliação">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>O município realiza avaliação das ações de Assistência Social?</b><br />
                                            <asp:RadioButtonList ID="rblAvalia" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rblAvalia_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" id="trNao" runat="server" visible="true">
                                        <div class="cell">
                                            <b>Indique os motivos de não ser realizada avaliação?</b><br />
                                            <asp:CheckBoxList ID="chkMotivos" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row" id="trSim" runat="server" visible="false">
                                        <div class="cell">
                                            <b>Quais são os objetivos desta avaliação:</b><br />
                                            <asp:CheckBoxList ID="chkObjetivos" runat="server" />
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <b>Utiliza os dados do monitoramento para a avaliação?</b><br />
                                                <asp:RadioButtonList ID="rblDadosMonitoramento" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Sim" />
                                                    <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell" align="left">
                                                <b>Quais procedimentos e métodos são empregados na avaliação:</b><br />
                                                <asp:CheckBoxList ID="chkProcedimentos" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell" align="left">
                                                <b>Quem realiza a avaliação da rede socioassistencial?</b>
                                                <br />
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkOrgaoGestor" runat="server" Text="O próprio órgão gestor da assistência social"
                                                            AutoPostBack="True" OnCheckedChanged="chkOrgaoGestor_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="row" id="trOrgaoGestor" runat="server" visible="false">
                                                    <div class="cell" style="padding-left: 20px;">
                                                        <asp:CheckBoxList ID="chkEspecificacaoOrgaoGestor" runat="server">
                                                            <asp:ListItem Value="EquipeEspecifica" Text="Através de equipe específica" />
                                                            <asp:ListItem Value="EquipeTecnicoProtecaoSocial" Text="Através de equipe ou técnico das proteções sociais" />
                                                            <asp:ListItem Value="TecnicosOutrasEquipes" Text="Através de técnicos de outras equipes" />
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkServicoTerceirizado" runat="server" Text="Através da contratação de serviço terceirizado (como por exemplo: empresas, universidades, consultorias, etc.)"
                                                            AutoPostBack="True" OnCheckedChanged="chkOrgaoGestor_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <b>O município utiliza-se de avaliações realizadas independentemente por outros órgãos?</b>
                                                <br />
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkGovernoEstadual" runat="server" Text="Governo Estadual" AutoPostBack ="true" OnCheckedChanged="chkGovernoEstadual_CheckedChanged"/>
                                                    </div>
                                                </div>
                                                <div class="row" id="trGovernoEstadual" runat="server" visible="false">
                                                    <div class="cell" style="padding-left: 20px;">
                                                        <asp:CheckBoxList ID="chkSecretariasGovernoEstadual" runat="server" AutoPostBack="true" onselectedindexchanged="chkSecretariasGovernoEstadual_SelectedIndexChanged">
                                                            <asp:ListItem Value="SedsDrads" Text="SEDS/DRADS" />
                                                            <asp:ListItem Value="TribunalDeContas" Text="Tribunal de Contas do Estado" />
                                                            <asp:ListItem Value="SecretariaDaFazenda" Text="Secretaria da Fazenda" />
                                                            <asp:ListItem Value="MinisterioPublico" Text="Ministério Público" />
                                                            <asp:ListItem Value="DefensoriaPublica" Text="Defensoria Pública" />
                                                            <asp:ListItem Value="Outros" Text="Outros" />
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="cell" style="padding-left: 20px;" id="trOutros" runat="server">
                                                        <%--<fieldset class="border-blue" style="width:50%">--%>
                                                            <%--<legend class="lgnd"><b class="fg-blue">Quais órgãos ?</b></legend>--%>
                                                            <asp:TextBox runat="server" ID="txtOutros" TextMode="MultiLine"  Height="64px" Width="50%" placeholder="Digite quais...."></asp:TextBox>
                                                        <%--</fieldset>--%>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkGovernoFederal" runat="server" Text="Governo Federal (MDS, Tribunal de Contas da União, Controladoria Geral da União, outros ministérios)" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkConselhosMunicipais" runat="server" Text="Conselhos Municipais"
                                                            AutoPostBack="True" OnCheckedChanged="chkConselhosMunicipais_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="row" id="trConselhosMunicipais" runat="server" visible="false">
                                                    <div class="cell" style="padding-left: 20px;">
                                                        <asp:CheckBoxList ID="chkEspecificacaoConselhosMunicipais" runat="server">
                                                            <asp:ListItem Value="CMAS" Text="CMAS" />
                                                            <asp:ListItem Value="CMDCA" Text="CMDCA" />
                                                            <asp:ListItem Value="OutrosConselhos" Text="Outros Conselhos" />
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkEmpresasPrivadas" runat="server" Text="Empresas privadas de pesquisa" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkONGs" runat="server" Text="ONGs, Associações ou Fundações" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
                        </div>
                    </div>
                </div>
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FMonitoramento.aspx">
                            <span class="mif-arrow-left" />

                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FAspectosGerais.aspx">Próximo
                                                          <span class="mif-arrow-right" /></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
