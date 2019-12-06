using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using PotLuck.Api.Entities;

namespace PotLuck.Api.Delete
{
    public static class DeleteDish
    {
        [FunctionName("DeleteDish")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "potluck/fooddishes/{id}")] HttpRequest req,
            [Table("PotLuck", "PotLuck", "{id}")] FoodDish foodDish,
            [Table("PotLuck")] CloudTable foodDishTable)
        {
            if (foodDish == null)
            {
                return new NotFoundResult();
            }

            var deleteFoodDish = TableOperation.Delete(foodDish);
            await foodDishTable.ExecuteAsync(deleteFoodDish);

            return new NoContentResult();
        }
    }
}
