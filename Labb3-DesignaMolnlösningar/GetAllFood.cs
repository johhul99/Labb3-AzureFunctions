using Labb3_DesignaMolnlösningar.Data;
using Labb3_DesignaMolnlösningar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Labb3_DesignaMolnlösningar
{
    public class GetAllFood
    {
        private readonly ILogger<GetAllFood> _logger;
        private readonly IConfiguration _configuration;

        public GetAllFood(ILogger<GetAllFood> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Function("GetAllFood")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "foods")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            FoodData db = new FoodData("Food", _configuration);

            List<Food> foodList = await db.GetAllFood();

            return new OkObjectResult(foodList);
        }
    }
}
