using CarService3.BL.Interfaces;
using CarService3.BL.Services;
using CarService3.DL.Interfaces;
using CarService3.Models.Entities;
using Moq;

namespace CarService3.Tests
{
    public class SellCarTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<ICarRepository> _carRepositoryMock;

        public SellCarTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _customerServiceMock = new Mock<ICustomerService>();    
        }

        [Fact]
        public void SellCar_ApplyDiscount_Ok()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            _carRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => new Car
            {
                BasePrice = 20000,
                Id = carId,
                Model = "Model S",
                Year = 2020
            });

            _customerServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => new Customer
            {
                Id = carId,
                Discount = 1500,
            });

            // Act
            var sellCarService = new SellCarService(
                _customerServiceMock.Object,
                _carRepositoryMock.Object);

            var result = sellCarService.SellCar(customerId, carId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(18500, result.SalePrice);
        }

        [Fact]
        public void SellCar_ApplyDiscount_MissingCustomer()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            _carRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => null);

            _customerServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => new Customer
            {
                Id = carId,
                Discount = 1500,
            });

            // Act
            var sellCarService = new SellCarService(
                _customerServiceMock.Object,
                _carRepositoryMock.Object);

            var result = sellCarService.SellCar(customerId, carId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SellCar_ApplyDiscount_MissingCar()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            _carRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => new Car
            {
                BasePrice = 20000,
                Id = carId,
                Model = "Model S",
                Year = 2020
            });

            _customerServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => null);

            // Act
            var sellCarService = new SellCarService(
                _customerServiceMock.Object,
                _carRepositoryMock.Object);

            var result = sellCarService.SellCar(customerId, carId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void SellCar_ApplyNegativeDiscount()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            _carRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => new Car
            {
                BasePrice = 20000,
                Id = carId,
                Model = "Model S",
                Year = 2020
            });

            _customerServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => new Customer
            {
                Id = carId,
                Discount = 150000,
            });

            // Act
            var sellCarService = new SellCarService(
                _customerServiceMock.Object,
                _carRepositoryMock.Object);

            var result = sellCarService.SellCar(customerId, carId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.SalePrice);
        }
    }
}
