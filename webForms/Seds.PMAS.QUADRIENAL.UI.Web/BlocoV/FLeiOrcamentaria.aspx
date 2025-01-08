<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FLeiOrcamentaria.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FLeiOrcamentaria" %>

<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script type="text/javascript">

        function RefreshUpdatePanel() {
            __doPostBack('<%= txtValorRecursoNaoAlocadosFMAS.ClientID %>', '');
        };

    </script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtValorRecursoNaoAlocadosFMAS" EventName="TextChanged" />
        </Triggers>
        <ContentTemplate>
            <input type="hidden" runat="server" id="hidTotalFMAS" value="0,00" />
            <input type="hidden" runat="server" id="hidTotalRecursos" value="0,00" />
            
            <form name="frmPrefeitura">
                <div class="accordion" data-close-any="true">
                    <div class="frame active">
                        <div id="Quadrienal">
                            <asp:HiddenField ID="hdfAno" runat="server" Value="" />                  
                            <asp:Button ID="btnExercicio1" OnClick="btnLoadExercicio1_Click" runat="server" Width="113px"></asp:Button>
                            <asp:Button ID="btnExercicio2" OnClick="btnLoadExercicio2_Click" runat="server" Width="113px"></asp:Button>
                            <asp:Button ID="btnExercicio3" OnClick="btnLoadExercicio3_Click" runat="server" Width="113px"></asp:Button>
                            <asp:Button ID="btnExercicio4" OnClick="btnLoadExercicio4_Click" runat="server" Width="113px"></asp:Button>
                        </div>
                        <div class="heading">
                            <asp:Label runat="server" ID="lblHeader"></asp:Label>

                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="lei orçamentária">
                                <div class="grid">
                                    <div class="row cells5">
                                        <div class="cell colspan4">
                                            <b>As informações sobre a Lei Orçamentária Municipal devem trazer apenas os valores aprovados para a rubrica de Assistência Social, e não o valor total da Lei Orçamentária Municipal.</b>
                                        </div>
                                        <div class="cell" align="right">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset>
                                                <legend class="lgnd"><b class="fg-blue"><span runat="server" id="lgndLom"></span>  </b></legend><b>
                                                    
                                                </b>
                                                <div class="row cells3">
                                                    <div class="cell">
                                                        <b>Valor aprovado:</b><br />
                                                        <div class="input-control text mid-size">
                                                            <asp:TextBox ID="txtValorAprovadoLei" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>Nº da Lei:</b><br />
                                                        <div class="input-control text mid-size">
                                                            <asp:TextBox ID="txtLei" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>Data de publicação:</b><br />
                                                        <div class="input-control text mid-size">
                                                            <uc4:data ID="txtDataLei" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Nome do veículo de comunicação em que foi publicada:</b><br />
                                                        <div class="input-control text full-size">
                                                            <asp:TextBox ID="txtVeiculoComunicacao" runat="server" Width="95%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Valor de recursos destinados a Política de Assistência Social alocados no FMAS:&nbsp; </b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtValorRecursosFMAS" Text="0,00" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>Valor de recursos destinados a Política de Assistência Social que não estão alocados no FMAS:&nbsp;</b>
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtValorRecursoNaoAlocadosFMAS" runat="server" Text="0,00" AutoPostBack="True" onblur="RefreshUpdatePanel();" OnTextChanged="txtValorRecursnoNaoAlocadosFMAS_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <tr id="trOutrosRecursos" runat="server" visible="false">
                                                    <td colspan="3">
                                                        <br />
                                                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" width="55%">
                                                            <tr>
                                                                <th class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix" colspan="2" style="height: 20px;"><span class="ui-jqgrid-title">&nbsp;&nbsp;Dos recursos que não estão alocados no FMAS, indique em quais áreas e os valores destes investimentos: </span></th>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" width="70%">Recursos Humanos: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRecursosHumanos" runat="server" Style="text-align: right" Text="0,00" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" width="70%">Manutenção e/ou reforma de equipamentos: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtManutencaoEquipamentos" runat="server" Style="text-align: right" Text="0,00" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" width="70%">Construção de novas unidades: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtConstrucaoUnidades" runat="server" Style="text-align: right" Text="0,00" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" width="70%">Aquisição de bens permanentes: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAquisicaoBens" runat="server" Style="text-align: right" Text="0,00" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" width="70%">Total: </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalRecursos" runat="server" Enabled="false" Style="text-align: right" Text="0,00"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </td>
                                                </tr>
                                                
                                                <div class="row" runat="server" id="trAprovacaoRecursos" visible="false">
                                                    <div class="cell" align="center" colspan="3">
                                                        <b class="titulo">Lembre-se de verificar se as previsões de recursos feitas durante o preenchimento do PMAS estão coerentes com os valores aprovados pela Lei Orçamentária.</b>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Comentários do Gestor:</b></legend>
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
                                                                    <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
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
                                                <div class="row" id="trFinalizarCalculo" runat="server" visible="false">
                                                    <div class="cell" align="center" colspan="3">
                                                        <asp:Button ID="btnFinalizarCalculo" runat="server" OnClick="btnFinalizarCalculo_Click" OnClientClick="return confirm('Tem certeza que deseja finalizar o registro de informações sobre Lei Orçamentária da Assistência Social? Caso escolha Finalizar, não poderá ser feita mais nenhuma alteração nestes dados sem autorização da DRADS e/ou do CMAS.');" SkinID="button-save" TabIndex="16" Text="Finalizar e enviar" Width="146px" />
                                                    </div>
                                                </div>
                                                <div class="row" id="trAprovacaoDRADS" runat="server" visible="false">
                                                    <div class="cell" align="center">
                                                        <div class="row">
                                                            <div class="cell">A DRADS conferiu e confirma os dados informados sobre a Lei Orçamentária?</div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:RadioButtonList ID="rblAprovacaoDRADS" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Sim" Value="1" />
                                                                    <asp:ListItem Selected="True" Text="Não" Value="0" />
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:Button ID="btnSalvarAprovacaoDRADS" runat="server" OnClick="btnSalvarAprovacaoDRADS_Click" SkinID="button-save" TabIndex="16" Text="Salvar" Width="89px" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" id="trAprovacaoCMAS" runat="server" visible="false">
                                                    <div class="cell" align="center">
                                                        <div class="row" style="height: 20px;">
                                                            <div class="cell">
                                                                <span style="text-align: center;">O CMAS aprova os valores informados na Lei Orçamentária? </span>
                                                                <asp:RadioButtonList ID="rblAprovacaoCMAS" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Sim" Value="1" />
                                                                    <asp:ListItem Selected="True" Text="Não" Value="0" />
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell" align="center" colspan="3">
                                                                <asp:Button ID="btnSalvarAprovacaoCMAS" runat="server" OnClick="btnSalvarAprovacaoCMAS_Click" SkinID="button-save" TabIndex="16" Text="Salvar" Width="89px" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" id="trCancelarAprovacao" runat="server" visible="false">
                                                    <div class="cell" align="center">
                                                        <div class="row">
                                                            <div class="cell" style="height: 20px;">Deseja desbloquear quadro de Lei Orçamentária?</div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:Button ID="btnCancelarAprovacao" runat="server" OnClick="btnCancelarAprovacao_Click" SkinID="button-save" TabIndex="16" Text="Desbloquear" Width="146px" />
                                                            </div>
                                                        </div>
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
                                            </fieldset>
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
                        <a href="FPrevisaoOrcamentaria.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FFonteFinanciamento.aspx">Próximo
                              <span class="mif-arrow-right"></span></a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
