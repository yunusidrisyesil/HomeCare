using HomeTechRepair.Models.Identiy;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTechRepair.Models.Entities
{
    public class Address : BaseEntity
    {
        public string UserId { get; set; }
        public int StateId { get; set; }
		public string? DoorNo { get; set; }
        public string? StreetName { get; set; }
        public string? BuildingNo { get; set; }
        public string? Line { get; set; }
        public string? Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
