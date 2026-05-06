using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySanPham.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [Display(Name = "Giá bán")]
        public double Price { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 0;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
