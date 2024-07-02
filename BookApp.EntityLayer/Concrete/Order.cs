using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.EntityLayer.Concrete
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        //public int UserId { get; set; }
        //public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
