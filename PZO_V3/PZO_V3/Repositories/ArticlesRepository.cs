using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using System.ComponentModel;
using System.Xml.Linq;

namespace PZO_V3.Models
{
    public abstract class ArticlesRepository
    {
        public static BindingList<Article> articlesFromDB = new BindingList<Article>();
        public static BindingList<Article> articlesInGrid = new BindingList<Article>();
        public const string Query = @"SELECT storagedetails.Id  AS 'IdStorageDetails', storagedetails.IdStorage, storagedetails.QuantityChanged AS 'Quantity', storage.Name AS 'Name' 
                FROM storagedetails JOIN storage ON storagedetails.IdStorage = storage.Id {0}";

        public static void insert(MainForm mainForm)
        {
            if (ShipmentsRepository.CurrentShipment.Id == 0)
            {
                MessageBox.Show("Select the shipment first");
                return;
            }

            string NameKontragentFrom = mainForm.gridShipments.SelectedRows[0].Cells["From"].Value.ToString();
            string NameKontragentTo = mainForm.gridShipments.SelectedRows[0].Cells["To"].Value.ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            StringBuilder queryArticles = new StringBuilder();

            List<Article> addedArticles = new List<Article>();
            List<Article> removedArticles = new List<Article>();
            List<Article> changedArticles = new List<Article>();

            string changelog = $"ChangeLog = 'Delivered from {NameKontragentFrom}'";

            if (articlesInGrid.Any(x => x.Quantity < 0)) changelog = $"ChangeLog = 'Sent to {NameKontragentTo}'";
            
            foreach(var articleGrid in articlesInGrid) // get added and changed articles
            {
                var currentArticleDB = articlesFromDB.FirstOrDefault(x => x.IdStorage == articleGrid.IdStorage);
                if (currentArticleDB != null)
                {
                    if (articleGrid.Quantity != currentArticleDB.Quantity)
                    {
                        changedArticles.Add(new Article(currentArticleDB.IdStorageDetails, currentArticleDB.IdStorage, articleGrid.Quantity - currentArticleDB.Quantity, currentArticleDB.Name, currentArticleDB.Date));
                    }
                }
                else
                {
                    addedArticles.Add(articleGrid);
                }
            }
            foreach (var article in articlesFromDB) // get removed articles
            {
                if (!articlesInGrid.Any(x => x.IdStorage == article.IdStorage))
                {
                    removedArticles.Add(article);
                }
            }

            if (mainForm.cbPendingArticles.Checked) // insert as pending
            {
                if (addedArticles.Count == 0 && changedArticles.Count == 0 && removedArticles.Count == 0)
                {
                    MessageBox.Show("No changes were made");
                    return;
                }
                foreach (var article in addedArticles)
                {
                    queryArticles.Append($@"
                        INSERT INTO storagedetails SET 
                        IdStorage = {article.IdStorage}, 
                        QuantityChanged = {article.Quantity}, 
                        Quantity = (SELECT Quantity FROM storage WHERE Id = {article.IdStorage}), 
                        {changelog},  
                        IdShipment = {ShipmentsRepository.CurrentShipment.Id};");
                }
                foreach (var article in changedArticles)
                {
                    queryArticles.AppendLine($@"UPDATE storagedetails SET 
                        IdStorage = {article.IdStorage}, 
                        Quantity = Quantity + {article.Quantity}, 
                        QuantityChanged = QuantityChanged + {article.Quantity}, 
                        {changelog}, 
                        IdShipment = {ShipmentsRepository.CurrentShipment.Id} 
                        WHERE Id = {articlesFromDB.First(x => x.IdStorage == article.IdStorage).IdStorageDetails};");
                }
                foreach (var article in removedArticles)
                {
                    queryArticles.AppendLine($@"DELETE FROM storagedetails WHERE Id = {article.IdStorageDetails};");
                }
            }
            else if (!mainForm.cbPendingArticles.Checked && articlesFromDB.Any(x => x.Date == null)) // check if were pending and now are not (must insert)
            {

                foreach (var article in articlesInGrid)
                {
                    if (changedArticles.Any(x => x.IdStorage == article.IdStorage)) continue;
                    queryArticles.AppendLine($@"UPDATE storage SET 
                    Quantity = Quantity + {article.Quantity} 
                    WHERE Id = {article.IdStorage};");
                    if (article.Date == null)
                    {
                        queryArticles.AppendLine($@"UPDATE storagedetails SET 
                        IdStorage = {article.IdStorage}, 
                        Quantity = Quantity + {article.Quantity}, 
                        QuantityChanged = {article.Quantity}, 
                        {changelog}, 
                        DtTm = '{date}', 
                        IdShipment = {ShipmentsRepository.CurrentShipment.Id} 
                        WHERE Id = {article.IdStorageDetails};");
                    }
                    else
                    {
                        queryArticles.Append($@"
                        INSERT INTO storagedetails SET 
                        IdStorage = {article.IdStorage}, 
                        QuantityChanged = {article.Quantity},
                        Quantity = (SELECT Quantity FROM storage WHERE Id = {article.IdStorage}),
                        {changelog}, 
                        DtTm = '{date}', 
                        IdShipment = {ShipmentsRepository.CurrentShipment.Id};");
                    }
                }

                foreach (var article in changedArticles)
                {
                    queryArticles.AppendLine($@"UPDATE storage SET 
                    Quantity = Quantity + {articlesInGrid.First(x => x.IdStorage == article.IdStorage).Quantity} 
                    WHERE Id = {article.IdStorage};");
                    queryArticles.AppendLine($@"UPDATE storagedetails SET 
                        IdStorage = {article.IdStorage}, 
                        Quantity = Quantity + {article.Quantity}, 
                        QuantityChanged = QuantityChanged + {article.Quantity}, 
                        {changelog}, 
                        DtTm = '{date}', 
                        IdShipment = {ShipmentsRepository.CurrentShipment.Id} 
                        WHERE Id = {article.IdStorageDetails};");
                }
                foreach (var article in removedArticles)
                {
                    queryArticles.AppendLine($@"DELETE FROM storagedetails WHERE Id = {article.IdStorageDetails};");
                }
            }
            else // normal insert without pending and were pending
            {
                if (addedArticles.Count == 0 && changedArticles.Count == 0 && removedArticles.Count == 0)
                {
                    MessageBox.Show("No changes were made");
                    return;
                }
                foreach (var article in addedArticles)
                {
                    queryArticles.AppendLine($@"UPDATE storage SET 
                    Quantity = Quantity + {article.Quantity} 
                    WHERE Id = {article.IdStorage};");

                    queryArticles.Append($@"INSERT INTO storagedetails SET 
                    IdStorage = {article.IdStorage}, 
                    QuantityChanged = {article.Quantity}, 
                    Quantity = (SELECT Quantity FROM storage WHERE Id = {article.IdStorage}), 
                    {changelog}, 
                    DtTm = '{date}', 
                    IdShipment = {ShipmentsRepository.CurrentShipment.Id};");
                }
                foreach (var article in changedArticles)
                {
                    queryArticles.AppendLine($@"UPDATE storage SET 
                    Quantity = Quantity + {article.Quantity} 
                    WHERE Id = {article.IdStorage};");
                    queryArticles.AppendLine($@"UPDATE storagedetails SET 
                        IdStorage = {article.IdStorage}, 
                        Quantity = Quantity + {article.Quantity}, 
                        QuantityChanged = QuantityChanged + {article.Quantity}, 
                        {changelog}, 
                        DtTm = '{date}', 
                        IdShipment = {ShipmentsRepository.CurrentShipment.Id} 
                        WHERE Id = {articlesFromDB.First(x => x.IdStorage == article.IdStorage).IdStorageDetails};");
                }
                foreach (var article in removedArticles)
                {
                    queryArticles.AppendLine($@"DELETE FROM storagedetails WHERE Id = {article.IdStorageDetails};
                    UPDATE storage SET Quantity = Quantity - {article.Quantity} WHERE Id = {article.IdStorage};");
                }
            }

            MainClass.mySqlCommand(queryArticles.ToString(), "Articles succesfuly inserted!");
            MainClass.refreshAllTables(mainForm);
            articlesFromDB.Clear();
            articlesInGrid.Clear();
            //foreach (var article in articlesFromDB) // check removed articles
            //{
            //    bool currentArticle = articlesInGrid.Any(x => x.IdStorage == article.IdStorage);
            //    if (!currentArticle)
            //    {
            //        removedArticles.Add(article);
            //    }
            //}
            //foreach (var articleGrid in articlesInGrid)// check added articles
            //{
            //    bool sameArticle = articlesFromDB.Any(x => x.IdStorage == articleGrid.IdStorage);
            //    if (sameArticle)
            //    {
            //        continue;
            //    }
            //    var changedArticle = articlesFromDB.First(x => x.IdStorage == articleGrid.IdStorage);
            //    if (changedArticle == null)
            //    {
            //        addedArticles.Add(articleGrid);
            //    }
            //}

            //if (werePending) // fix
            //{
            //    foreach (var article in articlesInGrid)
            //    {
            //queryArticles.AppendLine($@"UPDATE storage SET 
            //            Quantity = Quantity + {article.Quantity}
            //            WHERE Id = {article.IdStorage};");

            //queryArticles.AppendLine($@"UPDATE storagedetails SET 
            //            IdStorage = {article.IdStorage}, 
            //            Quantity = Quantity + {article.Quantity}, 
            //            QuantityChanged = QuantityChanged, 
            //            {changelog}, 
            //            DtTm = '{date}', 
            //            IdShipment = {ShipmentsRepository.CurrentShipment.Id} 
            //            WHERE Id = {article.IdStorageDetails};");
            //    }
            //}
        }

        public static void delete(MainForm mainForm)
        {
            if (mainForm.gridArticles.SelectedRows.Count > 0)
            {
                string name = mainForm.gridArticles.SelectedRows[0].Cells["Name"].Value.ToString();
                articlesInGrid.Remove(articlesInGrid.First(x => x.Name == name));
                //mainForm.gridArticles.DataSource = articlesInGrid.Select(a => new { a.Quantity, a.Name }).ToList();
                
                //DataTable dataTable = (DataTable)mainForm.gridArticles.DataSource;
                //dataTable.Rows.RemoveAt(mainForm.gridArticles.SelectedRows[0].Index);
                //dataTable.AcceptChanges();
            }
        }
        public static void showArticles(int id, string table, MainForm mainForm)// to find articles from every table
        {
            if (ShipmentsRepository.CurrentShipment.Id == 0)
            {
                return;
            }
            mainForm.cbPendingArticles.Checked = false;
            mainForm.gridArticles.Columns.Clear();
            articlesFromDB.Clear();
            articlesInGrid.Clear();

            string query = $@"SELECT storagedetails.Id  AS 'IdStorageDetails', storagedetails.IdStorage, storagedetails.QuantityChanged AS 'Quantity', storage.Name AS 'Name', storagedetails.DtTm as 'Date' 
                FROM storagedetails JOIN storage ON storagedetails.IdStorage = storage.Id WHERE storagedetails.{table} = {id}";
            using (IDbConnection dbConnection = new MySqlConnection(MainForm.mysqlConn))
            {
                dbConnection.Open();
                articlesFromDB = new BindingList<Article>(dbConnection.Query<Article>(query).ToList());
                articlesInGrid = new BindingList<Article>(dbConnection.Query<Article>(query).ToList());
            }
            mainForm.gridArticles.DataSource = articlesInGrid;
            mainForm.gridArticles.Columns["IdStorageDetails"].Visible = false;
            mainForm.gridArticles.Columns["IdStorage"].Visible = false;
            mainForm.gridArticles.Columns["Date"].Visible = false;

            if (articlesInGrid.Any(x => x.Date == null))
            {
                mainForm.cbPendingArticles.Checked = true;
            }
        }
        public static void rightClick(MainForm mainForm, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    mainForm.gridArticles[e.ColumnIndex, e.RowIndex].Selected = true;
                    mainForm.gridArticles.CurrentCell = mainForm.gridArticles[e.ColumnIndex, e.RowIndex];

                    string IdStorage = mainForm.gridArticles.CurrentRow.Cells["IdStorage"].Value.ToString();
                    ContextMenuStrip menu = new ContextMenuStrip();
                    ToolStripMenuItem ShowSingleLog = new ToolStripMenuItem("Show SingleLog");

                    ShowSingleLog.Click += (eventSender, eventArgs) => FindInSingleLog(IdStorage, mainForm);
                    menu.Items.AddRange(new ToolStripItem[] { ShowSingleLog });
                    mainForm.gridArticles.ContextMenuStrip = menu;
                    menu.Show(mainForm.gridArticles, mainForm.gridArticles.PointToClient(Cursor.Position));
                }
            }
        }
        public static void FindInSingleLog(string IdStorage, MainForm mainForm)
        {
            string query = $"SELECT * FROM storagedetails WHERE IdStorage = '{IdStorage}' ORDER BY DtTm DESC";
            MainClass.mySqlSelect(query, mainForm.gridDetails, "storagedetails", new List<string>() { "Id", "IdStorage" });
            mainForm.gridDetails.Columns["DtTm"].Width = 100;
            mainForm.gridDetails.Columns["DtTm"].DisplayIndex = 0;
            MainClass.findUpperTables(mainForm.gridArticles, mainForm.gridStorage, "IdStorage");
            MainClass.findUpperTables(mainForm.gridArticles, mainForm.gridDetails, "IdStorageDetails");
        }
        public static void clearFields(MainForm mainForm)
        {
            DataTable dataTable = (DataTable)mainForm.gridArticles.DataSource;
            dataTable.Clear();
            dataTable.AcceptChanges();
            mainForm.gridArticles.ClearSelection();
            mainForm.cbPendingArticles.Checked = false;
            mainForm.iconHasFolder.BackgroundImage = Properties.Resources.no_folder;
            articlesFromDB.Clear();
            articlesInGrid.Clear();
        }
    }// class
}// namespace
