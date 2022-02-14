using Microsoft.AspNetCore.Identity;

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
