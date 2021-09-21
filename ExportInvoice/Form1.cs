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

namespace ExportInvoice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();
            sql.DataSource = textBox1.Text;
            sql.UserID = textBox2.Text;
            sql.Password = textBox3.Text;
            sql.InitialCatalog = "CashExpert";
            SqlConnection connection = new SqlConnection(sql.ConnectionString);
            try
            {
                connection.Open();
                Form2.conStr = sql.ConnectionString;
                Form2 form2 = new Form2();
                form2.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Qoşulma uğursuzdur.");
            }
        }
    }
}
