using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Models.Request.File
{
    public class CreateFileRequest
    {
        public string RootFolder { get; set; }
        public List<string> ChildFolder { get; set; }
        public IEnumerable<IFormFile> FilesInRootFolder { get; set; }
    }
}
