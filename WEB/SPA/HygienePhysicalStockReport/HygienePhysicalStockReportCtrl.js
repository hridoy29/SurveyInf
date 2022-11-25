app.controller("HygienePhysicalStockReportCtrl", function ($scope, $cookieStore, $window, $location, $filter, $http, blockUI) {

    $scope.DefaultPerPage = 10;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    //getProgramHead();
    clear();
    getDistributorList();
  //  getList();
    //function getProgramHead() {

    //    var where = "EmployeeType = 'Faculty'";
    //    $http({
    //        url: '/Program/GetDynamicEmployee?where=' + where + '&orderBy=EmployeeName',
    //        method: 'GET',
    //        headers: { 'Content-Type': 'application/json' }
    //    }).success(function (data) {
    //        if (data.length) {
    //            $scope.employeeList = data;
    //        }
    //    });
    //}
    $scope.getImage = function (number) {

        $http({
            url: '/SurveyReport/GetImageLocation?number=' + number,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.imageLocationList = data;

                angular.forEach(data, function (obj) {
                    window.open('http://api.infinigentconsulting.com' + obj.ImageLocation);
                });
                //window.open('http://202.126.122.85:72/image/201_ICL_100017_1.png');
                //  window.open('http://api.infinigentconsulting.com' + obj.ImageLocation);

            }
        });
    }


    function clear() {
        $scope.entity = { ProgramId: 0, IsActive: true };
        $scope.DistributorName = '-- Select Distributor --';
        $("#txtFocus").focus();
    };

    $scope.onPageChange = function (currentpage) {
        $scope.currentPage = currentpage;
        var begin = ($scope.PerPage * ($scope.currentPage - 1));
        var end = begin + $scope.PerPage;
        var total_page = $scope.entityList.length / $scope.DefaultPerPage;
        $scope.entityListPaged = $scope.entityList.slice(begin, end);
    }

    $scope.getList= function () {
        $scope.lsitBlock.start();
        $scope.toDate = document.getElementById("toDate").value;
        var params = JSON.stringify({ todate: $scope.toDate, distributorId: $scope.distributorId });
        if ($scope.toDate == undefined || $scope.toDate == null) {

            alertify.log('Please Select Valid Date !', 'error', '10000');

        }
        else if ($scope.distributorId == 0 || $scope.distributorId == null || $scope.distributorId == undefined) {

            alertify.log('Please Select Distributor!', 'error', '10000');
        }
        else {
            $http({
                url: "/HygienePhysicalStockReport/Get",
                method: 'Get',
                params: { todate: $scope.toDate, distributorId: $scope.distributorId },
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {

                if (data.length) {
                    $scope.lsitBlock.stop();
                     angular.forEach(data, function (obj) {
                        var res = obj.Date.substring(0, 5);
                        if (res == "/Date") {
                            var parsedDate = new Date(parseInt(obj.Date.substr(6)));
                            var date = ($filter('date')(parsedDate, 'MMM dd, yyyy')).toString();
                            obj.Date = date;
                        }
    
    
                    })
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
        }
    };


    function submitRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType });

        $http.post('/Program/Post', params).success(function (data) {
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

    $scope.getExport = function (entityListPaged) {
     
        $scope.toDate = document.getElementById("toDate").value; 
        $scope.userId = $cookieStore.get('UserID');
        var params = JSON.stringify({ todate: $scope.toDate, distributorId: $scope.distributorId });
        if ($scope.toDate == undefined || $scope.toDate == null) {

            alertify.log('Please Select Valid Date !', 'error', '10000');

        }
        else if ($scope.distributorId == 0 || $scope.distributorId == null || $scope.distributorId == undefined) {

            alertify.log('Please Select Distributor!', 'error', '10000');
        }
        else {
            //var params = JSON.stringify({ userId: 1 });

            $http({
                url: '/QuestionnairePhysicalStocksReport/getExport',
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
    }


    function getDistributorList() {
        $scope.lsitBlock.start();
        $http({
            url: "/Distributor/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();

                $scope.distributorList = data;

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


    $scope.post = function (trnType) {
        var where = "ProgramCode = '" + $scope.entity.ProgramCode + "'";
        if ($scope.entity.ProgramId > 0)
            where += " AND ProgramId <> " + $scope.entity.ProgramId;

        $http({
            url: '/Program/GetDynamic?where=' + where + '&orderBy=ProgramCode',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.ProgramCode + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.ProgramId === 0 ? "INSERT" : "UPDATE";
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
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };

    $scope.Export2Word = function (element, filename = '') {
        var preHtml = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta charset='utf-8'><title>Export HTML To Doc</title></head><body>";
        var postHtml = "</body></html>";
        var html = preHtml + document.getElementById(element).innerHTML + postHtml;

        var blob = new Blob(['\ufeff', html], {
            type: 'application/msword'
        });

        // Specify link url
        var url = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(html);

        // Specify file name
        filename = filename ? filename + '.doc' : 'document.doc';

        // Create download link element
        var downloadLink = document.createElement("a");

        document.body.appendChild(downloadLink);

        if (navigator.msSaveOrOpenBlob) {
            navigator.msSaveOrOpenBlob(blob, filename);
        } else {
            // Create a link to the file
            downloadLink.href = url;

            // Setting the file name
            downloadLink.download = filename;

            //triggering the function
            downloadLink.click();
        }

        document.body.removeChild(downloadLink);
    }
});