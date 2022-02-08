using HomeTechRepair.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
    public class TicketViewModel
    {
        [Required]
        [StringLength(120)]
        public string Description { get; set; }
        //public Address Address { get; set; }
    }
}
