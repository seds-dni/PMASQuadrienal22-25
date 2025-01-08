<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RServicosRegionalizados.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RServicosRegionalizados" %>

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
                            width="150" rowspan="2">Drads
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
                        <th align="center" rowspan="2" style="width: 200px !important;">O município é Sede do Serviço?
                        </th>
                        <th align="center" rowspan="2" style="width: 200px !important;">Município Sede do Serviço
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Municipios que participam da oferta do servico
                        </th>
                        <th align="center"
                            width="50" rowspan="2">Forma juridica que regulamenta a oferta de servico
                        </th>
                        <th align="center" 
                            width="100" rowspan="2" >Nome Consórcio
                        </th>
                        <th align="center"
                            width="150" rowspan="2">CNPJ
                        </th>
                        <th align="center" 
                            width="100" rowspan="2">Municipio sede
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Municipios Envolvidos
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

                    </tr>

                </thead>


                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>


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
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipioSedeServico")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "SedeServico")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "IndicaMunicipiosParticipamOfertaServico")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "FormaJuridica")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NomeConsorcio")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CNPJ")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipioSede")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipiosEnvolvidos")) %>
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
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipioSedeServico")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "SedeServico")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "IndicaMunicipiosParticipamOfertaServico")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "FormaJuridica")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NomeConsorcio")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "CNPJ")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipioSede")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipiosEnvolvidos")) %>
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



