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
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Mysqlx.Crud;
using static FitBody.App;

namespace FitBody
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
 
        MySqlDataReader dr, dr1;
        private int _userId;
        public RegistrationWindow(int userId)
        {
            InitializeComponent();

            _userId = userId;
            Loaded += RegistrationWindow_Loaded;
        }
        private void RegistrationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;
        }

        //Buttons and window functions
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        //Passwords

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = txtpassword.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = txtpassword.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock.Visibility = txtpassword.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void confirmpasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock1.Visibility = txtconfirmpassword.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void confirmpasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            overlayTextBlock1.Visibility = txtconfirmpassword.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void confirmpasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            overlayTextBlock1.Visibility = txtconfirmpassword.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
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
        private void emailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "E-MAIL";
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(127, 0, 0, 0));
                textBox.Tag = null;
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
        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void reguButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            RegulationsWindow regulationsWindow = new RegulationsWindow(_userId);
            regulationsWindow.Show();
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

        //Registration -------

        private void Register_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseHelper.GetConnection();
            Encrypt en = new Encrypt();

            if (string.IsNullOrEmpty(txtconfirmpassword.Password) || string.IsNullOrEmpty(txtpassword.Password) ||
                string.IsNullOrEmpty(txtlogin.Text) || string.IsNullOrEmpty(txtemail.Text) || policyCheckBox.IsChecked != true)
            {
                MessageBoxError.Text = "Proszę wpisać wartość we wszystkie wymagane pola";
                conn.Close();
                return;
            }
            else if (txtpassword.Password == txtconfirmpassword.Password && IsValidEmail(txtemail.Text))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE Login = @Login", conn);
                cmd.Parameters.AddWithValue("@Login", txtlogin.Text);
                dr = cmd.ExecuteReader();
                bool userExists = dr.HasRows;
                dr.Close();

                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM users WHERE Email = @Email", conn);
                cmd1.Parameters.AddWithValue("@Email", txtemail.Text);
                dr1 = cmd1.ExecuteReader();
                bool emailExists = dr1.HasRows;
                dr1.Close();

                if (userExists)
                {
                    MessageBoxError.Text = "Użytkownik o takim loginie już istnieje.";
                }
                else if (emailExists)
                {
                    MessageBoxError.Text = "Użytkownik o takim emailu już istnieje.";
                }
                else
                {
                    MySqlCommand cmd2 = new MySqlCommand("SELECT MAX(idUser) FROM users", conn);
                    int maxIdUser = Convert.ToInt32(cmd2.ExecuteScalar()) + 1;
                    cmd2.Dispose();

                    cmd = new MySqlCommand("INSERT INTO users VALUES (@idUser, @Login, @Password, @Email, @username)", conn);
                    cmd.Parameters.AddWithValue("@idUser", null);
                    cmd.Parameters.AddWithValue("@Login", txtlogin.Text);
                    cmd.Parameters.AddWithValue("@Password", Encrypt.HashString(txtpassword.Password));
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@username", txtlogin.Text);
                    cmd.ExecuteNonQuery();

                    cmd2 = new MySqlCommand("INSERT INTO measurements VALUES (@Idm, 0, 0, 0, 0, 0, 0, 0, 0, 0, @FKuser, 0)", conn);
                    cmd2.Parameters.AddWithValue("@Idm", null);
                    cmd2.Parameters.AddWithValue("@FKuser", maxIdUser);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Twoje konto zostało utworzone. Proszę zalogować się.", "Gotowe");
                    WindowSizeManager.HeightWindow = this.Height;
                    WindowSizeManager.WidthWindow = this.Width;
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
            }
            else if (txtpassword.Password != txtconfirmpassword.Password)
            {
                MessageBoxError.Text = "Podane hasła muszą się zgadzać.";
                txtconfirmpassword.Clear();
            }
            else
            {
                MessageBoxError.Text = "Podaj poprawny email.";
                txtconfirmpassword.Clear();
                txtemail.Clear();
            }

            conn.Close();
        }

        private bool IsValidEmail(string email)
        {
            // Prosta walidacja adresu e-mail przy użyciu wyrażeń regularnych
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
