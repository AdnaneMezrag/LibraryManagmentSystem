using Business_Layer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagmentSystem
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = clsLoan.GetBooksStatistics();
        }

        private void btnPopular_Click_1(object sender, EventArgs e)
        {
            PopularForm popularForm = new PopularForm();
            popularForm.ShowDialog();
        }

        private void btnStatistics_Click_1(object sender, EventArgs e)
        {
            DashboardForm DashboardForm = new DashboardForm();
            DashboardForm.ShowDialog();
        }
    }
}
