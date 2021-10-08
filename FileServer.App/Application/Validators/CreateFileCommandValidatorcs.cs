using FileServer.App.Application.Commands.File;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Application.Validators
{
    public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        public CreateFileCommandValidator()
        {
            RuleFor(x => x.CreatedBy).NotEmpty();
            RuleFor(x => x.Request).NotEmpty();
        }
    }
}
