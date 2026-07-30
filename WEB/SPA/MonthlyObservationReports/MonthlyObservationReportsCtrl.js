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
    $scope.ExportBtnDisabled = true;
    $scope.toDateConvertedStr = "";
    //$scope.distributorDetails = [];
    //getProgramHead();
    //clear();

    $scope.onPageChange = function (currentpage) {
        $scope.currentPage = currentpage;
        var begin = ($scope.PerPage * ($scope.currentPage - 1));
        var end = begin + $scope.PerPage;
        var total_page = $scope.entityList.length / $scope.DefaultPerPage;
        $scope.entityListPaged = $scope.entityList.slice(begin, end);
    }

    $scope.getList = function () {
        $scope.toDate = document.getElementById("toDate").value;

        if ($scope.toDate == undefined || $scope.toDate == null || $scope.toDate == "" ) {

            alertify.log('Please Select Valid Date !', 'error', '20000');
        }
        else {
            $scope.toDateConvertedStr = String($scope.toDate)

            $scope.toDateConvertedStr = $scope.toDateConvertedStr.replace("/", " ");

            var params = JSON.stringify({ todate: $scope.toDate });
            $http({
                url: "/MonthlyObservationReports/Get_Distributor_Count",
                method: 'Get',
                params: { todate: $scope.toDate },
                headers: { 'Content-Type': 'application/json' }

            }).success(function (data) {

                if (data) {
                    alertify.log('Data Generated Sucessfully', 'success', '20000');
                    $scope.ExportBtnDisabled = false
                    $scope.lsitBlock.stop();
                    $scope.entityList = data;
                    populateObservationList($scope.entityList);
                    // populateDistributorInfoObservationList($scope.entityList)
                    $scope.distributorList = data.monthlyObservationReports_Get_Distributor_Infos;
                    $scope.toDate = "";

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
        }
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

    //working

    $scope.Export2Word = function (element, filename = '') {
        var preHtml = `
        <html xmlns:o='urn:schemas-microsoft-com:office:office' 
              xmlns:w='urn:schemas-microsoft-com:office:word' 
              xmlns='http://www.w3.org/TR/REC-html40'>
        <head>
            <meta charset='utf-8'>
            <title>Export HTML To Doc</title>
        <style>
            @page {
                @bottom-center {
                    content: "Page " counter(page) " of " counter(pages);
                    font-family: Arial;
                    font-size: 12px;
                }
            }
        </style>
        </head>
        <body>`;
        var postHtml = "</body></html>";

        // Get the HTML content of the element
        var elementContent = document.getElementById(element);
        var clonedElement = elementContent.cloneNode(true); // Clone to avoid modifying the original DOM

         //Process all images to embed and scale them
        var images = clonedElement.querySelectorAll('img');
        var imageConversionPromises = Array.from(images).map(img => {
            return new Promise((resolve) => {
                var imgSrc = img.src;

                // Skip if the image is already in base64
                if (imgSrc.startsWith('data:image')) {
                    // Add Word-specific scaling attributes
                    img.outerHTML = `<img src="${imgSrc}" style="max-width:100%; height:auto;" 
                                  width="500" height="auto">`;
                    resolve();
                    return;
                }

                // Load the image to convert it to base64
                var image = new Image();
                image.crossOrigin = 'Anonymous'; // Handle cross-origin issues
                image.src = imgSrc;

                image.onload = function () {
                    var canvas = document.createElement('canvas');
                    canvas.width = image.width;
                    canvas.height = image.height;

                    var context = canvas.getContext('2d');
                    context.drawImage(image, 0, 0);

                    // Convert to base64
                    var base64Image = canvas.toDataURL('image/png');
                    img.outerHTML = `<img src="${base64Image}" style="max-width:100%; height:auto;" 
                                  width="625" height="auto" align="right">`; // Width fixed to 400px
                    resolve();
                };

                image.onerror = function () {
                    console.error('Failed to load image: ', imgSrc);
                    resolve();
                };
            });
        });

        // Wait for all image conversions to complete
        Promise.all(imageConversionPromises).then(() => {
            // Generate final HTML with processed images
            var html = preHtml + clonedElement.innerHTML + postHtml;

            var blob = new Blob(['\ufeff', html], {
                type: 'application/msword'
            });

            // Specify file name
            filename = filename ? filename + '.doc' : 'MonthlyObservationReport.doc';

            // Create download link element
            var downloadLink = document.createElement("a");

            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                var url = URL.createObjectURL(blob);
                downloadLink.href = url;
                downloadLink.download = filename;
                downloadLink.click();
                URL.revokeObjectURL(url); // Clean up the URL object
            }

            document.body.removeChild(downloadLink);
        });
    };

    //$scope.Export2Word = function (element, filename = '') {
    //    // Convert your image to Base64 (replace with your actual Base64 string)
    //    var base64Image = "data:image/png;base64,iVBORw0..."; // Use a valid Base64 string here

    //    var preHtml = `
    //<html xmlns:o='urn:schemas-microsoft-com:office:office' 
    //      xmlns:w='urn:schemas-microsoft-com:office:word' 
    //      xmlns='http://www.w3.org/TR/REC-html40'>
    //<head>
    //    <meta charset='utf-8'>
    //    <title>Export HTML To Doc</title>
    //    <style>
    //        @page {
    //            mso-header-space: 1in;
    //        }
    //        div.Section1 {
    //            page: Section1;
    //        }
    //    </style>
    //    <xml>
    //        <w:WordDocument>
    //            <w:View>Print</w:View>
    //            <w:Zoom>100</w:Zoom>
    //            <w:DoNotOptimizeForBrowser/>
    //        </w:WordDocument>
    //    </xml>
    //</head>
    //<body>
    //    <div class="Section1">
    //        <!-- Header -->
    //        <div style="mso-element:header">
    //            <p style="text-align: center;">
    //                <img src="${base64Image}" style="max-width: 100%; height: auto;" />
    //            </p>
    //        </div>

    //        <!-- Content -->
    //        <div>`;

    //    var postHtml = `
    //        </div>
    //    </div>
    //</body>
    //</html>`;

    //    // Grab the content of the element
    //    var elementContent = document.getElementById(element);
    //    if (!elementContent) {
    //        console.error("Element not found:", element);
    //        return;
    //    }

    //    var clonedElement = elementContent.cloneNode(true);
    //    var html = preHtml + clonedElement.innerHTML + postHtml;

    //    // Create the Word document
    //    var blob = new Blob(['\ufeff', html], {
    //        type: 'application/msword'
    //    });

    //    filename = filename ? filename + '.doc' : 'Document.doc';
    //    var downloadLink = document.createElement("a");

    //    document.body.appendChild(downloadLink);

    //    if (navigator.msSaveOrOpenBlob) {
    //        navigator.msSaveOrOpenBlob(blob, filename);
    //    } else {
    //        var url = URL.createObjectURL(blob);
    //        downloadLink.href = url;
    //        downloadLink.download = filename;
    //        downloadLink.click();
    //        URL.revokeObjectURL(url);
    //    }

    //    document.body.removeChild(downloadLink);
    //};

    //$scope.Export2Word = function (element, filename = '') {
    //    // Convert your image to Base64 (replace with your actual Base64 string)
    //    var base64Image = "data:image/png;base64,iVBORw0..."; // Use a valid Base64 string here

    //    var preHtml = `
    //<html xmlns:o='urn:schemas-microsoft-com:office:office' 
    //      xmlns:w='urn:schemas-microsoft-com:office:word' 
    //      xmlns='http://www.w3.org/TR/REC-html40'>
    //<head>
    //    <meta charset='utf-8'>
    //    <title>Export HTML To Doc</title>
    //    <style>
    //        @page {
    //            mso-header-space: 1in;
    //        }
    //        div.Section1 {
    //            page: Section1;
    //        }
    //        h1 {
    //            page-break-before: always;
    //        }
    //    </style>
    //    <xml>
    //        <w:WordDocument>
    //            <w:View>Print</w:View>
    //            <w:Zoom>100</w:Zoom>
    //            <w:DoNotOptimizeForBrowser/>
    //        </w:WordDocument>
    //    </xml>
    //</head>
    //<body>
    //    <div class="Section1">
    //        <!-- Header -->
    //        <div style="mso-element:header">
    //            <p style="text-align: center;">
    //                <img src="${base64Image}" style="max-width: 100%; height: auto;" />
    //            </p>
    //        </div>

    //        <!-- Table of Contents -->
    //        <div>
    //            <h1>Table of Contents</h1>
    //            <p style="mso-element:field-begin"></p>
    //            TOC \\o "1-3" \\h \\z \\u
    //            <p style="mso-element:field-separate"></p>
    //            <p style="mso-element:field-end"></p>
    //        </div>

    //        <!-- Main Content -->
    //        <div>`;

    //    var postHtml = `
    //        </div>
    //    </div>
    //</body>
    //</html>`;

    //    // Grab the content of the element
    //    var elementContent = document.getElementById(element);
    //    if (!elementContent) {
    //        console.error("Element not found:", element);
    //        return;
    //    }

    //    var clonedElement = elementContent.cloneNode(true);
    //    var html = preHtml + clonedElement.innerHTML + postHtml;

    //    // Create the Word document
    //    var blob = new Blob(['\ufeff', html], {
    //        type: 'application/msword'
    //    });

    //    filename = filename ? filename + '.doc' : 'Document.doc';
    //    var downloadLink = document.createElement("a");

    //    document.body.appendChild(downloadLink);

    //    if (navigator.msSaveOrOpenBlob) {
    //        navigator.msSaveOrOpenBlob(blob, filename);
    //    } else {
    //        var url = URL.createObjectURL(blob);
    //        downloadLink.href = url;
    //        downloadLink.download = filename;
    //        downloadLink.click();
    //        URL.revokeObjectURL(url);
    //    }

    //    document.body.removeChild(downloadLink);
    //};
});