using MongoDB.Bson;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        public ObjectId Id { get; private set; } // Database MongoDB type ObjectId
        public Customer Customer { get; private set; } // Customer of the sale
        public List<Product> Products { get; private set; } // List of Products for sale
        public DateTime SaleDate { get; private set; } // Sale date
        public decimal TotalPrice { get; private set; } // Total sale price


        // Builder with parameters (customer, list of Products and Total price)
        public Sale(Customer customer, List<Product> products, decimal totalPrice)
        {
            Id = new ObjectId();  // A new Guid format Id
            Customer = customer;
            Products = products;
            SaleDate = DateTime.UtcNow;
            TotalPrice = totalPrice;
        }

    }
}
