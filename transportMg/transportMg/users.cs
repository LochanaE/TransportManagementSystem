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
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
            showusers();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            UserNameTb.Text = "";
            PhoneTb.Text = "";
            PassowrdTb.Text = "";
        }
        private void showusers()
        {
            con.Open();
            string Query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void savebtn_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || PhoneTb.Text == "" || PassowrdTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into UserTbl (UName,UPhone,UPassword) values(@UN,@UP,@UPa)", con);
                    cmd.Parameters.AddWithValue("@UN", UserNameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPa", PassowrdTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Recorded");
                    con.Close();
                    showusers();
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
                MessageBox.Show("Select a User");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UserTbl where UId=@UKey", con);
                    cmd.Parameters.AddWithValue("@UKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted");
                    con.Close();
                    showusers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UserNameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            PassowrdTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (UserNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || PhoneTb.Text == "" || PassowrdTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update UserTbl set UName=@UN,UPhone=@UP,UPassword=@UPa where UId = @UIKey", con);
                    cmd.Parameters.AddWithValue("@UN",UserNameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPa", PassowrdTb.Text);
                    cmd.Parameters.AddWithValue("@UIKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated");
                    con.Close();
                    showusers();
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
