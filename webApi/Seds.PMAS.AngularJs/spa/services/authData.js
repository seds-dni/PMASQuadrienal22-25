﻿(function () {
    'use strict';
    angular.module('pmas')
    .factory('authData', [function () {
        var authDataFactory = {};

        var _authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = _authentication;

        return authDataFactory;
    }])

}());