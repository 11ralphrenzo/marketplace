using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace marketplace.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
    }
}
