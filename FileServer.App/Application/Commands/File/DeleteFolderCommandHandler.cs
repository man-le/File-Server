using FileServer.App.Application.Infrastructures;
using FileServer.App.Models;
using FileServer.Domain.FileAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.App.Application.Commands.File
{
    public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, FileDetailResponse>
    {
        private readonly IFileRepository _fileRepo;

        public DeleteFolderCommandHandler(IFileRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public async Task<FileDetailResponse> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var existedFolder = _fileRepo.FindByID(request.FolderId);
            if (existedFolder is null)
                throw new ValidateException("Invalid folder");
            if (existedFolder.FileInformation.FileLocation != "")
                throw new ValidateException("Invalid folder");
            existedFolder.CleanUpFileInFolder(_fileRepo);
            var deletedFile = _fileRepo.DeleteFile(existedFolder);
            if(await _fileRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                return new FileDetailResponse()
                {
                    FileID = deletedFile.TenantID
                };
            }
            throw new DBSaveChangeException("Can't save file");
        }
    }
}
