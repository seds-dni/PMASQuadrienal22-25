<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mapa.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Mapa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    
    	#mapa{
	    	width:100%;
	    	height:500px;	
    	}
    
    </style>
    
    

	<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
    <script type="text/javascript">
        var map = null;
        function carregar() {
            var latlng = new google.maps.LatLng(-29.767954, -57.071657);

            var myOptions = {
                zoom: 12,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            //criando o mapa
            map = new google.maps.Map(document.getElementById("mapa"), myOptions);


            var lineCoordinates = [                                 
                new google.maps.LatLng(-45.275155, - 2.697053),
                new google.maps.LatLng(- 45.242714, - 2.487958),
                new google.maps.LatLng(- 45.248425, - 2.486634),
                new google.maps.LatLng(- 45.257911, - 2.475008),
                new google.maps.LatLng(- 45.258827, - 2.464663),
                new google.maps.LatLng(- 45.274669, - 2.441199),
                new google.maps.LatLng(- 45.287216, - 2.434346),
                new google.maps.LatLng(- 45.297249, - 2.428363),
                new google.maps.LatLng(-45.297360, -2.422554),
                new google.maps.LatLng(-45.301467, -2.418109),
                new google.maps.LatLng(-45.300364, -2.411030),
                new google.maps.LatLng(-45.160464, -2.347349),
                new google.maps.LatLng(-45.026843, -2.208062),
                new google.maps.LatLng(-44.967189, -2.170789),
                new google.maps.LatLng(-44.966054, -2.236968),
                new google.maps.LatLng(-44.965874, -2.251151),
                new google.maps.LatLng(-44.965714, -2.263725),
                new google.maps.LatLng(-44.963948, -2.351647),
                new google.maps.LatLng(-44.957437, -2.357673),
                new google.maps.LatLng(-44.951322, -2.355987),
                new google.maps.LatLng(-44.938645, -2.361122),
                new google.maps.LatLng(-44.898339, -2.384788),
                new google.maps.LatLng(-44.898747, -2.386026),
                new google.maps.LatLng(-44.916501, -2.391231),
                new google.maps.LatLng(-44.927945, -2.403445),
                new google.maps.LatLng(-44.925442, -2.408225),
                new google.maps.LatLng(-44.927220, -2.412480),
                new google.maps.LatLng(-44.941631, -2.416370),
                new google.maps.LatLng(-44.948131, -2.424425),
                new google.maps.LatLng(-44.955333, -2.427125),
                new google.maps.LatLng(-44.959171, -2.435770),
                new google.maps.LatLng(-44.959194, -2.459482),
                new google.maps.LatLng(-44.964696, -2.454963),
                new google.maps.LatLng(-44.969106, -2.459954),
                new google.maps.LatLng(-44.977561, -2.460198),
                new google.maps.LatLng(-44.981609, -2.466163),
                new google.maps.LatLng(-44.983601, -2.461294),
                new google.maps.LatLng(-44.990368, -2.462147),
                new google.maps.LatLng(-44.991387, -2.465684),
                new google.maps.LatLng(-44.987346, -2.470548),
                new google.maps.LatLng(-44.993736, -2.469490),
                new google.maps.LatLng(-44.994053, -2.478217),
                new google.maps.LatLng(-45.000396, -2.475172),
                new google.maps.LatLng(-45.002839, -2.464936),
                new google.maps.LatLng(-45.008134, -2.465714),
                new google.maps.LatLng(-45.011028, -2.471277),
                new google.maps.LatLng(-45.016163, -2.469207),
                new google.maps.LatLng(-45.018700, -2.472008),
                new google.maps.LatLng(-45.016835, -2.485836),
                new google.maps.LatLng(-45.022086, -2.486050),
                new google.maps.LatLng(-45.022494, -2.494478),
                new google.maps.LatLng(-45.026054, -2.489266),
                new google.maps.LatLng(-45.025077, -2.483819),
                new google.maps.LatLng(-45.035611, -2.491260),
                new google.maps.LatLng(-45.040137, -2.490629),
                new google.maps.LatLng(-45.033974, -2.476660),
                new google.maps.LatLng(-45.037389, -2.475701),
                new google.maps.LatLng(-45.037418, -2.469581),
                new google.maps.LatLng(-45.046136, -2.487741),
                new google.maps.LatLng(-45.050972, -2.486611),
                new google.maps.LatLng(-45.049327, -2.491769),
                new google.maps.LatLng(-45.039681, -2.509534),
                new google.maps.LatLng(-45.045972, -2.511987),
                new google.maps.LatLng(-45.040691, -2.514884),
                new google.maps.LatLng(-45.041810, -2.528775),
                new google.maps.LatLng(-45.027748, -2.533680),
                new google.maps.LatLng(-45.031812, -2.539250),
                new google.maps.LatLng(-45.029553, -2.545362),
                new google.maps.LatLng(-45.035718, -2.540884),
                new google.maps.LatLng(-45.043045, -2.542926),
                new google.maps.LatLng(-45.048591, -2.545034),
                new google.maps.LatLng(-45.048290, -2.552366),
                new google.maps.LatLng(-45.051296, -2.554949),
                new google.maps.LatLng(-45.047870, -2.557585),
                new google.maps.LatLng(-45.050450, -2.562433),
                new google.maps.LatLng(-45.047877, -2.568024),
                new google.maps.LatLng(-45.054133, -2.569047),
                new google.maps.LatLng(-45.058167, -2.558310),
                new google.maps.LatLng(-45.067849, -2.557378),
                new google.maps.LatLng(-45.072788, -2.561280),
                new google.maps.LatLng(-45.076184, -2.610090),
                new google.maps.LatLng(-45.078992, -2.614386),
                new google.maps.LatLng(-45.085991, -2.615232),
                new google.maps.LatLng(-45.088874, -2.629613),
                new google.maps.LatLng(-45.092860, -2.628863),
                new google.maps.LatLng(-45.094399, -2.634105),
                new google.maps.LatLng(-45.094109, -2.642889),
                new google.maps.LatLng(-45.086841, -2.651481),
                new google.maps.LatLng(-45.093537, -2.657519),
                new google.maps.LatLng(-45.105446, -2.657140),
                new google.maps.LatLng(-45.112327, -2.661045),
                new google.maps.LatLng(-45.107303, -2.669254),
                new google.maps.LatLng(-45.106029, -2.679984),
                new google.maps.LatLng(-45.108842, -2.691893),
                new google.maps.LatLng(-45.119693, -2.701654),
                new google.maps.LatLng(-45.120857, -2.710048),
                new google.maps.LatLng(-45.128513, -2.717661),
                new google.maps.LatLng(-45.129096, -2.745193),
                new google.maps.LatLng(-45.146058, -2.753778),
                new google.maps.LatLng(-45.146320, -2.753995),
                new google.maps.LatLng(-45.158402, -2.736666),
                new google.maps.LatLng(-45.178378, -2.732828),
                new google.maps.LatLng(-45.182193, -2.726795),
                new google.maps.LatLng(-45.197571, -2.733460),
                new google.maps.LatLng(-45.204948, -2.723240),
                new google.maps.LatLng(-45.225932, -2.723010),
                new google.maps.LatLng(-45.239566, -2.708544),
                new google.maps.LatLng(-45.243824, -2.708580),
                new google.maps.LatLng(-45.254349, -2.700619),
                new google.maps.LatLng(-45.272445, -2.700079),
                new google.maps.LatLng(-45.275155, -2.697053)
            ]; 

            var lineSymbol = {
                path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW
            };

            var line = new google.maps.Polyline({
                path: lineCoordinates,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
                map: map
            });
            //marcar();

        }



        function marcar() {
            var endereco = "Marilia, Sao Paulo";
            //alert(endereco)
            geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': endereco }, function (results, status) {
                if (status = google.maps.GeocoderStatus.OK) {
                    latlng = results[0].geometry.location;
                    //markerInicio = new google.maps.Marker({ position: latlng, map: map });
                    var populationOptions = {
                        strokeColor: "#FF0000",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: "#FF0000",
                        fillOpacity: 0.35,
                        map: map,
                        center: latlng,
                        radius: 10000
                    };
                    cityCircle = new google.maps.Circle(populationOptions)
                    map.setCenter(latlng);
                }
            });

            endereco = "Bauru, Sao Paulo";
            //alert(endereco)
            geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': endereco }, function (results, status) {
                if (status = google.maps.GeocoderStatus.OK) {
                    latlng = results[0].geometry.location;
                    //markerInicio = new google.maps.Marker({ position: latlng, map: map });
                    var populationOptions = {
                        strokeColor: "#003366",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: "#003366",
                        fillOpacity: 0.35,
                        map: map,
                        center: latlng,
                        radius: 10000
                    };
                    cityCircle = new google.maps.Circle(populationOptions)
                    map.setCenter(latlng);
                }
            });

            endereco = "Jau, Sao Paulo";
            //alert(endereco)
            geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': endereco }, function (results, status) {
                if (status = google.maps.GeocoderStatus.OK) {
                    latlng = results[0].geometry.location;
                    //markerInicio = new google.maps.Marker({ position: latlng, map: map });
                    var populationOptions = {
                        strokeColor: "#009900",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: "#009900",
                        fillOpacity: 0.35,
                        map: map,
                        center: latlng,
                        radius: 10000
                    };
                    cityCircle = new google.maps.Circle(populationOptions)
                    map.setCenter(latlng);
                }
            });            
        }
    </script>
</head>
<body onload="carregar()">
    <form id="form1" runat="server">   
        <div id="mapa"></div> 		
    </form>
</body>
</html>
