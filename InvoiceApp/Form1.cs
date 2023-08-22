namespace InvoiceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 App = new Form2();
            App.MdiParent = this;
            App.Show();
            
        }

        private void closeApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Appi = new Form3();
            
            Appi.Show();
        }
    }
}