angular.module('project', [])
    .service('Transactions', function ($q, $http) {
        this.fetch = function () {
            var deferred = $q.defer();

            $http({ method: 'GET', url: '/api/transactions' })
                .then(function (response) {
                    deferred.resolve(response.data);
                });

            return deferred.promise;
        };

        this.update = function (transaction) {
            var deferred = $q.defer();

            $http({method: 'POST', url: '/transactions/' +transaction.id, data: transaction })
                .then(function(response) {
                    angular.extend(transaction, response.data);
                    deferred.resolve(transaction);
                });

                return deferred.promise;
            };
        })
    .service('Goals', function ($q, $http) {
        this.fetch = function () {
            var deferred = $q.defer();

            $http({ method: 'GET', url: '/goals' })
                .then(function (response) {
                    deferred.resolve(response.data);
                });

            return deferred.promise;
        };

        this.add = function (goal) {
            var deferred = $q.defer();

            $http({ method: 'POST', url: '/goals', data: goal })
                .then(function (response) {
                    angular.extend(goal, response.data);
                    deferred.resolve(goal);
                });

            return deferred.promise;
        };

        this.update = function (goal) {
            var deferred = $q.defer();

            $http({ method: 'POST', url: '/goals/' + goal.id, data: goal})
                .then(function(response) {
                    angular.extend(goal, response.data);
                    deferred.resolve(goal);
                });

                return deferred.promise;
            };

        this.delete = function (goal) {
            var deferred = $q.defer();

            $http({ method: 'DELETE', url: '/goals' + goal.id })
                .then(function () {
                    deferred.resolve();
                });

            return deferred.promise;;
        };
    })
    .service('Categories', function ($q, $http) {
        this.fetch = function () {
            var deferred = $q.defer();

            $http({ method: 'GET', url: '/categories' })
                .then(function (response) {
                    deferred.resolve(response.data);
                });

            return deferred.promise;
        };

        this.add = function (category) {
            var deferred = $q.defer();

            $http({ method: 'POST', url: '/categories', data: category })
                .then(function (response) {
                    angular.extend(category, response.data);
                    deferred.resolve(category);
                });

            return deferred.promise;
        };

        this.update = function (category) {
            var deferred = $q.defer();

            $http({ method: 'PUT', url: '/categories/' + category.id, data: category })
                .then(function (response) {
                    angular.extend(category, response.data);
                    deferred.resolve(category);
                });

            return deferred.promise;
        };

        this.delete = function (category) {
            var deferred = $q.defer();

            $http({ method: 'DELETE', url: '/categories/' +category.id })
                .then(function () {
                    deferred.resolve();
                });

            return deferred.promise;;
        };
    })
    .controller('ListCtrl', function ($scope, Transactions, Categories, Goals) {
        $scope.transactions = [];
        $scope.categories = [];
        $scope.bankBalance = 2359;

        $scope.addGoal = function () {
            Goals.add({ name: $scope.goalText, hue: getRandomInt(0, 360) }).then(refreshGoals);
            $scope.goalText = null;
        };

        var refreshGoals = function () {
            Categories.fetch().then(function (data) {
                $scope.categories = data;

                $scope.goalTotal = $scope.categories.reduce(function (total, categories) {
                    return total + category.balance;
                }, 0);

                $scope.availableBalance = $scope.bankBalance - $scope.goalTotal;
            });
        };

        refreshGoals();

        Transactions.fetch().then(function (data) {
            $scope.transactions = data;
        });
    });


function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}