using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;

namespace Day3
{
    public static class SantasFurryFriends
    {
        [FunctionName("Day3")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Table("SecretSanta", Connection = "SecretSantaStorage")] IAsyncCollector<SantasFurryFriend> writeToTable)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var gitHubPush = JsonConvert.DeserializeObject<GitHubPush>(requestBody);

            foreach (var commit in gitHubPush.Commits)
            {
                foreach (var file in commit.Added)
                {
                    if (file.ToLower().EndsWith(".png"))
                    {
                        await writeToTable.AddAsync(new SantasFurryFriend
                        {
                            PartitionKey = "FurryFriends",
                            RowKey = Guid.NewGuid().ToString(),
                            Image = $"{gitHubPush.Repository.HtmlUrl}/{file}"
                        });
                    }
                }
            }
        }
    }

    public class SantasFurryFriend : TableEntity
    {
        public string Image { get; set; }
    }

    public class GitHubPush
    {
        public Repository Repository { get; set; }
        public IEnumerable<Commit> Commits { get; set; }
    }

    public class Repository
    {
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }

    public class Commit
    {
        public IEnumerable<string> Added { get; set; }
    }
}
