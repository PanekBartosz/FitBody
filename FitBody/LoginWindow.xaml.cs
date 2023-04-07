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

namespace FitBody
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
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

        private void resetPassButton_Click(object sender, RoutedEventArgs e)
        {
            ResetPassWindow resetPassWindow = new ResetPassWindow();
            resetPassWindow.Show();
            this.Close();
        }

        private void logginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
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
