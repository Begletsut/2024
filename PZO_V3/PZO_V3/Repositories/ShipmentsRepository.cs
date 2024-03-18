using MySqlConnector;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PZO_V3.Models
{
    public abstract class ShipmentsRepository
    {
        public static Shipment CurrentShipment { get; set; } = new Shipment(0,0,0,0,0, string.Empty, string.Empty, string.Empty);
        public static string CourierName { get; set; }

        public const string Query = @"
                SELECT s.*, kf.Name AS 'From', kt.Name AS 'To', c.Name AS 'Courier', c.Link AS Link FROM shipments s 
                JOIN kontragents kf ON s.IdKontragentFrom = kf.Id 
                JOIN kontragents kt ON s.IdKontragentTo = kt.Id 
                JOIN couriers c ON s.IdCourier = c.Id {0} ORDER BY DtTm DESC;";

        public static void insertOrUpdate(string cmd, MainForm mainForm)
        {
            string where = string.Empty;
            if (cmd == "UPDATE")
            {
                if (CurrentShipment.Id == 0)
                {
                    MessageBox.Show("First select Shipment which you want to update.");
                    return;
                }
                where = $" WHERE Id = '{CurrentShipment.Id}'";
            }
            string Courier = mainForm.tbShipmentCourier.Text;
            string Waybill = mainForm.tbShipmentWaybill.Text;
            string Status = mainForm.cobShipmentStatus.Text;

            if (!Checks.isValidString(mainForm.tbShipmentFrom.Text, 50, false) ||
                !Checks.isValidString(mainForm.tbShipmentTo.Text, 50, false) ||
                !Checks.isValidString(Courier, 50, false) ||
                !Checks.isValidString(Waybill, 50, false) ||
                Status == string.Empty ||
                !Checks.isValidString(mainForm.rtbShipmentDescription.Text, 255, true))
            {
                MessageBox.Show("Fill all the fields.");
                return;
            }

            DateTime fullDate = mainForm.dateShipments.Value;
            string year = fullDate.ToString("yyyy");
            string month = fullDate.ToString("MM");

            if (mainForm.cbShipmentToDemand.Checked)
            {
                if (DemandsRepository.Id == 0)
                {
                    MessageBox.Show("Select Demand first!");
                    return;
                }
                CurrentShipment.IdDemand = DemandsRepository.Id;
            }
            else
            {
                CurrentShipment.IdDemand = 0;
            }
            if (mainForm.cbShipmentFolder.Checked && !mainForm.shipmentHasFolder)// there is no folder, create it
            {
                string dir = $@"{MainForm.selectedDocsFolder}\{year}\{year}-{month}_invoices\{Courier}_{Waybill}";
                Directory.CreateDirectory(dir);
                MainClass.svnCommand("add", dir);
                MainClass.svnCommand("commit", MainForm.selectedDocsFolder);
            }
            else if (!mainForm.cbShipmentFolder.Checked && mainForm.shipmentHasFolder)// there is folder, delete it
            {
                string dir = $@"{MainForm.selectedDocsFolder}\{year}\{year}-{month}_invoices\{Courier}_{Waybill}";
                if (Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    if (MessageBox.Show("Folder is not empty, are you sure you want to delete folder with files?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                MainClass.svnCommand("remove", dir);
                MainClass.svnCommand("commit", MainForm.selectedDocsFolder);
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true);
                }
            }
            string query = $@"
                    {cmd} shipments SET 
                    IdKontragentFrom = {CurrentShipment.IdKontragentFrom},
                    IdKontragentTo = {CurrentShipment.IdKontragentTo},
                    IdCourier = {CurrentShipment.IdCourier},
                    DtTm = '{fullDate.ToString("yyyy-MM-dd HH:mm")}',
                    Status = '{Status}',
                    Waybill = '{Waybill}',
                    Description = '{MySqlHelper.EscapeString(mainForm.rtbShipmentDescription.Text)}', 
                    IdDemand = {CurrentShipment.IdDemand}
                    {where};";
            MainClass.mySqlCommand(query, "Done!");
            MainClass.refreshAllTables(mainForm);
            clearFields(mainForm);
        }
        public static void delete(MainForm mainForm)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                DataGridViewRow selectedRow = mainForm.gridShipments.SelectedRows[0];
                string Id = selectedRow.Cells["Id"].Value.ToString();
                string query = $"DELETE FROM shipments WHERE Id = '{Id}'";
                MainClass.mySqlCommand(query, "Done!");
                MainClass.refreshAllTables(mainForm);
            }
        }

        public static void cellClick(MainForm mainForm)
        {
            DataGridViewCellCollection selectedRow = mainForm.gridShipments.SelectedRows[0].Cells;
            CurrentShipment.Id = int.Parse(selectedRow["Id"].Value.ToString());
            CurrentShipment.IdDemand = int.Parse(selectedRow["IdDemand"].Value.ToString());
            CurrentShipment.IdKontragentFrom = int.Parse(selectedRow["IdKontragentFrom"].Value.ToString());
            CurrentShipment.IdKontragentTo = int.Parse(selectedRow["IdKontragentTo"].Value.ToString());
            CurrentShipment.IdCourier = int.Parse(selectedRow["IdCourier"].Value.ToString());
            CurrentShipment.Waybill = selectedRow["Waybill"].Value.ToString();
            CurrentShipment.Status = selectedRow["Status"].Value.ToString();
            CurrentShipment.Description = selectedRow["Description"].Value.ToString();
            CourierName = MainClass.couriers.First(x => x.Value == CurrentShipment.IdCourier).Key;
            mainForm.dateShipments.Value = DateTime.Parse(selectedRow["DtTm"].Value.ToString());
            mainForm.tbShipmentFrom.Text = selectedRow["From"].Value.ToString();
            mainForm.tbShipmentTo.Text = selectedRow["To"].Value.ToString();
            mainForm.tbShipmentCourier.Text = selectedRow["Courier"].Value.ToString();
            mainForm.tbShipmentWaybill.Text = CurrentShipment.Waybill;
            mainForm.cobShipmentStatus.Text = CurrentShipment.Status;
            mainForm.rtbShipmentDescription.Text = CurrentShipment.Description;
            MainClass.findUpperTables(mainForm.gridShipments, mainForm.gridDemands, "IdDemand");
            if (CurrentShipment.IdDemand == 0)
            {
                mainForm.cbShipmentToDemand.Checked = false;
            }
            else
            {
                mainForm.cbShipmentToDemand.Checked = true;
                DemandsRepository.cellClick(mainForm);
            }

            DateTime fullDate = (DateTime)selectedRow["DtTm"].Value;
            string year = fullDate.ToString("yyyy");
            string month = fullDate.ToString("MM");
            string courier = selectedRow["Courier"].Value.ToString();
            string dir = $"{MainForm.selectedDocsFolder}\\{year}\\{year}-{month}_invoices\\{courier}_{CurrentShipment.Waybill}";
            if (Directory.Exists(dir))
            {
                mainForm.iconHasFolder.BackgroundImage = Properties.Resources.folder;
                mainForm.shipmentHasFolder = true;
                mainForm.cbShipmentFolder.Checked = true;
            }
            else
            {
                mainForm.iconHasFolder.BackgroundImage = Properties.Resources.no_folder;
                mainForm.shipmentHasFolder = false;
                mainForm.cbShipmentFolder.Checked = false;
            }
            ArticlesRepository.showArticles(CurrentShipment.Id, "IdShipment", mainForm);
        }

        public static void clearFields(MainForm mainForm)
        {
            CurrentShipment.Id = 0;
            CurrentShipment.IdKontragentFrom = 0;
            CurrentShipment.IdKontragentTo = 0;
            CurrentShipment.IdCourier = 0;
            mainForm.tbShipmentFrom.Text = string.Empty;
            mainForm.tbShipmentTo.Text = string.Empty;
            mainForm.tbShipmentCourier.Text = string.Empty;
            mainForm.tbShipmentWaybill.Text = string.Empty;
            mainForm.cobShipmentStatus.Text = null;
            mainForm.rtbShipmentDescription.Text = string.Empty;
            mainForm.cbShipmentToDemand.Checked = false;
            mainForm.cbShipmentFolder.Checked = false;
        }
        public static void textBoxSearch(MainForm mainForm)
        {
            string text = mainForm.tbSearchShipments.Text;
            if (!Checks.isValidString(text, 15, true))
            {
                mainForm.tbSearchShipments.Text = string.Empty;
                return;
            }
            string query = $@"
                SELECT s.*, kf.Name AS 'From', kt.Name AS 'To', c.Name AS 'Courier', c.Link AS Link FROM shipments s 
                JOIN kontragents kf ON s.IdKontragentFrom = kf.Id 
                JOIN kontragents kt ON s.IdKontragentTo = kt.Id 
                JOIN couriers c ON s.IdCourier = c.Id 
                WHERE kf.Name LIKE '%{text}%' 
                OR kt.Name LIKE '%{text}%' 
                OR s.DtTm LIKE '%{text}%' 
                OR c.Name LIKE '%{text}%' 
                OR s.Waybill LIKE '%{text}%' 
                OR s.Status LIKE '%{text}%' 
                OR s.Description LIKE '%{text}%' 
                ORDER BY s.DtTm DESC;";
            MainClass.mySqlSelect(query, mainForm.gridShipments, "shipments", new List<string>());
        }
    }// class
}// namespace
