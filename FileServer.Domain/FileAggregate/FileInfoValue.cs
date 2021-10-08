using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileServer.Domain.FileAggregate
{
    public class FileInfoValue : ValueObject
    {
        public string FileName { get; private set; }
        public string ModifiedBy { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public string FileLocation { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }
        public FileInfoValue() { }
        public FileInfoValue(string fileName, string modifiedBy, DateTime modifiedDate,  string fileLocation, DateTime createDate, string createdBy)
        {
            FileName = fileName;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
            FileLocation = fileLocation;
            CreatedDate = createDate;
            CreatedBy = createdBy;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FileName;
            yield return ModifiedBy;
            yield return ModifiedDate;
            yield return FileLocation;
            yield return CreatedDate;
        }
    }
}
