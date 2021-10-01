namespace WooCommerce.NET
{
    public class WooCommerce
    {
        internal string host { get; set; }
        internal string key { get; set; }
        internal string secret { get; set; }
        
        public string userAgent { get; set; }

        public WooCommerce(string host, string key, string secret)
        {
            this.host = host;
            this.key = key;
            this.secret = secret;

            Orders = new _Orders(this);
        }

        public _Orders Orders;
    }
}