(function (angular) {
    'use strict';
    angular
        .module('app-module', ['ngRoute', 'smart-table', 'ngMessages','ngTagsInput'])
        .config(config);

    config.$inject = ['$locationProvider', '$routeProvider'];

    function config($locationProvider, $routeProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider
            .when('/', {
                template: '<user-list-component></user-list-component>'
            })
            .when('/user/:id?', {
                template:
                '<user-details-component></user-details-component>'
            })
            .otherwise({
                template: '<h1>not found</h1>'
            });
    }

})(window.angular);
