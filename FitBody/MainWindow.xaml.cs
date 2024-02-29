using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Threading;
using static FitBody.App;
using System.Diagnostics.Metrics;
using System.Windows.Controls.Primitives;
using Org.BouncyCastle.Math.EC;
using System.Diagnostics.Eventing.Reader;
using Mysqlx.Notice;
using System.Drawing;

namespace FitBody
{
    public partial class MainWindow : Window
    {
        private int _userId;
        private int kcalMain = 0;

        private double panelWidth;
        private bool hidden;
        private DispatcherTimer timer;

        public MainWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += MainWindow_Loaded;
            LoadRandomCuriosity();
            try
            {
                MySqlConnection conn = DatabaseHelper.GetConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `profile` WHERE `users_idUser` = " + _userId + " ", conn);


                int goal = 0, weight = 0, actualization = 0;

                if (cmd.ExecuteScalar() != null)
                {
                    var reader = cmd.ExecuteReader();
                    reader.Read();

                    int len = reader.GetString(1).Length;
                    string weight2 = reader.GetString(1).Substring(0, len - 3);
                    weight = Int32.Parse(weight2);

                    len = reader.GetString(7).Length;
                    yourgoal.Text = reader.GetString(7).Substring(0, len - 3) + "KG";
                    string goal2 = reader.GetString(7).Substring(0, len - 3);
                    goal = Int32.Parse(goal2);
                }
                int whole = Math.Abs(goal - weight);
                

                conn.Close();

                conn = DatabaseHelper.GetConnection();

                cmd = new MySqlCommand("SELECT Calf,Thigh,Waist,Belt,Chest,Neck,Biceps,Forearm,Weight,kcal FROM measurements WHERE users_idUser = @userId", conn);
                cmd.Parameters.AddWithValue("@userId", _userId);
                if (cmd.ExecuteScalar() != null)
                {
                    var reader = cmd.ExecuteReader();
                    reader.Read();

                    string actualization2 = reader.GetString(8);
                    actualization = Int32.Parse(actualization2);

                    string kal = reader.GetString(9);
                    kcalMain = Int32.Parse(kal);

                }

                if (goal >= weight) actualization = actualization - weight;
                else actualization = weight - actualization;


                int progresPanel = 150 / whole;
                actPAnel.Width = progresPanel * actualization;


                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Błąd: " + ex.Message);
            }

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;

