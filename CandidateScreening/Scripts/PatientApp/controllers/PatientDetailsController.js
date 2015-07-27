(function (module) {
    module.controller('PatientDetailsController', ['$scope', 'DataModels', 'patientsQueryApiService', 'patientDataStore', function (scope, dataModels, patientsQueryApiService, patientDataStore) {
        scope.Patient = new dataModels.Patient();

        scope.FormStatus = {
            addingTried: false,
            isEditable:false
        };

        if (scope.viewMode === 'AddMode' || scope.viewMode === 'EditMode') {
            scope.FormStatus.isEditable = true;
        }

        scope.SetDefaultAddress = function (address) {
            _.forEach(scope.Patient.Addresses, function(item) {
                item.DefaultAddress = false;
            });
            address.DefaultAddress = true;
        };
        
        scope.OK = function(form) {
            if (form.$invalid) {
                scope.FormStatus.addingTried = true;
                return;
            }
            var checkinFunc = (!scope.patientid ? patientDataStore.AddPatient : patientDataStore.UpdatePatient);
            checkinFunc(scope.Patient).then(function () {
                scope.onChanged();
            }, function (error) {
                return;
            });
        };

        if (scope.patientid) {
            patientsQueryApiService.GetPatient(scope.patientid).then(function (result) {
                scope.Patient = result;

                var residentialAddress = _.find(result.Addresses, function(item) {
                    return item.AddressType === dataModels.AddressTypes.Residential;
                });
                if (!residentialAddress) scope.Patient.Addresses.push(new dataModels.Address(dataModels.AddressTypes.Residential));

                var postalAddress = _.find(result.Addresses, function (item) {
                    return item.AddressType === dataModels.AddressTypes.Postal;
                });
                if (!postalAddress) scope.Patient.Addresses.push(new dataModels.Address(dataModels.AddressTypes.Postal));

            }, function(error) {
                scope.Error = error;
            });
        }
        scope.Close = function () {
            scope.onClosed();
        }
    }]);
})(angular.module('patientapp'));