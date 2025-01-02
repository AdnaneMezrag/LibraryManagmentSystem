using Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagmentSystem
{
    public partial class frmAddMember : Form
    {
        public frmAddMember()
        {
            InitializeComponent();
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, pattern))
            {
                return false;
            }

            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsMember member = new clsMember();
            member.FirstName = txtFirstName.Text;
            member.SecondName = txtLastName.Text;
            member.Email = txtEmail.Text;
            member.DateOfBirth = guna2DateTimePicker1.Value;
            member.PhoneNumber = txtPhoneNumber.Text;
            member.Password = txtPassword.Text;
            member.Sex = (rbMale.Checked == true ? true :  false);
            if (!IsValidEmail( member.Email )) {
                MessageBox.Show("Error Email Format Is Wrong: " , "Error Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(member.DateOfBirth.Year < 18)
            {
                MessageBox.Show("Enter a valid date of birth: ", "Error Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (member.Save())
            {
                MessageBox.Show("Member Has Been Registred Successfully: ", "Success Member Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Member Couldn't Be Registred: ", "Error Member Registration", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
