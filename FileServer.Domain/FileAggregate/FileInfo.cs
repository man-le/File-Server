using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileServer.Domain.FileAggregate
{
    public class FileInfo : Entity, IAggregateRoot
    {
        public string TenantID { get; private set; }
        public byte[] RowVersion { get; private set; }
        public FileInfoValue FileInformation { get; private set; }
        public virtual FileInfo RootFolder { get; private set; }
        private List<FileInfo> _childContents;
        public IEnumerable<FileInfo> ChildContents => _childContents.AsReadOnly();

        protected FileInfo()
        {
            TenantID = $"FI-{(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds}-{Guid.NewGuid().ToString().Split('-')[0]}";
            _childContents = new List<FileInfo>();
        }
        public FileInfo(string fileName,string modby, FileInfo rootFolder, string fileLoc ): this()
        {
            this.RootFolder = rootFolder;
            if(fileLoc is null)
            {
                FileInformation = new FileInfoValue(fileName, modby, DateTime.Now, "", DateTime.Now, modby);
            }
            else
            {
                FileInformation = new FileInfoValue(fileName, modby, DateTime.Now, System.IO.Path.Combine(fileLoc, this.TenantID), DateTime.Now, modby);
            }
        }
        public void UpdateFileName(string fileName, string modBy)
        {
            var copyFileInfo = (FileInfoValue)this.FileInformation.GetCopy();
            this.FileInformation = new FileInfoValue(fileName, modBy, DateTime.Now, copyFileInfo.FileLocation, copyFileInfo.CreatedDate, copyFileInfo.CreatedBy);
        }

        public void CleanUpFileInFolder(IFileRepository repo)
        {
            foreach (var child in ChildContents)
            {
                if (child.FileInformation.FileLocation == "")
                {
                    var childFolder = repo.FindByID(child.TenantID);
                    childFolder.CleanUpFileInFolder(repo);
                }
                repo.DeleteFile(child);
            }
        }
    }
}
