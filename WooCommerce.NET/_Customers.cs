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
    public class _Customers : WooCommerceSection
    {
        private WCObject WcObject { get; set; }

        public _Customers(WCObject wcObject)
        {
            this.WcObject = wcObject;
        }
        
        /// <summary>
        /// Fetch a single customer based on customer id
        /// </summary>
        /// <param name="customerId">Customer id to search for</param>
        /// <returns>A customer object</returns>
        public async Task<Customer> Fetch(long customerId)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/customers/{customerId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"), 
                Headers =
                {
                    { "Accept", "application/json" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<Customer>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                
                Console.WriteLine($"Failed fetching a customer from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }

            return null;
        }
        
        /// <summary>
        /// Update a customer, expects the customer id in an empty customer class with only the values that need changing.
        /// </summary>
        /// <param name="customer">A customer class with all values that need to be updated on WooCommercce</param>
        /// <returns>Success true/false</returns>
        public async Task<bool> Update(Customer customer)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/customers/{customer.id}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(customer, new JsonSerializerOptions(){ WriteIndented = false }))
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
        /// Fetch multiple customers from woocommerce
        /// </summary>
        /// <param name="page">The current page to fetch</param>
        /// <param name="offset">The offset of customers to fetch</param>
        /// <param name="perPage">How many customers to return per page</param>
        /// <param name="email">Filter based on email</param>
        /// <param name="role">Filter based on user role</param>
        /// <param name="orderBy">Order sort by</param>
        /// <param name="order">Sort customers ascending or descending</param>
        /// <param name="optionalParameters">Any optional URL parameters</param>
        /// <returns>A list of customers. (Returns null if it fails to retrieve anything, returns empty array if there were no results)</returns>
        public async Task<List<Customer>> MultiFetch(
            int page = 1,
            int offset = 0,
            int perPage = 10,
            string email = null,
            CustomerRole role = CustomerRole.Customer,
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

            string url = $"{WcObject.host}/wp-json/wc/v3/customers?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}&{optionalParams}";

            url += $"&per_page={perPage}";
            
            if (!string.IsNullOrEmpty(email))
                url += $"&email={email}";
            
            if (offset != 0)
                url += $"&offset={offset}";
            
            if (page != 1)
                url += $"&page={page}";
            
            url += $"&order={SortDirectionMapper.GetValue(order)}";
            url += $"&role={CustomerRoleMapper.GetValue(role)}";

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
                    List<Customer> customers =
                        JsonSerializer.Deserialize<List<Customer>>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());

                    return customers ?? new List<Customer>();
                }
                
                Console.WriteLine($"Failed fetching all customers from WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
            }
            return null;
        }

        /// <summary>
        /// Delete a customer from WooCommerce
        /// </summary>
        /// <param name="customerId">The customer id to delete</param>
        /// <param name="reassign">User ID to reassign posts to</param>
        /// <returns>Success true/false</returns>
        public async Task<bool> Delete(long customerId, long reassign = 0)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/customers/{customerId}?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}&force=true" + (reassign != 0 ? $"&reassign={reassign}" : "")), 
            };
            
            using var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        
        /// <summary>
        /// Create new customer on WooCommerce with supplied customer object
        /// </summary>
        /// <param name="customer">Customer object to place on WooCommerce</param>
        /// <returns>Returns the actual customer that's added to WooCommerce</returns>
        public async Task<Customer> Create(Customer customer)
        {
            HttpClient client = WcObject.PrepareHttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{WcObject.host}/wp-json/wc/v3/customers?consumer_key={WcObject.key}&consumer_secret={WcObject.secret}"),
                Content = new StringContent(JsonSerializer.Serialize(customer, new JsonSerializerOptions(){ WriteIndented = false }))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                Console.Write($"Create customer: {response.StatusCode} ");

                if (response.IsSuccessStatusCode)
                {
                    Customer c = JsonSerializer.Deserialize<Customer>(await response.Content.ReadAsStringAsync(), this.GetJsonSerializerOptions());
                    Console.WriteLine($"{c.id}");
                    return c;
                }
                
                Console.WriteLine($"\nFailed creating customer on WooCommerce:\n - Status code: {response.StatusCode}\n - Reason: {response.ReasonPhrase}\n - Response text: {await response.Content.ReadAsStringAsync()}");
                Console.WriteLine(" - Input towards WooCommerce: " + JsonSerializer.Serialize(customer, this.GetJsonSerializerOptions()));
            }
            return null;
        }
    }
}