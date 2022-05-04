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
    public partial class customers : Form
    {
        public customers()
        {
            InitializeComponent();
            showcustomers();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            CustNameTb.Text = "";
            CustGenCb.SelectedIndex = -1;
            CustPhoneTb.Text = "";
            CustAddTb.Text = "";
        }
        private void showcustomers()
        {
            con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CustomerTbl (CustName,CustAdd,CustPhone,CustGen) values(@CN,@CA,@CP,@CG)", con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Recorded");
                    con.Close();
                    showcustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId=@CustKey", con);
                    cmd.Parameters.AddWithValue("@CustKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    con.Close();
                    showcustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        string Customer;
        private void CountBookingByCust()
        {
            con.Open();
            string Query = "select count(*) from BookingTbl where CustName='" + Customer + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BNumLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void SumAmt()
        {
            con.Open();
            string Query = "select Sum(Amount) from BookingTbl where CustName='" + Customer + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            TotAmountLbl.Text = "Rs. "+dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            Customer = CustNameTb.Text;
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustGenCb.SelectedItem = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (CustNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
                CountBookingByCust();
                SumAmt();

            }
            

        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set CustName=@CN,CustAdd=@CA,CustPhone=@CP,CustGen=@CG where CustId = @CKey", con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    con.Close();
                    showcustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            drivers Obj = new drivers();
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
