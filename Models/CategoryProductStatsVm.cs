namespace QuanLySanPham.Models
{
    public class CategoryProductStatsVm
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProductCount { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double AvgPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}

