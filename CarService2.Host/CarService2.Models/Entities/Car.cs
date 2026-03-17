using MessagePack;

namespace CarService3.Models.Entities
{
    [MessagePackObject]
    public class Car
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public string Model { get; set; }
        [Key(2)]
        public int Year { get; set; }
        [Key(3)]
        public decimal BasePrice { get; set; }
    }
}
