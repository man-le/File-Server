using FileServer.App.Application.Commands.File;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Application.Validators
{
    public class UpdateFileCommandValidator : AbstractValidator<UpdateFileCommand>
    {
        public UpdateFileCommandValidator()
        {
            RuleFor(x => x.FileID).NotEmpty();
            RuleFor(x => x.ModBy).NotEmpty();
            RuleFor(x => x.Request).NotEmpty();
        }
    }
}
