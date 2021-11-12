using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Convertors
{
    public class IntegerJsonConvertor : JsonConverter<Int32>
    {
        public override Int32 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return 0;

            if (reader.TokenType == JsonTokenType.String)
                return 0;

            bool s = reader.TryGetInt32(out int value);
            return s ? value : 0;
        }

        public override void Write(Utf8JsonWriter writer, Int32 value, JsonSerializerOptions options) =>
            writer.WriteNumberValue(value == null ? 0 : value);
    }
}