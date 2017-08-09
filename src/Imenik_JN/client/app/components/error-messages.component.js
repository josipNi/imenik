(function (angular) {
    'use strict';
    angular.module('app-module')
        .component('errorMessagesComponent', {
            template: `
            <div ng-if="$ctrl.errors && $ctrl.errors.length">
                <span ng-repeat="error in $ctrl.errors">{{error}}</span>                           
            </div>
            `,
            controller: ErrorMessagesComponent,
            bindings: {
                errors: '<'
            }
        });
    function ErrorMessagesComponent() {
        var $ctrl = this;
        console.log($ctrl.errors);
    }
})(window.angular);