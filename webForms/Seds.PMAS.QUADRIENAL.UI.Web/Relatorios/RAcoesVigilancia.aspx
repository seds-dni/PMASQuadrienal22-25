<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RAcoesVigilancia.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAcoesVigilancia" %>

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
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" 
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center" 
                            width="120" rowspan="2">Município
                        </th>
                        <th align="center" 
                            width="120" rowspan="2">DRADS
                        </th>
                        <th align="center" 
                            width="100" rowspan="2">Realiza ações de
                            <br />
                            vigilância socioassistencial?
                        </th>
                        <th align="center" 
                            width="100" rowspan="2">Vigilância de riscos
                            <br />
                            e vulnerabilidades
                        </th>
                        <th align="center" 
                            width="180" rowspan="2">Vigilância de
                            <br />
                            padrões de serviços
                        </th>
                        <th align="center"  rowspan="2" colspan="2" width="300">Possui equipe específica<br />
                            de vigilância?
                        </th>
                        <th align="center" 
                            width="100" rowspan="2">Possui sistema
                            <br />
                            informatizado próprio?
                        </th>
                        <th align="center"  colspan="19">Base de dados utilizadas para vigilância socioassistencial
                        </th>
                    </tr>
                    <tr class="info" style="font-size: 10px;background-color: #7cc8ff;">
                        <th align="center" 
                            width="80">CadÚnico
                        </th>
                        <th align="center" 
                            width="80">PMASWeb
                        </th>
                       <%-- <th align="center" 
                            width="80">SisPETI
                        </th>
                        <th align="center" 
                            width="80">SisJovem
                        </th>--%>
                        <th align="center" 
                            width="80">Pró-Social
                        </th>
                        <th align="center" 
                            width="80">Instrumentais próprios<br />
                            não informatizados
                        </th>
                        <th align="center" 
                            width="80">Sistema Informatizado<br />
                            Municipal
                        </th>
                        <th align="center" 
                            width="80">Outros Aplicativos<br />
                            da Rede SUAS
                        </th>
                        <th align="center" 
                            width="80">Dados de outros órgãos<br />
                            públicos municipais
                        </th>
                        <th align="center" 
                            width="80">Fundação SEADE
                        </th>
                        <th align="center" 
                            width="80">Aplicativos da<br />
                            SAGI / MDS
                        </th>
                        <th align="center" 
                            width="80">Aplicativos do programa
                            <br />
                            Bolsa Família
                        </th>
                        <th align="center" 
                            width="80">IBGE
                        </th>
                        <th align="center" 
                            width="80">SISC
                        </th>
                        <th align="center" 
                            width="80">Censo SUAS
                        </th>
                        <th align="center" 
                            width="80">CNEAS
                        </th>
                        <th align="center" 
                            width="80">Cad SUAS
                        </th>
                        <th align="center" 
                            width="80">RMA
                        </th>

                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px;">
                        <td align="right" colspan="3" >
                            <b>Totais:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOfereceVigilancia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalVigilanciaRiscos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalVigilanciaPadroesServicos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalPossuiEquipeVigilanciaSocioassistencial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalEquipeVigilanciaSocioassistencial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalPossuiSistemaInformaizadoProprio" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCadUnico" runat="server" Font-Bold="true" />
                        </td>
                   <%--     <td align="right" >
                            <asp:Label ID="lblTotalCensoSUAS" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalPMASWeb" runat="server" Font-Bold="true" />
                        </td>
                      <%--  <td align="right" >
                            <asp:Label ID="lblTotalSisPETI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalSisJovem" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" >
                            <asp:Label ID="lblTotalProSocial" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalInstrumentaisProprios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalSistemaInformatizadoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                          <td align="right" >
                            <asp:Label ID="lblTotalOutrosAplicativosSUAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOutrosOrgaosMunicipais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalSEADE" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalAplicativosSAGIMDS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalAplicativosBolsaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalIBGE" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalSisc" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCensoSuas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalCneas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblCadSuas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblRMA" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OfereceVigilancia")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "VigilanciaRiscos")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "VigilanciaPadroesServicos")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiEquipeVigilanciaSocioassistencial")%>
                </td>
                <td class="align-rigth">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "EquipeVigilanciaSocioassistencial"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiSistemaInformaizadoProprio")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CadUnico")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PMASWeb")%>
                </td>
                <%--<td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SisPETI")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SisJovem")%>
                </td>--%>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "ProSocial")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "InstrumentaisProprios")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SistemaInformatizadoMunicipal")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OutrosAplicativosSUAS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OutrosOrgaosMunicipais")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SEADE")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AplicativosSAGIMDS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AplicativosBolsaFamilia")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IBGE")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SISC")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CensoSUAS")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CNEAS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CadSUAS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "RMAS")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OfereceVigilancia")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "VigilanciaRiscos")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "VigilanciaPadroesServicos")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiEquipeVigilanciaSocioassistencial")%>
                </td>
                <td class="align-rigth">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "EquipeVigilanciaSocioassistencial"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiSistemaInformaizadoProprio")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CadUnico")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PMASWeb")%>
                </td>
            <%--    <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SisPETI")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SisJovem")%>
                </td>--%>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "ProSocial")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "InstrumentaisProprios")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SistemaInformatizadoMunicipal")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OutrosAplicativosSUAS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OutrosOrgaosMunicipais")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SEADE")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AplicativosSAGIMDS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AplicativosBolsaFamilia")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IBGE")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "SISC")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CensoSUAS")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CNEAS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CadSUAS")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "RMAS")%>
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
