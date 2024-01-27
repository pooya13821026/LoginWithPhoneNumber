using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class RestPassViewModel
    {

        [Display(Name = "کد تایید")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Code { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        [StringLength(11, ErrorMessage = "{0} باید 11 کاراکتر باشد.", MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        [StringLength(20, ErrorMessage = "{0} باید بین {2} کاراکتر تا {1} کاراکتر باشد", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        [StringLength(20, ErrorMessage = "{0} باید بین {2} کاراکتر تا {1} کاراکتر باشد", MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن یکی نیست")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

    }
}