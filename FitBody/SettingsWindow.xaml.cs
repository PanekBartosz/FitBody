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
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private int _userId;
        
        public SettingsWindow(int userId, string user, string email)
        {
            InitializeComponent();
            TxtUserName.Text = user;
            TxtEmail.Text = email;
            _userId = userId;
            Loaded += SettingsWindow_Loaded;
        }
        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;
        }



        //Closing windows and etc.

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            MainWindow mainWindow = new MainWindow(_userId);
            mainWindow.Show();
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

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox textBox = (PasswordBox)sender;
            if ((string)textBox.Tag != "typed")
            {
                MessageBoxError.Clear();
            }
        }

        private void emailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Podaj e-mail";
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(127, 0, 0, 0));
                textBox.Tag = null;
            }
        }
     
        private void username_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Podaj nazwe";
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(127, 0, 0, 0));
                textBox.Tag = null;
            }
        }


        //Change password, username and email


        private void ChangeUserName(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = DatabaseHelper.GetConnection();
                MySqlCommand command = conn.CreateCommand();

                command.CommandText = "UPDATE users SET UserName = @nowaNazwa WHERE idUser =" + _userId;
                if (txtnewUserName.Text != "Podaj nazwe")
                {
                    command.Parameters.AddWithValue("@nowaNazwa", txtnewUserName.Text);
                }
                else
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Nie udało się zmienić nazwy użytkownika.";
                    return;
                }

                int rowsAffected = command.ExecuteNonQuery();

                conn.Close();

                if (rowsAffected > 0 && txtnewUserName.Text != null && txtnewUserName.Text != "" && txtnewUserName.Text != "Podaj nazwe")
                {

                    TxtUserName.Text = txtnewUserName.Text;
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Green);
                    MessageBoxError.Text = "Nazwa użytkownika została zmieniona.";
                    txtnewUserName.Clear();
                }
                else
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Nie udało się zmienić nazwy użytkownika.";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void ChangePassword(object sender, EventArgs e)
        {

            MySqlConnection conn = DatabaseHelper.GetConnection();
            MySqlCommand cmd = new MySqlCommand("select Password from users where idUser='" + _userId + "'", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string passw = reader.GetString("Password");
                reader.Close();


                if (txtpassword.Password == null || txtnewpassword == null || txtconfirmpassword.Password == null)
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Wprowadź dane";
                    return;
                }


                if (Encrypt.HashString(txtpassword.Password) == passw && Encrypt.HashString(txtnewpassword.Password) == Encrypt.HashString(txtconfirmpassword.Password))
                {

                    MySqlCommand updateCmd = new MySqlCommand("UPDATE users SET Password = @NewPassword WHERE idUser = @UserId", conn);
                    updateCmd.Parameters.AddWithValue("@NewPassword", Encrypt.HashString(txtnewpassword.Password));
                    updateCmd.Parameters.AddWithValue("@UserId", _userId);
                    int result = updateCmd.ExecuteNonQuery();


                    if (result > 0)
                    {
                        MessageBoxError.Foreground = new SolidColorBrush(Colors.Green);
                        MessageBoxError.Text = "Udało się zmienić hasło!";
                        txtconfirmpassword.Clear();
                        txtpassword.Clear();
                        txtnewpassword.Clear();
                    }
                    else
                    {
                        MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                        MessageBoxError.Text = "Nie udało się zmienić hasła";
                        txtconfirmpassword.Clear();
                        txtpassword.Clear();
                        txtnewpassword.Clear();
                    }
                }
                else if (txtpassword.Password != passw)
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Podaj poprawne hasło.";
                    txtconfirmpassword.Clear();
                    txtpassword.Clear();
                    txtnewpassword.Clear();
                }
                else
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Oba hasła muszą być takie same";
                    txtconfirmpassword.Clear();
                    txtnewpassword.Clear();
                }
            }
            conn.Close();
        }

        private void ChangeEmail(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseHelper.GetConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT Password FROM users WHERE idUser='" + _userId + "'", conn);
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM users WHERE Email='" + txtNewEmail.Text + "'", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string passw = reader.GetString("Password");
                reader.Close();

                if (txtPassword1.Password == null || txtNewEmail.Text == null || txtNewEmail.Text == "Podaj e-mail")
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Podaj wartości w puste pola.";
                    return;
                }
                else if (!IsValidEmail(txtNewEmail.Text))
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Podaj poprawny adres e-mail.";
                    txtPassword1.Clear();
                    txtNewEmail.Clear();
                    return;
                }
                else if (Encrypt.HashString(txtPassword1.Password) == passw)
                {
                    MySqlDataReader dr;
                    dr = cmd1.ExecuteReader();
                    bool emailExists = dr.HasRows;
                    dr.Close();

                    if (emailExists)
                    {
                        MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                        MessageBoxError.Text = "Ten email już istnieje.";
                        txtNewEmail.Clear();
                    }
                    else
                    {
                        MySqlCommand updateCmd = new MySqlCommand("UPDATE users SET Email = @NewEmail WHERE idUser ='" + _userId + "'", conn);
                        updateCmd.Parameters.AddWithValue("@NewEmail", txtNewEmail.Text);
                        int result = updateCmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            TxtEmail.Text = txtNewEmail.Text;
                            MessageBoxError.Text = "Udało się zmienić email!";
                            MessageBoxError.Foreground = new SolidColorBrush(Colors.Green);
                            txtPassword1.Clear();
                            txtNewEmail.Clear();
                        }
                        else
                        {
                            MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                            MessageBoxError.Text = "Nie udało się zmienić emailu!";
                            txtPassword1.Clear();
                            txtNewEmail.Clear();
                        }
                    }
                }
                else
                {
                    MessageBoxError.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBoxError.Text = "Podaj poprawne hasło.";
                    txtPassword1.Clear();
                }
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
