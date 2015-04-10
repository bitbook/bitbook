describe('RegisterController Tests', function () {
    beforeEach(module('yella'));
    var $rootScope, $scope, $controller, mock;

    beforeEach(inject(function (_$rootScope_, _$controller_, yellaService, currentUser) {
        $rootScope = _$rootScope_;
        $scope = $rootScope.$new();
        $controller = _$controller_;

        mock = {
            '$rootScope': $rootScope,
            '$scope': $scope,
            'yellaService': yellaService,
            'currentUser': currentUser
        };
        $controller('RegisterController', mock);
    }));

    it('name taken warning should be hidden', function () {
        expect($scope.nameTaken).toBe(false);
    });
    it('check new user', function () {
        // mock services
        spyOn(mock.yellaService, "newUser").and.returnValue(true);
        spyOn(mock.currentUser, "user").and.returnValue(true);

        // setup form values
        $scope.nick = "WOOP";

        //call form
        $scope.submit();

        //check correct changes were made
        expect($scope.nameTaken).toBe(false);
        expect($rootScope.user).not.toBe(false);
        expect(localStorage.getItem("user")).toBe('WOOP');

        //clear localstorage

        localStorage.clear();
    });
    it('check new user with name taken', function () {
        // mock services
        spyOn(mock.yellaService, "newUser").and.returnValue(false);
        //call form
        $scope.submit();
        //check correct changes were made
        expect($scope.nameTaken).toBe(true);
    });
});