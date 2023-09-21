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
        private ObservableCollection<RoomViewModel> _roomViewModels;

        private RoomService _roomService;

        public MainWindow()
        {
            InitializeComponent();
            _roomService = new RoomService();

            _roomViewModels = new ObservableCollection<RoomViewModel>();

            roomListView.ItemsSource = _roomViewModels;

            LoadRooms("Room Number");
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LoadRooms(string criteria)
        {
            _roomViewModels.Clear(); 

            foreach (var roomDto in _roomService.GetRoomsByCriteria(criteria))
            {
                _roomViewModels.Add(new RoomViewModel(roomDto));
            }
        }


        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked != null)
            {
                string columnHeader = headerClicked.Column.Header.ToString();

                int lastColonIndex = columnHeader.LastIndexOf(':');
                if (lastColonIndex >= 0)
                {
                    string sortingCriteria = columnHeader.Substring(lastColonIndex + 1).Trim();

                    LoadRooms(sortingCriteria);
                }
            }
        }

    }

}
