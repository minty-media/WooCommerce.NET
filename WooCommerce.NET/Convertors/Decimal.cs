using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Convertors
{
    public class DecimalJsonConvertor : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.GetString() == "" || reader.GetString() == null)
                    return 0;

                return string.IsNullOrEmpty(reader.GetString()) ? 0 : Convert.ToDecimal(reader.GetString());
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDecimal();
            }
            else
                return 0;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}