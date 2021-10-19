namespace WooCommerce.NET.Models
{
    public enum ProductStatus
    {
        Draft = 0,
        Pending = 1,
        Private = 2,
        Published = 3
    }

    public class ProductStatusMapper
    {
        private static readonly string[] options = new string[]
        {
            "draft", 
            "pending", 
            "private",
            "publish"
        };
        
        public static string GetValue(ProductStatus status) => options[(int)status];
    }
}