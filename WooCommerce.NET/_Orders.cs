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

        public async Task<Order> Create(Order order)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_wooCommerce.host}/orders?consumer_key={_wooCommerce.key}&consumer_secret={_wooCommerce.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(order, new JsonSerializerOptions(){ WriteIndented = false }))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                },
            };
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(_wooCommerce.userAgent));
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<Order>(await response.Content.ReadAsStringAsync());
                
                else
                    Debug.WriteLine($"Failed creating order on WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n\n{await response.Content.ReadAsStringAsync()}");
                
            }
            return null;
        }
    }
}