using System.ComponentModel.DataAnnotations;

namespace QuanLySanPham.Models
{
    public class Product
    {
        public int Id { get; set; }
        // Thêm dấu ? sau string
        public string? Name { get; set; }
        public double Price { get; set; }

        // BẠN VỪA THÊM 2 THUỘC TÍNH NÀY:
        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}