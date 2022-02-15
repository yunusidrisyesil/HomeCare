using System;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int StateId { get; set; }
        [Display(Name = "Door No")]
        public string? DoorNo { get; set; }
        [Display(Name = "Street Name")]
        public string? StreetName { get; set; }
        [Display(Name = "Building No")]
        public string? BuildingNo { get; set; }
        [Display(Name = "Line")]
        public string? Line { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
