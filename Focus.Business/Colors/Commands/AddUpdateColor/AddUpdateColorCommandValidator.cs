using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Colors.Commands.AddUpdateBrand
{
    public class AddUpdateColorCommandValidator : AbstractValidator<AddUpdateColorCommand>
    {
        public AddUpdateColorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
