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

        System.Timers.Timer DataThread; 
        DispatcherTimer UInterface;

        public MainWindow()
        {
            InitializeComponent();

            sclient = new SeatsClient();

            initializeHall();

            declareTimers();

            startRefreshing();
        }

        private void declareTimers()
        {
            UInterface = new DispatcherTimer();
            DataThread = new System.Timers.Timer();

            UInterface.Interval = TimeSpan.FromSeconds(0.5);
            DataThread.Interval = 500;

            UInterface.Tick += new EventHandler(Refresh);
            DataThread.Elapsed += new ElapsedEventHandler(timer_Tick);        
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                seatsList = sclient.GetSeats();
            });
        }

        private void startRefreshing()
        {
            if (DataThread.Enabled == false)
            {
                DataThread.Enabled = true;
                UInterface.Start();
            }
            else
            {
                DataThread.Enabled = false;
                UInterface.Stop();
            }
        }

        private void Refresh(object sender, EventArgs e)
        {           

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
                if (!s.Sold && !s.Reservation)
                {
                    var bt = ButtonList.Find(x => Convert.ToInt32(x.Content) == s.Id);
                    bt.Background = Brushes.WhiteSmoke;
                }
            }
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

        private void initializeHall()
        {
             initFirstHallAsync(8, 8);
        }

        private void initFirstHallAsync(int x,int y)
        {
            Button pole = new Button();
            ButtonList = new List<Button>();
            FirstGrid.RowDefinitions.Clear();
            FirstGrid.ColumnDefinitions.Clear();
            FirstGrid.Children.Clear();

            seatsList = sclient.GetSeats();

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
       
    }
}
