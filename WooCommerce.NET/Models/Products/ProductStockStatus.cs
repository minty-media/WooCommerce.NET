namespace WooCommerce.NET.Models
{
    public enum ProductStockStatus
    {
        InStock = 0,
        OutOfStock = 1,
        BackOrder = 2
    }

    public class ProductStockStatusMapper
    {
        private static readonly string[] options = new string[]
        {
            "instock", 
            "outofstock",
            "onbackorder"
        };
        
        public static string GetValue(ProductStockStatus stockStatus) => options[(int)stockStatus];
    }
}