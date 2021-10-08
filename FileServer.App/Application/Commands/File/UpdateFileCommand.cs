using FileServer.App.Models;
using FileServer.App.Models.Request.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileServer.App.Application.Commands.File
{
    public class UpdateFileCommand : IRequest<FileDetailResponse>
    {
        public string FileID { get; private set; }
        public UpdateFileRequest Request { get; private set; }
        public string ModBy { get; private set; }

        public UpdateFileCommand(string fileID, UpdateFileRequest request, string modBy)
        {
            FileID = fileID;
            Request = request;
            ModBy = modBy;
        }
    }
}
