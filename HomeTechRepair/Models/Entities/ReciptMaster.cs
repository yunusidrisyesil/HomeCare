﻿using HomeTechRepair.Models.Identiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeTechRepair.Models.Entities
{
    public class ReciptMaster
    {
        [Key]
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        //public int StateId { get; set; }
        //public string? DoorNo { get; set; }
        //public string? StreetName { get; set; }
        //public string? BuildingNo { get; set; }
        //public string? Line { get; set; }
        //public string? Description { get; set; }
        public List<ReciptDetail> ReciptDetails { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}
