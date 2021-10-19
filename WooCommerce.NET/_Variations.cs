using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WooCommerce.NET.Convertors;
using WooCommerce.NET.Models;

namespace WooCommerce.NET
{
    public class _Variations : WooCommerceSection
    {
        private WCObject WcObject { get; set; }

        public _Variations(WCObject wcObject)
        {
            this.WcObject = wcObject;
        }
        
        public async Task<List<Variation>> FetchAll(Product parent)
        {
            return parent._variations ?? (parent._variations = await FetchAll(parent.id));
        }
        
        public async Task<List<Variation>> FetchAll(int parentId)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products/{parentId}/variations?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"), 
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<List<Variation>>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                
                Console.WriteLine($"Failed fetching a products variations from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }

            return null;
        }
        
        public async Task<Variation> Fetch(int parentId, int variationId)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products/{parentId}/variations/{variationId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"), 
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<Variation>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                
                Console.WriteLine($"Failed fetching a products variations from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }
            return null;
        }
    }
}