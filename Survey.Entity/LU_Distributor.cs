using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_Distributor
	{

		
		public Int32 Id { get; set; }
		public string DbCode { get; set; }
		public string Name { get; set; }
		public string Mobile { get; set; }
		public string Address { get; set; }
		public string ContactPersonName { get; set; }
		public string GccCode { get; set; }
		public Int32 CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
