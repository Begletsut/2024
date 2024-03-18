namespace PZO_V3.Models
{
    public class Shipment
    {
        public Shipment(int id, int idDemand, int idKontragentFrom, int idKontragentTo, int idCourier, string waybill, string status, string description)
        {
            Id = id;
            IdDemand = idDemand;
            IdKontragentFrom = idKontragentFrom;
            IdKontragentTo = idKontragentTo;
            IdCourier = idCourier;
            Waybill = waybill;
            Status = status;
            Description = description;
        }

        public int Id { get; set; }
        public int IdDemand { get; set; }
        public int IdKontragentFrom { get; set; }
        public int IdKontragentTo { get; set; }
        public int IdCourier { get; set; }
        public string Waybill { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
