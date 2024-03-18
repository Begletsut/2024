using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using PZO_V3.Models;
using MySqlConnector;

namespace PZO_V3
{
    
    public partial class DBForm : MetroFramework.Forms.MetroForm
    {

        public string Title { get; set; }
        public string Table { get; set; }
        public List<string> Cells { get; set; }
        public DBForm(string title, string table, string artbsecond, bool rtbThirdVisible, string rtbThirdText, List<string> cells)
        {
            InitializeComponent();
            Title = title;
            Table = table;
            rtbSecond.Text = artbsecond;
            rtbThird.Visible = rtbThirdVisible;
            rtbThird.Text = rtbThirdText;
            Cells = cells;
            connDB();
            Text = title;
        }
        void connDB()
        {
            string query = $"SELECT * FROM {Table} ORDER BY name ASC";
            MainClass.mySqlSelect(query,gridDB,Table, new List<string>());
        }

        private void btnAddKontragents_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void btnInsertKontragents_Click(object sender, EventArgs e)
        {
            insertOrUpdate("INSERT INTO");
        }

        private void btnUpdateKontragents_Click(object sender, EventArgs e)
        {
            insertOrUpdate("UPDATE");
        }

        private void insertOrUpdate(string cmd)
        {

            string where = string.Empty;
            if (cmd == "UPDATE")
            {
                if (tbId.Text == string.Empty)
                {
                    MessageBox.Show($"First select {Table.Substring(0, Table.Length - 1)}");
                    return;
                }
                where = $"WHERE Id = {tbId.Text}";
            }
            StringBuilder query = new StringBuilder();

            query.Append($"{cmd} {Table} SET Name = '{MySqlHelper.EscapeString(tbName.Text)}'");
            if (Title == "Kontragents")
            {
                if (tbName.Text.Length > 30 ||
                    tbName.Text == string.Empty ||
                    rtbSecond.Text.Length > 255 || 
                    rtbThird.Text.Length > 255)
                {
                    MessageBox.Show("Invalid input in some field");
                    return;
                }
                query.Append($", Address = '{MySqlHelper.EscapeString(rtbSecond.Text)}', Description ='{MySqlHelper.EscapeString(rtbThird.Text)}' {where};");
            }
            else if (Title == "Couriers")
            {
                if (tbName.Text.Length > 30 || 
                    rtbSecond.Text == string.Empty || 
                    rtbSecond.Text.Length > 255)
                {
                    MessageBox.Show("Invalid input in some field");
                    return;
                }
                query.Append($", Link = '{MySqlHelper.EscapeString(rtbSecond.Text)}' {where};");
            }

            MainClass.mySqlCommand(query.ToString(), "Done!");
            connDB();
        }
        private void btnDeleteKontragents_Click(object sender, EventArgs e)
        {
            if (tbId.Text == string.Empty)
            {
                MessageBox.Show("Select record first!");
                return;
            }
            string query = $"DELETE FROM {Table} WHERE Id = '{tbId.Text}'";
            MainClass.mySqlCommand(query, "Done!");
            connDB();
        }

        private void btnExportKontragents_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbSecond.Text);
            Process.Start("Notepad.exe");
            Thread.Sleep(500);
            SendKeys.Send("^V");
        }

        private void textBoxSearchKontagents_TextChanged(object sender, EventArgs e)
        {
            if (Checks.isValidString(textBoxSearchKontagents.Text, 15, true))
            {
                textBoxSearchKontagents.Text = string.Empty;
                return;
            }

            StringBuilder query = new StringBuilder($"SELECT * FROM {Table} WHERE");

            foreach (string cell in Cells)
            {
                query.Append($" {cell.ToString()} LIKE '%{textBoxSearchKontagents.Text}%' OR");
            }
            query.Length-= 2; // to remove last OR
            query.Append($";");
            MainClass.mySqlSelect(query.ToString(), gridDB, Table,new List<string>(0));
        }

        private void btnRefreshTable_Click(object sender, EventArgs e)
        {
            connDB();
            clearFields();
        }

        private void gridDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellCollection currentRow = gridDB.SelectedRows[0].Cells;
            tbId.Text = currentRow["id"].Value.ToString();
            tbName.Text = currentRow["Name"].Value.ToString();
            if (Title == "Couriers")
            {
                rtbSecond.Text = currentRow["Link"].Value.ToString();
            }
            else if (Title == "Kontragents")
            {
                rtbSecond.Text = currentRow["Address"].Value.ToString();
                rtbThird.Text = currentRow["Description"].Value.ToString();
            }
        }
        public void clearFields()
        {
            tbId.Text = string.Empty;
            tbName.Text = string.Empty;
            rtbSecond.Text = string.Empty;
            rtbThird.Text = string.Empty;
        }
    } //class
} //namespace
