using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Threading;
using static FitBody.App;

namespace FitBody
{
    public partial class TrainingWindow : Window
    {
        private int _userId;
        public TrainingWindow(int userId)
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

            Loaded += TrainingWindow_Loaded;
        }
        private void TrainingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;
        }

        public List<string> trainingGenerator()
        {
            List<string> exercises = new List<string>();

            string advancement = getAdvancementLevel();

            string name = getTrainingName();

            int dayMax = 3;
            if (name == "SPL") { dayMax = 6; }
            else { dayMax = 3; }

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                for (int i = 1; i <= dayMax; i++)
                {
                    string getTrainingIdQuery = "SELECT idTraining FROM trainings WHERE Advancement = @advancement AND Day = @day AND LEFT(Name,3) = LEFT(@name,3)";
                    using (MySqlCommand getTrainingIdCommand = new MySqlCommand(getTrainingIdQuery, conn))
                    {
                        getTrainingIdCommand.Parameters.AddWithValue("@advancement", advancement);
                        getTrainingIdCommand.Parameters.AddWithValue("@day", i);
                        getTrainingIdCommand.Parameters.AddWithValue("@name", name);

                        int idTraining = Convert.ToInt32(getTrainingIdCommand.ExecuteScalar());

                        string getExercisesQuery = "SELECT Name,Sets,Repetitions FROM exercises WHERE trainings_idTraining = @idTraining";
                        using (MySqlCommand getExercisesCommand = new MySqlCommand(getExercisesQuery, conn))
                        {
                            getExercisesCommand.Parameters.AddWithValue("@idTraining", idTraining);

                            using (MySqlDataReader reader = getExercisesCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string exerciseName = reader.GetString("Name");
                                    string sets = reader.GetString("Sets");
                                    string repetitions = reader.GetString("Repetitions");


                                    if (sets == "5-7 min")
                                    {
                                        exercises.Add($"{exerciseName} ({sets})");
                                    }
                                    else if (sets == "")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        exercises.Add($"{exerciseName} ({sets} x {repetitions})");
                                    }
                                }
                            }
                        }
                        if (i <= dayMax)
                        {
                            exercises.Add("/");
                        }
                    }
                }
                conn.Close();
            }
            return exercises;
        }
        public void InsertTrainingUser()
        {
            List<string> exercises = trainingGenerator();
            string trainingName = getTrainingName();
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(29);

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                DeleteRecordsFromDate(conn, startDate);
                DateTime trainingDate = startDate;
                List<string> segment = new List<string>();

                while (trainingDate <= endDate)
                {
                    foreach (string exercise in exercises)
                    {
                        if (exercise == "/")
                        {
                            InsertSegment(conn, trainingName, segment, trainingDate);
                            trainingDate = trainingDate.AddDays(1);
                            segment.Clear();

                            if ((trainingName == "FBW" || trainingName == "PPL") && trainingDate < endDate)
                            {
                                InsertRestDay(conn, trainingName, trainingDate);
                                trainingDate = trainingDate.AddDays(1);
                            }
                            else if (trainingName == "SPL" && trainingDate.DayOfWeek == DayOfWeek.Sunday && trainingDate < endDate)
                            {
                                InsertRestDay(conn, trainingName, trainingDate);
                                trainingDate = trainingDate.AddDays(1);
                            }

                        }
                        else
                        {
                            segment.Add(exercise);
                        }
                    }
                }

                conn.Close();
            }
        }


        private void InsertSegment(MySqlConnection conn, string trainingName, List<string> segment, DateTime trainingDate)
        {
            foreach (string exercise in segment)
            {
                string insertQuery = "INSERT INTO training_user (TrainingName, Exercises, Day, Userid) VALUES (@trainingName, @exercise, @trainingDate, @userID)";
                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, conn))
                {
                    insertCommand.Parameters.AddWithValue("@trainingName", trainingName);
                    insertCommand.Parameters.AddWithValue("@exercise", exercise);
                    insertCommand.Parameters.AddWithValue("@trainingDate", trainingDate.ToString("yyyy.MM.dd"));
                    insertCommand.Parameters.AddWithValue("@userID", _userId);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        private void InsertRestDay(MySqlConnection conn, string trainingName, DateTime trainingDate)
        {
            string insertQuery = "INSERT INTO training_user (TrainingName, Exercises, Day, Userid) VALUES (@trainingName, 'DZIŚ MASZ WOLNE', @trainingDate, @userID)";
            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, conn))
            {
                insertCommand.Parameters.AddWithValue("@trainingName", trainingName);
                insertCommand.Parameters.AddWithValue("@trainingDate", trainingDate.ToString("yyyy.MM.dd"));
                insertCommand.Parameters.AddWithValue("@userID", _userId);

                insertCommand.ExecuteNonQuery();
            }
        }

        private void DeleteRecordsFromDate(MySqlConnection conn, DateTime fromDate)
        {
            string deleteQuery = "DELETE FROM training_user WHERE Day >= @fromDate";
            using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, conn))
            {
                deleteCommand.Parameters.AddWithValue("@fromDate", fromDate.ToString("yyyy-MM-dd"));
                deleteCommand.ExecuteNonQuery();
            }
        }

        private string getTrainingName()
        {
            string name = "";
            int trainingFrequency = 0;

            MySqlConnection conn = DatabaseHelper.GetConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT TrainingFrequency FROM profile WHERE users_idUser = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", _userId);

            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                trainingFrequency = Convert.ToInt32(result);
            }
            conn.Close();


            if (trainingFrequency == 0) { name = "FBW"; }
            else if (trainingFrequency == 1) { name = "PPL"; }
            else if (trainingFrequency == 2) { name = "SPL"; }

            return name;
        }

        private string getAdvancementLevel()
        {
            string advancement = "0";

            MySqlConnection conn = DatabaseHelper.GetConnection();
            MySqlCommand cmd = new MySqlCommand("SELECT Advancement FROM profile WHERE users_idUser = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", _userId);

            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                advancement = Convert.ToString(result);
            }
            conn.Close();

            return advancement;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            var loadingWindow = new LoadWindow();

            
            loadingWindow.Show();
            this.Close();

            var backgroundThread = new Thread(() =>
            {
                // Wywołanie funkcji wykonujących się w bazie danych
                InsertTrainingUser();
                // Zamknięcie okna ładowania za pomocą Dispatcher.Invoke
                loadingWindow.Dispatcher.Invoke(() =>
                {
                    MainWindow mainWindow = new MainWindow(_userId);
                    mainWindow.Show();
                    loadingWindow.Close();
                });
            });

            backgroundThread.Start();
        }

        private void submitTraining_Click(object sender, RoutedEventArgs e)
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
                        cmd = new MySqlCommand("INSERT INTO `profile` (`idProfile`, `Weight`, `Height`, `Gender`, `Age`, `Advancement`, `TrainingFrequency`, `goal`, `users_idUser`) VALUES ('" + idProfile + "', '" + w + "', '" + h + "', '" + p + "', '" + a + "', '" + adv + "', '" + frq + "', '" + g + "','" + _userId + "')", conn);
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
    }
}
