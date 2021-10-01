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
            _wooCommerce = new WooCommerce("https://mintybol-api.myio.nl",
                "ck_ebf92e16e4e44edd6b6bbaa17f91da9900bb6a5b", "cs_1eab44a4c963690e870765b998cb625e99b026bb");
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
                status = "completed",
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