using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class RegsisterViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string DisplayName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required]
        [StringLength(25, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required]
        [StringLength(25, MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "کلمه رمز عبور با تکرار ان یکی نیست")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
