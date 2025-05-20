using Labb3_DesignaMolnlösningar.Data;
using Labb3_DesignaMolnlösningar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Labb3_DesignaMolnlösningar
{
    public class PostFood
    {
        private readonly ILogger<PostFood> _logger;
        private readonly IConfiguration _configuration;

        public PostFood(ILogger<PostFood> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Function("PostFood")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,"post", Route = "food")] HttpRequest req, string name, string primaryMacronutrient, decimal caloriesPer100g, decimal proteinPer100g, decimal carbohydratesPer100g)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            FoodData db = new FoodData("Food", _configuration);
            Food newFood = new Food
            {
                Name = name,
                PrimaryMacronutrient = primaryMacronutrient,
                CaloriesPer100g = caloriesPer100g,
                ProteinPer100g = proteinPer100g,
                CarbohydratesPer100g = carbohydratesPer100g
            };

            await db.PostFood(newFood);

            return new OkObjectResult(newFood);
        }
    }
}
