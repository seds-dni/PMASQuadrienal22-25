<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ConsultaPMAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.ConsultaPMAS" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel runat="server" ID="pnlCadastro">
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
                                    <div class="row cells3">
                                        <div class="cell" align="center">
                                            DRADS:                                  
                                            <div class="input-control select mid-size">
                                                <asp:DropDownList runat="server" ID="ddlDrads" AutoPostBack="true" OnSelectedIndexChanged="ddlDrads_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            Município:                                   
                                            <div class="input-control select mid-size">
                                                <asp:DropDownList runat="server" ID="ddlMunicipio">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstPMAS" runat="server" OnItemDataBound="lstPMAS_ItemDataBound"
                                                OnItemCommand="lstPMAS_ItemCommand" DataKeyNames="IdPrefeitura,IdMunicipio,Situacao">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="4" style="height: 20px;">
                                                                    <span class="ui-jqgrid-title">Planos Municipais</span>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th  width="20" style="height: 22px;"></th>
                                                                <th  width="100">Visualizar Plano
                                                                </th>
                                                                <th  width="250">Município
                                                                </th>
                                                                <th width="200">Situação PMAS
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                           <%-- <tr class="jqgfirstrow" style="height: auto;">
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                            </tr>--%>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisMunicipio" ToolTip="Visualizar Plano"
                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar_Municipio" />
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Situacao.Nome") %>
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
