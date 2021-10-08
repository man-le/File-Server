using Autofac;
using FileServer.App.Application.Queries;
using FileServer.Domain.FileAggregate;
using FileServer.Infrastructure.Repositories.FileInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.App.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        private readonly string _connectionString;
        public ApplicationModule(string connStr)
        {
            _connectionString = connStr;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileRepository>().As<IFileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FileQueries>().As<IFileQueries>().InstancePerLifetimeScope();
        }
    }
}
