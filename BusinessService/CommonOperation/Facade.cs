using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autofac;
using BusinessService.Managers.IManger;

namespace BusinessService.CommonOperation
{
    public class Facade : BaseManager, IFacade
    {
        private readonly IImageProcessingManager _imageProcessingManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IUndoRedoManager _undoRedoManager;
        public Facade()
        {
            _imageProcessingManager = Container.Resolve<IImageProcessingManager>();
            _employeeManager = Container.Resolve<IEmployeeManager>();
            _undoRedoManager = Container.Resolve<IUndoRedoManager>();
        }

        #region Image Processing        
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns></returns>
        public BitmapImage GetImage(int pageNumber)
        {
            return _imageProcessingManager.ExtractImage();
        }

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <param name="fromPageNumber">From page number.</param>
        /// <param name="toPageNumber">To page number.</param>
        /// <returns></returns>
        public List<BitmapImage> GetImages(int fromPageNumber, int toPageNumber)
        {
            List<BitmapImage> lst = new List<BitmapImage>();
            for (; fromPageNumber <= toPageNumber; fromPageNumber++)
            {
                lst.Add(_imageProcessingManager.ExtractImage());
            }
            return lst;
        }
        #endregion

        #region Employee         
        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<string> AddEmployee(string name)
        {
            return _employeeManager.AddEmployee(name);
        }
        #endregion

        #region Undo/Redo        
        /// <summary>
        /// Undoes this instance.
        /// </summary>
        /// <returns></returns>
        public string Undo()
        {
            return _undoRedoManager.Undo();
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        /// <returns></returns>
        public string Redo()
        {
            return _undoRedoManager.Redo();
        }
        #endregion

    }
}
