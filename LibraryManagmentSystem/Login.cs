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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // dataGridView1.DataSource = clsMember.GetAllMember();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserLogin()
        {
            if (clsUser.Find(txtUsername.Text, txtPassword.Text) != null)
            {
                DashboardForm dashboardForm = new DashboardForm();
                //this.Hide();
                dashboardForm.ShowDialog();
                //this.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName/Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MemeberLogin()
        {

            if (int.TryParse(txtUsername.Text, out int MemberID))
            {
                MessageBox.Show($"{txtUsername.Text} is a valid integer!", "Integer Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsMember.Find(MemberID, txtPassword.Text) != null)
            {
                MessageBox.Show($"Member Screen Will Be Implemented Soon", "Not Implemented Yet",
    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid MemberID/Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (rbUser.Checked)
            {
                UserLogin();
            }else if (rbMember.Checked){
                MemeberLogin();
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
