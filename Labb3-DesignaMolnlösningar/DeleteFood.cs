using Labb3_DesignaMolnlösningar.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Labb3_DesignaMolnlösningar
{
    public class DeleteFood
    {
        private readonly ILogger<DeleteFood> _logger;
        private readonly IConfiguration _configuration;

        public DeleteFood(ILogger<DeleteFood> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Function("DeleteFood")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "food/{id}")] HttpRequest req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            FoodData db = new FoodData("Food", _configuration);
            var success = await db.DeleteFood(id);

            if (!success)
            {
                return new NotFoundObjectResult($"No food found with ID '{id}'.");
            }

            return new OkObjectResult($"Food with ID '{id}' was deleted.");
        }
    }
}
