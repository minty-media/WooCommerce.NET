using System.Text.Json.Serialization;

namespace WooCommerce.NET
{
    public class Self
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string href { get; set; }
    }
}