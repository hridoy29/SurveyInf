app.controller("MonthlyObservationReportsCtrl", function ($scope, $cookieStore, $window, $location, $filter, $http, blockUI) {

    $scope.DefaultPerPage = 10;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    $scope.distributorList = [];
    //$scope.distributorDetails = [];
    //getProgramHead();
    clear();


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

    $scope.getList = function () {
        $scope.toDate = document.getElementById("toDate").value;
        var params = JSON.stringify({ todate: $scope.toDate});
        $http({
            url: "/MonthlyObservationReports/Get_Distributor_Count",
            method: 'Get',
            params: { todate: $scope.toDate },
            headers: { 'Content-Type': 'application/json' }

        }).success(function (data) {

            if (data) {
                $scope.lsitBlock.stop();
                $scope.entityList = data;
                populateObservationList($scope.entityList);
               // populateDistributorInfoObservationList($scope.entityList)
                $scope.distributorList = data.monthlyObservationReports_Get_Distributor_Infos;           

            }
            else {
                $scope.lsitBlock.stop();
                //alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }

        }).error(function (data2) {
            $scope.lsitBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
        //$scope.lsitBlock.start();
        //$scope.toDate = document.getElementById("toDate").value;
        //var params = JSON.stringify({ todate: $scope.toDate, distributorId: $scope.distributorId });
        
    };

    function populateObservationList(entityList) {
        const observationList = document.getElementById('observationList');
        observationList.innerHTML = '';
        entityList.monthlyObservationReports_Get_Distributor_CCBBL_Observations.forEach(report => {
            const listItem = document.createElement('li');
            if (report.ObservationId == 2 || report.ObservationId == 4 ) {
                listItem.textContent = `${report.percentage}% ${report.ObservationText} (${report.distributor_selection}).`;

            } else {
                listItem.textContent = `${report.ObservationText} (${report.distributor_selection}).`;
            }
            
            observationList.appendChild(listItem);
        });
    }

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