using MySqlConnector;
using System.IO;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using MetroFramework.Controls;
using System.Collections.Generic;

namespace PZO_V3.Models
{
    public abstract class DocumentsRepository
    {
        public const string Query = "SELECT * FROM documents {0} ORDER BY DtTm DESC";

        public static int Id { get; set; }
        public static int IdDemand { get; set; }
        public static int IdShipment { get; set; }
        public static int IdInvoice { get; set; }
        public static string FileName { get; set; }

        public static void insertOrUpdate(string cmd, MainForm mainForm)
        {
            if (!Checks.isValidString(mainForm.tbDocumentsName.Text, 50, false) || 
                mainForm.tbDocumentsName.Text.Contains(" ") || 
                !Checks.isValidString(mainForm.rtbDocumentsDescription.Text, 255, true))
            {
                MessageBox.Show("Invalid input in some field");
                return;
            }

            if (mainForm.labelDocDir.Text == "No file selected!")
            {
                MessageBox.Show("Select file first!");
                return;
            }

            string folderDir = string.Empty;

            int idDemandNumber = mainForm.cbDocumentToDemand.Checked ? DemandsRepository.Id : 0;
            int idShipmentNumber = mainForm.cbDocumentToShipment.Checked ? ShipmentsRepository.CurrentShipment.Id : 0;
            int idInvoiceNumber = mainForm.cbDocumentToInvoice.Checked ? InvoicesRepository.Id : 0;
            string fileExtension = Path.GetExtension(mainForm.labelDocDir.Text.ToString());
            DateTime fullDate = DateTime.Parse(mainForm.dateDocuments.Value.ToString());
            string date = fullDate.ToString("yyyy-MM-dd");
            string year = fullDate.ToString("yyyy");
            string month = fullDate.ToString("MM");
            string where = string.Empty;
            if (mainForm.cbDocumentToFolder.Checked)
            {
                if (!mainForm.shipmentHasFolder)
                {
                    MessageBox.Show("The shipment has no folder");
                    return;
                }
                folderDir = $@"{ShipmentsRepository.CourierName}_{ShipmentsRepository.CurrentShipment.Waybill}\";
                fullDate = mainForm.dateShipments.Value;
                year = fullDate.ToString("yyyy");
                month = fullDate.ToString("MM");
            }

            string dirToCheck = $@"{MainForm.selectedDocsFolder}\{year}\{year}-{month}_invoices\{folderDir}";
            if (!Directory.Exists(dirToCheck)) Directory.CreateDirectory(dirToCheck);// if folder doesnt exist, create it check if folder exists                                                           // 

            string fileName = $@"{year}\{year}-{month}_invoices\{folderDir}{date}_Doc_{mainForm.tbDocumentsName.Text}_{fileExtension}";
            if (fileName != FileName)
            {
                int nameCounter = 1;
                while (File.Exists($@"{MainForm.selectedDocsFolder}\{fileName}"))// check if file with same name exists, if exists add (number) to name
                {
                    fileName = $@"{year}\{year}-{month}_invoices\{folderDir}{date}_Doc_{mainForm.tbDocumentsName.Text}_({nameCounter++}){fileExtension}";
                }

                File.Copy(mainForm.labelDocDir.Text, $@"{MainForm.selectedDocsFolder}\{fileName}");
                if (cmd == "UPDATE")
                {
                    where = $" WHERE Id = {Id}";
                    MainClass.svnCommand("remove", $@"{MainForm.selectedDocsFolder}\{FileName}");
                    if (File.Exists($@"{MainForm.selectedDocsFolder}\{FileName}"))
                    {
                        File.Delete($@"{MainForm.selectedDocsFolder}\{FileName}");
                    }
                }
                MainClass.svnCommand("add", dirToCheck);
                MainClass.svnCommand("commit", MainForm.selectedDocsFolder);
            }
            string fileNameCmdMysql = $"FileName = '{MySqlHelper.EscapeString(fileName)}', ";
            if (cmd == "UPDATE")
            {
                where = $" WHERE Id = {Id}";
            }
            string query = $@"{cmd} documents SET 
                IdShipment = '{idShipmentNumber}',
                IdDemand = '{idDemandNumber}',
                IdInvoice = '{idInvoiceNumber}',
                DtTm = '{fullDate.ToString("yyyy-MM-dd HH:mm")}',
                {fileNameCmdMysql}
                Name = '{MySqlHelper.EscapeString(mainForm.tbDocumentsName.Text)}',
                Description = '{MySqlHelper.EscapeString(mainForm.rtbDocumentsDescription.Text)}'
                {where};";
            MainClass.mySqlCommand(query, "Done!");
            MainClass.refreshAllTables(mainForm);
            clearFields(mainForm);
        }

