using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BusinessService.CommonOperation
{
    public interface IFacade
    {
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns></returns>
        BitmapImage GetImage(int pageNumber);

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <param name="fromPageNumber">From page number.</param>
        /// <param name="toPageNumber">To page number.</param>
        /// <returns></returns>
        List<BitmapImage> GetImages(int fromPageNumber, int toPageNumber);

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="name">The name.</param>
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
