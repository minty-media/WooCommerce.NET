using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using WooCommerce.NET.Models;

namespace WooCommerce.NET.Tests
{
    public class CustomerTests
    {
        private WooCommerce _wooCommerce;
        
        [SetUp]
        public void Setup()
        {
            _wooCommerce = new WooCommerce(Environment.GetEnvironmentVariable("WOO_HOST"), 
                Environment.GetEnvironmentVariable("WOO_KEY"), 
                Environment.GetEnvironmentVariable("WOO_SECRET"));

            _wooCommerce.userAgent = "WooCommerce.NET/1.0.0 (linux; ubuntu20.04)";
        }
        
        
        [Test]
        public async Task MultiFetchCustomers()
        {
            List<Customer> customers = new List<Customer>();
            Random rnd = new Random();
            for (int i = 0; i <= 5; i++)
                customers.Add(await PlaceDummyCustomer(rnd));
            
            List<Customer> customersFetched = await _wooCommerce.Customers.MultiFetch(role: CustomerRole.All);
            
            foreach (Customer c in customers)
                Assert.IsTrue(await _wooCommerce.Customers.Delete(c.id));
            
            Assert.IsTrue(customersFetched.Count >= 5);
        }
        
        [Test]
        public async Task FetchCustomer()
        {
            //Place an customer to try fetch
            Customer customer = await PlaceDummyCustomer(new Random());
            Assert.IsNotNull(customer);

            // Try fetching the customer
            Customer c = await _wooCommerce.Customers.Fetch(customer.id);
            Assert.IsNotNull(c);
            
            // Clean up our shit and delete the used customer
            bool success = await _wooCommerce.Customers.Delete(customer.id);
            Assert.IsTrue(success);
        }
        
        [Test]
        public async Task CreateCustomer()
        {
            Customer customer = await PlaceDummyCustomer(new Random());
            Assert.IsNotNull(customer);
            
            // Try delete an customer
            bool success = await _wooCommerce.Customers.Delete(customer.id);
            Assert.IsTrue(success);
        }
        
        [Test]
        public async Task UpdateCustomer()
        {
            Customer customer = await PlaceDummyCustomer(new Random());
            Assert.IsNotNull(customer);

            Assert.IsTrue(await _wooCommerce.Customers.Update(new Customer()
            {
                id = customer.id,
                first_name = "Updated first_name",
                last_name = "Updated last_name",
                billing = new BillingShippingInfo()
                {
                    city = "Updated city"
                }
            }));
            Customer c = await _wooCommerce.Customers.Fetch(customer.id);

            Assert.AreEqual(c.first_name, "Updated first_name");
            Assert.AreEqual(c.last_name, "Updated last_name");
            Assert.AreEqual(c.billing.city, "Updated city");
            
            // Try delete an customer
            bool success = await _wooCommerce.Customers.Delete(c.id);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task DeleteCustomer()
        {
            //Place an customer to try fetch
            Customer customer = await PlaceDummyCustomer(new Random());
            
            // Try delete an customer
            bool success = await _wooCommerce.Customers.Delete(customer.id);
            Assert.IsTrue(success);
        }

        public async Task<Customer> PlaceDummyCustomer(Random rnd)
        {
            return await _wooCommerce.Customers.Create(new Customer()
            {
                first_name = "John",
                last_name = "Dapper",
                username = $"dapperjohn{rnd.Next(0, 100000)}",
                email = $"johndapper{rnd.Next(0, 100000)}@example.com",
                billing = new BillingShippingInfo()
                {
                    first_name = "John",
                    last_name = "Dapper",
                    address_1 = "Dappstreet 69",
                    city = "Dogetown",
                    postcode = "42069",
                    country = "US",
                    company = "Doge Corp."
                },
                shipping = new BillingShippingInfo()
                {
                    first_name = "John",
                    last_name = "Dapper",
                    address_1 = "Dappstreet 69",
                    city = "Dogetown",
                    postcode = "42069",
                    country = "US",
                    company = "Doge Corp."
                }
            });
        }
    }
}