namespace WebApplication1.Models
{
    // Filter values bound from the query string, e.g. /excel-practice?Filter.Region=North
    public class ExcelPracticeFilter
    {
        public string? Search { get; set; }
        public string? Region { get; set; }
        public string? Product { get; set; }
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
    }

    public class ExcelPracticeViewModel
    {
        public ExcelPracticeFilter Filter { get; set; } = new();
        public IReadOnlyList<SalesRecord> Rows { get; set; } = [];
        public IReadOnlyList<string> Regions { get; set; } = [];
        public IReadOnlyList<string> Products { get; set; } = [];

        public int TotalUnits => Rows.Sum(r => r.Units);
        public decimal TotalRevenue => Rows.Sum(r => r.Total);
    }
}
