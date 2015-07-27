(function (module) {

    module.directive('patientDetailsDirective', [
        function () {
            return {
                restrict: 'EA',
                templateUrl: '/Scripts/PatientApp/templates/_patientDetailsTemplate.html',
                scope: {
                    patientid: '=',
                    onChanged: '=',
                    onClosed: '=',
                    title: '@',
                    viewMode:'='
                },
                controller: 'PatientDetailsController'
            }
        }
    ]);
})(angular.module('patientapp'));