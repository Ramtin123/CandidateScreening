(function (module) {

    module.factory('patientsQueryApiService', ['$http', '$q', function ($http, $q) {

        var loadingStatus = true;

        function loading() {
            return loadingStatus;
        };

        var paginationInfo = {
            PageSize: 10,
            CurrentPage: 1
        };

        var searchCriteria = {
            SearchText: ''
        };

        function getPatient(patientid) {
            loadingStatus = true;
            var deferred = $q.defer();
            $http.get("/api/patients?patientId=" + patientid)
              .then(function (result) {
                  deferred.resolve(result.data);
              },
              function (error) {
                  deferred.reject(error);
              }).finally(function() {
                  loadingStatus = false;
                });

            return deferred.promise;
        }

        function getPatients() {
            loadingStatus = true;
            var deferred = $q.defer();
            var url = "/api/patients?" + "page=" + paginationInfo.CurrentPage + "&pageSize=" + paginationInfo.PageSize
                      + (searchCriteria.SearchText ? "&searchString=" + searchCriteria.SearchText : "");
            $http.get(url)
              .then(function (result) {
                  deferred.resolve(result.data);
              },
              function (error) {
                  deferred.reject(error);
              }).finally(function() {
                    loadingStatus = false;
                });

            return deferred.promise;
        }

        return {
            GetPatient: getPatient,
            GetPatients: getPatients,
            SearchCriteria: searchCriteria,
            PaginationInfo: paginationInfo,
            Loading:loading
        };
    }]);
})(angular.module('patientapp'));

