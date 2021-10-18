using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooCommerce.NET.Models;

namespace WooCommerce.NET.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WCObject _wcObject = new WCObject(Environment.GetEnvironmentVariable("WOO_HOST"), 
                    Environment.GetEnvironmentVariable("WOO_KEY"), 
                    Environment.GetEnvironmentVariable("WOO_SECRET"));

            _wcObject.userAgent = "WooCommerce.NET/1.0.0 (linux; ubuntu20.04)";

            Order order = await _wcObject.Orders.Create(new Order()
            {
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
                },
                line_items = new List<LineItem>()
                {
                    new()
                    {
                        product_id = 25,
                        quantity = 4,
                        price = 15
                    },
                    new()
                    {
                        product_id = 18,
                        quantity = 2,
                        price = 55
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