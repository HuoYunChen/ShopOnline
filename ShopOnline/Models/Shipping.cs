using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Models
{
    public class Shipping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int Price { get; set; }

        public long OrderId { get; set; }

        public Order Order { get; set; }

    }
}
