namespace Eventures.Data.Models
{
    public class Ticket
    {
        public string Id { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public decimal RegularPrice { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
