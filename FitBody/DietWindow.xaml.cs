using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Threading.Tasks;
using static FitBody.App;


namespace FitBody
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class DietWindow : Window
    {
        private int _userId;
        public DietWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;

            try
            {
                MySqlConnection conn = DatabaseHelper.GetConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `profile` WHERE `users_idUser` = " + _userId + " ", conn);

                if (cmd.ExecuteScalar() != null)
                {
                    var reader = cmd.ExecuteReader();
                    reader.Read();

                    string gender = reader.GetString(3);
                    if (gender == "M") woman.IsChecked = true;
                    else man.IsChecked = true;

                    weight.GotFocus -= emailBox_GotFocus;
                    weight.Foreground = new SolidColorBrush(Colors.Black);
                    int len = reader.GetString(1).Length;
                    weight.Text = reader.GetString(1).Substring(0, len - 3);

                    height.GotFocus -= emailBox_GotFocus;
                    height.Foreground = new SolidColorBrush(Colors.Black);
                    len = reader.GetString(2).Length;
                    height.Text = reader.GetString(2).Substring(0, len - 3);


                    age.GotFocus -= emailBox_GotFocus;
                    age.Foreground = new SolidColorBrush(Colors.Black);
                    age.Text = reader.GetString(4);

                    string advance = reader.GetString(5);
                    if (advance == "0") advanced.Text = "Początkujący";
                    else if (advance == "1") advanced.Text = "Średnio-zaawansowany";
                    else advanced.Text = "Zaawansowany"; ;

                    string frequ = reader.GetString(6);
                    if (frequ == "0") frequency.Text = "Niska";
                    else if (frequ == "1") frequency.Text = "Średnia";
                    else frequency.Text = "Wysoka"; ;

                    goal.GotFocus -= emailBox_GotFocus;
                    goal.Foreground = new SolidColorBrush(Colors.Black);
                    len = reader.GetString(7).Length;
                    goal.Text = reader.GetString(7).Substring(0, len - 3);
                }


                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
            Loaded += DietWindow_Loaded;
        }
        private void DietWindow_Loaded(object sender, RoutedEventArgs e)
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

        private void submitDiet_Click(object sender, RoutedEventArgs e)
        {
            double factor = 0, kcal = 0, bmi = 0, adv = 0, frq = 0;
            char p = 'M';


            //Mifflin formula
            if (man.IsChecked == false && woman.IsChecked == false) MessageBox.Show("Płeć nie została wybrana.", "Uwaga");
            else if (!weight.Text.All(Char.IsDigit) || (weight.Text == "")) MessageBox.Show("Waga nie została prawidłowo wprowadzona.", "Uwaga");
            else if (!height.Text.All(Char.IsDigit) || (height.Text == "")) MessageBox.Show("Wzrost nie został prawidłowo wprowadzony.", "Uwaga");
            else if (!age.Text.All(Char.IsDigit) || (age.Text == "")) MessageBox.Show("Wiek nie został prawidłowo wprowadzony.", "Uwaga");
            else if (!goal.Text.All(Char.IsDigit) || (goal.Text == "")) MessageBox.Show("Cel nie został prawidłowo wprowadzony.", "Uwaga");
            else
            {
                if (man.IsChecked == true) { factor = -161; p = 'K'; }
                else if (woman.IsChecked == true)
                {
                    factor = 5; p = 'M';
                }

                int w = Int32.Parse(weight.Text);
                int h = Int32.Parse(height.Text);
                int a = Int32.Parse(age.Text);
                int g = Int32.Parse(goal.Text);
                double ad = 0, balance = 0;


                if (advanced.Text == "Początkujący") { ad = 1.4; adv = 0; }
                else if (advanced.Text == "Średnio-zaawansowany") { ad = 1.5; adv = 1; }
                else if (advanced.Text == "Zaawansowany") { ad = 1.7; adv = 2; }

                if (frequency.Text == "Niska") { ad += 0; frq = 0; }
                else if (frequency.Text == "Średnia") { ad += 0.2; frq = 1; }
                else if (frequency.Text == "Wysoka") { ad += 0.5; frq = 2; }

                if (g < w) balance = -300;
                else if (g > h) balance = 300;

                kcal = (((10 * w) + (6.25 * h) - (5 * a) + factor) * ad) + balance;
                double h2 = Double.Parse(height.Text);

                bmi = w / Math.Pow((h2 / 100), 2);


               kcal_bmi.Content = "Kcal: " + String.Format("{0:N0}", kcal) + " BMI " + String.Format("{0:N1}", bmi) + " ✪";

                try
                {
                    MySqlConnection conn = DatabaseHelper.GetConnection();

                    MySqlCommand cmd = new MySqlCommand("SELECT idProfile FROM profile WHERE users_idUser = @iduser", conn);
                    cmd.Parameters.AddWithValue("@iduser", _userId);



                    if (cmd.ExecuteScalar() == null)
                    {
                        cmd = new MySqlCommand("SELECT MAX(idProfile) FROM profile", conn);
                        int idProfile = (int)cmd.ExecuteScalar();

                        if (cmd.ExecuteScalar() == null) idProfile = 1;
                        else idProfile += 1;

                        //float calf = Convert.ToSingle(row["Calf"]);
                        cmd = new MySqlCommand("INSERT INTO `profile` (`idProfile`, `Weight`, `Height`, `Gender`, `Age`, `Advancement`, `TrainingFrequency`, `goal`, `users_idUser`) VALUES ('" + idProfile + "', '" + w + "', '" + h + "', '" + p + "', '" + a + "', '" + adv + "', '" + frq + "','" + g + "', '" + _userId + "')", conn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        int idProfile = (int)cmd.ExecuteScalar();
                        cmd = new MySqlCommand("DELETE FROM `profile` WHERE `profile`.`idProfile` = " + idProfile + " AND `profile`.`users_idUser` = " + _userId + "", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand("INSERT INTO `profile` (`idProfile`, `Weight`, `Height`, `Gender`, `Age`, `Advancement`, `TrainingFrequency`, `goal`, `users_idUser`) VALUES ('" + idProfile + "', '" + w + "', '" + h + "', '" + p + "', '" + a + "', '" + adv + "', '" + frq + "','" + g + "', '" + _userId + "')", conn);
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new MySqlCommand("UPDATE measurements SET kcal = @kcal WHERE users_idUser = " + _userId, conn);
                    cmd.Parameters.AddWithValue("@kcal", kcal);
                    cmd.ExecuteNonQuery();


                    conn.Close();

                    button_two.IsEnabled = true;
                    button_two.Background = Brushes.DarkRed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message);
                }
            }
        }

        private async void generateDiet_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;

            var loadingWindow = new LoadWindow();
            loadingWindow.Show();
            this.Hide();

            try
            {
                await Task.Run(async () =>
                {
                    await generateDietFun();
                    await generateDiet();

                    // Show the main window and close the loading window on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow mainWindow = new MainWindow(_userId);
                        mainWindow.Show();
                        this.Close();
                        loadingWindow.Close();
                    });
                });
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
            }
        }

        private async Task generateDietFun()
        {
            double factor = 0, kcal = 0, adv = 0, frq = 0;
            char p = 'M';

            await Dispatcher.InvokeAsync(async () =>
            { 
                if (man.IsChecked == true) { factor = -161; p = 'K'; }
                else if (woman.IsChecked == true)
                {
                    factor = 5; p = 'M';
                }

                int w = Int32.Parse(weight.Text);
                int h = Int32.Parse(height.Text);
                int a = Int32.Parse(age.Text);
                int g = Int32.Parse(goal.Text);
                double ad = 0, balance = 0;


                if (advanced.Text == "Początkujący") { ad = 1.4; adv = 0; }
                else if (advanced.Text == "Średnio-zaawansowany") { ad = 1.5; adv = 1; }
                else if (advanced.Text == "Zaawansowany") { ad = 1.7; adv = 2; }

                if (frequency.Text == "Niska") { ad += 0; frq = 0; }
                else if (frequency.Text == "Średnia") { ad += 0.2; frq = 1; }
                else if (frequency.Text == "Wysoka") { ad += 0.5; frq = 2; }

                if (g < w) balance = -300;
                else if (g > h) balance = 300;

                kcal = (((10 * w) + (6.25 * h) - (5 * a) + factor) * ad) + balance;

                WindowSizeManager.HeightWindow = this.Height;
                WindowSizeManager.WidthWindow = this.Width;
                
                this.Close();
            });
        }


        private async Task generateDiet()
        {

            string dateDa = DateTime.Now.ToString("yyyy-MM-dd");
            MySqlConnection conn = DatabaseHelper.GetConnection();

            //MySqlCommand cmd = new MySqlCommand("DELETE FROM `diet_user` WHERE `diet_user`.`Id_user` = " + _userId + "AND `diet_user`.`Day`>= \""+ dateDa +"\"", conn);
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `diet_user` WHERE `diet_user`.`Id_user` = " + _userId + "", conn);
            await cmd.ExecuteNonQueryAsync();

             for (int i=0;i<30;i++)
             {
                 for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        string[] mealMeal = { "Śniadanie", "Lunch", "Obiad", "Kolacja" };

                        cmd = new MySqlCommand("SELECT COUNT(idMeal) FROM `meal` WHERE `meal`.`_Type` = \"" + mealMeal[j] + "\"", conn);
                        long count = (long)await cmd.ExecuteScalarAsync();

                        cmd = new MySqlCommand("SELECT idMeal FROM `meal` WHERE `meal`.`_Type` = \"" + mealMeal[j] +"\"", conn);

                        List<string> mealList = new List<string>();

                        using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string _idMeal = reader.GetString("idMeal");
                                mealList.Add($"{_idMeal}");
                            }
                        }
                        var random = new Random();
                        int index = random.Next(mealList.Count);

                        DateTime dateD = DateTime.Now;
                        dateD = dateD.AddDays(i);
                        string date = dateD.ToString("yyyy-MM-dd");

                        cmd = new MySqlCommand("INSERT INTO `diet_user` (`Day`, `Id_meal`, `Id_user`) VALUES ('" + date + "', '" + mealList[index] + "', '" + _userId + "');", conn);
                        await cmd.ExecuteNonQueryAsync();




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd: " + ex.Message);
                    }
                }
             
             }
            conn.Close();
        }
    }
}
