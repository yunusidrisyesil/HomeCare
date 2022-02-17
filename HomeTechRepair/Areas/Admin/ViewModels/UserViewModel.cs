using HomeTechRepair.Models.Identiy;
using System;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }
    }
}
