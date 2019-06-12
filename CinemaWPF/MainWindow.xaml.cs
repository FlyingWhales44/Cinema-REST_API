using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CinemaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Button> ButtonList;

        private SeatsClient sclient;

        private List<RESTAPI.Models.SeatModel> seatsList;

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
                int id = Convert.ToInt32((sender as Button).Content);

                var result = sclient.Reservation(id);                
            }
            if(btReleaseRegister.Background == Brushes.Red)//reles id
            {
                int id = Convert.ToInt32((sender as Button).Content);

                var result = sclient.ClearReservation(id);
            }
            if(btBuy.Background == Brushes.Red)
            {
                int id = Convert.ToInt32((sender as Button).Content);

                var result = sclient.BuySeat(id);
            }

            RefreshAsync();
        }

        private void BuyTicketClick(object sender, RoutedEventArgs e)
        {
            if (btRegister.Background == Brushes.Red || btReleaseRegister.Background == Brushes.Red)
            {
                btRegister.Background = Brushes.WhiteSmoke;
                btReleaseRegister.Background = Brushes.WhiteSmoke;
            }
            btBuy.Background = Brushes.Red;
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if (btBuy.Background == Brushes.Red || btReleaseRegister.Background == Brushes.Red)
            {
                btBuy.Background = Brushes.WhiteSmoke;
                btReleaseRegister.Background = Brushes.WhiteSmoke;
            }
            btRegister.Background = Brushes.Red;
        }

        private void ReleaseClick(object sender, RoutedEventArgs e)
        {
            var response = sclient.ClearAll();

            MessageBox.Show("Wszystkie miejsca zostały zwolnione");

        }

        private void ReleaseRegisterClick(object sender, RoutedEventArgs e)
        {
            if (btBuy.Background == Brushes.Red || btRegister.Background == Brushes.Red)
            {
                btBuy.Background = Brushes.WhiteSmoke;
                btRegister.Background = Brushes.WhiteSmoke;
            }
            btReleaseRegister.Background = Brushes.Red;
        }

        private async Task initializeHalls()
        {
             initFirstHallAsync(8, 8);
             initSeccHallAsync(10, 10);
             initThirdHallAsync(15, 15);
        }

        private async Task initFirstHallAsync(int x,int y)
        {
            Button pole = new Button();
            ButtonList = new List<Button>();
            FirstGrid.RowDefinitions.Clear();
            FirstGrid.ColumnDefinitions.Clear();
            FirstGrid.Children.Clear();

            seatsList = await sclient.GetSeats();

            var ls = seatsList.FindAll(f => f.HallId == 1).OrderBy(f => f.Id);


            for (int j = 0; j < x; j++)
                FirstGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < y; i++)
                FirstGrid.RowDefinitions.Add(new RowDefinition());


            int q = 0, w = 0;

            foreach(var s in ls)
            {
                pole = new Button();
                pole.Background = Brushes.WhiteSmoke;

                pole.Content = s.Id;
                pole.Name = "I" + s.Id + "I";

                pole.SetValue(Grid.RowProperty, w);
                pole.SetValue(Grid.ColumnProperty, q);

                pole.PreviewMouseDown += new MouseButtonEventHandler(Func);
                ButtonList.Add(pole);
                FirstGrid.Children.Add(pole);

                q++;
                if(q >= x)
                {
                    w++;
                    q = 0;
                }
            }
        }

        private async Task initSeccHallAsync(int x, int y)
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

            seatsList = await sclient.GetSeats();

            var ls = seatsList.FindAll(f => f.HallId == 2).OrderBy(f => f.Id);

            int q = 0, w = 0;

            foreach (var s in ls)
            {
                pole = new Button();
                pole.Background = Brushes.WhiteSmoke;

                pole.Content = s.Id;
                pole.Name = "I" + s.Id + "I";

                pole.SetValue(Grid.RowProperty, w);
                pole.SetValue(Grid.ColumnProperty, q);

                pole.PreviewMouseDown += new MouseButtonEventHandler(Func);
                ButtonList.Add(pole);
                SecondGrid.Children.Add(pole);

                q++;
                if (q >= x)
                {
                    w++;
                    q = 0;
                }
            }
        }

        private async Task initThirdHallAsync(int x, int y)
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

            int q = 0, w = 0;

            seatsList = await sclient.GetSeats();

            var ls = seatsList.FindAll(f => f.HallId == 3).OrderBy(f => f.Id);

            foreach (var s in ls)
            {
                pole = new Button();
                pole.Background = Brushes.WhiteSmoke;

                pole.Content = s.Id;
                pole.Name = "I" + s.Id + "I";

                pole.SetValue(Grid.RowProperty, w);
                pole.SetValue(Grid.ColumnProperty, q);

                pole.PreviewMouseDown += new MouseButtonEventHandler(Func);
                ButtonList.Add(pole);
                ThirdGrid.Children.Add(pole);

                q++;
                if (q >= x)
                {
                    w++;
                    q = 0;
                }
            }
        }

        private async void RefreshAsync()
        {
            seatsList = await sclient.GetSeats();

            foreach (var s in seatsList)
            {
                if (s.Reservation)
                {
                    var bt = ButtonList.Find(x => Convert.ToInt32(x.Content) == s.Id);
                    bt.Background = Brushes.Yellow;
                }
                if (s.Sold)
                {
                    var bt = ButtonList.Find(x => Convert.ToInt32(x.Content) == s.Id);
                    bt.Background = Brushes.Red;
                }
                if(!s.Sold && !s.Reservation)
                {
                    var bt = ButtonList.Find(x => Convert.ToInt32(x.Content) == s.Id);
                    bt.Background = Brushes.WhiteSmoke;
                }
            }           
        }
    }
}
