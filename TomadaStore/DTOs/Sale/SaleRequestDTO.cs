using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleRequestDTO
    {
        [BsonElement("saleDate")]
        public DateTime SaleDate { get; init; }
        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; init; }
    }
}
