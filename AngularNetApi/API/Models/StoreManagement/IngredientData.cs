namespace AngularNetApi.API.Models.StoreManagement
{
    public class IngredientData
    {
        public int IngredientId { get; set; }
        public int StoreId { get; set; }
        public string IngredientName { get; set; }
        public decimal IngredientPrice { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
