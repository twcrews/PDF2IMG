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

namespace Crews.Utility.PDF2IMG
{
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl
    {
        /// <summary>
        /// Controls the text inside the button.
        /// </summary>
        public string Text
        {
            get { return textLabel.Content.ToString(); }
            set { textLabel.Content = value; }
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public CustomButton()
        {
            InitializeComponent();
        }

        private void CustomButton_MouseEnter(object sender, MouseEventArgs e)
        {
            mainBorder.Background = new SolidColorBrush(Color.FromRgb(119, 119, 119));
            textLabel.Foreground = new SolidColorBrush(Color.FromRgb(20, 20, 20));
        }

        private void CustomButton_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBorder.Background = new SolidColorBrush(Colors.Transparent);
            textLabel.Foreground = new SolidColorBrush(Color.FromRgb(187, 187, 187));
        }

        private void CustomButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainBorder.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
        }

        private void CustomButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainBorder.Background = new SolidColorBrush(Color.FromRgb(119, 119, 119));
        }
    }
}
