using System;
using System.Collections.Generic;

namespace HomeTechRepair.Models.Entities
{
	public class ReciptMaster
	{
		public Guid Id { get; set; }
		public double TotalAmount { get; set; }
		public DateTime Date { get; set; } = DateTime.UtcNow;
		public List<ReciptDetail> ReciptDetails { get; set; }
		//TODO UserId and connection
	}
}
