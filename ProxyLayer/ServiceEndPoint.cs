using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autofac;
using ProxyLayer.Configuration;
using BusinessService.CommonOperation;

namespace ProxyLayer
{
    public class ServiceEndPoint: IServiceEndPoint
    {
        
        private readonly IFacade _facade;
        protected IContainer Container { get; set; }
        public ServiceEndPoint()
        {
            Container = ApplicationConfiguration.BuildContainer();
            _facade = Container.Resolve<IFacade>();
        }
        /// <summary>
        /// Gets the specific page number image.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns></returns>
        public BitmapImage GetImage(int pageNumber)
        {
            return _facade.GetImage(pageNumber);
        }

        /// <summary>
        /// Gets the images of specific limited.
        /// </summary>
        /// <param name="fromPageNumber">From page number.</param>
        /// <param name="toPageNumber">To page number.</param>
        /// <returns></returns>
        public List<BitmapImage> GetImages(int fromPageNumber, int toPageNumber)
        {
            return _facade.GetImages(fromPageNumber, toPageNumber);
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        public List<string> AddEmployee(string name)
        {
            return _facade.AddEmployee(name);
        }

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public string Undo()
        {
            return _facade.Undo();
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public string Redo()
        {
            return _facade.Redo();
        }

    }
}
