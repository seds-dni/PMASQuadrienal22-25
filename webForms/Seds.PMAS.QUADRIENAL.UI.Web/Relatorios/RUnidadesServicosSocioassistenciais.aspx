<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RUnidadesServicosSocioassistenciais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RUnidadesServicosSocioassistenciais" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" style="height: 22px;" width="30">Seq.
                        </th>
                        <th align="center"
                            width="150">Município
                        </th>
                        <th align="center"
                            width="130">DRADS
                        </th>
                        <th align="center"
                            width="80">Código PMAS
                        </th>
                        <th align="center"
                            width="100">Tipo de Rede
                        </th>
                        <th align="center"
                            width="110">CNPJ
                        </th>
                        <th align="center"
                            width="350">Nome
                            
                        </th>
                        <th align="center"
                            width="120">Área de Atuação
                        </th>
                        <th align="center"
                            width="150">Forma prioritária de atuação
                        </th>
                        <th align="center"
                            width="100">Serviços<br /> Socioassistenciais
                        </th>
                        <th align="center"
                            width="100">Benefícios<br /> Eventuais
                        </th>
                        <th align="center"
                            width="100">Programas ou Projetos
                        </th>
                        <th align="center"
                            width="100">N° de inscrição<br />
                            no CMAS
                        </th>
                        <th align="center"
                            width="120">Situação no Pró Social
                        </th>
                        <th align="center"
                            width="150">Situação da inscrição                          
                        </th>
                        <th align="center"
                            width="100">Situação Atual
                        </th>
                        <th align="center"
                            width="140">Quantidade de serviços ofertados
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #7cc8ff;">
                        <%-- <td align="right" colspan="6" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b></b>
                        </td>
                        <td align="right" ></td>
                        <td align="right" >&nbsp;
                        </td>
                        <td align="right" ></td>
                        <td align="right" ></td>
                        <td align="right" >&nbsp;
                        </td>--%>
                        <td align="right" colspan="15" ></td>
                        <td align="right" >Total  </td>
                        <td align="right" >
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "IdUnidade") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoRede") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>&nbsp;
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "RazaoSocial") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "AreaAtuacao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "FormaAtuacao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "ServicosSocioAssistenciais") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "BeneficiosEventuais") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "VivaleiteBomPrato") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "InscricaoCMAS") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoProSocial") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoInscricao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoAtualInscricao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TotalLocais") %>&nbsp;
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF !important;">
                <td  style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "IdUnidade") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoRede") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>&nbsp;
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "RazaoSocial") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "AreaAtuacao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "FormaAtuacao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "ServicosSocioAssistenciais") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "BeneficiosEventuais") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "VivaleiteBomPrato") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "InscricaoCMAS") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoProSocial") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoInscricao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoAtualInscricao") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TotalLocais") %>&nbsp;
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
