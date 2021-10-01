namespace WooCommerce.NET
{
    public enum SortDirection
    {
        Ascending = 0, 
        Descending = 1
    }
    
    public static class SortDirectionMapper
    {
        private static readonly string[] options = new string[]
        {
            "asc", 
            "desc"
        };
        
        public static string GetValue(SortDirection orderStatus) => options[(int)orderStatus];
    }
}