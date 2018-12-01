(app.controller("homeCtrl", ["$rootScope", "$scope", "queryStringService", "httpService","$filter"
    , function ($rootScope
        , $scope
        , queryStringService, httpService, $filter) {

        $scope.Title = "Home Page";
        $scope.currency = {};
        $scope.currencies = [];
        $scope.transactionInputModel = {
           
        };
        $scope.checkBalance = {};
        $scope.depositCurrency = {};
        $scope.balanceCheckAccountNumber = "1111";
        $scope.responseOutput = {};        
        $scope.OnInit = function () {
            $scope.GetAllCurrency();
        };

        $scope.GetAllCurrency = function () {
            try {
                httpService.get("api/Prefix/GetAllCurrency", null).then(
                    function success(response) {
                        if (response.data.success) {
                            $scope.currencies = response.data.output;
                        }
                        else {
                            alert(response.data.message);
                        }

                    }, function error() {
                        alert("Failure response GetAllCurrency");
                    }
                );
            } catch (e) {
                alert('GetAllCurrency ' + e.message);
            }
        };
        $scope.OnDeposit = function () {
            try {
                $scope.transactionInputModel.currency = $scope.transactionInputModel.depositCurrency.IsoCode;
                var Param = JSON.stringify($scope.transactionInputModel);
                httpService.post("api/Account/Deposit", Param).then(
                    function success(response) {                       
                            alert(response.data.message);
                            $scope.responseOutput = response.data;                       
                    }, function error() {
                        alert("failure response OnDeposit");
                    }
                );
            } catch (e) {
                alert('OnDeposit ' + e.message);
            }
        };
        $scope.OnWithdraw = function () {
            try {
                $scope.transactionInputModel.currency = $scope.transactionInputModel.depositCurrency.IsoCode;
                var Param = JSON.stringify($scope.transactionInputModel);
                httpService.post("api/Account/Withdraw", Param).then(
                    function success(response) {
                        alert(response.data.message);
                        $scope.responseOutput = response.data;
                    }, function error() {
                        alert("failure response OnDeposit");
                    }
                );
            } catch (e) {
                alert('OnDeposit ' + e.message);
            }
        };
        $scope.OnBalanceCheck = function () {
            try {
                var accountNumber = $scope.checkBalance.balanceCheckAccountNo;
                httpService.get("api/Account/Balance?accountNumber=" + accountNumber, null).then(
                    function success(response) {
                        alert(response.data.message);
                        $scope.responseOutput = response.data;
                    }, function error() {
                        alert("failure response OnBalanceCheck");
                    }
                );
            } catch (e) {
                alert('OnBalanceCheck ' + e.message);
            }
        };
    }]));