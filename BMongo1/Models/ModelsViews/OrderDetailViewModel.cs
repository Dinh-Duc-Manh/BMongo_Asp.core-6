using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMongo1.Models.ModelsViews
{
    public class OrderDetailViewModel
    {
        public int _id { get; set; }
        public int? Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public double? TotalPrice { get; set; }
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0 $}")]
        public float? Price { get; set; }
        public int? OrdersId { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverPhone { get; set; }
        public string? ReceiverAddress { get; set; }
        public string? Note { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public int? AccountId { get; set; }
    }
}
