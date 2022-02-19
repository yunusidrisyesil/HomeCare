using System;
using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage ="Name space cannot be blank.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname space cannot be blank.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email space cannot be blank.")]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }

    }
}
