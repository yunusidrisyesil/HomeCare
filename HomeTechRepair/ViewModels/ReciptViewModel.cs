using System;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class ReciptViewModel
    {
        public bool hasitems { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public double ServicePrice { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public Guid ServiceId{ get; set; }
        public bool isPaid { get; set; }
    }
}
