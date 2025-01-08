<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FMotivoExclusaoServicoPrivado.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FMotivoExclusaoServicoPrivado" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form name="frmPrefeitura">
        <div class="accordion" data-role="accordion">
            <div class="frame active">
                <div class="heading">
                    Desativação De Serviço
                </div>
                <div class="content">
                    <div class="formInput">
                        <div class="grid">
                            <div class="row">
                                <fieldset class="border-blue">
                                    <legend class="lgnd"><b class="fg-blue"></b></legend>
                                    <div class="row">
                                        <div class="cell">
                                            <div class="row">
                                                <div class="cell">
                                                    Para proceder à desativação deste serviço socioassistencial no sistema PMASweb, registre as informações solicitadas:
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Data da desativação do registro:</b><asp:Label ID="lblDataExclusaoRegistro" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>A desativação deste serviço é devida a:</b><br />
                                                    <asp:RadioButtonList ID="rblMotivoExclusao" runat="server" OnSelectedIndexChanged="rblMotivoExclusao_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="trDataEncerramento" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>
                                                        <asp:Label ID="lblDataEncerramentoServico" runat="server"></asp:Label>&nbsp;</b><uc4:data ID="txtDataEncerramento" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row" id="trDetalhamento" runat="server" visible="false">
                                                <div class="cell">
                                                    <div class="input-control text big-input full-size" data-role="input">
                                                        <b>
                                                            <asp:Label ID="lblDetalhamento" runat="server"></asp:Label>:</b><br />
                                                        <asp:TextBox Width="100%" ID="txtDetalhamento" runat="server" TextMode="MultiLine"
                                                            Height="302px" MaxLength="2000"></asp:TextBox>
                                                        <button class="button helper-button clear"><span class="mif-cross"></span></button>
                                                        <br />
                                                    </div>
                                                    <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 2000 caracteres."
                                                        Font-Bold="True" TextBoxControlId="txtDetalhamento" maxcharacterlength="2000" />
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="row" id="trRecursosFinanceiros" runat="server" visible="false">
                                                <div class="cell">
                                                    <fieldset class="border-blue">
                                                        <legend class="lgnd"><b class="titulo fg-blue">Recursos Financeiros</b></legend>
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Fundos Municipais</b></legend>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <b>Assistência Social:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFMAS" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>Direitos da Criança e do Adolescente:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFMDCA" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>Idoso:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFMI" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Fundos Estaduais</b></legend>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <b>Assistência Social:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFEAS" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="cell" id="trFeasAnterior" runat="server">
                                                                    <b>FEAS - Reprogramação Ano Anterior:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFEASAnoAnterior" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>Direitos da Criança e do Adolescente:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFEDCA" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <b>Idoso:</b>
                                                                    <br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFEI" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="cell" id="trFeasDemandas" runat="server">
                                                                    <b>FEAS - Demandas Parlamentares</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox runat="server" ID="txtFEASDemandasExercicio" Text="0,00" Style="text-align:right;"/>
                                                                    </div>
                                                                </div>
                                                               <div class="cell">
                                                                   <b>FEAS - Reprogramação Demandas Parlamentares</b><br />
                                                                   <div class="input-control text">
                                                                       <asp:TextBox runat="server" ID="txtFEASReprogramacaoDemandasParlamentaresExercicio" Text="0,00" Style="text-align:right;"/>
                                                                   </div>
                                                               </div>
                                                            </div>
                                                        </fieldset>
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Fundos Nacionais</b></legend>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <b>Assistência Social:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFNAS" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>Direitos da Criança e do Adolescente:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFNDCA" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <b>Idoso:</b><br />
                                                                    <div class="input-control text">
                                                                        <asp:TextBox ID="txtFNI" runat="server" Text="0,00" Style="text-align: right;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdfExercicio" runat="server" />


                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div class="row">
                            <div class="cell" align="center">
                                <asp:Button ID="btnAlerta" runat="server" OnClick="btnAlerta_Click" SkinID="button-save" Text="Salvar" Width="89px" />
                                <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" SkinID="button-save" Text="Salvar" Visible="false" Width="89px" OnClientClick="return confirm('Tem certeza que deseja desativar o registro deste serviço?');" />
                                &nbsp;<asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" Text="Voltar" />
                            </div>
                        </div>
                        <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                            width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                            <tr>
                                <td style="padding: 15px 10px 2px 15px">
                                    <span class="mif-warning mif-2x"></span>
                                    <%-- <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />--%>
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
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SecondContent" runat="server">
</asp:Content>
