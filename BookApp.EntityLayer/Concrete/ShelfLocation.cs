using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.EntityLayer.Concrete
{
    public class ShelfLocation
    {
        public int ShelfLocationId { get; set; }
        public string LocationDescription { get; set; }
        public List<Book> Books { get; set; }
    }
}
