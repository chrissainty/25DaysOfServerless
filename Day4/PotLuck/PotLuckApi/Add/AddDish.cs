using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using PotLuck.Api.Entities;

namespace PotLuckApi
{
    public static class AddDish
    {
        [FunctionName("AddDish")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "potluck/fooddishes")] HttpRequest req,
            [Table("PotLuck", Connection = "PotLuckStorage")] IAsyncCollector<FoodDish> foodDishTable)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var newFoodDish = JsonConvert.DeserializeObject<FoodDish>(body);
            
            newFoodDish.PartitionKey = "PotLuck";
            newFoodDish.RowKey = Guid.NewGuid().ToString();

            await foodDishTable.AddAsync(newFoodDish);

            return new OkResult();
        }
    }
}
