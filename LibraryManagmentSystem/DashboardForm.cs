using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer;

namespace LibraryManagmentSystem
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();

        }

        string searched_book="";
        private void LoadBooks()
        {
            try
            {
                // Get all books from the database
                DataTable booksTable = clsBook.GetAllBook();


                // Bind the DataTable to the DataGridView
                guna2DataGridView2.DataSource = booksTable.DefaultView;
                guna2DataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                guna2DataGridView2.Refresh();

            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
           searched_book = guna2TextBox1.Text;
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

            LoadBooks();

            // Initially make the DataGridView read-only (disable editing, sorting, etc.)
            guna2DataGridView2.ReadOnly = true;
            guna2DataGridView2.AllowUserToAddRows = false;
            guna2DataGridView2.AllowUserToDeleteRows = false;
            guna2DataGridView2.AllowUserToOrderColumns = false;
            guna2DataGridView2.AutoResizeColumns();


        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_left_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnPopular_Click(object sender, EventArgs e)
        {
            PopularForm popularForm = new PopularForm();
            popularForm.ShowDialog();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.ShowDialog();
        }
        //the Search Button Logic
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            //LoadBooks();

            MessageBox.Show("UnImplemented");

            

        }

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            frmAddMember frmAddMember = new frmAddMember();
            frmAddMember.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmLoanAndReturns frmLoanAndReturns = new frmLoanAndReturns();
            frmLoanAndReturns.ShowDialog();
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            frmSubscriptions frm = new frmSubscriptions();
            frm.ShowDialog();
        }
    }
}
