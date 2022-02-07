using System.Collections.Generic;

namespace HomeTechRepair.Models
{
    public class RoleModels
    {
        public static string Admin = "Admin";
        public static string Operator = "Operator";
        public static string Technician = "Technician";
        public static string User = "User";
        public static string Passive = "Passive";

        public static ICollection<string> Roles => new List<string>() { Admin, Operator, Technician, User, Passive };
    }
}
