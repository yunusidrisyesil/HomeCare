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
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool AllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
    }
}
