using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class TRN_SurveyReports_Export_Infinigent_Formate
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public string DistributorName { get; set; }
        public Int32 GccCode { get; set; }
        public string MobileNumber { get; set; }
        public string OutlateTypeId { get; set; }
        public string VisitedDate { get; set; }
        public string OutlateName { get; set; }
        public string RetailSellerName { get; set; }
        public string ASMName { get; set; }
        public string AICName { get; set; }
        public string OutlateAddress { get; set; }

        public string IsKnowenAboutScheme { get; set; }
        public string SchemeDetails { get; set; }

        public string IsFacilitatedByScheme { get; set; }
        public string DateOfScheme { get; set; }
        public string IsWrittenRecordAvailable { get; set; }
        public string LatestChallanDate { get; set; }
        public string ChallanAmount { get; set; }
        public string DoesGotAnyChallan { get; set; }
        public Int32? ChallanTypeId { get; set; }
        public string DoesExpiredProductAvailable { get; set; }
        public string DoesSatisfiedWithSallesOfficer { get; set; }
        public string DoesSatisfiedWithProductOrderAndService { get; set; }
        public string SallesOfficerVisitingDay { get; set; }
        public string DoesGotLatestDiscountOffer { get; set; }
        public string WillGetAnyDiscountOfferFromDistributor { get; set; }
        public string DoesCocaColaLabelAvailable { get; set; }
        public string IsGccCodeAvailable { get; set; }
        public string CommentsType { get; set; }
        public string Comments { get; set; }
        public string CommentDetails { get; set; }
    }
}
