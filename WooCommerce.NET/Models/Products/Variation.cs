using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Models
{
    public class Variation
    {
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [JsonPropertyName("date_created")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("date_created_gmt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DateCreatedGmt { get; set; }

        [JsonPropertyName("date_modified")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DateModified { get; set; }

        [JsonPropertyName("date_modified_gmt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DateModifiedGmt { get; set; }

        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Description { get; set; }

        [JsonPropertyName("permalink")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Permalink { get; set; }

        [JsonPropertyName("sku")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Sku { get; set; }

        [JsonPropertyName("price")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Price { get; set; }

        [JsonPropertyName("regular_price")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string RegularPrice { get; set; }

        [JsonPropertyName("sale_price")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string SalePrice { get; set; }

        [JsonPropertyName("date_on_sale_from")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object DateOnSaleFrom { get; set; }

        [JsonPropertyName("date_on_sale_from_gmt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object DateOnSaleFromGmt { get; set; }

        [JsonPropertyName("date_on_sale_to")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object DateOnSaleTo { get; set; }

        [JsonPropertyName("date_on_sale_to_gmt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object DateOnSaleToGmt { get; set; }

        [JsonPropertyName("on_sale")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool OnSale { get; set; }

        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ProductStatus Status { get; set; }

        [JsonPropertyName("purchasable")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Purchasable { get; set; }

        [JsonPropertyName("virtual")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Virtual { get; set; }

        [JsonPropertyName("downloadable")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Downloadable { get; set; }

        [JsonPropertyName("downloads")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<object> Downloads { get; set; }

        [JsonPropertyName("download_limit")]
        public int DownloadLimit { get; set; }

        [JsonPropertyName("download_expiry")]
        public int DownloadExpiry { get; set; }

        [JsonPropertyName("tax_status")]
        public string TaxStatus { get; set; }

        [JsonPropertyName("tax_class")]
        public string TaxClass { get; set; }

        [JsonPropertyName("manage_stock")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ManageStock { get; set; }

        [JsonPropertyName("stock_quantity")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int StockQuantity { get; set; }

        [JsonPropertyName("stock_status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ProductStockStatus StockStatus { get; set; }

        [JsonPropertyName("backorders")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Backorders { get; set; }

        [JsonPropertyName("backorders_allowed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool BackordersAllowed { get; set; }

        [JsonPropertyName("backordered")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Backordered { get; set; }

        [JsonPropertyName("weight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Weight { get; set; }

        [JsonPropertyName("dimensions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Dimensions Dimensions { get; set; }

        [JsonPropertyName("shipping_class")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ShippingClass { get; set; }

        [JsonPropertyName("shipping_class_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ShippingClassId { get; set; }

        [JsonPropertyName("image")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Image Image { get; set; }

        [JsonPropertyName("attributes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<Attribute> Attributes { get; set; }

        [JsonPropertyName("menu_order")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int MenuOrder { get; set; }

        [JsonPropertyName("meta_data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<object> MetaData { get; set; }

        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links Links { get; set; }
    }
}