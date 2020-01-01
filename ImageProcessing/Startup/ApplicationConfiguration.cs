using Autofac;
using ImageProcessing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.ViewModel.IViewModel;
using ProxyLayer;

namespace ImageProcessing.Startup
{
    public class ApplicationConfiguration
    {
        /// <summary>
        /// Builds the container.
        /// </summary>
        /// <returns></returns>
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ThumbnailViewModel>().As<IThumbnailViewModel>();
            builder.RegisterType<ServiceEndPoint>().As<IServiceEndPoint>();
            
            return builder.Build();
        }
    }
}
