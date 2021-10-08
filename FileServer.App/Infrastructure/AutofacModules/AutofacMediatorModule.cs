using App_Core;
using Autofac;
using FileServer.App.Application.Commands.File;
using FileServer.App.Application.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileServer.App.Infrastructure.AutofacModules
{
    public class AutofacMediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            //Register commands
            builder.RegisterAssemblyTypes(typeof(CreateFileCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //////Register domain events
            //builder.RegisterAssemblyTypes(typeof(CleanUpFolderDomainEventHandler).GetTypeInfo().Assembly)
            //   .AsClosedTypesOf(typeof(INotificationHandler<>));

            //Register Validators
            builder
                .RegisterAssemblyTypes(typeof(CreateFileCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });


            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
