using Labb3_DesignaMolnlösningar.Data;
using Labb3_DesignaMolnlösningar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Labb3_DesignaMolnlösningar
{
    public class UpdateFood
    {
        private readonly ILogger<UpdateFood> _logger;
        private readonly IConfiguration _configuration;

        public UpdateFood(ILogger<UpdateFood> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Function("UpdateFood")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "put", Route = "food/{id}")] HttpRequest req, string id, string name, string primaryMacronutrient, decimal caloriesPer100g, decimal proteinPer100g, decimal carbohydratesPer100g)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            FoodData db = new FoodData("Food", _configuration);

            Food updatedFood = new Food
            {
                Id = id,
                Name = name,
                PrimaryMacronutrient = primaryMacronutrient,
                CaloriesPer100g = caloriesPer100g,
                ProteinPer100g = proteinPer100g,
                CarbohydratesPer100g = carbohydratesPer100g
            };

            var result = await db.UpdateFood(id, updatedFood);

            if (result == null)
            {
                return new NotFoundObjectResult($"Food item with ID '{id}' was not found.");
            }

            return new OkObjectResult(updatedFood);
        }
    }
}
