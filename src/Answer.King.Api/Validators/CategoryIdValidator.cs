﻿using Answer.King.Api.ViewModels;
using FluentValidation;

namespace Answer.King.Api.Validators
{
    public class CategoryIdValidator : AbstractValidator<CategoryId>
    {
        public CategoryIdValidator()
        {
            this.RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}