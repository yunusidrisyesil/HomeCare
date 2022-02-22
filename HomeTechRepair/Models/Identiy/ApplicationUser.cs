using HomeTechRepair.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.Models.Identiy
{
    public class ApplicationUser : IdentityUser
    {
        //TODO BirthDay
        [Required, StringLength(50)]
        [PersonalData]
        public string Name { get; set; }
        [Required, StringLength(50)]
        [PersonalData]
        public string Surname { get; set; }
        //[PersonalData]
        //public DateTime BirthDay { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        //public bool isDeleted { get; set; } = false;
        public List<SupportTicket> SupportTickets { get; set; }
        public List<ReciptMaster> ReciptMasters { get; set; }

        //This property is needed if we decide to implement a sms service in the future //https://www.twilio.com/docs/sms/quickstart/csharp-dotnet-core
        //public bool NotificationPreference { get; set; } //True : mail False: sms

    }
}
