using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DtoLayer.SharedNote
{
    public class CreateSharedNoteDto
    {
        public int BookNoteId { get; set; }
        public string PrivacySetting { get; set; } // Public, Friends, Private
    }
}
