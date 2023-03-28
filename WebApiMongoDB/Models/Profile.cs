using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongoDB.Models
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Server { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Level { get; set; }
    }
}
