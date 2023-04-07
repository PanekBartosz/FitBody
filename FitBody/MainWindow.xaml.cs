using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace FitBody
{
  
        /*public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();
            }
        }*/
   
    public partial class MainWindow : Window
    {
        double panelWidth;
        bool hidden;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Diet> diet = new ObservableCollection<Diet>();

            //Create diet
            diet.Add(new Diet { Meal = "Śniadanie"});
            diet.Add(new Diet {Meal = "Banan"});
            diet.Add(new Diet {Meal = "100g"});
            diet.Add(new Diet {Meal = "160", Product = "20", Size = "2", Kcal = "8" });


            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,10);

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
            public string Product { get; set;}
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
    }


}
