using MongoDB.Bson.Serialization.Attributes;

namespace LoaManagerApi.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public int Type { get; set; }
        public string Key { get; set; } = null!;
    }
}
