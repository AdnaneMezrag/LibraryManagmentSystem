using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

            if(clsLoan.IsBookBorrowedByMember(Loan.MemberID, Loan.BookID))
            {
                MessageBox.Show("Error You Already Borrowed This Book Before And You Haven't Returned It Yet: ", "Loan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(clsBook.Find(Loan.BookID).Quantity == 0)
            {
                MessageBox.Show("Failed You can't borrow this book", "Loan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Loan.Save())
            {
                MessageBox.Show("You Successfully Borrowed This Book", "Loan Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed You can't borrow this book" ,"Loan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLoanAndReturns_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = clsLoan.GetAllLoan();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int BookID = int.Parse(txtBookID.Text);
            int MemberID = int.Parse(txtMemberID.Text);

            if (clsLoan.ReturnBook(MemberID, BookID))
            {
                MessageBox.Show("You Successfully Returned This Book", "Return Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed You can't Return this book because either MemberID dosen't exist or BookID dosen't exist or the member didn't borrowed this book or has returned it already", "Return Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
