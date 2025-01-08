<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RInformacoesCadastraisPrefeituras.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RInformacoesCadastraisPrefeituras" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" DataKeyNames="Id" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" width="30" style="height: 22px;">Seq.
                        </th>
                        <th align="center" width="150">Município
                        </th>
                        <th align="center" width="150">DRADS
                        </th>
                        <th align="center" width="120">CNPJ
                        </th>
                        <th align="center" width="250">Prefeito
                        </th>
                        <th align="center" width="200">Endereço
                        </th>
                        <th align="center" width="100">Bairro
                        </th>
                        <th align="center" width="80">CEP
                        </th>
                        <th align="center" width="100">Telefone
                        </th>
                        <th align="center" width="100">Celular
                        </th>
                        <th align="center" width="200">Site
                        </th>
                        <th align="center" width="200">E-mail
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
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Prefeito") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Endereco") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "CEP") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Celular") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Site") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Email") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style=" background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Prefeito") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Endereco") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "CEP") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Celular") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Site") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Email") %>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as características
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
