using HomeTechRepair.Models.Entities;
using System;

namespace HomeTechRepair.ViewModels
{
    public class UserTicketViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public string DoctorName { get; set; }

    }
}
