using HotelApp.HotelDtos;
using HotelApp.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HotelApp.Interface.Windows
{
    /// <summary>
    /// Interaction logic for ReserveWindow.xaml
    /// </summary>
    public partial class ReserveWindow : Window
    {
        private CustomerService _customerService;
        private ReservationService _reservationService;

        private RoomDto _room;
        private CustomerDto? _customer;
        public ReserveWindow(RoomDto room)
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _reservationService = new ReservationService();
            _room = room;
            FillCustomerComboBox();
        }

        private void FillCustomerComboBox()
        {
            var customers = _customerService.GetCustomers();

            customerComboBox.Items.Clear();

            foreach (var customer in customers)
            {
                customerComboBox.Items.Add($"{customer.Name} {customer.LastName}");
            }
        }
        private CustomerDto CreateCustomerDto()
        {
            CustomerDto customerDto = new CustomerDto
            {
                Name = nameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Email = emailTextBox.Text,
                PhoneNumber = phoneTextBox.Text
            };

            return customerDto;
        }

        private ReservationDto CreateReservationDto()
        {
            DateTime? checkInDate = checkInDatePicker.SelectedDate;
            DateTime? checkOutDate = checkOutDatePicker.SelectedDate;

            if (checkInDate.HasValue && checkOutDate.HasValue)
            {
                var reservationDto = new ReservationDto
                {
                    StartDate = checkInDate.Value,
                    EndDate = checkOutDate.Value,
                    RoomId = _room.Id,
                    CustomerId = _customer.Id,
                    Price = _room.PricePerNight * (decimal)(checkOutDate.Value - checkInDate.Value).TotalDays,
                    Status = "Reserved"
                };

                return reservationDto;
            }

            return null;
        }

        private void ToggleControlsVisibility(bool isVisible)
        {
            customerComboBox.Visibility = isVisible ? Visibility.Hidden : Visibility.Visible;

            List<UIElement> elementsToToggle = new List<UIElement>
            {
                nameTextBox, nameTextBlock,
                lastNameTextBox, lastNameTextBlock,
                emailTextBox, emailTextBlock,
                phoneTextBox, phoneTextBlock,
                addCustomerButton
            };

            foreach (var element in elementsToToggle)
            {
                if (element != null)
                    element.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void existingCustomerRadio_Checked(object sender, RoutedEventArgs e)
        {
            ToggleControlsVisibility(false);
        }
        private void newCustomerRadio_Checked(object sender, RoutedEventArgs e)
        {
            ToggleControlsVisibility(true);
        }

        private void phoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||
               (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || e.Key == Key.Back))
            {
                e.Handled = true;
            }
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customerDto = CreateCustomerDto();

            string? validationMessage = _customerService.ValidateCustomerDto(customerDto);

            if (validationMessage != null)
            {
                MessageBox.Show(validationMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _customerService.AddCustomer(customerDto);

            ClearInputFields();

            AddToCustomerComboBox(customerDto);

            MessageBox.Show("Customer added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearInputFields()
        {
            nameTextBox.Clear();
            lastNameTextBox.Clear();
            emailTextBox.Clear();
            phoneTextBox.Clear();
        }

        private bool IsDateRangeValid(DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkOutDate <= checkInDate)
            {
                MessageBox.Show("Check-out date must be later than check-in date.", "Date Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (checkInDate < DateTime.Today)
            {
                MessageBox.Show("Check-in date cannot be in the past.", "Date Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool IsCustomerValid()
        {
            var selectedItem = customerComboBox.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Please select a customer or add a new one.", "Customer Validation Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            var customer = selectedItem.ToString();

            _customer = _customerService.GetCustomerByFullName(customer);

            return true;
        }

        private void reserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(checkInDatePicker.SelectedDate.ToString(), out DateTime checkInDate)
                && DateTime.TryParse(checkOutDatePicker.SelectedDate.ToString(), out DateTime checkOutDate))
            {
                if (IsDateRangeValid(checkInDate, checkOutDate) && IsCustomerValid())
                {
                    var reservationDto = CreateReservationDto();
                    _reservationService.AddReservation(reservationDto);

                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid date format. Please select valid dates.", "Date Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddToCustomerComboBox(CustomerDto customer)
        {
            string addData = $"{customer.Name} {customer.LastName}";
            customerComboBox.Items.Add(addData);
            customerComboBox.SelectedItem = addData;
            existingCustomerRadio.IsChecked = true;
        }
    }
}
