using HotelApp.Interface;
using HotelApp.Interface.Windows;
using HotelApp.Interface.Windows.RoomManagmentWindows;
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
        private ReservationService _reservationService;

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
                        roomDto.Status = false;
                        _roomService.UpdateRoom(roomDto);
                        LoadRooms("Room Number");
                    }
                }
            }
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddRoomWindow(_roomService);
            addWindow.ShowDialog();
            LoadRooms("Room Number");
        }

        private void FinancialReportButton_Click(object sender, RoutedEventArgs e)
        {
            var financialWindow = new FinancialWindow();
            financialWindow.ShowDialog();
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.Tag is string roomNumber)
                {
                    var roomDto = _roomService.GetRoomByNumber(roomNumber);

                    var updateWindow = new UpdateRoomWindow(_roomService, roomDto);
                    updateWindow.ShowDialog();
                    LoadRooms("Room Number");
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                if (deleteButton.Tag is string roomNumber)
                {
                    MessageBoxResult result = MessageBox.Show($"Do you want to delete room {roomNumber}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        var roomDto = _roomService.GetRoomByNumber(roomNumber);

                        _roomService.RemoveRoom(roomDto);
                        LoadRooms("Room Number");
                    }
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //DateTime? checkInDate = startDatePicker.SelectedDate;
            //DateTime? checkOutDate = endDatePicker.SelectedDate;

            //if (checkInDate != null && checkOutDate != null)
            //{
            //    if (checkOutDate <= checkInDate)
            //    {
            //        MessageBox.Show("Check-out date must be later than check-in date.", "Date Validation Error",
            //            MessageBoxButton.OK, MessageBoxImage.Error);
            //        startDatePicker.SelectedDate = null;
            //        endDatePicker.SelectedDate = null;
            //    }
            //    else if (checkInDate < DateTime.Today)
            //    {
            //        MessageBox.Show("Check-in date cannot be in the past.", "Date Validation Error",
            //            MessageBoxButton.OK, MessageBoxImage.Error);
            //        startDatePicker.SelectedDate = null;
            //        endDatePicker.SelectedDate = null;
            //    }
            //    else
            //    {
            //        _roomViewModels.Clear();

            //        var roomsDto = _reservationService.GetUnoccupiedRoomsInDateRange((DateTime)checkInDate, (DateTime)checkOutDate);

            //        foreach (var roomDto in roomsDto)
            //        {
            //            _roomViewModels.Add(new RoomViewModel(roomDto));
            //        }
            //    }
            //}
        }
    }
}
