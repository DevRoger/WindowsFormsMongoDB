using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WindowsFormsMongoDB.Models
{
    public class Ciudad
    {
        [BsonId]
        public ObjectId id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

    }
}
