<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RInformacoesCadastraisLocalExecucao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RInformacoesCadastraisLocalExecucao" %>

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
                    <tr class="info" style="height: 22px; background-color: #7cc8ff;">
                        <th align="center"  width="30" style="height: 22px;">
                            Seq.
                        </th>
                        <th align="center"  width="150">
                            Munic&#237;pio
                        </th>
                        <th align="center"  width="150">
                            DRADS
                        </th>                        
                        <th align="center"  width="80">
                            C&#243;digo PMAS
                        </th>
                        <th align="center"  width="80">
                            Tipo de rede
                        </th>
                        <th align="center"  width="250">
                            Nome da organiza&#231;&#227;o/Unidade
                        </th>                        
                        <th align="center"  width="130">
                            CNPJ
                        </th>
                        <th align="center"  width="200">
                            Local de execu&#231;&#227;o
                        </th>   
                        <th align="center"  width="150">
                            T&#233;cnico respons&#225;vel/<br />coordenador
                        </th>                                             
                        <th align="center"  width="350">
                            Endere&#231;o
                        </th>
                        <th align="center"  width="100">
                           Bairro
                        </th>
                        <th align="center"  width="80">
                           CEP
                        </th>
                        <th align="center"  width="150">
                           Cidade
                        </th>
                        <th align="center"  width="100">
                           Telefone
                        </th>      
                        <th align="center"  width="100">
                           Celular
                        </th>                        
                        <th align="center"   width="200">
                           E-mail
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
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>                
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Coordenador") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Endereco") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "CEP") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Cidade") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Celular") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Email") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="height: 22px; background-color: #7cc8ff;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                 <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>                
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Coordenador") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Endereco") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "CEP") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Cidade") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Celular") %>
                </td>                
                <td align="left">
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