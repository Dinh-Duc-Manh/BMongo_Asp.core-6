using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMongo1.Models
{
    public class Category
    {
     
        [Column("CategoryId")]
        [Required(ErrorMessage = "Category ID cannot be empty")]
        [Display(Name = "Category Code")]
        public int _id { get; set; }

        [Column("CategoryName")]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name cannot be empty")]
        public string? CategoryName { get; set; }
    }
}
