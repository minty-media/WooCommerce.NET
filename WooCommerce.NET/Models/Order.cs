using System;
using System.Collections.Generic;

namespace WooCommerce.NET
{
    public class Order
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public string version { get; set; }
        public bool prices_include_tax { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_modified { get; set; }
        public string discount_total { get; set; }
        public string discount_tax { get; set; }
        public string shipping_total { get; set; }
        public string shipping_tax { get; set; }
        public string cart_tax { get; set; }
        public string total { get; set; }
        public string total_tax { get; set; }
        public int customer_id { get; set; }
        public string order_key { get; set; }
        public Billing billing { get; set; }
        public Shipping shipping { get; set; }
        public string payment_method { get; set; }
        public string payment_method_title { get; set; }
        public string transaction_id { get; set; }
        public string customer_ip_address { get; set; }
        public string customer_user_agent { get; set; }
        public string created_via { get; set; }
        public string customer_note { get; set; }
        public DateTime date_completed { get; set; }
        public DateTime date_paid { get; set; }
        public string cart_hash { get; set; }
        public string number { get; set; }
        public List<MetaData> meta_data { get; set; }
        public List<LineItem> line_items { get; set; }
        public List<TaxLine> tax_lines { get; set; }
        public List<ShippingLine> shipping_lines { get; set; }
        public List<object> fee_lines { get; set; }
        public List<object> coupon_lines { get; set; }
        public List<object> refunds { get; set; }
        public DateTime date_created_gmt { get; set; }
        public DateTime date_modified_gmt { get; set; }
        public DateTime date_completed_gmt { get; set; }
        public DateTime date_paid_gmt { get; set; }
        public string currency_symbol { get; set; }
        public Links _links { get; set; }
        public bool set_paid { get; set; }
    }
}