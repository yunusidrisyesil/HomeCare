using HomeTechRepair.Models.Entities;
using System;
using System.Collections.Generic;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class CloseTicketViewModel
    {

        public Guid ReciptMasterId { get; set; }
        public Guid SupportTicketId { get; set; }
        //public double TotalPrice { get; set; }
    }
}
