using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Form1
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\ACER\db\Database2.accdb;Persist Security Info=False;";

        public Form1()
        {
            InitializeComponent();
            PopulateListView();
            txtID.ReadOnly = true;
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

                    string sql = "SELECT * FROM Table1";
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["ID"].ToString()); // Add ID to the list view
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

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string actionType = "Add"; 

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                  
                    DateTime currentTime = DateTime.Now;

                 
                    string sql = "INSERT INTO Table1 (Product, Price, Stock, Status) VALUES (@Product, @Price, @Stock, @Status)";
                    using (OleDbCommand command = new OleDbCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@Product", txtName.Text);
                        command.Parameters.AddWithValue("@Price", txtPrice.Text);
                        command.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                        command.Parameters.AddWithValue("@Status", cmbStat.Text);
                        command.ExecuteNonQuery();
                    }
                    string sql3 = "INSERT INTO Table3 (Product, Price, Stock, Status) VALUES (@Product, @Price, @Stock, @Status)";
                    using (OleDbCommand command = new OleDbCommand(sql3, conn))
                    {
                        command.Parameters.AddWithValue("@Product", txtName.Text);
                        command.Parameters.AddWithValue("@Price", txtPrice.Text);
                        command.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                        command.Parameters.AddWithValue("@Status", cmbStat.Text);
                        command.ExecuteNonQuery();
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
                        commandTable4.Parameters.AddWithValue("@Details", "ADMIN"); // Add "USER" to the Details column
                        commandTable4.ExecuteNonQuery();
                    }

                    MessageBox.Show("New records added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateListView();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                txtID.Text = selectedItem.SubItems[0].Text;

              
                txtName.Enabled = true;
                txtPrice.Enabled = true;
                txtQuantity.Enabled = true;
                cmbStat.Enabled = true;

                txtName.Text = selectedItem.SubItems[1].Text;
                txtPrice.Text = selectedItem.SubItems[2].Text;
                txtQuantity.Text = selectedItem.SubItems[3].Text;
                cmbStat.Text = selectedItem.SubItems[4].Text;
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedRecordID = Convert.ToInt32(txtID.Text);

                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();

                        
                        string sqlTable1 = "UPDATE Table1 SET Product = @Product, Price = @Price, Stock = @Stock, [Status] = @Status WHERE ID = @ID";
                        using (OleDbCommand commandTable1 = new OleDbCommand(sqlTable1, conn))
                        {
                            commandTable1.Parameters.AddWithValue("@Product", txtName.Text);
                            commandTable1.Parameters.AddWithValue("@Price", txtPrice.Text);
                            commandTable1.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                            commandTable1.Parameters.AddWithValue("@Status", cmbStat.Text);
                            commandTable1.Parameters.AddWithValue("@ID", selectedRecordID);

                            int rowsAffectedTable1 = commandTable1.ExecuteNonQuery();

                            if (rowsAffectedTable1 <= 0)
                            {
                                MessageBox.Show("Failed to update Table1 record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        string sqlTable3 = "UPDATE Table3 SET Product = @Product, Price = @Price, Stock = @Stock, [Status] = @Status WHERE ID = @ID";
                        using (OleDbCommand commandTable3 = new OleDbCommand(sqlTable3, conn))
                        {
                            commandTable3.Parameters.AddWithValue("@Product", txtName.Text);
                            commandTable3.Parameters.AddWithValue("@Price", txtPrice.Text);
                            commandTable3.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                            commandTable3.Parameters.AddWithValue("@Status", cmbStat.Text);
                            commandTable3.Parameters.AddWithValue("@ID", selectedRecordID);

                            int rowsAffectedTable3 = commandTable3.ExecuteNonQuery();

                            if (rowsAffectedTable3 <= 0)
                            {
                                MessageBox.Show("Failed to update Table3 record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                     
                        string sqlTable4 = "INSERT INTO Table4 (Product, Price, Stock, Status, [When], Type, Details) VALUES (@Product, @Price, @Stock, @Status, @When, @Type, @Details)";
                        using (OleDbCommand commandTable4 = new OleDbCommand(sqlTable4, conn))
                        {
                            commandTable4.Parameters.AddWithValue("@Product", txtName.Text);
                            commandTable4.Parameters.AddWithValue("@Price", txtPrice.Text);
                            commandTable4.Parameters.AddWithValue("@Stock", txtQuantity.Text);
                            commandTable4.Parameters.AddWithValue("@Status", cmbStat.Text);
                            commandTable4.Parameters.AddWithValue("@When", DateTime.Now);
                            commandTable4.Parameters.AddWithValue("@Type", "Edit");
                            commandTable4.Parameters.AddWithValue("@Details", "ADMIN"); // Add "ADMIN" to the Details column
                            commandTable4.ExecuteNonQuery();
                        }

                        PopulateListView();
                        ClearTextBoxes();
                        MessageBox.Show("Records updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    int selectedRecordID = Convert.ToInt32(listView1.SelectedItems[0].Text);

                    DialogResult result = MessageBox.Show("Are you sure you want to delete all records with ID " + selectedRecordID + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (OleDbConnection conn = new OleDbConnection(connectionString))
                        {
                            conn.Open();

                           
                            string sqlTable4 = "INSERT INTO Table4 (Product, Price, Stock, Status, [When], Type, Details) " +
                                               "SELECT Product, Price, Stock, Status, @When, 'Delete', @Details FROM Table1 WHERE ID = @ID";
                            using (OleDbCommand commandTable4 = new OleDbCommand(sqlTable4, conn))
                            {
                                commandTable4.Parameters.AddWithValue("@When", DateTime.Now);
                                commandTable4.Parameters.AddWithValue("@Details", "ADMIN");
                                commandTable4.Parameters.AddWithValue("@ID", selectedRecordID);
                                int rowsAffectedTable4 = commandTable4.ExecuteNonQuery();
                                if (rowsAffectedTable4 <= 0)
                                {
                                    MessageBox.Show("Failed to insert record into Table4.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return; 
                                }
                            }
                            string sqlTable1 = "DELETE FROM Table1 WHERE ID = @ID";
                            using (OleDbCommand commandTable1 = new OleDbCommand(sqlTable1, conn))
                            {
                                commandTable1.Parameters.AddWithValue("@ID", selectedRecordID);

                                int rowsAffectedTable1 = commandTable1.ExecuteNonQuery();
                                if (rowsAffectedTable1 > 0)
                                {
                                    MessageBox.Show("Records with ID " + selectedRecordID + " deleted successfully from Table1.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No records found with ID " + selectedRecordID + " in Table1.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            // Delete from Table3
                            string sqlTable3 = "DELETE FROM Table3 WHERE ID = @ID";
                            using (OleDbCommand commandTable3 = new OleDbCommand(sqlTable3, conn))
                            {
                                commandTable3.Parameters.AddWithValue("@ID", selectedRecordID);

                                int rowsAffectedTable3 = commandTable3.ExecuteNonQuery();
                                if (rowsAffectedTable3 > 0)
                                {
                                    MessageBox.Show("Records with ID " + selectedRecordID + " deleted successfully from Table3.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No records found with ID " + selectedRecordID + " in Table3.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            PopulateListView();
                            ClearTextBoxes();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a record to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void auditLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form5 f5 = new WindowsFormsApplication1.Form5();
            f5.Show();
            //this.Close();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login.Form2 f2 = new Login.Form2();
            f2.Show();
            this.Close();
        }

        private void ClearTextBoxes()
        {
            txtID.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            cmbStat.SelectedIndex = -1; // Reset the combo box
        }

        private void btnSO_Click(object sender, EventArgs e)
        {
            string input = InputBox.Show("Enter the number of stocks to subtract:", "Subtract Stocks");
            int numberOfStocks; // Declare the variable here
            if (int.TryParse(input, out numberOfStocks) && numberOfStocks > 0)
            {
                SubtractStocksFromDatabase(numberOfStocks);
                MessageBox.Show(string.Format("{0} stocks have been subtracted from the database.", numberOfStocks));
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

        private void btnS_Click(object sender, EventArgs e)
        {
            string input = InputBox.Show("Enter the number of stocks to add:", "Add Stocks");
            int numberOfStocks; 
            if (int.TryParse(input, out numberOfStocks) && numberOfStocks > 0)
            {
                AddStocksToDatabase(numberOfStocks);
                MessageBox.Show(string.Format("{0} stocks have been added to the database.", numberOfStocks));
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number.");
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
                        commandTable4.Parameters.AddWithValue("@Stock", -numberOfStocks); // Subtract stocks
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


    }
}
