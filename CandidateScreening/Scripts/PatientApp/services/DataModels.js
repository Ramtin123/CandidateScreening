(function (module) {

    module.factory('DataModels', [
        function() {
            function address(addresstype,isDefault) {
                var vm = {
                    StreetNumber: '',
                    Line1: '',
                    Line2: '',
                    PostCode: '',
                    Suburb: '',
                    Country: '',
                    DefaultAddress: isDefault,
                    AddressType: addresstype
                };
                return vm;
            }

            var addressTypes =
            {
                Residential: 0,
                Postal: 1
            }

            function patient() {
                var vm = {
                    Id: '',
                    Firstname: '',
                    Surname: '',
                    DateOfBirth: new Date(),
                    Gender: '',
                    Email: '',
                    Addresses:[]
                };
                vm.Addresses.push(new address(addressTypes.Residential,true));
                vm.Addresses.push(new address(addressTypes.Postal));
            return vm;
        }

        return {
            Patient: patient,
            AddressTypes: addressTypes,
            Address: address
        };
    }]);
})(angular.module('patientapp'));