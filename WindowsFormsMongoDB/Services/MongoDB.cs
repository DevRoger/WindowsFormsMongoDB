using MongoDB.Driver;

namespace WindowsFormsMongoDB.Services
{
    public static class MongoDB
    {
        public static MongoClient client = new MongoClient("mongodb://localhost:27017");
        public static IMongoDatabase db = client.GetDatabase("turismo");
    }
}
