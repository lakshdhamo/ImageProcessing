using Autofac;
using BusinessService.Managers;
using BusinessService.Managers.IManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Configuration
{
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns></returns>
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ImageProcessingManager>().As<IImageProcessingManager>();
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>();
            builder.RegisterType<UndoRedoManager>().As<IUndoRedoManager>();
            
            return builder.Build();
        }
    }
}
