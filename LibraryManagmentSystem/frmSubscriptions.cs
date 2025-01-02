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
    public partial class frmSubscriptions : Form
    {
        public frmSubscriptions()
        {
            InitializeComponent();
        }

        private void frmSubscriptions_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = clsMember.GetAllMember();

        }
    }
}
