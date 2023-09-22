using HotelApp.HotelDtos;
using HotelApp.Services;
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
        private ReservationService _roomService;

        private RoomDto _room;
        private CustomerDto? _customer;
        public ReserveWindow(RoomDto room)
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _roomService = new ReservationService();
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

        private void ToggleControlsVisibility(bool isVisible)
        {
            customerComboBox.Visibility = isVisible ? Visibility.Collapsed : Visibility.Visible;

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
                element.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
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
                return;
            }
            e.Handled = true;
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

        private void AddToCustomerComboBox(CustomerDto customer)
        {
            string addData = $"{customer.Name} {customer.LastName}";
            customerComboBox.Items.Add(addData);
            customerComboBox.SelectedItem = addData;
        }
    }
}
