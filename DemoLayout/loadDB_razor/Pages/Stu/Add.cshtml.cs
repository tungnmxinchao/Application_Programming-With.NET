using loadDB_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace loadDB_razor.Pages.Stu
{
    public class AddModel : PageModel
    {
		private readonly prn221Context _context;
        public AddModel(prn221Context context)
        {
			_context = context;
		}

		[BindProperty]
		public Student NewStudent { get; set; } = new Student();

		public List<Department> Departments { get; set; }
		public async Task OnGetAsync()
		{
			
			Departments = await _context.Departments.ToListAsync();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			//if (!ModelState.IsValid)
			//{
			//	Departments = await _context.Departments.ToListAsync();
			//	return Page();
			//}


			var existingStudent = await _context.Students.FindAsync(NewStudent.Id);
			if (existingStudent != null)
			{
				ModelState.AddModelError("NewStudent.Id", "A student with this ID already exists.");
				Departments = await _context.Departments.ToListAsync();
				return Page();
			}

			
			_context.Students.Add(NewStudent);
			await _context.SaveChangesAsync();

			
			return RedirectToPage("/Stu/View");
		}
	}
}
