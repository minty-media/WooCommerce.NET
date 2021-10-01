using System.Collections.Generic;

namespace WooCommerce.NET
{
    public class ShippingLine
    {
        public int id { get; set; }
        public string method_title { get; set; }
        public string method_id { get; set; }
        public string instance_id { get; set; }
        public string total { get; set; }
        public string total_tax { get; set; }
        public List<Tax> taxes { get; set; }
        public List<MetaData> meta_data { get; set; }
    }
}