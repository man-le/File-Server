using Domain.Core;
using FileServer.Domain.FileAggregate;
using FileServer.Infrastructure.EntityTypeConfigs;
using Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Infrastructure
{
    public class FileServerContext : DbContext, IUnitOfWork
    {
        public static string DEFFAULT_SCHEMA = "dbo";
        public DbSet<FileInfo> Files { get; set; }
        private readonly IMediator _mediator;
        public FileServerContext(DbContextOptions<FileServerContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileInfoTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            if (result != 0)
                return true;
            return false;
        }
    }
    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<FileServerContext>
    {
        public FileServerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FileServerContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=FileExplorerDb;Trusted_Connection=True;");

            return new FileServerContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }
        }
    }
}
