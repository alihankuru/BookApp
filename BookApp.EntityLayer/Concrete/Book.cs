using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.EntityLayer.Concrete
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int ShelfLocationId { get; set; }
        public ShelfLocation ShelfLocation { get; set; }
        public List<BookNote> BookNotes { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
