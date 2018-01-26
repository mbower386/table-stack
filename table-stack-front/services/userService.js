app.service("userService", function ($state, $http) {
  // Create a users array
  var _users = [];
  var _userId = 1;
  var _currentUser = null;
  var _reservationList = [];
  var _userList = [];
  var loginError = false;
  var _validId;

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

  // Get all user reservations
  var getUserReservations = function () {


  }

  // GET all
  this.getUsers = function () {
    return _users;
  }

  // GET one by ID
  this.getUserById = function (id) {
    if (id == "" || id == undefined || id == null) {
      var user = {
        id: "",
        userType: "",
        restaurantName: "",
        yelpId: "",
        fullName: "",
        email: "",
        password: "",
        phoneNumber: "",
        zipCode: "",
        reservations: [],
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

  this.returnReservationList = function () {
    // Get reservation list first
    return $http.get(`http://localhost:5000/api/reservations`)

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

        _validId = response.data;
        checkLogin(_validId);

      }, function (error) {

      })
  }

  var checkLogin = function (_validId) {
    if (_validId != -1) {

      $http.get(`http://localhost:5000/api/users/${_validId}`)
        .then(function (response) {
          getUserInfo(_validId, response);

          $state.go("user", { id: _validId })
        }, function (error) {

        })
    }
    else {
      loginError = true;
    }
  }

  var getUserInfo = function (_validId, response) {
    getUserReservations();

    _currentUser = new User(response.data.id, response.data.userType, response.data.restaurantName, response.data.yelpId, response.data.fullName, response.data.email, response.data.password, response.data.phoneNumber, response.data.zipCode, _reservationList);

    _users.push(_currentUser);
    //console.log(_currentUser);
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
    console.log(_currentUser);
    console.log(_users);
    $state.go("home") // navigate back to login page
  }

  this.notifyUser = function (smsCode, reservation) {
    console.log(reservation.phoneNumber);
    $http.get(`http://localhost:5000/api/reservations/sendMessage?smsCode=${smsCode}&phoneNumber=${reservation.phoneNumber}&fullName=${reservation.customerName}&partySize=${reservation.partySize}&waitTime=${reservation.waitTime}`)
      .then(function (response) {

        $state.go("user")
      }, function (error) {

      })
  }

  this.confirmCustomer = function (fullName, phoneNumber, partySize, waitTime) {
    console.log(reservation.phoneNumber);
    $http.get(`http://localhost:5000/api/reservations/sendMessage?smsCode=${"1"}&phoneNumber=${phoneNumber}&fullName=${customerName}&partySize=${partySize}&waitTime=${waitTime}`)
      .then(function (response) {

        $state.go("user")
      }, function (error) {

      })
  }

  this.deleteReservation = function (id) {

  }
})
