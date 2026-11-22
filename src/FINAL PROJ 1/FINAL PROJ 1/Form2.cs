using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Login
{
    public partial class Form2 : Form
        {
            
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\ACER\db\Database2.accdb;Persist Security Info=False;";

            public Form2()
            {
                InitializeComponent();
            }
            private void btnLogin_Click(object sender, EventArgs e)
            {
                string username = txtUname.Text;
                string password = txtPass.Text;

                
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    
                    if (LoginUser(username, password))
                    {
                        MessageBox.Show("Login successful!");
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both username and password!");
                }
            }

            private bool LoginUser(string username, string password)
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();

                        
                        string sql = "SELECT COUNT(*) FROM Table2 WHERE StrComp(Username, @Username, 0) = 0 AND StrComp([Password], @Password, 0) = 0";
                        using (OleDbCommand command = new OleDbCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);
                            int count = (int)command.ExecuteScalar();

                            return count > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while attempting to login: " + ex.Message);
                    return false;
                }
            }

            private void btnLogin_Click_1(object sender, EventArgs e)
            {
                string username = txtUname.Text;
                string password = txtPass.Text;

                if (string.IsNullOrEmpty(username.Trim()) || string.IsNullOrEmpty(password.Trim()))
                {
                    MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();

                      
                        string sql = "SELECT Role FROM Table2 WHERE StrComp(Username, @Username, 0) = 0 AND StrComp([Password], @Password, 0) = 0";
                        using (OleDbCommand command = new OleDbCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);
                            object roleObj = command.ExecuteScalar();

                            if (roleObj != null)
                            {
                                string role = roleObj.ToString();

                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (role == "Admin")
                                {
                                    
                                    Form1.Form1 f1 = new Form1.Form1();
                                    f1.Show();
                                }
                                else if (role == "User")
                                {
                                    
                                    WindowsFormsApplication1.Form4 f4 = new WindowsFormsApplication1.Form4();
                                    f4.Show();
                                }

                               
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            private void btnReg_Click_1(object sender, EventArgs e)
            {
                Register.Form3 f3 = new Register.Form3();
                f3.Show();
            }

            private void Form2_Load(object sender, EventArgs e)
            {

            }
        }
    }


