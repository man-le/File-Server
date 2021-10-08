using Domain.Core;
using FileServer.Domain.FileAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileServer.Infrastructure.Repositories.FileInfo
{
    public class FileRepository : IFileRepository
    {
        private readonly FileServerContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._context;
            }
        }

        public FileRepository(FileServerContext context)
        {
            _context = context;
        }

        public Domain.FileAggregate.FileInfo Add(Domain.FileAggregate.FileInfo fileInfo)
        {
            if (fileInfo.IsTransient())
            {
                this._context.Files.Add(fileInfo);
            }
            return fileInfo;
        }

        public Domain.FileAggregate.FileInfo FindByID(string id)
        {
            return this._context.Files.Include(x=>x.ChildContents).Single(f => f.TenantID.Equals(id));
        }
  
        public Domain.FileAggregate.FileInfo Update(Domain.FileAggregate.FileInfo fileInfo)
        {
            return this._context.Files.Update(fileInfo).Entity;
        }

        public Domain.FileAggregate.FileInfo FindByName(string folderName)
        {
            return this._context.Files.AsNoTracking().Where(x => x.FileInformation.FileName.Equals(folderName)).FirstOrDefault();
        }

        public Domain.FileAggregate.FileInfo DeleteFile(Domain.FileAggregate.FileInfo fileToDelete)
        {
            this._context.Files.Remove(fileToDelete);
            return fileToDelete;
        }

        public IQueryable<Domain.FileAggregate.FileInfo> GetFiles()
        {
            return this._context.Files.AsNoTracking().AsQueryable();
        }
    }
}
