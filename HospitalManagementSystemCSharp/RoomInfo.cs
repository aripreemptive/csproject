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
    public partial class RoomInfo : Form
    {
        public RoomInfo()
        {
            InitializeComponent();
        }

        private void RoomInfo_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            string str2 = "SELECT Id, building, r_type,no_bed,price,r_status FROM room";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtRoom = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            dtRoom.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dtRoom;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
           
            try
            {
                textBoxId.Clear();
                string str = "INSERT INTO room(building,r_type,no_bed,price,r_status) VALUES('" + textBoxBuildingName.Text + "','" + textBoxRoomType.Text  + "','" + comboBox2.Text + "','" + textBoxPrice.Text + "','" + comboBox1.Text + "'); ";

                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();

                string str2 = "SELECT Id,building, r_type,no_bed,price,r_status FROM room";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                DataTable dtRoom = new DataTable();

                SqlDataReader sdr = cmd2.ExecuteReader();
                dtRoom.Load(sdr);
                con.Close();

                dataGridView1.DataSource = dtRoom;

            }
            catch (SqlException excep)
            {
                MessageBox.Show(excep.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxId.Clear();
            textBoxBuildingName.Clear();
            textBoxPrice.Clear();
            textBoxRoomNumber.Clear();
            textBoxRoomType.Clear();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();

            try
            {
                string query = "Update room set building='" + textBoxBuildingName.Text + "', r_type='" + textBoxRoomType.Text +"',no_bed='" + comboBox2.Text + "',price='" + textBoxPrice.Text + "',r_status='" + comboBox1.Text + "'where Id='"+textBoxId.Text+"'";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfuly");

                string str2 = "SELECT Id,building, r_type, no_bed, price, r_status FROM room";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = new BindingSource(dt, null);

            }
            catch (SqlException excep)
            {
                MessageBox.Show(excep.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {

                string str1 = "DELETE FROM room WHERE Id = '" + Convert.ToInt32(textBoxId.Text) + "'";

                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                string str2 = "SELECT Id,building, r_type, no_bed, price, r_status FROM room";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                DataTable dtRoom = new DataTable();

                SqlDataReader sdr = cmd2.ExecuteReader();
                dtRoom.Load(sdr);


                dataGridView1.DataSource = dtRoom;

            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
