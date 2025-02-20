using NewsManagementSystem_Assigment01.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem_Assigment01.ViewModel
{
    public class NewsArticleViewModel
    {
        [Display(Name = "Id tin tức")]
        public string NewsArticleId { get; set; } = null!;
        [Display(Name = "Chủ đề tin tức")]
        public string? NewsTitle { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Headline { get; set; } = null!;
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Display(Name = "Nội dung tin tức")]
        public string? NewsContent { get; set; }
        [Display(Name = "Nguồn tin tức")]
        public string? NewsSource { get; set; }

        public short? CategoryId { get; set; }
        [Display(Name = "Trạng thái")]
        public bool? NewsStatus { get; set; }
        [Display(Name = "Id người tạo")]
        public string? CreatedById { get; set; }
        [Display(Name = "Id người chỉnh sửa lần cuối")]
        public short? UpdatedById { get; set; }
        [Display(Name = "Ngày chỉnh sửa cuối cùng")]
        public DateTime? ModifiedDate { get; set; }
    }
}
