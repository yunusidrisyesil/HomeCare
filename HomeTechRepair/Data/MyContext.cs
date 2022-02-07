using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeTechRepair.Data
{
    public class MyContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ReciptDetail>()
                .HasKey(c => new { c.ServiceId, c.ReciptMasterId }).HasName("AltarnateKey_ReciptServiceId");
        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<SupportTicket> SupportTickets { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ReciptMaster> ReciptMasters { get; set; }
        public virtual DbSet<ReciptDetail> ReciptDetails { get; set; }
        public virtual DbSet<Service> Services { get; set; }
    }
}
