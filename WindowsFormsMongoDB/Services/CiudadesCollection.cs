using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using WindowsFormsMongoDB.Models;

namespace WindowsFormsMongoDB.Services
{
    /// <summary>
    /// Clase estática que proporciona operaciones CRUD para la colección de Ciudades en MongoDB
    /// </summary>
    public static class CiudadesCollection
    {
        // Obtiene la referencia a la colección MongoDB para las ciudades
        private static IMongoCollection<Ciudad> ciudadesCollection = MongoDB.db.GetCollection<Ciudad>("ciudades");

        /// <summary>
        /// Obtiene todos los registros de ciudades de la colección
        /// </summary>
        /// <returns>Lista completa de ciudades</returns>
        public static List<Ciudad> GetAll()
        {
            // Busca todos los documentos sin filtros (condición true)
            List<Ciudad> ciudades = ciudadesCollection.Find(c => true).ToList();

            return ciudades;
        }

        /// <summary>
        /// Inserta una nueva ciudad en la colección
        /// </summary>
        /// <param name="ciudad">Objeto Ciudad a insertar</param>
        public static void Insert(Ciudad ciudad)
        {
            // Inserta un único documento en la colección
            ciudadesCollection.InsertOne(ciudad);
        }

        /// <summary>
        /// Actualiza una ciudad existente en la colección
        /// </summary>
        /// <param name="ciudad">Objeto Ciudad con los datos actualizados</param>
        public static void Update(Ciudad ciudad)
        {
            // Reemplaza el documento que coincide con el ID de la ciudad proporcionada
            ciudadesCollection.ReplaceOne(c => c.id == ciudad.id, ciudad);
        }

        /// <summary>
        /// Elimina una ciudad de la colección
        /// </summary>
        /// <param name="ciudad">Objeto Ciudad a eliminar</param>
        public static void Delete(Ciudad ciudad)
        {
            // Elimina el documento que coincide con el ID de la ciudad proporcionada
            ciudadesCollection.DeleteOne(c => c.id == ciudad.id);
        }
    }
}