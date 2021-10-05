using System.Text.Json;
using WooCommerce.NET.Convertors;

namespace WooCommerce.NET
{
    public class WooCommerceSection
    {
        public JsonSerializerOptions GetJsonSerializerOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
                    
            options.Converters.Add(new CustomDateTimeConverter());
            options.Converters.Add(new ProductStockStatusJsonConvertor());
            options.Converters.Add(new DecimalJsonConvertor());
            options.Converters.Add(new ProductTypeJsonConvertor());
            options.WriteIndented = false;
            
            return options;
        }
    }
}