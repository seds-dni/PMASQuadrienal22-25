<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapaEstado.aspx.cs" Inherits="Seds.PMAS2013.UI.Web.BlocoII.MapaEstado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">    
    	#mapa{
	    	width:100%;
	    	height:500px;	
    	}
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
<div id="mapa" align="center"></div>    
</asp:Content>
