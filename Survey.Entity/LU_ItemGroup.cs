using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_ItemGroup
	{
		public Int32 Id { get; set; }
		public string GroupName { get; set; }
		public Int32 CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
