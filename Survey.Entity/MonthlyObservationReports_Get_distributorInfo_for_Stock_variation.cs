using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class MonthlyObservationReports_Get_distributorInfo_for_Stock_variation
    {
        public Int32 DistributorId { get; set; }
        public string Name { get; set; }
        public Decimal StockDifference { get; set; }      
        public string DistributorInfo { get; set; }
    }
}
