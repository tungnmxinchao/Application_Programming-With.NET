using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrudWpf.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWpf
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
            loadDepart();
            cbxSearchIn.ItemsSource = elements;
        }

        private void load()
        {
            var student = prn221Context.Ins.Students
                .Include(x => x.Depart)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Gender = x.Gender ? "Male" : "Female",
                    DepartId = x.DepartId,
                    Dob = x.Dob,
                    Gpa = x.Gpa,
                    Department = x.Depart,              
                })
                .ToList();

            dgvDisplay.ItemsSource = student;  
        }
        string[] elements = { "Id", "Name", "Gender", "DepartName", "Dob", "Gpa" };

        private void loadDepart()
        {
            var dept = prn221Context.Ins.Departments.Select(X => X.Name).ToList();
            cbxDepartFilter.ItemsSource = dept;
            cbxDepartFilter.SelectedIndex = 0;
        }

        private void FilterStudents()
        {
            string dept = cbxDepartFilter.SelectedValue.ToString();
            string deptId = prn221Context.Ins.Departments.FirstOrDefault(x => x.Name.Equals(dept)).Id;

            var st = prn221Context.Ins.Students
                .Include(x => x.Depart)
                .Where(x => x.DepartId == deptId)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Gender = x.Gender ? "Male" : "Female",
                    Dob = x.Dob,
                    Gpa = x.Gpa,
                    Department = x.Depart
                })
                .ToList();

            dgvDisplay.ItemsSource = st;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string txt = txtSearch.Text;
            if (cbxSearchIn.SelectedItem.ToString().Equals(elements[1]))
            {
                dgvDisplay.ItemsSource = prn221Context.Ins.Students.Include(x => x.Depart).
                    Where(X => string.IsNullOrEmpty(txt) ? true : X.Name.Contains(txt)).ToList();
            }
        }

        private void cbxDepartFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterStudents();
        }
    }
}
// dinh dang theo dd/mm/yyyy
// thay the Datagrid --> ListView