
(function () {
    'use strict';

    angular.module('pmas', ['common.core', 'common.ui'])
    config(config)
    constant("prefeitoApiUrl", "http://localhost:63645/api/prefeito/");


    var config = function ($routeProvider, $locationProvider) {
        $routeProvider
        .when("/Home",
              {
                  templateUrl: "scripts/spa/home/Home.html",
                  controller: "homeController"
              }).when("/Prefeitura",
              {
                  templateUrl: "script/spa/prefeitura/edita.html",
                  controller: "prefeitoController"
              }).when("/Usuario", {
                  templateUrl: "script/spa/usuario/consultaCadUnico.html",
                  controller: "usuarioController"
              })
            .otherwise({ redirectTo: '/Home' });
        $locationProvider.html5Mode(true);
    };


    //app.constant("menuApiUrl", "http://localhost:63645/api/recurso/");
    //app.constant("usuarioApiUrl", "http://localhost:63645/api/usuario/");
    //app.run(function ($rootScope, AuthService) {
    //    $rootScope.$on('$routeChangeStart',
    //  function (evt, next, current) {
    //      // Verifica se o usuário esta logado
    //      if (!AuthService.userLoggedIn()) {
    //          if (next.templateUrl === "login.html") {
    //              // Caso não esteja, é redirecionado
    //          } else {
    //              $location.path('/login');
    //          }
    //      }
    //  });
    //});
}());
