app.controller("AssetConfigCtrl", function ($scope, $http, blockUI, $cookieStore) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityListPaged = [];
    $scope.entityListPagedPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    $scope.UserData = $cookieStore.get('UserData');
    clear();
    getUserid();

    function clear() {
        $scope.entity = { Id: 0, IsCancelled: true };
        $("#txtFocus").focus();
        $scope.entityListPagedPaged = [];
       
    };


    function getUserid() {
       
        $http({
            url: "/User/GetUserId?email=" + $scope.UserData.Username + "&passcode=" + $scope.UserData.Password,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.userId = data[0].Id;
            getList(0);
        });
    }

    $scope.Search = function () {
        $scope.entityListPaged = [];
        $scope.where = "1=1";
        if ($scope.search.DistributorId != undefined && $scope.search.DistributorId!="") {
            $scope.where = $scope.where + " and DistributorId='" + $scope.search.DistributorId + "'";
        }
        if ($scope.search.DistributorName != undefined && $scope.search.DistributorName != "") {
            $scope.where = $scope.where + " and DistributorName like'%" + $scope.search.DistributorName + "%'";
        }
        if ($scope.search.AssetNumber != undefined && $scope.search.AssetNumber != "") {
            $scope.where = $scope.where + " and AssetNumber='" + $scope.search.AssetNumber+"'";
        }
        getList(0);
    }

    $scope.Clear = function () {
        $scope.search.DistributorId = "";
        $scope.search.DistributorName = "";
        $scope.search.AssetNumber = "";
        $scope.where = "";
        getList(0);
    }
    function getList(curPage) {
        $scope.entityListPagedPaged = [];
        $scope.total_count = 0;
        var userId = $scope.userId;
        if (curPage == null) curPage = 1;
        var startRecordNo = ($scope.PerPage * (curPage - 1)) + 1;
        if (startRecordNo < 0) {
            startRecordNo = 1;
        }
        //$scope.fromDate = document.getElementById("fromDate").value;
        //$scope.toDate = document.getElementById("toDate").value;
        //$scope.DistrbutorId = $scope.entity.DistrbutorId;
        //$scope.DistrbutorName = $scope.entity.DistrbutorName;
        //$scope.SurveyorName = $scope.entity.SurveyorName;
        var whereCondition = "";
        if ($scope.where != undefined && $scope.where!="") {
            whereCondition = $scope.where;
            if (whereCondition.includes('&')) {
                whereCondition = whereCondition.replace('&', 'and22');
            }
        }

     

        $scope.lsitBlock.start();
        $http({
            url: encodeURI("/AssetConfig/AssetConfigPaged?startRecordNo=" + startRecordNo + '&rowPerPage=' + $scope.PerPage + "&whereClause=" + whereCondition + '&id=' + userId + '&rows=' + 0),
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.ListData.length) {
                $scope.entityListPaged = data.ListData;
                $scope.total_count = data.TotalRecord;
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


    $scope.GetPaged = function (curPage) {
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

    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        getList();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };

    $scope.post = function (trnType) {

        if (trnType == "save") {
            trnType = $scope.entity.Id === 0 ? "INSERT" : "UPDATE";
            if (trnType == "INSERT") {

                if ($scope.entity.AssetNumber != undefined && $scope.entity.SerialNumber != undefined) {
                    var where = "AssetNumber = " + $scope.entity.AssetNumber + " and IsCancelled=0";
                    $http({
                        url: '/AssetConfig/GetDynamic?where=' + where + '&orderBy=AssetNumber',
                        method: 'GET',
                        headers: { 'Content-Type': 'application/json' }
                    }).success(function (data) {
                        if (data.length > 0) {
                            alertify.log($scope.entity.AssetNumber + ' already exists!', 'already', '5000');
                            $('#txtFocus').focus();
                        } else {
                            if (trnType === "INSERT") {
                                if ($scope.entity.DistributorId == undefined) {
                                    $scope.entity.DistributorId = "N/A";
                                }
                                if ($scope.entity.DistributorName == undefined) {
                                    $scope.entity.DistributorName = "N/A";
                                }
                                if ($scope.entity.AICName == undefined) {
                                    $scope.entity.AICName = "N/A";
                                }
                                if ($scope.entity.ASMName == undefined) {
                                    $scope.entity.ASMName = "N/A";
                                }
                                if ($scope.entity.MDOId == undefined) {
                                    $scope.entity.MDOId = "N/A";
                                }
                                if ($scope.entity.MDOName == undefined) {
                                    $scope.entity.MDOName = "N/A";
                                }
                                if ($scope.entity.OutletId == undefined) {
                                    $scope.entity.OutletId = "N/A";
                                }
                                if ($scope.entity.OutletName == undefined) {
                                    $scope.entity.OutletName = "N/A";
                                }
                                if ($scope.entity.OutletAddress == undefined) {
                                    $scope.entity.OutletAddress = "N/A";
                                }
                                if ($scope.entity.ContactNo == undefined) {
                                    $scope.entity.ContactNo = "N/A";
                                }
                                if ($scope.entity.CoolerModel == undefined) {
                                    $scope.entity.CoolerModel = "N/A";
                                }
                                $scope.entity.IsCancelled = false;
                                $scope.entity.Device_Number = "N/A";
                                $scope.entity.IsRepeated = false;

                                $scope.entity.IsUpdated = false;
                                submitRequest(trnType);
                            }
                        }
                    });
                }
                else {
                    alertify.log('Please Insert AssetNumber or SerialNumber','error', '5000');
                }

               
            }
            else {
                $scope.entity.IsUpdated = true;

                submitRequest(trnType);
            }
        }
        else {
            trnType = "DELETE";

            alertify.set({
                labels: {
                    ok: "Yes",
                    cancel: "No"
                },
                buttonReverse: true
            });

            alertify.confirm('Are you sure to delete?', function (e) {
                if (e) {
                    submitRequest(trnType);
                }
            });
        }
    };


    function submitRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType });

        $http.post('/AssetConfig/Post', params).success(function (data) {
            $scope.entryBlock.start();
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.entryBlock.stop();
                    $scope.resetForm();
                    getList();
                    alertify.log(data, 'success', '5000');
                }
                else {
                    $scope.entryBlock.stop();
                    alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                }
            }
            else {
                $scope.entryBlock.stop();
                alertify.log('System could not execute the operation.', 'error', '10000');
            }
        }).error(function () {
            $scope.entryBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

  

})