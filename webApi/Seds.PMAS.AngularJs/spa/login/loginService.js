(function () {
    'use strict';

    angular
        .module('pmas')
        .service('loginService', loginService);
    function loginService(userService, $http, $q, serviceBase, authData) {
        var vm = {};
        vm.login = function (user) {
            var obj = { 'userName': user.userName, 'password': user.password, 'grant_type': 'password' };
            Object.toparams = function ObjectsToParams(obj) {
                var p = [];
                for (var key in obj) {
                    p.push(key + '=' + encodeURIComponent(obj[key]));
                }
                return p.join('&');
            }

            var defer = $q.defer();
            $http({
                method: 'post',
                url: serviceBase + "/api/token",
                data: Object.toparams(obj),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                userService.SetCurrentUser(response.data);
                authData.authenticationData.IsAuthenticated = true;
                authData.authenticationData.userName = response.data.userName;
                defer.resolve(response.data);
            }, function (error) {
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                console.log(error);
                defer.reject(error.data);
            })
            return defer.promise;
        }
        vm.logout = function () {
            userService.CurrentUser = null;
            userService.SetCurrentUser(userService.CurrentUser);
        }
        return vm;


    };

})();
//(function () {
//    'use strict';
//    app.factory('loginService', ['$http', '$q', 'serviceBase', 'userService', 'authData', function ($http, $q, serviceBase, userService, authData) {
//        var fac = {};
//        fac.login = function (user) {
//            var obj = { 'userName': user.userName, 'password': user.password, 'grant_type': 'password' };
//            Object.toparams = function ObjectsToParams(obj) {
//                var p = [];
//                for (var key in obj) {
//                    p.push(key + '=' + encodeURIComponent(obj[key]));
//                }
//                return p.join('&');
//            }

//            var defer = $q.defer();
//            $http({
//                method: 'post',
//                url: serviceBase + "/token",
//                data: Object.toparams(obj),
//                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
//            }).then(function (response) {
//                userService.SetCurrentUser(response.data);
//                authData.authenticationData.IsAuthenticated = true;
//                authData.authenticationData.userName = response.userName;
//                defer.resolve(response.data);
//            }, function (error) {
//                authData.authenticationData.IsAuthenticated = false;
//                authData.authenticationData.userName = "";
//                defer.reject(error.data);
//            })
//            return defer.promise;
//        }
//        fac.logout = function () {
//            userService.CurrentUser = null;
//            userService.SetCurrentUser(userService.CurrentUser);
//        }
//        return fac;
//    }])


//    .service('loginService', ['$http', '$q', 'authenticationService', 'authData', 'serviceBase',
//function ($http, $q, authenticationService, authData, serviceBase) {
//    var userInfo;
//    var loginServiceURL = serviceBase + 'api/token';
//    var deviceInfo = [];
//    var deferred;

//    this.login = function (userName, password) {
//        deferred = $q.defer();
//        var data = "grant_type=password&username=" + userName + "&password=" + password;
//        $http.post(loginServiceURL, data, {
//            headers:
//               { 'Content-Type': 'application/x-www-form-urlencoded' }
//        }).success(function (response) {
//            var o = response;
//            userInfo = {
//                accessToken: response.access_token,
//                userName: response.userName
//            };
//            authenticationService.setTokenInfo(userInfo);
//            authData.authenticationData.IsAuthenticated = true;
//            authData.authenticationData.userName = response.userName;
//            deferred.resolve(null);
//        })
//        .error(function (err, status) {
//            authData.authenticationData.IsAuthenticated = false;
//            authData.authenticationData.userName = "";
//            deferred.resolve(err);
//        });
//        return deferred.promise;
//    }

//    this.logOut = function () {
//        authenticationService.removeToken();
//        authData.authenticationData.IsAuthenticated = false;
//        authData.authenticationData.userName = "";
//    }
//}
//]);
//})();