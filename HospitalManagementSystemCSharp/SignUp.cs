using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManagementSystemCSharp
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            string username = textBoxName.Text;
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;

            if (username.Equals(""))
            {
                MessageBox.Show("Please enter your username");
            }
            else if (email.Equals(""))
            {
                MessageBox.Show("Please enter your email");
            }
            else if (password.Equals(""))
            {
                MessageBox.Show("Please enter your password");
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
                
                string query = "insert into Users(Username, Password, Email) values( '" + username + "','" + password + "','" + email + "'); ";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int row = cmd.ExecuteNonQuery();

                if(row == 1)
                {
                    MessageBox.Show("Account is created succesfully!");
                    this.Hide();

                    Login login = new Login();
                    login.Show();
                }
                else
                {
                    MessageBox.Show("Error Occured !");
                }
            }
        }
    }
}
