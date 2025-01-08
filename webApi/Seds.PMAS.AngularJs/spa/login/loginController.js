(function () {
    'use strict';
    angular.module("pmas").controller('loginController', ['$scope', 'loginService', '$location', function ($scope, loginService, $location) {
        //FETCH DATA FROM SERVICES
        $scope.loginData = {
            userName: '',
            password: ''
        }
        $scope.message = "";

        $scope.login = function () {
            loginService.login($scope.loginData).then(function (data) {
                $location.path('/index');
                console.log('Index')
            }, function (error) {
                $location.path('/login');
                $scope.message = error;
            })
        }

        $scope.isLoggedIn = function () {
            return AuthService.isAuthenticated();
        }
    }])
})();

//(function () {
//    'use strict';
//    app.controller('loginController', ['$scope', 'LoginService', '$location', function ($scope, loginService, $location) {

//        $scope.loginData = {
//            userName: "",
//            password: ""
//        };

//        $scope.login = function () {
//            loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
//                if (response != null && response.error != undefined) {
//                    console.log(response);
//                    $scope.message = response.error_description;
//                }
//                else {
//                    $location.path('/home');
//                }
//            });

//            $scope.isLoggedIn = function () {
//                return AuthService.isAuthenticated();
//            }
//        }
//    }]);
//})();


//myApp.controller('loginController', ['$scope', 'accountService', '$location', function ($scope, accountService, $location) {
//    //FETCH DATA FROM SERVICES
//    $scope.account = {
//        username: '',
//        password: ''
//    }
//    $scope.message = "";
//    $scope.login = function () {
//        accountService.login($scope.account).then(function (data) {
//            $location.path('/home');
//        }, function (error) {
//            $scope.message = error.error_description;
//        })
//    }
//}])