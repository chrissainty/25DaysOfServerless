using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using PotLuck.Api.Entities;

namespace PotLuck.Api.Get
{
    public static class GetDish
    {
        [FunctionName("GetDish")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "potluck/fooddishes/{id}")] HttpRequest req,
            [Table("PotLuck", "PotLuck", "{id}")] FoodDish foodDish)
        {
            if (foodDish == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(foodDish);
        }
    }
}
