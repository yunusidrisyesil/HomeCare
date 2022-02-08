
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password field required")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Your password must have a minimum of 5 characters!")]
        [Display(Name ="New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password repeat field is required.")]
        [DataType(DataType.Password)]
        [Display(Name ="Repeat New Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; }
        public string Code { get; set; } 
        public string UserId { get; set; } //email?

    }
}
