using System.Collections.ObjectModel;

namespace marketplace.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public User User { get; set; } = null;

        public ICollection<Order> Orders { get; set; } = null;
    }
}
