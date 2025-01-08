<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RRedesSocioassistenciais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RRedesSocioassistenciais" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound" OnPagePropertiesChanging="lst_PagePropertiesChanging">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead class="info">
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="183" colspan="2">Códigos
                        </th>
                        <th align="center"
                            width="180" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Porte
                        </th>
                        <th align="center"
                            width="180" rowspan="2" id="thDistrito" runat="server">Distrito
                        </th>
                        <th align="center"
                            width="150" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="70" rowspan="2">Tipo de rede
                        </th>
                        <th align="center"
                            width="350" rowspan="2">Nome da organização/unidade
                        </th>
                        <th align="center"
                            width="250" rowspan="2">Local de execução<br />
                            dos serviços
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Proteção social
                        </th>
                        <th align="center"
                            width="360" rowspan="2">Tipo de serviço
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Usuários
                        </th>
                        <th colspan="2">Funcionamento do serviço
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Abrangência
                        </th>
                        <th align="center" style="width: 200px !important;" colspan="4">Capacidade mensal de atendimento
                        </th>
                        <th align="center" colspan="4" style="width: 200px !important;">Média mensal de atendimento
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Sexo
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Região de moradia
                        </th>
                        <th align="center" rowspan="2" width="100">Total de trabalhadores do serviço
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Este serviço atende a alguma comunidade tradicional ou grupo específico?
                        </th>
                        <th align="center" colspan="12">Origem dos recursos de cofinanciamento via fundos
                            <br />
                            (valores anuais)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Recursos próprios da organização
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Valor do convênio<br />
                            estadualizado
                        </th>
                        <th align="center" width="80" rowspan="2">Recursos de outras fontes
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Total de recursos
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="90">Organização/<br />
                            Unidade
                        </th>
                        <th align="center"
                            width="93">Local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th style="width: 100px !important;">Início</th>
                        <th style="width: 100px !important;">Encerramento</th>
                        
                        <th style="width: 50px !important;">2022</th>
                        <th style="width: 50px !important;">2023</th>
                        <th style="width: 50px !important;">2024</th>
                        <th style="width: 50px !important;">2025</th>                        
                        <th style="width: 50px !important;">2021</th>
                        <th style="width: 50px !important;">2022</th>
                        <th style="width: 50px !important;">2023</th>
                        <th style="width: 50px !important;">2024</th>

                        <th align="center"
                            width="80">FMAS (R$)
                        </th>
                        <th align="center"
                            width="80">FMDCA (R$)
                        </th>
                        <th align="center"
                            width="80">FMI (R$)
                        </th>
                        <th align="center"
                            width="80">FEAS (R$)
                        </th>
                        <th align="center"
                            width="120">FEAS Reprogramado(R$)
                        </th>
                        <th align="center"
                            width="80">Demandas Parlamentares(R$)
                        </th>
                        <th align="center"
                            width="80">Demandas Parlamentares<br /> Reprogramado(R$)
                        </th>
                        <th align="center"
                            width="80">FEDCA (R$)
                        </th>
                        <th align="center"
                            width="80">FEI (R$)
                        </th>
                        <th align="center"
                            width="80">FNAS (R$)
                        </th>
                        <th align="center"
                            width="80">FNDCA (R$)
                        </th>
                        <th align="center"
                            width="80">FNI (R$)
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #7cc8ff;">
                        <td align="right" colspan="16">
                            <b>Totais:</b>
                        </td>
                       <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2021" runat="server" Font-Bold="true" />
                        </td>

                        <td align="right">
                            <asp:Label ID="lblMediaMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" colspan="2">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroTrabalhadoresServico" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEASReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalDemandasParlamentares" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalDemandasParlamentaresReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblValorFonteRecurso" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
           <asp:DataPager runat="server" ID="dpLista" PageSize="2000" PagedControlID="lst">
               <Fields>
                   <asp:NumericPagerField PreviousPageText="< Prev"
              NextPageText="Next >"
              ButtonCount="100"
              NextPreviousButtonCssClass="PrevNext"
              CurrentPageLabelCssClass="CurrentPage"
              NumericButtonCssClass="PageNumber"   />
               </Fields>
           </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td id="tdDistrito" runat="server">
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2024")) %>
                </td>

                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentares"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentaresReprogramacao"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td id="tdDistrito">
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2024")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentares"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentaresReprogramacao"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
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


    <asp:ListView ID="lstServicoPSC" runat="server" OnItemDataBound="lst_ItemDataBound" Visible="false">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead class="info">
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30" rowspan="3">Seq.
                        </th>
                        <th align="center"
                            width="183" colspan="2">Códigos
                        </th>
                        <th align="center"
                            width="180" rowspan="3">Município
                        </th>
                        <th align="center"
                            width="120" rowspan="3">Porte
                        </th>
                        <th align="center"
                            width="180" rowspan="3" id="thDistrito" runat="server">Distrito
                        </th>
                        <th align="center"
                            width="150" rowspan="3">DRADS
                        </th>
                        <th align="center"
                            width="70" rowspan="3">Tipo de rede
                        </th>
                        <th align="center"
                            width="250" rowspan="3">Nome da organização/unidade
                        </th>
                        <th align="center"
                            width="250" rowspan="3">Local de execução<br />
                            dos serviços
                        </th>
                        <th align="center"
                            width="120" rowspan="3">Proteção social
                        </th>
                        <th align="center"
                            width="360" rowspan="3">Tipo de serviço
                        </th>
                        <th align="center"
                            width="120" rowspan="3">Usuários
                        </th>
                        <th colspan="2">Funcionamento do serviço
                        </th>
                        <th align="center"
                            width="100" rowspan="3">Abrangência
                        </th>
                        <th align="center"
                            width="180" colspan="8">Capacidade mensal de atendimento
                        </th>
                        <th align="center"
                            width="180" colspan="8">Média mensal de atendimento
                        </th>
                        <th align="center"
                            width="100" rowspan="3">Sexo
                        </th>
                        <th align="center"
                            width="100" rowspan="3">Região de moradia
                        </th>
                        <th align="center" rowspan="3" width="100">Total de trabalhadores do serviço
                        </th>
                        <th align="center"
                            width="150" rowspan="3">Este serviço atende a alguma comunidade tradicional ou grupo específico?
                        </th>
                        <th align="center" colspan="12">Origem dos recursos de cofinanciamento via fundos
                            <br />
                            (valores anuais)
                        </th>
                        <th align="center"
                            width="80" rowspan="3">Recursos próprios da organização
                        </th>
                        <th align="center"
                            width="80" rowspan="3">Valor do convênio<br />
                            estadualizado
                        </th>
                        <th align="center" width="80" rowspan="3">Recursos de outras fontes
                        </th>
                        <th align="center"
                            width="100" rowspan="3">Total de recursos
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="90" rowspan="2">Organização/<br />
                            Unidade
                        </th>
                        <th align="center"
                            width="93" rowspan="2">Local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th rowspan="2">Início</th>
                        <th rowspan="2" width="200">Encerramento</th>
                        <th colspan="2">2022</th>
                        <th colspan="2">2023</th>
                        <th colspan="2">2024</th>
                        <th colspan="2">2025</th>
                        <th colspan="2">2021</th>
                        <th colspan="2">2022</th>
                        <th colspan="2">2023</th>
                        <th colspan="2">2024</th>
                        <th align="center"
                            width="80" rowspan="2">FMAS (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FMDCA (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FMI (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FEAS (R$)
                        </th>
                        <th align="center"
                            width="120" rowspan="2">FEAS Reprogramado(R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Demandas Parlamentares(R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Demandas Parlamentares<br /> Reprogramado(R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FEDCA (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FEI (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FNAS (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FNDCA (R$)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">FNI (R$)
                        </th>
                    </tr>
                    <tr class="info">
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #7cc8ff;">
                        <td align="right" colspan="16">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2021" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2021" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2017" runat="server" Font-Bold="true" />
                        </td>
                           <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2018" runat="server" Font-Bold="true" />
                        </td>
                          <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2019" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2020" runat="server" Font-Bold="true" />
                        </td>
                            <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" colspan="2">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroTrabalhadoresServico" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEASReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalDemandasParlamentares" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalDemandasParlamentaresReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblValorFonteRecurso" runat="server" Font-Bold="true" />
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
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td id="tdDistrito" runat="server">
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2022")) %>
                </td>                
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2024")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentares"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentaresReprogramacao"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
               <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td id="tdDistrito" runat="server">
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2022")) %>
                </td>                
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoLA2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoPSC2025")) %>
                </td>

                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2023")) %>
                </td>
                <td>
                   <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalLA2024")) %>
                </td>
                <td>
                   <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2024")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentares"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentaresReprogramacao"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
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

    <asp:ListView ID="lstExcel" runat="server" OnItemDataBound="lstExcel_ItemDataBound"  Visible="false">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReportExcel" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead class="info">
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="183" colspan="2">Códigos
                        </th>
                        <th align="center"
                            width="180" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Porte
                        </th>
                        <th align="center"
                            width="180" rowspan="2" id="thDistrito" runat="server">Distrito
                        </th>
                        <th align="center"
                            width="150" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="70" rowspan="2">Tipo de rede
                        </th>
                        <th align="center"
                            width="350" rowspan="2">Nome da organização/unidade
                        </th>
                        <th align="center"
                            width="250" rowspan="2">Local de execução<br />
                            dos serviços
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Proteção social
                        </th>
                        <th align="center"
                            width="360" rowspan="2">Tipo de serviço
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Usuários
                        </th>
                        <th colspan="2">Funcionamento do serviço
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Abrangência
                        </th>
                        <th align="center" style="width: 200px !important;" colspan="4">Capacidade mensal de atendimento
                        </th>
                        <th align="center" colspan="4" style="width: 200px !important;">Média mensal de atendimento
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Sexo
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Região de moradia
                        </th>
                        <th align="center" rowspan="2" width="100">Total de trabalhadores do serviço
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Este serviço atende a alguma comunidade tradicional ou grupo específico?
                        </th>
                        <th align="center" colspan="12">Origem dos recursos de cofinanciamento via fundos
                            <br />
                            (valores anuais)
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Recursos próprios da organização
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Valor do convênio<br />
                            estadualizado
                        </th>
                        <th align="center" width="80" rowspan="2">Recursos de outras fontes
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Total de recursos
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="90">Organização/<br />
                            Unidade
                        </th>
                        <th align="center"
                            width="93">Local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th style="width: 100px !important;">Início</th>
                        <th style="width: 100px !important;">Encerramento</th>
                        
                        <th style="width: 50px !important;">2022</th>
                        <th style="width: 50px !important;">2023</th>
                        <th style="width: 50px !important;">2024</th>
                        <th style="width: 50px !important;">2025</th>                        
                        <th style="width: 50px !important;">2021</th>
                        <th style="width: 50px !important;">2022</th>
                        <th style="width: 50px !important;">2023</th>
                        <th style="width: 50px !important;">2024</th>

                        <th align="center"
                            width="80">FMAS (R$)
                        </th>
                        <th align="center"
                            width="80">FMDCA (R$)
                        </th>
                        <th align="center"
                            width="80">FMI (R$)
                        </th>
                        <th align="center"
                            width="80">FEAS (R$)
                        </th>
                        <th align="center"
                            width="120">FEAS Reprogramado(R$)
                        </th>
                        <th align="center"
                            width="80">Demandas Parlamentares(R$)
                        </th>
                        <th align="center"
                            width="80">Demandas Parlamentares<br /> Reprogramado(R$)
                        </th>
                        <th align="center"
                            width="80">FEDCA (R$)
                        </th>
                        <th align="center"
                            width="80">FEI (R$)
                        </th>
                        <th align="center"
                            width="80">FNAS (R$)
                        </th>
                        <th align="center"
                            width="80">FNDCA (R$)
                        </th>
                        <th align="center"
                            width="80">FNI (R$)
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #7cc8ff;">
                        <td align="right" colspan="16">
                            <b>Totais:</b>
                        </td>
                       <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2021" runat="server" Font-Bold="true" />
                        </td>

                        <td align="right">
                            <asp:Label ID="lblMediaMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" colspan="2">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroTrabalhadoresServico" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEASReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalDemandasParlamentares" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalDemandasParlamentaresReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblValorFonteRecurso" runat="server" Font-Bold="true" />
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
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td id="tdDistrito" runat="server">
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2024")) %>
                </td>

                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentares"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentaresReprogramacao"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td id="tdDistrito">
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataFuncionamentoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DataDesativacao") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2024")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimentoTotal2025")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2021")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2022")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2023")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2024")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentares"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "DemandasParlamentaresReprogramacao"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFonteRecurso"))%>
                </td>
                <td>
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
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
