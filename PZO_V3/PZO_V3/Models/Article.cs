using System;

namespace PZO_V3.Models
{
    public class Article
    {
        public Article(int idStorageDetails, int idStorage, int quantity, string name, DateTime? date)
        {
            IdStorageDetails = idStorageDetails;
            IdStorage = idStorage;
            Quantity = quantity;
            Name = name;
            Date = date;
        }

        public int IdStorageDetails { get; set; }
        public int IdStorage { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
    }
}
