using FluentValidation;
using RpaWork.CMS.Models.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWork.CMS.Validators
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Bu alan boş geçilemez").Length(1, 50).WithMessage("1-50 karakter sınırı vardır");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Bu alan boş geçilemez").GreaterThan(0).WithMessage("0'dan büyük bir değer giriniz");
        }
    }
}
