using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_Item
	{
		public Int32 Id { get; set; }
		public string ItemCode { get; set; }
		public string Name { get; set; }
		public Int32 ItemGroupId { get; set; }
		public Int32 CategoryId { get; set; }
		public Int32 CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsActive { get; set; }

		public string GroupName { get; set; }
		public string CategoryName { get; set; }
	}
}
