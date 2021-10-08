using FileServer.App.Models;
using FileServer.Domain.FileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Application.Queries
{
    public class FileQueries : IFileQueries
    {
        private readonly IFileRepository _fileRepo;

        public FileQueries(IFileRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public FileInformationResponse GetFileAtFolder(string folderID)
        {
            var files = _fileRepo.GetFiles();
            var existedFolder = _fileRepo.FindByID(folderID);
            var result = files.Where(x => x.RootFolder.TenantID.Equals(folderID)).ToList();
            return new FileInformationResponse()
            {
                RootFile = existedFolder.TenantID,
                RootFileName = existedFolder.FileInformation.FileName,
                Files = ToFileDetails(result)
            };
        }

        public FileInformationResponse GetFilesAtRoot()
        {
            var files = _fileRepo.GetFiles();
            var result = files.Where(file => file.RootFolder == null).ToList();
            return new FileInformationResponse()
            {
                RootFile = null,
                Files = ToFileDetails(result)
            };
        }
        public List<FileDetailResponse> ToFileDetails(List<FileInfo> details)
        {
            List<FileDetailResponse> list = new List<FileDetailResponse>();
            foreach (var detail in details)
            {
                list.Add(new FileDetailResponse()
                {
                    FileID = detail.TenantID,
                    ModifiedDate = detail.FileInformation.ModifiedDate.Date,
                    ModifiedBy = detail.FileInformation.ModifiedBy,
                    FileLocation = detail.FileInformation.FileLocation,
                    FileName = detail.FileInformation.FileName,
                    RowVersion = detail.RowVersion,
                    CreatedDate = detail.FileInformation.CreatedDate,
                    CreatedBy = detail.FileInformation.CreatedBy
                });
            }
            return list;
        }
    }
}
