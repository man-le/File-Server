using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Models
{
    public class FileInformationResponse
    {
        public string RootFile { get; set; }
        public string RootFileName { get; set; }
        public IEnumerable<FileDetailResponse> Files { get; set; }
    }
    public class FileDetailResponse
    {
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public string FileLocation { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
