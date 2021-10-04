namespace WooCommerce.NET
{
    public enum OrderStatus
    {
        Pending = 0, 
        Processing = 1,
        OnHold = 2,
        Completed = 3, 
        Cancelled = 4,
        Refunded = 5, 
        Failed = 6, 
        Trash = 7,
        Any = 8
    }
    
    public static class OrderStatusMapper
    {
        private static readonly string[] options = new string[]
        {
            "pending", 
            "processing",
            "on-hold",
            "completed", 
            "cancelled",
            "refunded", 
            "failed", 
            "trash",
            "any"
        };
        
        public static string GetValue(OrderStatus orderStatus) => options[(int)orderStatus];
    }
}