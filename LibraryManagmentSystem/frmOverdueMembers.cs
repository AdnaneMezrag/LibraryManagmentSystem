using Business_Layer;
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
    public partial class frmOverdueMembers : Form
    {
        public frmOverdueMembers()
        {
            InitializeComponent();
        }

        private void frmOverdueMembers_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource =  clsLoan.GetOverdueMembers();

        }

        private void frmOverdueMembers_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("This is the list of members who are overdue for more than 15 days", "Overdue Members", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
