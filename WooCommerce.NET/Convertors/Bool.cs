using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Convertors
{
    public class BoolJsonConvertor : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                return reader.GetString() == "true";
            
            return reader.GetBoolean();
        }
        
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
            writer.WriteBooleanValue(value);
    }
}