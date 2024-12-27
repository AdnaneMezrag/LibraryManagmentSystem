﻿using System;
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
            LoadBooks();
            /////
        }
        private void LoadBooks()
        {
            try
            {
                // Get all books from the database
                DataTable booksTable = clsBook.GetAllBook();

                // Bind the DataTable to the DataGridView
                guna2DataGridView1.DataSource = booksTable;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Create an instance of DashboardForm
            DashboardForm dashboardForm = new DashboardForm();

            // Hide the current form
            this.Hide();

            // Show the DashboardForm
            dashboardForm.Show();

        }

        private void btnPopular_Click(object sender, EventArgs e)
        {
            // Create an instance of PopularForm
            PopularForm popularForm = new PopularForm();

            // Hide the current form
            this.Hide();

            // Show the PopularForm
            popularForm.Show();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            // Create an instance of StatisticsForm
            Statistics statisticsForm = new Statistics();

            // Hide the current form
            this.Hide();

            // Show the StatisticsForm
            statisticsForm.Show();
        }

        
    }
}
