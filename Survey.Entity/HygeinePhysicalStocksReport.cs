using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class HygeinePhysicalStocksReport
    {

       
        public int Id { get; set; }
        public string DbCode { get; set; }
        public string DistributorName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ContactPersonName { get; set; }
        public string ItemName { get; set; }
        public string ItemGroup { get; set; }
        public decimal SystemStock { get; set; }
        public decimal PhysicalStock { get; set; }
        public decimal BBDDamage { get; set; }
        public decimal Difference { get; set; }
       
        public string Issue { get; set; }

    }
    public class IssueWisePhysicalStocksReport
    {
        public List<HygeinePhysicalStocksReport> IssueWisePhysicalStockList{ get; set; }
        public string DbCode { get; set; }
        public string DistributorName { get; set; }
        public string ItemGroup { get; set; }
        public decimal SystemStockSum { get; set; }
        public decimal PhysicalStockSum { get; set; }
        public decimal BBDDamageSum { get; set; }
        public decimal DifferenceSum { get; set; }
    }
}
