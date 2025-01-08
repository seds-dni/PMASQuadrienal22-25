var app = angular.module('pmas-old', ['ngRoute', 'ui.bootstrap']);
app.config(function ($routeProvider, $locationProvider) {

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "scripts/spa/login/login.html"
    })
    .when("/home",
          {
              templateUrl: "scripts/spa/home/home.html",
              controller: "homeController"
          }).when("/prefeitura",
          {
              templateUrl: "scripts/spa/prefeitura/edita.html",
              controller: "prefeitoController",
          }).otherwise({ redirectTo: '/home' });
    $locationProvider.html5Mode(true);
})


    .config(['$httpProvider', function ($httpProvider) {
     //$httpProvider.interceptors.push(function ($q, $rootScope, $window, $location, userService) {
     var interceptor = function (userService, $q, $location) {
         return {
             request: function (config) {
                 var currentUser = userService.GetCurrentUser();
                 if (currentUser != null) {
                     config.headers['Authorization'] = 'Bearer ' + currentUser.access_token;
                 }
                 return config;
             },
             responseError: function (rejection) {
                 if (rejection.status === 401) {
                     $location.path('/login');
                     return $q.reject(rejection);
                 }
                 if (rejection.status === 403) {
                     $location.path('/unauthorized');
                     return $q.reject(rejection);
                 }
                 return $q.reject(rejection);
             }
         }
     }
     var params = ['userService', '$q', '$location'];
     interceptor.$inject = params;
     $httpProvider.interceptors.push(interceptor);
     //return {
     //    request: function (config) {
     //        var currentUser = userService.GetCurrentUser();
     //        if (currentUser != null) {
     //            config.headers['Authorization'] = 'Bearer ' + currentUser.access_token;
     //        }
     //        return config;
     //    },
     //    requestError: function (rejection) {

     //        return $q.reject(rejection);
     //    },
     //    response: function (response) {
     //        if (response.status == "401") {
     //            console.log('passei por aqui');
     //            $location.path('/login');
     //        }
     //        //the same response/modified/or a new one need to be returned.
     //        return response;
     //    },
     //    responseError: function (rejection) {
     //        if (rejection.status === 401) {
     //            console.log('passei aqui');
     //            $location.path('/login');
     //            return $q.reject(rejection);
     //        }
     //        if (rejection.status === 403) {
     //            $location.path('/unauthorized');
     //            return $q.reject(rejection);
     //        }
     //        return $q.reject(rejection);
     //    }
     //};

 }]).constant('serviceBase', 'http://localhost:60097');



//(function () {
//    'use strict';

//    angular.module('pmas', ['common.core', 'common.ui'])
//    .config(config);

//    config.$inject = ['$routeProvider', '$locationProvider'];
//    function config($routeProvider, $locationProvider) {
//        $routeProvider
//            .when("/home", {
//                templateUrl: "scripts/spa/home/home.html",
//                controller: "homeController",
//                text: "home"
//            })
//           .when("/prefeitura", {
//               templateUrl: "scripts/spa/prefeitura/edita.html",
//               controller: "prefeituraController"
//           })
//            .when("/usuarios", {
//                templateUrl: "scripts/spa/usuario/consultaCadUnico.html",
//                controller: "customersCtrl"
//            })
//            .when("/movies", {
//                templateUrl: "scripts/spa/movies/movies.html",
//                controller: "moviesCtrl"
//            })
//            .when("/movies/edit/:id", {
//                templateUrl: "scripts/spa/movies/edit.html",
//                controller: "movieEditCtrl"
//            })
//            .when("/rental", {
//                templateUrl: "scripts/spa/rental/rental.html",
//                controller: "rentStatsCtrl"
//            }).otherwise({ redirectTo: "/home" });
//            $locationProvider.html5Mode(true);
//    }
//}());




//var config = function ($routeProvider, $locationProvider) {
//    $routeProvider
//    .when("/Home",
//          {
//              templateUrl: "scripts/spa/home/Home.html",
//              controller: "homeController"
//          }).when("/Prefeitura",
//          {
//              templateUrl: "script/spa/prefeitura/edita.html",
//              controller: "prefeitoController"
//          }).when("/Usuario", {
//              templateUrl: "script/spa/usuario/consultaCadUnico.html",
//              controller: "usuarioController"
//          })
//        .otherwise({ redirectTo: '/Home' });
//    $locationProvider.html5Mode(true);
//};


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

