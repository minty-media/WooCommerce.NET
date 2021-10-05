namespace WooCommerce.NET.Models
{
    public enum ProductType
    {
        Simple = 0,
        Grouped = 1,
        External = 2,
        Variable = 3
    }
    
    public static class ProductTypeMapper
    {
        private static readonly string[] options = new string[]
        {
            "simple", 
            "grouped", 
            "external", 
            "variable"
        };
        
        public static string GetValue(ProductType productType) => options[(int)productType];
    }
}