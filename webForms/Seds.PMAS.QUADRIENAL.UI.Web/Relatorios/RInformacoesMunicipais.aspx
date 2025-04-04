﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RInformacoesMunicipais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RInformacoesMunicipais" %>

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
            <table id="tbReport" width="100%" runat="server" cellspacing="0" cellpadding="0" border="0" class="table striped border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30">Seq.
                        </th>
                        <th align="center"
                            width="100">C&#243;digo IBGE
                        </th>
                        <th align="center"
                            width="150">Munic&#237;pio
                        </th>
                        <th align="center"
                            width="150">DRADS
                        </th>
                        <th align="center"
                            width="100">Situa&#231;&#227;o do<br />
                            PMAS
                        </th>
                        <th align="center"
                            width="100">N&#237;vel de<br />
                            gest&#227;o
                        </th>
                        <th align="center"
                            width="100">Porte
                        </th>
                        <th align="center"
                            width="100">N&#250;mero de<br />
                            habitantes
                        </th>
                        <th width="140" align="center">N&deg;  de CRAS Implantados
                        </th>
                        <th width="140" align="center">N&deg;  de CREAS Implantados
                        </th>
                        <th width="140" align="center">N&deg;  de CENTROS POP Implantados
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px; background-color: #7cc8ff;">
                        <td align="right" colspan="7">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalHabitantes" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCRASImplantados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCREASImplantados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCentroPOPImplantados" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "IBGE") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Situacao") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "NivelGestao") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Habitantes")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CRASImplantados")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CREASImplantados")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CentroPOPImplantados")) %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
               <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "IBGE") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Situacao") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "NivelGestao") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Habitantes")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CRASImplantados")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CREASImplantados")) %>
                </td>
                <td align="right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CentroPOPImplantados")) %>
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
