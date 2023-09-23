using HotelApp.DAL.Entities;
using HotelApp.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HotelApp.Providers
{
    public class CustomerProvider
    {
        private readonly IRepository<Customer> _repository;

        public CustomerProvider(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public void AddCustomers(List<Customer> customers)
        {
            customers.ForEach(customer => AddCustomer(customer));
        }

        public void AddCustomer(Customer customer)
        {
            _repository.Add(customer);
        }

        public void RemoveCustomers(List<Customer> customers)
        {
            customers.ForEach(customer => RemoveCustomer(customer));
        }

        public void RemoveCustomer(Customer customer)
        {
            _repository.Remove(customer);
        }

        public void RemoveCustomer(int Id)
        {
            var customer = GetCustomer(Id);
            _repository.Remove(customer);
        }

        public Customer GetCustomer(int Id)
        {
            return _repository.Get(Id);
        }

        public Customer GetCustomerByName(string name, string lastName)
        {
            return _repository.GetAll()
                .FirstOrDefault(customer =>
                    customer.Name == name && customer.LastName == lastName);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _repository.GetAll();
        }
    }
}