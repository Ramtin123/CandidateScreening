(function (module) {

    module.factory('patientDataStore', ['$http', '$q', function ($http, $q) {
        function addPatient(patient) {
            var deferred = $q.defer();

            $http.post("/api/patients", patient)
             .then(function (result) {
                 deferred.resolve(result.data);
             },
             function (error) {
                 deferred.reject(error);
             });

            return deferred.promise;
        };

        function updatePatient(patient) {
            var deferred = $q.defer();

            $http.put("/api/patients", patient)
             .then(function (result) {
                 deferred.resolve(result.data);
             },
             function (error) {
                 deferred.reject(error);
             });

            return deferred.promise;
        };

        return {
            AddPatient: addPatient,
            UpdatePatient:updatePatient
        };
    }]);
})(angular.module('patientapp'));

