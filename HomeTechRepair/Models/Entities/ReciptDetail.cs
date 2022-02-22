using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTechRepair.Models.Entities
{
    public class ReciptDetail
    {
        public Guid ReciptMasterId { get; set; }
        public Guid ServiceId { get; set; }
        public double ServicePrice { get; set; }
        public int Quantity { get; set; } = 1;
        public string? Description { get; set; }

        [ForeignKey(nameof(ReciptMasterId))]
        public virtual ReciptMaster ReciptMaster { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; }
    }
}
