namespace WooCommerce.NET.Models
{
    public enum CustomerRole
    {
        All = 0,
        Administrator = 1,
        Editor = 2, 
        Author = 3, 
        Contributor = 4, 
        Subscriber = 5, 
        Customer = 6, 
        ShopManager = 7
    }
    
    public static class CustomerRoleMapper
    {
        private static readonly string[] options = new string[]
        {
            "all", 
            "administrator", 
            "editor", 
            "author", 
            "contributor", 
            "subscriber", 
            "customer", 
            "shop_manager"
        };
        
        public static string GetValue(CustomerRole customerRole) => options[(int)customerRole];
    }
}