            double kalor = 0, prot = 0, fat = 0, carb = 0;
            DateTime selectedDate = DateTime.Today;
            string content = GetTrainingContent(selectedDate);
            List<string> contentDiet = GetDietContent(selectedDate);
            if (contentDiet != null && contentDiet.Any())
            {
                sniadanie.Text = contentDiet[0] + Environment.NewLine;
                string meal = contentDiet[0].ToString();
                List<string> productList = GetProductContent(meal);
                int k = 0;
                double kcalSniadanie = kcalMain * 0.2;

                foreach (var i in productList) k++;
                k = k / 5;
                kcalSniadanie = kcalSniadanie / k;
                k = 0;
                double reszta = 0, reszta2 = 0;
                foreach (var i in productList)
                {
                    kcalSniadanie = kcalSniadanie + reszta;
                    reszta2 = reszta;
                    reszta = 0; 
                    if (k % 5 == 0)
                    {
                        sniadanieProdukty.Text += i;
                        double gramy = Int32.Parse(productList[k + 1]);
                        if (gramy <= 50)
                        {
                            kcalSniadanie = kcalSniadanie / 4;
                            reszta = kcalSniadanie * 3;
                            reszta2 = reszta;
                        }
                        gramy = kcalSniadanie / gramy;

                        if (reszta > 0) kcalSniadanie = kcalSniadanie * 4;

                        kcalSniadanie = kcalSniadanie - reszta2;
                        reszta2 = 0;

                        sniadanieProdukty.Text += (String.Format("{0:N0}", gramy*100)) + "  gram" + Environment.NewLine;

                        kalor += gramy * Int32.Parse(productList[k + 1]);
                        prot += gramy * Int32.Parse(productList[k + 2]);
                        carb += gramy * Int32.Parse(productList[k + 3]);
                        fat += gramy * Int32.Parse(productList[k + 4]);
                    }
                    k++;
                }
                lunch.Text = contentDiet[1] + Environment.NewLine;
                meal = contentDiet[1].ToString();
                productList = GetProductContent(meal);
                k = 0;
                double kcalLunch = kcalMain * 0.3;

                foreach (var i in productList) k++;
                k = k / 5;
                kcalLunch = kcalLunch / k;
                k = 0;
                foreach (var i in productList)
                {
                    kcalLunch = kcalLunch + reszta;
                    reszta2 = reszta;
                    reszta = 0;
                    if (k % 5 == 0)
                    {
                        lunchProdukty.Text += i;
                        double gramy = Int32.Parse(productList[k + 1]);
                        if (gramy <= 50)
                        {
                            kcalLunch = kcalLunch / 4;
                            reszta = kcalLunch * 3;
                            reszta2 = reszta;
                        }
                        gramy = kcalLunch / gramy;


                        if (reszta > 0) kcalLunch = kcalLunch * 4;

                        kcalLunch = kcalLunch - reszta2;
                        reszta2 = 0;

                        lunchProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;

                        
                        kalor += gramy * Int32.Parse(productList[k + 1]);
                        prot += gramy * Int32.Parse(productList[k + 2]);
                        carb += gramy * Int32.Parse(productList[k + 3]);
                        fat += gramy * Int32.Parse(productList[k + 4]);
                    }
                    k++;
                }
                obiad.Text = contentDiet[2] + Environment.NewLine;
                meal = contentDiet[2].ToString();
                productList = GetProductContent(meal);
                k = 0;
                double kcalObiad = kcalMain * 0.5;

                foreach (var i in productList) k++;
                k = k / 5;
                kcalObiad = kcalObiad / k;
                k = 0;
                foreach (var i in productList)
                {
                    kcalObiad = kcalObiad + reszta;
                    reszta2 = reszta;
                    reszta = 0;
                    if (k % 5 == 0)
                    {
                        obiadProdukty.Text += i;
                        double gramy = Int32.Parse(productList[k + 1]);
                        if (gramy <= 50)
                        {
                            kcalObiad = kcalObiad / 4;
                            reszta = kcalObiad * 3;
                            reszta2 = reszta;
                        }
                        gramy = kcalObiad / gramy;

                        if (reszta > 0) kcalObiad = kcalObiad * 4;

                        kcalObiad = kcalObiad - reszta2;
                        reszta2 = 0;

                        obiadProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;

                        
                        kalor += gramy * Int32.Parse(productList[k + 1]);
                        prot += gramy * Int32.Parse(productList[k + 2]);
                        carb += gramy * Int32.Parse(productList[k + 3]);
                        fat += gramy * Int32.Parse(productList[k + 4]);
                    }
                    k++;
                }
                kolacja.Text = contentDiet[3] + Environment.NewLine;
                meal = contentDiet[3].ToString();
                productList = GetProductContent(meal);
                k = 0;
                double kcalKolacja = kcalMain * 0.3;

                foreach (var i in productList) k++;
                k = k / 5;
                kcalKolacja = kcalKolacja / k;
                k = 0;
                foreach (var i in productList)
                {
                    kcalKolacja = kcalKolacja + reszta;
                    reszta2 = reszta;
                    reszta = 0;
                    if (k % 5 == 0)
                    {
                        kolacjaProdukty.Text += i;
                        double gramy = Int32.Parse(productList[k + 1]);
                        if (gramy <= 50)
                        {
                            kcalKolacja = kcalKolacja / 4;
                            reszta = kcalKolacja * 3;
                            reszta2 = reszta;
                        }
                        gramy = kcalKolacja / gramy;

                        if (reszta > 0) kcalKolacja = kcalKolacja * 4;

                        kcalKolacja = kcalKolacja - reszta2;
                        reszta2 = 0;

                        kolacjaProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;

                        
                        kalor += gramy * Int32.Parse(productList[k + 1]);
                        prot += gramy * Int32.Parse(productList[k + 2]);
                        carb += gramy * Int32.Parse(productList[k + 3]);
                        fat += gramy * Int32.Parse(productList[k + 4]);
                    }
                    k++;
                }
                kalorie.Text = (String.Format("{0:N0}", kalor ));
                bialko.Text = (String.Format("{0:N0}", prot));
                wegle.Text = (String.Format("{0:N0}", carb));
                tluszcze.Text = (String.Format("{0:N0}", fat));

            }
            trening.Text = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public class Diet
        {
            public string Meal { get; set; }
            public string Product { get; set; }
            public string Size { get; set; }
            public string Kcal { get; set; }
            public string Protein { get; set; }
            public string Fat { get; set; }
            public string Carb { get; set; }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            TrainingWindow trainingWindow = new TrainingWindow(_userId);
            trainingWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            DietWindow dietWindow = new DietWindow(_userId);
            dietWindow.Show();
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = DatabaseHelper.GetConnection();

            MySqlCommand cmd = new MySqlCommand("select UserName from users where idUser='" + _userId + "'", conn);
            MySqlCommand cmd1 = new MySqlCommand("select Email from users where idUser='" + _userId + "'", conn);

            string _Username = (string)cmd.ExecuteScalar();
            conn.Close();

            conn.Open();
            string _Email = (string)cmd1.ExecuteScalar();
            conn.Close();
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            SettingsWindow settingsWindow = new SettingsWindow(_userId, _Username, _Email);
            settingsWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            MeasurementWindow measurementWindow = new MeasurementWindow(_userId);
            measurementWindow.Show();
            this.Close();
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            ExercisesWindow exercisesWindow = new ExercisesWindow(_userId);
            exercisesWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            RecipesWindow recipesWindow = new RecipesWindow(_userId);
            recipesWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowSizeManager.HeightWindow = this.Height;
            WindowSizeManager.WidthWindow = this.Width;
            DietWindow dietWindow = new DietWindow(_userId);
            dietWindow.Show();
            this.Close();
        }

