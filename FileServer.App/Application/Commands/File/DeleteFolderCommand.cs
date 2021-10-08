using FileServer.App.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileServer.App.Application.Commands.File
{
    public class DeleteFolderCommand : IRequest<FileDetailResponse>
    {
        public string FolderId { get; private set; }

        public DeleteFolderCommand(string folderId)
        {
            FolderId = folderId;
        }
    }
}
