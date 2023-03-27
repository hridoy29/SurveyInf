app.controller("AssetConfigCtrl", function ($scope, $http, blockUI) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();

    function clear() {
        $scope.entity = { Id: 0, IsCancelled: true };
        $("#txtFocus").focus();

        $scope.entityListPaged = [];
       
    };


    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/AssetConfig/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();

                $scope.entityList = data;
                $scope.total_count = data.length;

                var begin = ($scope.PerPage * ($scope.currentPage - 1));
                var end = begin + $scope.PerPage;
                $scope.entityListPaged = $scope.entityList.slice(begin, end);
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
        $scope.currentPage = curPage;
        $scope.PerPage = (angular.isUndefined($scope.PerPage) || $scope.PerPage == null) ? $scope.DefaultPerPage : $scope.PerPage;

        if ($scope.PerPage > 100) {
            $scope.PerPage = 100;
            alertify.log('Maximum record  per page is 100', 'error', '5000');
        }
        else if ($scope.PerPage < 1) {
            $scope.PerPage = 1;
            alertify.log('Minimum record  per page is 1', 'error', '5000');
        }

        var begin = ($scope.PerPage * (curPage - 1));
        var end = begin + $scope.PerPage;

        $scope.entityListPaged = $scope.entityList.slice(begin, end);
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