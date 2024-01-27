using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Identity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public bool ConfrimPhoneNumber { get; set; }
        public string ConfrimCode { get; set; }
        public DateTime ConfrimCodeCreateDate { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public DateTime RegisterTimea { get; set; }
        public bool IsDeleted { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<UserRole> UserRole { get; set; }
    }
}
