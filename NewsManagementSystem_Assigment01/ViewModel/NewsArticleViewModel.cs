using NewsManagementSystem_Assigment01.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem_Assigment01.ViewModel
{
    public class NewsArticleViewModel
    {
        [Display(Name = "Id tin tức")]
        [Required]
        [StringLength(10)]
        [Key]
        public string NewsArticleId { get; set; } = null!;

        [Display(Name = "Chủ đề tin tức")]
        [Required]
        public string? NewsTitle { get; set; }

        [Display(Name = "Tiêu đề")]
        [Required]
        public string Headline { get; set; } = null!;
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Nội dung tin tức")]
        [Required]
        public string? NewsContent { get; set; }

        [Display(Name = "Nguồn tin tức")]
        [Required]
        public string? NewsSource { get; set; }

        [Display(Name ="CategoryName")]
        [Required]
        public short? CategoryId { get; set; }
        [Display(Name = "Trạng thái")]
        [Required]
        public bool? NewsStatus { get; set; }
        [Display(Name = "Id người tạo")]
        public string? CreatedById { get; set; }
        [Display(Name = "Id người chỉnh sửa lần cuối")]
        public string? UpdatedById { get; set; }
        [Display(Name = "Ngày chỉnh sửa cuối cùng")]
        public DateTime? ModifiedDate { get; set; }
    }
}
