using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
    public class TicketViewModel
    {
        [Required]
        [StringLength(120)]
        public string Description { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }

    }
}
