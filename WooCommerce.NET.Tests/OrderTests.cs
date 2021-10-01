using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WooCommerce.NET.Tests
{
    public class Tests
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
        public async Task UpdateOrder()
        {
            //Place an order to try update
            Order order = await PlaceDummyOrder();

            Assert.IsTrue(await _wooCommerce.Orders.Update(new Order()
            {
                id = order.id,
                set_paid = true,
                status = "bol"
            }));
            
            // Try fetching the order
            Order o = await _wooCommerce.Orders.Fetch(order.id);
            Assert.AreEqual(o.status, "bol");
            
            // Clean up our shit and delete the used order
            bool success = await _wooCommerce.Orders.Delete(order.id, true);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task CreateOrder()
        {
            Order order = await PlaceDummyOrder();
            Assert.IsNotNull(order);
        }
        
        [Test]
        public async Task FetchAllOrders()
        {
            List<Order> orders = new List<Order>();
            for (int i = 0; i <= 5; i++)
                orders.Add(await PlaceDummyOrder());
            
            Assert.IsTrue((await _wooCommerce.Orders.MultiFetch()).Count >= 5);

            foreach (Order o in orders)
                Assert.IsTrue(await _wooCommerce.Orders.Delete(o.id, true));
        }
        
        [Test]
        public async Task FetchOrder()
        {
            //Place an order to try fetch
            Order order = await PlaceDummyOrder();
            
            // Try fetching the order
            Order o = await _wooCommerce.Orders.Fetch(order.id);
            
            // Clean up our shit and delete the used order
            bool success = await _wooCommerce.Orders.Delete(order.id, true);
            Assert.IsTrue(success);
            
            Assert.IsNotNull(o);
        }

        [Test]
        public async Task DeleteOrder()
        {
            //Place an order to try fetch
            Order order = await PlaceDummyOrder();
            
            // Try delete an order
            bool success = await _wooCommerce.Orders.Delete(order.id, true);
            Assert.IsTrue(success);
        }
        
        [Test]
        public async Task SearchOrderWithMetaData()
        {
            //Place an order to try fetch
            Order order = await PlaceDummyOrder();
            Assert.IsNotEmpty(await _wooCommerce.Orders.MetaSearch("_bol_orderId", "4234546"));
            
            // Clean up our shit and delete the used order
            bool success = await _wooCommerce.Orders.Delete(order.id, true);
            Assert.IsTrue(success);
        }

        private async Task<Order> PlaceDummyOrder()
        {
            return await _wooCommerce.Orders.Create(new Order()
            {
                status = "processing",
                billing = new CustomerInfo()
                {
                    first_name = "John",
                    last_name = "Dapper",
                    address_1 = "Dappstreet 69",
                    city = "Dogetown",
                    postcode = "42069",
                    country = "US",
                    company = "Doge Corp."
                },
                shipping = new CustomerInfo()
                {
                    first_name = "John",
                    last_name = "Dapper",
                    address_1 = "Dappstreet 69",
                    city = "Dogetown",
                    postcode = "42069",
                    country = "US",
                    company = "Doge Corp."
                },
                line_items = new List<LineItem>()
                {
                    new()
                    {
                        product_id = 25,
                        quantity = 4
                    },
                    new()
                    {
                        product_id = 18,
                        quantity = 2
                    }
                },
                meta_data = new List<MetaData>()
                {
                    new ()
                    {
                        key = "_bol_orderId",
                        value = "4234546"
                    }
                }
            });
        }
    }
}