using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HospitalManagementSystemCSharp
{
    public partial class StaffInformation : Form
    {
        public StaffInformation()
        {
            InitializeComponent();
        }

        private void StaffInformation_Load(object sender, EventArgs e)
        {
            this.staffTableAdapter.Fill(this.hospitalDataSet.staff);
            using (SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30"))
            {

                string str2 = "SELECT * FROM staff";
                SqlCommand cmd2 = new SqlCommand(str2, con1);
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = new BindingSource(dt, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string gen = string.Empty;
            if (radioButton1.Checked)
            {
                gen = "Male";
            }
            else
            {
                gen = "Female";
            }
            try
            {
                string str = "INSERT INTO staff(name,gender,position,salary,contact,address,pass) VALUES('" + textBoxName.Text + "','" + gen + "','" + textBoxPosition.Text + "','" + textBoxSalary.Text + "','" + textBoxContact.Text + "','" + textBoxAddress.Text + "','" + textBox10.Text + "'); ";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Staff Added Successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                using (SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30"))
                {

                    string str2 = "SELECT * FROM staff";
                    SqlCommand cmd2 = new SqlCommand(str2, con1);
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = new BindingSource(dt, null);
                }
            }
            catch (SqlException excep)
            {
                MessageBox.Show(excep.Message);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();
            if (textBoxName.Text != "")
            {
                try
                {
                    string getCust = "select name,gender,position,salary,contact,address,pass from staff where name ='" + textBoxName.Text + "' ;";

                    SqlCommand cmd = new SqlCommand(getCust, con);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        // textBoxName.Text = dr.GetValue(0).ToString();
                        if (dr[1].ToString() == "Male")
                        {
                            radioButton1.Checked = true;
                        }
                        else
                        {
                            radioButton2.Checked = true;
                        }

                        textBoxPosition.Text = dr.GetValue(2).ToString();
                        textBoxSalary.Text = dr.GetValue(3).ToString();
                        textBoxContact.Text = dr.GetValue(4).ToString();
                        textBoxAddress.Text = dr.GetValue(5).ToString();
                        textBox10.Text = dr.GetValue(6).ToString();

                    }
                    else
                    {
                        MessageBox.Show(" Sorry, This Person, " + textBoxName.Text + " is not Available.   ");
                                           }
                }
                catch (SqlException excep)
                {
                    MessageBox.Show(excep.Message);
                }
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string gen = string.Empty;
            if (radioButton1.Checked)
            {
                gen = "Male";
            }
            else
            {
                gen = "Female";
            }
            try
            {
                string str = " Update staff set name='" + textBoxName.Text + "',gender='" + gen + "',position='" + textBoxPosition.Text + "',salary='" + textBoxSalary.Text + "',contact='" + textBoxContact.Text + "',address='" + textBoxAddress.Text + "',pass='" + textBox10.Text + "' where name='" + textBoxName.Text + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Staff Updated Successfuly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30"))
                {

                    string str2 = "SELECT * FROM staff";
                    SqlCommand cmd2 = new SqlCommand(str2, con1);
                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = new BindingSource(dt, null);
                }

            }
            catch (SqlException excep)
            {
                MessageBox.Show(excep.Message);
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxPosition.Text = "";
            textBoxSalary.Text = "";
            textBoxContact.Text = "";
            textBoxAddress.Text = "";
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
