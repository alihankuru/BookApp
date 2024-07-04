﻿using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.ShelfLocation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Validators.BookValidators
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Lütfen Başlık açıklamasını giriniz.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Lütfen en az 5 karakter veri girişi yapınız");

        }
    }
}
