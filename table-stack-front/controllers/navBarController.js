app.controller("navBarController", function ($scope, $state, $stateParams, $http, userService) {
  $scope.userService = userService;

  $scope.loggingOut = function () {
    userService.logout();
  };
});