using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProxyLayer
{
    public interface IServiceEndPoint
    {
        /// <summary>
        /// Gets the Thumbnail image.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns></returns>
        BitmapImage GetImage(int pageNumber);

        /// <summary>
        /// Gets the Thumbnail images.
        /// </summary>
        /// <param name="fromPageNumber">From page number.</param>
        /// <param name="toPageNumber">To page number.</param>
        /// <returns></returns>
        List<BitmapImage> GetImages(int fromPageNumber, int toPageNumber);

        /// <summary>
        /// Adds the employee.
        /// </summary>
        List<string> AddEmployee(string name);

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        string Undo();

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        string Redo();
        
    }
}
