(function (angular) {
    'use strict';
    angular
        .module('app-module')
        .filter('unique', UniqueFilter);

    function UniqueFilter() {
        return function (arr,collection, property) {
            var o = [], i,j, l = arr.length;

            for (i = 0; i < l; i += 1) {               
                for (j = 0; j < arr[i][collection].length; j++) {
                    var itemValue = arr[i][collection][j][property];
                    var index = o.indexOf(itemValue);
                    if (index === -1)
                        o.push(itemValue);
                }             
            }
            return o;
        };
    }
})(window.angular);