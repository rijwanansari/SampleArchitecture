(app.controller("homeCtrl", ["$rootScope", "$scope", "queryStringService", "httpService"
    , function ($rootScope
        , $scope
        , queryStringService, httpService) {

        $scope.Title = "Home Page";
        $scope.cardInputViewModel = {

        };
        $scope.responseOutput = {};
        $scope.ValidateCard = function () {
            try {
                var cardInputViewModel = $scope.cardInputViewModel;
                httpService.post("api/Card/CheckCard", cardInputViewModel).then(
                    function success(response) {
                        $scope.responseOutput = response.data;  
                    }, function error() {
                        alert("Something went wrong!");
                    }
                );
            } catch (e) {
                alert(e);
            }
        };

        $rootScope.replaceUrl = function (url, paramter, value) {
            var n = url.includes("?");
            if (n)
                return url + "&" + paramter + "=" + value;
            else
                return url + "?" + paramter + "=" + value;
        };

    }]));