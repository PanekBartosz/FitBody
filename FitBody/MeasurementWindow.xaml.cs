using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Controls.Primitives;
using static FitBody.App;

namespace FitBody
{

    public partial class MeasurementWindow : Window
    {
        private int _userId;
        public MeasurementWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += MeasurementWindow_Loaded;
        }
        private void MeasurementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (WindowSizeManager.HeightWindow > 768 || WindowSizeManager.WidthWindow > 1024)
                this.WindowState = WindowState.Maximized;
        }

        private void LoadData (object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = DatabaseHelper.GetConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT Calf,Thigh,Waist,Belt,Chest,Neck,Biceps,Forearm,Weight FROM measurements WHERE users_idUser = @userId", conn);
                cmd.Parameters.AddWithValue("@userId", _userId);

                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds, "LoadDataBinding");
                dataGridView.DataContext = ds;
                dataGridInsert.DataContext = ds;

                conn.Close();
            }
            catch (Exception ex)
            {
                string er1 = string.Format("Connection Failed", ex.Message);
                MessageBox.Show(er1, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void dataGridView_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.DisplayIndex == 0 || e.Column.DisplayIndex == 2 || e.Column.DisplayIndex == 3)
            {
                e.Cancel = true;
            }
        }


        private void measurementSaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                MySqlConnection conn = DatabaseHelper.GetConnection();

                foreach (DataRowView row in dataGridInsert.Items)
                {
                    float calf = Convert.ToSingle(row["Calf"]);
                    float thigh = Convert.ToSingle(row["Thigh"]);
                    float waist = Convert.ToSingle(row["Waist"]);
                    float belt = Convert.ToSingle(row["Belt"]);
                    float chest = Convert.ToSingle(row["Chest"]);
                    float neck = Convert.ToSingle(row["Neck"]);
                    float biceps = Convert.ToSingle(row["Biceps"]);
                    float forearm = Convert.ToSingle(row["Forearm"]);
                    float weight = Convert.ToSingle(row["Weight"]);

                    MySqlCommand cmd = new MySqlCommand("UPDATE measurements SET Calf = @calf, Thigh = @thigh, Waist = @waist, Belt = @belt, Chest = @chest, Neck = @neck, Biceps = @biceps, Forearm = @forearm, Weight = @weight WHERE users_idUser = " + _userId, conn);
                    cmd.Parameters.AddWithValue("@calf", calf);
                    cmd.Parameters.AddWithValue("@thigh", thigh);
                    cmd.Parameters.AddWithValue("@waist", waist);
                    cmd.Parameters.AddWithValue("@belt", belt);
                    cmd.Parameters.AddWithValue("@chest", chest);
                    cmd.Parameters.AddWithValue("@neck", neck);
                    cmd.Parameters.AddWithValue("@biceps", biceps);
                    cmd.Parameters.AddWithValue("@forearm", forearm);
                    cmd.Parameters.AddWithValue("@weight", weight);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                WindowSizeManager.HeightWindow = this.Height;
                WindowSizeManager.WidthWindow = this.Width;
                MainWindow mainWindow = new MainWindow(_userId);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }

        }

    }
}
