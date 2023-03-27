using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class LU_Asset_Config
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
		public bool IsCancelled { get; set; }
		public bool IsRepeated { get; set; }
		public bool IsUpdated { get; set; }
		public string Device_Number { get; set; }
		public int CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public int ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
	}
}
