
(function () {

    var app = angular.module('pmas', ['ngRoute', 'ngMessages', 'ui.bootstrap']);

    var config = function ($routeProvider, $locationProvider) {
        $routeProvider
        .when("/Home",
              {
                  templateUrl: "Home.html",
                  controller: "homeController"
              }).when("/Prefeitura",
              {
                  templateUrl: "/View/Identificacao/edita.html",
                  controller: "prefeitoController"
              }).when("/Usuario",{
                  templateUrl: "/View/Usuario/consultaCadUnico.html",
                  controller: "usuarioController"
              })
            .otherwise({ redirectTo: '/Home' });
        $locationProvider.html5Mode(true);
    };

    app.config(config);
    app.constant("prefeitoApiUrl", "http://localhost:63645/api/prefeito/");
    app.constant("menuApiUrl", "http://localhost:63645/api/recurso/");
    app.constant("usuarioApiUrl", "http://localhost:63645/api/usuario/");
}());
