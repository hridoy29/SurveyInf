using System;
using System.Text;

namespace SurveyEntity
{
	public class TRN_SurveyReports_get
    {
        //public Int32 Id { get; set; }
        public string Number { get; set; }
        
		public string OutlateName { get; set; }
		//public DateTime Date { get; set; }
		public Int32 GccCode { get; set; }
		public string RetailSellerName { get; set; }
		public string MobileNumber { get; set; }
		 
		public string DistributorName { get; set; }
 
		public string OutlateAddress { get; set; }
		public string IsKnowenAboutScheme { get; set; }
		public string SchemeDetails { get; set; }
		//public Int32 SchemeMediaTypeId { get; set; }
		public string IsFacilitatedByScheme { get; set; }
		//public DateTime DateOfScheme { get; set; }
		public string IsWrittenRecordAvailable { get; set; }
		//public DateTime LatestChallanDate { get; set; }
		public string ChallanAmount { get; set; }
		public string DoesGotAnyChallan { get; set; }
		//public Int32? ChallanTypeId { get; set; }
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
        public string ASMName { get; set; }
        public string AICName { get; set; }
        //public SByte CreatorName { get; set; }
        //public DateTime CreationDate { get; set; }
        //public SByte ModifierName { get; set; }
        //public DateTime ModificationDate { get; set; }

        //child
        //public Int32 CId { get; set; }
        //public string CNumber { get; set; }
        public string ImageLocation { get; set; }

        //AuditShopDetails
        //public Int32 Id { get; set; }
        //public string Number { get; set; }
        //public string OutlateName { get; set; }
        //public string GccCode { get; set; }
        //public string RetailSellerName { get; set; }
        //public string MobileNumber { get; set; }
        //public string OutlateTypeId { get; set; }
        //public DateTime VisitedDate { get; set; }
        //public string DistributorName { get; set; }
        //public Int32 AsmId { get; set; }
        //public Int32 AicId { get; set; }
        //public string OutlateAddress { get; set; }


    }
}
