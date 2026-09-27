namespace WebApplication1.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Region { get; set; } = "";
        public string Salesperson { get; set; } = "";
        public string Product { get; set; } = "";
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Total => Units * UnitPrice;
    }
}
