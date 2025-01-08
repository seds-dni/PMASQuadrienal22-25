<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FPopulacaoVulnerabilidade.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.FPopulacaoVulnerabilidade" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <contenttemplate>
        <form name="frmPopulacaoVulnerabilidade">
            <div class="accordion" data-role="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro2" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado  
                                    </a>&nbsp;      
                            2.2 - População e vulnerabilidade social
                            </div>
                          <div class="content">
                            <div class="formInput" data-text="População e vulnerabilidade social">
                                <div class="grid">
                                   <div class="row">
                                    <div class="cell">
                                     <table class="table striped border bordered" width="100%" cellspacing="0">
                                         <thead class="info">
                                        <tr style="height: 25px;">
                                            <td align="center" rowspan="2" width="300">Indicador
                                            </td>
                                             <td align="center" width="60" rowspan="2" >Unidade
                                            </td>
                                            <td rowspan="2" align="center" width="70">Nota<br />
                                                explicativa
                                            </td>
                                            <td align="center" width="70" rowspan="2">Referência
                                            </td>
                                            <td align="center" colspan="3">Valores
                                            </td>
                                            <td align="center" width="60" rowspan="2">Fonte
                                            </td>
                                        </tr>
                                        <tr style="height: 25px;">
                                            <td width="60" align="center" >Município</td>
                                            <td width="60" align="center" >DRADS
                                            </td>
                                            <td align="center" width="60" >Estado
                                            </td>
                                         </tr></thead>
                                        <tr style="height: 22px;">
                                          <td align="left" rowspan="2">População com menos de 15 anos (estimativa)</td>
                                             <td align="center">Pessoas</td>
                                            <td align="center" >
                                                  <button id="btnAjudaIdade15Anos" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="center" rowspan="2">2020 </td>
                                            <td align="right">
                                                            <asp:Label ID="lblNumeroPopulacao15" Text="0,00" runat="server"></asp:Label>
                                                        </td>
                                            <td align="right">
                                                            <asp:Label ID="lblNumeroPopulacao15DRADS" Text="0,00" runat="server"></asp:Label>
                                                        </td>
                                            <td align="right">8.422.372</td>
                                            <td align="center" rowspan="2">
                                                            <asp:HyperLink ID="HyperLink14" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                                                Text="SEADE" runat="server"></asp:HyperLink><br />
                                                        </td>
                                         </tr>
                                        <tr>
                                                 <td align="center">%
                                                 </td>
                                                 <td align="center" >
                                                <button id="btnAjudaPercIdade15Anos" class="note" onclick="return false;"></button>
                                                     </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblPercIdade15Anos" Text="0%" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblPercIdade15AnosDRADS" Text="0%" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">18,8</td>
                                             </tr>
                                        <tr style="height: 22px;">
                                            <td align="left" rowspan="2">
                                              <asp:Label ID="Label14" Text="População com 60 anos ou mais (estimativa)"
                                                                runat="server"></asp:Label>
                                              </td>
                                              <td align="center">Pessoas
                                              </td>
                                              <td align="center">
                                                 <button id="btnAjudaIdosos60Anos" class="note" onclick="return false;"></button>
                                              </td>
                                              <td align="center" rowspan="2">2020
                                              </td>
                                              <td align="right">
                                                            <asp:Label ID="lblIdososNumero" runat="server"></asp:Label>
                                                        </td>
                                              <td align="right">
                                                            <asp:Label ID="lblIdososNumeroDRADS" runat="server"></asp:Label>
                                                        </td>
                                              <td align="right">6.831.702</td>
                                              <td align="center" rowspan="2">
                                                            <asp:HyperLink ID="HyperLink9" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                                                runat="server" Text="SEADE"></asp:HyperLink>
                                                        </td>
                                         </tr>
                                        <tr>
                                            <td align="center">%</td>
                                              <td align="center">
                                                 <button id="btnAjudaIdosos60AnosPerc" class="note" onclick="return false;"></button>
                                              </td>
                                              <td align="right">
                                                            <asp:Label ID="lblIdososPerc" runat="server"></asp:Label>
                                                        </td>
                                              <td align="right">
                                                            <asp:Label ID="lblIdososPercDRADS" runat="server"></asp:Label>
                                                        </td>
                                              <td align="right">15,2</td>
                                        </tr>
                                         <tr style="height: 30px;">
                                            <td align="left">
                                               Índice de envelhecimento</td>
                                              <td align="center">
                                                Índice
                                            </td>
                                            <td align="center">
                                                 <button id="btnAjudaEnvelhecimento" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="center">2020
                                            </td>
                                            <td align="right" height="25">
                                                <asp:Label ID="lblEnvelhecimentoMunicipio" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Label ID="lblEnvelhecimentoDRADS" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Label ID="lblEnvelhecimentoEstado" Text="81,1" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink1" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                                    runat="server" Text="SEADE/SEDS"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td align="left">
                                                Razão de dependência</td>
                                              <td align="center">
                                                %
                                            </td>
                                            <td align="center">
                                                 <button id="btnRazaoDependencia" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="center">2020

                                            </td>
                                            <td align="right" height="25">
                                                <asp:Label ID="lblRazaoDependenciaMunicipio" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Label ID="lblRazaoDependenciaDRADS" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Label ID="lblRazaoDependenciaEstado" Text="0,42" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink26" NavigateUrl="http://www.br.undp.org/" Target="_blank"
                                                    runat="server" Text="SEADE/SEDS"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        
                                     </table>
                                     </div>
                                   </div>
                                    <div class="row">
                                        <div class="cell">
                                             <strong> Este quadro permite ao município elaborar uma análise sobre as situações de vulnerabilidade ou risco social existentes no município com base nos indicadores aqui apresentados e outros que sejam considerados relevantes,
                                                  inclusive considerando também as possibilidades de sua superação ou minimização. 
                                                 <br />É importante considerar que as situações de vulnerabilidade relacionam-se a determinadas circunstâncias sociais e familiares tais como: ciclo de vida, 
                                                 características da organização e relações familiares, escolaridade, renda, inserção no mercado de trabalho, saúde e acesso aos bens e serviços ofertados pelo poder público, 
                                                 sociedade e mercado. </strong>
                                            <br />
                                               <br />
                                              <asp:TextBox Width="100%" ID="txtCaracterizacaoPopulacao" runat="server" TextMode="MultiLine"
                                        Height="302px" MaxLength="4000"></asp:TextBox>
                                    <br />
                                    <skm:TextboxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                        Font-Bold="True" TextBoxControlId="txtCaracterizacaoPopulacao" MaxCharacterLength="4000" />
                                    <br />
                                    <asp:Button ID="btnSalvarCaracterizacao" runat="server" SkinID="button-save" Width="89px"
                                        Text="Salvar" OnClick="btnSalvarCaracterizacao_Click" />
                                        </div>
                                    </div>
                                 </div>
                            </div>
                        </div>
                </div>
        </form>
                            <table id="tbInconsistencias" runat="server" align="center" border="0" cellpadding="0" cellspacing="2" visible="false" width="100%">
                                <tr>
                                    <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />
                                        <b style="color: #000000 !important">Verifique as inconsistências:</b>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblInconsistencias" runat="server" ForeColor="Red" />
                                    </td>
                                </tr>
           </table>
         <table width="100%" align="center" class="ui-text">
                 <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FAnaliseDiagnostica.aspx">
                          <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FEvolucaoRedeSocioassistencial.aspx">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </contenttemplate>
    <script type="text/javascript">
        $(function () {
            $('#btnAjudaIdade15Anos').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\ padding5"></span>"', caption: 'População com menos de 15 anos (estimativa)', content: "População com idade entre 0 e 14 anos tendo como referência a estimativa para 2021 elaborada pela Fundação SEADE." });
                }, 500);
            });
            $('#btnAjudaPercIdade15Anos').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\ padding5""></span>"', caption: 'População com menos de 15 anos (porcentagem)', content: "Proporção da população de 0 a 14 anos em relação ao total da população do município, tendo como referência a estimativa para 2021 elaborada pela Fundação SEADE." });
                }, 500);
            });
            $('#btnAjudaIdosos60Anos').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\ padding5"></span>"', caption: 'População com 60 anos ou mais (estimativa)', content: "População com 60 anos de idade e mais, tendo como referência a estimativa para 2020 elaborada pela Fundação SEADE." });
                }, 500);
            });

            $('#btnAjudaIdosos60AnosPerc').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\ padding5"></span>"', caption: 'População com 60 anos ou mais (porcentagem)', content: "Proporção da população com 60 anos e mais em relação ao total da população do município, tendo como referência a estimativa para 2020 elaborada pela Fundação SEADE." });
                }, 500);
            });
            $('#btnAjudaEnvelhecimento').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Índice de envelhecimento', content: "Proporção de pessoas de 60 anos e mais por 100 indivíduos de 0 a 14 anos, tendo como referência a estimativa para 2020 elaborada pela Fundação SEADE. Adota-se o corte etário da população idosa em 60 anos, de acordo com Rede Interagencial de Informações para a Saúde - Ripsa e 25ª Conferência Sanitária Pan-Americana da Organização Pan-Americana da Saúde - Opas. Cálculos realizados pela CGE/SEDS. " });
                }, 500);
            });
            $('#btnRazaoDependencia').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Razão de Dependência', content: "Percentual entre a população dependente e a população potencialmente ativa, calculado pela razão entre a soma da população com menos de 15 anos com a população de 65 anos e mais em relação à população de 15 a 64 anos. Os dados aqui apresentados foram calculados pela CGE/SEDS com base na estimativa de população para 2021 elaborada pela Fundação SEADE." });
                }, 500);
            });
        });

    </script>
</asp:Content>
