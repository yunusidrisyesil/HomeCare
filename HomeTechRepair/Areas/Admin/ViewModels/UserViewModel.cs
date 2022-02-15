﻿using System;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MyProperty { get; set; }
    }
}
