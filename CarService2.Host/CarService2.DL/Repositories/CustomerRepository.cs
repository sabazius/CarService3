using CarService3.DL.Interfaces;
using CarService3.Models.Configurations;
using CarService3.Models.Entities;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService3.DL.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customersCollection;
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<CustomerRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;
            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);

            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _customersCollection = database.GetCollection<Customer>($"{nameof(Customer)}s");
        }

        public async Task Add(Customer? customer)
        {
            if (customer == null) return;

            try
            {
                await _customersCollection.InsertOneAsync(customer);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(Add)}:{e.Message}-{e.StackTrace}");
            }

        }

        public async Task<List<Customer>> GetAll()
        {
            return await _customersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Customer?> GetById(Guid id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return await _customersCollection.Find(c => c.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;

        }

        public async Task Delete(Guid id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = 
                    await _customersCollection
                        .DeleteOneAsync(c => c.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No car found with Id: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(Delete)}:{e.Message}-{e.StackTrace}");
            }
        }
    }
}
