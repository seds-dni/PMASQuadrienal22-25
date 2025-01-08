<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CLocaisPublicoDesativados.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CLocaisPublicoDesativados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                           Locais de execução dos serviços da rede direta desativados
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Rede Direta">
                                <div class="grid">
                                    <div class="row cells5">
                                        <div class="cell colspan4">
                                        </div>
                                        <div class="cell ">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro16" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstUnidades" runat="server" DataKeyNames="Id" OnItemDataBound="lstUnidades_ItemDataBound"
                                                OnItemCommand="lstUnidades_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="20" style="height: 22px;" rowspan="2"></th>
                                                                <th width="50" rowspan="2">Visualizar
                                                                </th>
                                                                <th width="70" rowspan="2">Código PMAS
                                                                </th>
                                                                <th width="300" rowspan="2">Nome
                                                                </th>
                                                                <th width="220" rowspan="2"><%--Bruno V. --%>
                                                                            Responsável
                                                                </th>
                                                                <th width="230" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                    valign="top">Serviços socioassistenciais
                                                                </th>
                                                                <th width="50" rowspan="2">Data de Desativação
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="70">Visualizar
                                                                </th>
                                                                <th width="90">Total de
                                                                    serviços desativados
                                                                </th>
                                                                <th width="90">Cofinanciamento<br />
                                                                    estadual
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
                                                        <td class="info" style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                                                CommandName="Visualizar" />
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                        </td>
                                                        <td class="align-left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Responsavel") %>&nbsp;
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros desativados"
                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TotalServicosDesativados") %>
                                                        </td>
                                                        <td class="align-center">
                                                              <div style="width: 100%; height: 22px; padding: 2px; font-size: 10px; text-align: center;">
                                                                  <asp:Repeater ID="rptCofinanciamentos" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "Cofinanciamentos") %>'>
                                                                      <HeaderTemplate>
                                                                      </HeaderTemplate>
                                                                      <ItemTemplate>
                                                                          <asp:Panel ID="painelConfinanciamentoExercicio1" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2018)? true : false %>'>
                                                                              <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                  <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                      <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                          <b>2018</b>
                                                                                      </div>
                                                                                  </div>
                                                                                  <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                      <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                          <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                      </div>
                                                                                  </div>
                                                                              </div>
                                                                          </asp:Panel>

                                                                          <asp:Panel ID="painelConfinanciamentoExercicio2" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2019)? true : false %>'>
                                                                              <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                  <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                      <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                          <b>2019</b>
                                                                                      </div>
                                                                                  </div>
                                                                                  <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                      <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                          <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                      </div>
                                                                                  </div>
                                                                              </div>
                                                                          </asp:Panel>

                                                                          <asp:Panel ID="painelConfinanciamentoExercicio3" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2020)? true : false %>'>
                                                                              <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                  <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                      <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                          <b>2020</b>
                                                                                      </div>
                                                                                  </div>
                                                                                  <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                      <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                          <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                      </div>
                                                                                  </div>
                                                                              </div>
                                                                          </asp:Panel>
                                                                          <asp:Panel ID="painelConfinanciamentoExercicio4" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2021)? true : false %>'>
                                                                              <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                  <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                      <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                          <b>2021</b>
                                                                                      </div>
                                                                                  </div>
                                                                                  <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                      <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                          <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                      </div>
                                                                                  </div>
                                                                              </div>
                                                                          </asp:Panel>
                                                                      </ItemTemplate>
                                                                  </asp:Repeater>
                                                              </div>                                                            
                                                        </td>
                                                        <td class="align-center">
                                                              <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataEncerramento")).ToShortDateString() %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro de locais de execução da rede direta desativados</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            <div class="row">
                                                <div class="cell">
                                                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" SkinID="button-save" OnClick="btnVoltar_Click" />
                                                </div>
                                            </div>
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
<%--                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FAnaliseDiagnostica.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="CUnidadesPrivadas.aspx">Próximo
                               <span class="mif-arrow-right"></span></a>
                    </td>--%>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
