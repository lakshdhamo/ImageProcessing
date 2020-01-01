using ImageProcessing.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.ViewModel.IViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IThumbnailViewModel
    {
        /// <summary>
        /// Binds the thumbnail images.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        void BindThumbnailImages(int pageNumber);

        /// <summary>
        /// Gets or sets the thumbnail images.
        /// </summary>
        /// <value>
        /// The thumbnail images.
        /// </value>
        ObservableCollection<ImageItem> ThumbnailImages { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        string Title { get; }

        /// <summary>
        /// Gets or sets the employee list.
        /// </summary>
        /// <value>
        /// The employee list.
        /// </value>
        ObservableCollection<string> EmployeeList { get; set; }
    
        /// <summary>
        /// Adds the employee.
        /// </summary>
        void AddEmployee(string name);

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        void Undo();

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        void Redo();



    }
}
