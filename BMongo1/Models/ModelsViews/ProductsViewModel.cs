using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMongo1.Models.ModelsViews
{
    public class ProductsViewModel
    {
        public string? _id { get; set; }
        public string? ProductName { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductStatus { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
