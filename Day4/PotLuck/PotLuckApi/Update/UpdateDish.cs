using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using PotLuck.Api.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.IO;
using Newtonsoft.Json;

namespace PotLuck.Api.Update
{
    public static class UpdateDish
    {
        [FunctionName("UpdateDish")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "potluck/fooddishes/{id}")] HttpRequest req,
            [Table("PotLuck", "PotLuck", "{id}")] FoodDish foodDish,
            [Table("PotLuck")] CloudTable foodDishTable)
        {
            if (foodDish == null)
            {
                return new NotFoundResult();
            }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var updatedFoodDish = JsonConvert.DeserializeObject<FoodDish>(requestBody);
            
            foodDish.Name = updatedFoodDish.Name;
            foodDish.Title = updatedFoodDish.Title;
            foodDish.Description = updatedFoodDish.Description;

            var updateFoodDish = TableOperation.Replace(foodDish);
            await foodDishTable.ExecuteAsync(updateFoodDish);

            return new OkObjectResult(foodDish);
        }
    }
}
