using System;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.Models.Entities
{
    public class SupportTicket
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ResolutionDate { get; set; }

        //TODO UserID, Appointment connection
    }
}
