using LinqToDB;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using static LinqToDB.Common.Configuration;

namespace InvoiceApp
{
    public partial class Form3 : Form
    {
        BindingSource bindingSource = new BindingSource();
        BindingSource bindingSourceBedrijven = new BindingSource();
        BindingSource bindingSourceKlanten = new BindingSource();

        static string info = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\", "OfferteDatabase.mdf"));


        string infoConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                    $"AttachDbFilename={info};" +
                                    "Integrated Security=True;" +
                                    "Connect Timeout=30";

        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

           
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ConnectDataGridOfferte();
            ConnectDataGridBedrijven();
            ConnectDataGridKlanten();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btn_zoek_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();
            if (tb_zoek.Text == "")
            {
                using SqlConnection connection = new SqlConnection(infoConnection);

                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Offertes", connection);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                dataGridView1.DataSource = dtbl;
            }
            else
            {

                bindingSource.DataSource = offerteDAO.searchTitel(tb_zoek.Text);

                dataGridView1.DataSource = bindingSource;
            }
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ConnectDataGridOfferte()
        {

            using SqlConnection connection = new SqlConnection(infoConnection);

            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Offertes", connection);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);

            dataGridView1.DataSource = dtbl;
        }

        private void ConnectDataGridBedrijven()
        {

            using SqlConnection connection = new SqlConnection(infoConnection);

            SqlDataAdapter sqlDa1 = new SqlDataAdapter("SELECT * FROM Bedrijven", connection);
            DataTable dtbl1 = new DataTable();
            sqlDa1.Fill(dtbl1);

            dataGridView2.DataSource = dtbl1;
        }

        private void ConnectDataGridKlanten()
        {

            using SqlConnection connection = new SqlConnection(infoConnection);

            SqlDataAdapter sqlDa2 = new SqlDataAdapter("SELECT * FROM Klanten", connection);
            DataTable dtbl2 = new DataTable();
            sqlDa2.Fill(dtbl2);

            dataGridView3.DataSource = dtbl2;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int tel = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["Id"].Value).Count(x => x != null);
            

                int rowSelected = dataGridView1.CurrentRow.Index;




                OfferteDAO offerteDAO = new OfferteDAO();



                bindingSourceBedrijven.DataSource = offerteDAO.GetBedrijfForOfferte((int)
                    dataGridView1.Rows[rowSelected].Cells[0].Value);



                dataGridView4.DataSource = bindingSourceBedrijven;

                


        
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();

            if (textBox3.Text == "")
            {
                MessageBox.Show("Fill an amount!");
            }
            else 
            {

                bindingSource.DataSource = offerteDAO.ToonBigOffertes(float.Parse(textBox3.Text));

                dataGridView4.DataSource = bindingSource; 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();

            if (textBox2.Text == "")
            {
                using SqlConnection connection = new SqlConnection(infoConnection);

                SqlDataAdapter sqlDa2 = new SqlDataAdapter("SELECT * FROM Klanten", connection);
                DataTable dtbl2 = new DataTable();
                sqlDa2.Fill(dtbl2);

                dataGridView3.DataSource = dtbl2;
            }
            else
            {

                bindingSourceKlanten.DataSource = offerteDAO.searchKlantenNaam(textBox2.Text);

                dataGridView3.DataSource = bindingSourceKlanten;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();
            if (textBox1.Text == "")
            {
                using SqlConnection connection = new SqlConnection(infoConnection);

                SqlDataAdapter sqlDa1 = new SqlDataAdapter("SELECT * FROM Bedrijven", connection);
                DataTable dtbl1 = new DataTable();
                sqlDa1.Fill(dtbl1);

                dataGridView2.DataSource = dtbl1;
            }
            else
            {

                bindingSourceBedrijven.DataSource = offerteDAO.searchBedrijfNaam(textBox1.Text);

                dataGridView2.DataSource = bindingSourceBedrijven;
            }
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            int tel = dataGridView2.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["BedrijfId"].Value).Count(x => x != null);

            if (tel == 0)
            {
                MessageBox.Show("Empty!");
            }
            else
            {
                int rowClicked = dataGridView2.CurrentRow.Index;
                int BedrijfId = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;

                OfferteDAO offerteDAO = new OfferteDAO();

                int result = offerteDAO.deleteBedrijf(BedrijfId);

                dataGridView2.DataSource = null;
                ConnectDataGridBedrijven();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int tel = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["Id"].Value).Count(x => x != null);


            if (tel == 0)
            {
                MessageBox.Show("Empty!");
            }
            else
            {
                int rowClicked = dataGridView1.CurrentRow.Index;
                int OfferteId = (int)dataGridView1.Rows[rowClicked].Cells[0].Value;

                OfferteDAO offerteDAO = new OfferteDAO();

                int result = offerteDAO.deleteOfferte(OfferteId);

                dataGridView1.DataSource = null;
                ConnectDataGridOfferte();
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int tel = dataGridView3.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["KlantId"].Value).Count(x => x != null);

            if (tel == 0)
            {
                MessageBox.Show("Empty!");
            }
            else
            {
                int rowClicked = dataGridView3.CurrentRow.Index;
                int KlantId = (int)dataGridView3.Rows[rowClicked].Cells[0].Value;

                OfferteDAO offerteDAO = new OfferteDAO();

                int result = offerteDAO.deleteKlant(KlantId);

                dataGridView3.DataSource = null;
                ConnectDataGridKlanten();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();

            int tel = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["Id"].Value).Count(x => x != null);

            if (tel == 0)
            {
                MessageBox.Show("No Invoices available");
            }
            else
            {
                MessageBox.Show("The greatest total amount " + offerteDAO.ToonGrootsteOfferte().ToString() + "€");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();
            

                bindingSourceBedrijven.DataSource = offerteDAO.OrderByBedrijfNaamDesc();

                dataGridView2.DataSource = bindingSourceBedrijven;
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int tel = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["Id"].Value).Count(x => x != null);

            MessageBox.Show(tel.ToString() + " Invoices saved");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();


            bindingSourceBedrijven.DataSource = offerteDAO.OrderByBedrijfNaamAsc();

            dataGridView2.DataSource = bindingSourceBedrijven;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int tel = dataGridView2.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["BedrijfId"].Value).Count(x => x != null);

            MessageBox.Show(tel.ToString() + " Companies saved");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OfferteDAO offerteDAO = new OfferteDAO();


            bindingSourceKlanten.DataSource = offerteDAO.OrderByKlantBedrijfNaamDesc();

            dataGridView3.DataSource = bindingSourceKlanten;
        }

        private void button11_Click(object sender, EventArgs e)
        {

            OfferteDAO offerteDAO = new OfferteDAO();


            bindingSourceKlanten.DataSource = offerteDAO.OrderByKlantBedrijfNaamAsc();

            dataGridView3.DataSource = bindingSourceKlanten;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int tel = dataGridView3.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["KlantId"].Value).Count(x => x != null);

            MessageBox.Show(tel.ToString() + " customers saved");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
