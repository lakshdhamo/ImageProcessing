using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BusinessService.CommonOperation;

namespace ProxyLayer.Configuration
{
    public class ApplicationConfiguration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Facade>().As<IFacade>();

            return builder.Build();
        }
    }
}
