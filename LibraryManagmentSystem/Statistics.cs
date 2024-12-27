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
