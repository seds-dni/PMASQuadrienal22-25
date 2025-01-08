<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FVigilancia.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVII.FVigilancia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmVigilancia">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>7.1 - Vigilância socioassistencial</b>
                            <a href="#" runat="server" id="linkAlteracoesQuadro68" visible="false">&nbsp;
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>
                            <span class="mif-meter icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="vigilância socioassistencial">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>O município realiza ações de vigilância socioassistencial?</b><br />
                                            <asp:RadioButtonList ID="rblOferece" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblOferece_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row" id="trSim" runat="server" visible="false">
                                        <div class="cell">
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Assinale em qual(is) do(s) eixo(s) da vigilância socioassistencial são desenvolvidas
                                        essas ações: </b>
                                                    <br />
                                                    <asp:CheckBox ID="chkEixo1" runat="server" Text="Vigilância de riscos e vulnerabilidades, ou seja, coleta e sistematização de informações relativas à incidência de violações de direitos e necessidades de proteção social da população"
                                                        AutoPostBack="True" OnCheckedChanged="chkEixo1_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="row" id="trAcoes1" runat="server" visible="false">
                                                <div class="cell" style="padding-left: 20px;">
                                                    <b>Dentre as opções abaixo, assinale as ações realizadas:</b><br />
                                                    <asp:CheckBoxList ID="chkAcoes1" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <asp:CheckBox ID="chkEixo2" runat="server" Text="Vigilância de padrões de serviços, ou seja, acompanha e analisa as características e distribuição da rede de proteção social instalada para a oferta de serviços."
                                                        AutoPostBack="True" OnCheckedChanged="chkEixo2_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="row" id="trAcoes2" runat="server" visible="false">
                                                <div class="cell" style="padding-left: 20px;">
                                                    <b>Dentre as opções abaixo, assinale as ações realizadas:</b><br />
                                                    <asp:CheckBoxList ID="chkAcoes2" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Assinale qual(is) da(s) seguinte(s) base(s) de dados são utilizadas pela vigilância
                                        em seu município:</b><br />
                                                    <asp:CheckBoxList ID="chkBaseDados" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="100%">
                                                        <%--Bruno V.--%>
                                                        <asp:ListItem Value="8" Text="Instrumentais próprios não informatizados" />
                                                        <asp:ListItem Value="9" Text="Sistema Informatizado Municipal" />
                                                        <asp:ListItem Value="10" Text="Dados de outros órgãos públicos municipais" />
                                                        <asp:ListItem Value="3" Text="PMASweb" />
                                                        <asp:ListItem Value="7" Text="Pró-Social" />
                                                        <asp:ListItem Value="11" Text="Fundação SEADE" />
                                                        <asp:ListItem Value="1" Text="CadÚnico" />
                                                        <%--<asp:ListItem Value="2" Text="Censo SUAS" />--%>
                                                        <asp:ListItem Value="2" Text="Outros Aplicativos da Rede SUAS" />
                                                        <asp:ListItem Value="12" Text="Aplicativos da SAGI / MDS" />
                                                        <asp:ListItem Value="13" Text="Aplicativos do Programa Bolsa Família" />
                                                        <%--     <asp:ListItem Value="4" Text="SisPETI" />--%>
                                                        <%--     <asp:ListItem Value="5" Text="SisJovem" />--%>
                                                        <asp:ListItem Value="14" Text="IBGE" />
                                                        <asp:ListItem Value="15" Text="SISC" />
                                                        <asp:ListItem Value="16" Text="Censo SUAS" />
                                                        <asp:ListItem Value="17" Text="CNEAS" />
                                                        <asp:ListItem Value="18" Text="Cad SUAS" />
                                                        <asp:ListItem Value="19" Text="RMA" />
                                                    </asp:CheckBoxList>
                                                    <%--<div style="width: 100%; padding-left: 3px;">
                                        <asp:CheckBox ID="chkOutraBaseDados" runat="server" Text="Outra" AutoPostBack="True"
                                            OnCheckedChanged="chkOutraBaseDados_CheckedChanged" />
                                    </div>--%>
                                                    <br />
                                                    <asp:Label ID="lblEspecifiqueBaseDados" runat="server" Font-Bold="true" Text="Especifique:"
                                                        Visible="false" />
                                                    <asp:TextBox ID="txtEspecifiqueBaseDados" runat="server" Visible="false" Width="181px"></asp:TextBox>
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
                                    <div class="row">
                                        <div class="cell">
                                            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
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
                    <td width="50%" align="left" style="padding-top: 10px;">&nbsp;
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FMonitoramento.aspx">Próximo
                              <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
