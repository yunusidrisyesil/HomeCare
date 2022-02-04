using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Name")]
		[StringLength(25,ErrorMessage = "Name cannot exceed 25 characters")]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Surname")]
		[StringLength(25, ErrorMessage = "Surname cannot exceed 25 characters")]
		public string Surname { get; set; }
		[Required(ErrorMessage = "Email cannot be empty")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password cannot be empty")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password cannot be empty")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare(nameof(Password),ErrorMessage ="Passwords don't match")]
		public string ConfirmPassword { get; set; }
	}
}
