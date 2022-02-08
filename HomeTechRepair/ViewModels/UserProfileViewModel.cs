using System.ComponentModel.DataAnnotations;

namespace HomeTechRepair.ViewModels
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı alanı gereklidir.")]
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Ad alanı gereklidir.")]
        [Display(Name ="Ad")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Soyad alanı gereklidir.")]
        [Display(Name ="Soyad")]
        public string Surname { get; set; }
        [Required(ErrorMessage ="E-posta alanı gereklidir.")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
