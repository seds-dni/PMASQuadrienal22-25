<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FBeneficioEventual.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FBeneficioEventual" %>

<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmCRAS">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>
                            <asp:Label ID="lblNumeracao" runat="server" />-
                            <asp:Label ID="lblTitulo" runat="server" /></b>
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Benefícios Eventuais">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <b>Existe regulamentação municipal para concessão desse benefício?</b><br />
                                            <asp:RadioButtonList ID="rblRegulamentacao" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblRegulamentacao_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div id="trGeral" class="row" runat="server" visible="false">
                                    
                                    <div class="row">

                                        <div class="cell" align="left">
                                            <b>Tipo de Legislação:</b>
                                        </div>
                                        
                                    </div>

                                    <div class="row" id="trLegislacao" runat="server" visible="false">
                                        <div class="cell">
                                            <div class="row cells3">
                                                <div class="cell" align="left">
                                                    <div class="input-control select">
                                                        <b>Lei</b><br />
                                                        <asp:Checkbox ID="chklTipoLegislacao" runat="server" Enabled="true" OnCheckedChanged="chklTipoLegislacao_CheckedChanged" AutoPostBack="true"/>
                                                    </div>
                                                </div>
                                                <div class="cell" align="left">
                                                    <br />
                                                    Número da Lei :<br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtNumeroLei" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLei" runat="server"
                                                            TargetControlID="txtNumeroLei" FilterType="Numbers" />
                                                        /
                                                       <asp:TextBox ID="txtAnoLei" runat="server" Width="30px" MaxLength="2"></asp:TextBox>
                                                        (Ex: 129/11)
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLei" runat="server"
                                                            TargetControlID="txtAnoLei" FilterType="Numbers" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                    <br />
                                                    Data de publicação da Lei :<br />
                                                    <uc4:data ID="txtDataPublicacao" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row cells3">
                                                <div class="cell">
                                                    <asp:Label ID="lblHouveAlteracao" runat="server">Houve alteração na Lei de criação ?</asp:Label><br />
                                                    <asp:RadioButtonList ID="rblAlteracaoLei" runat="server" CssClass="RadioButton" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" Height="16px" OnSelectedIndexChanged="rblAlteracaoLei_SelectedIndexChanged">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>

                                                <div id="tdAlteracaoLei" runat="server" visible="false" class="cell">
                                                    
                                                    <asp:Label ID="lblLeiAlteracao" runat="server">&nbsp;&nbsp;Nº Lei (Alteração):</asp:Label><br />
                                                    <asp:TextBox ID="txtNumeroLeiAlteracao" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        TargetControlID="txtNumeroLeiAlteracao" FilterType="Numbers" />
                                                    /
                                                <asp:TextBox ID="txtNumeroLeiAlteracaoComplemento" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        TargetControlID="txtNumeroLeiAlteracaoComplemento" FilterType="Numbers" />
                                                    (Ex: 129/11)
                                                </div>

                                                <div id="tdAlteracaoLeiData" runat="server" class="cell" visible="false">
                                                        <asp:Label ID="lblDataAlteracao" runat="server">Data de publicação da Lei: </asp:Label><br />
                                                    <uc4:data ID="txtDataAlteracaoLei" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row cells3">
                                                <div class="cell" align="left">
                                                    <div class="input-control select">
                                                        <b>
                                                            <asp:CheckBox runat="server"  id="chkResolucao" value="3" Enabled="true" Text="Resolução CMAS" OnCheckedChanged="chkResolucao_CheckedChanged" AutoPostBack="true"/>                            
                                                        </b>
                                                    </div>
                                                </div>
                                                <div class="cell" align="left">
                                                    Número da resolução :<br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtNumeroResolucao" runat="server" Width="50px" MaxLength="5" Enabled="false"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            TargetControlID="txtDecretoPortaria" FilterType="Numbers" />
                                                        /
                                                       <asp:TextBox ID="txtAnoResolucao" runat="server" Width="30px" MaxLength="2" Enabled="false"></asp:TextBox>
                                                        (Ex: 129/11)
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoResolucao" runat="server"
                                                            TargetControlID="txtAnoResolucao" FilterType="Numbers" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                     Data de publicação da Resolução:<br />
                                                    <uc4:data ID="txtDataResolucao" runat="server" Enabled="false"/>
                                                </div>
                                            </div>


                                            <div class="row cells3">
                                                <div class="cell">
                                                    
                                                    <asp:Label ID="lblHouveAlteracaoResolucao" runat="server">Houve alteração na Resolução ?</asp:Label><br />
                                                    
                                            		<asp:RadioButtonList ID="rblAlteracaoResolucao" runat="server" CssClass="RadioButton" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" Height="16px" OnSelectedIndexChanged="rblAlteracaoResolucao_SelectedIndexChanged">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                            		
                                                </div>
                                                <div id="tdResolucaoAlterada" runat="server" visible="false" class="cell">
                                                   
                                                    <asp:Label ID="lblAlteracaoResolucao" runat="server">&nbsp;&nbsp;Nº Resolução (Alteração):</asp:Label><br />
                                                    <asp:TextBox ID="txtResocaoAlteracao" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtResocaoAlteracao" runat="server"
                                                        TargetControlID="txtResocaoAlteracao" FilterType="Numbers" />
                                                    /
                                            		
                                            		<asp:TextBox ID="txtResocaoAlteracaoComplemento" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                            		
                                            		<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtResocaoAlteracaoComplemento" runat="server"
                                            			TargetControlID="txtResocaoAlteracaoComplemento" FilterType="Numbers" />
                                            		(Ex: 129/11)
                                                </div>
                                                <div id="tdDataResolucaoAlterada" runat="server" class="cell" visible="false">
                                                    <asp:Label ID="lblDataAlteracaoResolucao" runat="server">Data da Alteração da resolução: </asp:Label><br />
                                                    <uc4:data ID="txtDataAlteracaoResolucao" runat="server" />
                                                </div>
                                            </div>
                                            

                                            <div class="row cells3">
                                                 <div class="cell" align="left">
                                                    <div class="input-control select">
                                                        <b>
                                                            <asp:CheckBox runat="server"  id="chkDecreto" Enabled="true" value="2" Text="Decreto/Portaria" AutoPostBack="true"  OnCheckedChanged="chkDecreto_CheckedChanged"/>                                                       
                                                        </b>
                                                    </div>
                                                 </div>
                                                <div class="cell" align="left">
                                                    Número do Decreto/Portaria :<br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtDecretoPortaria" runat="server" Width="50px" MaxLength="5" Enabled="false"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDecretoPortaria" runat="server"
                                                            TargetControlID="txtDecretoPortaria" FilterType="Numbers" />
                                                        /
                                                       <asp:TextBox ID="txtAnoDecretoPortaria" runat="server" Width="30px" MaxLength="2" Enabled="false"></asp:TextBox>
                                                        (Ex: 129/11)
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoDecretoPortaria" runat="server"
                                                            TargetControlID="txtAnoDecretoPortaria" FilterType="Numbers" />
                                                    </div>
                                                </div>
                                                <div class="cell">
                                                     Data de publicação do decreto:<br />
                                                    <uc4:data ID="txtDataDecretoPortaria" runat="server" Enabled="false"/>
                                                </div>
                                            </div>


                                            <div class="row cells3">
                                                <div class="cell">
                                                    
                                                    <asp:Label ID="lblHouveAlteracaoDecreto" runat="server">Houve alteração no Decreto/Portaria ?</asp:Label><br />
                                                    
                                            		<asp:RadioButtonList ID="rblAlteracaoDecreto" runat="server" CssClass="RadioButton" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" Height="16px" OnSelectedIndexChanged="rblAlteracaoDecreto_SelectedIndexChanged">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                            		
                                                </div>
                                                <div id="tdDecretoAlterado" runat="server" visible="false" class="cell">
                                                    
                                            		
                                                    <asp:Label ID="lblAlteracaoDecreto" runat="server">&nbsp;&nbsp;Nº Decreto/Portaria (Alteração) :</asp:Label><br />
                                                    <asp:TextBox ID="txtDecretoAlteracao" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDecretoAlteracao" runat="server"
                                                        TargetControlID="txtDecretoAlteracao" FilterType="Numbers" />
                                                    /
                                            		
                                            		<asp:TextBox ID="txtDecretoAlteracaoComplemento" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                            		
                                            		<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDecretoAlteracaoComplemento" runat="server"
                                            			TargetControlID="txtDecretoAlteracaoComplemento" FilterType="Numbers" />
                                            		(Ex: 129/11)
                                                </div>
                                                <div id="tdDataDecretoAlterada" runat="server" class="cell" visible="false">
                                                    <asp:Label ID="lblDataAlteracaoDecreto" runat="server">Data da Alteração do Decreto/Portaria: </asp:Label><br />
                                                    <uc4:data ID="txtDataAlteracaoDecreto" runat="server" />
                                                </div>
                                            </div>	

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <b>Quais os critérios usados para concessão desse benefício:</b><br />
                                            <asp:CheckBoxList ID="chkCriterios" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>O auxílio é concedido de que forma?</b><br />
                                            <asp:RadioButtonList ID="rblFormaAuxilio" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Auxílio Financeiro" />
                                                <asp:ListItem Value="2" Text="Auxílio Material" />
                                                <asp:ListItem Value="3" Text="Ambos" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Número de benefícios concedidos e de beneficiários:</b></legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Qual a média anual de beneficiários?</b><br />
                                                    <div class="input-control text low-size">
                                                        <asp:TextBox ID="txtMediaSemestralBeneficiarios" runat="server" />
                                                    </div>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtMediaSemestralBeneficiarios"
                                                        runat="server" TargetControlID="txtMediaSemestralBeneficiarios" FilterType="Numbers" />
                                                </div>
                                                <div class="cell">
                                                    <b>Qual a média anual de benefícios concedidos? </b>
                                                    <br />
                                                    <div class="input-control text low-size">
                                                        <asp:TextBox ID="txtMediaSemestralBeneficiariosConcedidos" runat="server" />
                                                    </div>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtMediaSemestralBeneficiariosConcedidos"
                                                        runat="server" TargetControlID="txtMediaSemestralBeneficiariosConcedidos" FilterType="Numbers" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div id="trNecessidades" class="row" runat="server" visible="false">
                                        <div class="cell">
                                            <b>Indique quais necessidades o benefício atende: </b>
                                            <br />
                                            <asp:CheckBoxList ID="chkNecessidades" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row" id="trBeneficiosOferecidos" runat="server" visible="false">
                                        <div class="cell" align="left">
                                            <b>Indique quais benefícios eventuais o município oferece: </b>
                                            <br />
                                            <asp:CheckBoxList ID="chkBeneficiosOferecidos" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Quais os órgãos responsáveis pela execução desse benefício eventual concedido pelo município?</b><br />
                                            <asp:CheckBoxList ID="chkResponsaveis" runat="server" RepeatDirection="Horizontal"
                                                RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="chkResponsaveis_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trUnidadeExecutora" visible="false">
                                        <div class="cell" align="left">
                                            <b>Unidade que executa este benefício:</b><br />
                                            <asp:CheckBoxList ID="chkUnidadeExecutora" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Recursos financeiros aplicados </b></legend>
                                             <div id="Quadrienal">
                                                <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnLoadExercicio1_Click"></asp:Button>
                                                <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnLoadExercicio2_Click"></asp:Button>
                                                 <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnLoadExercicio3_Click"></asp:Button>
                                                 <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnLoadExercicio4_Click"></asp:Button>
                                            </div>
                                            <div class="row">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Municipal</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            Fundo Municipal de Assistência Social:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFMAS" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Fundo Social de Solidariedade:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFundoMunicipalSolidariedade" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Orçamento Municipal:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOrcamentoMunicipal" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="row">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            Fundo Estadual de Assistência Social:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEAS" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell" id="trReprogramacao" runat="server">
                                                            FEAS - Reprogramação ano anterior:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox runat="server" ID="txtReprogramacaoAnoAnterior"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Fundo Social de Solidariedade:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFundoEstadualSolidariedade" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            FEAS - Demandas Parlamentares:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEASDemandasParlamentares" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            FEAS - Demandas Parlamentares Reprogramação:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEASDemandasParlamentaresReprogramacao" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="row">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Federal</b></legend>
                                                    <div class="row">
                                                        <div class="cell">
                                                            Fundo Nacional de Assistência Social:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFNAS" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div id="trDemandasExercicio" runat="server" visible="true"> 
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Recursos financeiros Demandas Parlamentares</b></legend>
                                                 
                                                <div class="row cells2">
                                                    <div class="cell">
                                                        <b>Código / Número da Demanda Parlamentar:</b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtCodigoDemandaExercicio" runat="server" Text=" " Style="text-align: right;" />
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <b>Objeto da Demanda Parlamentar:</b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtObjetoDemandaExercicio" runat="server" Text=" " Style="text-align: right;" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row cells2">
                                                        <div class="cell">
                                                            <b>Contrapartida Municipal:</b><br />
                                                            <asp:RadioButtonList ID="rblContraPartida" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" OnSelectedIndexChanged="rblContraPartida_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Text="Sim" />
                                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    <div id="trValorContraExercicio" class="cell" visible="false" runat="server">
                                                        <b></b><br />
                                                        <div class="input-control text">
                                                            <asp:TextBox ID="txtValorContraExercicio" runat="server" Text="0,00" Style="text-align: right;" /> 
                                                        </div>
                                                    </div>
                                                </div>
                                            
                                               </fieldset>
                                            </div>

                                        </fieldset>
                                        <asp:HiddenField ID="hdfAno" runat="server" />
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
                                            &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/BlocoIII/CBeneficioEventual.aspx" />
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
                </div>
            </form>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
