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

        
            cbxMaleFilter.ItemsSource = new List<string> { "All", "Male", "Female" };
            cbxMaleFilter.SelectedIndex = 0; 
        }

        private void load()
        {
            var student = prn221Context.Ins.Students
                .Include(x => x.Depart)
                //.Select(x => new
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Gender = x.Gender ? "Male" : "Female",
                //    DepartId = x.DepartId,
                //    Dob = x.Dob,
                //    Gpa = x.Gpa,
                //    Department = x.Depart,              
                //})
                .ToList();

            dgvDisplay.ItemsSource = student;

            //lvDisplay.ItemsSource = student;
        }
        string[] elements = { "Id", "Name", "Gender", "DepartName", "Dob", "Gpa" };

        private void loadDepart()
        {
            var dept = prn221Context.Ins.Departments.Select(X => X.Name).ToList();
            cbxDepartFilter.ItemsSource = dept;
            cbxDepartFilter.SelectedIndex = 0;

            cbxDepart.ItemsSource = prn221Context.Ins.Departments.Select(X => X.Name).ToList();
            cbxDepart.SelectedIndex = 0;
        }

        private void FilterStudents()
        {
            string selectedDept = cbxDepartFilter.SelectedItem?.ToString();
            string deptId = prn221Context.Ins.Departments.FirstOrDefault(x => x.Name.Equals(selectedDept))?.Id;

            string selectedGender = cbxMaleFilter.SelectedItem?.ToString();

            var query = prn221Context.Ins.Students.Include(x => x.Depart).AsQueryable();

            if (!string.IsNullOrEmpty(deptId))
            {
                query = query.Where(x => x.DepartId == deptId);
            }

            if (selectedGender == "Male")
            {
                query = query.Where(x => x.Gender == true);
            }
            else if (selectedGender == "Female")
            {
                query = query.Where(x => x.Gender == false);
            }

            var filteredStudents = query.ToList();
            dgvDisplay.ItemsSource = filteredStudents;
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

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Student st = getStudent();
            if(st == null)
            {
                clearForm();
                return;
            }
            var x = prn221Context.Ins.Students.Find(st.Id);
            if(x == null)
            {
                prn221Context.Ins.Students.Add(st);
                prn221Context.Ins.SaveChanges();
                load();
            }
        }

        private void clearForm()
        {
            txtId.Clear();
            txtName.Clear();
            rdbFemale.IsChecked = true;
            rdbMale.IsChecked = true;
            cbxDepart.SelectedIndex = 0;
            dpkDob.SelectedDate = null;
            txtGpa.Clear();
        }

        private Student getStudent()
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                bool gender = rdbMale.IsChecked == true;
                string deptId = prn221Context.Ins.Departments.FirstOrDefault(
                    x => x.Name.Equals(cbxDepart.SelectedValue.ToString())).Id;
                DateTime? dob = dpkDob.SelectedDate.Value;
                float gpa = float.Parse(txtGpa.Text);

                return new Student()
                {
                    Id = id,
                    Name = name,
                    Gender = gender,
                    DepartId = deptId,
                    Dob = dob,
                    Gpa = gpa
                };
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private void dgvDisplay_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //Student st = dgvDisplay.SelectedItem as Student;
            //if (st != null)
            //{
            //    txtId.Text = st.Id.ToString();
            //    txtName.Text = st.Name.ToString();
            //    rdbMale.IsChecked = st.Gender;
            //    rdbFemale.IsChecked = !st.Gender;
            //    cbxDepart.SelectedItem = st.Depart.Name;
            //    dpkDob.SelectedDate = st.Dob;
            //    txtGpa.Text = st.Gpa.ToString();
            //}
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Student updatedStudent = getStudent();
            //if (updatedStudent == null)
            //{
            //    MessageBox.Show("Invalid student details.");
            //    return;
            //}

            //var existingStudent = prn221Context.Ins.Students.Find(updatedStudent.Id);
            //if (existingStudent != null)
            //{
            //    existingStudent.Name = updatedStudent.Name;
            //    existingStudent.Gender = updatedStudent.Gender;
            //    existingStudent.DepartId = updatedStudent.DepartId;
            //    existingStudent.Dob = updatedStudent.Dob;
            //    existingStudent.Gpa = updatedStudent.Gpa;

            //    prn221Context.Ins.SaveChanges();
            //    load();
            //    MessageBox.Show("Student updated successfully.");
            //}
            //else
            //{
            //    MessageBox.Show("Student not found.");
            //}

            Student st = getStudent();
            var x = prn221Context.Ins.Students.Find(st.Id);
            if (x != null)
            {
                x.Name = st.Name;
                x.Gender = st.Gender;
                x.DepartId = st.DepartId;
                x.Gpa = st.Gpa;
                x.Dob = st.Dob;
                prn221Context.Ins.Students.Update(x);
                prn221Context.Ins.SaveChanges();
                load();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
 
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this student?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var student = prn221Context.Ins.Students.Find(int.Parse(txtId.Text));

                    if (student != null)
                    {
                        prn221Context.Ins.Students.Remove(student);
                        prn221Context.Ins.SaveChanges();
                        load(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbxMaleFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterStudents();
        }
    }
}
// dinh dang theo dd/mm/yyyy
// thay the Datagrid --> ListView
//filter theo gender (male /femake/all)

// them confirm msg when delete
// thay the gender thanh checkbox, comboBox
// thay the department =  group radiobutton, groupCheckbox




