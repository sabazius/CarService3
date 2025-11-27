using CarService3.DL.Interfaces;
using CarService3.Models.Entities;

namespace CarService3.DL.Repositories
{
    internal class CustomerStaticRepository : ICustomerRepository
    {
        public void Add(Customer? customer)
        {
            if (customer == null) return;

            MyStaticDb.StaticDb.Customers.Add(customer);
        }

        public List<Customer> GetAll()
        {
            return MyStaticDb.StaticDb.Customers;
        }

        public Customer? GetById(int id)
        {
            if (id <= 0) return null;

            return MyStaticDb.StaticDb
                .Customers
                .FirstOrDefault(c => c.Id == id);
        }

        public void Delete(int id)
        {
            if (id <= 0) return;

            var customer = GetById(id);

            if (customer != null)
            {
                MyStaticDb.StaticDb.Customers.Remove(customer);
            }
        }
    }
}
