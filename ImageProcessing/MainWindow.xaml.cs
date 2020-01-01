using ImageProcessing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageProcessing.Helpers;
using ImageProcessing.Model;
using Autofac;
using ImageProcessing.Startup;
using ImageProcessing.ViewModel.IViewModel;

namespace ImageProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IThumbnailViewModel _thumbnailViewModel;
        private IContainer Container { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Container = ApplicationConfiguration.BuildContainer();
            _thumbnailViewModel = Container.Resolve<IThumbnailViewModel>();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _thumbnailViewModel;
            Images.ItemsSource = _thumbnailViewModel.ThumbnailImages;
            Employees.ItemsSource = _thumbnailViewModel.EmployeeList;

        }

        /// <summary>
        /// Handles the ScrollChanged event of the Images control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ScrollChangedEventArgs"/> instance containing the event data.</param>
        private void Images_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            VirtualizingStackPanel panel = VisualChild.Find<VirtualizingStackPanel>(Images);
            if (Images.Items.Count > 0 && panel != null)
            {
                int offset =
                  (panel.Orientation == Orientation.Horizontal) ? (int)panel.HorizontalOffset : (int)panel.VerticalOffset;
                var item = Images.Items[offset] as ImageItem;
                _thumbnailViewModel.BindThumbnailImages(item.ImageID);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnAddText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnAddText_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtString.Text.Trim()))
                return;

            _thumbnailViewModel.AddEmployee(txtString.Text);
            txtString.Clear();
            txtString.Focus();
        }

        /// <summary>
        /// Handles the Click event of the Undo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            _thumbnailViewModel.Undo();
        }

        /// <summary>
        /// Handles the Click event of the Redo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            _thumbnailViewModel.Redo();
        }

    }
}
