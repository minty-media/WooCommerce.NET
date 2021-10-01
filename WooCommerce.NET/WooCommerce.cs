using System.Net.Http;

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
            Products = new _Products(this);
        }

        public HttpClient PrepareHttpClient()
        {
            HttpClient client = new HttpClient();
            
            if (!string.IsNullOrEmpty(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            return client;
        }
        
        public _Orders Orders;
        public _Products Products;
    }
}