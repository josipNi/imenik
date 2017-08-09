(function (angular) {
    'use strict';
    angular
        .module('app-module')
        .service('dataService', dataService);

    dataService.$inject = ['$http'];

    function dataService($http) {
        var _url = '/api/';
        var _controller = undefined;
        var _apiUrl = undefined;

        return {
            getUsers: getUsers,
            getUser: getUser,
            apiController: apiController,
            save: save,
            deleteUsers: deleteUsers,
            dispose: dispose
        };

        function dispose() {
            _apiUrl = undefined;
            _controller = undefined;
        }

        function apiController(controller) {
            if (controller) {
                _apiUrl = `${_url}${controller}`;
                console.log(_apiUrl);
                return true;
            } else
                return false;
        }

        function getUsers() {
            return $http.get(_apiUrl)
                .then(getSuccess, getFailed);
        }

        function getUser(value) {            
            return $http.get(`${_apiUrl}/${value}`)
                .then(getSuccess, getFailed);
        }

        function save(user) {
            console.log(user, user.id);
            if (user && user.id) // update
                return putUser(JSON.stringify(user));
            else if(user) // create new
                return postUser(JSON.stringify(user));
        }

        function putUser(user) {
            console.log('put user', user);
            return $http.put(_apiUrl,user)
                .then(getSuccess, getFailed);
        }

        function postUser(user) {
            console.log('post user', user);
            return $http.post(_apiUrl,user)
                .then(getSuccess, getFailed);
        }

        function deleteUsers(users) {
            console.log('delete users', users);
            //return $http.post(_apiUrl + '/delete', JSON.stringify(users)).then(getSuccess, getFailed);
            return $http({
                method: 'DELETE',
                url: _apiUrl,
                data: users,
                headers: {
                    'Content-type': 'application/json'
                }
            }).then(getSuccess, getFailed);
        }

     

        function getSuccess(response) {
            return response;
        }
        function getFailed(error) {
            return error;
        }
    }

})(window.angular);