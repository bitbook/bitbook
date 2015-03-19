app.controller('ModuleImageFormController', ['$state','$scope', 'yellaService','currentUser', function ($state, $scope, yellaService, currentUser) {
    $scope.url = {
        text:''
    };
    $scope.submitStatus = function () {
        alert($scope.ImageUrl);
    };
    $scope.submitStatus = function () {
        yellaService.newStatus(currentUser.user().nick, 'Image', 'v0.1', {
            imageSrc: $scope.ImageUrl
        });
        $state.forceReload();
    };
}]);
app.controller('ModuleImageController', ['$scope', function ($scope) {
    $scope.imageSrc = $scope.status.data.imageSrc; // + "Yaay for controllers";
}]);