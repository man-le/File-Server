using FileServer.App.Application.Infrastructures;
using FileServer.App.Models;
using FileServer.Domain.FileAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.App.Application.Commands.File
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, FileDetailResponse>
    {
        private readonly IFileRepository _fileRepo;

        public UpdateFileCommandHandler(IFileRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public async Task<FileDetailResponse> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var existedFile = _fileRepo.FindByID(request.FileID);
            if (existedFile is null)
                throw new ValidateException("Invalid file");
            existedFile.UpdateFileName(request.Request.FileName, request.ModBy);
            var updatedFile = _fileRepo.Update(existedFile);
            if(await _fileRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken))
            {
                return new FileDetailResponse()
                {
                    FileID = updatedFile.TenantID,
                    FileLocation = updatedFile.FileInformation.FileLocation,
                    CreatedDate = updatedFile.FileInformation.CreatedDate.Date,
                    ModifiedDate = updatedFile.FileInformation.ModifiedDate.Date,
                    CreatedBy = updatedFile.FileInformation.CreatedBy,
                    FileName = updatedFile.FileInformation.FileName,
                    ModifiedBy = updatedFile.FileInformation.ModifiedBy,
                    RowVersion = updatedFile.RowVersion
                };
            }
            else
            {
                throw new DBSaveChangeException("Can't update file");
            }
        }
    }
}
