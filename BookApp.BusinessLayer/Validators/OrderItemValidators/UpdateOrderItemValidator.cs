using BookApp.DtoLayer.OrderItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Validators.OrderItemValidators
{
    public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Lütfen Fiyat giriniz.");


        }
    }
}
