using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class ForgotPassViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        [StringLength(11, ErrorMessage = "{0} باید 11 کاراکتر باشد.", MinimumLength = 11)]
        public string PhoneNumber { get; set; }
    }
}