        private Dictionary<DateTime, string> contentDictionary = new Dictionary<DateTime, string>();

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                double kalor = 0, prot = 0, fat = 0, carb = 0;
                DateTime selectedDate = (DateTime)e.AddedItems[0];
                string content = GetTrainingContent(selectedDate);
                List<string> contentDiet = GetDietContent(selectedDate);
                if (contentDiet != null && contentDiet.Any())
                {
                    sniadanie.Text = contentDiet[0] + Environment.NewLine;
                    string meal = contentDiet[0].ToString();
                    List<string> productList = GetProductContent(meal);
                    int k = 0;
                    double kcalSniadanie = kcalMain * 0.2;

                    foreach (var i in productList) k++;
                    k = k / 5;
                    kcalSniadanie = kcalSniadanie / k;
                    k = 0;
                    double reszta = 0, reszta2 = 0;
                    sniadanieProdukty.Text = "";
                    foreach (var i in productList)
                    {
                        kcalSniadanie = kcalSniadanie + reszta;
                        reszta2 = reszta;
                        reszta = 0;
                        if (k % 5 == 0)
                        {
                            sniadanieProdukty.Text += i;
                            double gramy = Int32.Parse(productList[k + 1]);
                            if (gramy <= 50)
                            {
                                kcalSniadanie = kcalSniadanie / 4;
                                reszta = kcalSniadanie * 3;
                                reszta2 = reszta;

                            }
                            gramy = kcalSniadanie / gramy;

                            if (reszta > 0) kcalSniadanie = kcalSniadanie * 4;

                            kcalSniadanie = kcalSniadanie - reszta2;
                            reszta2 = 0;

                            sniadanieProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;

                            kalor += gramy * Int32.Parse(productList[k + 1]);
                            prot += gramy * Int32.Parse(productList[k + 2]);
                            carb += gramy * Int32.Parse(productList[k + 3]);
                            fat += gramy * Int32.Parse(productList[k + 4]);

                        }
                        k++;
                    }
                    lunch.Text = contentDiet[1] + Environment.NewLine;
                    meal = contentDiet[1].ToString();
                    productList = GetProductContent(meal);
                    k = 0;
                    double kcalLunch = kcalMain * 0.3;

                    foreach (var i in productList) k++;
                    k = k / 5;
                    kcalLunch = kcalLunch / k;
                    k = 0;
                    lunchProdukty.Text = "";
                    foreach (var i in productList)
                    {
                        kcalLunch = kcalLunch + reszta;
                        reszta2 = reszta;
                        reszta = 0;
                        if (k % 5 == 0)
                        {
                            lunchProdukty.Text += i;
                            double gramy = Int32.Parse(productList[k + 1]);
                            if (gramy <= 50)
                            {
                                kcalLunch = kcalLunch / 4;
                                reszta = kcalLunch * 3;
                                reszta2 = reszta;

                            }
                            gramy = kcalLunch / gramy;

                            if (reszta > 0) kcalLunch = kcalLunch * 4;

                            kcalLunch = kcalLunch - reszta2;
                            reszta2 = 0;

                            lunchProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;


                            kalor += gramy * Int32.Parse(productList[k + 1]);
                            prot += gramy * Int32.Parse(productList[k + 2]);
                            carb += gramy * Int32.Parse(productList[k + 3]);
                            fat += gramy * Int32.Parse(productList[k + 4]);
                        }
                        k++;
                    }
                    obiad.Text = contentDiet[2] + Environment.NewLine;
                    meal = contentDiet[2].ToString();
                    productList = GetProductContent(meal);
                    k = 0;
                    double kcalObiad = kcalMain * 0.5;

                    foreach (var i in productList) k++;
                    k = k / 5;
                    kcalObiad = kcalObiad / k;
                    k = 0;
                    obiadProdukty.Text = "";
                    foreach (var i in productList)
                    {
                        

                        kcalObiad = kcalObiad + reszta;
                        reszta = 0;
                        if (k % 5 == 0)
                        {
                            obiadProdukty.Text += i;
                            double gramy = Int32.Parse(productList[k + 1]);
                            if (gramy <= 50)
                            {
                                kcalObiad = kcalObiad / 4;
                                reszta = kcalObiad * 3;
                                reszta2 = reszta;
                            }
                            gramy = kcalObiad / gramy;

                            if (reszta > 0) kcalObiad = kcalObiad * 4;

                            kcalObiad = kcalObiad - reszta2;

                            reszta2 = 0;

                            obiadProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;


                            kalor += gramy * Int32.Parse(productList[k + 1]);
                            prot += gramy * Int32.Parse(productList[k + 2]);
                            carb += gramy * Int32.Parse(productList[k + 3]);
                            fat += gramy * Int32.Parse(productList[k + 4]);
                        }
                        k++;
                    }
                    kolacja.Text = contentDiet[3] + Environment.NewLine;
                    meal = contentDiet[3].ToString();
                    productList = GetProductContent(meal);
                    k = 0;
                    double kcalKolacja = kcalMain * 0.3;

                    foreach (var i in productList) k++;
                    k = k / 5;
                    kcalKolacja = kcalKolacja / k;
                    k = 0;
                    kolacjaProdukty.Text = "";
                    foreach (var i in productList)
                    {
                        kcalKolacja = kcalKolacja + reszta;
                        reszta2 = reszta;
                        reszta = 0;
                        if (k % 5 == 0)
                        {
                            kolacjaProdukty.Text += i;
                            double gramy = Int32.Parse(productList[k + 1]);
                            if (gramy <= 50)
                            {
                                kcalKolacja = kcalKolacja / 4;
                                reszta = kcalKolacja * 3;
                                reszta2 = reszta;

                            }
                            gramy = kcalKolacja / gramy;

                            if (reszta > 0) kcalKolacja = kcalKolacja * 4;

                            kcalKolacja = kcalKolacja - reszta2;
                            reszta2 = 0;

                            kolacjaProdukty.Text += (String.Format("{0:N0}", gramy * 100)) + "  gram" + Environment.NewLine;


                            kalor += gramy * Int32.Parse(productList[k + 1]);
                            prot += gramy * Int32.Parse(productList[k + 2]);
                            carb += gramy * Int32.Parse(productList[k + 3]);
                            fat += gramy * Int32.Parse(productList[k + 4]);
                        }
                        k++;
                    }
                    kalorie.Text = (String.Format("{0:N0}", kalor));
                    bialko.Text = (String.Format("{0:N0}", prot));
                    wegle.Text = (String.Format("{0:N0}", carb));
                    tluszcze.Text = (String.Format("{0:N0}", fat));

                }
                trening.Text = content;
            }
        }

        private string GetTrainingContent(DateTime date)
        {
            string content = string.Empty;
            DateTime selectedDate = new DateTime(date.Year, date.Month, date.Day);

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string selectQuery = "SELECT Exercises FROM training_user WHERE Day = @selectedDate AND Userid = @userID";
                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, conn))
                {
                    selectCommand.Parameters.AddWithValue("@selectedDate", selectedDate.ToString("yyyy.MM.dd"));
                    selectCommand.Parameters.AddWithValue("@userID", _userId);

                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            content += reader.GetString("Exercises") + Environment.NewLine + Environment.NewLine;
                        }
                    }
                }

                conn.Close();
            }

            return content;
        }

        private List<string> GetDietContent(DateTime date)
        {
            DateTime selectedDate = new DateTime(date.Year, date.Month, date.Day);

            List<string> typeMealList = new List<string>();
            List<string> mealList = new List<string>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string cmd = "SELECT Id_meal FROM `diet_user` WHERE Day = @selectedDate AND Id_user = @userID";
                MySqlCommand selectCommand = new MySqlCommand(cmd, conn);
                selectCommand.Parameters.AddWithValue("@selectedDate", selectedDate.ToString("yyyy.MM.dd"));
                selectCommand.Parameters.AddWithValue("@userID", _userId);

                using (MySqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string _idMeal = reader.GetString("Id_meal");
                        mealList.Add($"{_idMeal}");
                    }
                }

                if (mealList != null && mealList.Any())
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cmd = "SELECT Name, _Type FROM `meal` WHERE idMeal = @idMeal";
                        selectCommand = new MySqlCommand(cmd, conn);
                        selectCommand.Parameters.AddWithValue("@idMeal", mealList[i]);
                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string _Type = reader.GetString("_Type");
                                typeMealList.Add($"{_Type}");
                                string _idMeal = reader.GetString("Name");
                                mealList[i] = _idMeal;
                            }
                        }
                    }
                }

                conn.Close();
            }

            return mealList;
        }

        private List<string> GetProductContent(string meal)
        {
            //List<string> finalProductList = new List<string>();
            List<string> productList = new List<string>();
            //List<string> kcalProductList = new List<string>();
            //List<string> protainProductList = new List<string>();
            //List<string> carbProductList = new List<string>();
            //List<string> fatProductList = new List<string>();
            string _idmeal = "";

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string cmd = "SELECT idMeal FROM meal WHERE Name = @name";
                MySqlCommand selectCommand = new MySqlCommand(cmd, conn);
                selectCommand.Parameters.AddWithValue("@name", meal);

                using (MySqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _idmeal = reader.GetString("idMeal");
                    }
                }

                cmd = "SELECT * FROM `product` WHERE meal_idMeal = @idMeal";
                selectCommand = new MySqlCommand(cmd, conn);
                selectCommand.Parameters.AddWithValue("@idMeal", _idmeal);

                using (MySqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string result = reader.GetString("Name");
                        productList.Add(result + " ");
                        result = reader.GetString("Kcal");
                        productList.Add(result);
                        result = reader.GetString("Proteins");
                        productList.Add(result);
                        result = reader.GetString("Carbohydrates");
                        productList.Add(result);
                        result = reader.GetString("Fats");
                        productList.Add(result);
                    }
                }

                conn.Close();
            }

            return productList;
        }

        private void LoadRandomCuriosity()
        {
            Random random = new Random();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT content FROM curiosities ORDER BY RAND() LIMIT 3";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string content = reader.GetString("content");
                    curio1.Text = content;
                }

                if (reader.Read())
                {
                    string content = reader.GetString("content");
                    curio2.Text = content;
                }

                if (reader.Read())
                {
                    string content = reader.GetString("content");
                    curio3.Text = content;
                }

                reader.Close();
            }
        }

    }
}