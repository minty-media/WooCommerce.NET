using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using WooCommerce.NET.Models;

namespace WooCommerce.NET.Convertors
{
    public class ProductStatusJsonConvertor : JsonConverter<ProductStatus>
    {
        public override ProductStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            switch (value)
            {
                case "publish":
                    return ProductStatus.Published;
                case "draft":
                    return ProductStatus.Draft;
                case "private":
                    return ProductStatus.Private;
                case "pending":
                    return ProductStatus.Pending;
                default:
                    return ProductStatus.Draft;
            }
        }

        public override void Write(Utf8JsonWriter writer, ProductStatus value, JsonSerializerOptions options) =>
            writer.WriteStringValue(ProductStatusMapper.GetValue(value));
    }
}