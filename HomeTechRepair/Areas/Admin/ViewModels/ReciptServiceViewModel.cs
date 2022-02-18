using System;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class ReciptServiceViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
