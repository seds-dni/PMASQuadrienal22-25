<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RComunidadesGrupos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RComunidadesGrupos" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;" >
                        <th align="center"
                            width="30" rowspan="3">Seq.
                        </th>
                        <th align="center"
                            width="150" rowspan="3">Munic&#237;pio
                        </th>
                        <th align="center"
                            width="150" rowspan="3">DRADS
                        </th>
                        <th align="center"
                            width="150" rowspan="3">Porte do munic&#237;pio
                        </th>
                        <th align="center"
                            width="150" rowspan="3">N&#237;vel de gest&#227;o
                        </th>
                        <th align="center"
                            width="160" rowspan="3">N&deg; de<br />habitantes
                        </th>
                        <th align="center"
                            width="160" rowspan="3">Ano de Refer&#234;ncia
                        </th>
                        <th align="center" colspan="14">Presen&#231;a de povos e comunidades tradicionais
                        </th>
                        <th align="center" colspan="12">Presen&#231;a de grupos espec&#237;ficos
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            colspan="2">Ciganos
                        </th>
                        <th align="center"
                            colspan="2">Extrativistas
                        </th>
                        <th align="center"
                            colspan="2">Pescadores<br />
                            artesanais
                        </th>
                        <th align="center"
                            colspan="2">Comunidade <br />tradicional ou matriz<br />africana
                        </th>
                        <th align="center"
                            colspan="2">Comunidade<br /> ribeirinha
                        </th>
                        <th align="center"
                            colspan="2">Ind&#237;genas
                        </th>
                        <th align="center"
                            colspan="2">Quilombolas
                        </th>
                        <th align="center"
                            colspan="2">Agricultores familiares
                        </th>
                        <th align="center"
                            colspan="2">Acampamentos
                        </th>
                        <th align="center"
                            colspan="2">Popula&#231;&#227;o flutuante<br />decorrente de<br />instala&#231;&#227;o prisional
                        </th>
                        <th align="center"
                            colspan="2">Trabalhadores sazonais
                        </th>
                        <th align="center"
                            colspan="2">Aglomerados subnormais
                        </th>
                        <th align="center"
                            colspan="2">Assentamentos<br /> prec&#225;rios <br />e&#47;ou irregulares
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                        <th>Presen&#231;a</th>
                        <th>N&deg; de<br />
                            fam&#237;lias</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px;background-color: #7cc8ff;" class="info">
                        <td align="right" colspan="7">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPresencaCiganos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliasCiganos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPresencaExtrativistas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliasExtrativistas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPresencaPescadores" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaPescadores" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPresencaAfro" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaAfros" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaRibeirinha" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaRibeirinha" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaIndigena" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaIndigena" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaQuilombola" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaQuilombola" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaAgricultores" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaAgricultores" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaAcampamentos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFamiliaAcampamentos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaInstalacaoPrisional" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInstalacaoPrisional" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaTrabalhoSazonal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalTrabalhoSazonal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaAglomeradosSubnormais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalAglomeradosSubnormais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblPresencaAssentamentosPrecarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalAssentamentosPrecarios" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff;" >
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#(DataBinder.Eval(Container.DataItem, "Porte"))  %>
                </td>
                <td class="align-center">
                    <%#(DataBinder.Eval(Container.DataItem, "NivelGestao")) %>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "Populacao")) %>
                </td>
                <td class="align-center">2018
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteCiganos")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroCiganos")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteExtrativistas")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroExtrativistas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExistePescadores")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroPescadores")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAfros")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAfros")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteRibeirinha")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroRibeirinhas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteIndigena")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroIndigenas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteQuilombola")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroQuilombolas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAgricultor")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAgricultores")) %>
                </td>
                 <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAcampamento")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAcampamentos")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, " ExisteInstalacaoPrisional")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroInstalacoesPrisionais")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteTrabalhoSazonal ")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroTrabalhoSazonais")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAglomeradoSubnormal")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAglomeradoSubnormais")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAssentamentoPrecario ")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAssentamentosPrecarios")) %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff;" >
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#(DataBinder.Eval(Container.DataItem, "Porte"))  %>
                </td>
                <td class="align-center">
                    <%#(DataBinder.Eval(Container.DataItem, "NivelGestao")) %>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "Populacao")) %>
                </td>
                <td class="align-center">2018
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteCiganos")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroCiganos")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteExtrativistas")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroExtrativistas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExistePescadores")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroPescadores")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAfros")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAfros")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteRibeirinha")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroRibeirinhas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteIndigena")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroIndigenas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteQuilombola")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroQuilombolas")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAgricultor")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAgricultores")) %>
                </td>
                   <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAcampamento")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAcampamentos")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, " ExisteInstalacaoPrisional")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroInstalacoesPrisionais")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteTrabalhoSazonal ")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroTrabalhoSazonais")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAglomeradoSubnormal")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAglomeradoSubnormais")) %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "ExisteAssentamentoPrecario ")) ? "Sim" : "Não"%>
                </td>
                <td class="align-center">
                    <%#((Int32)DataBinder.Eval(Container.DataItem, "NumeroAssentamentosPrecarios")) %>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as caracter&#237;sticas
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
