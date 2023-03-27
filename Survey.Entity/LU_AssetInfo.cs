using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class LU_AssetInfo
    {
		public int Id { get; set; }
		public string AssetNumber { get; set; }
		public string SerialNumber { get; set; }
		public int DistributorId { get; set; }
		public int AICId { get; set; }
		public int ASMId { get; set; }
		public int MDOId { get; set; }
		public int OutletId { get; set; }
		public int CoolerId { get; set; }
		public bool IsCancelled { get; set; }
		public int CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public int ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }

		public string DistributorName { get; set; }
		public string AICName { get; set; }
		public string ASMName { get; set; }
		public string MDOName { get; set; }
		public string OutletName { get; set; }
		public string CoolerName { get; set; }
	}
}
