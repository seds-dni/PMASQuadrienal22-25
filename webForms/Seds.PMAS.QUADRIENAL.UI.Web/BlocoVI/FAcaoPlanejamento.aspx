<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FAcaoPlanejamento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI.FAcaoPlanejamento" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <script src="../Scripts/Util.js" type="text/javascript"></script>
            <form name="frmOrgaoGestor">
                <div class="accordion">
                    <div class="frame active" id="fraOrgaoGestor" runat="server">
                        <div class="heading">
                            6.3 - Planejamento das ações <a href="#" runat="server" id="linkAlteracoesQuadro61" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="ações planejadas">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Principal eixo da ação:</b><br />
                                            <div class="input-control select mid-size">
                                                <asp:DropDownList ID="ddlEixo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEixo_SelectedIndexChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Identificação da ação:</b><br />
                                            <div class="input-control select mid-size">
                                                <asp:DropDownList ID="ddlAcao" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcao_SelectedIndexChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Denominação da ação:</b><br />
                                            <div class="input-control text mid-size">
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="50" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Principal objetivo a ser alcançado:</b><br />
                                            <div class="input-control textarea mid-size">
                                                <asp:TextBox ID="txtObjetivos" runat="server" MaxLength="500" TextMode="MultiLine"
                                                    Height="70px" />
                                            </div>
                                            <br />
                                            <skm:TextBoxCounter ID="NameCounterObjetivos" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
                                                Font-Size="11px" TextBoxControlId="txtObjetivos" MaxCharacterLength="500" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Descrição (Definição da ação, Etapas, Metodologia e Estratégia):</b><br />
                                            <div class="input-control textarea mid-size">
                                                <asp:TextBox ID="txtDescricao" runat="server" MaxLength="1000" TextMode="MultiLine"
                                                    Height="100px" />
                                            </div>
                                            <br />
                                            <skm:TextBoxCounter ID="NameCounterDescricao" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 1000 caracteres."
                                                Font-Size="11px" TextBoxControlId="txtDescricao" MaxCharacterLength="1000" />
                                        </div>
                                    </div>
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Estimativas</b></legend>
                                        <fieldset class="border-blue">
                                            <legend class="lgnd fg-blue">Estimativa de custo total desta ação:</legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtEstimativaCusto" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="border-blue">
                                            <legend class="lgnd fg-blue">Período previsto para realização desta ação:</legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Início:</b>
                                                </div>
                                                <div class="cell">
                                                    <b>Término:</b>
                                                </div>
                                            </div>
                                            <div class="row cells4">
                                                <div class="cell">
                                                    Mês:
                                                    <br />
                                                    <div class="input-control select">
                                                        <asp:DropDownList ID="ddlMesPrevistoInicio" runat="server">
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
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    Ano:<br />
                                                    <div class="input-control text">
                                                        <asp:DropDownList ID="ddlAnoPrevistoInicio" runat="server">
                                                            <asp:ListItem Value="0" Text="[Selecione o Ano]" Selected="True" />
                                                            <asp:ListItem Value="2021" Text="2021" />
                                                            <asp:ListItem Value="2022" Text="2022" />
                                                            <asp:ListItem Value="2023" Text="2023" />
                                                            <asp:ListItem Value="2024" Text="2024" />
                                                            <asp:ListItem Value="2025" Text="2025" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    Mês:
                                                    <br />
                                                    <div class="input-control select">
                                                        <asp:DropDownList ID="ddlMesPrevistoTermino" runat="server">
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
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    Ano:<br />
                                                    <div class="input-control text">
                                                        <asp:DropDownList ID="ddlAnoPrevistoTermino" runat="server">
                                                            <asp:ListItem Value="0" Text="[Selecione o Ano]" Selected="True" />
                                                            <asp:ListItem Value="2022" Text="2022" />
                                                            <asp:ListItem Value="2023" Text="2023" />
                                                            <asp:ListItem Value="2024" Text="2024" />
                                                            <asp:ListItem Value="2025" Text="2025" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Outros envolvidos na realização desta ação:</b><br />
                                            <div class="input-control textarea mid-size">
                                                <asp:TextBox ID="txtOutrosEnvolvidos" runat="server" MaxLength="150" TextMode="MultiLine" />
                                            </div>
                                            <br />
                                            <skm:TextBoxCounter ID="NameCounterEnvolvidos" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 150 caracteres."
                                                Font-Size="11px" TextBoxControlId="txtOutrosEnvolvidos" MaxCharacterLength="150" />
                                        </div>
                                    </div>

                                    <div class="row" id="trRecursosReprogramados" runat="server">

                                    <div class="row">
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Previsão da(s) fonte(s) e valores dos recursos financeiros
                                            para a execução desta ação:</b></legend>
                                        É necessário informar a(s) fonte(s) de recursos prevista(s) para a execução desta
                                        ação, sendo opcional a informação da previsão dos valores destes recursos.
                                        <br />
                                        <br />

                                        <div class="row" id="trRecursosFinanceirosAbas" runat="server">
                                               <div id="Quadrienal">
                                                   <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnExercicio1_Click" ></asp:Button>
                                                   <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnExercicio2_Click" ></asp:Button>
                                                   <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnExercicio3_Click" ></asp:Button>
                                                   <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnExercicio4_Click" ></asp:Button>
                                               </div>
                                         </div>

                                        <br />

                                        <fieldset class="border-blue">
                                            <legend class="lgnd fg-blue">Municipal</legend>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkFMAS" runat="server" Text="FMAS:" AutoPostBack="true" OnCheckedChanged="chkFMAS_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtFMAS" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkOrcamentoMunicipal" runat="server" Text="Orçamento Municipal:"
                                                        AutoPostBack="true" OnCheckedChanged="chkOrcamentoMunicipal_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtOrcamentoMunicipal" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkOutrosFundosMunicipais" runat="server" Text="Outros Fundos Municipais:"
                                                        AutoPostBack="true" OnCheckedChanged="chkOutrosFundosMunicipais_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtOutrosFundosMunicipais" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="border-blue">
                                            <legend class="lgnd fg-blue">Estadual</legend>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkFEAS" runat="server" Text="FEAS:" AutoPostBack="true" OnCheckedChanged="chkFEAS_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtFEAS" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkOrcamentoEstadual" runat="server" Text="Orçamento Estadual:"
                                                        AutoPostBack="true" OnCheckedChanged="chkOrcamentoEstadual_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtOrcamentoEstadual" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkOutrosFundosEstaduais" runat="server" Text="Outros Fundos Estaduais:"
                                                        AutoPostBack="true" OnCheckedChanged="chkOutrosFundosEstaduais_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtOutrosFundosEstaduais" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkFEASReprogramado" runat="server" Text="FEAS - Reprogramação Ano Anterior:" AutoPostBack="true" OnCheckedChanged="chkFEASReprogramado_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtFEASReprogramado" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset class="border-blue">
                                            <legend class="lgnd fg-blue">Federal</legend>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkFNAS" runat="server" Text="FNAS:" AutoPostBack="true" OnCheckedChanged="chkFNAS_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtFNAS" runat="server" Enabled="false" />
                                                    </div>
                                                </div>

                                                <div class="cell">
                                                    <asp:CheckBox ID="chkOrcamentoFederal" runat="server" Text="Orçamento Federal:" AutoPostBack="true"
                                                        OnCheckedChanged="chkOrcamentoFederal_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtOrcamentoFederal" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkOutrosFundosFederais" runat="server" Text="Outros Fundos Nacionais:"
                                                        AutoPostBack="true" OnCheckedChanged="chkOutrosFundosFederais_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtOutrosFundosFederais" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkIGDPBF" runat="server" Text="IGD PBF:" AutoPostBack="true" OnCheckedChanged="chkIGDPAIF_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtIGDPBF" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="cell colspan2">
                                                    <asp:CheckBox ID="chkIGDSUAS" runat="server" Text="IGD SUAS:" AutoPostBack="true"
                                                        OnCheckedChanged="chkIGDSUAS_CheckedChanged" /><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtIGDSUAS" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                   </div>
                                  </div>	
                                  <asp:HiddenField ID="hdfAno" runat="server" />
						        </div>

                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" SkinID="button-save" Width="89px"
                                                OnClick="btnSalvar_Click" ValidationGroup="vgCampos" />
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"
                                                CausesValidation="false" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
