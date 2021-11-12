using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WooCommerce.NET.Models
{
    public class Customer
    {
        [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int id { get; set; }

        [JsonPropertyName("date_created"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string date_created { get; set; }

        [JsonPropertyName("date_created_gmt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string date_created_gmt { get; set; }

        [JsonPropertyName("date_modified"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string date_modified { get; set; }

        [JsonPropertyName("date_modified_gmt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string date_modified_gmt { get; set; }

        [JsonPropertyName("email"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string email { get; set; }

        [JsonPropertyName("first_name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string first_name { get; set; }

        [JsonPropertyName("last_name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string last_name { get; set; }

        [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string role { get; set; }

        [JsonPropertyName("username"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string username { get; set; }

        [JsonPropertyName("password"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string password { get; set; }

        [JsonPropertyName("billing"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public BillingShippingInfo billing { get; set; }

        [JsonPropertyName("shipping"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public BillingShippingInfo shipping { get; set; }

        [JsonPropertyName("is_paying_customer"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool is_paying_customer { get; set; }

        [JsonPropertyName("avatar_url"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string avatar_url { get; set; }

        [JsonPropertyName("meta_data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<MetaData> meta_data { get; set; }

        [JsonPropertyName("_links"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links links { get; set; }
    }
}