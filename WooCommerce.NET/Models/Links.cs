using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WooCommerce.NET
{
    public class Links
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<Self> self { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<Collection> collection { get; set; }
    }
}