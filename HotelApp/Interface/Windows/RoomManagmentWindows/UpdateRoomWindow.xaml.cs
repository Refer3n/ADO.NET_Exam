using HotelApp.HotelDtos;
using HotelApp.Services;
using System.Windows;
using System.Windows.Controls;

namespace HotelApp.Interface.Windows.RoomManagmentWindows
{
    /// <summary>
    /// Interaction logic for UpdateRoomWindow.xaml
    /// </summary>
    public partial class UpdateRoomWindow : Window
    {
        private readonly RoomService _roomService;
        private RoomDto _roomDto;
        public UpdateRoomWindow(RoomService roomService, RoomDto roomDto)
        {
            InitializeComponent();
            _roomService = roomService;
            _roomDto = roomDto;
            SetRoomData(_roomDto);
        }

        private void SetRoomData(RoomDto roomDto)
        {
            RoomNumberTextBox.Text = roomDto.RoomNumber;
            foreach (ComboBoxItem item in RoomClassComboBox.Items)
            {
                if ((string)item.Content == roomDto.Class)
                {
                    RoomClassComboBox.SelectedItem = item;
                    break;
                }
            }
            PriceTextBox.Text = roomDto.PricePerNight.ToString();
            DescriptionTextBox.Text = roomDto.Description;
        }

        private void UpdateRoomButton_Click(object sender, RoutedEventArgs e)
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

            _roomDto.RoomNumber = roomNumber;
            _roomDto.Class = roomClass;
            _roomDto.PricePerNight = pricePerNight;
            _roomDto.Description = description;

            _roomService.UpdateRoom(_roomDto);

            MessageBox.Show("Room Updated successfully.");
            Close();
        }
    }
}
