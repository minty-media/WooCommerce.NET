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

        public async Task<List<Variation>> FetchAll(Product parent, bool forceFetch = false)
        {
            if (forceFetch)
                parent._variations = await FetchAll(parent.id);
            
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
                    return (JsonSerializer.Deserialize<List<Variation>>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions()) ?? new List<Variation>()).Select(
                        x =>
                        {
                            x.parent = parentId;
                            return x;
                        }).ToList();
                
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
                {
                    Variation variation = JsonSerializer.Deserialize<Variation>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());

                    if (variation != null)
                        variation.parent = parentId;
                    
                    return variation;
                }
                
                Console.WriteLine($"Failed fetching a products variations from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }
            return null;
        }

        public async Task<bool> Update(Variation variation, int parentId)
        {
            
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products/{parentId}/variations/{variation.id}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(variation, this.GetJsonSerializerOptions()))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            
            using var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;         
        }
    }
}