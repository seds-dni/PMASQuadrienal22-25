using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas.Relatorios;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoII
{
    public partial class MapaRedeProtecaoSocial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), getScript(), true);
        }

        String getScript()
        {
            var script = new StringBuilder();
            script.Append("var map = null;");
            script.AppendLine("var stylez = [ ");
			script.AppendLine("	{ featureType: \"all\", elementType: \"all\", stylers: [ { visibility: \"simplified\" }, { hue: \"#005eff\" } ] },");
			script.AppendLine("	{ featureType: \"administrative.country\", elementType: \"geometry\", stylers: [ { visibility: \"on\" }, { hue: \"#0022ff\" }, { saturation: -100 }, { lightness: -100 }, { gamma: 9.99 } ] },");
			script.AppendLine("	{ featureType: \"administrative.province\", elementType: \"geometry\", stylers: [ { visibility: \"on\" },{ hue: \"#0022ff\" }, { saturation: -100 }, { lightness: -100 }, { gamma: 9.99 }, { visibility: \"on\" } ] },");
			script.AppendLine("	{ featureType: \"administrative.locality\", elementType: \"all\", stylers: [ { visibility: \"on\" } ] },");
			script.AppendLine("	{ featureType: \"road.highway\", elementType: \"all\", stylers: [ { visibility: \"off\" } ] },");
			script.AppendLine("	{ featureType: \"road.arterial\", elementType: \"all\", stylers: [ { visibility: \"off\" } ] },");
			script.AppendLine("	{ featureType: \"road.local\", elementType: \"all\", stylers: [ { visibility: \"on\" } ] },");
			script.AppendLine("	{ featureType: \"transit.line\", elementType: \"all\", stylers: [ { visibility: \"on\" } ] } ];");
            
            script.AppendLine("var cidade = \""+ SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Nome.ToUpper() + "/SP\";");
            script.AppendLine("geocoder = new google.maps.Geocoder();");
            script.AppendLine("geocoder.geocode({ 'address': cidade }, function (results, status) {");
            script.AppendLine("if (status = google.maps.GeocoderStatus.OK) {");
            script.AppendLine("latlng = results[0].geometry.location;");
            script.AppendLine("var myOptions = {");
            script.AppendLine("zoom: 12,");
            script.AppendLine("center: latlng,");
            script.AppendLine("draggable: true,");				
			script.AppendLine("mapTypeControl: true,");
			script.AppendLine("mapTypeControlOptions: { mapTypeIds: ['hiphop',google.maps.MapTypeId.ROADMAP, google.maps.MapTypeId.SATELLITE ]}");
            script.AppendLine("};");                
            script.AppendLine("map = new google.maps.Map(document.getElementById(\"mapa\"), myOptions);");
            script.AppendLine("var styledMapOptions = {map: map,name: \"Ação Social\"}");
			script.AppendLine("var jayzMapType =  new google.maps.StyledMapType(stylez,styledMapOptions);");
			script.AppendLine("map.mapTypes.set('hiphop', jayzMapType);");
			script.AppendLine("map.setMapTypeId('hiphop');");
            script.AppendLine("}");

            var filtro = new RelatorioFiltroInfo();
            filtro.MunIDs = new List<int>() { SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio };
            var items = new List<InformacoesCadastraisLocalExecucaoInfo>();
            using (var proxy = new ProxyRelatorios())
            {
                items = proxy.Service.GetInformacoesCadastraisLocalExecucao(filtro).ToList();
            }

            foreach (var i in items)
            {
                script.AppendLine("var endereco = \"" + i.Endereco.Trim().ToUpper() + " ," + i.Cidade.Trim().ToUpper() + "/SP\";");
                script.AppendLine("geocoder = new google.maps.Geocoder();");
                script.AppendLine("geocoder.geocode({ 'address': endereco }, function (results, status) {");
                script.AppendLine("    if (status = google.maps.GeocoderStatus.OK) {");
                script.AppendLine("        latlng = results[0].geometry.location;");
                script.AppendLine("        markerInicio = new google.maps.Marker({ position: latlng, map: map, icon: '" + getIcon((ETipoUnidade)i.IdTipoUnidade) + "'  });");
                script.AppendLine("        var infowindow = new google.maps.InfoWindow({");
                script.AppendLine("        content: \"<strong>"+ i.TipoUnidade +"<br/>"+ i.UnidadeResponsavel +"</strong>\"");
                script.AppendLine("        });");

                script.AppendLine("        google.maps.event.addListener(markerInicio, 'click', function(event) {");
                script.AppendLine("             infowindow.open(map,markerInicio);");
                script.AppendLine("        });");
                script.AppendLine("    }");
                script.AppendLine("});");

            }

            script.AppendLine("});");

            return script.ToString();
        }

        String getIcon(ETipoUnidade e)
        {
            switch (e)
            {
                case ETipoUnidade.Privada:
                case ETipoUnidade.Publica: return "http://www.desenvolvimentosocial.sp.gov.br/mapa/img/icon_entidades.png";
                case ETipoUnidade.CRAS: return "http://www.desenvolvimentosocial.sp.gov.br/mapa/img/icon_cras.png";
                case ETipoUnidade.CREAS:
                case ETipoUnidade.CentroPOP: return "http://www.desenvolvimentosocial.sp.gov.br/mapa/img/icon_creas.png";
            }
            return "";
        }
    }
}