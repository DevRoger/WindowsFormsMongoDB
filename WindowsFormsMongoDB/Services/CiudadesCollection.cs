using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using WindowsFormsMongoDB.Models;

namespace WindowsFormsMongoDB.Services
{
    public static class CiudadesCollection
    {
        private static IMongoCollection<Ciudad> ciudadesCollection = MongoDB.db.GetCollection<Ciudad>("ciudades");

        public static List<Ciudad> GetAll()
        {
            List<Ciudad> ciudades = ciudadesCollection.Find(c => true).ToList();

            return ciudades;
        }

        public static void Insert(Ciudad ciudad)
        {

            ciudadesCollection.InsertOne(ciudad);
        }
        public static void Update(Ciudad ciudad)
        {

            ciudadesCollection.ReplaceOne(c => c.id == ciudad.id, ciudad);

        }

        public static void Delete(Ciudad ciudad)
        {

            ciudadesCollection.DeleteOne(c => c.id == ciudad.id);
        }
    }
}
