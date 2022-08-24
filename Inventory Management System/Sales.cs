using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace Inventory_Management_System
{
    public partial class Sales : Form
    {
        bool isGenerated = false;
        public Sales()
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
            string query = "Select ProdName, ProdQty from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void populatebills()
        {
            Con.Open();
            string query = "Select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void Sales_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            fillCombo();
            
            AttendantNamelbl.Text = Form1.AttendantName;
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            ProdQty.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
            
        }

        private void Datelbl_Click(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Today.Day.ToString()+"/"+ DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
        }
        int Grdtotal = 0, n = 0;

        public object Aname { get; private set; }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BillD.Text == "")
            {
                MessageBox.Show("Missing Bill id");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTbl values("+BillD.Text+", '"+AttendantNamelbl.Text+"','"+Datelbl.Text+"', '"+Amtlbl.Text+"') ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully");
                    Con.Close();
                    populatebills();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("SHOPRITE GHANA LIMITED", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Blue, new Point(230));
            e.Graphics.DrawString("Bill ID:"+BillDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Coral, new Point(100,70));
            e.Graphics.DrawString("Attendant Name:"+BillDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Coral, new Point(100, 100));
            e.Graphics.DrawString("Date:"+BillDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Coral, new Point(100, 160));
            e.Graphics.DrawString("Total Amount:"+BillDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Coral, new Point(100, 70));
            e.Graphics.DrawString("THANK YOU", new Font("Century Gothic", 30, FontStyle.Bold), Brushes.Blue, new Point(230,270));


        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void CatCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }
        private void fillCombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            CatCb.ValueMember = "catName";
            CatCb.DataSource = dt;
            
            Con.Close();
        }
        private void CatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void barcodebtn_Click(object sender, EventArgs e)

        {
            isGenerated=true;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox.Image = barcode.Draw(BarcodeTextBox.Text, 200);
        }

        private void Qrbtn_Click(object sender, EventArgs e)
        {
            isGenerated=true;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox.Image = qrBarcode.Draw(QRCodeTextBox.Text, 200); 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (isGenerated)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                pictureBox.Image.Save(path+"\\"+DateTime.Now.Second.ToString()+DateTime.Now.Millisecond.ToString()+".jpg",  ImageFormat.Jpeg);
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void BillD_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ProdName.Text == "" || ProdQty.Text == "")
            {
                MessageBox.Show("Missing Data");

            }
            else
            {
                int total = Convert.ToInt32(ProdQty.Text)*Convert.ToInt32(ProdQty.Text);
                int Grdtotal = 0;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ORDERDGV);
                newRow.Cells[0].Value= n + 1;
                newRow.Cells[1].Value= ProdName.Text;
                newRow.Cells[2].Value= ProdQty.Text;
                newRow.Cells[3].Value= ProdQty.Text;
                newRow.Cells[4].Value= Convert.ToInt32(ProdQty.Text)*Convert.ToInt32(ProdQty.Text);
                ORDERDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal+total;
                Amtlbl.Text = ""+ Grdtotal;

            }
        }
    }
}
