<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Relatorios/Relatorio.Master" CodeBehind="RAuxilioReclusaoPensaoMorte.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAuxilioReclusaoPensaoMorte" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lstAuxilioReclusaoPensaoMorte" runat="server" OnItemDataBound="lstAuxilioReclusaoPensaoMorte_ItemDataBound">
       <LayoutTemplate>
        <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
           <thead>
               <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30">Seq.
                        </th>
                        <th align="center"
                            width="150">Município
                        </th>
                        <th align="center"
                            width="140">DRADS
                        </th>
                        <th align="center"
                            width="150">Porte
                        </th>
                        <th align="center"
                            width="150">Tipo de Rede
                        </th>
                        <th align="center"
                            width="150">Nome da Organização/Unidade
                        </th>
                        <th align="center"
                            width="150">Local de execução dos Serviços
                        </th>
                        <th align="center"
                            width="150">Proteção Social
                        </th>
                        <th align="center"
                            width="150">Tipo de Serviço
                        </th>
                        <th align="center"
                            width="150">Usuário
                        </th>
                        
                        <th align="center"
                            width="180">Crianças e adolescentes aptos para recebimento de Auxílio-Reclusão
                        </th>
                        <th align="center"
                            width="120">Quantidade de Auxílio-Reclusão requeridos
                        </th>
                        <th align="center"
                            width="120">Quantidade de Auxílio-Reclusão aprovados
                        </th>
                        <th align="center"
                            width="120">Quantidade de Auxílio-Reclusão negados
                        </th>


                        <th align="center"
                            width="180">Crianças e adolescentes aptos para recebimento de Pensão por Morte
                        </th>
                        <th align="center"
                            width="120">Quantidade de Pensão por Morte requeridos
                        </th>
                        <th align="center"
                            width="120">Quantidade de Pensão por Morte aprovados
                        </th>
                        <th align="center"
                            width="120">Quantidade de Pensão por Morte negados
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
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
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
                    <%#(DataBinder.Eval(Container.DataItem, "AtendeCriancasAuxilioReclusao") == null ? "Não Informado" : DataBinder.Eval(Container.DataItem, "AtendeCriancasAuxilioReclusao")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasAuxilioReclusaoFeitos") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasAuxilioReclusaoAprovados") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasAuxilioReclusaoNegado") %>
                </td>
                <td>
                    <%# (DataBinder.Eval(Container.DataItem, "AtendeCriancasPensaoMorte") == null ? "Não Informado": DataBinder.Eval(Container.DataItem, "AtendeCriancasPensaoMorte"))%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasPensaoMorteFeitos") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasPensaoMorteAprovados") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasPensaoMorteNegado") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
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
                    <%#(DataBinder.Eval(Container.DataItem, "AtendeCriancasAuxilioReclusao") == null ? "Não Informado" : DataBinder.Eval(Container.DataItem, "AtendeCriancasAuxilioReclusao")) %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasAuxilioReclusaoFeitos") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasAuxilioReclusaoAprovados") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasAuxilioReclusaoNegado") %>
                </td>
                <td>
                    <%#(DataBinder.Eval(Container.DataItem, "AtendeCriancasPensaoMorte") == null ? "Não Informado": DataBinder.Eval(Container.DataItem, "AtendeCriancasPensaoMorte"))%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasPensaoMorteFeitos") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasPensaoMorteAprovados") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "CriancasPensaoMorteNegado") %>
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