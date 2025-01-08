<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FFonteFinanciamento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FFonteFinanciamento" %>

<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script type="text/javascript">

        function CalculateTotalFMAS() {
            var txtFMAS = document.getElementById('<%=txtFMAS.ClientID%>').value;
            var txtFNAS = document.getElementById('<%=txtFNAS.ClientID%>').value;
            var txtFEAS = document.getElementById('<%=txtFEAS.ClientID%>').value;
            var valoresrecursosfinanceiros = [txtFMAS, txtFEAS, txtFNAS];
            PageMethods.CalcularValores(valoresrecursosfinanceiros, function (val) {
                document.getElementById('<%=txtTotalFMAS.ClientID%>').value = val;
                document.getElementById('<%=hidTotalFMAS.ClientID%>').value = val;
            });
        }
    </script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <%--  <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtValorRecursoNaoAlocadosFMAS" EventName="TextChanged" />
        </Triggers>--%>
        <ContentTemplate>
            <input type="hidden" runat="server" id="hidTotalFMAS" value="0,00" />
            <input type="hidden" runat="server" id="hidTotalRecursos" value="0,00" />
            <form name="frmConselhos">
                <div class="accordion">
                    <div class="frame active">
                        <div id="Quadrienal">
                            <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnLoadExercicio1_Click"></asp:Button>
                            <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnLoadExercicio2_Click"></asp:Button>
                            <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnLoadExercicio3_Click"></asp:Button>
                            <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnLoadExercicio4_Click"></asp:Button>
                        </div>
                        <div class="heading">
                            <asp:Label runat="server" ID="lblHeader"></asp:Label>
                            <a href="#" runat="server" id="linkAlteracoesQuadro" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="fontes de recursos">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Recursos financeiros alocados no FMAS</b></legend><b>
                                                    <label runat="server" id="lblInformePrevisaoFMAS"></label>
                                                </b>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Previsão de recursos municipais alocados no FMAS:</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtFMAS" runat="server" Text="0,00" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Valor dos recursos municipais destinado apenas para custeio dos serviços:</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtCusteio" runat="server" Text="0,00" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Previsão de cofinanciamento estadual através do Fundo Estadual de Assistencia Social (FEAS):</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtFEAS" runat="server" Text="0,00" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Previsão de cofinanciamento federal através do Fundo Nacional de Assistencia Social (FNAS):</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtFNAS" runat="server" Text="0,00" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Total de recursos alocados no FMAS:</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtTotalFMAS" runat="server" Enabled="false" Text="0,00" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Incentivos à gestão</b></legend>
                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Índice de Gestão Descentralizada do Programa Bolsa Família (IGD-PBF):</b>
                                                    </div>
                                                    <div class="cell">
                                                        <b>Índice de Gestão Descentralizada do Sistema Único de Assistência Social (IGD-SUAS):</b>
                                                    </div>
                                                </div>
                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Valor do índice:</b>
                                                        <div class="input-control text low-size">
                                                            <asp:TextBox ID="txtIGDPBF" runat="server" MaxLength="5" Style="text-align: right" Width="72px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>Valor do índice:&nbsp;</b>
                                                        <div class="input-control text low-size">
                                                            <asp:TextBox ID="txtIGDSUAS" runat="server"  MaxLength="6" Style="text-align: right" Width="72px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Valor mensal do recurso:&nbsp;&nbsp;</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtIGDPBFValorMensal" runat="server" MaxLength="30" Style="text-align: right" Width="133px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>Valor mensal do recurso:&nbsp;</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtIGDSUASValorMensal" runat="server" MaxLength="30" Style="text-align: right" Width="133px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row cells2">
                                                    <div class="cell" height="25">
                                                        <b>Valor anual do recurso:&nbsp;</b>
                                                        R$&nbsp;<asp:Label ID="lblIGDPBFValorAnual" runat="server"></asp:Label><br />
                                                        
                                                    </div>
                                                    <div class="cell">
                                                        <b>Valor anual do recurso:&nbsp;</b>
                                                        R$&nbsp;<asp:Label ID="lblIGDSUASValorAnual" runat="server"></asp:Label><br />
                                                        


                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="tbInconsistenciasIGD" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                <tr>
                                                    <td style="padding: 15px 10px 2px 15px">
                                                        <span class="mif-warning mif-2x"></span>
                                                        <b style='color: #000000 !important'>Verifique as inconsistências:</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px 10px 12px 45px;">
                                                        <asp:Label ID="lblInconsistenciasIGD" ForeColor="Red" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Comentários do Órgão Gestor:</b></legend>
                                                <div class="row">
                                                    <div class="cell">
                                                        <div class="input-control textarea full-size">
                                                            <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" MaxLength="500"
                                                                Height="64px" />
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 1000 caracteres."
                                                            Font-Bold="True" TextBoxControlId="txtComentario" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" SkinID="button-save" TabIndex="16" Text="Salvar" Width="90px" />
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
                            <asp:HiddenField ID="hdfAno" runat="server" Value="" />
                        </div>
                    </div>
                </div>
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FLeiOrcamentaria.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FExecucaoFinanceira.aspx">Próximo
                            <span class="mif-arrow-right"></span></a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
