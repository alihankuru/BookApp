using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.BookNote;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Validators.BookNoteValidators
{
    public class CreateBookNoteValidator : AbstractValidator<CreateBookNoteDto>
    {
        public CreateBookNoteValidator()
        {
            RuleFor(x => x.NoteText).NotEmpty().WithMessage("Lütfen Başlık açıklamasını giriniz.");
            RuleFor(x => x.NoteText).MinimumLength(5).WithMessage("Lütfen en az 5 karakter veri girişi yapınız");
        }
    
    }
}
