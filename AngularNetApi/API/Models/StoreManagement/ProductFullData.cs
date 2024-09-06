namespace AngularNetApi.API.Models.StoreManagement
{
    public class ProductFullData
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Description { get; set; }
        public string? ProductImg { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<IngredientData> Ingredients { get; set; }
    }
}
