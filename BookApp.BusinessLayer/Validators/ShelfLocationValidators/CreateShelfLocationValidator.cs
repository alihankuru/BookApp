using BookApp.DtoLayer.ShelfLocation;
using BookApp.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Validators.ShelfLocationValidators
{
    public class CreateShelfLocationValidator:AbstractValidator<CreateShelfLocationDto>
    {
        public CreateShelfLocationValidator()
        {
            RuleFor(x => x.LocationDescription).NotEmpty().WithMessage("Lütfen lokasyon açıklamasını giriniz.");
            RuleFor(x => x.LocationDescription).MinimumLength(5).WithMessage("Lütfen en az 5 karakter veri girişi yapınız");

        }
    }
}
