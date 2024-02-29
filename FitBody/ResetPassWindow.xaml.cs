using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using static FitBody.App;

namespace FitBody
{
    public partial class ResetPassWindow : Window
    {
        private MySqlDataReader dr;

        public ResetPassWindow()
        {
            InitializeComponent();
            Loaded += ResetPassWindow_Loaded;
        }
        private void ResetPassWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;
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
        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
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

        private void resetPassButton_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = DatabaseHelper.GetConnection();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", emailBox.Text);
            dr = cmd.ExecuteReader();
            bool emailExists = dr.HasRows;
            dr.Close();

            if (emailExists)
            {
                string ranPass= GeneratePassword(12);

                MySqlCommand updateCmd = new MySqlCommand("UPDATE users SET Password = @NewPassword WHERE Email = @Email", conn);
                updateCmd.Parameters.AddWithValue("@NewPassword", Encrypt.HashString(ranPass));
                updateCmd.Parameters.AddWithValue("@Email", emailBox.Text);
                dr = updateCmd.ExecuteReader();


                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("fitbody.kontakt@outlook.com");
                mailMessage.To.Add(emailBox.Text);
                mailMessage.Subject = "FitBody - Resetowanie hasła";
                mailMessage.Body = $@"<html>
                    <body>
                        <h3>Nowe hasło - FitBody</h3>
                        <p>Szanowny Użytkowniku,</p>
                        <p>Twoje hasło do konta w aplikacji FitBody zostało zresetowane. Poniżej znajduje się Twoje nowe hasło:</p>
                        <p><strong><span style='font-size: 18px;'>{ranPass}</span></strong></p>
                        <p>Zalecamy, abyś jak najszybciej zalogował się do aplikacji FitBody przy użyciu nowego hasła i dokonał jego zmiany na bardziej osobiste. Możesz to zrobić, otwierając aplikację FitBody i korzystając z opcji ""Zmień hasło"" w ustawieniach konta.</p>
                        <p>Jeśli nie żądałeś zmiany hasła lub masz jakiekolwiek pytania dotyczące Twojego konta, skontaktuj się z naszym zespołem obsługi klienta pod adresem fitbody.kontakt@outlook.com.</p>
                        <p>Dziękujemy za korzystanie z aplikacji FitBody!</p>
                        <p>Z poważaniem,<br/>Zespół FitBody</p>
                    </body>
                </html>";

                mailMessage.IsBodyHtml = true;


                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp-mail.outlook.com";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("fitbody.kontakt@outlook.com", "zaq12wsxXSW@!QAZ");
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("Wysłano wiadomość e-mail");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd podczas wysyłania wiadomości e-mail: " + ex.Message);
                }

                WindowSizeManager.HeightWindow = this.Height;
                WindowSizeManager.WidthWindow = this.Width;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBoxError.Text = "Podaj poprawny E-MAIL";
            } 
        }
        static string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }

        public static void emailMessage(string server)
        {
           
        }
    }
}
