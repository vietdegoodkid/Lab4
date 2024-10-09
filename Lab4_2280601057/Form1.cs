using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_2280601057
{
    public partial class Form1 : Form
    {
        private Model1 dbContext;
        public Form1()
        {

            InitializeComponent();
            dbContext = new Model1();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadFaculties();
        }
        private void LoadData()
        {
            dtgv_QLSV.DataSource = dbContext.Students.ToList();
        }
        private void LoadFaculties()
        {
            cmbKhoa.DataSource = dbContext.Faculties.ToList();
            cmbKhoa.DisplayMember = "FacultyName";
            cmbKhoa.ValueMember = "FacultyID";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMSSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                StudentID = txtMSSV.Text,
                FullName = txtTen.Text,
                AverageScore = double.TryParse(txtDiem.Text, out double score) ? score : 0,
                FacultyID = (int)cmbKhoa.SelectedValue
            };

            dbContext.Students.Add(student);
            dbContext.SaveChanges();
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var selectedStudent = dtgv_QLSV.CurrentRow.DataBoundItem as Student;
            if (selectedStudent != null)
            {
                // Create a new student object
                var newStudent = new Student
                {
                    StudentID = txtMSSV.Text, // Use the new ID
                    FullName = txtTen.Text,
                    AverageScore = double.TryParse(txtDiem.Text, out double score) ? score : 0,
                    FacultyID = (int)cmbKhoa.SelectedValue
                };

                // Remove the old student from the context
                dbContext.Students.Remove(selectedStudent);
                // Add the new student to the context
                dbContext.Students.Add(newStudent);
                dbContext.SaveChanges(); // Save changes to the database
                LoadData(); // Reload data to reflect changes
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var selectedStudent = dtgv_QLSV.CurrentRow.DataBoundItem as Student;
            if (selectedStudent != null)
            {
                dbContext.Students.Remove(selectedStudent);
                dbContext.SaveChanges();
                LoadData();
            }
        }

        private void dtgv_QLSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedStudent = dtgv_QLSV.CurrentRow.DataBoundItem as Student;
            if (selectedStudent != null)
            {
                txtMSSV.Text = selectedStudent.StudentID; // Set the StudentID
                txtTen.Text = selectedStudent.FullName;
                txtDiem.Text = selectedStudent.AverageScore.ToString();
                cmbKhoa.SelectedValue = selectedStudent.FacultyID;

                // Make the StudentID textbox read-only
                txtMSSV.ReadOnly = true; // Prevent modification
            }
        }
    }
}
