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
        }

        [Test]
        public async Task UpdateOrder()
        {
            //Place an order to try update
            Order order = await PlaceDummyOrder();
            
            Assert.IsTrue(await _wooCommerce.Orders.Update(new Order()
            {
                id = order.id,
                status = "completed"
            }));
            
            // Try fetching the order
            Order o = await _wooCommerce.Orders.Fetch(order.id);
            Assert.AreEqual(o.status, "completed");
            
            // Try delete an order
            await _wooCommerce.Orders.Delete(order.id, true);
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
            Assert.IsNotEmpty(await _wooCommerce.Orders.FetchAll());
        }
        
        [Test]
        public async Task FetchOrder()
        {
            //Place an order to try fetch
            Order order = await PlaceDummyOrder();
            
            // Try fetching the order
            Order o = await _wooCommerce.Orders.Fetch(order.id);
            
            // Clean things up
            await _wooCommerce.Orders.Delete(o.id, true);
            
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

        private async Task<Order> PlaceDummyOrder()
        {
            return await _wooCommerce.Orders.Create(new Order()
            {
                payment_method = "Bol.com",
                payment_method_title = "Bol.com koppeling",
                set_paid = true,
                billing = new CustomerInfo()
                {
                    first_name = "Pawel",
                    last_name = "TEST",
                    address_1 = "Mollerusweg 82",
                    city = "Haarlem",
                    postcode = "2020 AB",
                    country = "NL",
                    company = "Minty Media"
                },
                shipping = new CustomerInfo()
                {
                    first_name = "Pawel",
                    last_name = "TEST",
                    address_1 = "Mollerusweg 82",
                    city = "Haarlem",
                    postcode = "2020 AB",
                    country = "NL",
                    company = "Minty Media"
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
                        key = "_btw",
                        value = "dsfghjk"
                    }
                }
            });
        }
    }
}