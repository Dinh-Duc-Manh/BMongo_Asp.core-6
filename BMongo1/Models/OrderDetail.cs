using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMongo1.Models
{
    public class OrderDetail
    {
     
        [Column("OrderDetailId")]
        [Display(Name = "Order Detail code")]
        public int _id { get; set; }

        [Column("Quantity")]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Column("TotalPrice")]
        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public double? TotalPrice { get; set; }

        [Column("ProductId")]
        [Display(Name = "Product")]
        public string? ProductId { get; set; }

        [Column("OrdersId")]
        [Display(Name = "Orders")]
        public int? OrdersId { get; set; }
    }
}
