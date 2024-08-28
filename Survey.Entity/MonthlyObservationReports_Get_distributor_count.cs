using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class MonthlyObservationReports_Get_distributor_count
    {
        public Int32 issueId { get; set; }
        public string issueDescription { get; set; }
        public Int32 total_distributors { get; set; }
        public Int32 distributor_count { get; set; }
        public string distributor_selection { get; set; }
    }

    public class MonthlyObservationReports_Get_distributor_CCBBL_Observation
    {
        public Int32 Distributor_count { get; set; }
        public Int32 total_count { get; set; }
        public Int32 ObservationId { get; set; }
        public string name { get; set; }
        public Int32 total_distributor_count { get; set; }
        public Decimal percentage { get; set; }
        public string distributor_selection { get; set; }
        public string ObservationText { get; set; }
    }

    public class MonthlyObservationReports_Get_distributor_Info
    {
        public int Id { get; private set; }
        public string DbCode { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ContactPersonName { get; set; }
        public bool IsActive { get; set; }
        public string GccCode { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }

        public List<List<MonthlyObservationReports_Get_distributor_StockDetails>> monthlyObservationReports_Get_Distributor_StockDetails { get; set; }
        public MonthlyObservationReports_Get_distributor_CCBBL_Status_2 monthlyObservationReports_Get_Distributor_CCBBL_Statuses { get; set; }



}

    public class MonthlyObservationReports_Get_distributor_StockDetails
    {
        public int DistributorId { get; private set; }
        public string number { get; set; }
        public int IssueId { get; set; }
        public Decimal PhysicalStock { get; set; }
        public Decimal SystemStock { get; set; }
        public Decimal BBDDamage { get; set; }
        public Decimal Difference { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string IssueText { get; set; }

    }


    public class MonthlyObservationReports_Get_distributor_CCBBL_Status
    {
        public int observationId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class MonthlyObservationReports_Get_distributor_CCBBL_Status_2
    {
        public List<MonthlyObservationReports_Get_distributor_CCBBL_Status> Status_list { get; set; }
        public List<MonthlyObservationReports_Get_distributor_CCBBL_Status> Improvment { get; set; } = new List<MonthlyObservationReports_Get_distributor_CCBBL_Status>();
        public List<MonthlyObservationReports_Get_distributor_CCBBL_Status> Continuing { get; set; } = new List<MonthlyObservationReports_Get_distributor_CCBBL_Status>();
    }
}
