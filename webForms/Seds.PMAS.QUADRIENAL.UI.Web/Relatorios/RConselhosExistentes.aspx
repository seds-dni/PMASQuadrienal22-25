<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RConselhosExistentes.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RConselhosExistentes" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table
        {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;height: 22px;" >
                        <th align="center"
                            width="30">
                            Seq.
                        </th>
                        <th align="center"
                            width="150">
                            Município
                        </th>
                        <th align="center"
                            width="150">
                            DRADS
                        </th>
                        <th align="center"
                            width="100">
                            CMAS
                        </th>
                        <th align="center"
                            width="100">
                            CMDCA
                        </th>
                        <th align="center"
                            width="160" colspan="2">
                            Conselho Tutelar
                        </th>
                        <th align="center"
                            width="100">
                           Conselho do<br />Idoso
                        </th>
                        <th align="center"
                            width="130">
                            Conselho da Pessoa<br />com Deficiência
                        </th>
                        <th align="center"
                            width="160">
                            Conselho da Segurança <br />Alimentar e Nutricional
                        </th>
                        <th align="center"
                            width="100">
                            Conselho da <br />Juventude
                        </th>
                        <th align="center"
                            width="100">
                            Conselho de<br /> Políticas sobre Drogas 
                        </th>
                        <th align="center"
                            width="160" colspan="2">
                            Outros conselhos
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff;height: 22px;">
                        <td align="right" colspan="3" >
                            <b>Totais:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                              &nbsp;
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCT" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" >
                            <asp:Label ID="lblTotalCMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalPCD" runat="server" Font-Bold="true" />
                        </td>                     
                        <td align="right" >
                            <asp:Label ID="lblTotalCONSEA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCJ" runat="server" Font-Bold="true" />
                        </td>                    
                        <td align="right" >
                            <asp:Label ID="lblTotalCME" runat="server" Font-Bold="true" />
                        </td> 
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                              &nbsp;
                        </td>                   
                        <td align="right" >
                            <asp:Label ID="lblTotalOutros" runat="server" Font-Bold="true" />
                        </td>                    
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CMAS")) ? "Sim" : "Não há registro" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CMDCA")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "CT")) > 0 ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CT"))%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CMI")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "PCD")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CONSEA")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CJ")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CME")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "Outros")) > 0 ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Outros"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
               <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CMAS")) ? "Sim" : "Não há registro" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CMDCA")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "CT")) > 0 ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CT"))%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CMI")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "PCD")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CONSEA")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CJ")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CME")) ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "Outros")) > 0 ? "Sim" : "Não há registro"%>
                </td>
                <td class="align-rigth">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Outros"))%>
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
