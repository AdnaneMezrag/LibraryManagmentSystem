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
    public partial class frmLoanAndReturns : Form
    {
        public frmLoanAndReturns()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a digit or a control key (like Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // If not, cancel the input
                e.Handled = true;
            }
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a digit or a control key (like Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // If not, cancel the input
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsLoan Loan = new clsLoan();
            Loan.LoanDate = DateTime.Now;
            Loan.BookID = int.Parse( txtBookID.Text);
            Loan.MemberID = int.Parse(txtMemberID.Text);
            Loan.ReturnDate = DateTime.MaxValue;

            if (Loan.Save())
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Failed You can't borrow this book");

            }
        }

        private void frmLoanAndReturns_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = clsLoan.GetAllLoan();
        }
    }
}
