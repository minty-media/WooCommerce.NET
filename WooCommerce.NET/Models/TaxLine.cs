using System.Collections.Generic;

namespace WooCommerce.NET
{
    public class TaxLine
    {
        public int id { get; set; }
        public string rate_code { get; set; }
        public int rate_id { get; set; }
        public string label { get; set; }
        public bool compound { get; set; }
        public string tax_total { get; set; }
        public string shipping_tax_total { get; set; }
        public int rate_percent { get; set; }
        public List<MetaData> meta_data { get; set; }
    }
}