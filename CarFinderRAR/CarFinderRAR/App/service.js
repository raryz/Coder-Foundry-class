(function () {
    angular.module('app').factory('svc', ['$http', '$q', function ($http, $q) {

        // factory builds a service, the name given here of the service is svc
        // $http and $q are also named here so that we can use them in the service

        var service = {};

        service.getYears = function () {
            var deferred = $q.defer();
            return $http.get('api/Car/GetYears')
                   .then(function (response) {
                       return response.data;
                   })

            //return deferred.promise;

        }

        service.getMakes = function (year) {
            var deferred = $q.defer();
            return $http.get('api/Car/GetMakes/year')
                   .then(function (response) {
                       return response.data;
                   })
            }

            //return deferred.promise;
        

        service.getModels = function (year, make) {
            var deferred = $q.defer();
            return $http.get('api/Car/getModels/year/make')
                   .then(function (response) {
                       return response.data;
                   })
            
        }

        service.getTrims = function (year, make, model) {
            var deferred = $q.defer();
            return $http.get('api/Car/getTrims/year/make/model')
                   .then(function (response) {
                       return response.data;
                   })

            
        }
        return service;

    }])
})();