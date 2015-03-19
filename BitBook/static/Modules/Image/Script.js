app.controller('ModuleImageFormController', ['$scope', function ($scope) {
    $scope.url = {
        text:''
    };
    $scope.submitStatus = function () {
        alert($scope.ImageUrl);
    };
}]);
app.controller('ModuleImageController', ['$scope', function ($scope) {
    $scope.imageSrc = $scope.status.data.imageSrc; // + "Yaay for controllers";
}]);