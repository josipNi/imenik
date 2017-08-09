(function (angular) {
    'use strict';
    angular
        .module('app-module')
        .directive('multiSelect', MultiSelectDirective);

    function MultiSelectDirective() {     
        return {
            require: '^stTable',
            restrict:'AE',
            template: '<input type="checkbox" />',
            scope: {
                row: '=row',
                collection:'=collection'
            },
            link: function (scope, element, attr, ctrl) {
                // dodati bind samo na checkbox.
               
                element.bind('click', function (event) {
                    scope.$apply(function () {                        
                        ctrl.select(scope.row);
                    });
                });               

                scope.$watch('row.isSelected', function (newValue, oldValue) {
                    if (newValue === true) {                        
                        if (scope && scope.collection) {
                            console.log(element, element.parent());
                                element.parent().parent().addClass('st-selected');
                                scope.collection.push(scope.row);
                         }
                    }
                    else
                    {
                        if (scope.collection) {
                            var index = scope.collection.indexOf(scope.row);
                            if (index !== -1) {
                                scope.collection.splice(index, 1);
                                element.parent().parent().removeClass('st-selected');
                                if (element && element.length && element[0].firstChild)
                                    angular.element(element[0].firstChild).prop('checked', false);
                            }
                        }
                    }
                       
                });
            }
        };
    }

})(window.angular);