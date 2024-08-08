using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BMongo1.Models.ModelsViews
{
    public class OrdersViewModel
    {
        public int _id { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverPhone { get; set; }
        public string? ReceiverAddress { get; set; }
        public string? Note { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public int? AccountId { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
    }
}
