(function () {
    var commondirectives = angular.module('commondirectives', []);

    //This directive makes a text box to response on pressing Enter
    commondirectives.directive('ngEnter', function () {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    });
})();