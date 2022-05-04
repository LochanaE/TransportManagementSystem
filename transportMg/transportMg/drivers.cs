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
    public partial class drivers : Form
    {
        public drivers()
        {
            InitializeComponent();
           // GetCars();
            showdrivers();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            dashboard obj = new dashboard();
            this.Hide();
            obj.Show();
        }
    
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");

      /*  private void GetCars()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from VehicleTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VLp", typeof(string));
            dt.Load(rdr);
            VehicleCb.ValueMember = "VLp";
            VehicleCb.DataSource = dt;
            con.Close();
        }*/
        private void Clear()
        {
            DrNameTb.Text = "";
            GenCb.SelectedIndex = -1;
            PhoneTb.Text = "";
            DrAddTb.Text = "";
        }
        private void showdrivers()
        {
            con.Open();
            string Query = "select * from DriverTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DriverDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void svnbtn_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAddTb.Text == "" || RatingCb.SelectedIndex == -1 )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into DriverTbl (DrName,DrPhone,DrAdd,DrDOB,DrJoinDate,DrGen,DrRating) values(@DRN,@DrP,@DrA,@DrD,@DrJ,@DrG,@DrR)", con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DrP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DrA", DrAddTb.Text);
                    cmd.Parameters.AddWithValue("@DrD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DrJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DrG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DrR", RatingCb.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Recorded");
                    con.Close();
                    showdrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        string Driver;
        private void CountBookingByDriver()
        {
            con.Open();
            string Query = "select count(*) from BookingTbl where Driver='" + Driver + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CountBookingLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        
        int Rate;
        private void GetStars()
        {
            Rate = Convert.ToInt32(DriverDGV.SelectedRows[0].Cells[7].Value.ToString());
            RateLbl.Text = "" + Rate;
            if (Rate == 1 || Rate == 2)
            {
                LevelLbl.Text = "OK";
                LevelLbl.ForeColor = Color.Red;
            }
            else if (Rate == 3 || Rate == 4)
            {
                LevelLbl.Text = "Good";
                LevelLbl.ForeColor = Color.DodgerBlue;
            }
            else {
                LevelLbl.Text = "Excellent";
                LevelLbl.ForeColor = Color.Green;
            }
               
        }
        int key = 0;
        private void DriverDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DrNameTb.Text = DriverDGV.SelectedRows[0].Cells[1].Value.ToString();
            Driver = DrNameTb.Text;
            PhoneTb.Text = DriverDGV.SelectedRows[0].Cells[2].Value.ToString();
            DrAddTb.Text = DriverDGV.SelectedRows[0].Cells[3].Value.ToString();
            DOB.Text = DriverDGV.SelectedRows[0].Cells[4].Value.ToString();
            JoinDate.Text = DriverDGV.SelectedRows[0].Cells[5].Value.ToString();
            GenCb.Text = DriverDGV.SelectedRows[0].Cells[6].Value.ToString();
            RatingCb.Text = DriverDGV.SelectedRows[0].Cells[7].Value.ToString();

            if (DrNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(DriverDGV.SelectedRows[0].Cells[0].Value.ToString());
                CountBookingByDriver();
                GetStars();
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a Driver");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from DriverTbl where DrId=@DKey", con);
                    cmd.Parameters.AddWithValue("@DKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Deleted");
                    con.Close();
                    showdrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAddTb.Text == "" || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Driver Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update DriverTbl set DrName=@DRN,DrPhone=@DrP,DrAdd=@DrA,DrDob=@DrD,DrJoinDate=@DrJ,DrGen=@DrG,DrRating=@DrR where DrId=@DKey", con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DrP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DrA", DrAddTb.Text);
                    cmd.Parameters.AddWithValue("@DrD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DrJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DrG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DrR", RatingCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Update");
                    con.Close();
                    showdrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

       private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            customers Obj = new customers();
            Obj.Show();
            this.Hide();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            vehicles Obj = new vehicles();
            Obj.Show();
            this.Hide();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bookings Obj = new bookings();
            Obj.Show();
            this.Hide();
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
