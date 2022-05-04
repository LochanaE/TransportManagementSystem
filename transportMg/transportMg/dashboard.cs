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
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            CountVehicle();
            CountUsers();
            CountDrivers();
            CountBooking();
            CountCustomers();
            SumAmount();
            BestCustomer();
            BestDriver();
        } 

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountVehicle()
        {
            con.Open();
            string Query = "select count(*) from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountUsers()
        {
            con.Open();
            string Query = "select count(*) from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountDrivers()
        {
            con.Open();
            string Query = "select count(*) from DriverTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountBooking()
        {
            con.Open();
            string Query = "select count(*) from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountCustomers()
        {
            con.Open();
            string Query = "select count(*) from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void SumAmount()
        {
            con.Open();
            string Query = "select Sum(Amount) from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            IncNumLbl.Text = "Rs"+dt.Rows[0][0].ToString();
            con.Close();
        }

        private void BestCustomer()
        {
            con.Open();
            string InnerQuery = "select Max(Amount) from BookingTbl";
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, con);
            sda1.Fill(dt1);
            string Query = "select CustName from BookingTbl where Amount = '"+dt1.Rows[0][0].ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BestCusLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void BestDriver()
        {
            con.Open();
            string Query = "select Driver,Count(*) from BookingTbl Group By Driver Order By Count(Driver) Desc";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BestDriverLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bookings obj = new bookings();
            this.Hide();
            obj.Show();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            vehicles obj = new vehicles();
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
