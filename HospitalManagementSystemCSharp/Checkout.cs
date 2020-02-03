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
    public partial class Checkout : Form
    {
        public Checkout()
        {
            InitializeComponent();
        }

        private void Checkout_Load(object sender, EventArgs e)
        {

        }

        string roomid, docid, cmimi, cmimi_doktorit;

        private void buttonPay_Click(object sender, EventArgs e)
        {

            try
            {
                int idp;
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

                string query = "Select id from patient Where name='" + textBoxName.Text + "';";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    idp = int.Parse(dr.GetValue(0).ToString());
                    string query2 = "Insert into dalje(sid,rid,pid,Datein,semundje,status) select sid, rid, pid, Datein, semundje, status from hyrje where pid ='" + idp + "' ";

                    con.Close();


                    con.Open();
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();






                    string query3 = "update dalje Set dateout= '" + dateTimePickerOut.Value + "', docpri='" + textBoxDocPrice.Text + "',totpri='" + textBoxTotal.Text + "'where pid= '"+idp+"'";

                    con.Close();



                    con.Open();
                    SqlCommand cmd3 = new SqlCommand(query3, con);
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Checkout was Successful !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query1 = "Delete from hyrje where pid='" + idp + "';";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (SqlException excep)
            {
                MessageBox.Show(excep.Message);
            }


        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");


            int idp;
      

            string query = "Select id from patient Where name='" + textBoxName.Text + "';";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                idp = int.Parse(dr.GetValue(0).ToString());



                con.Close();





                string query3 = "Select Datein,rid,sId from dalje Where pid='" + idp + "';";
                con.Open();
                SqlCommand cmd3 = new SqlCommand(query3, con);
                SqlDataReader dr3;
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    string datein = dr3.GetValue(0).ToString();
                    MessageBox.Show("You are Logged in successfuly! Doctor" + datein);
                    dateTimePickerIn.Value = Convert.ToDateTime(datein);
                    roomid = dr3.GetValue(1).ToString();
                    docid = dr3.GetValue(2).ToString();
                }
            }
            con.Close();

            con.Open();
            string query1 = "Select price from room Where Id='" + roomid + "';";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                cmimi = dr1.GetValue(0).ToString();
                textBoxRoomPrice.Text = cmimi;
            }
            con.Close();

            con.Open();
            string query2 = "Select salary from staff Where Id='" + docid + "';";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                cmimi_doktorit = dr2.GetValue(0).ToString();
                textBoxDocPrice.Text = cmimi_doktorit;
            }
            con.Close();

            int shuma = Convert.ToInt32(cmimi) + Convert.ToInt32(cmimi_doktorit);

            textBoxTotal.Text = shuma.ToString();
        }
    }
}
    
