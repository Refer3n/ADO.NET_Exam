using HotelApp.HotelDtos;
using HotelApp.Services;
using System.Windows;
using System.Windows.Controls;

namespace HotelApp.Interface.Windows.RoomManagmentWindows
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        private readonly RoomService _roomService;

        public AddRoomWindow(RoomService roomService)
        {
            InitializeComponent();
            _roomService = roomService;
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            string roomNumber = RoomNumberTextBox.Text.Trim();
            string roomClass = (string)((ComboBoxItem)RoomClassComboBox.SelectedItem)?.Content;
            decimal pricePerNight;

            if (!decimal.TryParse(PriceTextBox.Text, out pricePerNight))
            {
                MessageBox.Show("Invalid Price Per Night. Please enter a valid number.");
                return;
            }

            string description = DescriptionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(roomNumber) || string.IsNullOrWhiteSpace(roomClass))
            {
                MessageBox.Show("Room Number and Room Class are required fields.");
                return;
            }

            var roomDto = new RoomDto
            {
                RoomNumber = roomNumber,
                Class = roomClass,
                PricePerNight = pricePerNight,
                Description = description,
                Status = true
            };

            _roomService.AddRoom(roomDto);

            MessageBox.Show("Room added successfully.");
            Close();
        }
    }
}