        public static void delete(MainForm mainForm)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                DataGridViewCellCollection selectedRow = mainForm.gridDocuments.CurrentRow.Cells;
                string Id = selectedRow["Id"].Value.ToString();
                File.Delete($@"{MainForm.selectedDocsFolder}\{selectedRow["FileName"].Value.ToString()}");
                MainClass.svnCommand("remove", $@"{MainForm.selectedDocsFolder}\{selectedRow["FileName"].Value.ToString()}");
                MainClass.svnCommand("commit", MainForm.selectedDocsFolder);
                string query = $"DELETE FROM documents WHERE Id = '{Id}'";
                MainClass.mySqlCommand(query, "Done!");
            }
            MainClass.refreshAllTables(mainForm);
        }

        public static void browse(MetroLabel label)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK) label.Text = openFileDialog.FileName;
        }

        public static void OpenFile(DataGridView grid)
        {
            string fileName = $@"{MainForm.selectedDocsFolder}\{grid.CurrentRow.Cells["FileName"].Value.ToString()}";
            if (!File.Exists(fileName))
            {
                MessageBox.Show("File does't exist");
                return;
            }
            Process.Start(fileName);
        }

        public static void OpenDirectory(DataGridView grid)
        {
            string dir = $"/select, \"{MainForm.selectedDocsFolder}\\{grid.CurrentRow.Cells["FileName"].Value.ToString()}\"";
            
            Process.Start("explorer.exe", dir);
        }

        public static void cellClick(MainForm mainForm)
        {

            DataGridViewCellCollection selectedRow = mainForm.gridDocuments.SelectedRows[0].Cells;
            Id = int.Parse(selectedRow["Id"].Value.ToString());
            IdDemand = int.Parse(selectedRow["IdDemand"].Value.ToString());
            IdShipment = int.Parse(selectedRow["IdShipment"].Value.ToString());
            IdInvoice = int.Parse(selectedRow["IdInvoice"].Value.ToString());
            FileName = selectedRow["FileName"].Value.ToString();

            mainForm.dateDocuments.Text = selectedRow["DtTm"].Value.ToString();
            mainForm.tbDocumentsName.Text = selectedRow["Name"].Value.ToString();
            mainForm.rtbDocumentsDescription.Text = selectedRow["Description"].Value.ToString();

            MainClass.findUpperTables(mainForm.gridDocuments, mainForm.gridInvoices, "IdInvoice");
            if (IdInvoice == 0)
            {
                mainForm.cbDocumentToInvoice.Checked = false;
            }
            else
            {
                mainForm.cbDocumentToInvoice.Checked = true;
            }
            if (IdShipment == 0)
            {
                mainForm.cbDocumentToShipment.Checked = false;
            }
            else
            {
                mainForm.cbDocumentToShipment.Checked = true;
                MainClass.findUpperTables(mainForm.gridDocuments, mainForm.gridShipments, "IdShipment");
                ShipmentsRepository.cellClick(mainForm);
                bool documentInFolder = FileName.Contains($"{ShipmentsRepository.CourierName}_{ShipmentsRepository.CurrentShipment.Waybill}");
                if (documentInFolder)
                {
                    mainForm.cbDocumentToFolder.Checked = true;
                }
                else
                {
                    mainForm.cbDocumentToFolder.Checked = false;
                }
            }
            MainClass.findUpperTables(mainForm.gridDocuments, mainForm.gridShipments, "IdShipment");
            if (IdDemand == 0)
            {
                mainForm.cbDocumentToDemand.Checked = false;
            }
            else
            {
                mainForm.cbDocumentToDemand.Checked = true;
            }
            MainClass.findUpperTables(mainForm.gridDocuments, mainForm.gridDemands, "IdDemand");
        }
        public static void textBoxSearch(MainForm mainForm)
        {
            string text = mainForm.tbSearchDocuments.Text;
            if (!Checks.isValidString(text, 15, true))
            {
                mainForm.tbSearchDocuments.Text = string.Empty;
                return;
            }
            string query = $@"
                SELECT * FROM documents 
                WHERE DtTm LIKE '%{text}%' 
                OR Description LIKE '%{text}%' 
                ORDER BY DtTm DESC;";
            MainClass.mySqlSelect(query, mainForm.gridDocuments, "documents", new List<string>());
        }

        public static void checkBoxSameFile(CheckBox checkBox, Label label, DataGridView grid)
        {
            if (checkBox.Checked)
            {
                label.Text = $@"{MainForm.selectedDocsFolder}\{grid.SelectedRows[0].Cells["FileName"].Value.ToString()}";
            }
            else
            {
                label.Text = "No file selected!";
            }
        }
        public static void clearFields(MainForm mainForm)
        {
            mainForm.tbDocumentsName.Text = string.Empty;
            mainForm.rtbDocumentsDescription.Text = string.Empty;
            mainForm.tbSearchDocuments.Text = string.Empty;
            mainForm.cbDocumentToDemand.Checked = false;
            mainForm.cbDocumentToShipment.Checked = false;
            mainForm.cbDocumentToInvoice.Checked = false;
            mainForm.cbDocumentToFolder.Checked = false;
            mainForm.labelDocDir.Text = "No file selected!";
            mainForm.cbDocSameFile.Checked = false;
        }
    }// class
}// namespace
