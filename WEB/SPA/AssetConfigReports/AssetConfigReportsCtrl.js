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
<<<<<<< HEAD
        $scope.entity = {};
    };
    function getList(curPage) {
        $scope.entityList = [];
        $scope.total_count = 0;
        var userId = $scope.userId = $cookieStore.get('UserID');
        if (curPage == null) curPage = 1;
        var startRecordNo = ($scope.PerPage * (curPage - 1)) + 1;
        if (startRecordNo<0) {
            startRecordNo = 1;
        }

        $scope.fromDate = document.getElementById("fromDate").value;
        $scope.toDate = document.getElementById("toDate").value;
        $scope.DistrbutorId = $scope.entity.DistrbutorId;
        $scope.DistrbutorName = $scope.entity.DistrbutorName;
        $scope.SurveyorName = $scope.entity.SurveyorName;
        var whereCondition = '1=1 ';
        //var params = JSON.stringify({ userId: 1 });
        if ($scope.fromDate != '' && $scope.fromDate != undefined && $scope.toDate != '' && $scope.toDate != undefined) {
            whereCondition += "and l.ModificationDate between '" + $scope.fromDate + "' and '" + $scope.toDate + "'";
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
            url: encodeURI("/AssetConfigReport/AssetConfigReportPaged?startRecordNo=" + startRecordNo + '&rowPerPage=' + $scope.PerPage + "&whereClause=" + whereCondition + '&id=' + userId + '&rows=' + 0),
=======
    };
    function getList() {
        var userId = $scope.userId = $cookieStore.get('UserID');
        $http({
            url: "/AssetConfigReport/Get?id=" + userId,
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

<<<<<<< HEAD
            if (data.ListData.length) {
                $scope.entityList = data.ListData;
                $scope.total_count = data.TotalRecord;
=======
            if (data.length) {
                $scope.entityListPaged = data;
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
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
<<<<<<< HEAD
            alertify.log(data2, 'error', '10000');
=======
            alertify.log('Unknown server error', 'error', '10000');
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
        });
    };
    $scope.getImage = function (number) {

<<<<<<< HEAD
       // window.open('http://inf.sundarbanautomation.com' + number);
        window.open('http://api.infinigentconsulting.com' + number);
    }
    $scope.search = function () {
        //$scope.fromDate = document.getElementById("fromDate").value;
        //$scope.toDate = document.getElementById("toDate").value;
        //$scope.DistrbutorId = $scope.entity.DistrbutorId;
        //$scope.DistrbutorName = $scope.entity.DistrbutorName;
        //$scope.SurveyorName = $scope.entity.SurveyorName;
        //var whereCondition = '1=1 ';
        ////var params = JSON.stringify({ userId: 1 });
        //if ($scope.fromDate != '' && $scope.fromDate != undefined && $scope.toDate != '' && $scope.toDate != undefined) {
        //    whereCondition += "and l.CreationDate between '" + $scope.fromDate + "' and '" + $scope.toDate + "'";
        //}
        //if ($scope.DistrbutorId != '' && $scope.DistrbutorId != undefined) {
        //    /*and DistributorId = +''' + $scope.DistrbutorId+'';*/
        //    whereCondition += "and DistributorId= '" + $scope.DistrbutorId + "'";
        //}
        //if ($scope.DistrbutorName != '' && $scope.DistrbutorName != undefined) {
        //    /*and DistributorId = +''' + $scope.DistrbutorId+'';*/
        //    whereCondition += "and DistributorName= '" + $scope.DistrbutorName + "'";
        //}
        //if ($scope.SurveyorName != '' && $scope.SurveyorName != undefined) {
        //    /*and DistributorId = +''' + $scope.DistrbutorId+'';*/
        //    whereCondition += "and u.Name like '" + $scope.SurveyorName + "%'";
        //}
        //$http({
        //    url: '/AssetConfigReport/GetDynamic?where=' + whereCondition + '&orderBy=l.Id',
        //    method: "GET",
        //    headers: {
        //        'Content-type': 'application/json'
        //    }
        //}).success(function (data) {
        //    $scope.entityListPaged = data
        //}).error(function (data, status, headers, config) {

        //});
        getList(0);
    }

    $scope.getExport = function (entityListPaged) {
        $scope.fromDate = document.getElementById("fromDate").value;
        $scope.toDate = document.getElementById("toDate").value;
        $scope.userId = $cookieStore.get('UserID');
=======
        window.open('http://inf.sundarbanautomation.com' + number);
    }
    $scope.search = function () {
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
        $scope.fromDate = document.getElementById("fromDate").value;
        $scope.toDate = document.getElementById("toDate").value;
        $scope.DistrbutorId = $scope.entity.DistrbutorId;
        $scope.DistrbutorName = $scope.entity.DistrbutorName;
        $scope.SurveyorName = $scope.entity.SurveyorName;
<<<<<<< HEAD

        var whereCondition = '';
=======
        var whereCondition = '1=1 ';
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
        //var params = JSON.stringify({ userId: 1 });
        if ($scope.fromDate != '' && $scope.fromDate != undefined && $scope.toDate != '' && $scope.toDate != undefined) {
            whereCondition += "and l.ModificationDate between '" + $scope.fromDate + "' and '" + $scope.toDate + "'";
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
<<<<<<< HEAD
        var params = JSON.stringify({ userId: $scope.userId, where: whereCondition });
        //var params = JSON.stringify({ userId: 1 });
        $http({
            url: '/AssetConfigReport/getExportDynamically',
=======
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
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
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
<<<<<<< HEAD
    $scope.onPageChange = function (curPage) {
        if ($scope.PerPage > 100) {
            $scope.PerPage = 100;
            $scope.currentPage = curPage;
            getList($scope.currentPage);
            toastr.error('Maximum record  per page is 100');
        }
        else if ($scope.PerPage < 1) {
            $scope.PerPage = 1;
            $scope.currentPage = curPage;
            getList($scope.currentPage);
            toastr.error('Maximum record  per page is 100');
        }
        else {
            $scope.currentPage = curPage;
            getList($scope.currentPage);
        }
    }
=======
>>>>>>> 21c34740701a270f0917d93b07ae13698f9654b8
})