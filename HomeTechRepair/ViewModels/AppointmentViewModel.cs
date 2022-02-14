using System;

namespace HomeTechRepair.ViewModels
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? ResolutionDate { get; set; }
        public string? DoctorId { get; set; }
        public bool isActive { get; set; }
    }
}
