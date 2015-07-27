(function (module) {

    module.directive('patientListDirective', [
        function () {
            return {
                restrict: 'EA',
                templateUrl: '/Scripts/PatientApp/templates/_patientListTemplate.html',
                scope: {
                    onEditRequested: '=',
                    onDetailsViewRequested:'='
                },
                controller: 'PatientListController'
            }
        }
    ]);
})(angular.module('patientapp'));
