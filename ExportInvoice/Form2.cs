using ClosedXML.Excel;
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
    public partial class Form2 : Form
    {
        public static string conStr;
        DataTable td;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Invoice id yanlışdır.");
                return;
            }
            else
            {
                try
                {
                    int a = int.Parse(textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("Invoice id yanlışdır.");
                    return;
                }
            }

            int id = int.Parse(textBox1.Text);

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = con;
                        command.CommandText = "SELECT * FROM [CashExpert].[dbo].[InvoiceDetails] where InvoiceID = @id";
                        command.Parameters.AddWithValue("id", id);

                        using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            command.ExecuteNonQuery();

                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            td = dt;
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Invoice_" + textBox1.Text + ".xlsx";
            saveFile.ShowDialog();


            XLWorkbook wb = new XLWorkbook();
            td.Columns.Remove(td.Columns[13]);
            wb.Worksheets.Add(td, "WorksheetName").Column(14).Width = 100;
            wb.SaveAs(saveFile.FileName);
            MessageBox.Show("Uğurlu.");
        }
    }
}
