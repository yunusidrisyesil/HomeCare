using HomeTechRepair.Models.Identiy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTechRepair.Models.Entities
{
    public class SupportTicket
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ResolutionDate { get; set; }
        public string? OperatorId { get; set; }
        public string? DoctorId { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        
    }
}
