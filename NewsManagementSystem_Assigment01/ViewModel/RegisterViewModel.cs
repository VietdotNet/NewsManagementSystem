using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem_Assigment01.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Key]
        [Display(Name = "ID")]
        public short AccountId { get; set; }
        [Required]
        [Display(Name = "Tên người dùng")]
        public string? AccountName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? AccountEmail { get; set; }

        [Display(Name = "Vai trò(ID) ")]
        public int? AccountRole { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required]
        [PasswordPropertyText]
        public string? AccountPassword { get; set; }

    }
}
