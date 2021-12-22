using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [Display(Name = "用户名")]
    public string Name { get; set; }

    [Display(Name = "密码")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public void OnGet()
    {

    }

    public void OnPost()
    {

    }
}
