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
    public partial class ViewwPatientCheckOut : Form
    {
        public ViewwPatientCheckOut()
        {
            InitializeComponent();
        }

        private void ViewwPatientCheckOut_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            string str2 = "SELECT patient.name,patient.addr,patient.gen,patient.age,dalje.status,dalje.semundje,dalje.Datein,dalje.dateout,dalje.docpri,dalje.totpri,staff.name,staff.position, room.building, room.Id, room.r_type FROM patient,dalje ,staff , room where dalje.pid=patient.Id and dalje.sid=staff.Id and dalje.rid=room.Id ";

            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtcheckout = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            dtcheckout.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dtcheckout;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ari Vogli\Downloads\projalk\ss\HospitalManagementSystemCSharp\hospital.mdf;Integrated Security=True;Connect Timeout=30");

            string str2 = "select name,gen,age,date,cont,addr,id from patient where name='" + textBox1.Text + "' ;";
            SqlCommand cmd2 = new SqlCommand(str2, con);
            DataTable dtcheckout = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            dtcheckout.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dtcheckout;
        }
    }
}
