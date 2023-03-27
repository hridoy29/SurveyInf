app.controller("AssetConfigReportsCtrl", function ($scope, $http, blockUI, $cookieStore) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    $scope.UserData = $cookieStore.get('UserData');
    clear();
    getList();
    getUserDetails();

    function clear() {
        $scope.entityListPaged = [];
        $scope.userData = {};
    };
    function getList() {
        var userId = $scope.userId = $cookieStore.get('UserID');
        $http({
            url: "/AssetConfigReport/Get?id=" + userId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.entityListPaged = data;
            }
            else {
                $scope.lsitBlock.stop();
                //alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }

        }).error(function (data2) {
            $scope.lsitBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function getUserDetails() {
        var userId = $scope.userId = $cookieStore.get('UserID');
        var where = "Id=" + userId;
        $http({
            url: "/User/GetDynamic?where=" + where,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            $scope.userData = data[0];

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };
    $scope.getImage = function (number) {

        window.open('http://inf.sundarbanautomation.com' + number);
    }
    $scope.search = function () {
        $scope.fromDate = document.getElementById("fromDate").value;
        $scope.toDate = document.getElementById("toDate").value;
        $scope.DistrbutorId = $scope.entity.DistrbutorId;
        $scope.DistrbutorName = $scope.entity.DistrbutorName;
        $scope.SurveyorName = $scope.entity.SurveyorName;
        var whereCondition = '1=1 ';
        //var params = JSON.stringify({ userId: 1 });
        if ($scope.fromDate != '' && $scope.fromDate != undefined && $scope.toDate != '' && $scope.toDate != undefined) {
            whereCondition += "and l.CreationDate between '" + $scope.fromDate + "' and '" + $scope.toDate + "'";
        }
        if ($scope.DistrbutorId != '' && $scope.DistrbutorId != undefined) {
            /*and DistributorId = +''' + $scope.DistrbutorId+'';*/
            whereCondition += "and DistributorId= '" + $scope.DistrbutorId + "'";
        }
        if ($scope.DistrbutorName != '' && $scope.DistrbutorName != undefined) {
            /*and DistributorId = +''' + $scope.DistrbutorId+'';*/
            whereCondition += "and DistributorName= '" + $scope.DistrbutorName + "'";
        }
        if ($scope.SurveyorName != '' && $scope.SurveyorName != undefined) {
            /*and DistributorId = +''' + $scope.DistrbutorId+'';*/
            whereCondition += "and u.Name like '" + $scope.SurveyorName + "%'";
        }
        $http({
            url: '/AssetConfigReport/GetDynamic?where=' + whereCondition + '&orderBy=l.Id',
            method: "GET",
            headers: {
                'Content-type': 'application/json'
            }
        }).success(function (data) {
            $scope.entityListPaged = data
        }).error(function (data, status, headers, config) {

        });
    }

    $scope.getExport = function (entityListPaged) {
        $scope.fromDate = document.getElementById("fromDate").value;
        $scope.toDate = document.getElementById("toDate").value;
        $scope.userId = $cookieStore.get('UserID');
        var params = JSON.stringify({ userId: $scope.userId, fromDate: $scope.fromDate, toDate: $scope.toDate });
        //var params = JSON.stringify({ userId: 1 });
        $http({
            url: '/AssetConfigReport/getExport',
            method: "POST",
            data: params, //this is your json data string
            headers: {
                'Content-type': 'application/json'
            },
            responseType: 'blob'
        }).success(function (data, status, headers, config) {
            var blob = new Blob([data], { type: 'application/vnd.ms-excel' });
            var objectUrl = URL.createObjectURL(blob);
            window.open(objectUrl);
        }).error(function (data, status, headers, config) {

        });
    }
})