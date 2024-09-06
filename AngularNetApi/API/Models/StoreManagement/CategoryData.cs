using System.ComponentModel.DataAnnotations;

namespace AngularNetApi.API.Models.StoreManagement
{
    public class CategoryData
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
