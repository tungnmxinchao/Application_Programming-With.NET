using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using loadDB_razor.Models;

namespace loadDB_razor.Pages.StdNew
{
    public class CreateModel : PageModel
    {
        private readonly loadDB_razor.Models.prn221Context _context;

        public CreateModel(loadDB_razor.Models.prn221Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DepartId"] = new SelectList(_context.Departments, "Id", "Name");
            return Page();


            //// dung khi thay doi depart sang radiobutton
            //var departments = _context.Departments.ToList();


            //ViewData["DepartId"] = departments.Select(d => new SelectListItem
            //{
            //    Value = d.Id.ToString(), 
            //    Text = d.Name
            //}).ToList();

            //return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
