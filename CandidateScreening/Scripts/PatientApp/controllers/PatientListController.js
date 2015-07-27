(function (module) {
    module.controller('PatientListController', ['$scope', 'patientsQueryApiService', function (scope, patientsQueryApiService) {
        var patientList = [];
        var totalItems = 0;
        var errorMessage = "";

        scope.GetList = function () {
            return patientList;
        };
        scope.GetTotalItems = function () {
            return totalItems;
        };
        
        scope.GetError = function () {
            return errorMessage;
        };

        scope.Loading = patientsQueryApiService.Loading;

        scope.PaginationInfo = patientsQueryApiService.PaginationInfo;
        scope.SearchCriteria = patientsQueryApiService.SearchCriteria;
        scope.PageError = "";
        function load() {
            patientsQueryApiService.GetPatients().then(function (result) {
                patientList = result.List;
                totalItems = result.TotalItems;
            }, function (error) {
                errorMessage = error;
            });
        }

        scope.Refresh = load;

        load();

    }]);
})(angular.module('patientapp'));