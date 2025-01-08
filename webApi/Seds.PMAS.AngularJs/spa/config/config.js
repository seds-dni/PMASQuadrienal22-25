(function () {
    'use strict';
    angular.module('pmas')
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
     }])
        .constant('serviceBase', 'http://localhost:49950');
}());






//myApp.config(['$httpProvider', function ($httpProvider) {
//    var interceptor = function (userService, $q, $location) {
//        return {
//            request: function (config) {
//                var currentUser = userService.GetCurrentUser();
//                if (currentUser != null) {
//                    config.headers['Authorization'] = 'Bearer ' + currentUser.access_token;
//                }
//                return config;
//            },
//            responseError: function (rejection) {
//                if (rejection.status === 401) {
//                    $location.path('/login');
//                    return $q.reject(rejection);
//                }
//                if (rejection.status === 403) {
//                    $location.path('/login');
//                    return $q.reject(rejection);
//                }
//                return $q.reject(rejection);
//            }

//        }
//    }
//    var params = ['userService', '$q', '$location'];
//    interceptor.$inject = params;
//    $httpProvider.interceptors.push(interceptor);
//}]);