using Microsoft.Win32;
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

namespace PDF2IMG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach (string fileName in fileNames)
                {
                    if (System.IO.Path.GetExtension(fileName).ToUpperInvariant() != ".PDF")
                    {
                        e.Effects = DragDropEffects.None;
                        e.Handled = true;
                    }
                }
            }
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
                successLabel.Content = "Working, please wait...";
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                int pages = await Task.Run(() => App.Convert(files[0]));
                ShowSuccessMessage(pages);
        }

        private async void CustomButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "PDF documents (.pdf)|*.pdf"
            };
            if (dialog.ShowDialog() == true)
            {
                successLabel.Content = "Working, please wait...";
                int pages = await Task.Run(() => App.Convert(dialog.FileName));
                ShowSuccessMessage(pages);
            }
        }

        private void ShowSuccessMessage(int pages)
        {
            successLabel.Content = "Successfully converted " + pages.ToString() + " page(s).";
        }
    }
}
