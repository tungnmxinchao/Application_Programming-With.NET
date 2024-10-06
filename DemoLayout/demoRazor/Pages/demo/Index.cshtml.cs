using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demoRazor.Pages.demo
{
    public class IndexModel : PageModel
    {
		[BindProperty]
		public string Name { get; set; } = "Nguyen Manh Tung";
		[BindProperty]
		public string Id { get; set; } = "0";
		[BindProperty]
		public string Age { get; set; } = "1";
		public void OnGet()
        {
            //ViewData["name"] = Name;
        }

		//public IActionResult OnPost()
		//{
		//    Name = Request.Form["name"];
		//    Id = Request.Form["id"];

		//    return Page();
		//}

		//public IActionResult OnPost(IFormCollection form)
		//{
		//	Name = form["name"];
		//	Id = form["id"];

		//	return Page();
		//}

		//public IActionResult OnPost(string id, string name)
		//{
		//	Name = name;
		//	Id = id;

		//	return Page();
		//}

		public IActionResult OnPost()
		{
			return Page();
		}



		//public IActionResult OnGet()
		//{
		//    ViewData["name"] = Name;
		//    return Page();
		//}
	}
}
