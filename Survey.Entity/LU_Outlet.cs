﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Entity
{
    public class LU_Outlet
    {
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string ContactNo { get; set; }
		public int CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public int ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
