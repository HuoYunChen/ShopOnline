using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ProductId { get; set; }

        public OrderStatus Status { get; set; }

        public Product Product { get; set; }

        public string GetOrderStatus()
        {
            switch (Status)
            {
                case OrderStatus.PayamentCompleted:
                    {
                        return "Payament Completed";
                    };
                case OrderStatus.ToBeShipped:
                    {
                        return "To Be Shipped";
                    }
            }

            return string.Empty;
        }
    }

    public enum OrderStatus
    { 
        Default = 0,
        PayamentCompleted = 1,
        ToBeShipped = 2
    }
}
