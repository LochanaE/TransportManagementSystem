using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace transportMg
{
    public partial class bookings : Form
    {
        public bookings()
        {
            InitializeComponent();
            GetCustomers();
            showBookings();
            GetCars();
            UnameLbl.Text = Login.User;
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCustomers()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CustomerTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustName", typeof(string));
            dt.Load(rdr);
            CustCb.ValueMember = "CustName";
            CustCb.DataSource = dt;
            con.Close();
        }
        private void GetDriver()
        {
            con.Open();
            string Query = "select * from VehicleTbl where VLp='" + VehicleCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                DriverTb.Text = dr["Driver"].ToString();
            }
            con.Close();
        }
        private void GetCars()
        {
            string IsBooked = "No";
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from VehicleTbl where Booked='" +IsBooked+ "' ", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VLp", typeof(string));
            dt.Load(rdr);
            VehicleCb.ValueMember = "VLp";
            VehicleCb.DataSource = dt;
            con.Close();
        }
        private void Clear()
        {
            CustCb.SelectedIndex = -1;
            VehicleCb.SelectedIndex = -1; ;
            DriverTb.Text = "";
            AmountTb.Text = "";
        }
        private void showBookings()
        {
            con.Open();
            string Query = "select * from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void UpdateVehicle()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update VehicleTbl set Booked=@VB where VLp=@VP", con);
                cmd.Parameters.AddWithValue("@VP", VehicleCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@VB", "Yes");
  
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vehicle Updated");
                con.Close();
                Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustCb.SelectedIndex == -1 || VehicleCb.SelectedIndex == -1 || DriverTb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into BookingTbl(CustName,Vehicle,Driver,PickUpDate,DropOffDate,Amount,BUser) values(@CN,@Veh,@Dri,@PDate,@DDate,@Am,@Bu)", con);
                    cmd.Parameters.AddWithValue("@CN", CustCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Veh", VehicleCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Dri", DriverTb.Text);
                    cmd.Parameters.AddWithValue("@PDate", PickUpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@DDate", RetDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Am",AmountTb.Text);
                    cmd.Parameters.AddWithValue("@Bu", UnameLbl.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Booked");
                    con.Close();
                    showBookings();
                    UpdateVehicle();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void VehicleCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDriver();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            vehicles Obj = new vehicles();
            Obj.Show();
            this.Hide();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            customers Obj = new customers();
            Obj.Show();
            this.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            drivers Obj = new drivers();
            Obj.Show();
            this.Hide();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            dashboard obj = new dashboard();
            this.Hide();
            obj.Show();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            this.Hide();
            obj.Show();
        }

        private void slidmenu_Click(object sender, EventArgs e)
        {
            if (guna2Panel1.Width == 219)
            {
                guna2Panel1.Width = 56;
            }
            else

                guna2Panel1.Width = 219;

        }
    }
}
