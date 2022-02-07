using HomeTechRepair.Models.Identiy;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTechRepair.Models.Entities
{
	public class Appointment
	{
		[Key]
		public Guid Id { get; set; }
		public Guid SupportTicketId { get; set; }
		public DateTime AppointmentDate { get; set; }

		[ForeignKey(nameof(SupportTicketId))]
		public virtual SupportTicket SupportTicket { get; set; }
    }
}
