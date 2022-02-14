using System;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class SupportTicketViewModel
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ResolutionDate { get; set; }
        public string? DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Patient { get; set; }
        public bool isActive { get; set; }
    }
}
