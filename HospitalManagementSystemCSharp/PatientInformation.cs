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
    public partial class PatientInformation : Form
    {
        string roomId, staffId;
        public PatientInformation()
        {
            InitializeComponent();
        }

        // mbaroi
        private void PatientInformation_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
                
            string str2 = "SELECT name, gen, age, date, cont, addr FROM patient";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtPatientHistory = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            dtPatientHistory.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dtPatientHistory;
            
        }

        //search mbaroi
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();
            if (textBoxName.Text != "")
            {
                try
                {

                    string getCust = "select name,gen,age,date,cont,addr,id from patient where name='" + textBoxName.Text + "' ;";
                    SqlCommand cmd = new SqlCommand(getCust, con);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBoxName.Text = dr.GetValue(0).ToString();
                        if (dr[1].ToString() == "Male")
                        {
                            radioButton1.Checked = true;
                        }
                        else
                        {
                            radioButton2.Checked = true;
                        }

                     



                        textBoxAge.Text = dr.GetValue(2).ToString();
                        textBoxDate.Text = dr.GetValue(3).ToString();
                        textBoxContact.Text = dr.GetValue(4).ToString();
                        textBoxAddress.Text = dr.GetValue(5).ToString();

                        int idja = int.Parse(dr.GetValue(6).ToString());
                        con.Close();
                        con.Open();
                        string detaje = "select * from dalje where pid='" + idja + "'";
                        SqlCommand cmd2 = new SqlCommand(detaje, con);
                        SqlDataReader dr2;
                        dr2 = cmd2.ExecuteReader();
                        if (dr2.Read())
                        {








                            textBoxDiesease.Text = dr2.GetValue(5).ToString();
                            textBoxStatus.Text = dr2.GetValue(6).ToString();
                            textBoxRoomId.Text = dr2.GetValue(2).ToString();
                            roomId = textBoxRoomId.Text;
                            textBoxDoctorId.Text = dr2.GetValue(1).ToString();
                            staffId = textBoxDoctorId.Text;
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Sorry, There is No patient with this Name.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxPatientId.Text = "";
                    }
                }
                catch (SqlException excep)
                {
                    MessageBox.Show(excep.Message);
                }
                con.Close();
                con.Open();
            }
          //  if (textBoxRoomId.Text != "")
            {
                try
                {
                    string getRoom = "select building,r_type,price from room where id=" + Convert.ToInt32(roomId) + " ;";

                    SqlCommand cmd1 = new SqlCommand(getRoom, con);
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.Read())
                    {
                        textBoxBuilding.Text = dr1.GetValue(0).ToString();
                        textBoxRoomType.Text = dr1.GetValue(1).ToString();
                        textBoxPrice.Text = dr1.GetValue(2).ToString();
                    }
                }
                catch (SqlException excep)
                {
                    MessageBox.Show(excep.Message);
                }
            }
            con.Close();
            con.Open();
            if (textBoxDoctorId.Text != "")
            {
                try
                {
                    string getStaff = "select name,position,contact from staff where id=" + Convert.ToInt32(staffId) + " ;";

                    SqlCommand cmd3 = new SqlCommand(getStaff, con);
                    SqlDataReader dr3;
                    dr3 = cmd3.ExecuteReader();
                    if (dr3.Read())
                    {
                        textBoxDoctorName.Text = dr3.GetValue(0).ToString();
                        textBoxDoctorPosition.Text = dr3.GetValue(1).ToString();
                        textBoxDoctorContact.Text = dr3.GetValue(2).ToString();
                    }
                }
                catch (SqlException excep)
                {
                    MessageBox.Show(excep.Message);
                }
                con.Close();
            }
        }
       
        //Add maroi
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string gen1;

            if (radioButton1.Checked)
            {
                gen1 = "Male";
            }
            else
            {
                gen1 = "Female";
            }
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string query1 = "insert into patient(name, gen, age, date, cont, addr) values('"+textBoxName.Text+"','"+gen1+"','"+textBoxAge.Text+"','"+textBoxDate.Text+"','"+textBoxContact.Text+"','"+textBoxAddress.Text+"'); ";
            SqlCommand cmd = new SqlCommand(query1, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Patient History Created Successfuly!","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);

            //mbush grid view
            con.Open();
            string str2 = "SELECT name, gen, age, date, cont, addr FROM patient";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtPatientHistory = new DataTable();

            SqlDataReader sdr = cmd2.ExecuteReader();
            dtPatientHistory.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dtPatientHistory;
            clear_fields();
        }

        //Delete mbaroi
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                string str = "DELETE FROM patient WHERE name = '" + textBoxName.Text + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
   
                MessageBox.Show(" Patient Record Delete Successfully");
               
                string str2 = "SELECT name, gen, age, date, cont, addr from patient";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                DataTable dtPatientHistory = new DataTable();

                SqlDataReader sdr = cmd2.ExecuteReader();
                dtPatientHistory.Load(sdr);
                con.Close();

                dataGridView1.DataSource = dtPatientHistory;

                clear_fields();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Please Enter Patient Id..","Need ID Please",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void textBoxDoctorId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDoctorId_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void clear_fields()
        {
            textBoxName.Clear();
            textBoxAge.Clear();
            textBoxDate.Clear();
            textBoxContact.Clear();
            textBoxAddress.Clear();
            textBoxDiesease.Clear();
            textBoxStatus.Clear();
            textBoxRoomId.Clear();
            textBoxDoctorId.Clear();
            textBoxBuilding.Clear();
            textBoxRoomType.Clear();
            textBoxRoomNumber.Clear();
            textBoxPrice.Clear();
            textBoxDoctorName.Clear();
            textBoxDoctorPosition.Clear();
            textBoxDoctorContact.Clear();
        }

    }
    
}
