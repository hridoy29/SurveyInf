app.controller("ItemCtrl", function ($scope, $cookieStore, $window, $location, $http, blockUI) {
    $scope.DefaultPerPage = 10;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();
    getItemGroup();
    getCategory();

    function clear() {
        $scope.entity = { Id: 0, IsActive: true };
        $("#txtFocus").focus();
        $scope.GroupName = '-- Select Item Group --';
        $scope.CategoryName = '-- Select Category --';
    };

    $scope.onPageChange = function (currentpage) {
        $scope.currentPage = currentpage;
        var begin = ($scope.PerPage * ($scope.currentPage - 1));
        var end = begin + $scope.PerPage;
        var total_page = $scope.entityList.length / $scope.DefaultPerPage;
        $scope.entityListPaged = $scope.entityList.slice(begin, end);
    }

    function getItemGroup() {


        $http({
            url: "/ItemGroup/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.itemGroupList = data;
            }
        });
    }

    function getCategory() {


        $http({
            url: "/Category/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.categoryList = data;
            }
        });
    }
    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Item/Get",
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

    function submitRequest(trnType) {

        var id = $cookieStore.get('UserID');
        $scope.entity.CreatorId = id;
        $scope.entity.ModifierId = id;
            
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType });

        $http.post('/Item/Post', params).success(function (data) {
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

    $scope.post = function (trnType) {
        var where = "Name = '" + $scope.entity.Name + "'";
        if ($scope.entity.Id > 0)
            where += " AND Id <> " + $scope.entity.Id;

        $http({
            url: '/Item/GetDynamic?where=' + where + '&orderBy=Name',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.Name + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
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
            }
        });
    };


    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        var a = $scope.itemGroupList
        var d = $scope.categoryList
        var i
        for (i = 0; i <= a.length; i++) {
            if (a[i].Id == obj.ItemGroupId) {
                $scope.cmbGroupType = a[i].GroupName
                $scope.GroupName = a[i].GroupName
                $scope.cmbCategory = d[i].Description
                $scope.CategoryName = d[i].Description
            }
            if (d[i].Id == obj.CategoryId) {
                $scope.cmbCategory = d[i].Description
                $scope.CategoryName = d[i].Description
            }
        }
        $('#txtFocus').focus();
        resetForm();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };
})