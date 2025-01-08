<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RInformacoesBeneficiosEventuais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RInformacoesBeneficiosEventuais" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="180" rowspan="2">Município
                        </th>
                        <th align="center" width="130" rowspan="2">DRADS
                        </th>
                        <th align="center" width="190" rowspan="2">Tipo de benefício
                        </th>
                        <th align="center" width="80" rowspan="2">Existe regulamentação<br />
                            municipal?
                        </th>

                        <th align="center" width="80" rowspan="2">Lei Municipal

                        </th>
                        <th align="center" width="80" rowspan="2">Número da <br />
                            Lei Municipal
                        </th>
                        <th align="ri" width="75" rowspan="2">Data de <br />
                            Publicação <br />
                            da Lei
                        </th>

                        <th align="center" width="80" rowspan="2">Resolução

                        </th>
                        <th align="center" width="80" rowspan="2">Número da <br />
                            Resolução
                        </th>
                        <th align="center" width="80" rowspan="2">Data de <br />
                            Publicação <br />
                            da Resolução
                        </th>

                        <th align="center" width="80" rowspan="2">Decreto/Portaria

                        </th>
                        <th align="center" width="80" rowspan="2">Número do <br />
                            Decreto/Portaria
                        </th>
                        <th align="center" width="80" rowspan="2">Data de <br />
                            Publicação <br />
                            da Decreto/Portaria
                        </th>


                        <th align="center" width="80" rowspan="2">Média anual<br />
                            de beneficiários
                        </th>
                        <th align="center" width="100" rowspan="2">Média anual de
                            <br />
                            benefícios concedidos
                        </th>
                        <th align="center" width="150" rowspan="2">Forma de auxílio
                        </th>
                        <th align="center" colspan="6" style="height: 22px;">Responsável pela execução
                        </th>
                        <th align="center" width="90" colspan="2" rowspan="2" style="height: 22px;">Integração com<br />
                            serviços
                        </th>
                        <th align="center" colspan="8" style="height: 22px;">Origem dos recursos financeiros
                        </th>
                        <th align="center" width="80" rowspan="2">Total de<br />
                            recursos
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" width="80">Órgão Gestor
                        </th>
                        <th align="center" width="60">CRAS
                        </th>
                        <th align="center" width="150">Unidade socioassistencial<br />
                            privada
                        </th>
                        <th align="center" width="50">CREAS
                        </th>
                        <th align="center" width="70">Centro POP
                        </th>
                        <th align="center" width="100">Fundo Social
                            <br />
                            de Solidariedade
                        </th>
                        <th align="center" width="80">FMAS (R$)
                        </th>
                        <th align="center" width="190">Fundo Social de<br />
                            Solidariedade (municipal) (R$)
                        </th>
                        <th align="center" width="100">Orçamento<br />
                            Municipal (R$)
                        </th>
                        <th align="center" width="80">FEAS (R$)
                        </th>
                        <th align="center" width="80" >Valor<br />Reprogramação FEAS (R$)
                        </th>
                        <th align="center" width="190">Fundo Social de<br />
                            Solidariedade (estadual) (R$)
                        </th>
                        <th align="center" width="80">FNAS (R$)
                        </th>
                        <th align="center" width="80" >Demandas <br /> Parlamentares (R$)
                        </th>

                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="4">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRegulamentado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" colspan="9">
                            
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMediaSemestralBeneficiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMediaSemestralBeneficios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOrgaoGestor" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCRAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalUnidadePrivada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCREAs" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCentroPOP" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFundoSocialSolidariedade" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIntegracaoServicos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFundoSocialSolidariedadeMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOrcamentoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblValorReprogramacaoAnoAnterior" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFundoSocialSolidariedadeEstadual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblValorDemandasParlamentares" runat="server" Font-Bold="true" />
                        </td>

                        <td align="right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
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
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Beneficio") %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "Regulamentado")) ? "Sim" : "Não" %>
                </td>


                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiLeiMunicipal") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NumeroLegislacao") %>&nbsp;
                </td>

                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "DataPublicacaoLei")%>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiResolucao") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NumeroResolucao") %>&nbsp;
                </td>

                <td class="align-center">
                    <%# DataBinder.Eval(Container.DataItem, "DataResolucao") %>&nbsp;
                </td>


                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiDecreto") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NumeroDecreto") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataDecreto") %>&nbsp;
                </td>


                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaSemestralBeneficiarios")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaSemestralBeneficiosConcedidos")) %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "FormaAuxilio") %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "OrgaoGestorResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CRASResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "UnidadePrivadaResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CREASResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CentroPOPResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "FundoSocialSolidariedadeResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IntegracaoServicos") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoMunicipalSolidariedade"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFEAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorReprogramacaoAnoAnterior")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoEstadualSolidariedade"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFNAS")) %>
                </td>

                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorDemandasParlamentares")) %>
                </td>

                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Total")) %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Beneficio") %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "Regulamentado")) ? "Sim" : "Não" %>
                </td>


                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiLeiMunicipal") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NumeroLegislacao") %>&nbsp;
                </td>

                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "DataPublicacaoLei")%>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiResolucao") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NumeroResolucao") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataResolucao") %>&nbsp;
                </td>


                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiDecreto") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NumeroDecreto") %>&nbsp;
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataDecreto") %>&nbsp;
                </td>



                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaSemestralBeneficiarios")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaSemestralBeneficiosConcedidos")) %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "FormaAuxilio") %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "OrgaoGestorResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CRASResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "UnidadePrivadaResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CREASResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "CentroPOPResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#((Boolean)DataBinder.Eval(Container.DataItem, "FundoSocialSolidariedadeResponsavel")) ? "Sim" : "Não" %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IntegracaoServicos") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFMAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoMunicipalSolidariedade"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFEAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorReprogramacaoAnoAnterior")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFundoEstadualSolidariedade"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFNAS")) %>
                </td>

                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorDemandasParlamentares")) %>
                </td>

                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Total")) %>
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
