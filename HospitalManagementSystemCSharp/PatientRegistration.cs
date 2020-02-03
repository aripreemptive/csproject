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
    public partial class PatientRegistration : Form
    {
        public PatientRegistration()
        {
            InitializeComponent();


        }


        




        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
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

                int docid = int.Parse(comboBox1.SelectedValue.ToString());
                int roomid = int.Parse(comboBox2.SelectedValue.ToString());
                int patid = int.Parse(cbCustomers.SelectedValue.ToString());
                
                //   string str = "INSERT INTO patient(name,gen,age,date,cont,addr,disease,status,room_id,staff_id) VALUES('" + cbCustomers.Text + "','" + gen + "','" + textBoxAge.Text + "','" + date + "', '" + textBoxContact.Text + "','" + textBoxAddress.Text + "','" + textBoxDisease.Text + "','" + textBoxStatus.Text + "', @docid ,@roomid); ";

                string str = "INSERT INTO hyrje([sId], [rid], [pid], Datein,semundje,status ) VALUES( @docid,@roomid,@patid,'" + date + "','" + textBoxDisease.Text + "','" + textBoxStatus.Text + "'); ";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.Add(new SqlParameter("@patid", patid));
                cmd.Parameters.Add(new SqlParameter("@docid", docid));
                cmd.Parameters.Add(new SqlParameter("@roomid", roomid));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Patient Information Saved Successfully..");



            }
            catch (SqlException excep)
            {
                MessageBox.Show(excep.Message);
            } 
            con.Close();
            string str2 = "SELECT patient.name,patient.addr,patient.gen,patient.age,hyrje.status,hyrje.semundje,hyrje.Datein,staff.name,staff.position, room.building, room.Id, room.r_type FROM patient,hyrje ,staff , room where hyrje.pid=patient.Id and hyrje.sid=staff.Id and hyrje.rid=room.Id ";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtPatient = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            dtPatient.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dtPatient;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            textBoxAge.Clear();
            textBoxContact.Clear();
            textBoxAddress.Clear();
            textBoxDisease.Clear();
            textBoxStatus.Clear();


        }

        private void ComboBoxTest_Load(object sender, EventArgs e)

        {

        }
        private void combobox_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();



            SqlDataAdapter da;
            DataTable dtbl = new DataTable();
            string strCmd = "select name,id from staff";



            try
            {
                SqlCommand cmd = new SqlCommand(strCmd, con);
                da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                comboBox1.Enabled = true;
                this.comboBox1.SelectedIndex = -1;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();

            }
        }



        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                int patid = int.Parse(cbCustomers.SelectedValue.ToString());
                string str1 = "DELETE FROM hyrje WHERE pid = '" + patid + "'";

                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                string str = "DELETE FROM patientHistory WHERE p_name = '" + cbCustomers.Text + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show(" Patient Record Delete Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string str2 = "SELECT * FROM patient";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                DataTable dtPatient = new DataTable();

                SqlDataReader sdr = cmd2.ExecuteReader();
                dtPatient.Load(sdr);


                dataGridView1.DataSource = dtPatient;

            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void PatientRegistration_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            string str2 = "SELECT patient.name,patient.addr,patient.gen,patient.age,hyrje.status,hyrje.semundje,hyrje.Datein,staff.name,staff.position, room.building, room.Id, room.r_type FROM patient,hyrje ,staff , room where hyrje.pid=patient.Id and hyrje.sid=staff.Id and hyrje.rid=room.Id ";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtPatient = new DataTable();
            
            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            dtPatient.Load(sdr);
            con.Close();



            string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Id, name FROM patient", con1))
                {
                    //Fill the DataTable with records from Table.
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dt.Rows.InsertAt(row, 0);

                    //Assign DataTable as DataSource.
                    cbCustomers.DataSource = dt;
                    cbCustomers.DisplayMember = "name";
                    cbCustomers.ValueMember = "Id";

                    //Set AutoCompleteMode.
                    cbCustomers.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cbCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }

















            dataGridView1.DataSource = dtPatient;
        }

        private void comboboxroom_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();



            SqlDataAdapter da;
            DataTable dtbl = new DataTable();
            string strCmd = "select building,id from room ";



            try
            {
                SqlCommand cmd = new SqlCommand(strCmd, con);
                da = new SqlDataAdapter(strCmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                comboBox2.DataSource = ds.Tables[0];
                comboBox2.DisplayMember = "name" + "id";
                comboBox2.ValueMember = "id";
                comboBox2.Enabled = true;
                this.comboBox2.SelectedIndex = -1;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        public void showPatient(object sender, EventArgs e)
        {
            int id = (int)cbCustomers.SelectedValue;
            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();
            
                try
                {
                    string getCust = "select name,gen,age,cont,addr from patient where id ='" + id + "' ;";

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

                        
                        textBoxAddress.Text = dr.GetValue(4).ToString();
                       
                        textBoxContact.Text = dr.GetValue(3).ToString();
                        textBoxAge.Text = dr.GetValue(2).ToString();

                    }
                    else
                    {
                        MessageBox.Show(" Sorry, This Person, " + cbCustomers.Text + " is not Available.   ");
                    }
                }
                catch (SqlException excep)
                {
                    MessageBox.Show(excep.Message);
                }
                con.Close();
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            PatientRegistration pt = new PatientRegistration();
            pt.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }






