﻿using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Convertors
{
    public class StringJsonConvertor : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return "";

            try
            {
                String jsonString = reader.GetString();
                return jsonString;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value);
    }
}
