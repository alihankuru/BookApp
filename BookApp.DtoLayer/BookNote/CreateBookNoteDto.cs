using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DtoLayer.BookNote
{
    public class CreateBookNoteDto
    {
        public string NoteText { get; set; }
        public bool IsShared { get; set; }
        public int BookId { get; set; }
    }
}
