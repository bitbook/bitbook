app.controller('StatusFormController', ['$scope', function ($scope) {
    $scope.maxLength = 500;
    $scope.message = "";
    $scope.submitStatus = function () {
        alert($scope.message);
    };
}]);
app.controller('StatusController', ['$scope', function ($scope) {
    $scope.statusTxt = $scope.status.data.status; // + "Yaay for controllers";
}]);