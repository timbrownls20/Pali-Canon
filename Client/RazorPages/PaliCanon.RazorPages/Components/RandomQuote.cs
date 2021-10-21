using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaliCanon.RazorPages.ApiClient;
using System.Net;
using System.Threading.Tasks;

namespace PaliCanon.RazorPages.Components
{
    public class RandomQuote: ViewComponent
    {
        private readonly IConfiguration config;

        public RandomQuote(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Quote quote = null;
            string baseUrl = config.GetValue<string>("Api");
            int maxLength = config.GetValue<int>("Quote:MaxLength");

            using (var apiClient = new PaliCanonClient(baseUrl))
            {
                (Quote content, HttpStatusCode status) result = await apiClient.Quote(maxLength);
                if (result.status == HttpStatusCode.OK)
                {
                    quote  = result.content;
                }
            }

            return View("Default", quote);
        }
    }
}
