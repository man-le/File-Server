using FileServer.App.Application.Commands.File;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Application.Validators
{
    public class DeleteFileCommandValidator :AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator()
        {
            RuleFor(x => x.FileID).NotEmpty();
        }
    }
}
