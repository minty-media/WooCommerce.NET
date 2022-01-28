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
    public class _Products : WooCommerceSection
    {
        private WCObject WcObject { get; set; }

        public _Products(WCObject wcObject)
        {
            this.WcObject = wcObject;
        }
        
        /// <summary>
        /// Fetch a single product based on product id
        /// </summary>
        /// <param name="productId">Product id to search for</param>
        /// <returns>A product object</returns>
        public async Task<Product> Fetch(long productId)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products/{productId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"), 
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<Product>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                
                Console.WriteLine($"Failed fetching a product from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }

            return null;
        }
        
        /// <summary>
        /// Fetch a single product based on SKU
        /// </summary>
        /// <param name="productSku">Product SKU to search for</param>
        /// <returns>A product object</returns>
        public async Task<Product> Fetch(string productSku)
        {
            return (await MultiFetch(optionalParameters: new Dictionary<string, string>()
            {
                { "sku", productSku }
            })).SingleOrDefault();
        }
        
        
        
        /// <summary>
        /// Update a product, expects the product id in an empty product class with only the values that need changing.
        /// </summary>
        /// <param name="product">A product class with all values that need to be updated on WooCommercce</param>
        /// <returns>Success true/false</returns>
        public async Task<bool> Update(Product product)
        {
            Console.WriteLine(JsonSerializer.Serialize(product, this.GetJsonSerializerOptions()));

            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products/{product.id}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(product, this.GetJsonSerializerOptions()))
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

        /// <summary>
        /// Fetch multiple products from woocommerce
        /// </summary>
        /// <param name="page">The current page to fetch</param>
        /// <param name="offset">The offset of products to fetch</param>
        /// <param name="perPage">How many products to return per page</param>
        /// <param name="order">Sort products ascending or descending</param>
        /// <param name="optionalParameters">Any optional URL parameters</param>
        /// <returns>A list of products. (Returns null if it fails to retrieve anything, returns empty array if there were no results)</returns>
        public async Task<List<Product>> MultiFetch(
            int page = 1,
            int offset = 0,
            int perPage = 10,
            SortDirection order = SortDirection.Descending,
            Dictionary<string, string> optionalParameters = null)
        {
            if (page < 1)
                page = 1;

            if (perPage < 1)
                perPage = 10;
            
            string optionalParams = "";

            if (optionalParameters != null)
                optionalParams = string.Join("&", optionalParameters.Select(x => x.Key + "=" + x.Value).ToArray());

            string url = $"{WcObject.host}/wp-json/wc/v3/products?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}&{optionalParams}";

            url += $"&per_page={perPage}";
            
            if (offset != 0)
                url += $"&offset={offset}";
            
            if (page != 1)
                url += $"&page={page}";
            
            url += $"&order={SortDirectionMapper.GetValue(order)}";

            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url), 
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Product> products =
                        JsonSerializer.Deserialize<List<Product>>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());

                    return products ?? new List<Product>();
                }
                
                Console.WriteLine($"Failed fetching all products from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }
            return null;
        }

        /// <summary>
        /// Delete a product from WooCommerce
        /// </summary>
        /// <param name="productId">The product id to delete</param>
        /// <param name="reassign">User ID to reassign posts to</param>
        /// <returns>Success true/false</returns>
        public async Task<bool> Delete(long productId, long reassign = 0)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products/{productId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}&force=true" + (reassign != 0 ? $"&reassign={reassign}" : "")), 
            };
            
            using var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        
        /// <summary>
        /// Create new product on WooCommerce with supplied product object
        /// </summary>
        /// <param name="product">Product object to place on WooCommerce</param>
        /// <returns>Returns the actual product that's added to WooCommerce</returns>
        public async Task<Product> Create(Product product)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/products?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(product, this.GetJsonSerializerOptions()))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                Console.Write($"Create product: {response.StatusCode} ");

                if (response.IsSuccessStatusCode)
                {
                    Product c = JsonSerializer.Deserialize<Product>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                    Console.WriteLine($"{c.id}");
                    return c;
                }
                
                Console.WriteLine($"\nFailed creating product on WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
                Console.WriteLine(" - Input towards WooCommerce: " + JsonSerializer.Serialize(product, this.GetJsonSerializerOptions()));
            }
            return null;
        }
    }
}