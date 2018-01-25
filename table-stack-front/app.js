var app = angular.module("tableStack", ["ui.router"])

app.config(function ($stateProvider, $urlRouterProvider) {

  $urlRouterProvider.otherwise("/")

  $stateProvider
    // Home
    .state("home", {
      url: "/",
      templateUrl: "./views/home.html"
    })

    // User
    .state("login", { //INDEX
      url: "/login",
      templateUrl: "./views/login.html",
      controller: "userController"
    })
    .state("userCreate", { // CREATE
      url: "/signup",
      templateUrl: "./views/users-form.html",
      controller: "userController"
    })
    .state("user", { // SHOW
      url: "/users/:id",
      templateUrl: "./views/user.html",
      controller: "userController"
    })
    .state("logout", { // SHOW
      url: "/",
      templateUrl: "./views/home.html",
      controller: "userController"
    })

    // Reservations
    .state("reservations", { //INDEX
      url: "/reservations",
      templateUrl: "./views/reservations.html",
      controller: "reservationController"
    })
    .state("reservationCreate", { // CREATE
      url: "/reservations/new",
      templateUrl: "./views/reservations-form.html",
      controller: "reservationController"
    })
    .state("reservation", { // SHOW
      url: "/reservation",
      templateUrl: "./views/reservation.html",
      controller: "viewreservationController"
    })

}) 