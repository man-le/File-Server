using FileServer.App.Models;
using FileServer.App.Models.Request.File;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace FileServer.App.Application.Commands.File
{
    public class CreateFileCommand : IRequest<IEnumerable<FileDetailResponse>>
    {
        public CreateFileRequest Request { get; private set; }
        public string CreatedBy { get; private set; }
        public CreateFileCommand(CreateFileRequest req, string createdBy)
        {
            Request = req;
            CreatedBy = createdBy;
        }
    }
}
