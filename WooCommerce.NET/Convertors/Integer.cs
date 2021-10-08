using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Convertors
{
    public class IntegerJsonConvertor : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            bool s = reader.TryGetInt32(out int value);
            return s ? value : 0;
        }
        
        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options) =>
            writer.WriteNumberValue(value == null ? 0 : (int)value);
    }
}