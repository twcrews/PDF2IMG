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

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            BeginConvert((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void CustomButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "PDF documents (.pdf)|*.pdf",
                Multiselect = true
            };
            if (dialog.ShowDialog() == true)
            {
                BeginConvert(dialog.FileNames);
            }
        }

        private async void BeginConvert(string[] files)
        {
            successLabel.Content = "Working, please wait...";
            int totalPages = 0;
            foreach (string file in files)
            {
                totalPages += await Task.Run(() => App.Convert(file));
            }
            successLabel.Content = "Successfully converted " + totalPages.ToString() + 
                " page(s) in " + files.Count().ToString() + " file(s).";
        }
    }
}
