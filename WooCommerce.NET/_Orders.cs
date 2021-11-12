using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WooCommerce.NET.Models;

namespace WooCommerce.NET
{
    public class _Orders : WooCommerceSection
    {
        private WCObject WcObject { get; set; }

        public _Orders(WCObject wcObject)
        {
            this.WcObject = wcObject;
        }

        /// <summary>
        /// Update an order, expects the order id in an empty order class with only the values that need changing.
        /// </summary>
        /// <param name="order">A order class with all values that need to be updated on WooCommercce</param>
        /// <returns>Success true/false</returns>
        public async Task<bool> Update(Order order)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/orders/{order.id}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(order, this.GetJsonSerializerOptions()))
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
        /// Delete an order from WooCommerce
        /// </summary>
        /// <param name="orderId">The order id to delete</param>
        /// <param name="forceDelete">Delete the order from WooCommerce without moving it to trash</param>
        /// <returns>Success true/false</returns>
        public async Task<bool> Delete(long orderId, bool forceDelete = false)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/orders/{orderId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}&force={forceDelete}"),
            };

            using var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Fetch multiple orders from woocommerce
        /// </summary>
        /// <param name="page">The current page to fetch</param>
        /// <param name="offset">The offset of orders to fetch</param>
        /// <param name="perPage">How many orders to return per page</param>
        /// <param name="orderBy">Order sort by</param>
        /// <param name="order">Sort orders ascending or descending</param>
        /// <param name="orderStatus">The status of the order</param>
        /// <param name="optionalParameters">Any optional URL parameters</param>
        /// <returns>A list of orders. (Returns null if it fails to retrieve anything, returns empty array if there were no results)</returns>
        public async Task<List<Order>> MultiFetch(
            Dictionary<string, string> optionalParameters,
            int page = 1,
            int offset = 0,
            int perPage = 10,
            OrderOrderBy orderBy = OrderOrderBy.Date,
            SortDirection order = SortDirection.Descending,
            OrderStatus orderStatus = OrderStatus.Any)
        {
            if (page < 1)
                page = 1;

            if (perPage < 1)
                perPage = 10;

            string optionalParams = "";

            if (optionalParameters != null)
                optionalParams = string.Join("&", optionalParameters.Select(x => x.Key + "=" + x.Value).ToArray());

            string url = $"{WcObject.host}/wp-json/wc/v3/orders?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}&{optionalParams}";

            if (orderStatus != OrderStatus.Any)
                url += $"&status={OrderStatusMapper.GetValue(orderStatus)}";

            url += $"&per_page={perPage}";

            if (offset != 0)
                url += $"&offset={offset}";

            if (page != 1)
                url += $"&page={page}";

            url += $"&orderby={OrderOrderByMapper.GetValue(orderBy)}";
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
                    List<Order> orders =
                        JsonSerializer.Deserialize<List<Order>>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());

                    return orders ?? new List<Order>();
                }

                Console.WriteLine($"Failed fetching all orders from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }
            return null;
        }

        /// <summary>
        /// Fetch order based on order id
        /// </summary>
        /// <param name="orderId">The order id to look for</param>
        /// <returns>The actual order class, if it can't find the order it returns null.</returns>
        public async Task<Order> Fetch(long orderId)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/orders/{orderId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<Order>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());

                Console.WriteLine($"Failed fetching an order from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }

            return null;
        }

        /// <summary>
        /// Create new order on WooCommerce with supplied order object
        /// </summary>
        /// <param name="order">Order object to place on WooCommerce</param>
        /// <returns>Returns the actual order that's added to WooCommerce</returns>
        public async Task<Order> Create(Order order)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/orders?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(order, this.GetJsonSerializerOptions()))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                Console.Write($"Create order: {response.StatusCode} ");

                if (response.IsSuccessStatusCode)
                {
                    Order o = JsonSerializer.Deserialize<Order>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                    Console.WriteLine($"{o.id}");
                    return o;
                }

                Console.WriteLine($"\nFailed creating order on WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
                Console.WriteLine(" - Input towards WooCommerce: " + JsonSerializer.Serialize(order, this.GetJsonSerializerOptions()));
            }
            return null;
        }

        /// <summary>
        /// Search for an order with a specific meta-data key and meta-data value. (WARNING! This search is limited to the 100 latest orders listed on the WooCommerce host!)
        /// </summary>
        /// <param name="metaKey">The meta-data key you want to look for.</param>
        /// <param name="metaValue">The value of what the meta-data key should contain.</param>
        /// <param name="pages">Amount of pages to fetch to search through</param>
        /// <returns>A list of matching orders. (Returns null if failed to fetch any orders)</returns>
        public async Task<List<Order>> MetaSearch(string metaKey, string metaValue, int pages = 2)
        {
            List<Order> orders = new List<Order>();
            for (int i = 0; i < pages; i++)
            {
                List<Order> os = await MultiFetch(
                    null,
                    perPage: 100,
                    page: i + 1,
                    orderBy: OrderOrderBy.Date,
                    order: SortDirection.Descending,
                    orderStatus: OrderStatus.Any
                );

                if (os != null)
                    orders.AddRange(os);
            }
            return orders.Count > 0 ? orders?.Where(x => x.meta_data.Any(y => y.key == metaKey && y.value.ToString() == metaValue)).ToList() : orders;
        }
    }
}