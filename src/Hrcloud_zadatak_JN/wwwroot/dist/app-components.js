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
(function (angular) {
    'use strict';
    angular
        .module('app-module')
        .component('userListComponent', {
            templateUrl: '/templates/user-list.component.html',
            controller: UserListComponent
        });

    UserListComponent.$inject = ['dataService', 'uniqueFilter'];

    function UserListComponent(dataService, uniqueFilter) {
        var $ctrl = this;

        $ctrl.deleteCollection = deleteCollection;
        $ctrl.isLoading = false;
        $ctrl.searchOnServer = searchOnServer;
        $ctrl.collection = [];
        $ctrl.displayedCollection = [];

        $ctrl.$onDestroy = function () {
            dataService.dispose();
        };

        $ctrl.$onInit = function () {
            activate();
        };

        function deleteCollection() {
            if (confirm('Are you sure you want to delete --' + $ctrl.collection.length + '-- items')) {
                $ctrl.isLoading = true;
                dataService.deleteUsers($ctrl.collection).then(function (data) {
                    if (data.status === 200 && data.statusText === "OK")
                        $ctrl.isLoading = !deleteSelectedRows();
                    else
                        $ctrl.errors = [].concat(data.data);
                });
            }
        }

        function searchOnServer(tableState) {
            console.log(tableState, "table state");
            var searchTerm = undefined;

            if (tableState && tableState.search && tableState.search.predicateObject) {              
                       
                searchTerm = tableState.search.predicateObject.tagName || tableState.search.predicateObject.$;
                if (searchTerm && searchTerm.length > 0) {
                    $ctrl.isLoading = true;
                    if (dataService.apiController('search'))
                        dataService.getUser(searchTerm).then(function (data) {
                            setData(data);
                            $ctrl.isLoading = false;
                        });
                } else
                    activate();
            }
        }

        function deleteSelectedRows() {

            if ($ctrl && $ctrl.collection && $ctrl.collection.length === 0)
                return false;

            for (var i = 0; i < $ctrl.collection.length; i++) {
                var index = $ctrl.userCollection.indexOf($ctrl.collection[i]);
                if (index > -1) {
                    $ctrl.displayedCollection.splice(index, 1);
                    $ctrl.userCollection.splice(index, 1);
                    $ctrl.filterTags = uniqueFilter($ctrl.userCollection, 'tagCollection', 'tagName');
                }
            }
            $ctrl.collection = [];
            return true;
        }

        function activate() {
            if (dataService.apiController('user'))
                return get().then(function () {
                });
        }

        function get() {
            return dataService.getUsers().then(function (data) {
                setData(data);
                $ctrl.filterTags = uniqueFilter($ctrl.userCollection, 'tagCollection', 'tagName');
            });
        }

        function setData(data) {
            $ctrl.userCollection = data.data;
            $ctrl.displayedCollection = [].concat($ctrl.userCollection);          
        }
    }
})(window.angular);