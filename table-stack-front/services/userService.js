app.service("userService", function ($state, $http) {
  // Create a users array
  var _users = [];
  var _userId = 0;
  var _currentUser = null;
  var _reservationList = [];
  var _userList = [];
  var loginError = false;
  var _isLoginValid;

  // Create a users constructor
  var User = function (id, userType, restaurantName, yelpId, fullName, email, password, phoneNumber, zipCode) {
    this.id = id;
    this.userType = userType;
    this.restaurantName = restaurantName;
    this.yelpId = yelpId;
    this.fullName = fullName;
    this.email = email;
    this.password = password;
    this.phoneNumber = phoneNumber;
    this.zipCode = zipCode;
    this.reservations = [];
  }

  // Get reservation list first
  $http.get(`http://localhost:5000/api/reservations`)
    .then(function (response) {
      console.log(response);
      _reservationList = response.data;
    }, function (error) {

    })

  // GET all
  this.getUsers = function () {
    return _users;
  }

  // GET one by ID
  this.getUserById = function (id) {
    if (id == "" || id == undefined || id == null) {
      var user = {
        name: "",
        email: "",
        password: "",
        status: false
      }
      return user;
    }
    else {
      for (var i = 0; i < _users.length; i++) {
        if (_users[i].id == id) {
          return _users[i]
        }
      }
    }
  }

  this.returnUser = function () {
    return _currentUser;
  }

  // CREATE
  this.addUser = function (user) {
    user.id = _userId++
    _users.unshift(user)
    $state.go("login")
  }

  // LOGIN
  this.login = function (user) {
    $http.get(`http://localhost:5000/api/users/login?email=${user.email}&password=${user.password}`)
      .then(function (response) {
        console.log(response);

        _isLoginValid = response.data;
        checkLogin(_isLoginValid);

      }, function (error) {

      })
  }

  var checkLogin = function (validLogin) {
    if (validLogin == "true") {
      $state.go("user")
    }
    else {
      loginError = true;
    }
  }

  // REGISTER
  this.register = function (user) {
    user.id = _userId++ // set the id
    _users.unshift(user) // add the user
    console.log(_users);
    $state.go("login") // navigate to login for user to now login.
  }

  // LOGOUT
  this.logout = function () {
    _currentUser.status = false;
    _currentUser = null; // Set user back to null
    console.log(_users);
    $state.go("login") // navigate back to login page
  }

})
