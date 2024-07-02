using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.EntityLayer.Concrete
{
    public class BookNote
    {
        public int BookNoteId { get; set; }
        public string NoteText { get; set; }
        public bool IsShared { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        //public int UserId { get; set; }
        //public User User { get; set; }
        public List<SharedNote> SharedNotes { get; set; }
    }
}
