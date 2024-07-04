using BookApp.DtoLayer.Book;
using BookApp.DtoLayer.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Validators.OrderValidators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Lütfen başlık açıklamasını giriniz.");
            RuleFor(x => x.Description).MinimumLength(1).WithMessage("Lütfen en az 5 karakter veri girişi yapınız");
        }
    
    }
}
