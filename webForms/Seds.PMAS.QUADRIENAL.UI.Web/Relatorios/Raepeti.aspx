<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Relatorios/Relatorio.Master" CodeBehind="Raepeti.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.Raepeti" %>

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
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" 
                            width="30" rowspan="2">
                            Seq.
                        </th>                        
                        <th align="center" 
                            width="180" rowspan="2">
                            Município
                        </th>
                        <th align="center" 
                            width="180" rowspan="2">
                            DRADS
                        </th>
                        <th align="center" 
                            width="180" rowspan="2">
                            Porte
                        </th>
                        <th align="center" 
                            width="180" rowspan="2">
                            Aderiu ao termo de<br /> aceite federal
                        </th>

                        <th align="center" 
                            width="100" rowspan="2">
                            Data de Assinatura do<br /> termo
                        </th>            
                        
                        <th align="center"   style="height:22px;" rowspan="2">
                            Valor Mensal do<br /> cofinanciamento
                        </th>      
                        <th align="center"  width="80" rowspan="2" >
                            Técnico Responsável
                        </th>                                                                        
                        <th align="center" width="100" rowspan="2">
                            Telefone
                        </th>
                        <th align="center" width="150" rowspan="2">
                            E-mail
                        </th>                
                        <th align="center" width="150" colspan="4">
                            Idade10a13Anos
                        </th>  
                        <th align="center" width="150" colspan="4">
                            Idade14a15Anos
                        </th>  
                        <th align="center" width="150" colspan="4">
                            Idade16a17Anos
                        </th>  
                        <th align="center" width="150" colspan="4">
                            MetaMunicipal
                        </th>
                        <th align="center" width="150" colspan="4">
                            
                        </th>                                                  
                    </tr>
                     <tr class="info" style="background-color: #7cc8ff;">
                         

                        <th align="center" width="120">
                            2021
                        </th>                        
                        <th align="center" 
                            width="120">
                            2022
                        </th>                        
                        <th align="center" width="120">
                            2023
                        </th>                        
                        <th align="center" width="120">
                            2024
                        </th>

                        <th align="center" width="120">
                            2021
                        </th>                        
                        <th align="center" 
                            width="120">
                            2022
                        </th>                        
                        <th align="center" width="120">
                            2023
                        </th>                        
                        <th align="center" width="120">
                            2024
                        </th>

                        <th align="center" width="120">
                            2021
                        </th>                        
                        <th align="center" 
                            width="120">
                            2022
                        </th>                        
                        <th align="center" width="120">
                            2023
                        </th>                        
                        <th align="center" width="120">
                            2024
                        </th>

                        <th align="center" width="120">
                            2021
                        </th>                        
                        <th align="center" 
                            width="120">
                            2022
                        </th>                        
                        <th align="center" width="120">
                            2023
                        </th>                        
                        <th align="center" width="120">
                            2024
                        </th>

                       <th align="center" width="120">
                            PetiAcoesTrabalhoInfantil
                        </th>                        
                        <th align="center" 
                            width="120">
                            Eixo
                        </th>                        
                        <th align="center" width="120">
                            Acao
                        </th>                        
                        <th align="center" width="120">
                            PeriodoRealizacao
                        </th>


                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px;" class="info">
                        <td align="right" colspan="6">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValorMensalDoCofinanciamento" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" colspan="3">
                            
                        </td>
                        
                        <td align="right">
                            <asp:Label ID="lblTotalIdade10a13Ano2021" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade10a13Ano2022" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade10a13Ano2023" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade10a13Ano2024" runat="server" Font-Bold="true" />
                        </td>
                        
                        <td align="right">
                            <asp:Label ID="lblTotalIdade14a15Ano2021" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade14a15Ano2022" runat="server" Font-Bold="true" />
                        </td>            
                        <td align="right">
                            <asp:Label ID="lblTotalIdade14a15Ano2023" runat="server" Font-Bold="true" />
                        </td>                          
                        <td align="right">
                            <asp:Label ID="lblTotalIdade14a15Ano2024" runat="server" Font-Bold="true" />
                        </td>

                        <td align="right">
                            <asp:Label ID="lblTotalIdade16a17Ano2021" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade16a17Ano2022" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade16a17Ano2023" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalIdade16a17Ano2024" runat="server" Font-Bold="true" />
                        </td>

                        <td align="right">
                            <asp:Label ID="lblTotalMetaMunicipal2021" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMetaMunicipal2022" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMetaMunicipal2023" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMetaMunicipal2024" runat="server" Font-Bold="true" />
                        </td>
                        
                        <td align="right" colspan="4">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "Porte")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PetiAderiuCofinanciamentoFederal")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PetiDataAdesao","{0: dd/MM/yyyy}")%>
                </td>
                <td class="align-center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAepeti"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NomeGestorAcao")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Email")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2024")%>
                </td>



                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2024")%>
                </td>



                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2024")%>
                </td>


                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2024")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PetiAcoesTrabalhoInfantil")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Eixo")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Acao")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PeriodoRealizacao")%>
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
                    <%#DataBinder.Eval(Container.DataItem, "Porte")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PetiAderiuCofinanciamentoFederal")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PetiDataAdesao","{0: dd/MM/yyyy}")%>
                </td>
                <td class="align-center">
                   <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorAepeti"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "NomeGestorAcao")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Email")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade10a13Ano2024")%>
                </td>



                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade14a15Ano2024")%>
                </td>



                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Idade16a17Ano2024")%>
                </td>


                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2021")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2022")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2023")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MetaMunicipal2024")%>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PetiAcoesTrabalhoInfantil")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Eixo")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Acao")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PeriodoRealizacao")%>
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