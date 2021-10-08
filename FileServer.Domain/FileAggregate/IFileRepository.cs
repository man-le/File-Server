using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileServer.Domain.FileAggregate
{
    public interface IFileRepository : IRepository<FileInfo>
    {
        FileInfo Add(FileInfo fileInfo);
        FileInfo Update(FileInfo fileInfo);
        FileInfo FindByID(string id);
        FileInfo FindByName(string folderName);
        IQueryable<FileInfo> GetFiles();
        FileInfo DeleteFile(Domain.FileAggregate.FileInfo fileToDelete);
    }
}
