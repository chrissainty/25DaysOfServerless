using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using PotLuck.Api.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace PotLuck.Api.List
{
    public static class ListDish
    {
        [FunctionName("ListDish")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "potluck/fooddishes")] HttpRequest req,
            [Table("PotLuck")] CloudTable foodDishTable)
        {
            var getAll = new TableQuery<FoodDish>();
            var foodDishes = await foodDishTable.ExecuteQuerySegmentedAsync(getAll, null);

            return new OkObjectResult(foodDishes);
        }
    }
}
