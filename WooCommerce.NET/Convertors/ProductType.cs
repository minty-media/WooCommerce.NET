using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using WooCommerce.NET.Models;

namespace WooCommerce.NET.Convertors
{
    public class ProductTypeJsonConvertor : JsonConverter<ProductType>
    {
        public override ProductType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            switch (value)
            {
                case "simple":
                    return ProductType.Simple;
                case "grouped":
                    return ProductType.Grouped;
                case "external":
                    return ProductType.External;
                case "variable":
                    return ProductType.Variable;
                default:
                    return ProductType.Simple;
            }
        }

        public override void Write(Utf8JsonWriter writer, ProductType value, JsonSerializerOptions options) =>
            writer.WriteStringValue(ProductTypeMapper.GetValue(value));
    }
}