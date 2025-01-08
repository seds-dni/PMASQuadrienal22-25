<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaUsuarios.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.ConsultaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form name="frmUsuarios">
        <div class="accordion">
            <div class="frame active">
                <div class="heading">
                    <b>Consultar Usuários</b>
                    <span class="mif-users icon"></span>
                </div>
                <div class="content">
                    <div class="formInput" data-text="Usuários do PMAS">
                        <div class="grid">
                            <div class="row cells3">
                                <div class="cell">
                                    Nome:
                                     <asp:TextBox ID="txtNome" Width="200px" runat="server"></asp:TextBox>
                                </div>

                                <div class="cell">
                                    RG:
                                    <div class="input-control text size-p80">
                                        <asp:TextBox ID="txtRg" runat="server"></asp:TextBox>
                                    </div>
                                    <b>Sem o dígito </b>
                                </div>
                                <div class="cell">
                                    Perfil:
                                    <div class="input-control select">
                                        <asp:DropDownList runat="server" ID="ddlPerfil" Width="150px">
                                            <asp:ListItem Text="Todos" Selected="True" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="CAS" Value="67"></asp:ListItem>
                                            <asp:ListItem Text="Administrador" Value="68"></asp:ListItem>
                                            <asp:ListItem Text="CMAS" Value="71"></asp:ListItem>
                                            <asp:ListItem Text="Consulta" Value="69"></asp:ListItem>
                                            <asp:ListItem Text="DRADS" Value="70"></asp:ListItem>
                                            <asp:ListItem Text="DRADS Administrador" Value="65"></asp:ListItem>
                                            <asp:ListItem Text="Orgão Gestor" Value="64"></asp:ListItem>
                                            <asp:ListItem Text="SEDS" Value="66"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row cells3">
                                <div class="cell">
                                    DRADS:
                                    <div class="input-control select">
                                        <asp:DropDownList runat="server" ID="ddlDrads" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlDrads_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="cell">
                                    Municípios:
                                    <div class="input-control select">
                                        <asp:DropDownList runat="server" ID="ddlMunicipio" Width="150px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="cell">
                                    Instituição:
                                  <div class="input-control select">
                                      <asp:TextBox ID="txtInstituicao" runat="server" Width="150px"></asp:TextBox>
                                  </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell text-center">
                                    <asp:Button ID="btnPesquisar" runat="server" SkinID="button-find"
                                        Text="Pesquisar " OnClick="btnPesquisar_Click" />
                                </div>
                            </div>
                            <div class="row flex-grid">
                                <asp:ListView ID="lstUsuarios" runat="server" OnItemDataBound="lstUsuarios_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="table striped border bordered" cellspacing="0" cellpadding="0" border="0" width="100%">
                                            <thead class="info">
                                                <tr>
                                                    <th colspan="10" style="height: 20px;">
                                                        <span class="mif-users"></span>Usuários
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th width="50" style="height: 22px;"></th>
                                                    <th width="200">Nome                                            
                                                    </th>
                                                    <th width="300">Instituição/Órgão                                            
                                                    </th>
                                                    <th width="100">Login                   
                                                    </th>
                                                    <th width="180">Município                   
                                                    </th>
                                                    <th width="200">DRADS
                                                    </th>
                                                    <th width="200">Perfil
                                                    </th>
                                                    <th width="130">RG
                                                    </th>
                                                    <th width="250">Senha
                                                    </th>
                                                    <th width="80">Situação
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--<tr class="jqgfirstrow" style="height: auto;">
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                    <td style="height: 0px;"></td>
                                                </tr>--%>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="info" style="height: 22px;">
                                                <asp:Label ID="lblSequencia" runat="server" /></td>
                                            <td align="left">
                                                <a href="Usuario/FUsuario.aspx?id=<%#Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem,"IdUsuario").ToString()))%>"><%#DataBinder.Eval(Container.DataItem, "Nome") %></a>
                                            </td>
                                            <td align="left"><%#DataBinder.Eval(Container.DataItem, "Instituicao") %>&nbsp</td>
                                            <td align="left"><%#DataBinder.Eval(Container.DataItem, "Login") %></td>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "Municipio") %>&nbsp
                                            </td>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "Drads") %>&nbsp
                                            </td>
                                            <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "Perfil") %></td>
                                            <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "RG") %>&nbsp</td>
                                            <td class="align-center"><a href="Usuario/AlterarSenhaUsuario.aspx?id=<%#Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "IdUsuario").ToString())) %>">Alterar Senha</a></td>
                                            <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "Situacao") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</asp:Content>
