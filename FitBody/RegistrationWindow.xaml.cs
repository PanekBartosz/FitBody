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
using System.Windows.Shapes;

namespace FitBody
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }
        private void emailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if ((string)textBox.Tag != "typed")
            {
                textBox.Clear();
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                textBox.Tag = "typed";
            }
        }
        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if ((string)textBox.Tag != "typed")
            {
                textBox.Clear();
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                textBox.Tag = "typed";
            }
        }
        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void closeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void minimizeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void resizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(sender as DependencyObject);

            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
        private void resizeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(sender as DependencyObject);

            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
    }
}
