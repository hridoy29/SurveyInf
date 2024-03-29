﻿app.controller("SideBarController", function ($scope, $cookieStore, $window, $location) {
    $scope.loginUserSb = [];
    $scope.loginUserSb = $cookieStore.get('UserData');
    $scope.dashboardPermission = { CanView: true};
    $scope.securityMenuView = true;
    $scope.settingMenuView = true;
    $scope.setupMenuView = true;
    $scope.reportMenuView = true;

    $scope.permissionList = JSON.parse($window.localStorage.getItem('permissionList'));

    $scope.dashboardPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Dashboard'").FirstOrDefault();
    $scope.userPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'User'").FirstOrDefault();
    $scope.userGroupPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'User Group'").FirstOrDefault();
    $scope.departmentPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Department'").FirstOrDefault();
    $scope.permissionPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Permission'").FirstOrDefault();
    $scope.changePassPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Change Password'").FirstOrDefault();
    $scope.schemeNumPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Scheme Number'").FirstOrDefault();
    $scope.assetConfigPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Asset Config'").FirstOrDefault();
    $scope.outletTypePermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Outlet Type'").FirstOrDefault();
    $scope.commentTypePermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Comment Type'").FirstOrDefault();
    $scope.OutLetPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'outlet'").FirstOrDefault();
    $scope.AICPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'AIC'").FirstOrDefault();
    $scope.ASMPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'ASM'").FirstOrDefault();
    $scope.DistributorlistPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Distributor list'").FirstOrDefault();
    $scope.issuesPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Issues'").FirstOrDefault();
    $scope.itemsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Item'").FirstOrDefault();
    $scope.distributorsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Distributor'").FirstOrDefault();
    $scope.observationsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Observation'").FirstOrDefault();
    $scope.bbdsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'BBD'").FirstOrDefault();
    $scope.identitiesPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Identity'").FirstOrDefault();
    $scope.categoryPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Category'").FirstOrDefault();
    $scope.itemGroupPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Item Group'").FirstOrDefault();
    $scope.coolerPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Cooler'").FirstOrDefault();
    $scope.mdoPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'MDO'").FirstOrDefault();
    $scope.shortNotePermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Short Note'").FirstOrDefault();
    $scope.commentPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Comment'").FirstOrDefault();
    $scope.surveyReportsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Survey Reports'").FirstOrDefault();
    $scope.AssetConfigReportsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Asset Config Reports'").FirstOrDefault();
    $scope.questionnaireReportsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Questionnaire Reports'").FirstOrDefault();
    $scope.questionnaireDetailsReportsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Questionnaire Details Reports'").FirstOrDefault();
    $scope.questionnairePhysicalStocksReportPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Questionnaire Physical Stocks Reports'").FirstOrDefault();
    $scope.questionnaireObservationReportsPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Questionnaire Observation Reports'").FirstOrDefault();
    $scope.hygienePhysicalStocksReportPermission = Enumerable.From($scope.permissionList).Where("$.ScreenName === 'Hygiene Physical Stock Report'").FirstOrDefault();

    if (!$scope.userPermission.CanView && !$scope.userGroupPermission.CanView && !$scope.permissionPermission.CanView && !$scope.changePassPermission.CanView && !$scope.departmentPermission.CanView ) {
        $scope.securityMenuView = false;
    }

    if (!$scope.schemeNumPermission.CanView && !$scope.assetConfigPermission.CanView) {
        $scope.settingMenuView = false;
    }

    if (!$scope.OutLetPermission.CanView && !$scope.outletTypePermission.CanView && !$scope.commentTypePermission.CanView && !$scope.commentPermission.CanView && !$scope.AICPermission.CanView && !$scope.ASMPermission.CanView && !$scope.DistributorlistPermission.CanView && !$scope.itemsPermission.CanView && !$scope.issuesPermission.CanView && !$scope.itemGroupPermission.CanView && !$scope.categoryPermission.CanView && !$scope.bbdsPermission.CanView && !$scope.identitiesPermission.CanView && !$scope.observationsPermission.CanView && !$scope.distributorsPermission.CanView && !$scope.coolerPermission.CanView && !$scope.shortNotePermission.CanView && !$scope.mdoPermission.CanView) {
        $scope.setupMenuView = false;
    }

    if (!$scope.surveyReportsPermission.CanView && !$scope.questionnaireReportsPermission.CanView && !$scope.questionnaireDetailsReportsPermission.CanView && !$scope.questionnairePhysicalStocksReportPermission.CanView && !$scope.questionnaireObservationReports.CanView && !$scope.hygienePhysicalStocksReportPermission.CanView && !$scope.AssetConfigReportsPermission.CanView) {
        $scope.reportMenuView = false;
    }

    ResetMenuCSS();

    var path = $location.path();
    if (path === '/Home')
        $scope.isHome = true;

    else if (path === '/Admission' || path === '/Program' || path === '/Registration' || path === '/Semester')
        $scope.isSecurity = true;

    else if (path === '/Course')
		$scope.isSettings = true;
    
    else if (path === '/Attendance'|| path === '/ExamResult')
        $scope.isSetup = true;

    else if (path === '/SurveyReports')
        $scope.isReports = true;

    else
        $scope.isHome = true;

    function ResetMenuCSS() {
        $scope.isHome = false;

        $scope.isSecurity = false;
        $scope.isSecurityUser = false;
        $scope.isSecurityUserGroup = false;
        $scope.isSecurityDepartment = false;
        $scope.isSecurityPermission = false;
        $scope.isSecurityChangePass = false;

        $scope.isSettings = false;
		$scope.isSettingsSchemeNumber = false;
        $scope.isSettingsAssetConfig = false;

        $scope.isSetup = false;
        $scope.isSetupOutletType = false;
        $scope.isSetupCommentType = false;
        $scope.isSetupAIC = false;
        $scope.isSetupASM = false;
        $scope.isSetupDistributorlist = false;
        $scope.isSetupBBDs = false;
        $scope.isSetupIdentity = false;
        $scope.isSetupItems = false;
        $scope.isSetupIssues = false;
        $scope.isSetupDistributors = false;
        $scope.isSetupObservations = false;
        $scope.isSetupCategory = false;
        $scope.isSetupItemGroup = false;
        $scope.isSetupCooler = false;
        $scope.isSetupMDO = false;
        $scope.isSetupShortNote = false;
        $scope.isAssetConfigReports = false;
        $scope.isSetupComment  = false;
        $scope.isReports = false;
        $scope.isReportsSurveyReports = false;
        $scope.isQuestionnaireReports = false;
        $scope.isQuestionnaireDetailsReports = false;
        $scope.isQuestionnairePhysicalStocksReport = false;
        $scope.isHygienePhysicalStocksReport = false;
        $scope.isQuestionnaireObservationReports = false;
        $scope.isSetupOutLet = false;
    };

    $scope.setActiveMenu = function (menu) {
        ResetMenuCSS();

        if (menu === 'home')
            $scope.isHome = true;

        else if (menu === 'user') {
            $scope.isSecurity = true;
            $scope.isSecurityUser = true;
        }
        else if (menu === 'outlet') {
            $scope.isSetup = true;
            $scope.isSetupOutLet = true;
        }
        else if (menu === 'userGroup') {
            $scope.isSecurity = true;
            $scope.isSecurityUserGroup = true;
        }
        else if (menu === 'department') {
            $scope.isSecurity = true;
            $scope.isSecurityDepartment = true;
        }
        else if (menu === 'permission') {
            $scope.isSecurity = true;
            $scope.isSecurityPermission = true;
        }
        else if (menu === 'changePass') {
            $scope.isSecurity = true;
            $scope.isSecurityChangePass = true;
        }

        else if (menu === 'schemeNumber') {
            $scope.isSettings = true;
            $scope.isSettingsSchemeNumber = true;
        }
        else if (menu === 'assetConfig') {
            $scope.isSettings = true;
            $scope.isSettingsAssetConfig = true;
        }

        else if (menu === 'outletType') {
            $scope.isSetup = true;
            $scope.isSetupOutletType = true;
        }
        else if (menu === 'commentType') {
            $scope.isSetup = true;
            $scope.isSetupCommentType = true;
        }
        else if (menu === 'AIC') {
            $scope.isSetup = true;
            $scope.isSetupAIC = true;
        }
        else if (menu === 'ASM') {
            $scope.isSetup = true;
            $scope.isSetupASM = true;
        }
        else if (menu === 'Distributorlist') {
            $scope.isSetup = true;
            $scope.isSetupDistributorlist = true;
        }

        else if (menu === 'items') {
            $scope.isSetup = true;
            $scope.isSetupItems = true;
        }
        else if (menu === 'distributors') {
            $scope.isSetup = true;
            $scope.isSetupDistributors = true;
        }
        else if (menu === 'issues') {
            $scope.isSetup = true;
            $scope.isSetupIssues = true;
        }
        else if (menu === 'observations') {
            $scope.isSetup = true;
            $scope.isSetupObservations = true;
        }
        else if (menu === 'bbds') {
            $scope.isSetup = true;
            $scope.isSetupBBDs = true;
        }
        else if (menu === 'identities') {
            $scope.isSetup = true;
            $scope.isSetupIdentity = true;
        }
        else if (menu === 'itemGroup') {
            $scope.isSetup = true;
            $scope.isSetupItemGroup = true;
        }
        else if (menu === 'cooler') {
            $scope.isSetup = true;
            $scope.isSetupCooler = true;
        }
        else if (menu === 'shortNote') {
            $scope.isSetup = true;
            $scope.isSetupShortNote = true;
        }
        else if (menu === 'AssetConfigReports') {
            $scope.isReports = true;
            $scope.isAssetConfigReports = true;
        }
        else if (menu === 'MDO') {
            $scope.isSetup = true;
            $scope.isSetupMDO = true;
        }
        else if (menu === 'category') {
            $scope.isSetup = true;
            $scope.isSetupCategory = true;
        }
        else if (menu === 'comment') {
            $scope.isSetup = true;
            $scope.isSetupComment = true;
        }

        else if (menu === 'surveyReports') {
            $scope.isReports = true;
            $scope.isReportsSurveyReports = true; 
        }
        else if (menu === 'questionnaireReports') {
            $scope.isReports = true;
            $scope.isQuestionnaireReports = true;
        }
        else if (menu === 'questionnaireDetailsReports') {
            $scope.isReports = true;
            $scope.isQuestionnaireDetailsReports = true;
        }
        else if (menu === 'questionnairePhysicalStocksReport') {
            $scope.isReports = true;
            $scope.isQuestionnairePhysicalStocksReport = true;
        } 
        else if (menu === 'questionnaireObservationReports') {
            $scope.isReports = true;
            $scope.isQuestionnaireObservationReports = true;
        }
        else if (menu === 'hygienePhysicalStocksReport') {
            $scope.isReports = true;
            $scope.isHygienePhysicalStocksReport = true;
        }
    };
});