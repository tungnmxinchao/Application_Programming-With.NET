using loadDB_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

      
        public void OnGet()
        {
            Departments = _context.Departments.ToList();
        }

     
        public IActionResult OnPost()
        {
          
            //if (!ModelState.IsValid)
            //{
            //    Departments = _context.Departments.ToList();
            //    return Page();
            //}

           
            var existingStudent = _context.Students.Find(NewStudent.Id);
            if (existingStudent != null)
            {
                ModelState.AddModelError("NewStudent.Id", "A student with this ID already exists.");
                Departments = _context.Departments.ToList();
                return Page();
            }

            
            _context.Students.Add(NewStudent);
            _context.SaveChanges();

      
            return RedirectToPage("/Stu/View");
        }
    }
}
