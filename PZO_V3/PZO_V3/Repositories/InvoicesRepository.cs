using MetroFramework.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PZO_V3.Models
{
    public abstract class InvoicesRepository
    {
        public static int Id { get; set; }
        public static int IdShipment { get; set; }
        public static int IdKontragentFrom { get; set; }
        public static int IdKontragentTo { get; set; }
        public static int IdDemand { get; set; }
        public static string FileName { get; set; }
        public const string Query = @"
                SELECT i.*, kf.Name AS 'From', kt.Name AS 'To' FROM invoices i 
                JOIN kontragents kf ON i.IdKontragentFrom = kf.Id 
                JOIN kontragents kt ON i.IdKontragentTo = kt.Id {0} ORDER BY DtTm DESC";

        public static void insertOrUpdate(string cmd, MainForm mainForm)//rename, preformat and copy doc to new dir
        {
            if (mainForm.labelInvoiceDir.Text == "No file selected!")
            {
                MessageBox.Show("Select file first!");
                return;
            }
            string from = mainForm.tbInvoicesFrom.Text;
            string to = mainForm.tbInvoicesTo.Text;

            if (!Checks.isValidString(from, 50, false) ||
                !Checks.isValidString(to, 50, false) ||
                !Checks.isValidString(mainForm.tbInvoicesNumber.Text, 50, false) ||
                !Checks.isValidDouble(mainForm.tbInvoicesPrice.Text) ||
                !Checks.isValidString(mainForm.rtbInvoicesDescription.Text, 255, true))
            {
                MessageBox.Show("Invalid input in some field");
                return;
            }

            DateTime fullDate = mainForm.dateInvoices.Value;
            string date = fullDate.ToString("yyyy-MM-dd");
            string year = fullDate.ToString("yyyy");
            string month = fullDate.ToString("MM");
            string price = mainForm.tbInvoicesPrice.Text.ToString().Replace(",", ".");
            int idDemandNumber = mainForm.cbInvoiceToDemand.Checked ? DemandsRepository.Id : 0;
            int idShipmentNumber = mainForm.cbInvoiceToShipment.Checked ? ShipmentsRepository.CurrentShipment.Id : 0;
            string fileExtension = Path.GetExtension(mainForm.labelInvoiceDir.Text.ToString());
            string folderDir = string.Empty;
            string where = string.Empty;
            if (mainForm.cbInvoiceToFolder.Checked)
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

            string fileName = $@"{year}\{year}-{month}_invoices\{folderDir}{date}_Invoice_{from}_TO_{to},{price}{fileExtension}";
            if (fileName != FileName)
            {

                int nameCounter = 1;
                while (File.Exists($@"{MainForm.selectedDocsFolder}\{fileName}"))// check if file with same name exists, if exists add (number) to name
                {
                    fileName = $@"{year}\{year}-{month}_invoices\{folderDir}{date}_Invoice_{from}_TO_{to},{price}({nameCounter++}){fileExtension}";
                }

                File.Copy(mainForm.labelInvoiceDir.Text, $@"{MainForm.selectedDocsFolder}\{fileName}");
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
            string query = $@"{cmd} invoices SET 
                IdKontragentFrom = {IdKontragentFrom},
                IdKontragentTo = {IdKontragentTo},
                IdShipment = {idShipmentNumber},
                IdDemand = {idDemandNumber},
                DtTm = '{fullDate:yyyy-MM-dd HH:mm}',
                TotalPrice = {price},
                Number = '{MySqlHelper.EscapeString(mainForm.tbInvoicesNumber.Text)}',
                {fileNameCmdMysql}
                Description = '{MySqlHelper.EscapeString(mainForm.rtbInvoicesDescription.Text)}'
                {where};";

            MainClass.mySqlCommand(query, "Done!");
            MainClass.refreshAllTables(mainForm);
            clearFields(mainForm);
        }
        public static void delete(MainForm mainForm)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                DataGridViewCellCollection currentRow = mainForm.gridInvoices.SelectedRows[0].Cells;
                string Id = currentRow["Id"].Value.ToString();
                MainClass.svnCommand("remove", $@"{MainForm.selectedDocsFolder}\{currentRow["FileName"].Value}");
                MainClass.svnCommand("commit", MainForm.selectedDocsFolder);

                File.Delete(MainForm.selectedDocsFolder + "\\\\" + currentRow["FileName"].Value.ToString());
                string query = $"DELETE FROM invoices WHERE Id = '{Id}'";
                MainClass.mySqlCommand(query, "Done!");
                MainClass.refreshAllTables(mainForm);
            }
        }

        public static void cellClick(MainForm mainForm)
        {
            DataGridViewCellCollection selectedRow = mainForm.gridInvoices.SelectedRows[0].Cells;
            Id = int.Parse(selectedRow["Id"].Value.ToString());
            IdDemand = int.Parse(selectedRow["IdDemand"].Value.ToString());
            IdShipment = int.Parse(selectedRow["IdShipment"].Value.ToString());
            IdKontragentFrom = int.Parse(selectedRow["IdKontragentFrom"].Value.ToString());
            IdKontragentTo = int.Parse(selectedRow["IdKontragentTo"].Value.ToString());
            FileName = selectedRow["FileName"].Value.ToString();

            mainForm.dateInvoices.Value = DateTime.Parse(selectedRow["DtTm"].Value.ToString());
            mainForm.tbInvoicesFrom.Text = selectedRow["From"].Value.ToString();
            mainForm.tbInvoicesTo.Text = selectedRow["To"].Value.ToString();
            mainForm.tbInvoicesNumber.Text = selectedRow["Number"].Value.ToString();
            mainForm.tbInvoicesPrice.Text = selectedRow["TotalPrice"].Value.ToString();
            mainForm.rtbInvoicesDescription.Text = selectedRow["Description"].Value.ToString();

            if (IdDemand == 0)
            {
                mainForm.cbInvoiceToDemand.Checked = false;
            }
            else
            {
                mainForm.cbInvoiceToDemand.Checked = true;
                MainClass.findUpperTables(mainForm.gridInvoices, mainForm.gridDemands, "IdDemand");
                DemandsRepository.cellClick(mainForm);
            }
            if (IdShipment == 0)
            {
                mainForm.cbInvoiceToShipment.Checked = false;
            }
            else
            {
                mainForm.cbInvoiceToShipment.Checked = true;
                MainClass.findUpperTables(mainForm.gridInvoices, mainForm.gridShipments, "IdShipment");
                ShipmentsRepository.cellClick(mainForm);
                bool invoiceInFolder = FileName.Contains($"{ShipmentsRepository.CourierName}_{ShipmentsRepository.CurrentShipment.Waybill}");
                if (invoiceInFolder)
                {
                    mainForm.cbInvoiceToFolder.Checked = true;
                }
                else
                {
                    mainForm.cbInvoiceToFolder.Checked = false;
                }
            }
            
        }

        public static void textBoxSearch(MainForm mainForm)
        {
            string text = mainForm.tbSearchInvoices.Text;
            if (!Checks.isValidString(text, 15, true))
            {
                mainForm.tbSearchInvoices.Text = string.Empty;
                return;
            }
            string query = $@"
                SELECT i.*, kf.Name AS 'From', kt.Name AS 'To' FROM invoices i 
                JOIN kontragents kf ON i.IdKontragentFrom = kf.Id 
                JOIN kontragents kt ON i.IdKontragentTo = kt.Id 
                WHERE kf.Name LIKE '%{text}%' 
                OR kt.Name LIKE '%{text}%' 
                OR i.DtTm LIKE '%{text}%' 
                OR i.Number LIKE '%{text}%' 
                OR i.TotalPrice LIKE '%{text}%' 
                OR i.Description LIKE '%{text}%' 
                ORDER BY i.DtTm DESC;";
            MainClass.mySqlSelect(query, mainForm.gridInvoices, "invoices", new List<string>());
        }
        public static void clearFields(MainForm mainForm)
        {
            mainForm.tbInvoicesFrom.Text = string.Empty;
            mainForm.tbInvoicesTo.Text = string.Empty;
            mainForm.tbInvoicesNumber.Text = string.Empty;
            mainForm.tbInvoicesPrice.Text = string.Empty;
            mainForm.rtbInvoicesDescription.Text = string.Empty;
            mainForm.tbSearchInvoices.Text = string.Empty;
            mainForm.cbInvoiceToDemand.Checked = false;
            mainForm.cbInvoiceToShipment.Checked = false;
            mainForm.cbInvoiceToFolder.Checked = false;
            mainForm.labelInvoiceDir.Text = "No file selected!";
            mainForm.cbInvoiceSameFile.Checked = false;
        }

        public static void browse(MetroLabel label)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == DialogResult.OK) label.Text = openFileDialog.FileName;
        }
    }// class
}// namespace
