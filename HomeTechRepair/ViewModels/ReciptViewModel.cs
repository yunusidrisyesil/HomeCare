using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class ReciptViewModel
    {
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }




    }
}
