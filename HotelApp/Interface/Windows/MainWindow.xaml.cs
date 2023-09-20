using HotelApp.Interface;
using HotelApp.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<RoomViewModel> RoomViewModels { get; set; } = new ObservableCollection<RoomViewModel>();

        private ReservationService _reservationService;

        public MainWindow()
        {
            InitializeComponent();
            _reservationService = new ReservationService();

            roomListView.ItemsSource = RoomViewModels;

            var rooms = _reservationService.GetRooms(); 
            foreach (var roomDto in rooms)
            {
                RoomViewModels.Add(new RoomViewModel(roomDto));
            }
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string RoomNumber)
            {
                // _reservationService.ReserveRoom(RoomNumber); 
            }
        }
    }

}
