using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WindowsFormsMongoDB.Services
{
    public static class MongoDB
    {
        public static MongoClient client = new MongoClient("mongodb://localhost:27017");
        public static IMongoDatabase db = client.GetDatabase("turismo");
    }
}
