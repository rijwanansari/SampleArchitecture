app.service('httpService', ['$http',function ($http) {
    var httpService = {};
    var serviceUrl = "http://localhost:58007/";
    httpService.get = function (url, data) {
        return $http.get(serviceUrl + url
            , data
            , { headers: { 'Content-Type': 'application/json' } });
    };

    httpService.post = function (url, data) {
        var returnData = "=" + JSON.stringify(data);
        return $http.post(serviceUrl + url
            , data
            , { headers: { 'Content-Type': 'application/json' } });
    };

    httpService.upload = function (url, data) {
        return $http.post(serviceUrl + url
            , data
            , { headers: { 'Content-Type': undefined } });
    };

    return httpService;
}]);
