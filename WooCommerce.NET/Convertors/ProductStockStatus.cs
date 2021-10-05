using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using WooCommerce.NET.Models;

namespace WooCommerce.NET.Convertors
{
    public class ProductStockStatusJsonConvertor : JsonConverter<ProductStockStatus>
    {
        public override ProductStockStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            switch (value)
            {
                case "instock":
                    return ProductStockStatus.InStock;
                case "outofstock":
                    return ProductStockStatus.OutOfStock;
                case "onbackorder":
                    return ProductStockStatus.BackOrder;
                default:
                    return ProductStockStatus.OutOfStock;
            }
        }
        
        public override void Write(Utf8JsonWriter writer, ProductStockStatus value, JsonSerializerOptions options) =>
            writer.WriteStringValue(ProductStockStatusMapper.GetValue(value));
    }
}