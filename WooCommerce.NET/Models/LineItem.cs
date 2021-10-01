using System.Collections.Generic;

namespace WooCommerce.NET
{
    public class LineItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int product_id { get; set; }
        public int variation_id { get; set; }
        public int quantity { get; set; }
        public string tax_class { get; set; }
        public string subtotal { get; set; }
        public string subtotal_tax { get; set; }
        public decimal total { get; set; }
        public string total_tax { get; set; }
        public List<Tax> taxes { get; set; }
        public List<MetaData> meta_data { get; set; }
        public object sku { get; set; }
        public decimal price { get; set; }
    }
}