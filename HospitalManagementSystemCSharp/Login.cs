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
    public partial class Login : Form

    {
        static class userData
        {
            public static Boolean admin;

        }
        DataTable dtUsers = new DataTable();

        string id, name, passwordi, emaili;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {




            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (username.Equals(""))
            {
                MessageBox.Show("Please enter your username");
            }
            else if (password.Equals(""))
            {
                MessageBox.Show("Please enter your password");
            }

            else
            {

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Desktop\Projekt\HospitalManagementSystemCSharp\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                //  + '" variable "' + 
                string query = "Select * from staff Where  name= '" + username + "' AND pass = '" + password + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtUsers);


                // SqlDataReader dr = cmd.ExecuteReader();

                //1 parameter -> query 
                //2 parameter -> DataTable where we will store our data from the database


                if (dtUsers.Rows.Count == 1) //means that row is inserted
                {
                    //kap te dhenat nga dataTable
                    id = dtUsers.Rows[0]["Id"].ToString();
                    name = dtUsers.Rows[0]["name"].ToString();
                    passwordi = dtUsers.Rows[0]["pass"].ToString();
                    userData.admin = (Boolean)dtUsers.Rows[0]["admin"];
                    con.Close();
                    if (userData.admin)
                    {
                        MessageBox.Show("You are Logged in successfuly! Doctor" + userData.admin);


                        this.Hide();
                        Home home = new Home();
                        home.Show();

                    }
                    else
                    {


                        MessageBox.Show("You are Logged in successfuly! Doctor" + userData.admin);


                        this.Hide();
                        Home home = new Home();
                        home.Show();


                    }
                }
                else
                {
                    MessageBox.Show("Error Occured!");
                }

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            // SignUp signup = new SignUp();
            //signup.Show();
            PatientRegistration pa = new PatientRegistration();
            pa.Show();
        }
    }
}
