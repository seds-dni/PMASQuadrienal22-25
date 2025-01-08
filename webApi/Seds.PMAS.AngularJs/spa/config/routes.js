(function () {
    'use strict';
    angular.module('pmas')
        .config(function ($stateProvider, $compileProvider, $urlRouterProvider, $locationProvider) {
            $urlRouterProvider.otherwise('Index');

            $stateProvider
                //.state("index", {
                //    url: "/index",
                //    views: {
                //        "main": {
                //            controller: "indexController",
                //            templateUrl: "views/authenticated.html"
                //        }
                //    }
                //})
                .state("home", {
                    url: '/home',
                    views: {
                        'main': {
                            controller: "homeController",
                            templateUrl: "views/home.html"

                        }
                    }
                })
                .state("prefeitura", {
                    url: '/prefeitura',
                    views: {
                        'main': {
                            controller: "prefeituraController",
                            templateUrl: "views/Identificacao/prefeitura.html"
                        }
                    }
                })
                .state("orgaogestor", {
                    url: '/orgaogestor',
                    views: {
                        'main': {
                            controller: "prefeituraController",
                            templateUrl: "views/Identificacao/orgaogestor.html"
                        }
                    }
                })
                .state("gestormunicipal", {
                    url: '/gestormunicipal',
                    views: {
                        'main': {
                            controller: "prefeituraController",
                            templateUrl: "views/Identificacao/gestormunicipal.html"
                        }
                    }
                })
                .state("fundomunicipal", {
                    url: '/fundomunicipal',
                    views: {
                        'main': {
                            controller: "prefeituraController",
                            templateUrl: "views/Identificacao/fundomunicipal.html"
                        }
                    }
                })
                .state("conselhos", {
                    url: '/conselhos',
                    views: {
                        'main': {
                            controller: "prefeituraController",
                            templateUrl: "views/Identificacao/conselhomunicipal.html"
                        }
                    }
                })
                .state("login", {
                    url: '/login',
                    views: {
                        "main": {
                            controller: "loginController",
                            templateUrl: "views/login.html",
                        }
                    }
                })
            $locationProvider.html5Mode(true);
        })

        .run(function ($rootScope, $state, $location, authData) {
            $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState) {
                var shouldLogin = toState.data !== undefined
                    && toState.data.requireLogin
                    && !authData.IsAuthenticated;
                if (shouldLogin) {
                    $state.go('login');
                    event.preventDefault();
                    return;
                }
                if (authData.isLoggedIn) {
                    var shouldGoToMain = fromState.name === ""
                        && toState.name !== "index";

                    if (shouldGoToMain) {
                        console.log('passei por aqui');
                        $state.go('index');
                        event.preventDefault();
                    }
                    return;
                }
            })
            //$rootScope.$on('$stateChangeError',
            //  function (event, toState, toParams, fromState, fromParams, error) {
            //      if (error === 'PERMISSOES_INSUFICIENTES') {
            //          console.log('passei por aqui');
            //          $state.go('error', {
            //              code: '403',
            //              message: 'Usuário sem permissao de acesso'
            //          });
            //      }
            //  });

            //$rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams, error) {
            //    $window.scrollTo(0, 0);

            //    if (!$rootScope.crumbs) {
            //        $rootScope.crumbs = JSON.parse(localStorage.getItem('crumbs'));
            //    }
            //});
        });
})();