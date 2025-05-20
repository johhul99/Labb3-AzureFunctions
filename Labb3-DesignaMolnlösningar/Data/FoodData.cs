using Labb3_DesignaMolnlösningar.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_DesignaMolnlösningar.Data
{
    public class FoodData
    {
        private IMongoDatabase db;

        public FoodData(string database, IConfiguration configuration)
        {
            var connectionString = configuration["DefaultConnection"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Missing 'DefaultConnection' in configuration.");
            }
            var client = new MongoClient(connectionString);
            db = client.GetDatabase(database);
        }

        public async Task<List<Food>> GetAllFood()
        {
            var collection = db.GetCollection<Food>("Food");
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Food> GetFoodById(string id)
        {
            var collection = db.GetCollection<Food>("Food");
            BsonDocument filter = new BsonDocument("_id", new ObjectId(id));
            var food = await collection.Find(filter).FirstOrDefaultAsync();
            if(food == null)
            {
                Console.WriteLine("No food found");
                return null;
            }
            return food;
        }

        public async Task<Food> PostFood(Food food)
        {
            var collection = db.GetCollection<Food>("Food");
            await collection.InsertOneAsync(food);
            return food;
        }

        public async Task<Food?> UpdateFood(string id, Food food)
        {
            var collection = db.GetCollection<Food>("Food");

            BsonDocument filter = new BsonDocument("_id", new ObjectId(id));

            var existingFood = await collection.Find(filter).FirstOrDefaultAsync();
            if(existingFood == null)
            {
                return null;
            }
            var result = await collection.ReplaceOneAsync(filter, food);
            return result.IsAcknowledged ? food : null;
        }

        public async Task<bool> DeleteFood(string id)
        {
            var collection = db.GetCollection<Food>("Food");

            if (!ObjectId.TryParse(id, out var objectId))
            {
                return false;
            }

            var filter = Builders<Food>.Filter.Eq("_id", objectId);
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
