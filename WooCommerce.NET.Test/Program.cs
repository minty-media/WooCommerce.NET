using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WooCommerce.NET.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WooCommerce wooCommerce = new WooCommerce("https://mintybol-api.myio.nl",
                "ck_ebf92e16e4e44edd6b6bbaa17f91da9900bb6a5b", "cs_1eab44a4c963690e870765b998cb625e99b026bb");
            
            Order order = await wooCommerce.Orders.Create(new Order()
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