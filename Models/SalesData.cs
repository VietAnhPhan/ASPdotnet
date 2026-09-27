namespace WebApplication1.Models
{
    // In-memory sample data (no database yet). Seeded so the rows are the same on every run.
    public static class SalesData
    {
        public static readonly string[] Regions = ["North", "South", "East", "West"];
        public static readonly string[] Products = ["Laptop", "Monitor", "Keyboard", "Mouse", "Headset"];

        private static readonly string[] Salespeople = ["An", "Binh", "Chi", "Dung", "Giang", "Hoa"];
        private static readonly Dictionary<string, decimal> Prices = new()
        {
            ["Laptop"] = 1200m,
            ["Monitor"] = 250m,
            ["Keyboard"] = 45m,
            ["Mouse"] = 25m,
            ["Headset"] = 80m,
        };

        public static readonly IReadOnlyList<SalesRecord> All = Generate(60);

        private static List<SalesRecord> Generate(int count)
        {
            var random = new Random(42);
            var start = new DateOnly(2026, 1, 1);
            var records = new List<SalesRecord>();

            for (var i = 1; i <= count; i++)
            {
                var product = Products[random.Next(Products.Length)];
                records.Add(new SalesRecord
                {
                    Id = i,
                    Date = start.AddDays(random.Next(0, 180)),
                    Region = Regions[random.Next(Regions.Length)],
                    Salesperson = Salespeople[random.Next(Salespeople.Length)],
                    Product = product,
                    Units = random.Next(1, 20),
                    UnitPrice = Prices[product],
                });
            }

            return records.OrderBy(r => r.Date).ToList();
        }
    }
}
