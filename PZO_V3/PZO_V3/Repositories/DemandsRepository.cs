using System.Windows.Forms;
using System;
using System.Collections.Generic;
using MySqlConnector;

namespace PZO_V3.Models
{
    public abstract class DemandsRepository

    {
        public static int Id { get; set; }
        public static int IdKontragent { get; set; }
        public const string Query = @"
                SELECT demands.*, kontragents.Name AS Kontragent FROM demands 
                JOIN kontragents ON demands.IdKontragent = kontragents.Id {0} ORDER BY DtTm DESC;";
        public static void InsertOrUpdate(string cmd, MainForm mainForm)
        {
            if (!Checks.isValidString(mainForm.rtbDemandDescription.Text, 255, false) || 
                !Checks.isValidString(mainForm.tbDemandKontragent.Text, 30, false) || 
                mainForm.cobDemandStatus.Text == "Status")
            {
                MessageBox.Show("Invalid input in some field");
                return;
            }
            string where = string.Empty;

            if (cmd == "UPDATE")
            {
                if (Id == 0)
                {
                    MessageBox.Show("First select Demand which you want to update.");
                    return;
                }
                where = $"WHERE Id = '{Id}'";
            }
            string Status = mainForm.cobDemandStatus.Text;

            string date = mainForm.dateDemands.Value.ToString("yyyy-MM-dd HH:mm");

            string query = $@"{cmd} demands SET 
                IdKontragent = '{IdKontragent}',
                DtTm = '{date}',
                Status = '{Status}',
                Description = '{MySqlHelper.EscapeString(mainForm.rtbDemandDescription.Text)}' {where};";
            MainClass.mySqlCommand(query, "Done!");
            MainClass.refreshAllTables(mainForm);
            clearFields(mainForm);
        }

        public static void Delete(MainForm mainForm)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataGridViewRow selectedRow = mainForm.gridDemands.SelectedRows[0];
                string Id = selectedRow.Cells["Id"].Value.ToString();
                string query = $"DELETE FROM demands WHERE Id = '{Id}'";
                MainClass.mySqlCommand(query, "Done!");
                MainClass.refreshAllTables(mainForm);
                clearFields(mainForm);
            }
        }

        public static void textBoxSearch(MainForm mainForm)
        {
            string text = mainForm.tbSearchDemands.Text;
            if (!Checks.isValidString(text, 15, true))
            {
                mainForm.tbSearchDemands.Text = string.Empty;
                return;
            }
            string query = $@"SELECT demands.*, kontragents.Name AS Kontragent 
                FROM demands JOIN kontragents ON demands.IdKontragent = kontragents.Id 
                WHERE kontragents.Name LIKE '%{text}%' 
                OR demands.DtTm LIKE '%{text}%'
                OR demands.Description LIKE '%{text}%' 
                ORDER BY demands.DtTm DESC;";
            MainClass.mySqlSelect(query, mainForm.gridDemands, "demands", new List<string>());
        }

        public static void cellClick(MainForm mainForm)
        {
            DataGridViewCellCollection selectedRow = mainForm.gridDemands.SelectedRows[0].Cells;
            Id = int.Parse(selectedRow["Id"].Value.ToString());
            IdKontragent = int.Parse(selectedRow["IdKontragent"].Value.ToString());
            mainForm.dateDemands.Value = DateTime.Parse(selectedRow["DtTm"].Value.ToString());
            mainForm.cobDemandStatus.Text = selectedRow["Status"].Value.ToString();
            mainForm.tbDemandKontragent.Text = selectedRow["Kontragent"].Value.ToString();
            mainForm.rtbDemandDescription.Text = selectedRow["Description"].Value.ToString();
        }

        public static void clearFields(MainForm mainForm)
        {
            Id = 0;
            IdKontragent = 0;
            mainForm.rtbDemandDescription.Text = string.Empty;
            mainForm.cobDemandStatus.Text = null;
            mainForm.tbDemandKontragent.Text = string.Empty;
            mainForm.tbSearchDemands.Text = string.Empty;
        }
    }// class
}// namespace
