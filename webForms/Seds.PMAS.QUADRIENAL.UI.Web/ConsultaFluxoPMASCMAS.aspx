<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ConsultaFluxoPMASCMAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.ConsultaFluxoPMASCMAS" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="pnlConsulta">
        <ContentTemplate>
            <form name="frmConsulta">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>Consultar/Fluxo PMAS</b>
                            <span class="mif-flow-cascade icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Consulta">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Plano Municipal de
                                <asp:Label ID="lblMunicipio" runat="server" /></b>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Situação do Plano: </b>
                                            <asp:Label ID="lblSituacao" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <b>Fluxo/PMAS: </b>
                                            <asp:LinkButton ID="lnkDevolverOrgaoGestor" runat="server" Text="Devolver para Órgão Gestor"
                                                OnClientClick="return confirm('Tem certeza que deseja Devolver para o Órgão Gestor ?');"
                                                OnClick="lnkDevolverOrgaoGestor_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkParecer" runat="server"
                                Text="Parecer sobre as alterações" OnClick="lnkParecer_Click"></asp:LinkButton>
                                            <asp:Label ID="lblSituacaoFluxo" runat="server" Visible="false" />
                                        </div>
                                    </div>

                                    <hr />

                                    <div class="row">
                                        <div class="cell">
                                            <b>Execução financeira:</b>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" >
                                            <div style="float:left; position:relative; margin-right:10px;">
                                                <b>2022:</b>
                                                <asp:Label ID="lblSituacaoQuadroEFExercicio1" Text="Não Informado" runat="server" />
                                            </div>
                                            <div style="float:left; position:relative;margin-right:10px; ">
                                                <b>2023:</b>
                                                <asp:Label ID="lblSituacaoQuadroEFExercicio2" Text="Não Informado" runat="server" />
                                            </div>
                                            <div style="float:left; position:relative;margin-right:10px; ">
                                                <b>2024:</b>
                                                <asp:Label ID="lblSituacaoQuadroEFExercicio3" Text="Não Informado" runat="server" />
                                            </div>
                                            <div style="float:left; position:relative;margin-right:10px; ">
                                                <b>2025:</b>
                                                <asp:Label ID="lblSituacaoQuadroEFExercicio4" Text="Não Informado" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <b>Lei Orçamentária:</b>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <div style="float:left; position:relative; margin-right:10px;">
                                                <b>2022:</b>
                                                <asp:Label ID="lblSituacaoQuadroLOExercicio1" Text="Não Informado" runat="server" />
                                            </div>
                                            <div style="float:left; position:relative;margin-right:10px; ">
                                                <b>2023:</b>
                                                <asp:Label ID="lblSituacaoQuadroLOExercicio2" Text="Não Informado" runat="server" />
                                            </div>
                                            <div style="float:left; position:relative;margin-right:10px; ">
                                                <b>2024:</b>
                                                <asp:Label ID="lblSituacaoQuadroLOExercicio3" Text="Não Informado" runat="server" />
                                            </div>
                                            <div style="float:left; position:relative;margin-right:10px; ">
                                                <b>2025:</b>
                                                <asp:Label ID="lblSituacaoQuadroLOExercicio4" Text="Não Informado" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <br />
                                            <asp:ListView ID="lstHistorico" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0" width="750">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="5"
                                                                    style="height: 20px;">
                                                                    <span class="ui-jqgrid-title">Histórico do Plano</span>
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="80">Data
                                                                </th>
                                                                <th width="120">Situação
                                                                </th>
                                                                <th width="200">Responsável
                                                                </th>
                                                                <th width="400">Descrição/Motivo
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
                                                    <tr style="height: 22px;">
                                                        <td align="center">
                                                            <b><%#((DateTime)DataBinder.Eval(Container.DataItem, "Data")).ToString("dd/MM/yyyy HH:mm") %></b>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Situacao")%>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Usuario")%>
                                                        </td>
                                                        <td class="align-left">
                                                            <%#((String)DataBinder.Eval(Container.DataItem, "Descricao")).Length > 150 ? (((String)DataBinder.Eval(Container.DataItem, "Descricao")).Substring(0,150) + " ...") : DataBinder.Eval(Container.DataItem, "Descricao")%>
                                                        </td>
                                                        <td align="center">
                                                            <a href="VHistorico.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                                <img src="Styles/Icones/find.png" alt="Visualizar" border="0" /></a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
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
