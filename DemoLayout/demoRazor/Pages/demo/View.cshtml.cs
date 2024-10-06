using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demoRazor.Pages.demo
{
    public class ViewModel : PageModel
    {

		[BindProperty]
		public int Id { get; set; }
		[BindProperty]
		public string NameView { get; set; }
		public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			return Page();
		}

    }
}
