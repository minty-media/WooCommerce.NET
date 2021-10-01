using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WooCommerce.NET
{
    public class _Orders
    {
        private WooCommerce _wooCommerce { get; set; }

        public _Orders(WooCommerce _wooCommerce)
        {
            this._wooCommerce = _wooCommerce;
        }

        public async Task<Order> Create(Order order, bool dumpJson = false)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_wooCommerce.host}/wp-json/wc/v3/orders?consumer_key={_wooCommerce.key}&consumer_secret={_wooCommerce.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(order, new JsonSerializerOptions(){ WriteIndented = false }))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            
            if (dumpJson)
                Console.WriteLine(JsonSerializer.Serialize(order, new JsonSerializerOptions(){ WriteIndented = true }));
            
            if (!string.IsNullOrEmpty(_wooCommerce.userAgent))
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(_wooCommerce.userAgent));
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<Order>(await response.Content.ReadAsStringAsync());
                
                Console.WriteLine($"Failed creating order on WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
                Console.WriteLine(" - Input towards WooCommerce: " + JsonSerializer.Serialize(order, new JsonSerializerOptions(){ WriteIndented = true }));
            }
            return null;
        }
    }
}