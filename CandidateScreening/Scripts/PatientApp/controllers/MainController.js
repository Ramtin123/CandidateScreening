(function (module) {

    module.controller('maincontroller', [function () {
        var vm = this;

        vm.patientInfo = {
            currentPatientId: undefined
        };

        vm.statusList = {
            ListMode: 'ListMode',
            AddMode: 'AddMode',
            DetailsViewMode: 'DetailsViewMode',
            EditMode: 'EditMode'
        };

        vm.currentState = vm.statusList.ListMode;

        vm.changeState=function(state) {
            vm.currentState = state;
        };

        vm.addPatientHandler = function(patient) {
            vm.currentState = vm.statusList.ListMode;
        }
        vm.editPatientHandler = function (patient) {
            vm.currentState = vm.statusList.ListMode;
        }
        vm.getBackToListHandler= function() {
            vm.currentState = vm.statusList.ListMode;
        }
        vm.detailsShowHandler = function (patientId) {
            vm.patientInfo.currentPatientId = patientId;
            vm.currentState = vm.statusList.DetailsViewMode;
        }
        vm.editShowHandler = function (patientId) {
            vm.patientInfo.currentPatientId = patientId;
            vm.currentState = vm.statusList.EditMode;
        }



    }]);
})(angular.module('patientapp'));