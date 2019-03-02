namespace Taxi.Data.Models
{
    public class Driver : Person
    {
        public Driver(string firstname, string lastname) : base(firstname, lastname)
        {
        }

        public string CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
