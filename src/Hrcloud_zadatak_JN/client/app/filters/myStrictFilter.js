
(function (angular) {
    'use strict';
    angular
        .module('app-module')
        .filter('myStrictFilter', MyStrictFilter);

    MyStrictFilter.$inject = ['$filter'];

    function MyStrictFilter($filter) {
        return function (input, predicate) {
            console.log("input,predicate", input, predicate);
            return $filter('filter')(input, { tagCollection: { tagName: predicate.tagName } });
        };
    }
})(window.angular);