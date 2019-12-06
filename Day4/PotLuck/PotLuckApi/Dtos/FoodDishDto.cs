using Microsoft.WindowsAzure.Storage.Table;

namespace PotLuck.Api.Dtos
{
    public class FoodDishDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
