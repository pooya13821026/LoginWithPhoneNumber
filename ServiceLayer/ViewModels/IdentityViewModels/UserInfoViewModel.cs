using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class UserInfoViewModel
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime RegisterTime { get; set; }
        public string PhoneNumber { get; set; }
    }
}
