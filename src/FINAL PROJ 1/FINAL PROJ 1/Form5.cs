using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\ACER\db\Database2.accdb;Persist Security Info=False;";

        public Form5()
        {
            InitializeComponent();
            PopulateListView();
        }

        private void PopulateListView()
        {
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("When", 150);
            listView1.Columns.Add("Details", 150);
            listView1.Columns.Add("Product", 150);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Stock", 100);
            listView1.Columns.Add("stocks_add_min", 120);
            listView1.Columns.Add("Status", 100);

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM Table4";
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["ID"].ToString());
                        item.SubItems.Add(reader["Type"].ToString());
                        item.SubItems.Add(reader["When"].ToString());
                        item.SubItems.Add(reader["Details"].ToString());
                        item.SubItems.Add(reader["Product"].ToString());
                        item.SubItems.Add(reader["Price"].ToString());
                        item.SubItems.Add(reader["Stock"].ToString());
                        item.SubItems.Add(reader["stocks_add_min"].ToString());
                        item.SubItems.Add(reader["Status"].ToString());
                        listView1.Items.Add(item);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating ListView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Refresh the ListView by calling PopulateListView
            PopulateListView();
        }
    }
}
