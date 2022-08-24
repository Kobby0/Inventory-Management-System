using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_Management_System
{
    public partial class AttendantForm : Form
    {
        public AttendantForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mrr..Kobby\OneDrive\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void populate()
        {
            Con.Open();
            string query = "Select * from AttendantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ATTENDANTDGV.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ATTENDANTDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           Aid.Text =ATTENDANTDGV.SelectedRows[0].Cells[0].Value.ToString();
            AName.Text = ATTENDANTDGV.SelectedRows[0].Cells[1].Value.ToString();
           AAge.Text =ATTENDANTDGV.SelectedRows[0].Cells[2].Value.ToString();
            APhone.Text = ATTENDANTDGV.SelectedRows[0].Cells[3].Value.ToString();
            APass.Text = ATTENDANTDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                string query = "insert into AttendantTbl values("+Aid.Text+", '"+AName.Text+"','"+AAge.Text+"', '"+APhone.Text+"', '"+APass.Text+"') ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Attendant Added Successfully");
                Con.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Aid.Text =="" || AName.Text == "" || AAge.Text == "" || APhone.Text == "" || APass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update AttendantTbl set AttendantId= '"+Aid.Text+"', AttendantName='"+AName.Text+"', where Aage='"+AAge.Text+", where APhone='"+APhone.Text+"', where APass="+APass.Text+";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendant Updated Successfully");
                    Con.Close();
                    populate();
                }
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Aid.Text == "")
                {
                    MessageBox.Show("Select The Attendant to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from AttendantTbl where AttendantId="+Aid.Text+"";
                    MessageBox.Show("Attendant Deleted Successfully");
                    Con.Close();
                    populate();
                    Aid.Text = "";
                    AName.Text = "";
                    AAge.Text = "";
                    APhone.Text = "";
                    APass.Text = "";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AttendantForm att = new AttendantForm();
            att.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sell = new Sales();
            sell.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
