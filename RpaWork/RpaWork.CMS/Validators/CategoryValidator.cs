using FluentValidation;
using RpaWork.CMS.Models.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWork.CMS.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Bu alan boş geçilemez").Length(1, 50).WithMessage("1-50 karakter sınırı vardır");
        }
    }
}
