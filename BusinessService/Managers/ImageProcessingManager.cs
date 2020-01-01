using BusinessService.Managers.IManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace BusinessService.Managers
{
    public class ImageProcessingManager: BaseManager, IImageProcessingManager
    {
        /// <summary>
        /// Extracts the image.
        /// </summary>
        /// <returns></returns>
        public BitmapImage ExtractImage()
        {
            var uu = new Uri(@".\Content\Images\Berlin.jpg", UriKind.Relative);
            return new BitmapImage(new Uri(@".\Content\Images\Berlin.jpg", UriKind.Relative));
        }
    }
}
