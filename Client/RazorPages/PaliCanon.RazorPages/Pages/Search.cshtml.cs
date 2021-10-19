using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PaliCanon.RazorPages.ApiClient;

namespace PaliCanon.RazorPages.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IConfiguration config;

        [Required(ErrorMessage = "Please enter a search term")]
        [BindProperty]
        public string SearchTerm { get; set; }

        public List<Quote> Quotes { get; set; }

        public SearchModel(IConfiguration config)
        {
            this.config = config;
            Quotes = new List<Quote>();
        }

        public void OnGet()
        {
            

        }

        public async Task OnPost()
        {
            var baseUrl = config.GetValue<string>("Api");
            using (var apiClient = new PaliCanonClient(baseUrl))
            {
                var result = await apiClient.Search(SearchTerm);
                if(result.status == HttpStatusCode.OK)
                {
                    Quotes = result.content;
                } 
            }
        }
    }
}
