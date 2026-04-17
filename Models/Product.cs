using System.ComponentModel.DataAnnotations;

namespace QuanLySanPham.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }

        [Range(1, 1000000, ErrorMessage = "Giá phải > 0")]
        public double Price { get; set; }
    }
}
