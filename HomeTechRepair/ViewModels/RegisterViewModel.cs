using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Name")]
		[StringLength(25)]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Surname")]
		[StringLength(25)]
		public string Surname { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare(nameof(Password),ErrorMessage ="Passwords don't match")]
		public string ConfirmPassword { get; set; }
	}
}
