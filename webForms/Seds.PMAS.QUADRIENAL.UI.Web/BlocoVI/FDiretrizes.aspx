<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDiretrizes.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI.FDiretrizes" %>
<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <script src="../Scripts/Util.js" type="text/javascript"></script>
            <form name="frmDiretriz">
                <div class="accordion" data-role="accordion" data-close-any="true">
                  <div class="frame" id="fraOrgaoGestor" runat="server">

                         <div id="Quadrienal">
                              <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnExercicio1_Click"></asp:Button>
                              <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnExercicio2_Click"></asp:Button>
                              <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnExercicio3_Click"></asp:Button>
                              <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnExercicio4_Click"></asp:Button>
                         </div>
                        
                        <div class="heading">
                            <b>6.1 - Diretrizes e prioridades - Situações de Vulnerabilidades</b><a href="#" runat="server" id="linkAlteracoesQuadro61" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                           <span class="mif-home icon"></span>
                        </div>

                     <div class="content">
                         <div class="row">
                                <div class="cell">
                                    <table class="table striped border bordered" cellspacing="0"
                                        cellpadding="0" border="0" width="100%">
                                        <thead class="info">
                                            <tr>
                                                <th width="300">Povos, comunidades tradicionais e grupos específicos existentes no município</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="bg-lightgray">
                                                <td colspan="2" style="height: 22px; background-color: #c9e6f6;"><b>Povos e comunidades tradicionais </b>
                                                </td>
                                            </tr>
                                            <tr id="trCiganos" runat="server" visible="false">
                                                <td>

                                                    <asp:Label ID="lblCigano" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trExtrativistas" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblExtrativista" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trPescadores" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblPescadores" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trAfro" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblAfro" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trRibeirinha" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblRibeirinha" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trIndigenas" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblIndigenas" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trQuilombolas" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblQuilombolas" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trNenhumaComunidade" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblNenhumaComunidade" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-lightgray">
                                                <td colspan="2" style="height: 22px; background-color: #c9e6f6;"><b>Grupos específicos</b>
                                                </td>
                                            </tr>
                                            <tr id="trAgricultores" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblAgricultores" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trAcampamentos" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblAcampamentos" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trPopulacaoPrisional" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblPopulacaoPrisional" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trTrabalhadoresSazonais" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblTrabalhadoresSazonais" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trAglomeradosSubnormais" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblAglomeradosSubnormais" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trAssentamentos" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblAssentamentos" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trNaoExisteGrupo" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lblNaoExisteGrup" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            
                                <div class="cell auto-size">
                                    <asp:ListView ID="lstAnaliseDiagnostica" runat="server" DataKeyNames="Id" Onitemcommand="lstAnaliseDiagnostica_ItemCommand" >
                                        <LayoutTemplate>
                                            <table class="table striped border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th width="200">Situações de vulnerabilidade ou risco mais graves</th>
                                                        <th width="70">Classificação</th>
                                                        <th width="80">Demanda estimada</th>
                                                        <th width="150">Número de serviços existentes<br />
                                                            que atendem esta demanda
                                                        </th>
                                                        <th width="50">Visualizar Serviços</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                           
                                             <tr>
                                                <td align="left">
                                                    <b><%#DataBinder.Eval(Container.DataItem, "SituacaoVulnerabilidade")%></b>
                                                </td>
                                                <td align="center">
                                                    <%#DataBinder.Eval(Container.DataItem, "Classificacao") %>
                                                </td>
                                                <td align="center">
                                                    <%#String.Format("{0:N0}",DataBinder.Eval(Container.DataItem, "Demanda"))%>
                                                </td>
                                                <td align="center">
                                                    <%#DataBinder.Eval(Container.DataItem, "TotalServicos")%>
                                                </td>
                                                <td align="center">
                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar/Adicionar/Editar Serviços e Recursos Financeiros"
                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" style="width: 100%;">
                                                <b class="titulo">Não existe registro.</b>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                         </div>
                     </div>
                  </div>
                  <div class="frame" runat="server">
                        <div class="heading">
                            <b>6.2 - Diretrizes e prioridades - Órgão Gestor e Rede Socioassistencial</b><a href="#" runat="server" id="A1" visible="false">
                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                            <span class="mif-home icon"></span>
                        </div>
                     <div class="content">
                       <div class="row">
						  <fieldset class="border-blue">
                             <div class="row">
                                 <div class="cell auto-size">
                                   <asp:ListView ID="lstIntencaoAcao" runat="server" DataKeyNames="IdUnidade">
                                       <LayoutTemplate>
                                           <table class="table striped border bordered" cellspacing="0"
                                               cellpadding="0" border="0" width="100%">
                                               <thead class="info">
                                                   <tr>
                                                       <th style="width:200PX">Locais de execução</th>
                                                       <th style="width:200PX">Denominação do local</th>
                                                       <th style="width:200PX">Necessidades apontadas</th>
                                                       <th style="width:90px">Status</th>
                                                   </tr>
                                               </thead>
                                               <tbody>
                                                   <tr id="itemPlaceholder" runat="server">
                                                   </tr>
                                               </tbody>
                                           </table>
                                       </LayoutTemplate>
                                       <ItemTemplate>
                                           <tr>
                                               <td align="left">
                                                   <%#DataBinder.Eval(Container.DataItem, "TipoLocal")%>
                                               </td>
                                               <td align="left">
                                                   <%#DataBinder.Eval(Container.DataItem, "Nome")%>
                                               </td>
                                               <td align="left">
                                                   <%#DataBinder.Eval(Container.DataItem, "IntencaoAcao") %>
                                               </td>
                                               <td align="center"><%#((Boolean)DataBinder.Eval(Container.DataItem, "Desativado")) ? "Inativo" : "Ativo"%>
                                               </t>
                                           </tr>
                                       </ItemTemplate>
                                       <EmptyDataTemplate>
                                           <div align="center" style="width: 100%;">
                                               <b class="titulo">Não existe registro.</b>
                                           </div>
                                       </EmptyDataTemplate>
                                   </asp:ListView>
                                 </div>
                             </div>
						  </fieldset>
	                   </div>
                       <div class="row">
                           <fieldset class="border-blue">
                               <legend class="lgnd"><b class="fg-blue">Serviços socioassistenciais</b></legend>
                               <div class="row">
                                   <div class="cell">
                                       <asp:ListView ID="lstServicosSocioassistencias" runat="server">
                                           <LayoutTemplate>
                                               <table class="table striped border bordered" cellspacing="0"
                                                   cellpadding="0" border="0" width="100%">
                                                   <thead class="info">
                                                       <tr>
                                                           <th width="80">Locais de execução</th>
                                                           <th width="200">Denominação do local</th>
                                                           <th width="100">Tipo de Serviço
                                                           </th>
                                                           <th width="150">Usuário
                                                           </th>
                                                           <th width="150">Avaliação de Serviço
                                                           </th>
                                                           <th width="90">Status</th>
                                                       </tr>
                                                   </thead>
                                                   <tbody>
                                                       <tr id="itemPlaceholder" runat="server">
                                                       </tr>
                                                   </tbody>
                                               </table>
                                           </LayoutTemplate>
                                           <ItemTemplate>
                                               <tr style="height: 22px; background-color: #c9e6f6;">
                                                   <td colspan="6">
                                                       <b>Proteção Social:</b>
                                                       <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                                   </td>
                                               </tr>
                                               <tr>
                                                   <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>'>
                                                       <LayoutTemplate>
                                                           <tr id="itemPlaceholder" runat="server">
                                                           </tr>
                                                       </LayoutTemplate>
                                                       <ItemTemplate>
                                                           <tr>
                                                               <td align="left">
                                                                   <%#DataBinder.Eval(Container.DataItem, "TipoLocal")%>
                                                               </td>
                                                               <td align="left">
                                                                   <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                                                               </td>
                                                               <td align="left">
                                                                   <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                                                               </td>
                                                               <td align="left">
                                                                   <%#DataBinder.Eval(Container.DataItem, "Usuario")%>
                                                               </td>
                                                               <td align="left">
                                                                   <%#DataBinder.Eval(Container.DataItem, "AvaliacaoServico")%>
                                                               </td>
                                                               <td align="center"><%#((Boolean)DataBinder.Eval(Container.DataItem, "Desativado")) ? "Inativo" : "Ativo"%>
                                                               </t>
                                                           </tr>
                                                       </ItemTemplate>
                                                   </asp:ListView>
                                               </tr>
                                           </ItemTemplate>
                                           <EmptyDataTemplate>
                                               <div align="center" style="width: 100%;">
                                                   <b class="titulo">Não existe registro.</b>
                                               </div>
                                           </EmptyDataTemplate>
                                       </asp:ListView>
                                   </div>
                               </div>
                           </fieldset>
                       </div>
                       <div class="row">
                           <fieldset class="border-blue">
                               <legend class="lgnd"><b class="fg-blue">Equipe especifica do órgão gestor</b></legend>
                               <div class="row">
                                   <div class="cell auto-size">
                                       <table class="table striped border bordered" cellspacing="0"
                                           cellpadding="0" border="0" width="100%">
                                           <thead class="info">
                                               <tr>
                                                   <th width="100">Equipe Específica</th>
                                                   <th width="300">Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</th>
                                               </tr>
                                           </thead>
                                           <tbody>
                                               <tr id="trEquipeBasica" runat="server">
                                                   <td>Proteção Social Básica
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeBasica" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeEspecial" runat="server">
                                                   <td>Proteção Social Especial
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeEspecial" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeVigilancia" runat="server">
                                                   <td>Vigilância Socioassistencial
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeVigilancia" runat="server"></asp:Label>
                                                   </td>
                                               </tr>

                                                <tr id="trEquipeGestaodoSuas" runat="server">
                                                    <td>Gestão Suas
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEstrurarEquipeGestaoSuas" runat="server"></asp:Label>
                                                    </td>
                                                </tr>

                                               <tr id="trEquipeGestaoBeneficios" runat="server">
                                                   <td>Gestão de Benefícios/Transferência de Renda
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeGestaoBeneficios" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeCadUnico" runat="server">
                                                   <td>Gestão do Cadastro Único
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeCadUnico" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeGestaoFinanceira" runat="server">
                                                   <td>Gestão Financeira e Orçamentária
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeGestaoFinanceira" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeGestaoSuas" runat="server">
                                                   <td>Gestão do Trabalho no SUAS
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeTrabalho" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeRegulacaoSuas" runat="server">
                                                   <td>Regulação do Suas
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblEstrurarEquipeRegulacaoSuas" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr id="trEquipeRedeDireta" runat="server">
                                                   <td>Execução dos serviços socioassistenciais da rede direta
                                                   </td>
                                                   <td>
                                                       <asp:Label ID="lblAumentarEquipeRedeDireta" runat="server"></asp:Label>
                                                   </td>
                                               </tr>
                                               <tr style="background-color: #c9e6f6;" id="trEquipeOrgaoGestor" runat="server">
                                                   <td colspan="2">
                                                       <b>
                                                           <asp:Label ID="lblAumentarEquipeOrgaoGestor" runat="server"></asp:Label></b>
                                                   </td>
                                               </tr>
                                           </tbody>
                                       </table>
                                   </div>
                               </div>
                           </fieldset>
                       </div>		
                      </div>  
                  </div>
                </div>

                <table width="100%" align="center" class="ui-text">
                  <tr>
                      <td width="50%" align="right" style="padding-top: 10px;">
                          <a href="CAcaoPlanejamento.aspx">Próximo <span class="mif-arrow-right" />
                          </a>
                      </td>
                  </tr>
                </table>
                <asp:HiddenField ID="hdfAno" runat="server" Value="" />
            </form>
</asp:Content>
