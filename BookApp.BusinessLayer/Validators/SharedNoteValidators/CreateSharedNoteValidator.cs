using BookApp.DtoLayer.Order;
using BookApp.DtoLayer.SharedNote;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Validators.SharedNoteValidators
{
    public class CreateSharedNoteValidator : AbstractValidator<CreateSharedNoteDto>
    {
        public CreateSharedNoteValidator()
        {
            RuleFor(x => x.PrivacySetting).NotEmpty().WithMessage("Lütfen Başlık açıklamasını giriniz.");
            
        }
   
    }
}
