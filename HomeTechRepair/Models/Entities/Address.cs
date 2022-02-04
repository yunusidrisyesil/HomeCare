using System;

namespace HomeTechRepair.Models.Entities
{
    public class Address : BaseEntity
    {
        //public string UserId { get; set; }
        public int StateId { get; set; }
		public string DoorNo { get; set; }
		public string StreetName { get; set; }
		public string BuildingNo { get; set; }
		public string Line { get; set; }
		public string Description { get; set; }

		//TODO UserId and User connection
	}
}
