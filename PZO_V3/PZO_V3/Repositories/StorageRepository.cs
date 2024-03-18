using System.Windows.Forms;
using System;
using System.Linq;
using System.Collections.Generic;
using MySqlConnector;

namespace PZO_V3.Models
{
    public abstract class StorageRepository
    {
        public const string Query = "SELECT * FROM storage {0} ORDER BY name ASC";

        public static int Id {  get; set; }

        public static void InsertOrUpdate(string cmd, MainForm mainForm)
        {
            if (!Checks.isValidString(mainForm.tbStorageName.Text, 255, false) ||
                !Checks.isValidInt(mainForm.tbStorageQuantity.Text) ||
                !Checks.isValidString(mainForm.rtbStorageDescription.Text, 255, true))
            {
                MessageBox.Show("Invalid input in some field");
                return;
            }
            string changelog = mainForm.tbStorageChangelog.Text == string.Empty ? "MODIFIED" : mainForm.tbStorageChangelog.Text;
            string QuantityChanged = mainForm.tbStorageQtyToChange.Text == string.Empty ? "0" : mainForm.tbStorageQtyToChange.Text;
            string query = $@"
                    {cmd} storage SET 
                    Name = '{MySqlHelper.EscapeString(mainForm.tbStorageName.Text)}',
                    Quantity = Quantity + {QuantityChanged},
                    Description = '{MySqlHelper.EscapeString(mainForm.rtbStorageDescription.Text)}'";
            if (cmd == "UPDATE")
            {// add to storage + update trigger
                query += $@" WHERE Id = '{Id}';
                        INSERT INTO storagedetails SET IdStorage = '{Id}',
                        QuantityChanged = '{QuantityChanged}',
                        Quantity = {mainForm.tbStorageQuantity.Text},
                        ChangeLog = '{changelog}',
                        DtTm = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}'";

            }
            query += ";";
            MainClass.mySqlCommand(query, "Done!");
            MainClass.refreshAllTables(mainForm);
            clearFields(mainForm);
        }
        public static void addArticleFromStorage(int idStorage, string name) // add articles from storage to Articles grid
        {
            if (ShipmentsRepository.CurrentShipment.Id == 0)
            {
                MessageBox.Show("Select shipment first!");
                return;
            }
            if (ArticlesRepository.articlesInGrid.Any(x => x.IdStorage == idStorage))
            {
                MessageBox.Show("Article is already in Grid");
                return;
            }
            ShowDialogForm dialogForm = new ShowDialogForm("Insert Quantity", "Ok", "Cancel", true);
            dialogForm.ShowDialog();

            if (dialogForm.DialogResult == DialogResult.OK && dialogForm.textBoxInput.Text != string.Empty)
            {
                ArticlesRepository.articlesInGrid.Add(new Article(0, idStorage, int.Parse(dialogForm.textBoxInput.Text), name,DateTime.Now));
            }
        }

        public static void cellClick(MainForm mainForm)
        {
            DataGridViewCellCollection selectedRow = mainForm.gridStorage.SelectedRows[0].Cells;
            Id = int.Parse(selectedRow["Id"].Value.ToString());
            mainForm.tbStorageName.Text = selectedRow["Name"].Value.ToString();
            mainForm.tbStorageQuantity.Text = selectedRow["Quantity"].Value.ToString();
            mainForm.rtbStorageDescription.Text = selectedRow["Description"].Value.ToString();

            MainClass.mySqlSelect(string.Format(MainClass.QuerySingleLog, Id), mainForm.gridDetails, "storagedetails", new List<string>() { "Id", "IdStorage" });
            mainForm.gridDetails.Columns["DtTm"].Width = 100;
            mainForm.gridDetails.Columns["DtTm"].DisplayIndex = 0;

        }

        public static void delete(MainForm mainForm)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (mainForm.gridStorage.SelectedRows.Count == 0)
                {
                    return;
                }
                string query = $"DELETE FROM storage WHERE Id = '{Id}'";
                MainClass.mySqlCommand(query, "Done!");
                MainClass.refreshAllTables(mainForm);
            }
        }
        public static void textBoxSearch(MainForm mainForm)
        {
            string text = mainForm.tbStorageSearch.Text;
            if (!Checks.isValidString(text, 15, true))
            {
                mainForm.tbStorageSearch.Text = string.Empty;
                return;
            }
            string query = $@"
                SELECT * FROM storage 
                WHERE Name LIKE '%{text}%' 
                OR Quantity LIKE '%{text}%' 
                OR Description LIKE '%{text}%' 
                ORDER BY name DESC;";
            MainClass.mySqlSelect(query, mainForm.gridStorage, "storage", new List<string>());
        }
        public static void clearFields(MainForm mainForm)
        {
            Id = 0;
            mainForm.tbStorageName.Text = string.Empty;
            mainForm.tbStorageQuantity.Text = string.Empty;
            mainForm.tbStorageQtyToChange.Text = string.Empty;
            mainForm.rtbStorageDescription.Text = string.Empty;
            mainForm.tbStorageSearch.Text = string.Empty;
            mainForm.tbStorageChangelog.Text = string.Empty;
        }
    } // class
}// namespace
