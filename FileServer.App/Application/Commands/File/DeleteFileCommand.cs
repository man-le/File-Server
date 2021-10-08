using FileServer.App.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileServer.App.Application.Commands.File
{
    public class DeleteFileCommand : IRequest<FileDetailResponse>
    {
        public  string FileID { get; private set; }

        public DeleteFileCommand(string fileID)
        {
            FileID = fileID;
        }
    }
}
