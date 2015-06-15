(function () {
    angular.module('app').factory('svc', ['$http', '$q', function ($http, $q) {

        // factory builds a service, the name given here of the service is svc
        // $http and $q are also named here so that we can use them in the service

        var service = {};

        service.getYears = function () {
            var deferred = $q.defer();
            $http.get('api/Cars/GetYears')
                   .then(function (response) {
                       deferred.resolve(response.data);
                   })

            return deferred.promise;

        }

        service.getMakes = function (selected) {
            var options = {
                params: {
                    year: selected.year
                }
            }

            return $http.get('api/Cars/GetMakes', options)
                   .then(function (response) {
                       return response.data;
                   })
            }
        

        service.getModels = function (selected) {
            return $http.post('api/Cars/GetModels', selected)
                   .then(function (response) {
                       return response.data;
                   })
            
        }

        service.getTrims = function (selected) {
            return $http.post('api/Cars/GetTrims', selected)
                   .then(function (response) {
                       return response.data;
                   })

            
        }
        return service;

    }])
})();