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
            this.key = key;
            this.secret = secret;
            this.host = host.EndsWith("/wp-json/wc/v3") ? host.Replace("/wp-json/wc/v3", "") : host;

            Orders = new _Orders(this);
        }

        public _Orders Orders;
    }
}