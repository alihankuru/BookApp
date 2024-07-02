using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.EntityLayer.Concrete
{
    public class SharedNote
    {
        public int SharedNoteId { get; set; }
        public int BookNoteId { get; set; }
        public BookNote BookNote { get; set; }
        //public int UserId { get; set; }
        //public User User { get; set; }
        public string PrivacySetting { get; set; } // Public, Friends, Private
    }
}
