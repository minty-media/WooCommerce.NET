using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Convertors
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            DateTime.ParseExact(reader.GetString() ?? "1970-01-01T00:00:00", "yyyy-MM-ddTHH\\:mm\\:ss", null); //DateTime.ParseExact(reader.GetString(), "o", null);
        
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH\\:mm\\:ss")); //writer.WriteStringValue(value.ToString("o"));
    }
}