using AutoMapper;
using HotelApp.HotelDtos;
using HotelApp.Providers;
using System.Collections.Generic;

namespace HotelApp.Services
{
    public class CustomerService
    {
        private CustomerProvider _provider;
        private IMapper _mapper;

        public CustomerService()
        {
            CreateProvider();
            CreateMapper();
        }

        public List<CustomerDto> GetCustomers()
        {
            var customers = _provider.GetCustomers();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
            return customerDtos;
        }

        public void AddCustomer(CustomerDto customerDto)
        {
            var customerEntity = _mapper.Map<DAL.Entities.Customer>(customerDto);

            _provider.AddCustomer(customerEntity);
        }

        public string? ValidateCustomerDto(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return "Customer data is missing.";
            }

            if (string.IsNullOrWhiteSpace(customerDto.Name) ||
                string.IsNullOrWhiteSpace(customerDto.LastName) ||
                string.IsNullOrWhiteSpace(customerDto.Email) ||
                string.IsNullOrWhiteSpace(customerDto.PhoneNumber))
            {
                return "Please fill in all required fields (Name, LastName, Email, Phone).";
            }

            if (!IsValidEmail(customerDto.Email))
            {
                return "Invalid email format.";
            }

            return null;
        }

        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        private void CreateProvider()
        {
            var context = new DAL.HotelContext();
            var repository = new DAL.Repositories.Repository<DAL.Entities.Customer>(context);
            _provider = new CustomerProvider(repository);
        }
        private void CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DAL.Entities.Room, RoomDto>();
            });

            _mapper = config.CreateMapper();
        }
    }
}
