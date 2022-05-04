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
    public partial class Login : Form
    {    
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lochana\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string User;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "select count(*) from UserTbl where UName='" + UnameTb.Text + "' and Upassword='" + PasswordTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString() == "1")
            {
                User = UnameTb.Text;
                bookings Obj = new bookings();
                Obj.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Wrong UserName Or Password");
                UnameTb.Text = "";
                PasswordTb.Text = "";
            }
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();

        }
    }
}
