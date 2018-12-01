app.service('queryStringService', ['$location', function ($location) {
    this.getFilters = function (filterObj) {
        var queryString = $location.search();
        for (var param in filterObj) {
            if (param in queryString) {
                filterObj[param] = queryString[param];
            }
        }
        return filterObj;
    };
}]);