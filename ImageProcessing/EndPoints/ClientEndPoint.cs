using Autofac;
using ImageProcessing.Startup;
using ProxyLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessing.EndPoints
{
    public class ClientEndPoint
    {
        #region private variables
        private readonly IServiceEndPoint _serviceEndPoint;
        private static IContainer Container { get; set; }
        #endregion

        public ClientEndPoint()
        {
            Container = ApplicationConfiguration.BuildContainer();
            _serviceEndPoint = Container.Resolve<IServiceEndPoint>();
        }

        /// <summary>
        /// Gets the specific page number image.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns></returns>
        public BitmapImage GetImage(int pageNumber)
        {
            return _serviceEndPoint.GetImage(pageNumber);
        }

        /// <summary>
        /// Gets the images of specific limited.
        /// </summary>
        /// <param name="fromPageNumber">From page number.</param>
        /// <param name="toPageNumber">To page number.</param>
        /// <returns></returns>
        public List<BitmapImage> GetImages(int fromPageNumber, int toPageNumber)
        {
            return _serviceEndPoint.GetImages(fromPageNumber, toPageNumber);
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        public List<string> AddEmployee(string name)
        {
            return _serviceEndPoint.AddEmployee(name);
        }

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public string Undo()
        {
            return _serviceEndPoint.Undo();
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public string Redo()
        {
            return _serviceEndPoint.Redo();
        }
    }
}
