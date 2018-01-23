app.controller("userController", function ($scope, $state, $stateParams, userService) {
  $scope.errorMessage = false;
  $scope.user = userService.returnUser();

  // GET all
  $scope.users = userService.getUsers()

  // GET one by ID
  if ($stateParams.id == "" || $stateParams.id == undefined || $stateParams.id == null) {
    //$scope.user = userService.getUserById($stateParams.id) // should be empty object
    $scope.submitButton = true;
    $scope.heading = "Sign Up!"
  }
  else {
    $scope.user = userService.getUserById($stateParams.id) // should be full object
    $scope.submitButton = false;
    $scope.heading = "Update User!"
  }

  // CREATE
  $scope.addUser = function () {
    userService.addUser($scope.user)
  }

  // LOGIN
  $scope.login = function () {
    userService.login($scope.user)
  }

  // REGISTER
  $scope.register = function () {
    console.log($scope.user.name.length);
    // checking fields to either submit user OR trigger error message
    if (($scope.user.password == $scope.user.confirmPassword) && $scope.user.name.length != 0 && $scope.user.email.length != 0) {
      userService.register($scope.user)
    }
    else {
      $scope.errorMessage = true;
    }
  }


  // LOGOUT
  $scope.logout = function () {
    userService.logout()
  }

})
