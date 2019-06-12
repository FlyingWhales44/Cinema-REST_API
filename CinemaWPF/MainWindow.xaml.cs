using Newtonsoft.Json;
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

namespace CinemaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Button> ButtonList;

        public SeatsClient sclient;

        public MainWindow()
        {
            InitializeComponent();

            sclient = new SeatsClient();

            initializeHalls();

            
        }

        

        private void Func(object sender, RoutedEventArgs e)
        {
            if(btRegister.Background == Brushes.Red)
            {
                //rezerwacja biletow
                MessageBox.Show("Zarezerwowałeś bilet: " + (sender as Button).Name);
            }
            if(btRelease.Background == Brushes.Red)
            {
                MessageBox.Show("releeese");
            }
            if(btBuy.Background == Brushes.Red)
            {
                MessageBox.Show("kupiłeś bilet: " + (sender as Button).Name);
            }
        }

        private void BuyTicketClick(object sender, RoutedEventArgs e)
        {
            if (btRegister.Background == Brushes.Red || btRelease.Background == Brushes.Red)
            {
                btRegister.Background = Brushes.WhiteSmoke;
                btRelease.Background = Brushes.WhiteSmoke;
            }
            btBuy.Background = Brushes.Red;
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if (btBuy.Background == Brushes.Red || btRelease.Background == Brushes.Red)
            {
                btBuy.Background = Brushes.WhiteSmoke;
                btRelease.Background = Brushes.WhiteSmoke;
            }
            btRegister.Background = Brushes.Red;
        }

        private void ReleaseClick(object sender, RoutedEventArgs e)
        {
            if (btRegister.Background == Brushes.Red || btBuy.Background == Brushes.Red)
            {
                btRegister.Background = Brushes.WhiteSmoke;
                btBuy.Background = Brushes.WhiteSmoke;
            }
            btRelease.Background = Brushes.Red;
        }

        private void initializeHalls()
        {
            initFirstHall(8, 8);
            initSeccHall(10, 10);
            initThirdHall(15, 15);
        }

        private void initFirstHall(int x,int y)
        {
            Button pole = new Button();
            ButtonList = new List<Button>();
            FirstGrid.RowDefinitions.Clear();
            FirstGrid.ColumnDefinitions.Clear();
            FirstGrid.Children.Clear();

            for (int j = 0; j < x; j++)
                FirstGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < y; i++)
                FirstGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    pole = new Button();
                    pole.Background = Brushes.Blue;

                    var model = sclient.CreateSeat(1);
                    string s = model.Id.ToString();
                    //pole.Name = "Id+" + s +"+id";
                    pole.Content = s;
                    pole.SetValue(Grid.RowProperty, i);
                    pole.SetValue(Grid.ColumnProperty, j);
                    pole.PreviewMouseDown += new MouseButtonEventHandler(Func);
                    ButtonList.Add(pole);
                    FirstGrid.Children.Add(pole);
                }

        }

        private void initSeccHall(int x, int y)
        {
            Button pole = new Button();
            ButtonList = new List<Button>();
            SecondGrid.RowDefinitions.Clear();
            SecondGrid.ColumnDefinitions.Clear();
            SecondGrid.Children.Clear();

            for (int j = 0; j < x; j++)
                SecondGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < y; i++)
                SecondGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    pole = new Button();
                    pole.Background = Brushes.Blue;
                    //var m = sclient.CreateSeat(2);
                    pole.Name = "I" + i + "I" + j;
                    pole.SetValue(Grid.RowProperty, i);
                    pole.SetValue(Grid.ColumnProperty, j);
                    //pole.PreviewMouseDown += new MouseButtonEventHandler(OnClick);
                    ButtonList.Add(pole);
                    SecondGrid.Children.Add(pole);
                }

        }

        private void initThirdHall(int x, int y)
        {
            Button pole = new Button();
            ButtonList = new List<Button>();
            ThirdGrid.RowDefinitions.Clear();
            ThirdGrid.ColumnDefinitions.Clear();
            ThirdGrid.Children.Clear();

            for (int j = 0; j < x; j++)
                ThirdGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < y; i++)
                ThirdGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    pole = new Button();
                    pole.Background = Brushes.Blue;
                    pole.Name = "I" + i + "I" + j;
                    pole.SetValue(Grid.RowProperty, i);
                    pole.SetValue(Grid.ColumnProperty, j);
                    //pole.PreviewMouseDown += new MouseButtonEventHandler(OnClick);
                    ButtonList.Add(pole);
                    ThirdGrid.Children.Add(pole);
                }

        }      
    }
}
