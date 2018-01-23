app.service("userService", function ($state) {
  // Create a users array
  var _users = [];
  var _userId = 0;
  var _currentUser = null;
  
  // Create a users constructor
  var User = function (id, name, email, password, status) {
    this.id = id;
    this.name = name;
    this.email = email;
    this.password = password;
    this.status = status;
  }
  // Seed users array with 3 users
  _users.push(new User(_userId++, "Bob", "bob@gmail.com", "boom", false))
  _users.push(new User(_userId++, "Sue", "sue@gmail.com", "blam", false))
  _users.push(new User(_userId++, "Barb", "barb@gmail.com", "stick", false))

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
    for (var i = 0; i < _users.length; i++) {
      if (_users[i].email == user.email && _users[i].password == user.password) { // checking email and pw
        _currentUser = _users[i] // setting current user
        _currentUser.status = true; // setting status to logged in
        console.log(_currentUser);

        $state.go("user", { id: _users[i].id }) // navigate to user's profile
      }
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
