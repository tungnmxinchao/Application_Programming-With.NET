using loadDB_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace loadDB_razor.Pages.Stu
{
    public class ViewModel : PageModel
    {
        public List<Student> std = new List<Student>();
        public List<Department> Departments = new List<Department>();

        private readonly prn221Context conn;
        public ViewModel(prn221Context _conn)
        {
            conn = _conn;
        }
        public void OnGet()
        {
            std = conn.Students.Include(x => x.Depart).ToList();
            Departments = conn.Departments.ToList();
        }
    }
}
