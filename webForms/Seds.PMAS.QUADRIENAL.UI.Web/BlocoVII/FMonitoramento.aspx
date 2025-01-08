<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FMonitoramento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVII.FMonitoramento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmMonitoramento">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            7.2 - Monitoramento
                             <a href="#" runat="server" id="linkAlteracoesQuadro69" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>&nbsp;
                            <span class="mif-meter icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Monitoramento">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>O município realiza monitoramento das ações de Assistência Social?</b><br />
                                            <asp:RadioButtonList ID="rblMonitora" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblMonitora_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" id="trSim" runat="server" visible="false">
                                        <div class="cell">
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Como é operacionalizado esse monitoramento?</b>
                                                    <br />
                                                    <asp:CheckBox ID="chkOrgaoGestor" runat="server" Text="O próprio órgão gestor da assistência social"
                                                        AutoPostBack="True" OnCheckedChanged="chkOrgaoGestor_CheckedChanged" />

                                                </div>
                                            </div>
                                            <div class="row" id="trOrgaoGestor" runat="server" visible="false">
                                                <div class="cell" style="padding-left: 20px;">
                                                    <asp:RadioButtonList ID="rblOrgaoGestor" runat="server">
                                                        <asp:ListItem Value="EquipeEspecifica" Text="Através de equipe específica" />
                                                        <asp:ListItem Value="EquipeTecnicoProtecaoSocial" Text="Através de equipe ou técnico das proteções sociais" />
                                                        <asp:ListItem Value="TecnicosOutrasEquipes" Text="Através de técnicos de outras equipes" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkServicoTerceirizado" runat="server" Text="Através da contratação de serviço terceirizado (como por exemplo: empresas, universidades, consultorias, etc.)"
                                                        AutoPostBack="True" OnCheckedChanged="chkOrgaoGestor_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>As informações do PMAS são objeto de monitoramento?</b><br />
                                                    <asp:RadioButtonList ID="rblPMASMonitoramento" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="left">
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Qual a periodicidade e os focos do monitoramento realizado na rede socioassistencial:</b><br />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkRedePublica" runat="server" Text="Rede executora direta" AutoPostBack="true"
                                                                OnCheckedChanged="chkRedePublica_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trFocosRedePublica" runat="server" visible="false">
                                                        <div class="cell" style="padding-left: 20px;">
                                                            Focos do monitoramento:
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkFocoRedePublica1" runat="server" Text="Atendimento da demanda existente"
                                                                AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica1_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica1" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica1" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica1" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:CheckBox ID="chkFocoRedePublica2" runat="server" Text="Execução das atividades previstas"
                                                                        AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica2_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica2" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica2" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica2" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:CheckBox ID="chkFocoRedePublica3" runat="server" Text="Frequência e evasão de usuários"
                                                                        AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica3_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica3" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica3" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica3" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:CheckBox ID="chkFocoRedePublica4" runat="server" Text="Adequação e qualificação dos recursos humanos"
                                                                        AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica4_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica4" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica4" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica4" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:CheckBox ID="chkFocoRedePublica5" runat="server" Text="Aplicação e gestão dos recursos financeiros"
                                                                        AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica5_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica5" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica5" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica5" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:CheckBox ID="chkFocoRedePublica6" runat="server" Text="Adequação do espaço físico e materiais"
                                                                        AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica6_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica6" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica6" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica6" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:CheckBox ID="chkFocoRedePublica7" runat="server" Text="Alcance dos objetivos dos programas/projetos e serviços"
                                                                        AutoPostBack="true" OnCheckedChanged="chkFocoRedePublica7_CheckedChanged" />
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trPeriodicidadeFocoRedePublica7" runat="server" visible="false">
                                                                <div class="cell" style="padding-left: 20px;">
                                                                    <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePublica7" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="Mensal" />
                                                                        <asp:ListItem Value="2" Text="Bimestral" />
                                                                        <asp:ListItem Value="3" Text="Semestral" />
                                                                        <asp:ListItem Value="4" Text="Anual" />
                                                                    </asp:RadioButtonList>
                                                                    <asp:HiddenField ID="hdfFocoRedePublica7" runat="server" Value="0" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkRedePrivada" runat="server" Text="Rede executora indireta" AutoPostBack="true"
                                                                OnCheckedChanged="chkRedePrivada_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trFocosRedePrivada" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="cell" style="padding-left: 20px;">
                                                               <b> Focos do monitoramento:</b>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell"  style="padding-left: 30px;">
                                                                <asp:CheckBox ID="chkFocoRedePrivada1" runat="server" Text="Atendimento da demanda existente"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada1_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada1" runat="server" visible="false">
                                                            <td style="padding-left: 40px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada1" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada1" runat="server" Value="0" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkFocoRedePrivada2" runat="server" Text="Execução das atividades previstas"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada2_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada2" runat="server" visible="false">
                                                            <td style="padding-left: 20px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada2" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada2" runat="server" Value="0" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkFocoRedePrivada3" runat="server" Text="Frequência e evasão de usuários"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada3_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada3" runat="server" visible="false">
                                                            <div class="cell" style="padding-left: 20px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada3" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada3" runat="server" Value="0" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkFocoRedePrivada4" runat="server" Text="Adequação e qualificação dos recursos humanos"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada4_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada4" runat="server" visible="false">
                                                            <div class="cell" style="padding-left: 20px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada4" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada4" runat="server" Value="0" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkFocoRedePrivada5" runat="server" Text="Aplicação e gestão dos recursos financeiros"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada5_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada5" runat="server" visible="false">
                                                            <div class="cell" style="padding-left: 20px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada5" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada5" runat="server" Value="0" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkFocoRedePrivada6" runat="server" Text="Adequação do espaço físico e materiais"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada6_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada6" runat="server" visible="false">
                                                            <div class="cell" style="padding-left: 20px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada6" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada6" runat="server" Value="0" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkFocoRedePrivada7" runat="server" Text="Alcance dos objetivos dos programas/projetos e serviços"
                                                                    AutoPostBack="true" OnCheckedChanged="chkFocoRedePrivada7_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trPeriodicidadeFocoRedePrivada7" runat="server" visible="false">
                                                            <div class="cell" style="padding-left: 20px;">
                                                                <asp:RadioButtonList ID="rblPeriodicidadeFocoRedePrivada7" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Mensal" />
                                                                    <asp:ListItem Value="2" Text="Bimestral" />
                                                                    <asp:ListItem Value="3" Text="Semestral" />
                                                                    <asp:ListItem Value="4" Text="Anual" />
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="hdfFocoRedePrivada7" runat="server" Value="0" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkNaoHaMonitoramento" runat="server" Text="Não há monitoramento da rede socioassistencial"
                                                                AutoPostBack="true" OnCheckedChanged="chkNaoHaMonitoramento_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Quais procedimentos e instrumentos são utilizados no monitoramento das ações?</b><br />
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkProcedimento1" runat="server" Text="Envio de informações pelos serviços que compõem a rede socioassistencial"
                                                                AutoPostBack="true" OnCheckedChanged="chkProcedimento1_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trInstrumentos1" runat="server" visible="false">
                                                        <div class="cell" style="padding-left: 20px;">
                                                            <asp:CheckBoxList ID="chkInstrumentos1" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkProcedimento2" runat="server" Text="Reuniões/grupos de discussão com executores"
                                                                AutoPostBack="true" OnCheckedChanged="chkProcedimento2_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trInstrumentos2" runat="server" visible="false">
                                                        <div class="cell" style="padding-left: 20px;">
                                                            <asp:CheckBoxList ID="chkInstrumentos2" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkProcedimento3" runat="server" Text="Reuniões/grupos de discussão com usuários"
                                                                AutoPostBack="true" OnCheckedChanged="chkProcedimento3_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trInstrumentos3" runat="server" visible="false">
                                                        <div class="cell" style="padding-left: 20px;">
                                                            <asp:CheckBoxList ID="chkInstrumentos3" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:CheckBox ID="chkProcedimento4" runat="server" Text="Visitas de supervisão" AutoPostBack="true"
                                                                OnCheckedChanged="chkProcedimento4_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="row" id="trInstrumentos4" runat="server" visible="false">
                                                        <div class="cell" style="padding-left: 20px;">
                                                            <asp:CheckBoxList ID="chkInstrumentos4" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>As informações de monitoramento são sistematizadas?</b><br />
                                                    <asp:RadioButtonList ID="rblInformacoesSistematizadas" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Os resultados de monitoramento são divulgados?</b><br />
                                                    <asp:RadioButtonList ID="rblResultados" runat="server" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="rblResultados_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="trMeiosDivulgacao" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Quais os meios de divulgação?</b><br />
                                                    <asp:CheckBoxList ID="chkMeiosDivulgacao" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="trNao" runat="server" visible="true">
                                        <div class="cell">
                                            <b>Se o município ainda não realiza monitoramento, pretende realizar no próximo ano?</b><br />
                                            <asp:RadioButtonList ID="rblProximoAno" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
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
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FVigilancia.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FAvaliacao.aspx">Próximo
                             <span class="mif-arrow-right"></span></a>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
