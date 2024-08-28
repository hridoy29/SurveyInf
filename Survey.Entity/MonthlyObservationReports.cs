using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class MonthlyObservationReports
    {
        public List<MonthlyObservationReports_Get_distributor_count> monthlyObservationReports_Get_Distributor_Count { get; set; }
        public List<MonthlyObservationReports_Get_distributorInfo_for_Stock_variation> monthlyObservationReports_Get_DistributorInfo_For_Stock_Variation { get; set; }
        public List<MonthlyObservationReports_Get_distributor_CCBBL_Observation> monthlyObservationReports_Get_Distributor_CCBBL_Observations { get; set; }
        public List<MonthlyObservationReports_Get_distributor_Info> monthlyObservationReports_Get_Distributor_Infos  { get; set; }
       
    }
}
