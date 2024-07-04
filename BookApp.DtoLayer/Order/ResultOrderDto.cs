using BookApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DtoLayer.Order
{
    public class ResultOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }


    }
}
