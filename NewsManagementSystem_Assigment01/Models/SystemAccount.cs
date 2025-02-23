using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem_Assigment01.Models;

public partial class SystemAccount
{
    public short AccountId { get; set; }

    public string? AccountName { get; set; }

    public string? AccountEmail { get; set; }

    public int? AccountRole { get; set; }

    [Display(Name = "Mật khẩu")]
    public string? AccountPassword { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
