using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading;
using System.Threading.Tasks;
using static FitBody.App;

namespace FitBody
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private int UserID { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            Loaded += LoginWindow_Loaded;
        }
        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;
        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            MessageBoxError.Clear();
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
                MessageBoxError.Clear();
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                textBox.Tag = "typed";
            }
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "LOGIN";
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(127, 0, 0, 0));
                textBox.Tag = null;
            }
        }

        private void resetPassButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            ResetPassWindow resetPassWindow = new ResetPassWindow();
            resetPassWindow.Show();
            this.Close();
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if ((string)textBox.Tag != "typed")
            {
                MessageBoxError.Clear();
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            RegistrationWindow registrationWindow = new RegistrationWindow(UserID);
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

        private async void logginButton_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = DatabaseHelper.GetConnection();
            await Dispatcher.InvokeAsync(async () =>
            {

                string Login = loginTextBox.Text;
                string Loginpassword = passwordBox.Password;

                if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Loginpassword) && Login != "LOGIN")
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT idUser, Password FROM users WHERE Login = @Login", conn);
                    cmd.Parameters.AddWithValue("@Login", Login);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string Accpassw = reader.GetString("Password");
                        int idUser = reader.GetInt32("idUser");
                        reader.Close();

                        if (Accpassw == Encrypt.HashString(Loginpassword))
                        {
                            conn.Close();
                            UserID = idUser;
                            WindowSizeManager.HeightWindow = this.Height;
                            WindowSizeManager.WidthWindow = this.Width;


                            var loadingWindow = new LoadWindow();
                            loadingWindow.Show();
                            this.Close();

                            try
                            {
                                await Task.Run(async () =>
                                {

                                    Thread.Sleep(2000);
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        WindowSizeManager.HeightWindow = this.Height;
                                        WindowSizeManager.WidthWindow = this.Width;
                                        MainWindow mainWindow = new MainWindow(UserID);
                                        mainWindow.Show();
                                        loadingWindow.Close();
                                    });
                                });
                            }
                            catch (Exception ex)
                            {
                                // Obsłuż wyjątek
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBoxError.Text = "Podano błędne dane";
                            passwordBox.Clear();
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBoxError.Text = "Podano błędne dane";
                        passwordBox.Clear();
                    }
                }
                else
                {
                    MessageBoxError.Text = "Podano błędne dane";
                    passwordBox.Clear();
                }

                conn.Close();
            });
        }
    }
}