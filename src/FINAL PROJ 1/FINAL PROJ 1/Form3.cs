using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Register
{
    public partial class Form3 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\ACER\db\Database2.accdb;Persist Security Info=False;";

        public Form3()
        {
            InitializeComponent();

        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string username = txtUname.Text;
            string password = txtPass.Text;
            string role = cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "";
            string name = txtName.Text;
            string address = txtAdd.Text;
            string contact = txtCon.Text;

            if (string.IsNullOrEmpty(username.Trim()) || string.IsNullOrEmpty(password.Trim()) || string.IsNullOrEmpty(role.Trim()) ||
                string.IsNullOrEmpty(name.Trim()) || string.IsNullOrEmpty(address.Trim()) || string.IsNullOrEmpty(contact.Trim()))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO Table2 (Username, [Password], [Role], [Name], [Address], [Contact]) VALUES (@Username, @Password, @Role, @Name, @Address, @Contact)";
                    OleDbCommand command = new OleDbCommand(sql, conn);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Contact", contact);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("New record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add new record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
