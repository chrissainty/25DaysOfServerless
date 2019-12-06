using Microsoft.WindowsAzure.Storage.Table;

namespace PotLuck.Api.Entities
{
    public class FoodDish : TableEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
