using HotelApp.Interface;
using HotelApp.Interface.Windows;
using HotelApp.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
            if (sender is Button reserveButton)
            {
                if (reserveButton.Tag is string roomNumber)
                {
                    var roomDto = _roomService.GetRoomByNumber(roomNumber);

                    var reserveWindow = new ReserveWindow(roomDto);

                    reserveWindow.ShowDialog();

                    if (reserveWindow.DialogResult == true)
                    {
                        _roomService.UpdateRoomStatus(roomDto, false);
                        LoadRooms("Room Number");
                    }
                }
            }
        }

        private void LoadRooms(string criteria)
        {
            _roomViewModels.Clear();

            foreach (var roomDto in _roomService.GetAvailableRoomsByCriteria(criteria))
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
