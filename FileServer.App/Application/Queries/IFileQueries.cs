using FileServer.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Application.Queries
{
    public interface IFileQueries
    {
        FileInformationResponse GetFilesAtRoot();
        FileInformationResponse GetFileAtFolder(string folderID);
    }
}
