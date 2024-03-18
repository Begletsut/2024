using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace EMC_Testbench
{
    public partial class DBForm : MetroFramework.Forms.MetroForm
    {
        public DBForm()
        {
            InitializeComponent();
            connDB();
        }
        string checkInternalOrExternal()
        {
            List<string> computerName = new List<string> { "DESKTOP-28P6C2K", "Zlati"};
            bool isLocal = computerName.Contains(Environment.MachineName.ToString());
            string result = "server =XXX.XXX.X.XXX; port =0000; user =user; password =pass; database = db";
            if (!isLocal)
            {
                result = "server =XXX.XXX.X.XXX; port =0000; user =user; password =pass; database = db";
            }
            return result;
        }
        public MySqlConnection connection { get{ return new MySqlConnection(checkInternalOrExternal()); } }

        public void connDB()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM testedemc ORDER BY Date DESC", connection);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.Fill(ds, "testedemc");
                gridTestedEmc.DataSource = ds.Tables["testedemc"];
                connection.Close();
                gridTestedEmc.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void insertOrUpdate(string pCommand)
        {

            string where = string.Empty;
            DataGridViewRow selectedRow = gridTestedEmc.SelectedRows[0];
            string Id = selectedRow.Cells["Id"].Value.ToString();
            string SN = selectedRow.Cells["SN"].Value.ToString();
            string AinVPower = selectedRow.Cells["AinVPower"].Value.ToString();
            string Keyboard = selectedRow.Cells["Keyboard"].Value.ToString();
            string BootloaderVersion = selectedRow.Cells["BootloaderVersion"].Value.ToString();
            string AppVersion = selectedRow.Cells["AppVersion"].Value.ToString();
            string Engine = selectedRow.Cells["Engine"].Value.ToString();
            string Oxygen = selectedRow.Cells["Oxygen"].Value.ToString();
            string RPM = selectedRow.Cells["RPM"].Value.ToString();
            string POut = selectedRow.Cells["POut"].Value.ToString();
            string LINMT05 = selectedRow.Cells["LINMT05"].Value.ToString();
            string LINCO = selectedRow.Cells["LINCO"].Value.ToString();
            string StepMotor = selectedRow.Cells["StepMotor"].Value.ToString();
            string Flash = selectedRow.Cells["Flash"].Value.ToString();

            if (pCommand == "UPDATE")
            {
                where = $" WHERE Id = '{Id}'";
            }

            string query = $@"{pCommand} testedemc SET 
                SN = '{SN}',
                AinVPower = '{AinVPower}',
                Keyboard = '{Keyboard}',
                BootloaderVersion = '{BootloaderVersion}',
                AppVersion = '{AppVersion}',
                Engine = '{Engine}',
                Oxygen = '{Oxygen}',
                RPM = '{RPM}',
                POut = '{POut}',
                LINMT05 = '{LINMT05}',
                LINCO = '{LINCO}',
                StepMotor = '{StepMotor}',
                Flash = '{Flash}',
                Date = '{DateTime.Now}'{where};";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            connDB();
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)gridTestedEmc.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            dataTable.Rows.InsertAt(drToAdd, 0);
            dataTable.AcceptChanges();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text.Contains("\'") || textBoxSearch.Text.Contains("\""))
            {
                textBoxSearch.Text = string.Empty;
                return;
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM testedemc WHERE SN LIKE '%{textBoxSearch.Text}%' OR AinVPower LIKE '%{textBoxSearch.Text}%' OR Keyboard LIKE '%{textBoxSearch.Text}%' OR BootloaderVersion LIKE '%{textBoxSearch.Text}%' OR AppVersion LIKE '%{textBoxSearch.Text}%' OR Engine LIKE '%{textBoxSearch.Text}%' OR Oxygen LIKE '%{textBoxSearch.Text}%' OR RPM LIKE '%{textBoxSearch.Text}%' OR POut LIKE '%{textBoxSearch.Text}%' OR LINMT05 LIKE '%{textBoxSearch.Text}%' OR LINCO LIKE '%{textBoxSearch.Text}%' OR StepMotor LIKE '%{textBoxSearch.Text}%' OR Flash LIKE '%{textBoxSearch.Text}%' OR Date LIKE '%{textBoxSearch.Text}%';", connection);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                adapter.Fill(ds, "testedemc");
                gridTestedEmc.DataSource = ds.Tables["testedemc"];
                connection.Close();
                gridTestedEmc.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            insertOrUpdate("INSERT INTO");
            MessageBox.Show("Successfully inserted!");
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            insertOrUpdate("UPDATE");
            MessageBox.Show("Successfully updated!");
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                DataGridViewRow selectedRow = gridTestedEmc.SelectedRows[0];
                string Id = selectedRow.Cells["Id"].Value.ToString();
                string query = $"DELETE FROM testedemc WHERE Id = '{Id}'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                connDB();
                MessageBox.Show("Successfully deleted!");
            }
        }

        private void DBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            connDB();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SN\tAinVpower\tKeyboard\tBootloaderVersion\tAppVersion\tEngine\tOxygen\tRPM\tPOut\tLINMT05\tLINCO\tStepMotor\tFlash\tDate");
            foreach (DataGridViewRow row in gridTestedEmc.Rows)
            {
                bool firstPassed = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!firstPassed)
                    {
                        firstPassed = true;
                        continue;
                    }
                    stringBuilder.Append($"{cell.Value}\t");
                }
                stringBuilder.AppendLine("");
            }
            File.WriteAllText(Path.Combine(Application.StartupPath, "table.xls"), stringBuilder.ToString());
            MessageBox.Show("Successfully exported!");
        }
    }
}