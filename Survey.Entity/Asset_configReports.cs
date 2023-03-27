using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class Asset_configReports
    {
		public int Id { get; set; }
		public string AssetNumber { get; set; }
		public string SerialNumber { get; set; }
		public string DistributorId { get; set; }
		public string DistributorName { get; set; }
		public string AICName { get; set; }
		public string ASMName { get; set; }
		public string MDOId { get; set; }
		public string MDOName { get; set; }
		public string OutletId { get; set; }
		public string OutletName { get; set; }
		public string OutletAddress { get; set; }
		public string ContactNo { get; set; }
		public string CoolerModel { get; set; }
		public bool NightCover { get; set; }
        public string ShortNote { get; set; }
		public string Remarks { get; set; }
		public string ShopImage { get; set; }
		public string AssetImage { get; set; }
		public string SignImage { get; set; }
		public string CoolerImage { get; set; }
	}
}
