<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Relatorios/Relatorio.Master" CodeBehind="RStatusPrestacaoDeContas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RStatusPrestacaoDeContas" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lstStatusPrestacaoDeContas" runat="server" OnItemDataBound="lstStatusPrestacaoDeContas_ItemDataBound">
       <LayoutTemplate>
        <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
           <thead>
               <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30">Seq.
                        </th>
                        <th align="center"
                            width="140">DRADS
                        </th>
                        <th align="center"
                            width="150">Município
                        </th>
                        <th align="center"
                            width="180">2021
                        </th>
                        <th align="center"
                            width="120">2022
                        </th>
                        <th align="center"
                            width="120">2023
                        </th>
                        <th align="center"
                            width="120">2024
                        </th>
               </tr>
           </thead>
           <tbody>
              <tr id="itemPlaceholder" runat="server">
              </tr>
           </tbody>
           <tfoot>
               <tr class="info" style="background-color: #7cc8ff; height: 22px;">
               </tr>
           </tfoot>
        </table>
       </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2021") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2022") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2023") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2024") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2021") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2022") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2023") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Exercicio2024") %>
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