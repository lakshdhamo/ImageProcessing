using Autofac;
using BusinessService.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public class BaseManager
    {
        protected IContainer Container { get; set; }
        public BaseManager()
        {
            Container = ApplicationConfiguration.BuildContainer();
        }
    }
}
