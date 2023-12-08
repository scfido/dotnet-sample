using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnet.Samples.RazorPages;

public class Page1Model : PageModel
{
    private readonly ILogger<Page1Model> _logger;

    public Page1Model(ILogger<Page1Model> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
