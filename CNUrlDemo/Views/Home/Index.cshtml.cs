public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public string Password { get; set; }

    public async Task OnGet()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://www.cnblogs.com");
        var html = await response.Content.ReadAsStringAsync();
        _logger.LogInformation(html);
    }

    public void OnPost()
    {
        string password = Request.Form["Password"];

       
    }
}