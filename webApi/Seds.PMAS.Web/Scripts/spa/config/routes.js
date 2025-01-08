(function () {
    'use strict';
    angular.module('pmas')
    .config(function ($stateProvider, $compileProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('login');
        $stateProvider
         .state('home', {
             url: '/home',
             templateUrl: "scripts/spa/home/home.html",
             controller: "homeController"
         })
        .state('prefeitura', {
            url: '/prefeitura',
            templateUrl: "scripts/spa/prefeitura/edita.html",
            controller: "prefeituraController"
        })

    })

    .run(function ($rootScope, $state, $q, $window, $templateCache) {

        $rootScope.$on('$stateChangeError',
          function (event, toState, toParams, fromState, fromParams, error) {
              if (error === 'PERMISSOES_INSUFICIENTES') {
                  $state.go('error', {
                      code: '403',
                      message: 'Usuário sem permissao de acesso'
                  });
              }
          });

        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams, error) {
            $window.scrollTo(0, 0);

            if (!$rootScope.crumbs) {
                $rootScope.crumbs = JSON.parse(localStorage.getItem('crumbs'));
            }
        });

    });
}());