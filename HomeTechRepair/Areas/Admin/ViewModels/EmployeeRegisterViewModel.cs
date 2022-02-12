using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
	public class EmployeeRegisterViewModel
	{
		[Required]
		[Display(Name = "Name")]
		[StringLength(25)]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Surname")]
		[StringLength(25)]
		public string Surname { get; set; }
		[Required(ErrorMessage = "Email cannot be empty")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password cannot be empty")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Display(Name ="Roles Type")]
		public List<DropdownViewModel> Roles { get; set; }
		[Required]
        public string RoleName { get; set; }


    }
}
