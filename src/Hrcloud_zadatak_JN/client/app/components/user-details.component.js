(function (angular) {
    'use strict';
    angular
        .module('app-module')
        .component('userDetailsComponent', {
            templateUrl: '/templates/user-details.component.html',
            controller: UserDetailsComponent
        });

    UserDetailsComponent.$inject = ['dataService', '$routeParams', '$location'];

    function UserDetailsComponent(dataService, $routeParams, $location) {
        var $ctrl = this;

        $ctrl.onSubmit = onSubmit;
        $ctrl.addBookmark = addBookmark;

        $ctrl.$onDestroy = function () {
            dataService.dispose();
        };

        $ctrl.$onInit = function () {
            var isControllerSet = dataService.apiController('user');
            if ($routeParams && $routeParams.id && isControllerSet)
                activate();
            else
                $ctrl.user = {};
        };

        function onSubmit() {
            console.log($ctrl.user);
            dataService.save($ctrl.user).then(function (data) {               
                console.log(data);
                if (data.status === 200 && data.statusText === "OK")
                    $location.path('/');
                else 
                    $ctrl.errors=[].concat(data.data);               
                    
            });
           
        }
        function addBookmark() {
            alert('Press ' + (navigator.userAgent.toLowerCase().indexOf('mac') !== - 1 ? 'Command/Cmd' : 'CTRL') + ' + D to bookmark this page.');
        }

        function activate() {
            return get().then(function () {

            });
        }

        function get() {
            return dataService.getUser($routeParams.id).then(function (data) {
                $ctrl.user = data.data;            
            });
        }
    }
})(window.angular);