using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace PaliCanon.Api.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;

        public string Version { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
            Version = configuration.GetValue<string>("Api:Version");
        }

        public void OnGet()
        {
        }
    }
}
