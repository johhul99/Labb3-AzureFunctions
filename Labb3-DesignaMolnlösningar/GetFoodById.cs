using Labb3_DesignaMolnlösningar.Data;
using Labb3_DesignaMolnlösningar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Labb3_DesignaMolnlösningar
{
    public class GetFoodById
    {
        private readonly ILogger<GetFoodById> _logger;
        private readonly IConfiguration _configuration;

        public GetFoodById(ILogger<GetFoodById> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

            [Function("GetFoodById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "food/{id}")] HttpRequest req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            FoodData db = new FoodData("Food", _configuration);
            var selectedFood = await db.GetFoodById(id);

            if (selectedFood == null)
            {
                return new NotFoundObjectResult($"Food item with ID '{id}' not found.");
            }

            return new OkObjectResult(selectedFood);
        }
    }
}
