using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMongo1.Models.ModelsViews
{
    public class CartViewModel
    {
        public int _id { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public double TotalPrice { get; set; }
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public float Price { get; set; }
        public int AccountId { get; set; }
    }
}
