using CarService3.DL.Interfaces;
using CarService3.Models.Entities;
using Microsoft.Extensions.Logging;

namespace CarService3.DL.Repositories
{
    internal class CustomerStaticRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerStaticRepository> _logger;

        public CustomerStaticRepository(ILogger<CustomerStaticRepository> logger)
        {
            _logger = logger;
        }

        public void Add(Customer? customer)
        {
            if (customer == null) return;

            MyStaticDb.StaticDb.Customers.Add(customer);
        }

        public List<Customer> GetAll()
        {
            try
            {
                return MyStaticDb.StaticDb.Customers;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}-{e.StackTrace}");
            }

            return new List<Customer>();
        }

        public Customer? GetById(Guid id)
        {
            if (id == Guid.Empty) return null;

            return MyStaticDb.StaticDb
                .Customers
                .FirstOrDefault(c => c.Id == id);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            var customer = GetById(id);

            if (customer != null)
            {
                MyStaticDb.StaticDb.Customers.Remove(customer);
            }
        }
    }
}
