using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using loadDB_razor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace loadDB_razor.Pages.StdNew
{
    public class IndexModel : PageModel
    {
        private readonly loadDB_razor.Models.prn221Context _context;

        public IndexModel(loadDB_razor.Models.prn221Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        //public SelectList Departments { get; set; } = default!;


        // su dung khi thay depart thanh radiobutton, checkbox
        public IList<Department> Departments { get; set; } = new List<Department>();


        public async Task OnGetAsync()
        {
            if (_context.Students != null)
            {
                Student = await _context.Students
                .Include(s => s.Depart).ToListAsync();
            }

            //Departments = new SelectList(_context.Departments, "Id", "Name");

            // su dung khi thay depart thanh radiobutton, checkbox
            Departments = await _context.Departments.ToListAsync();
        }
    }
}
