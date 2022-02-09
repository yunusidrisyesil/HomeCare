using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.ViewModels
{
    public class UpdatePasswordViewModel
    {

        [Required(ErrorMessage = "Old Password field is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Your password must have a minimum of 5 characters!")]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Old password field is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Your password must have a minimum of 5 characters!")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "New Password field is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat New Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; }


















    }
}
