using FileServer.App.Application.Commands.File;
using FileServer.App.Application.Queries;
using FileServer.App.Models;
using FileServer.App.Models.Request.File;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileQueries _fileQueries;
        public FileController(IMediator mediator, IFileQueries fileQueries)
        {
            _mediator = mediator;
            _fileQueries = fileQueries;
        }
        private string GetCurrentUserName()
        {
            return HttpContext.User.Identities.First().Claims.Where(claim => claim.Type == "name").FirstOrDefault().Value;
        }
        
        [HttpGet]
        public  FileInformationResponse GetRootFilesInformation()
        {
            return _fileQueries.GetFilesAtRoot();
        }

        [HttpPost]
        [Route("upload-files-or-folders")]
        public async Task<IEnumerable<FileDetailResponse>> UploadFileAsync([FromForm] CreateFileRequest files)
        {
            return await _mediator.Send(new CreateFileCommand(files, GetCurrentUserName()));
        }
        [HttpGet]
        [Route("{folderID}/files")]
        public FileInformationResponse GetFileAtFolder(string folderID)
        {
            return _fileQueries.GetFileAtFolder(folderID);
        }
        [HttpPut]
        [Route("{fileId}/update-file")]
        public async Task<FileDetailResponse> UpdateFileInformation(string fileID, [FromBody] UpdateFileRequest request)
        {
            return await _mediator.Send(new UpdateFileCommand(fileID, request, GetCurrentUserName()));
        }
    
        [HttpDelete]
        [Route("{fileId}/delete-file")]
        public async Task<FileDetailResponse> DeleteFile(string fileId)
        {
            return await _mediator.Send(new DeleteFileCommand(fileId));
        }
        [HttpDelete]
        [Route("{folderId}/delete-folder")]
        public async Task<FileDetailResponse> DeleteFolder(string folderId)
        {
            return await _mediator.Send(new DeleteFolderCommand(folderId));
        }
    }
}
