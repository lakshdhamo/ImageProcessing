using ImageProcessing.EndPoints;
using ImageProcessing.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;
using ImageProcessing.ViewModel.IViewModel;
using Models;
using Newtonsoft.Json;

namespace ImageProcessing.ViewModel
{
    public class ThumbnailViewModel : INotifyPropertyChanged, IThumbnailViewModel
    {
        #region private variables
        private ClientEndPoint _clientEndPoint = new ClientEndPoint();
        private ImageItem _imageItem;
        private ObservableCollection<ImageItem> _thumbnailImages = new ObservableCollection<ImageItem>();
        private ObservableCollection<string> _employeeList = new ObservableCollection<string>();
        private static ManualResetEvent _manualResetEvent = new ManualResetEvent(false);
        private bool PauseThread = false;
        private bool _enableUndo = false;
        private bool _enableRedo = false;
        #endregion

        public ThumbnailViewModel()
        {

            List<BitmapImage> lstImages = _clientEndPoint.GetImages(1, 50);
            BitmapImage _bitmapSource;
            bool _hasImage = false;
            for (int i = 1; i <= 5000; i++)
            {
                if (i <= 50)
                {
                    _bitmapSource = lstImages[i - 1];
                    _hasImage = true;
                }
                else
                {
                    _bitmapSource = GetLoadingImage();
                    _hasImage = false;
                }

                _imageItem = new ImageItem()
                {
                    Source = _bitmapSource,
                    ImageName = "Image " + i.ToString(),
                    ImageID = i,
                    HasImage = _hasImage
                };
                _thumbnailImages.Add(_imageItem);
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += GenerateImage;
            worker.RunWorkerAsync();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Pause the BackgroundWorker
        /// </summary>
        private void PauseBackgroundWorker()
        {
            PauseThread = true;
            _manualResetEvent.Reset();
        }

        /// <summary>
        /// Resume the BackgroundWorker
        /// </summary>
        private void ResumeBackgroundWorker()
        {
            PauseThread = false;
            _manualResetEvent.Set();
        }

        /// <summary>
        /// Generate Thumbnail images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateImage(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < _thumbnailImages.Count; i++)
            {
                if (PauseThread)
                {
                    _manualResetEvent.WaitOne();
                }
                if (!_thumbnailImages[i].HasImage)
                {
                    var src = _clientEndPoint.GetImage(i + 1);
                    src.Freeze();
                    _thumbnailImages[i].Source = src;
                    _thumbnailImages[i].HasImage = true;
                    Thread.Sleep(500);
                }

            }
        }

        /// <summary>
        /// Bind Thumbnail Images based on the scrolling position.
        /// 5 pages before visible + 5 visible pages + 5 pages after visible
        /// </summary>
        public void BindThumbnailImages(int pageNumber)
        {
            if (pageNumber <= 5)
                return;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += GenerateSpecificImages;
            worker.RunWorkerAsync(pageNumber);
        }

        /// <summary>
        /// Generate Thumbnail images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateSpecificImages(object sender, DoWorkEventArgs e)
        {
            int pageNumber = (int)e.Argument;
            PauseBackgroundWorker();
            for (int i = pageNumber - 5; i < pageNumber + 10; i++)
            {
                if (!_thumbnailImages[i].HasImage)
                {
                    var src = _clientEndPoint.GetImage(i + 1);
                    src.Freeze();
                    _thumbnailImages[i].Source = src;
                    _thumbnailImages[i].HasImage = true;
                    Thread.Sleep(500);
                }
            }
            ResumeBackgroundWorker();
        }

        /// <summary>
        /// Returns Loading image
        /// </summary>
        /// <returns></returns>
        private BitmapImage GetLoadingImage()
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(@".\Content\Images\Loading.jpg", UriKind.RelativeOrAbsolute);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            return src;

        }

        /// <summary>
        /// Application Title
        /// </summary>
        public string Title
        {
            get { return "Thumbnail view & UndoRedo Demo"; }
        }

        /// <summary>
        /// Holds thumbnail images
        /// </summary>
        public ObservableCollection<ImageItem> ThumbnailImages
        {
            get { return _thumbnailImages; }
            set
            {
                _thumbnailImages = value;
                OnPropertyChange("ThumbnailImages");
            }
        }

        public ObservableCollection<string> EmployeeList
        {
            get { return _employeeList; }
            set
            {
                _employeeList = value;
                OnPropertyChange("EmployeeList");
            }
        }

        public bool EnableUndo
        {
            get { return _enableUndo; }
            set
            {
                _enableUndo = value;
                OnPropertyChange("EnableUndo");
            }
        }

        public bool EnableRedo
        {
            get { return _enableRedo; }
            set
            {
                _enableRedo = value;
                OnPropertyChange("EnableRedo");
            }
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        public void AddEmployee(string name)
        {
            _employeeList.Clear();
            foreach (string value in _clientEndPoint.AddEmployee(name))
            {
                _employeeList.Add(value);
            }
            EnableUndo = _employeeList.Count() > 0 ? true : false;
        }

        /// <summary>
        /// Undoes this instance.
        /// </summary>
        public void Undo()
        {
            string result = _clientEndPoint.Undo();
            ExtractOutput(result);
        }

        /// <summary>
        /// Redoes this instance.
        /// </summary>
        public void Redo()
        {
            string result = _clientEndPoint.Redo();
            ExtractOutput(result);
        }

        private void ExtractOutput(string result)
        {
            BaseResult _baseResult = JsonConvert.DeserializeObject<BaseResult>(result);

            switch (_baseResult.ActionName)
            {
                case "EmployeeAction":
                    EnableUndo = _baseResult.EnableUndo;
                    EnableRedo = _baseResult.EnableRedo;
                    _employeeList.Clear();
                    List<string> employeeList = JsonConvert.DeserializeObject<EmployeeResult>(result).EmployeeList;
                    foreach (string value in employeeList)
                    {
                        _employeeList.Add(value);
                    }
                    break;
            }

        }

    }
}
