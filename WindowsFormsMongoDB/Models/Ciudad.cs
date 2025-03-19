using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
