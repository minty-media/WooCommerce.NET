using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using WooCommerce.NET.Models;

namespace WooCommerce.NET.Tests
{
    public class ProductTests
    {
        private WCObject _wcObject;
        
        [SetUp]
        public void Setup()
        {
            _wcObject = new WCObject(Environment.GetEnvironmentVariable("WOO_HOST"), 
                Environment.GetEnvironmentVariable("WOO_KEY"), 
                Environment.GetEnvironmentVariable("WOO_SECRET"));

            _wcObject.userAgent = "WooCommerce.NET/1.0.0 (linux; ubuntu20.04)";
        }
        
        
        [Test]
        public async Task MultiFetchProducts()
        {
            List<Product> products = new List<Product>();
            Random rnd = new Random();
            for (int i = 0; i <= 5; i++)
                products.Add(await PlaceDummyProduct(rnd));
            
            List<Product> productsFetched = await _wcObject.Products.MultiFetch();
            
            foreach (Product c in products)
                Assert.IsTrue(await _wcObject.Products.Delete(c.id));
            
            Assert.IsTrue(productsFetched.Count >= 5);
        }
        
        [Test]
        public async Task FetchProduct()
        {
            //Place an product to try fetch
            Product product = await PlaceDummyProduct(new Random());
            Assert.IsNotNull(product);

            // Try fetching the product
            Product c = await _wcObject.Products.Fetch(product.id);
            Assert.IsNotNull(c);
            
            // Clean up our shit and delete the used product
            bool success = await _wcObject.Products.Delete(product.id);
            Assert.IsTrue(success);
        }
        
        [Test]
        public async Task CreateProduct()
        {
            Product product = await PlaceDummyProduct(new Random());
            Assert.IsNotNull(product);
            
            // Try delete an product
            bool success = await _wcObject.Products.Delete(product.id);
            Assert.IsTrue(success);
        }
        
        [Test]
        public async Task UpdateProduct()
        {
            Product product = await PlaceDummyProduct(new Random());
            Assert.IsNotNull(product);

            Assert.IsTrue(await _wcObject.Products.Update(new Product()
            {
                id = product.id,
                name = "Updated product name",
                stock_quantity = 70
            }));
            Product c = await _wcObject.Products.Fetch(product.id);

            Assert.AreEqual(c.name, "Updated product name");
            Assert.AreEqual(c.stock_quantity, 70);
            
            // Try delete an product
            bool success = await _wcObject.Products.Delete(c.id);
            Assert.IsTrue(success);
        }

        [Test]
        public async Task DeleteProduct()
        {
            //Place an product to try fetch
            Product product = await PlaceDummyProduct(new Random());
            
            // Try delete an product
            bool success = await _wcObject.Products.Delete(product.id);
            Assert.IsTrue(success);
        }

        public async Task<Product> PlaceDummyProduct(Random rnd)
        {
            string sku = rnd.Next(0, 100000000).ToString();
            return await _wcObject.Products.Create(new Product()
            {
                sku = sku,
                name = $"Test product {sku}",
                regular_price = 12.99m,
                sale_price = 10.99m,
                on_sale = true,
                manage_stock = true,
                stock_status = ProductStockStatus.InStock,
                stock_quantity = 69,
                type = ProductType.Variable
            });
        }
    }
}