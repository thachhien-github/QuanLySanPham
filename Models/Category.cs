using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QuanLySanPham.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(50)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
