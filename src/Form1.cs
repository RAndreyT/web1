using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Form1
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\\\ACER\\db\\Database2.accdb;Persist Security Info=False;";

        public Form1()
        {
            InitializeComponent();
            PopulateListView();
            txtID.ReadOnly = true;
        }
        // ... (rest of the code omitted for brevity) ...
    }
}
