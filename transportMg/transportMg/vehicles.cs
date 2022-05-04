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
namespace transportMg
{
    public partial class vehicles : Form
    {
        public vehicles()
        {
            InitializeComponent();
            ShowVehicles();
            GetDrivers();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            LPlateTb.Text = "";
            MarkCb.SelectedIndex = -1;
            ModelTb.Text = "";
            VYearCb.SelectedIndex = -1;
            EngTypeCb.SelectedIndex = -1;
            ColorTb.Text = "";
            MilleageTb.Text = "";
            TypeCb.SelectedIndex = -1;
            BookedCb.SelectedIndex = -1;
        }
        private void btnhide_Click(object sender, EventArgs e)
        {

        }

        private void ShowVehicles()
        {
            con.Open();
            string Query = "select * from vehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void GetDrivers()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from DriverTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DrName", typeof(string));
            dt.Load(rdr);
            DriverCb.ValueMember = "DrName";
            DriverCb.DataSource = dt;
            con.Close();
        }
        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into VehicleTbl (Vlp,Vmark,Vmodel,VYear,VEngType,VColor,VMilleage,VType,Booked,Driver) values(@VP,@Vma,@Vmo,@VY,@VEng,@VCo,@VMi,@VTy,@VB,@Dr)", con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@Vmi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTY", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Recorded");
                    con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "")
            {
                MessageBox.Show("Select a Vehicle");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from VehicleTbl where VLP=@VPlate", con);
                    cmd.Parameters.AddWithValue("@VPlate", LPlateTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Deleted");
                    con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        string V;
        private void CountBookingByVehicle()
        {
            con.Open();
            string Query = "select count(*) from BookingTbl where Vehicle='" + V + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LPlateTb.Text = VehicleDGV.SelectedRows[0].Cells[0].Value.ToString();
            V = LPlateTb.Text;
            CountBookingByVehicle();
            MarkCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[1].Value.ToString();
            ModelTb.Text = VehicleDGV.SelectedRows[0].Cells[2].Value.ToString();
            VYearCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[3].Value.ToString();
            EngTypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[4].Value.ToString();
            ColorTb.Text = VehicleDGV.SelectedRows[0].Cells[5].Value.ToString();
            MilleageTb.Text = VehicleDGV.SelectedRows[0].Cells[6].Value.ToString();
            TypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[7].Value.ToString();
            BookedCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[8].Value.ToString();


        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update VehicleTbl set Vmark=@Vma,Vmodel=@Vmo,VYear=@VY,VEngType=@VEng,VColor=@VCo,VMilleage=@VMi,VType=@VTy,Booked=@VB, Driver=@Dr where VLp=@VP", con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@Vmi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTY", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Updated");
                    con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            this.Hide();
            obj.Show();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            dashboard obj = new dashboard();
            this.Hide();
            obj.Show();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bookings obj = new bookings();
            this.Hide();
            obj.Show();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            customers obj = new customers();
            this.Hide();
            obj.Show();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            drivers obj = new drivers();
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
