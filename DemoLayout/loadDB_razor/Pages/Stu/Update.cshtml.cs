using System.Reflection;
using System;
using System.Xml.Linq;
using loadDB_razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace loadDB_razor.Pages.Stu
{
	public class UpdateModel : PageModel
	{
		[BindProperty]
		public Student std { get; set; }
		public List<Department> Departments = new List<Department>();

		public void OnGet(string id)
		{
			try
			{
				int Id = int.Parse(id);
				std = prn221Context.Ins.Students.Find(Id);
				Departments = prn221Context.Ins.Departments.ToList();

			}
			catch (Exception ex)
			{

			}


		}

		public IActionResult OnPost()
		{

			try
			{
				var student = prn221Context.Ins.Students.Find(std.Id);
				if (student != null)
				{

					student.Name = std.Name;
					student.Gender = std.Gender;
					student.DepartId = std.DepartId;
					student.Dob = std.Dob;
					student.Gpa = std.Gpa;

					prn221Context.Ins.Students.Update(student);
					prn221Context.Ins.SaveChanges();
					return RedirectToPage("/Stu/View");

				}
				else
				{

					ModelState.AddModelError(string.Empty, "Student not found.");
					return RedirectToPage("/Stu/Update");
				}

			}
			catch (Exception ex)
			{

				Departments = prn221Context.Ins.Departments.ToList();
				return Page();
			}


			// way 2
			//string id = Request.Form["Id"];
			//string name = Request.Form["Name"];
			//string gender = Request.Form["Gender"];
			//string departId = Request.Form["DepartId"];
			//string dob = Request.Form["Dob"];
			//string gpa = Request.Form["Gpa"];

			//var student = prn221Context.Ins.Students.Find(int.Parse(id));

			//student.Name = name;
			//student.Gender = gender == "true";
			//student.DepartId = departId;
			//student.Dob = DateTime.Parse(dob);
			//student.Gpa = double.Parse(gpa);

			//prn221Context.Ins.Students.Update(student);
			//prn221Context.Ins.SaveChanges();

			//return RedirectToPage("/Stu/View");

			//way 3
			//string id = form["Id"];
			//string name = form["Name"];
			//string gender = form["Gender"];
			//string departId = form["DepartId"];
			//string dob = form["Dob"];
			//string gpa = form["Gpa"];

			//var student = prn221Context.Ins.Students.Find(int.Parse(id));

			//if(student != null)
			//{
			//	student.Name = name;
			//	student.Gender = gender == "true";
			//	student.DepartId = departId;
			//	student.Dob = DateTime.Parse(dob);
			//	student.Gpa = double.Parse(gpa);
			//}

			//prn221Context.Ins.Students.Update(student);
			//prn221Context.Ins.SaveChanges();

			//return RedirectToPage("/Stu/View");

			//way4	
			//if (!int.TryParse(Id, out int studentId))
			//{
			//	return BadRequest("Invalid Student Id");
			//}


			//var student = prn221Context.Ins.Students.Find(studentId);

			//if (student == null)
			//{
			//	return NotFound("Student not found");
			//}

			//student.Name = Name;
			//student.Gender = Gender == "true";

			//student.DepartId = DepartId;


			//if (DateTime.TryParse(Dob, out DateTime dobValue))
			//{
			//	student.Dob = dobValue;
			//}
			//else
			//{
			//	return BadRequest("Invalid Date of Birth");
			//}

			//if (double.TryParse(Gpa, out double gpaValue))
			//{
			//	student.Gpa = gpaValue;
			//}
			//else
			//{
			//	return BadRequest("Invalid GPA");
			//}

			//prn221Context.Ins.Students.Update(student);

			//prn221Context.Ins.SaveChanges();

			//return RedirectToPage("/Stu/View");

		}
	}
}
// way 4 have argument : string Id, string Name, string Gender, string DepartId, string Dob, string Gpa
// way 3 have argumen : IFormCollection form
// way 2 : fe have name each field
//way 1: fe do not need name eacdh field , use bindingPropety with be