namespace WooCommerce.NET
{
    public enum OrderOrderBy
    {
        Date = 0, 
        Id = 1, 
        Include = 2, 
        Title = 2, 
        Slug = 3, 
        Modified = 4
    }
    
    public static class OrderOrderByMapper
    {
        private static readonly string[] options = new string[]
        {
            "date", 
            "id",
            "include",
            "title",
            "slug",
            "modified"
        };
        
        public static string GetValue(OrderOrderBy orderStatus) => options[(int)orderStatus];
    }
}