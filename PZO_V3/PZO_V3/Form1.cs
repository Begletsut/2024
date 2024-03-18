using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using MySqlConnector;
using PZO_V3.Models;

namespace PZO_V3
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MainClass.refreshAllTables(this);
            if (selectedDocsFolder != null)
            {
                menuItemSelectedFolder.Text = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\PZOV3", "LastSelectedFolder", null);
            }
            LoadAutoCompleteSource();
            
        }
        public static MySqlConnection connection => new MySqlConnection(mysqlConn);
        public static string selectedDocsFolder = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\PZOV3", "LastSelectedFolder", null);
        public static string mysqlConn = checkLocalOrExternal();
        DBForm dbForm;

        //************************ Starter functions ************************

        static string checkLocalOrExternal()
        {
            string server = "XXX.XXX.X.XXXX";
            string port = "0000";
            string user = "user";
            string pass = "pass";
            string db = "db_inko2";
            List<string> localPCs = new List<string> { "DESKTOP-XXXXX", "XXXX" };
            bool isLocal = localPCs.Contains(Environment.MachineName.ToString());
            string result = $"server ={server}; port ={port}; user ={user}; password ={pass}; database = {db}";
            if (!isLocal)
            {
                server = "XXX.XXX.XXX.XXX";
                result = $"server ={server}; port ={port}; user ={user}; password ={pass}; database = {db}";
            }
            if (selectedDocsFolder != null)
            {
                MainClass.svnCommand("update", selectedDocsFolder);
            }
            return result;
        }
        private void menuItemSelectDocFolder_Click(object sender, EventArgs e) // select folder for invoices and documents in SVN or other FS
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                selectedDocsFolder = fd.SelectedPath;
                menuItemSelectedFolder.Text = fd.SelectedPath;
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\PZOV3", "LastSelectedFolder", fd.SelectedPath);
            }
        }

        //------------------------ Starter functions ------------------------

        //************************ Demands functions ************************

        private void tbDemandKontragent_Leave(object sender, EventArgs e)
        {
            if (MainClass.kontragents.ContainsKey(tbDemandKontragent.Text))
            {
                DemandsRepository.IdKontragent = MainClass.kontragents[tbDemandKontragent.Text];
            }
            else
            {
                tbDemandKontragent.Text = string.Empty;
                DemandsRepository.IdKontragent = 0;
            }
        }
        private void btnAddDemands_Click(object sender, EventArgs e)
        {
            DemandsRepository.clearFields(this);
        }
        private void btnInsertDemands_Click(object sender, EventArgs e)
        {
            DemandsRepository.InsertOrUpdate("INSERT INTO", this);
        }
        private void btnUpdateDemands_Click(object sender, EventArgs e)
        {
            DemandsRepository.InsertOrUpdate("UPDATE", this);
        }
        private void btnDeleteDemands_Click(object sender, EventArgs e)
        {
            if (gridDemands.Rows.Count == 0) return;
            if (gridDemands.SelectedRows.Count == 0) return;
            DemandsRepository.Delete(this);
        }
        private void btnExportDemands_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridDemands);
        }
        public void gridDemands_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DemandsRepository.cellClick(this);
        }
        private void gridDemands_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1) MainClass.rightClickMenus(gridDemands, false, false, true, e, this);
            }
        }
        private void textBoxSearchDemands_TextChanged(object sender, EventArgs e)
        {
            DemandsRepository.textBoxSearch(this);
        }
        //------------------------ Demands functions ------------------------

        //************************ Shipments functions ************************
        public bool shipmentHasFolder = false;

        private void tbShipmentFrom_Leave(object sender, EventArgs e)
        {
            if (MainClass.kontragents.ContainsKey(tbShipmentFrom.Text))
            {
                ShipmentsRepository.CurrentShipment.IdKontragentFrom = MainClass.kontragents[tbShipmentFrom.Text];
            }
            else
            {
                tbShipmentFrom.Text = string.Empty;
                ShipmentsRepository.CurrentShipment.IdKontragentFrom = 0;
            }
        }

        private void tbShipmentTo_Leave(object sender, EventArgs e)
        {
            if (MainClass.kontragents.ContainsKey(tbShipmentTo.Text))
            {
                ShipmentsRepository.CurrentShipment.IdKontragentTo = MainClass.kontragents[tbShipmentTo.Text];
            }
            else
            {
                tbShipmentTo.Text = string.Empty;
                ShipmentsRepository.CurrentShipment.IdKontragentTo = 0;
            }

        }

        private void tbShipmentCourier_Leave(object sender, EventArgs e)
        {
            if (MainClass.couriers.ContainsKey(tbShipmentCourier.Text))
            {
                ShipmentsRepository.CurrentShipment.IdCourier = MainClass.couriers[tbShipmentCourier.Text];
            }
            else
            {
                tbShipmentCourier.Text = string.Empty;
                ShipmentsRepository.CurrentShipment.IdCourier = 0;
            }
        }
        private void checkBoxDocumentToShipment_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDocumentToShipment.Checked)
            {
                cbDocumentToFolder.Enabled = true;
            }
            else
            {
                cbDocumentToFolder.Checked = false;
                cbDocumentToFolder.Enabled = false;
            }
        }
        private void gridShipments_DoubleClick(object sender, EventArgs e)
        {
            string link = gridShipments.SelectedRows[0].Cells["Link"].Value.ToString();
            string waybill = gridShipments.SelectedRows[0].Cells["Waybill"].Value.ToString();
            Process.Start(link + waybill);
        }
        private void btnAddShipments_Click(object sender, EventArgs e)
        {
            ShipmentsRepository.clearFields(this);
        }
        private void btnInsertShipments_Click(object sender, EventArgs e)
        {
            ShipmentsRepository.insertOrUpdate("INSERT INTO", this);
        }
        private void btnUpdateShipments_Click(object sender, EventArgs e)
        {
            ShipmentsRepository.insertOrUpdate("UPDATE", this);
        }
        private void btnDeleteShipments_Click(object sender, EventArgs e)
        {
            if (gridShipments.Rows.Count == 0 || ShipmentsRepository.CurrentShipment.Id == 0)
            {
                MessageBox.Show("Select shipment first");
                return;
            }
            else
            {
                ShipmentsRepository.delete(this);
            } 
        }
        private void btnExportShipments_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridShipments);
        }
        public void gridShipments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShipmentsRepository.cellClick(this);
        }
        private void gridShipments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)// right click
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1) 
                {
                    DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(0, gridShipments.SelectedRows[0].Index);
                    gridShipments_CellClick(gridShipments, args);
                    MainClass.rightClickMenus(gridShipments, true, false, true, e, this);                     
                }
            }
        }
        private void textBoxSearchShipments_TextChanged(object sender, EventArgs e)
        {
            ShipmentsRepository.textBoxSearch(this);
        }

        //------------------------ Shipments functions ------------------------

        //************************ Invoices functions ************************

        private void checkBoxInvoiceToShipment_CheckedChanged(object sender, EventArgs e)
        {
            if (cbInvoiceToShipment.Checked)
            {
                cbInvoiceToFolder.Enabled = true;
            }
            else
            {
                cbInvoiceToFolder.Checked = false;
                cbInvoiceToFolder.Enabled = false;
            }
        }

        private void checkBoxInvoiceSameFile_CheckedChanged(object sender, EventArgs e)
        {
            DocumentsRepository.checkBoxSameFile(cbInvoiceSameFile, labelInvoiceDir, gridInvoices);
        }

        private void btnAddInvoices_Click(object sender, EventArgs e)
        {
            InvoicesRepository.clearFields(this);
        }
        private void btnInsertInvoices_Click(object sender, EventArgs e)
        {
            InvoicesRepository.insertOrUpdate("INSERT INTO", this);
        }
        private void btnUpdateInvoices_Click(object sender, EventArgs e)
        {
            InvoicesRepository.insertOrUpdate("UPDATE", this);
        }
        private void btnDeleteInvoices_Click(object sender, EventArgs e)
        {
            if (gridInvoices.Rows.Count == 0 || InvoicesRepository.Id == 0)
            {
                return;
            }
            else
            {
                InvoicesRepository.delete(this);
            }
        }
        private void btnExportInvoices_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridInvoices);
        }
        private void gridInvoices_DoubleClick(object sender, EventArgs e)
        {
            DocumentsRepository.OpenFile(gridInvoices);
        }

        private void gridInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            InvoicesRepository.cellClick(this);
        }

        private void textBoxSearchInvoices_TextChanged(object sender, EventArgs e)
        {
            InvoicesRepository.textBoxSearch(this);
        }
        private void btnBrowseInvoice_Click(object sender, EventArgs e)
        {
            InvoicesRepository.browse(labelInvoiceDir);
        }
        private void gridInvoices_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)// right click menu
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1) MainClass.rightClickMenus(gridInvoices, false, true, true, e, this);
            }
        }

        private void tbInvoicesFrom_Leave(object sender, EventArgs e)
        {
            if (MainClass.kontragents.ContainsKey(tbInvoicesFrom.Text))
            {
                InvoicesRepository.IdKontragentFrom = MainClass.kontragents[tbInvoicesFrom.Text];
            }
            else
            {
                tbInvoicesFrom.Text = string.Empty;
                InvoicesRepository.IdKontragentFrom = 0;
            }
        }


        private void tbInvoicesTo_Leave(object sender, EventArgs e)
        {
            if (MainClass.kontragents.ContainsKey(tbInvoicesTo.Text))
            {
                InvoicesRepository.IdKontragentTo = MainClass.kontragents[tbInvoicesTo.Text];
            }
            else
            {
                tbInvoicesTo.Text = string.Empty;
                InvoicesRepository.IdKontragentTo = 0;
            }
        }
        //------------------------ Invoices functions ------------------------

        //************************ Documents functions ************************

        private void checkBoxDocSameFile_CheckedChanged(object sender, EventArgs e)
        {
            DocumentsRepository.checkBoxSameFile(cbDocSameFile, labelDocDir, gridDocuments);
        }

        private void btnAddDocuments_Click(object sender, EventArgs e)
        {
            DocumentsRepository.clearFields(this);
        }

        private void btnInsertDocuments_Click(object sender, EventArgs e)
        {
            DocumentsRepository.insertOrUpdate("INSERT INTO", this);

        }
        private void btnUpdateDocuments_Click(object sender, EventArgs e)
        {
            DocumentsRepository.insertOrUpdate("UPDATE", this);
        }
        private void btnDeleteDocuments_Click(object sender, EventArgs e)
        {
            if (gridDocuments.Rows.Count == 0)
            {
                return;
            }
            else
            {
                DocumentsRepository.delete(this);
            }
        }
        private void btnExportDocuments_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridDocuments);
        }
        private void textBoxSearchDocuments_TextChanged(object sender, EventArgs e)
        {
            DocumentsRepository.textBoxSearch(this);
        }
        private void gridDocuments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)// right click
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1) MainClass.rightClickMenus(gridDocuments, false, true, false, e, this);
            }
        }
        private void gridDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DocumentsRepository.cellClick(this);
        }
        private void btnBrowseDoc_Click(object sender, EventArgs e)
        {
            DocumentsRepository.browse(labelDocDir);
        }

        private void gridDocuments_DoubleClick(object sender, EventArgs e)
        {
            DocumentsRepository.OpenFile(gridDocuments);
        }
        
        //------------------------ Documents functions ------------------------

        //************************ Storage functions ************************
        private void btnAddStorage_Click(object sender, EventArgs e)
        {
            StorageRepository.clearFields(this);
        }
        private void gridStorage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StorageRepository.cellClick(this);
        }
        private void btnInsertStorage_Click(object sender, EventArgs e)
        {
            StorageRepository.InsertOrUpdate("INSERT INTO", this);
        }

        private void btnUpdateStorage_Click(object sender, EventArgs e)
        {
            StorageRepository.InsertOrUpdate("UPDATE", this);
        }

        private void btnDeleteStorage_Click(object sender, EventArgs e)
        {
            StorageRepository.delete(this);
        }
        private void textBoxSearchStorage_TextChanged(object sender, EventArgs e)
        {
            StorageRepository.textBoxSearch(this);
        }
        private void btnExportStorage_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridStorage);
        }
        private void gridStorage_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) // right click on storage add article to Articles
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    gridStorage.ClearSelection();
                    gridStorage[e.ColumnIndex, e.RowIndex].Selected = true;
                    gridStorage.CurrentCell = gridStorage[e.ColumnIndex, e.RowIndex];

                    int currentId = int.Parse(gridStorage.CurrentRow.Cells["Id"].Value.ToString());
                    string currentName = gridStorage.CurrentRow.Cells["Name"].Value.ToString();
                    ContextMenuStrip menu = new ContextMenuStrip();
                    ToolStripMenuItem AddArticle = new ToolStripMenuItem("Add Article");

                    AddArticle.Click += (eventSender, eventArgs) => StorageRepository.addArticleFromStorage(currentId, currentName);
                    menu.Items.AddRange(new ToolStripItem[] { AddArticle });
                    gridDocuments.ContextMenuStrip = menu;
                    menu.Show(gridDocuments, gridDocuments.PointToClient(Cursor.Position));
                }
            }
        }

        //------------------------ Storage functions ------------------------

        //************************ History and Singlelog functions ************************

        private void textBoxSearchHistory_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearchHistory.Text.Contains("\'") || textBoxSearchHistory.Text.Contains("\""))
            {
                textBoxSearchHistory.Text = string.Empty;
                return;
            }
            string query = $@"
                SELECT storagedetails.*, storage.Name AS Article FROM storagedetails 
                JOIN storage ON storagedetails.IdStorage = storage.Id 
                WHERE storagedetails.DtTm LIKE '%{textBoxSearchHistory.Text}%' 
                OR storage.Name LIKE '%{textBoxSearchHistory.Text}%' 
                OR storagedetails.Quantity LIKE '%{textBoxSearchHistory.Text}%' 
                OR storagedetails.QuantityChanged LIKE '%{textBoxSearchHistory.Text}%' 
                OR storagedetails.ChangeLog LIKE '%{textBoxSearchHistory.Text}%' 
                ORDER BY DtTm DESC;";
            MainClass.mySqlSelect(query, gridDetails, "storagedetails", new List<string>());
        }

        
        private void gridHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)// right click
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1) MainClass.rightClickMenus(gridHistory, false, false, false, e, this);
            }
        }
        private void btnExportHistory_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridHistory);
        }

        private void gridDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)// right click Single Log
        {
            
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1) MainClass.rightClickMenus(gridDetails, false, false, false, e, this);
            }
        }
       
        private void btnExportSingleLog_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridDetails);
        }
        //------------------------ History and Singlelog functions ------------------------

        //************************ articles functions ************************

        private void btnDeleteArticles_Click(object sender, EventArgs e)
        {
            ArticlesRepository.delete(this);
        }

        private void btnExportArticles_Click(object sender, EventArgs e)
        {
            MainClass.exportToDoc(gridArticles);
        }

        private void gridArticles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) // right click, find in storage and in storagedetails
        {
            ArticlesRepository.rightClick(this, e);
        }
        private void btnInsertArticles_Click(object sender, EventArgs e)
        {
            ArticlesRepository.insert(this);
        }
        //------------------------ articles functions ------------------------

        //************************ other functions ************************


        private void kontragentsToolStripMenuItem_Click(object sender, EventArgs e) // open form for kontragents
        {
            if (dbForm != null && dbForm.IsHandleCreated && dbForm.Title != "Kontragents")
            {
                dbForm.Close();
            }
            
            if (dbForm == null || dbForm.IsDisposed)
            {
                dbForm = new DBForm( "Kontragents", "kontragents", "Address", true, "Description", new List<string>() { "Name", "Address", "Description" });
            }
            dbForm.Show();
            dbForm.BringToFront();
        }
        private void couriersToolStripMenuItem_Click(object sender, EventArgs e) // open form for couriers 
        {
            if (dbForm != null && dbForm.IsHandleCreated && dbForm.Title != "Couriers")
            {
                dbForm.Close();
            }
            if (dbForm == null || dbForm.IsDisposed)
            {
                dbForm = new DBForm("Couriers", "couriers", "Link", false, string.Empty, new List<string>() { "Name", "Link" });
            }
            dbForm.Show();
            dbForm.BringToFront();
        }

        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            MainClass.refreshAllTables(this);
        }

        public void LoadAutoCompleteSource()
        {
            MainClass.getKontragents();
            MainClass.getCouriers();
            tbDemandKontragent.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbDemandKontragent.AutoCompleteCustomSource.Clear();

            tbShipmentFrom.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbShipmentFrom.AutoCompleteCustomSource.Clear();

            tbShipmentTo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbShipmentTo.AutoCompleteCustomSource.Clear();

            tbShipmentCourier.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbShipmentCourier.AutoCompleteCustomSource.Clear();

            tbInvoicesFrom.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbInvoicesFrom.AutoCompleteCustomSource.Clear();

            tbInvoicesTo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbInvoicesTo.AutoCompleteCustomSource.Clear();

            foreach (var kontragent in MainClass.kontragents.Keys)
            {
                tbDemandKontragent.AutoCompleteCustomSource.Add(kontragent);
                tbShipmentFrom.AutoCompleteCustomSource.Add(kontragent);
                tbShipmentTo.AutoCompleteCustomSource.Add(kontragent);
                tbInvoicesFrom.AutoCompleteCustomSource.Add(kontragent);
                tbInvoicesTo.AutoCompleteCustomSource.Add(kontragent);
            }
            foreach (var courier in MainClass.couriers.Keys)
            {
                tbShipmentCourier.AutoCompleteCustomSource.Add(courier);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbForm != null)
            {
                dbForm.Close();
            }
        }

        //------------------------ other functions ------------------------
    }//class
}//namespace
