namespace marketplace.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFullfilled { get; set; }
        public Customer Customer { get; set; } = null;
        public int CustomerId { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = null;

    }
}
