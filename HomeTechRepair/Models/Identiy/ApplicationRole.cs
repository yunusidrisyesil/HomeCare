using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Models.Identiy
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {








        }
        public ApplicationRole(string name)
        {
            this.Name = name;
        }
    }
}
