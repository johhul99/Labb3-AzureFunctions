using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_DesignaMolnlösningar.Models
{
    public class Food
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string PrimaryMacronutrient { get; set; }
        public decimal CaloriesPer100g { get; set; }
        public decimal ProteinPer100g { get; set; }
        public decimal CarbohydratesPer100g { get; set; }
    }
}
