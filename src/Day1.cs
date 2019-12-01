using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace TwentyFiveDaysOfServerless
{
    public static class Day1
    {
        [FunctionName("Day1")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            var returnOptions = new string[] { "Nun", "Gimmel", "Hay", "Shin" };
            var rand = new Random();
            var index = rand.Next(returnOptions.Length);

            return new OkObjectResult($"{returnOptions[index]}");
        }
    }
}
