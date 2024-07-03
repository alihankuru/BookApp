using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DtoLayer.ShelfLocation
{
    public class GetByIdShelfLocationDto
    {
        public int ShelfLocationId { get; set; }
        public string LocationDescription { get; set; }
    }
}
