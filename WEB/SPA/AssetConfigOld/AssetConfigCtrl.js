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
        $scope.distributorType = "Distributor";

        $scope.entityListPaged = [];
        $scope.distributorList = [];
        $scope.AICList = [];
        $scope.ASMList = [];
        $scope.MDOList = [];
        $scope.OutletList = [];
        $scope.CoolerList = [];

        $scope.cmbdistributor = {};
        $scope.cmbAIC = {};
        $scope.cmbASM = {};
        $scope.cmbMDO = {};
        $scope.cmbOutlet = {};
        $scope.cmbCooler = {};

        getDistributorList();
        getAICList();
        getASMList();
        getMDOList();
        getOutletList();
        getCoolerList();
    };

    function getDistributorList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Distributor/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.distributorList = data;

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function getAICList() {
        $scope.lsitBlock.start();
        $http({
            url: "/AIC/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.AICList = data;

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function getASMList() {
        $scope.lsitBlock.start();
        $http({
            url: "/ASM/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.ASMList = data;

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };


    function getMDOList() {
        $scope.lsitBlock.start();
        $http({
            url: "/MDO/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.MDOList = data;

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function getOutletList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Outlet/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.OutletList = data;

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function getCoolerList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Cooler/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.CoolerList = data;

        }).error(function (data2) {
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/AssetInfo/Get",
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
        var dis= $scope.distributorList.filter(x => x.Id == obj.DistributorId);
        $scope.cmbdistributor = dis[0];
        var AIC = $scope.AICList.filter(x => x.Id == obj.AICId);
        $scope.cmbAIC = AIC[0];
        var ASM = $scope.ASMList.filter(x => x.Id == obj.ASMId);
        $scope.cmbASM = ASM[0];
        var MDO = $scope.MDOList.filter(x => x.Id == obj.MDOId);
        $scope.cmbMDO = MDO[0];
        var Outlet = $scope.OutletList.filter(x => x.Id == obj.OutletId);
        $scope.cmbOutlet = Outlet[0];
        var Cooler = $scope.CoolerList.filter(x => x.Id == obj.CoolerId);
        $scope.cmbCooler = Cooler[0];
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        getList();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };

    $scope.post = function (trnType) {
        if (trnType === 'save') {
            //if ($scope.cmbdistributor == undefined || $scope.cmbAIC == undefined || $scope.cmbASM == undefined || $scope.cmbMDO == undefined || $scope.cmbOutlet == undefined || $scope.cmbCooler == undefined) {
            //    alertify.error("All the fields should be selected! ");
            //}
            //else {
            //    trnType = $scope.entity.Id === 0 ? "INSERT" : "UPDATE";
            //    submitRequest(trnType);
            //}
            trnType = $scope.entity.Id === 0 ? "INSERT" : "UPDATE";
            submitRequest(trnType);
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

        $http.post('/AssetInfo/Post', params).success(function (data) {
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