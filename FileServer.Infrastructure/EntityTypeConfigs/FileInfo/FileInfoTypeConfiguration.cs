using FileServer.Domain.FileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileServer.Infrastructure.EntityTypeConfigs
{
    public class FileInfoTypeConfiguration : IEntityTypeConfiguration<FileInfo>
    {
        public void Configure(EntityTypeBuilder<FileInfo> builder)
        {
            builder.ToTable("FileInfo", FileServerContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => new { x.TenantID});
            builder.Property<int>("Id").UseIdentityColumn().Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            builder.Ignore(x => x.DomainEvents);
            builder.Property(x => x.TenantID).HasColumnName("FileID").HasMaxLength(100).IsRequired();
            builder.HasMany(x => x.ChildContents).WithOne(x => x.RootFolder).IsRequired(false);
            builder.OwnsOne(x => x.FileInformation, fi =>
            {
                fi.Property(x => x.FileLocation).IsRequired(true).HasMaxLength(200);
                fi.Property(x => x.FileName).IsRequired(true).HasMaxLength(200);
                fi.Property(x => x.ModifiedDate).IsRequired(true);
                fi.Property(x => x.ModifiedBy).IsRequired(true).HasMaxLength(100);
                fi.Property(x => x.CreatedDate).IsRequired(true);
                fi.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(100);
                fi.WithOwner();
            });
            
        }
    }
}
