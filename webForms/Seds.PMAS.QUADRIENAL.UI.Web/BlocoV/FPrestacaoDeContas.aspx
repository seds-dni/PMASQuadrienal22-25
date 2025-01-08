<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FPrestacaoDeContas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FPrestacaoDeContas" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script src="../Scripts/dataFormat.js" type="text/javascript"></script>
            <form name="frmPrestacaoDeContas">
               <div id="Quadrienal">
                    <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnExercicio1_Click"></asp:Button>
                    <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnExercicio2_Click"></asp:Button>
                    <asp:Button ID="btnExercicio3" runat="server" Width="113px" Onclick="btnExercicio3_Click" ></asp:Button>
                    <asp:Button ID="btnExercicio4" runat="server" Width="113px" Onclick="btnExercicio4_Click" ></asp:Button>
                </div>
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame">
                        <div class="heading">
                            1ª - Demonstrativo sintético físico-financeiro dos recursos estaduais recebidos no exercício de <asp:Label ID="lblExercicio" runat="server" />
                        </div>
                        <div class="content">
                         <asp:UpdatePanel runat="server" ID="pnlAbaUm">
                          <ContentTemplate>
                            <div class="formInput" data-text="prestação de contas">
                                <asp:ListView ID="lstProtecaoBasica" runat="server" >
                                  <LayoutTemplate>
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                        <thead class="info">
                                           <tr>
                                               <th colspan="4" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Serviços da Proteção Social Básica</span>
                                               </th>
                                               <th colspan="2" style="height: 20px;">
                                                   <span >Execução Física</span>
                                               </th>
                                               <th colspan="5" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Receitas</span>
                                               </th>
                                               <th colspan="3" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Despesas</span>
                                               </th>
                                               <th colspan="1" rowspan="2" width="60">Editar
                                               </th>
                                           </tr>
                                           <tr class="ui-jqgrid-labels" style="height: 22px;">
                                               <th width="80">Tipo de Rede
                                               </th>
                                               <th width="120">Unidade/organização
                                               </th>
                                               <th width="120">Tipo de Serviço
                                               </th>
                                               <th width="80">Usuários
                                               </th>
                                               <th width="60">Capacidade Mensal de Atendimento
                                               </th>
                                               <th width="60">Média Mensal de Atendimento
                                               </th>
                                               <th width="60">Cofinanciamento estadual no exercício atual
                                               </th>
                                               <th width="60">Recursos reprogramados do ano anterior
                                               </th>
                                               <th width="60">Demandas Parlamentares
                                               </th>
                                               <th width="60">Demandas Parlamentares Recursos Reprogramados 
                                               </th>
                                               <th width="60">Aplicações Financeiras
                                               </th>
                                               <th width="60">Recursos Humanos
                                               </th>
                                               <th width="60">Material de Consumo
                                               </th>
                                               <th width="60">Outras Despesas
                                               </th>
                                           </tr>    
                                                                                      
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </tbody>
                                    </table>
                                  </LayoutTemplate>
                                  <ItemTemplate>
                                      <tr >
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "TipoUnidade"))%>
                                            </td>
                                            <td class="align-left"style="width:120px;"">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "UnidadeResponsavel"))%>
                                            </td>
                                            <td class="align-left" style="width:120px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "TipoServico"))%>
                                            </td>
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "Usuario"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "CapacidadeDeAtendimento")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "MediaMensalDeAtendimento")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CofinanciamentoEstadual"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosReprogramadosAnoAnterior"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentares"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentaresReprogramados"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAplicacoesFinanceiras"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosHumanos"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "MaterialDeConsumo"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "OutrasDespesas"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">                                                
                                                <a  href="FDespesas.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                   <image src="../Styles/Icones/find.png" alt="Visualizar" border="0" />
                                                </a> 
                                            </td>
                                      </tr>
                                  </ItemTemplate>
                                </asp:ListView>

                                <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" id="tbTotalBasica" runat="server">
                                     <tr class="ui-jqgrid-labels" style="height: 22px">
                                           <td class="ui-state-default ui-th-column ui-th-ltr ui-jqgrid-labels" style="width:520px;text-align: right;" >
                                                Total
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label ID="lblTotalCapacidadeMensalBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:105px;">
                                               <asp:Label ID="lblTotalMediaMensalBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:120px;">
                                              <asp:Label ID="lblTotalCofinanciamentoEstadualBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:115px;">
                                               <asp:Label ID="lblReprogramacaoBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:120px;">
                                               <asp:Label ID="lblTotalDemandasBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:125px;">
                                               <asp:Label ID="lblTotalDemandasReprogramacaoBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:85px;">
                                               <asp:Label ID="lblTotalValoresAplicacoesBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                               <asp:Label ID="lblTotalRecursosHumanosBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:70px;">
                                               <asp:Label ID="lblTotalMaterialConsumoBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                               <asp:Label ID="lblTotalOutrasDespesasBasica" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:65px;">
                                     
                                           </td>
                                     </tr>

                                </table>

                                <br />
                                <br />
                                <asp:ListView ID="lstProtecaoMedia" runat="server">
                                  <LayoutTemplate>
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                        <thead class="info">
                                           <tr>
                                               <th colspan="4" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Serviços da Proteção Social Média</span>
                                               </th>
                                               <th colspan="2" style="height: 20px;">
                                                   <span >Execução Física</span>
                                               </th>
                                               <th colspan="5" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Receitas</span>
                                               </th>
                                               <th colspan="3" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Despesas</span>
                                               </th>
                                               <th colspan="1" rowspan="2" width="60">Editar
                                               </th>
                                           </tr>
                                           <tr class="ui-jqgrid-labels" style="height: 22px;">
                                               <th width="80">Tipo de Rede
                                               </th>
                                               <th width="120">Unidade/organização
                                               </th>
                                               <th width="120">Tipo de Serviço
                                               </th>
                                               <th width="80">Usuários
                                               </th>
                                               <th width="60">Capacidade Mensal de Atendimento
                                               </th>
                                               <th width="60">Média Mensal de Atendimento
                                               </th>
                                               <th width="60">Cofinanciamento estadual no exercício atual
                                               </th>
                                               <th width="60">Recursos reprogramados do ano anterior
                                               </th>
                                               <th width="60">Demandas Parlamentares
                                               </th>
                                               <th width="60">Demandas Parlamentares Recursos Reprogramados
                                               </th>
                                               <th width="60">Aplicações Financeiras
                                               </th>
                                               <th width="60">Recursos Humanos
                                               </th>
                                               <th width="60">Material de Consumo
                                               </th>
                                               <th width="60">Outras Despesas
                                               </th>
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
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "TipoUnidade"))%>
                                            </td>
                                            <td class="align-left"style="width:120px;"">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "UnidadeResponsavel"))%>
                                            </td>
                                            <td class="align-left" style="width:120px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "TipoServico"))%>
                                            </td>
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "Usuario"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "CapacidadeDeAtendimento")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "MediaMensalDeAtendimento")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CofinanciamentoEstadual"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosReprogramadosAnoAnterior"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentares"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentaresReprogramados"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAplicacoesFinanceiras"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosHumanos"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "MaterialDeConsumo"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "OutrasDespesas"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <a href="FDespesas.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                   <img src="../Styles/Icones/find.png" alt="Visualizar" border="0" />
                                                </a>                                          
                                            </td>
                                      </tr>
                                  </ItemTemplate>
                                </asp:ListView>

                                <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" align="center" runat="server" id="tbTotalMedia">
                                     <tr class="ui-jqgrid-labels" style="height: 22px">
                                           <td class="ui-state-default ui-th-column ui-th-ltr ui-jqgrid-labels" style="width:520px;text-align: right;" >
                                                Total
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label ID="lblTotalCapacidadeMensalMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:105px;">
                                               <asp:Label ID="lblTotalMediaMensalMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:120px;">
                                              <asp:Label ID="lblTotalCofinanciamentoEstadualMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:115px;">
                                               <asp:Label ID="lblReprogramacaoMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:120px;">
                                               <asp:Label ID="lblTotalDemandasMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:125px;">
                                               <asp:Label ID="lblTotalDemandasReprogramacaoMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:85px;">
                                               <asp:Label ID="lblTotalValoresAplicacoesMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                               <asp:Label ID="lblTotalRecursosHumanosMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:70px;">
                                               <asp:Label ID="lblTotalMaterialConsumoMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                               <asp:Label ID="lblTotalOutrasDespesasMedia" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:65px;">
                                     
                                           </td>
                                     </tr>

                                </table>

                                <br />
                                <br />
                                <asp:ListView ID="lstProtecaoAlta" runat="server">
                                  <LayoutTemplate>
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                        <thead class="info">
                                           <tr>
                                               <th colspan="4" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Serviços da Proteção Social Alta</span>
                                               </th>
                                               <th colspan="2" style="height: 20px;">
                                                   <span >Execução Física</span>
                                               </th>
                                               <th colspan="5" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Receitas</span>
                                               </th>
                                               <th colspan="3" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Despesas</span>
                                               </th>
                                               <th colspan="1" rowspan="2" width="60">Editar
                                               </th>
                                           </tr>
                                           <tr class="ui-jqgrid-labels" style="height: 22px;">
                                               <th width="80">Tipo de Rede
                                               </th>
                                               <th width="120">Unidade/organização
                                               </th>
                                               <th width="120">Tipo de Serviço
                                               </th>
                                               <th width="80">Usuários
                                               </th>
                                               <th width="60">Capacidade Mensal de Atendimento
                                               </th>
                                               <th width="60">Média Mensal de Atendimento
                                               </th>
                                               <th width="60">Cofinanciamento estadual no exercício atual
                                               </th>
                                               <th width="60">Recursos reprogramados do ano anterior
                                               </th>
                                               <th width="60">Demandas Parlamentares
                                               </th>
                                               <th width="60">Demandas Parlamentares Recursos Reprogramados
                                               </th>
                                               <th width="60">Aplicações Financeiras
                                               </th>
                                               <th width="60">Recursos Humanos
                                               </th>
                                               <th width="60">Material de Consumo
                                               </th>
                                               <th width="60">Outras Despesas
                                               </th>
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
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "TipoUnidade"))%>
                                            </td>
                                            <td class="align-left"style="width:120px;"">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "UnidadeResponsavel"))%>
                                            </td>
                                            <td class="align-left" style="width:120px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "TipoServico"))%>
                                            </td>
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "Usuario"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "CapacidadeDeAtendimento")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "MediaMensalDeAtendimento")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CofinanciamentoEstadual"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosReprogramadosAnoAnterior"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentares"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentaresReprogramados"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAplicacoesFinanceiras"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosHumanos"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "MaterialDeConsumo"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "OutrasDespesas"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <a href="FDespesas.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                   <img src="../Styles/Icones/find.png" alt="Visualizar" border="0" />
                                                </a>                                          
                                            </td>
                                      </tr>
                                  </ItemTemplate>
                                </asp:ListView>

                                <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" align="center" runat="server" id="tbTotalAlta">
                                     <tr class="ui-jqgrid-labels" style="height: 22px">
                                           <td class="ui-state-default ui-th-column ui-th-ltr ui-jqgrid-labels" style="width:520px;text-align: right;" >
                                                Total
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label ID="lblTotalCapacidadeMensalAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:105px;">
                                               <asp:Label ID="lblTotalMediaMensalAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:120px;">
                                              <asp:Label ID="lblTotalCofinanciamentoEstadualAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:115px;">
                                               <asp:Label ID="lblReprogramacaoAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:120px;">
                                               <asp:Label ID="lblTotalDemandasAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:125px;">
                                               <asp:Label ID="lblTotalDemandasReprogramacaoAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:85px;">
                                               <asp:Label ID="lblTotalValoresAplicacoesAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                               <asp:Label ID="lblTotalRecursosHumanosAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:70px;">
                                               <asp:Label ID="lblTotalMaterialConsumoAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                               <asp:Label ID="lblTotalOutrasDespesasAlta" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:65px;">
                                     
                                           </td>
                                     </tr>
                                </table>


                                <br />
                                <br />
                                <asp:ListView ID="lstProgramaProjetos" runat="server">
                                  <LayoutTemplate>
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                        <thead class="info">
                                           <tr>
                                               <th style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Programas e/ou projetos</span>
                                               </th>
                                               <th colspan="2" style="height: 20px;">
                                                   <span >Execução Física</span>
                                               </th>
                                               <th colspan="3" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Receitas</span>
                                               </th>
                                               <th colspan="3" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Despesas</span>
                                               </th>
                                               <th colspan="1" rowspan="2" width="60">Editar
                                               </th>
                                           </tr>
                                           <tr class="ui-jqgrid-labels" style="height: 22px;">
                                               <th width="120">Identificação Programa/Projeto
                                               </th>
                                               <th width="60">Demanda Estimada
                                               </th>
                                               <th width="60">Número de Atendidos
                                               </th>
                                               <th width="60">Cofinanciamento estadual no exercício atual
                                               </th>
                                               <th width="60">Recursos reprogramados do ano anterior
                                               </th>
                                               <th width="60">Aplicações Financeiras
                                               </th>
                                               <th width="60">Recursos Humanos
                                               </th>
                                               <th width="60">Material de Consumo
                                               </th>
                                               <th width="60">Outras Despesas
                                               </th>
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
                                            <td class="align-left"style="width:120px;"">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "Nome"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                 <%#DataBinder.Eval(Container.DataItem, "DemandaEstimada")%>
                                            </td>
                                            <td class="align-center" style="width:60px;" >
                                                 <%#DataBinder.Eval(Container.DataItem, "NumeroAtendidos")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CofinanciamentoEstadual"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RecursosReprogramadosAnoAnterior"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAplicacoesFinanceiras"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosHumanos"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "MaterialDeConsumo"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "OutrasDespesas"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <a href="FDespesas.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                   <img src="../Styles/Icones/find.png" alt="Visualizar" border="0" />
                                                </a>                                          
                                            </td>
                                      </tr>
                                  </ItemTemplate>
                                </asp:ListView>

                                <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" align="center" runat="server" id="tbTotalProgramaProjeto">
                                     <tr class="ui-jqgrid-labels" style="height: 22px;">
                                           
                                           <td class="ui-state-default ui-th-column ui-th-ltr ui-jqgrid-labels" style="width:185px;text-align: right;">
                                                Total
                                           </td>
                                           <td class="align-left" style="width:105px;">
                                               <asp:Label runat="server" ID="lblTotalDemandasEstimadas"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label runat="server" ID="lblTotalNumeroAtendidos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:170px;">
                                               <asp:Label runat="server" ID="lbltotalCofinanciamentoEstadualProgramasProjetos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:150px;">
                                               <asp:Label runat="server" ID="lbltotalReprogramacaoProgramasProjetos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:115px;">
                                              <asp:Label runat="server" ID="lbltotalValoresAplicacoesProgramasProjetos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label runat="server" ID="lbltotalRecursosHumanosProgramasProjetos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label runat="server" ID="lbltotalMaterialConsumoProgramasProjetos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label runat="server" ID="lbltotalOutrasDespesasProgramasProjetos"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:80px;">
                                     
                                           </td>
                                     </tr>

                                </table>

                                <br />
                                <br />
                                <asp:ListView ID="lstBeneficiosEventuais" runat="server">
                                  <LayoutTemplate>
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                        <thead class="info">
                                           <tr>
                                               <th style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Benefícios eventuais</span>
                                               </th>
                                               <th colspan="2" style="height: 20px;">
                                                   <span >Execução Física</span>
                                               </th>
                                               <th colspan="4" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Receitas</span>
                                               </th>
                                               <th colspan="4" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Despesas</span>
                                               </th>
                                               <th colspan="1" rowspan="2" width="60">Editar
                                               </th>
                                           </tr>
                                           <tr class="ui-jqgrid-labels" style="height: 22px;">
                                               <th width="120">Tipo de benefício eventual
                                               </th>
                                               <th width="60">Quantidade anual de beneficiários
                                               </th>
                                               <th width="60">Quantidade anual de benefícios concedidos
                                               </th>
                                               <th width="60">Cofinanciamento estadual no exercício atual
                                               </th>
                                               <th width="60">Recursos reprogramados do ano anterior
                                               </th>
                                               <th width="60">Demandas Parlamentares
                                               </th>
                                               <th width="60">Recursos reprogramados demandas parlamentares
                                               </th>
                                               <th width="60">Aplicações Financeiras
                                               </th>
                                               <th width="60">Recursos Humanos
                                               </th>
                                               <th width="60">Material de Consumo
                                               </th>
                                               <th width="60">Outras Despesas
                                               </th>
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
                                            <td class="align-left"style="width:120px;"">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "Nome"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "QuantidadeAnualBeneficiarios")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#DataBinder.Eval(Container.DataItem, "QuantidadeAnualBeneficiariosConcedidos")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CofinanciamentoEstadual"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorRecursosReprogramados"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentares"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValoresDemandasParlamentaresReprogramacao"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAplicacoesFinanceiras"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursosHumanos"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "MaterialDeConsumo"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "OutrasDespesas"))%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <a href="FDespesas.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                   <img src="../Styles/Icones/find.png" alt="Visualizar" border="0" />
                                                </a>                                          
                                            </td>
                                      </tr>
                                  </ItemTemplate>
                                </asp:ListView>

                                <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" align="center" runat="server" id="tbTotalBeneficiosEventuais">
                                     <tr class="ui-jqgrid-labels" style="height: 22px;">
                                           
                                           <td class="ui-state-default ui-th-column ui-th-ltr ui-jqgrid-labels" style="width:190px;text-align: right;">
                                                Total
                                           </td>
                                           <td class="align-left" style="width:135px;">
                                               <asp:Label ID="lblTotalQuantidadeBeneficiariosBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:130px;">
                                               <asp:Label ID="lblTotalQuantidadeBeneficiariosConcedidosBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:160px;">
                                              <asp:Label ID="lblTotalCofinanciamentoEstadualBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:160px;">
                                               <asp:Label ID="lblTotalReprogramadosBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:150px;">
                                               <asp:Label ID="lblTotalDemandasBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:150px;">
                                               <asp:Label ID="lblTotalDemandasBeneficiosEventuaisReprogramacao" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:115px;">
                                               <asp:Label ID="lblTotalValoresAplicacoesFinanceirasBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label ID="lblTotalRecursosHumanosBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label ID="lblTotalMaterialConsumoBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:110px;">
                                               <asp:Label ID="lblTotalOutrasDespesasBeneficiosEventuais" runat="server"></asp:Label>
                                           </td>
                                           <td class="align-left" style="width:90px;">
                                     
                                           </td>
                                     </tr>

                                </table>

                            </div>
                          </ContentTemplate>
                         </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="frame">
                        <div class="heading">
                            2ª - Resumo da execução financeira dos recursos estaduais recebidos no exercício de <asp:Label ID="lblExercicio2" runat="server" />
                        </div>
                        <div class="content">
                          <asp:UpdatePanel ID="pnlAbaDois" runat="server" UpdateMode="Conditional">
                           <ContentTemplate>
                            <div class="formInput" data-text="prestação de contas">
                                <div class="cell">
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <thead class="info">
                                                    <tr>
                                                        <th align="center" width="80" style="height: 40px;">Origem dos<br />
                                                            recursos
                                                        </th>
                                                        <th align="center" width="180">Destinações
                                                        </th>
                                                        <th align="center" width="120">Previsão inicial de<br />
                                                            repasse
                                                        </th>
                                                        <th align="center" width="100">Recursos<br />
                                                            disponibilizados
                                                        </th>
                                                        <th align="center" width="150">Resultado de<br />
                                                            aplicações financeiras
                                                        </th>
                                                        <th align="center" width="100">Valores executados
                                                        </th>
                                                        <th align="center" width="100">Valores passíveis de reprogramação
                                                        </th>
                                                        <th align="center" width="100">Valores devolvidos
                                                        </th>
                                                        <th align="center" width="100">Porcentagens
                                                            <br />
                                                            de Execução
                                                        </th>
                                                    </tr>
                                    </thead>
                                    <tbody>
                                         <tr runat="server" id="trEstadualBasica">
                                          <td runat="server" id="tdSubTitulo" class="info" align="center" rowspan="9">
                                                 <b>Recursos<br />
                                                     estaduais<br />
                                                     (FEAS)</b>
                                             </td>

                                             <td align="left">Serviços da Proteção Social Básica
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoBasica" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>

                                        <tr runat="server" id="trReprogramacaoBasica">
                                             <td>Reprogramação Proteção Básica
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPrevisaoInicialBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoRecursosDisponibilizadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoResultadoAppFinanceirasBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresExecutadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresReprogramadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresDevolvidosBasica" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPorcentagensBasica" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>

                                        <tr runat="server" id="trDemandasParlamentaresBasica">
                                             <td align="left">Demandas Parlamentares na Proteção Social Básica
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialBasicaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosBasicaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasBasicaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosBasicaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosBasicaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosBasicaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoBasicaDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>
										
                                        <tr runat="server" id="trReprogramacaoDemandasParlamentaresBasica">
                                             <td align="left">Reprogramação Demandas Parlamentares na Proteção Social Básica
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialBasicaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosBasicaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasBasicaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosBasicaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosBasicaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosBasicaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoBasicaReprogramacaoDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>

                                         <tr runat="server" id="trEstadualMedia">
                                             <td align="left">Serviços da Proteção Social Especial de Média Complexidade
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoMedia" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>

                                        <tr runat="server" id="trReprogramacaoMedia">
                                             <td>Reprogramação Proteção Média
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPrevisaoInicialMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoRecursosDisponibilizadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoResultadosAppFinanceirasMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresExecutadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresReprogramadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresDevolvidosMedia" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPorcentagensMedia" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>

                                        <tr runat="server" id="trDemandasParlamentaresMedia">
                                             <td align="left">Demandas Parlamentares na Proteção Social Média
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialMediaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosMediaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasMediaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosMediaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosMediaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosMediaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoMediaDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>
										
                                        <tr runat="server" id="trReprogramacaoDemandasParlamentaresMedia">
                                             <td align="left">Reprogramação Demandas Parlamentares na Proteção Social Média
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialMediaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosMediaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasMediaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosMediaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosMediaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosMediaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoMediaReprogramacaoDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>	

                                         <tr runat="server" id="trEstadualAlta">
                                             <td align="left">Serviços da Proteção Social Especial de Alta Complexidade
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoAlta" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>

                                        <tr runat="server" id="trReprogramacaoAlta">
                                             <td>Reprogramação Proteção Alta
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPrevisaoInicialAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoRecursosDisponibilizadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoResultadoAppFinanceirasAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresExecutadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresReprogramadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresDevolvidosAlta" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPorcentagensAlta" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>

                                        <tr runat="server" id="trDemandasParlamentaresAlta">
                                             <td align="left">Demandas Parlamentares na Proteção Social Alta
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialAltaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosAltaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasAltaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosAltaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosAltaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosAltaDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoAltaDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>
										

                                        <tr runat="server" id="trReprogramacaoDemandasParlamentaresAlta">
                                             <td align="left">Reprogramação Demandas Parlamentares na Proteção Social Alta
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialAltaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosAltaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasAltaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosAltaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosAltaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosAltaReprogramacaoDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoAltaReprogramacaoDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>


                                        <tr runat="server" id="trEstadualAnterior" visible="false">
                                             <td>Reprogramação do exercício anterior
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtPrevisaoInicialReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtRecursosDisponibilizadosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtResultadosAplicacaoReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtValoresExecutadosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtValoresReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtValoresDevolvidosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtPorcentagensDevolucaoReprogramacao" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>

                                         <tr runat="server" id="tr1">
                                             <td align="left">Beneficios eventuais
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoBeneficiosEventuais" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>

                                        <tr runat="server" id="trReprogramacaoBeneficiosEventuais">
                                             <td>Reprogramação Benefícios Eventuais
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPrevisaoInicialBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoRecursosDisponiveisBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoResultadosAppFinanceirasBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresExecutadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresReprogramadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoValoresDevolvidosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtReprogramacaoPorcentagensBeneficiosEventuais" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                        </tr>

										 <tr runat="server" id="trDemandasParlamentaresBeneficiosEventuais">
                                             <td align="left">Demandas Parlamentares nos Beneficios eventuais
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialBeneficiosEventuaisDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosBeneficiosEventuaisDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosBeneficiosEventuaisDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosBeneficiosEventuaisDemandas" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandas" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>
										 
										 <tr runat="server" id="trReprogramacaoDemandasParlamentaresBeneficiosEventuais">
                                             <td align="left">Reprogramação Demandas Parlamentares nos Beneficios eventuais
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoBeneficiosEventuaisDemandasReprogramacao" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>											 


                                         <tr runat="server" id="tr2">
                                             <td align="left">Programas e Projetos
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosProgramasProjetos" runat="server" Width="110px"  Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoProgramasProjetos" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>
                                         <tr runat="server" id="trReprogramacaoProgramasProjetos" visible="false">
                                             <td align="left">Reprogramação Programas e Projetos
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPrevisaoInicialProgramasProjetosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASRecursosDisponibilizadosProgramasProjetosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASResultadoAppFinanceirasProgramasProjetosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresExecutadosProgramasProjetosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresReprogramadosProgramasProjetosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASValoresDevolvidosProgramasProjetosReprogramacao" runat="server" Width="110px"  Style="text-align: right" />
                                             </td>
                                             <td align="center">
                                                 <asp:TextBox ID="txtFEASPorcentagensExecucaoProgramasProjetosReprogramacao" runat="server" Width="80px" Style="text-align: right" />
                                             </td>
                                         </tr>
                                         <tr class="info" style="height: 20px" runat="server" id="trEstadualTotais">
                                             <td align="right">
                                                 <b>Totais</b>
                                             </td>
                                             <td align="center"></td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASPrevisaoInicial" runat="server" />
                                             </td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASRecursosDisponibilizados" runat="server" />
                                             </td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASResultadoAppFinanceiras" runat="server" />
                                             </td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASValoresExecutados" runat="server" />
                                             </td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASValoresReprogramados" runat="server" />
                                             </td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASValoresDevolvidos" runat="server" />
                                             </td>
                                             <td align="center">
                                                 <asp:Label ID="lblFEASPorcentagensExecucao" runat="server" />
                                             </td>
                                         </tr>
                                    </tbody>
                                    </table>
                                    <div class="row" runat="server" id="trBotaoCalcular">
                                        <center>
                                             <asp:Button runat="server" ID="btnCalcular" Text="Calcular" OnClick="btnCalcular_Click"/>       
                                         </center>
                                    </div>
                                    <div class="row" runat="server" id="trComentarioCMAS" >
                                      <fieldset class="border-blue">                                                                    
                                            <legend class="lgnd"><b class="fg-blue">Comentários do órgão gestor municipal da Assistência Social sobre a execução financeira dos recursos estaduais</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" MaxLength="5000" Height="64px" Width="100%" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <skm:TextBoxCounter ID="NameCounter2" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 5000 caracteres."
                                                        Font-Bold="True" TextBoxControlId="txtComentario" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <b>
                                            <p style="text-align:justify;border:solid 1px #c2c1c1;padding:5px;font-size:medium"> As informações aqui registradas são de inteira responsabilidade de seus declarantes, que deverão manter arquivados na sede do município os documentos comprobatórios legalmente aceitos das despesas realizadas na execução do objeto da transferência, em boa ordem e conservação, identificados e à disposição da Secretaria Estadual de Desenvolvimento Social (SEDS) de São Paulo e dos órgãos de controle interno e externo, pelo prazo de 5 (cinco) anos ou pelo prazo determinado em legislações específicas.										
                                            </p>
                                        </b>
                                    </div>
                                    <div class="row" runat="server" id="trResponsavel" style="background-color:#21a3f1;">
                                        <strong>
                                            <asp:Label runat="server" ID="lblResponsavelOrgaoGestorTitulo" Text=" Responsável pelas informações pelo órgão gestor municipal de Assistência Social: "></asp:Label>
                                            &nbsp<asp:Label runat="server" ID="lblNomeOrgaoGestor" Text="Nome Responsável gestor"></asp:Label> 
                                            &nbsp<asp:Label runat="server" text="CPF:"></asp:Label>
                                            &nbsp<asp:Label runat="server" ID="lblCPFOrgaoGestor" Text="xxx.xxx.xxx-xx"></asp:Label>
                                            &nbsp<asp:Label runat="server" Text="Data:"></asp:Label>
                                            &nbsp<asp:Label runat="server" ID ="lblDataOrgaoGestor" Text="00/00/0000"></asp:Label>
                                        </strong>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row" runat="server" id="trBotoesOrgaoGestor">
                                        <center><asp:Button runat="server" ID="btnSalvar"  Text="Salvar" OnClick="btnSalvar_Click"/>&nbsp&nbsp
                                        <asp:Button runat="server" ID="btnFinalizar" Text="Finalizar" Enabled="false" OnClick="btnFinalizar_Click" /></center>
                                    </div>
                                </div>
                            </div>
                           </ContentTemplate>
                           <Triggers>
                               <asp:PostBackTrigger ControlID="btnFinalizar"/>
                           </Triggers>
                          </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="frame">
                        <div class="heading">
                            3ª - Comentários, parecer e deliberação do Conselho Municipal de Assistência Social sobre a execução financeira dos recursos estaduais recebidos no exercício de <asp:Label ID="lblExercicio3" runat="server" />
                        </div>
                        <div class="content">
                            <asp:UpdatePanel runat="server" ID="pnlAbaTres" UpdateMode="Conditional">
                            <ContentTemplate>
                            <div class="formInput" data-text="prestação de contas">
                                <div class="row" >
                                    <legend class="lgnd"><p style="background-color:#21a3f1;"><B>Questões Auxiliares</B></p></legend>
                                    
                                    <legend class="lgnd"><p>1. O Conselho acompanhou a execução do orçamento da Assistência Social no município?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoUmCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim, Mensalmente</asp:ListItem>
                                        <asp:ListItem Value="2">Sim, Trimestralmente</asp:ListItem>
                                        <asp:ListItem Value="3">Sim, Semestralmente</asp:ListItem>
                                        <asp:ListItem Value="4">Sim, Anualmente</asp:ListItem>
                                        <asp:ListItem Value="5">Não acompanhou</asp:ListItem>
                                    </asp:RadioButtonList>
                                    
                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>2. Os recursos estaduais destinados a execução dos serviços/programas/benefícios foram utilizados conforme as normas e legislação estabelecidas?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoDoisCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim, Todos os recursos</asp:ListItem>
                                        <asp:ListItem Value="2">Sim, Parte dos recursos</asp:ListItem>
                                        <asp:ListItem Value="3">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>3. Os relatórios de execução orçamentária e financeira foram apresentados ao Conselho Municipal de Assistência Social?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoTresCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim, os relatórios foram apresentados integralmente</asp:ListItem>
                                        <asp:ListItem Value="2">Sim, parcialmente</asp:ListItem>
                                        <asp:ListItem Value="3">Não, os relatórios não foram apresentados</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>4. Com qual periodicidade os relatórios de execução orçamentária e financeira foram apresentados ao Conselho Municipal de Assistência Social pelo órgão gestor?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoQuatroCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Mensalmente</asp:ListItem>
                                        <asp:ListItem Value="2">Trimestralmente</asp:ListItem>
                                        <asp:ListItem Value="3">Semestralmente</asp:ListItem>
                                        <asp:ListItem Value="4">Anualmente</asp:ListItem>
                                        <asp:ListItem Value="5">Não, os relatórios não foram apresentados</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>5. O município cofinancfiou os serviços/programas/benefícios?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoCincoCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim, somente através do Fundo Municipal de Assistência Social</asp:ListItem>
                                        <asp:ListItem Value="2">Sim, somente através de outras fontes</asp:ListItem>
                                        <asp:ListItem Value="3">Sim, através do Fundo Municipal de Assistência Social e de outras fontes </asp:ListItem>
                                        <asp:ListItem Value="4">O município não cofinanciou</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd" ><p>6. Os serviços/programas/benefícios cofinanciados pelo Estado foram ofertados à população de maneira regular e continuada durante este exercício?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoSeisCMAS" RepeatDirection="Horizontal" Style="margin-left:4%" AutoPostBack="true" OnSelectedIndexChanged="rblQuestaoSeisCMAS_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Sim, todos</asp:ListItem>
                                        <asp:ListItem Value="2">Alguns tiveram solução de continuidade</asp:ListItem>
                                        <asp:ListItem Value="3">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <legend class="lgnd" runat="server" id="lgdQuestaoSeisCMAS">6.a. Identifique no quadro abaixo quais serviços/programas/benefícios cofinanciados pelo Estado sofreram solução de continuidade em sua oferta durante este exercício:</legend>
                                    <asp:TextBox runat="server" ID="txtQuestaoSeisCMAS" style="margin-left:1%" TextMode="MultiLine" MaxLength="1000" Height="64px" Width="100%"></asp:TextBox>
                                    

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>7. O Conselho teve algum tipo de dificuldade em analisar as informações prestadas pelo órgão gestor municipal?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoSeteCMAS" RepeatDirection="Horizontal" Style="margin-left:4%" AutoPostBack="true" OnSelectedIndexChanged="rblQuestaoSeteCMAS_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="3">Não</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <legend class="lgnd" runat="server" id="lgdQuestaoSeteCMAS">7.a. Explicite no quadro abaixo quais foram as dificuldades encontradas pelo CMAS para análise das informações?</legend>
                                    <asp:TextBox runat="server" ID="txtQuestaoSeteCMAS" style="margin-left:1%" TextMode="MultiLine" MaxLength="1000" Height="64px" Width="100%"></asp:TextBox>
                                    
                                    
                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>8. O Conselho Municipal de Assistência Social possui livre acesso às documentações comprobatórias dos gastos?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoOitoCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim, totalmente</asp:ListItem>
                                        <asp:ListItem Value="2">Sim, parcilamente</asp:ListItem>
                                        <asp:ListItem Value="3">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>9. O Conselho considera as despesas efetuadas no exercício como comprovadas?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoNoveCMAS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="2">Parcilamente</asp:ListItem>
                                        <asp:ListItem Value="3">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <fieldset class="border-blue"   width="100%">
                                          <legend class="lgnd"><b class="fg-blue">Comentários e Parecer do Conselho Municipal de Assistência Social</b></legend>
                                          <div class="row">
                                              <div class="cell">
                                                  <div class="input-control textarea full-size">
                                                      <asp:TextBox ID="txtComentarioCMAS" runat="server" TextMode="MultiLine" MaxLength="5000"
                                                          Height="64px" Width="100%" />
                                                  </div>
                                              </div>
                                          </div>
                                          <div class="row">
                                              <div class="cell" align="center">
                                                  <skm:TextBoxCounter ID="TextBoxCounter3" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 5000 caracteres."
                                                      Font-Bold="True" TextBoxControlId="txtComentarioCMAS" />
                                              </div>
                                          </div>
                                    </fieldset>                                    
                                    <div class="cell" align="left" style="background-color:#7CC8FF">
                                    <b>Deliberação do Conselho Municipal de Assistência Social</b>
                                    </div>
                                    <div class="cell" style="border:solid 1px #d9d9d9">
                                        <asp:RadioButtonList runat="server" ID="rblDeliberacaoCMAS" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDeliberacaoCMAS_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value ="1">Aprovado</asp:ListItem>
                                            <asp:ListItem Value="2">Aprovado com ressalvas</asp:ListItem>
                                            <asp:ListItem Value ="3">Rejeitado</asp:ListItem>
                                            <asp:ListItem Value="4">Foram detectadas incorreções nos registros que devem ser corrigidas</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="cell" style="border:solid 1px #d9d9d9;height:80px" Width="100%">
                                        <br />
                                        &nbsp&nbsp<asp:Label ID="Label1" runat="server"><b>Data da reunião:</b></asp:Label>&nbsp <asp:TextBox runat="server"  ID="txtDataReuniaoCMAS" onkeypress="mascaraData(this)" Width="110px" MaxLength="10" ></asp:TextBox> &nbsp&nbsp

                                        <asp:Label ID="Label2" runat="server"><b>Nº de conselheiros participantes com direito a voto:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroConselheirosCMAS"  Width="110px" MaxLength="5" ></asp:TextBox> &nbsp
                                        
                                        <asp:Label ID="Label3" runat="server"><b>Nº da Ata:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroAtaCMAS" Width="110px" MaxLength="5"></asp:TextBox> &nbsp
                                        
                                        <asp:Label ID="Label4" runat="server"><b>Nº da Resolução:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroResolucaoCMAS" Width="110px" MaxLength="5"></asp:TextBox> &nbsp
                                        
                                        <asp:Label ID="Label5" runat="server"><b>Data de publicação:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtDataPublicacaoCMAS" Width="110px" MaxLength="10" onkeypress="mascaraData(this)"> </asp:TextBox>
                                    </div>
                                    <div class="cell" runat="server" id="lgdUsuarioCMAS" width="100%">
                                        
                                        <p style="background-color:#7CC8FF;">
                                            <b>
                                             <asp:Label ID="Label6" runat="server" Text="Responsável pelo registro das informações do CMAS:"></asp:Label>&nbsp
                                             <asp:Label ID="lblNomeCMAS" runat="server" Text ="Nome Responsável CMAS"></asp:Label>&nbsp&nbsp
                                             <asp:Label ID="Label7" runat="server" Text="CPF:"></asp:Label>&nbsp
                                             <asp:Label ID="lblCpfCMAS"  runat="server" text="xxx.xxx.xxx-xx" ></asp:Label>&nbsp&nbsp
                                             <asp:Label ID="Label8" runat="server" Text="Data:"></asp:Label>&nbsp
                                             <asp:Label ID="lblDataCMAS" runat="server" text="00/00/0000"></asp:Label>
                                            </b>
                                        </p>
                                        
                                    </div>
                                    <div class="cell">
                                        <asp:Button ID="btnSalvarCMAS" runat="server" Text="Salvar" onclick="btnSalvarCMAS_Click" />&nbsp&nbsp
                                        <asp:Button ID="btnDevolverCMAS" runat="server" Text="Devolver ao órgão gestor para alterações"  Enabled="false"  onclick="btnDevolverCMAS_Click" />
                                        <br /><br />
                                        <asp:Button ID ="btnFinalizarCMAS" runat="server" Text="Finalizar" Enabled ="false" onclick="btnFinalizarCMAS_Click" />
                                    </div>
                                    </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnDevolverCMAS"/>
                                    <asp:PostBackTrigger ControlID="btnFinalizarCMAS"/>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="frame">
                        <div class="heading">
                            4ª - Análise e manifestação do órgão gestor estadual (Diretoria Regional de Assistência e Desenvolvimento Social - DRADS) sobre a execução financeira dos recursos estaduais recebidos no exercício de <asp:Label ID="lblExercicio4" runat="server" />
                        </div>
                        <div class="content">
                            <asp:UpdatePanel ID ="pnlAbaQuatro" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <div class="formInput" data-text="prestação de contas">
                                <div class="row">
                                    <legend class="lgnd"><p style="background-color:#21a3f1;"><B>Questões Auxiliares</B></p></legend>
                                    
                                    <legend class="lgnd"><p>1. As respostas registradas no parecer do CMAS suscitam alguma dúvida sobre a correção da prestação de contas?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoUmDRADS" RepeatDirection="Horizontal" Style="margin-left:4%" AutoPostBack="true" OnSelectedIndexChanged="rblQuestaoUmDRADS_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="2">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <legend class="lgnd" runat="server" id="lgdQuestaoUmDRADS">1.a. Explicite no quadro abaixo quais foram as dúvidas encontradas?</legend>
                                    <asp:TextBox runat="server" ID="txtQuestaoUmDRADS" style="margin-left:1%" TextMode="MultiLine" MaxLength="1000" Height="64px" Width="100%"></asp:TextBox>
                                    
                                    
                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <asp:RadioButtonList runat="server" ID="rblQuestaoDoisDRADS" RepeatDirection="Horizontal" Style="margin-left:4%" Visible="false">
                                        <asp:ListItem Value="1">Sim, Todos os recusos</asp:ListItem>
                                        <asp:ListItem Value="2">Sim, Parte dos recursos</asp:ListItem>
                                        <asp:ListItem Value="3">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <legend class="lgnd"><p>2. Houve alguma ressalva mencionada pelo CMAS quanto à prestação de contas?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoTresDRADS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="2">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>3. De acordo com o monitoramento realizado pela Diretoria Regional de Assistência e Desenvolvimento Social, a aplicação dos recursos do Fundo Estadual de Assistência Social foi feita em conformidade com as ações registradas no Sistema dos Planos Municipais de Assistência Social - PMASweb?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoQuatroDRADS" RepeatDirection="Horizontal" Style="margin-left:4%">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="2">Parcialmente</asp:ListItem>
                                        <asp:ListItem Value="5">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <legend class="lgnd"><p>4. Os serviços/programas/benefícios cofinanciados pelo Estado foram ofertados à população de maneira regular e continuada durante este exercício?</p></legend>
                                    <asp:RadioButtonList runat="server" ID="rblQuestaoCincoDRADS" RepeatDirection="Horizontal" Style="margin-left:4%" AutoPostBack="true" OnSelectedIndexChanged="rblQuestaoCincoDRADS_SelectedIndexChanged" >
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="2">Alguns tiveram solução de continuidade</asp:ListItem>
                                        <asp:ListItem Value="4">Não</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <legend class="lgnd" runat="server" id="lgdQuestaoCincoDRADS">4.a. Identifique no quadro abaixo quais serviços/programas/benefícios cofinanciados pelo Estado sofreram solução de continuidade em sua oferta durante este exercício:</legend>
                                    <asp:TextBox runat="server" ID="txtQuestaoCincoDRADS" style="margin-left:1%" TextMode="MultiLine" MaxLength="1000" Height="64px" Width="100%"></asp:TextBox>

                                    <hr style="border-bottom-color:#c2c1c1;" />

                                    <fieldset class="border-blue"   width="100%">
                                          <legend class="lgnd"><b class="fg-blue">Análise, comentários e manifestação da Diretoria Regional de Assistência e Desenvolvimento Social (DRADS)</b></legend>
                                          <div class="row">
                                              <div class="cell">
                                                  <div class="input-control textarea full-size">
                                                      <asp:TextBox ID="txtComentarioDRADS" runat="server" TextMode="MultiLine" MaxLength="5000"
                                                          Height="64px" Width="100%" />
                                                  </div>
                                              </div>
                                          </div>
                                          <div class="row">
                                              <div class="cell" align="center">
                                                  <skm:TextBoxCounter ID="TextBoxCounter6" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 5000 caracteres."
                                                      Font-Bold="True" TextBoxControlId="txtComentarioDRADS" />
                                              </div>
                                          </div>
                                    </fieldset>                                    
                                    <div class="cell" align="left" style="background-color:#7CC8FF">
                                    <b>Posição final da Diretoria Regional de Assistência e Desenvolvimento Social (DRADS) sobre a prestação de contas</b>
                                    </div>
                                    <div class="cell" style="border:solid 1px #d9d9d9">
                                        <asp:RadioButtonList runat="server" ID="rblDeliberacaoDRADS" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDeliberacaoDRADS_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value ="1">Favorável</asp:ListItem>
                                            <asp:ListItem Value ="2">Desfavorável</asp:ListItem>                                            
                                            <asp:ListItem Value ="3">Foram detectadas incorreções nos registros que devem ser corrigidas</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div runat="server" class="cell" style="border:solid 1px #d9d9d9;height:80px" Width="100%" visible="false" >
                                        <br />
                                        &nbsp&nbsp<asp:Label ID="Label9" runat="server"><b>Data da reunião:</b></asp:Label>&nbsp <asp:TextBox runat="server"  ID="txtDataReuniaoDRADS" onkeypress="mascaraData(this)" Width="110px" MaxLength="10" ></asp:TextBox> &nbsp&nbsp

                                        <asp:Label ID="Label10" runat="server"><b>Nº de conselheiros participantes com direito a voto:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroConselheirosDRADS"  Width="110px" MaxLength="5" ></asp:TextBox> &nbsp
                                        
                                        <asp:Label ID="Label11" runat="server"><b>Nº da Ata:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroAtaDRADS" Width="110px" MaxLength="5"></asp:TextBox> &nbsp
                                        
                                        <asp:Label ID="Label12" runat="server"><b>Nº da Resolução:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroResolucaoDRADS" Width="110px" MaxLength="5"></asp:TextBox> &nbsp
                                        
                                        <asp:Label ID="Label13" runat="server"><b>Data de publicação:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtDataPublicacaoDRADS" Width="110px" MaxLength="10" onkeypress="mascaraData(this)"> </asp:TextBox>
                                    </div>
                                    <div class="cell" runat="server" id="DivUsuarioCMAS" width="100%">
                                        
                                        <p style="background-color:#7CC8FF;">
                                            <b>
                                             <asp:Label ID="Label14" runat="server" Text="“Responsável pelo registro das informações sobre a manifestação da DRADS:"></asp:Label>&nbsp
                                             <asp:Label ID="lblNomeDRADS" runat="server" Text="Nome Responsável Drads"></asp:Label>&nbsp&nbsp
                                             <asp:Label ID="Label16" runat="server" Text="CPF:"></asp:Label>&nbsp
                                             <asp:Label ID="lblCPFDRADS"  runat="server" text="xxx.xxx.xxx-xx"></asp:Label>&nbsp&nbsp
                                             <asp:Label ID="Label18" runat="server" Text="Data:"></asp:Label>&nbsp
                                             <asp:Label ID="lblDataDRADS" runat="server" text="00/00/0000"></asp:Label>
                                            </b>
                                        </p>
                                        
                                    </div>
                                    <div class="cell">
                                        <asp:Button ID="btnSalvarDRADS" runat="server" Text="Salvar" onclick="btnSalvarDRADS_Click" />&nbsp&nbsp
                                        <asp:Button ID="btnDevolverDRADS" runat="server" Text="Devolver ao órgão gestor para alterações" Enabled="false" onclick="btnDevolverDRADS_Click" />
                                    </div>
                                    <div class ="cell" style="background-color:#7CC8FF;">
                                        <asp:CheckBox runat="server"  ID="chkDeAcordo" Text="De Acordo" Enabled="false" autopostback="true"/>
                                    </div>
                                    <div class="cell">
                                        <asp:Button ID="btnFinalizarDrads" runat="server" Text="Finalizar" Enabled="false" OnClick="btnFinalizarDrads_Click"/>
                                    </div>
                                </div>
                            </div>
                           </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnFinalizarDrads" />
                                    <asp:PostBackTrigger ControlID="btnDevolverDRADS" />
                                </Triggers>
                          </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="frame">
                        <div class="heading">
                            5ª - Histórico do fluxo do quadro da prestação de contas do exercício de <asp:Label ID="lblExercicio5" runat="server" />
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="prestação de contas">
                                <asp:ListView ID="lstHistorico" runat="server">
                                  <LayoutTemplate>
                                    <table class="table border bordered" cellspacing="0" cellpadding="0" border="0">
                                        <thead class="info">
                                           <tr>
                                               <th colspan="5" style="height: 20px;">
                                                   <span class="ui-jqgrid-title">Histórico Prestação de Contas</span>
                                               </th>
                                           </tr>
                                           <tr class="ui-jqgrid-labels" style="height: 22px;">
                                               <th width="80">Data
                                               </th>
                                               <th width="120">Situacao
                                               </th>
                                               <th width="120">Responsável
                                               </th>
                                               <th width="80">Descrição/Motivo
                                               </th>
                                               <th width="60">Visualizar
                                               </th>
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
                                            <td class="align-left" style="width:80px;">
                                                <%#((DateTime)DataBinder.Eval(Container.DataItem, "Data")).ToString("dd/MM/yyyy HH:mm")%>
                                            </td>
                                            <td class="align-left"style="width:120px;"">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "SituacaoStatus"))%>
                                            </td>
                                            <td class="align-left" style="width:120px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "NomeResponsavel"))%>
                                            </td>
                                            <td class="align-left" style="width:80px;">
                                                <%#((String)DataBinder.Eval(Container.DataItem, "DescricaoMotivo")).Length > 150 ? (((String)DataBinder.Eval(Container.DataItem, "DescricaoMotivo")).Substring(0,150) + " ...") : DataBinder.Eval(Container.DataItem, "DescricaoMotivo")%>
                                            </td>
                                            <td class="align-left" style="width:60px;">
                                                <a href="FHistorico.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                   <img src="../Styles/Icones/find.png" alt="Visualizar" border="0" />
                                                </a>                                          
                                            </td>
                                      </tr>
                                  </ItemTemplate>
                                </asp:ListView>
                                <center><asp:Label ID="lblSemDadosHistoricos" runat="server" Visible="false" Text ="Ainda não foram preenchidas informações"></asp:Label></center>
                            </div>
                           
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="divDesbloqueio" visible="false">
                      <div class="cell" align="center">
                          <div class="row">
                              <div class="cell">
                                  Deseja desbloquear quadro de Prestação de contas?
                              </div>
                          </div>
                          <div class="row">
                              <div class="cell">
                                  <asp:Button ID="btnDesbloqueio" runat="server" TabIndex="16" Width="146px" Text="Desbloquear" SkinID="button-save" OnClick="btnDesbloqueio_Click" />
                              </div>
                          </div>
                      </div>
                </div>
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
               <asp:HiddenField ID="hdfAno" runat="server" Value="" />
               <asp:HiddenField ID="hdnSomaCapacidadeMediaAtendimentoBasica" runat="server" />
                <asp:HiddenField ID="hdnSomaCapacidadeMediaAtendimentoMedia" runat="server" />
                <asp:HiddenField ID="hdnSomaCapacidadeMediaAtendimentoAlta" runat="server" />
                
                <asp:HiddenField ID="hdnMediaMensalAtendimentoBasica" runat="server" />
                <asp:HiddenField ID="hdnMediaMensalAtendimentoMedia" runat="server" />
                <asp:HiddenField ID="hdnMediaMensalAtendimentoAlta" runat="server" />
                                                
                <asp:HiddenField ID="hdnCofinanciamentoEstadualBasica" runat="server" />
                <asp:HiddenField ID="hdnCofinanciamentoEstadualMedia" runat="server" />
                <asp:HiddenField ID="hdnCofinanciamentoEstadualAlta" runat="server" />
                
                <asp:HiddenField ID="hdnRecursosReprogramadosBasica" runat="server" />
                <asp:HiddenField ID="hdnRecursosReprogramadosMedia" runat="server" />
                <asp:HiddenField ID="hdnRecursosReprogramadosAlta" runat="server" />

                <asp:HiddenField ID="hdnValoresAplicacoesBasica" runat="server" />
                <asp:HiddenField ID="hdnValoresAplicacoesMedia" runat="server" />
                <asp:HiddenField ID="hdnValoresAplicacoesAlta" runat="server" />

                <asp:HiddenField ID="hdnDemandasBasica" runat="server" />
                <asp:HiddenField ID="hdnDemandasMedia" runat="server" />
                <asp:HiddenField ID="hdnDemandasAlta" runat="server" />									 
                				
                <asp:HiddenField ID="hdnDemandasReprogramacaoBasica" runat="server" />
                <asp:HiddenField ID="hdnDemandasReprogramacaoMedia" runat="server" />
                <asp:HiddenField ID="hdnDemandasReprogramacaoAlta" runat="server" />	


                <asp:HiddenField ID="hdnRecursosHumanosBasica" runat="server" />
                <asp:HiddenField ID="hdnRecursosHumanosMedia" runat="server" />
                <asp:HiddenField ID="hdnRecursosHumanosAlta" runat="server" />

                <asp:HiddenField ID="hdnMaterialDeConsumoBasica" runat="server" />
                <asp:HiddenField ID="hdnMaterialDeConsumoMedia" runat="server" />
                <asp:HiddenField ID="hdnMaterialDeConsumoAlta" runat="server" />

                <asp:HiddenField ID="hdnOutrasDespesasBasica" runat="server" />
                <asp:HiddenField ID="hdnOutrasDespesasMedia" runat="server" />
                <asp:HiddenField ID="hdnOutrasDespesasAlta" runat="server" />



                <asp:HiddenField ID="hdnCofinanciamentoEstadualProgramasProjetos" runat="server" />
                <asp:HiddenField ID="hdnRecursosReprogramadosProgramasProjetos" runat="server" />
                <asp:HiddenField ID="hdnValoresAplicacoesProgramasProjetos" runat="server" />
                <asp:HiddenField ID="hdnRecursosHumanosProgramasProjetos" runat="server" />
                <asp:HiddenField ID="hdnMaterialDeConsumoProgramasProjetos" runat="server" />
                <asp:HiddenField ID="hdnOutrasDespesasProgramasProjetos" runat="server" />


                <asp:HiddenField ID="hdnCofinanciamentoEstadualMaisDemandasBasica" runat="server" />
                <asp:HiddenField ID="hdnCofinanciamentoEstadualMaisDemandasMedia" runat="server" />
                <asp:HiddenField ID="hdnCofinanciamentoEstadualMaisDemandasAlta" runat="server" />
                <asp:HiddenField ID="hdnCofinanciamentoEstadualMaisDemandasBeneficiosEventuais" runat="server" />

                <asp:HiddenField ID="hdnSomaDosReprogramadosBasica" runat="server" />
                <asp:HiddenField ID="hdnSomaDosReprogramadosMedia" runat="server" />
                <asp:HiddenField ID="hdnSomaDosReprogramadosAlta" runat="server" />

                <asp:HiddenField ID="hdnQuantidadeBeneficiariosBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnQuantidadeBeneficiariosConcedidosBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnCofinanciamentoEstadualBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnReprogramadosBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnDemandasBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnDemandasReprogramacaoBeneficiosEventuais" runat="server" />                
                <asp:HiddenField ID="hdnValoresAplicacoesFinanceirasBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnRecursosHumanosBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnMaterialConsumoBeneficiosEventuais" runat="server" />
                <asp:HiddenField ID="hdnOutrasDespesasBeneficiosEventuais" runat="server" />
            </form>

</asp:Content>