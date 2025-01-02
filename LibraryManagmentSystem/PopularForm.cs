using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;

namespace LibraryManagmentSystem
{
    public partial class PopularForm : Form
    {
        public PopularForm()
        {
            InitializeComponent();
        }

        private void PopularForm_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = clsLoan.GetPopularBooks();
        }

        private void btnPopular_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.ShowDialog();

        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.ShowDialog();
        }

        
    }
}
