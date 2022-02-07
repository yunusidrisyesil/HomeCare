using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeTechRepair.Data
{
    public class MyContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<SupportTicket> SupportTickets { get; set; }
        public virtual DbSet<SupportTicketAssign> SupportTicketAssigns{ get; set; }
    }
}
