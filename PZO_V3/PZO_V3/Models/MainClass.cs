using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace PZO_V3.Models
{
    public abstract class MainClass// MEDIATOR Class
    {
        public static Dictionary<string, int> kontragents = new Dictionary<string, int>();
        public static Dictionary<string, int> couriers = new Dictionary<string, int>();
        public static string QueryHistory = @"
                SELECT storagedetails.*, storage.Name AS Article FROM storagedetails 
                JOIN storage ON storagedetails.IdStorage = storage.Id {0} 
                ORDER BY DtTm DESC";
        public static string QuerySingleLog = "SELECT * FROM storagedetails WHERE IdStorage = {0} ORDER BY DtTm DESC";
        public static void mySqlCommand(string query, string message)
        {
            if (query == string.Empty)
            {
                MessageBox.Show("No changes are made.");
                return;
            }
            MySqlCommand cmd = new MySqlCommand(query, MainForm.connection);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            if (message != string.Empty)
            {
                MessageBox.Show(message);
            }
        }
        public static void mySqlSelect(string query, DataGridView grid, string table, List<string> hiddenColumns)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, MainForm.connection);
            DataSet ds = new DataSet();
            try
            {
                MainForm.connection.Open();
                adapter.Fill(ds, table);
                grid.DataSource = ds.Tables[table];
                MainForm.connection.Close();
                foreach (var column in hiddenColumns)
                {
                    grid.Columns[column].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void exportToDoc(DataGridView grid) // export buttons for every grid to export to doc
        {
            StringBuilder stringBuilder = new StringBuilder();
            string headerNames = string.Join("\t", grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText));
            stringBuilder.AppendLine(headerNames);
            foreach (DataGridViewRow row in grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible) stringBuilder.Append($"{cell.Value}\t");
                }
                stringBuilder.AppendLine("");
            }
            Clipboard.SetText(stringBuilder.ToString());
            Process.Start("Notepad.exe");
            Thread.Sleep(500);
            SendKeys.Send("^V");
        }

        public static void svnCommand(string cmd, string path)
        {
            Process svnProcess = new Process();
            svnProcess.StartInfo.FileName = "TortoiseProc.exe";
            svnProcess.StartInfo.Arguments = $"/command:{cmd} /path:\"{path}\" /closeonend:2 /closeonend:3 /noquestion";
            svnProcess.Start();
            svnProcess.WaitForExit();
        }


        
        public static void getKontragents() // list of kontragents names for autocomplete
        {
            using (MySqlConnection connection = new MySqlConnection(MainForm.mysqlConn))
            {
                connection.Open();

                string query = "SELECT Id, Name FROM kontragents";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            kontragents[name] = id;
                        }
                    }
                }
            }
        }
        public static void getCouriers() // list of couriers names for autocomplete
        {
            using (MySqlConnection connection = new MySqlConnection(MainForm.mysqlConn))
            {
                connection.Open();

                string query = "SELECT Id, Name FROM couriers";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            couriers[name] = id;
                        }
                    }
                }
            }
        }

        public static void rightClickMenus(DataGridView currentGrid, bool hasArticles, bool isDocumentOrInvoice, bool showDocument, DataGridViewCellMouseEventArgs e, MainForm mainForm) // right click menus for most of grids
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            List<ToolStripItem> options = new List<ToolStripItem>();

            currentGrid.ClearSelection();
            currentGrid[e.ColumnIndex, e.RowIndex].Selected = true;
            currentGrid.CurrentCell = currentGrid[e.ColumnIndex, e.RowIndex];

            Dictionary<DataGridView, (string id, string name, string table,  string classQuery)> gridsIdsNameCheckbox = new Dictionary<DataGridView, (string, string, string, string)>
            {
                {mainForm.gridDemands, ("IdDemand", "Find Demand", "demands", DemandsRepository.Query) },
                {mainForm.gridShipments, ("IdShipment", "Find Shipment", "shipments", ShipmentsRepository.Query) },
                {mainForm.gridInvoices, ("IdInvoice", "Find Invoice", "invoices", InvoicesRepository.Query) },
                {mainForm.gridDocuments, ("IdDocument", "Find Document", "documents", DocumentsRepository.Query) }
            };
            if (!showDocument) gridsIdsNameCheckbox.Remove(mainForm.gridDocuments);

            bool foundGrid = false;
            foreach (var grid in gridsIdsNameCheckbox)
            {
                if (currentGrid == grid.Key)
                {
                    foundGrid = true;
                    continue;
                }
                ToolStripMenuItem currentOption = new ToolStripMenuItem(gridsIdsNameCheckbox[grid.Key].name);
                currentOption.Font = !foundGrid ? new Font(mainForm.Font, FontStyle.Bold) : mainForm.Font;
                currentOption.Click += foundGrid
            ? new EventHandler((eventSender, eventArgs) => findLowerTables(currentGrid, grid.Key, gridsIdsNameCheckbox[currentGrid].id, grid.Value.table, gridsIdsNameCheckbox[grid.Key].classQuery))
            : new EventHandler((eventSender, eventArgs) => findUpperTables(currentGrid, grid.Key, grid.Value.id));
                options.Add(currentOption);
            }
            if (hasArticles)
            {
                ToolStripMenuItem articlesOption = new ToolStripMenuItem("Show Articles");
                articlesOption.Click += (eventSender, eventArgs) => ArticlesRepository.showArticles(ShipmentsRepository.CurrentShipment.Id, gridsIdsNameCheckbox[currentGrid].id, mainForm);
                options.Add(articlesOption);
            }
            if (isDocumentOrInvoice)
            {
                ToolStripMenuItem openFile = new ToolStripMenuItem("Open File");
                ToolStripMenuItem openDir = new ToolStripMenuItem("Open Folder");
                openFile.Click += (eventSender, eventArgs) => DocumentsRepository.OpenFile(currentGrid);
                openDir.Click += (eventSender, eventArgs) => DocumentsRepository.OpenDirectory(currentGrid);
                options.AddRange(new ToolStripMenuItem[] { openFile, openDir });
            }
            if (currentGrid == mainForm.gridShipments)
            {
                string link = currentGrid.SelectedRows[0].Cells["Link"].Value.ToString();
                string waybill = currentGrid.SelectedRows[0].Cells["Waybill"].Value.ToString();
                string courier = currentGrid.SelectedRows[0].Cells["Courier"].Value.ToString();
                DateTime fullDate = (DateTime)currentGrid.SelectedRows[0].Cells["DtTm"].Value;
                string date = fullDate.ToString("yyyy-MM-dd");
                string year = fullDate.ToString("yyyy");
                string month = fullDate.ToString("MM");
                ToolStripMenuItem trackShipment = new ToolStripMenuItem("Track shipment");
                trackShipment.Click += (eventSender, eventArgs) => { Process.Start(link + waybill); };
                options.Add(trackShipment);
                if (mainForm.shipmentHasFolder)
                {
                    ToolStripMenuItem openFolder = new ToolStripMenuItem("Open folder");
                    openFolder.Click += (eventSender, eventArgs) => { Process.Start("explorer.exe", $@"{MainForm.selectedDocsFolder}\{year}\{year}-{month}_invoices\{courier}_{waybill}\"); };
                    options.Add(openFolder);
                }
            }
            currentGrid.ContextMenuStrip = menu;
            ToolStripItem[] convertedOptions = options.ToArray();
            menu.Items.AddRange(convertedOptions);
            menu.Show(currentGrid, currentGrid.PointToClient(Cursor.Position));
        }

        public static void findUpperTables(DataGridView gridFrom, DataGridView gridTo, string cellFrom)
        {
            if (gridFrom.SelectedRows.Count == 0) return;
            string id = gridFrom.SelectedRows[0].Cells[cellFrom].Value.ToString();
            gridTo.ClearSelection();

            foreach (DataGridViewRow row in gridTo.Rows)
            {
                if (row.Cells["Id"].Value.ToString() == id)
                {
                    row.Selected = true;
                    gridTo.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }
        public static void findLowerTables(DataGridView gridFrom, DataGridView gridTo, string cell, string table, string classQuery)//for right clicks lower - query
        {
            string id = gridFrom.SelectedRows[0].Cells["Id"].Value.ToString();
            string query = string.Format(classQuery, $"WHERE {cell} = {id}");
            mySqlSelect(query, gridTo, table, new List<string>());
        }

        public static void refreshAllTables(MainForm mainForm)
        {
            // demands
            mySqlSelect(string.Format(DemandsRepository.Query, string.Empty), mainForm.gridDemands, "demands", new List<string>() { "Id", "IdKontragent" });
            mainForm.gridDemands.Columns["DtTm"].DisplayIndex = 0;
            mainForm.gridDemands.Columns["DtTm"].Width = 100;
            mainForm.gridDemands.Columns["Kontragent"].DisplayIndex = 1;
            DemandsRepository.clearFields(mainForm);
            // shipments

            mySqlSelect(string.Format(ShipmentsRepository.Query, string.Empty), mainForm.gridShipments, "shipments", new List<string>() { "Id", "IdCourier", "IdKontragentTo", "IdKontragentFrom", "IdDemand", "Link" });
            mainForm.gridShipments.Columns["DtTm"].Width = 100;
            mainForm.gridShipments.Columns["DtTm"].DisplayIndex = 0;
            mainForm.gridShipments.Columns["From"].DisplayIndex = 1;
            mainForm.gridShipments.Columns["To"].DisplayIndex = 2;
            mainForm.gridShipments.Columns["Courier"].DisplayIndex = 3;
            ShipmentsRepository.clearFields(mainForm);
            // invoices
            mySqlSelect(string.Format(InvoicesRepository.Query, string.Empty), mainForm.gridInvoices, "invoices", new List<string>() { "Id", "IdKontragentFrom", "IdKontragentTo", "IdShipment", "IdDemand" });
            mainForm.gridInvoices.Columns["DtTm"].Width = 100;
            mainForm.gridInvoices.Columns["DtTm"].DisplayIndex = 0;
            mainForm.gridInvoices.Columns["From"].DisplayIndex = 1;
            mainForm.gridInvoices.Columns["To"].DisplayIndex = 2;
            InvoicesRepository.clearFields(mainForm);
            // documents
            mySqlSelect(string.Format(DocumentsRepository.Query, string.Empty), mainForm.gridDocuments, "documents", new List<string>() { "Id", "IdInvoice", "IdShipment", "IdDemand" });
            mainForm.gridDocuments.Columns["DtTm"].DisplayIndex = 0;
            mainForm.gridDocuments.Columns["Name"].DisplayIndex = 1;
            DocumentsRepository.clearFields(mainForm);
            // storage
            mySqlSelect(string.Format(StorageRepository.Query, string.Empty), mainForm.gridStorage, "storage", new List<string>() { "Id" });
            mainForm.gridStorage.Columns["Quantity"].Width = 70;
            StorageRepository.clearFields(mainForm);
            // history
            mySqlSelect(string.Format(MainClass.QueryHistory, string.Empty), mainForm.gridHistory, "storagedetails", new List<string>() { "Id", "IdStorage" });
            mainForm.gridHistory.Columns["DtTm"].Width = 100;
            mainForm.gridHistory.Columns["DtTm"].DisplayIndex = 0;
            mainForm.gridHistory.Columns["Article"].DisplayIndex = 1;

            // articles
            //mySqlSelect(string.Format(ArticlesRepository.Query, string.Empty), mainForm.gridArticles, "storagedetails", new List<string>() { "IdStorage", "IdStorageDetails" });
            //ArticlesRepository.clearFields(mainForm);
        }
    }// class
}// namespace
