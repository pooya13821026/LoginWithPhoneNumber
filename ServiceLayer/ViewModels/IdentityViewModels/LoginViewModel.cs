using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]
        [Required]
        [StringLength(25, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; } = false;
    }
}
