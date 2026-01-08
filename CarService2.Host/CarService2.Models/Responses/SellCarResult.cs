using CarService3.Models.Entities;

namespace CarService3.Models.Responses
{
    public class SellCarResult
    {
        public Customer Customer { get; set; }

        public Car Car { get; set; }

        public decimal SalePrice { get; set; }
    }
}
