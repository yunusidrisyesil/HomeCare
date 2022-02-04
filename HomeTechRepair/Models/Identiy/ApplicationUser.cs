using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Models.Identiy
{
    public class ApplicationUser
    {
        [Required, StringLength(50)]
        [PersonalData]
        public string Name { get; set; }
        [Required, StringLength(50)]
        [PersonalData]
        public string SurName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
