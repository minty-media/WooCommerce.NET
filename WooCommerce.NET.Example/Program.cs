using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooCommerce.NET.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WooCommerce _wooCommerce = new WooCommerce(Environment.GetEnvironmentVariable("WOO_HOST"), 
                    Environment.GetEnvironmentVariable("WOO_KEY"), 
                    Environment.GetEnvironmentVariable("WOO_SECRET"));

            _wooCommerce.userAgent = "WooCommerce.NET/1.0.0 (linux; ubuntu20.04)";

            List<Order> orders = await _wooCommerce.Orders.MetaSearch("_bol_orderId", "9742011");
        }
    }
}