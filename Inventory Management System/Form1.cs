
using System.Data;
using System.Data.SqlClient;

namespace Inventory_Management_System
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string AttendantName = "";

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mrr..Kobby\OneDrive\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text=="" || txtPassword.Text == "")
            {
                MessageBox.Show("Invalid Username or Password, Please try again");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {
                    if (RoleCb.SelectedItem.ToString()=="ADMIN")
                    {
                        if (txtUsername.Text == "Admin" &&  txtPassword.Text == "Admin")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Please Re-try again");
                        }

                    }
                    else
                    {
                        //MessageBox.Show("You are an Attendant");
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from AttendantTbl where AttendantName='"+txtUsername.Text+"' and AttPass='"+txtPassword.Text+"'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            AttendantName = txtUsername.Text;
                            Sales sell = new Sales();
                            sell.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                        }
                        Con.Close();
                    }

                }

                else
                {
                    MessageBox.Show("Select A Role");
                }





            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CheckbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '*';

            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}