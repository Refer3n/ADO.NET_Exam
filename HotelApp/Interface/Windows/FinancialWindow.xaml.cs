using HotelApp.Services;
using System;
using System.Windows;

namespace HotelApp.Interface.Windows
{
    /// <summary>
    /// Interaction logic for FinancialWindow.xaml
    /// </summary>
    public partial class FinancialWindow : Window
    {
        private ReservationService _reservationService;
        public FinancialWindow()
        {
            InitializeComponent();
            _reservationService = new ReservationService();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = startDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = endDatePicker.SelectedDate ?? DateTime.MaxValue;

            if (endDate < startDate)
            {
                MessageBox.Show("End date cannot be before the start date. Please select valid dates.", "Date Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (endDate == DateTime.MaxValue || startDate == DateTime.MinValue)
            {
                MessageBox.Show("Start or end date was not selected!", "Date Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string report = GenerateFinancialReport(startDate, endDate);

            financialReportTextBox.Text = report;
        }

        private string GenerateFinancialReport(DateTime startDate, DateTime endDate)
        {
            string financialReport = _reservationService.GetStatistics(startDate, endDate);

            return financialReport;
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            financialReportTextBox.Clear();
        }
    }
}
