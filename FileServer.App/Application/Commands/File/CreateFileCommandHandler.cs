using FileServer.App.Application.Infrastructures;
using FileServer.App.Infrastructure;
using FileServer.App.Models;
using FileServer.Domain.FileAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.App.Application.Commands.File
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, IEnumerable<FileDetailResponse>>
    {
        private readonly IFileRepository _fileRepo;

        public CreateFileCommandHandler(IFileRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public async Task<IEnumerable<FileDetailResponse>> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            List<FileDetailResponse> addedFiles = new List<FileDetailResponse>();
            var rootFolderLocation = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "FileServer", request.CreatedBy);
            var existedRootFolder = request.Request.RootFolder == default(string) ? null : _fileRepo.FindByID(request.Request.RootFolder);
            if(request.Request.FilesInRootFolder != null)
            {
                foreach (var file in request.Request.FilesInRootFolder)
                {
                    ValidateFile(file.ContentType);
                }
            }
            if(request.Request.ChildFolder != null)
            {
                foreach (var folder in request.Request.ChildFolder)
                {
                    var newFolder = new FileInfo(folder, request.CreatedBy, existedRootFolder, default(string));
                    addedFiles.Add(new FileDetailResponse()
                    {
                        CreatedDate = newFolder.FileInformation.CreatedDate,
                        CreatedBy = newFolder.FileInformation.CreatedBy,
                        FileID = newFolder.TenantID,
                        FileLocation = newFolder.FileInformation.FileLocation,
                        FileName = newFolder.FileInformation.FileName,
                        ModifiedDate = newFolder.FileInformation.ModifiedDate,
                        ModifiedBy = newFolder.FileInformation.ModifiedBy,
                        RowVersion = newFolder.RowVersion
                    });
                    _fileRepo.Add(newFolder);
                }
            }
            if(request.Request.FilesInRootFolder != null)
            {
                foreach (var file in request.Request.FilesInRootFolder)
                {
                    var newFile = new FileInfo(file.FileName, request.CreatedBy, existedRootFolder, rootFolderLocation);
                    if (!System.IO.File.Exists(rootFolderLocation))
                    {
                        System.IO.Directory.CreateDirectory(rootFolderLocation);
                    }
                    using (var stream = System.IO.File.Create(newFile.FileInformation.FileLocation))
                    {
                        await file.CopyToAsync(stream);
                        //System.IO.File.Delete()
                    }
                    _fileRepo.Add(newFile);
                    addedFiles.Add(new FileDetailResponse()
                    {
                        CreatedDate = newFile.FileInformation.CreatedDate.Date,
                        CreatedBy = newFile.FileInformation.CreatedBy,
                        FileID = newFile.TenantID,
                        FileLocation = newFile.FileInformation.FileLocation,
                        FileName = newFile.FileInformation.FileName,
                        ModifiedDate = newFile.FileInformation.ModifiedDate.Date,
                        ModifiedBy = newFile.FileInformation.ModifiedBy,
                        RowVersion = newFile.RowVersion
                    });
                }
            }
        
            if(await _fileRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                return addedFiles;
            }
            else
            {
                throw new DBSaveChangeException("Can't add files");
            }
        }
        private void ValidateFile(string fileContentType)
        {
            if (!Constraints.FILE_ALLOW_EXT.Contains(fileContentType))
            {
                throw new Exception("Invalid file exts");
            }
        }

    }
}
