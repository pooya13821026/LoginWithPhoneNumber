using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class ConfrimViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "کد تایید")]
        [Required]
        public string Code { get; set; }
    }
}
