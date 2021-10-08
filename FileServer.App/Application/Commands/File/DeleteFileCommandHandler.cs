using FileServer.App.Application.Infrastructures;
using FileServer.App.Models;
using FileServer.Domain.FileAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.App.Application.Commands.File
{
    public class DeleteFileCommandHandler: IRequestHandler<DeleteFileCommand,FileDetailResponse>
    {
        private readonly IFileRepository _fileRepo;

        public DeleteFileCommandHandler(IFileRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public async Task<FileDetailResponse> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var existedFile = _fileRepo.FindByID(request.FileID);
            if (existedFile is null)
                throw new ValidateException("Invalid file to delete");
            if (existedFile.FileInformation.FileLocation == "")
                throw new ValidateException("Invalid file to delete");
            var deletedFile = _fileRepo.DeleteFile(existedFile);
            if(await _fileRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                System.IO.File.Delete(deletedFile.FileInformation.FileLocation);
                return new FileDetailResponse()
                {
                    FileID = deletedFile.TenantID,
                    FileLocation = deletedFile.FileInformation.FileLocation,
                    CreatedDate = deletedFile.FileInformation.CreatedDate.Date,
                    ModifiedDate = deletedFile.FileInformation.ModifiedDate.Date,
                    CreatedBy = deletedFile.FileInformation.CreatedBy,
                    FileName = deletedFile.FileInformation.FileName,
                    ModifiedBy = deletedFile.FileInformation.ModifiedBy,
                    RowVersion = deletedFile.RowVersion
                };
            }
            else
            {
                throw new DBSaveChangeException("Can't save to db");
            }
        }
    }
}
