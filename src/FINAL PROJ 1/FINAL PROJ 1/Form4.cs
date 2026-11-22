using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\ACER\db\Database2.accdb;Persist Security Info=False;";

        public Form4()
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
            listView1.Columns.Add("Product", 150);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Stock", 100);
            listView1.Columns.Add("Status", 100);

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM Table3";
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["ID"].ToString());
                        item.SubItems.Add(reader["Product"].ToString());
                        item.SubItems.Add(reader["Price"].ToString());
                        item.SubItems.Add(reader["Stock"].ToString());
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
            Login.Form2 f2 = new Login.Form2();
            f2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                   
                    string sql1 = "INSERT INTO Table1 (Product, Price, Stock, Status) VALUES (@Product, @Price, @Stock, @Status)";
                    using (OleDbCommand command1 = new OleDbCommand(sql1, conn))
                    {
                        command1.Parameters.AddWithValue("@Product", txtName.Text);
                        command1.Parameters.AddWithValue("@Price", txtPrice.Text);
                        command1.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                        command1.Parameters.AddWithValue("@Status", cmbStat.Text);
                        command1.ExecuteNonQuery();
                    }

                    string sql2 = "INSERT INTO Table3 (Product, Price, Stock, Status) VALUES (@Product, @Price, @QStock, @Status)";
                    using (OleDbCommand command2 = new OleDbCommand(sql2, conn))
                    {
                        command2.Parameters.AddWithValue("@Product", txtName.Text);
                        command2.Parameters.AddWithValue("@Price", txtPrice.Text);
                        command2.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                        command2.Parameters.AddWithValue("@Status", cmbStat.Text);
                        command2.ExecuteNonQuery();
                    }

                    MessageBox.Show("New record added successfully to both tables.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateListView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

     private void btnAddS_Click(object sender, EventArgs e)
     {
         string actionType = "Add"; 

         try
         {
             using (OleDbConnection conn = new OleDbConnection(connectionString))
             {
                 conn.Open();

              
                 DateTime currentTime = DateTime.Now;

               
                 string sqlTable1 = "INSERT INTO Table1 (Product, Price, Stock, Status) VALUES (@Product, @Price, @Stock, @Status)";
                 using (OleDbCommand commandTable1 = new OleDbCommand(sqlTable1, conn))
                 {
                     commandTable1.Parameters.AddWithValue("@Product", txtName.Text);
                     commandTable1.Parameters.AddWithValue("@Price", txtPrice.Text);
                     commandTable1.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                     commandTable1.Parameters.AddWithValue("@Status", cmbStat.Text);
                     commandTable1.ExecuteNonQuery();
                 }

                 
                 string sqlTable3 = "INSERT INTO Table3 (Product, Price, Stock, Status) VALUES (@Product, @Price, @Stock, @Status)";
                 using (OleDbCommand commandTable3 = new OleDbCommand(sqlTable3, conn))
                 {
                     commandTable3.Parameters.AddWithValue("@Product", txtName.Text);
                     commandTable3.Parameters.AddWithValue("@Price", txtPrice.Text);
                     commandTable3.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                     commandTable3.Parameters.AddWithValue("@Status", cmbStat.Text);
                     commandTable3.ExecuteNonQuery();
                 }

                 
                 string sqlTable4 = "INSERT INTO Table4 (Product, Price, Stock, Status, [When], Type, Details) VALUES (@Product, @Price, @Stock, @Status, @When, @Type, @Details)";
                 using (OleDbCommand commandTable4 = new OleDbCommand(sqlTable4, conn))
                 {
                     commandTable4.Parameters.AddWithValue("@Product", txtName.Text);
                     commandTable4.Parameters.AddWithValue("@Price", txtPrice.Text);
                     commandTable4.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                     commandTable4.Parameters.AddWithValue("@Status", cmbStat.Text);
                     commandTable4.Parameters.AddWithValue("@When", currentTime);
                     commandTable4.Parameters.AddWithValue("@Type", actionType);
                     commandTable4.Parameters.AddWithValue("@Details", "USER"); 
                     commandTable4.ExecuteNonQuery();
                 }

                 MessageBox.Show("New records added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 PopulateListView();
             }
         }
         catch (Exception ex)
         {
             MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
     }



        private void button2_Click_1(object sender, EventArgs e)
        {

            WindowsFormsApplication1.Form5 f5 = new WindowsFormsApplication1.Form5();
            f5.Show();
         
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnSI_Click(object sender, EventArgs e)
        {
            string input = InputBox.Show("Enter the number of stocks to add:", "Add Stocks");
            int numberOfStocks;
            if (int.TryParse(input, out numberOfStocks) && numberOfStocks > 0)
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();

                        DateTime currentTime = DateTime.Now;
  
                        string actionType = "Add";

                        string sqlTable1 = "UPDATE Table1 SET Stock = Stock + ?";
                        using (OleDbCommand commandTable1 = new OleDbCommand(sqlTable1, conn))
                        {
                            commandTable1.Parameters.AddWithValue("@Stock", numberOfStocks);
                            commandTable1.ExecuteNonQuery();
                        }

                        string sqlTable3 = "UPDATE Table3 SET Stock = Stock + ?";
                        using (OleDbCommand commandTable3 = new OleDbCommand(sqlTable3, conn))
                        {
                            commandTable3.Parameters.AddWithValue("@Stock", numberOfStocks);
                            commandTable3.ExecuteNonQuery();
                        }

                        string status = "IN STOCK";
                        if (numberOfStocks < 1000)
                        {
                            status = "Low Stock";
                        }
                        else if (numberOfStocks == 0)
                        {
                            status = "No Stock";
                        }

                        string sqlTable4 = "INSERT INTO Table4 (product, Stock, Status, [When], Type, Details) VALUES (@product, @Stock, @Status, @When, @Type, @Details)";
                        using (OleDbCommand commandTable4 = new OleDbCommand(sqlTable4, conn))
                        {
                            commandTable4.Parameters.AddWithValue("@product", "");
                            commandTable4.Parameters.AddWithValue("@Stock", numberOfStocks);
                            commandTable4.Parameters.AddWithValue("@Status", status);
                            commandTable4.Parameters.AddWithValue("@When", currentTime);
                            commandTable4.Parameters.AddWithValue("@Type", actionType);
                            commandTable4.Parameters.AddWithValue("@Details", "ADMIN");
                            commandTable4.ExecuteNonQuery();
                        }

                        MessageBox.Show("New records added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopulateListView();
                        ClearTextBoxes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating stocks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number.");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string input = InputBox.Show("Enter the number of stocks to subtract:", "Subtract Stocks");
            int numberOfStocks;
            if (int.TryParse(input, out numberOfStocks) && numberOfStocks > 0)
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();

                        string queryTable1 = "INSERT INTO Table4 (Product, Stock, Details, Type, [When]) " +
                                             "SELECT Product, -@StockCount, 'Subtract', 'Subtract', @When FROM Table1 WHERE ID = ?";
                        using (OleDbCommand commandTable1 = new OleDbCommand(queryTable1, conn))
                        {
                            commandTable1.Parameters.AddWithValue("@StockCount", numberOfStocks);
                            commandTable1.Parameters.AddWithValue("@When", DateTime.Now);
                            commandTable1.Parameters.AddWithValue("@ID", Convert.ToInt32(txtID.Text));
                            commandTable1.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating stocks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number.");
            }
        }
        public static class InputBox
        {
            public static string Show(string prompt, string title)
            {
                Form form = new Form();
                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = title;
                label.Text = prompt;
                textBox.Text = "";

                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                DialogResult dialogResult = form.ShowDialog();
                return dialogResult == DialogResult.OK ? textBox.Text : null;
            }
        }

         

        private void AddStocksToDatabase(int numberOfStocks)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

               
                    DateTime currentTime = DateTime.Now;

                
                    string actionType = "Add";

                    string sqlTable1 = "UPDATE Table1 SET Stock = Stock + ?, Status = IIF(Stock + ? < 1000, 'Low Stock', IIF(Stock + ? = 0, 'No Stock', 'IN STOCK'))";
                    using (OleDbCommand commandTable1 = new OleDbCommand(sqlTable1, conn))
                    {
                        commandTable1.Parameters.AddWithValue("@Stock", numberOfStocks);
                        commandTable1.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable1.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable1.ExecuteNonQuery();
                    }

             
                    string sqlTable3 = "UPDATE Table3 SET Stock = Stock + ?, Status = IIF(Stock + ? < 1000, 'Low Stock', IIF(Stock + ? = 0, 'No Stock', 'IN STOCK'))";
                    using (OleDbCommand commandTable3 = new OleDbCommand(sqlTable3, conn))
                    {
                        commandTable3.Parameters.AddWithValue("@Stock", numberOfStocks);
                        commandTable3.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable3.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable3.ExecuteNonQuery();
                    }

                
                    string status = "IN STOCK";
                    if (numberOfStocks < 1000)
                    {
                        status = "Low Stock";
                    }
                    else if (numberOfStocks == 0)
                    {
                        status = "No Stock";
                    }

                 
                    string sqlTable4 = "INSERT INTO Table4 (product, Stock, Status, [When], Type, Details) VALUES (@product, @Stock, @Status, @When, @Type, @Details)";
                    using (OleDbCommand commandTable4 = new OleDbCommand(sqlTable4, conn))
                    {
                        commandTable4.Parameters.AddWithValue("@product", txtName.Text);
                        commandTable4.Parameters.AddWithValue("@Stock", numberOfStocks);
                        commandTable4.Parameters.AddWithValue("@Status", status);
                        commandTable4.Parameters.AddWithValue("@When", currentTime);
                        commandTable4.Parameters.AddWithValue("@Type", actionType);
                        commandTable4.Parameters.AddWithValue("@Details", "ADMIN");
                        commandTable4.ExecuteNonQuery();
                    }

                    MessageBox.Show("New records added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateListView();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating stocks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubtractStocksFromDatabase(int numberOfStocks)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                  
                    DateTime currentTime = DateTime.Now;

                   
                    string actionType = "Subtract";

                    
                    string sqlTable1 = "UPDATE Table1 SET Stock = Stock - ?, Status = IIF(Stock - ? < 1000, 'Low Stock', IIF(Stock - ? = 0, 'No Stock', 'IN STOCK'))";
                    using (OleDbCommand commandTable1 = new OleDbCommand(sqlTable1, conn))
                    {
                        commandTable1.Parameters.AddWithValue("@Stock", numberOfStocks);
                        commandTable1.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable1.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable1.ExecuteNonQuery();
                    }

                  
                    string sqlTable3 = "UPDATE Table3 SET Stock = Stock - ?, Status = IIF(Stock - ? < 1000, 'Low Stock', IIF(Stock - ? = 0, 'No Stock', 'IN STOCK'))";
                    using (OleDbCommand commandTable3 = new OleDbCommand(sqlTable3, conn))
                    {
                        commandTable3.Parameters.AddWithValue("@Stock", numberOfStocks);
                        commandTable3.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable3.Parameters.AddWithValue("@StockCheck", numberOfStocks);
                        commandTable3.ExecuteNonQuery();
                    }

                 
                    string status = "IN STOCK";
                    if (numberOfStocks < 1000)
                    {
                        status = "Low Stock";
                    }
                    else if (numberOfStocks == 0)
                    {
                        status = "No Stock";
                    }

             
                    string sqlTable4 = "INSERT INTO Table4 (product, Stock, Status, [When], Type, Details) VALUES (@product, @Stock, @Status, @When, @Type, @Details)";
                    using (OleDbCommand commandTable4 = new OleDbCommand(sqlTable4, conn))
                    {
                        commandTable4.Parameters.AddWithValue("@product", txtName.Text);
                        commandTable4.Parameters.AddWithValue("@Stock", -numberOfStocks); 
                        commandTable4.Parameters.AddWithValue("@Status", status);
                        commandTable4.Parameters.AddWithValue("@When", currentTime);
                        commandTable4.Parameters.AddWithValue("@Type", actionType);
                        commandTable4.Parameters.AddWithValue("@Details", "ADMIN");
                        commandTable4.ExecuteNonQuery();
                    }

                    MessageBox.Show("Stocks subtracted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateListView();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating stocks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearTextBoxes()
        {
            txtID.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            cmbStat.SelectedIndex = -1;
        }



    }
}
